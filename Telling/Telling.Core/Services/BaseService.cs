using Cheesebaron.MvxPlugins.Settings.Interfaces;
using MvvmCross.Platform;
using Newtonsoft.Json;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telling.Core.Interfaces;
using Telling.Core.Models;
using Telling.Core.Models.Responses;
using static Telling.Core.Constants;

namespace Telling.Core.Services
{
    //public abstract class BaseService
    //{
    //    protected const string API_URL = "http://telling.fullstack.co.za/api";

    //    protected IRestService RestService { get; private set; }

    //    protected BaseService(IRestService restService)
    //    {
    //        RestService = restService;
    //    }

    //    //protected static void AddParamsVersion(Dictionary<string, object> parameters, int versionNumber = 1)
    //    //{
    //    //    parameters.Add("version", versionNumber);
    //    //}
    //}

    public interface IBaseService
    {
    }

    public abstract class BaseService : IBaseService
    {
        internal readonly ISettings _settings;
        //internal readonly IEventLogger _eventLogger;
        internal readonly IConnectivityService _connectivityService;
        internal CancellationTokenSource CancelationSource;
        //internal static string AccessToken;

        protected BaseService(ISettings settings)
        {
            _settings = settings;
            //_eventLogger = Mvx.Resolve<IEventLogger>();
            _connectivityService = Mvx.Resolve<IConnectivityService>();
        }

        internal HttpClient CreateHttpClient()
        {
            var client = new HttpClient(new CustomMessageHandler());

            // Timeout cancellation token
            CancelationSource = new CancellationTokenSource();
            CancelationSource.CancelAfter(60000);
            //var accessToken = _settings.GetValue(Constants.SETTING_TOKEN, "");
            //if (accessToken?.Length > 0)
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //    client.DefaultRequestHeaders.Add("Accept", "application/json ; odata.metadata = none");
            //}

            client.Timeout = TimeSpan.FromSeconds(60);

            return client;
        }


        #region Handle API Responses

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResp"></typeparam>
        /// <param name="message"></param>
        /// <param name="responseString"></param>
        /// <param name="responseType"></param>
        /// <param name="exceptionMessage"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        static internal BaseResponse<TResp> HandleApiCalls<TResp>(string message, string responseString, ServiceReponseType responseType, HttpStatusCode statusCode, string exceptionMessage = null)
             where TResp : class
        {
            return new BaseResponse<TResp>
            {
                ReponseType = responseType,
                Message = message,
                ExceptionMessage = exceptionMessage,
                Result = responseString == null ? null : JsonConvert.DeserializeObject<TResp>(responseString),
                StatusCode = statusCode
            };
        }

        /// <summary>
        /// Handle the Sanlam success flow.
        /// </summary>
        /// <typeparam name="TResp">The type of the response.</typeparam>
        /// <param name="successMessage">The success message.</param>
        /// <param name="responseString">The response string to deserialized.</param>
        /// <param name="statusCode"></param>
        /// <returns>BaseResp with result of type TResp.</returns>
        static internal BaseResponse<TResp> HandleSuccess<TResp>(string successMessage, string responseString, HttpStatusCode statusCode)
             where TResp : class
        {
            return HandleApiCalls<TResp>(successMessage, responseString, ServiceReponseType.Successful, statusCode);
        }

        /// <summary>
        /// Handle the Sanlam error flow.
        /// </summary>
        /// <typeparam name="TResp">The type of the response.</typeparam>
        /// <param name="httpType">Type of the HTTP.</param>
        /// <param name="endpoint">The URL.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="errorMessageException">The error message exception.</param>
        /// <param name="statusCode"></param>
        /// <returns>BaseResp with a null result.</returns>
        internal BaseResponse<TResp> HandleError<TResp>(string httpType, Endpoint endpoint, string errorMessage, string errorMessageException, HttpStatusCode statusCode)
             where TResp : class
        {
            Mvx.Error("ERROR on Sanlam Service [" + httpType + " : " + endpoint.Url + "] Message : {0} >>>>> Exception: {1}", errorMessage, errorMessageException);

            //_eventLogger.TrackEvent(Constants.ERROR_EVENT_NAME, endpoint.UrlAction, $"{statusCode.ToString()} || {errorMessage}");

            return HandleApiCalls<TResp>(errorMessage, null, ServiceReponseType.Error, statusCode, errorMessageException);
        }

        /// <summary>
        /// Handle the timeout on the Sanlam API.
        /// </summary>
        /// <typeparam name="TResp">The type of the response.</typeparam>
        /// <param name="httpType">Type of the HTTP.</param>
        /// <param name="endpoint">Name of the host.</param>
        /// <param name="action">The action of the request..</param>
        /// <param name="exception">The exception.</param>
        /// <param name="statusCode"></param>
        /// <param name="suffix">[Optional] add a suffix to the logger.</param>
        /// <returns>BaseResp with a null result.</returns>
        internal BaseResponse<TResp> HandleTimeout<TResp>(string httpType, Endpoint endpoint, Exception exception, HttpStatusCode statusCode, string suffix = "")
             where TResp : class
        {
            Mvx.Error("TIMEOUT on Sanlam Service [" + httpType + " : URL: " + endpoint.Url + "] {0}{1}", exception.Message, suffix);

            //_eventLogger.TrackEvent(Constants.TIMEOUT_EVENT_NAME, endpoint.UrlAction, $"{statusCode.ToString()} || {exception?.Message}{ suffix }");

            return HandleApiCalls<TResp>(API_ERROR_TIMEOUT, null, ServiceReponseType.Timeout, statusCode, exception.Message);
        }

        /// <summary>
        /// Handle a crash against the Sanlam API.
        /// </summary>
        /// <typeparam name="TResp">The type of the response.</typeparam>
        /// <param name="httpType">Type of the HTTP.</param>
        /// <param name="endpoint">Name of the host.</param>
        /// <param name="action">The action of the request.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="statusCode"></param>
        /// <returns>BaseResp with a null result.</returns>
        internal BaseResponse<TResp> HandleCrash<TResp>(string httpType, Endpoint endpoint, Exception exception, HttpStatusCode statusCode)
             where TResp : class
        {
            Mvx.Error("CRASH on Sanlam Service [" + httpType + " : URL: " + endpoint.Url + "] Message: {0} >>>>> StackTrace: {1}", exception.Message, exception.StackTrace);

            //_eventLogger.TrackEvent(Constants.CRASH_EVENT_NAME, endpoint.UrlAction, $"{(statusCode == HttpStatusCode.BadRequest ? "n/a" : statusCode.ToString())} || {exception?.Message}");

            return HandleApiCalls<TResp>(API_ERROR_CRASH, null, ServiceReponseType.Error, statusCode, exception.Message);
        }

        /// <summary>
        /// Handle a lost connectivity against the Sanlam API.
        /// </summary>
        /// <typeparam name="TResp">The type of the response.</typeparam>
        /// <param name="httpType">Type of the HTTP.</param>
        /// <param name="endpoint">Name of the host.</param>
        /// <param name="action">The action of the request.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="statusCode"></param>
        /// <returns>BaseResp with a null result.</returns>
        internal BaseResponse<TResp> HandleLostConnnectivity<TResp>(string httpType, Endpoint endpoint, Exception exception, HttpStatusCode statusCode)
             where TResp : class
        {
            Mvx.Error("No Connectivity on device [" + httpType + " : URL: " + endpoint.Url + "] Message: {0} >>>>> StackTrace: {1}", exception?.Message, exception.StackTrace);

            //_eventLogger.TrackEvent(Constants.NONETWORK_EVENT_NAME, endpoint.UrlAction, $"{(statusCode == HttpStatusCode.BadRequest ? "n/a" : statusCode.ToString())} || {exception?.Message}");

            return HandleApiCalls<TResp>(API_ERROR_NONETWORK, null, ServiceReponseType.NoNetwork, statusCode, exception.Message);
        }

        #endregion

        internal async Task<BaseResponse<ReturnT>> CallToApi<ReturnT>(object request, Endpoint endpoint) where ReturnT : class
        {
            try
            {
                var connectPolicy = Policy.HandleResult<bool>(false).WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(5));
                var connected = await connectPolicy.ExecuteAsync(() => Task.FromResult<bool>(_connectivityService.IsConnected));

                if (!connected)
                    return HandleLostConnnectivity<ReturnT>(endpoint.Verb.ToString(), endpoint, new Exception("ConnectivityService determined no connectivity"), HttpStatusCode.GatewayTimeout);

                var responseStringasd = JsonConvert.SerializeObject(request);
                var policy = Policy.Handle<Exception>().WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(5));
                var response = await policy.ExecuteAsync(() => GetResponse(request, endpoint));

                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var friendlyErrorMessage = API_ERROR_NO_MESSAGE;
                    if (!string.IsNullOrEmpty(responseString) && response.StatusCode != HttpStatusCode.InternalServerError)
                    {
                        var serverErrorResponse = JsonConvert.DeserializeObject<ServerErrorResponse>(responseString);
                        friendlyErrorMessage = serverErrorResponse.Message;
                    }
                    return HandleError<ReturnT>(endpoint.Verb.ToString(), endpoint, friendlyErrorMessage, response.ReasonPhrase, response.StatusCode);
                }

                return HandleSuccess<ReturnT>(response.ReasonPhrase, responseString, response.StatusCode);
            }
            catch (OperationCanceledException ex)
            {
                return HandleTimeout<ReturnT>(endpoint.Verb.ToString(), endpoint, ex, HttpStatusCode.RequestTimeout);
            }
            catch (TimeoutException ex)
            {
                return HandleTimeout<ReturnT>(endpoint.Verb.ToString(), endpoint, ex, HttpStatusCode.GatewayTimeout);
            }
            catch (WebException ex)
            {
                return HandleLostConnnectivity<ReturnT>(endpoint.Verb.ToString(), endpoint, ex, HttpStatusCode.GatewayTimeout);
            }
            catch (Exception ex1)
            {
                // HACK [JF]: timeout on HTTP client, sometimes causes JAVA IO exception on Android
                return ex1.Message.ToLower().Equals("exception of type 'java.io.ioexception' was thrown.") ?
                    HandleTimeout<ReturnT>(endpoint.Verb.ToString(), endpoint, ex1, HttpStatusCode.RequestTimeout, " [Threw error]") :
                    HandleCrash<ReturnT>(endpoint.Verb.ToString(), endpoint, ex1, HttpStatusCode.BadRequest);
            }
        }

        internal async Task<HttpResponseMessage> GetResponse(object request, Endpoint endpoint)
        {
            HttpResponseMessage response = null;
            using (var client = CreateHttpClient())
            {
                if (endpoint.Verb == HTTP_VERB.POST)
                    response = await client.PostAsync(endpoint.ConstructSomething(), new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"), CancelationSource.Token);
                else if (endpoint.Verb == HTTP_VERB.GET)
                    response = await client.GetAsync(endpoint.ConstructSomething(), CancelationSource.Token);
                else if (endpoint.Verb == HTTP_VERB.PUT)
                    response = await client.PutAsync(endpoint.Url, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"), CancelationSource.Token);
            }

            return response;
        }
    }

    public sealed class Endpoint
    {
        ////Account Endpoints
        //public static readonly Endpoint ACCOUNT_SAVE_DEVICE_TOKEN = new Endpoint("/Odata/Account/SaveDeviceToken", HTTP_VERB.POST);

        ////Register Endpoints
        //public static readonly Endpoint REGISTER_EMAIL = new Endpoint("/Odata/Account/RegisterEmail", HTTP_VERB.POST);
        //public static readonly Endpoint REGISTER_SMS = new Endpoint("/Odata/Account/RegisterSMS", HTTP_VERB.POST);
        //public static readonly Endpoint VERIFY_EMAIL = new Endpoint("/Odata/Account/ConfirmEmail ", HTTP_VERB.POST);
        //public static readonly Endpoint VERIFY_SMS = new Endpoint("/Odata/Account/ConfirmSMS ", HTTP_VERB.POST);
        //public static readonly Endpoint REGISTER_GOOGLE = new Endpoint("/Odata/Account/GoogleLogin ", HTTP_VERB.POST);
        //public static readonly Endpoint REGISTER_FACEBOOK = new Endpoint("/Odata/Account/FacebookLogin  ", HTTP_VERB.POST);
        //public static readonly Endpoint REGISTER_ANON = new Endpoint("/Odata/Account/RegisterSilent?clientId=", HTTP_VERB.POST);

        ////Dashboard Endpoints
        ////public static readonly Endpoint DASHBOARD_GET_CURRENT_TIME = new Endpoint("/Odata/Me/GetCurrentTime", HTTP_VERB.GET);
        //public static readonly Endpoint DASHBOARD_GET_HAS_POLICIES = new Endpoint("/Odata/Me/GetHasPolicies", HTTP_VERB.GET);
        //public static readonly Endpoint DASHBOARD_GET_ACTIVE_POLICIES = new Endpoint("/Odata/Me/GetActivePolicies", HTTP_VERB.GET);
        //public static readonly Endpoint DASHBOARD_PENDING_POLICIES = new Endpoint("/Odata/Me/GetPendingPolicies", HTTP_VERB.GET);

        ////Advice Endpoints
        //public static readonly Endpoint ADVICE_ADVICETYPE = new Endpoint("/Odata/Advice?Key= ", HTTP_VERB.GET);
        //public static readonly Endpoint ADVICE_CALLME = new Endpoint("/Odata/Advice/CallMe  ", HTTP_VERB.POST);
        //public static readonly Endpoint ADVICE_SMSME = new Endpoint("/Odata/Advice/SMSMe  ", HTTP_VERB.POST);
        //public static readonly Endpoint ADVICE_MAILME = new Endpoint("/Odata/Advice/MailMe  ", HTTP_VERB.POST);
        //public static readonly Endpoint ADVICE_REMOVE = new Endpoint("/Odata/Advice/Untether ", HTTP_VERB.POST);

        ////Buy Endpoints
        //public static readonly Endpoint BUY_PRODUCTS = new Endpoint("/Odata/Products?$expand=ProductDetails,ProductPerDayPricings", HTTP_VERB.GET);
        //public static readonly Endpoint BUY_PURCHASE_POLICIES = new Endpoint("/Odata/Policies/PuchasePolicy", HTTP_VERB.POST);
        ////Promotions
        //public static readonly Endpoint ISSUE_PROMOTION_POLICY = new Endpoint("/Odata/Policies/IssuePolicy", HTTP_VERB.POST);
        //public static readonly Endpoint REDEEM_PROMOTION = new Endpoint("/Odata/Policies/RedeemPromotion", HTTP_VERB.POST);

        ////Dashboard
        //public static readonly Endpoint GET_REDEMPTION_PURCHASE_STATUS = new Endpoint("/Odata/Me/RedemptionPurchaseStatus", HTTP_VERB.GET);
        //public static readonly Endpoint GET_ME_INFO = new Endpoint("/Odata/Me", HTTP_VERB.GET);
        //public static readonly Endpoint CHECK_LOCATION = new Endpoint("/Odata/Me/CheckLocation", HTTP_VERB.POST);
        //public static readonly Endpoint GET_DISPLAY_NAME = new Endpoint("/Odata/Me/DisplayName", HTTP_VERB.GET);

        //// Expired Cover History
        //public static readonly Endpoint GET_EXPIRED_COVER = new Endpoint("/Odata/Me/GetExpiredPolicies", HTTP_VERB.GET);

        ////Profile Endpoints
        //public static readonly Endpoint PROFILE_UPDATE_CUSTOMER = new Endpoint("/Odata/Me/Profile/Customer", HTTP_VERB.PUT);
        //public static readonly Endpoint PROFILE_UPDATE_BENEFICIARY = new Endpoint("/Odata/Me/Profile/Beneficiary", HTTP_VERB.PUT);
        //public static readonly Endpoint PROFILE_UPDATE_CONTACT = new Endpoint("/Odata/Me/Profile/Contact", HTTP_VERB.PUT);
        //public static readonly Endpoint PROFILE_UPDATE_PAYMENT = new Endpoint("/Odata/Me/Profile/Payment", HTTP_VERB.PUT);

        //// Terms and conditions
        //public static readonly Endpoint GET_TERMS_AND_CONDITIONS = new Endpoint("/Odata/TermsAndConditions?$orderby=TermsAndConditionId%20desc&$top=1", HTTP_VERB.GET);
        //public static readonly Endpoint GET_TERMS_AND_CONDITIONS_ID = new Endpoint("/Odata/TermsAndConditions?$orderby=TermsAndConditionId%20desc&$top=1&$select=TermsAndConditionId", HTTP_VERB.GET);
        //public static readonly Endpoint ACCEPT_TERMS_AND_CONDITIONS = new Endpoint("/Odata/UserTermsAndConditions", HTTP_VERB.POST);

        ////Promotions
        //public static readonly Endpoint PROMOTIONS_ALL = new Endpoint("/Odata/Promotions", HTTP_VERB.GET);
        //public static readonly Endpoint PROMOTION_IMPRESSION = new Endpoint("/Odata/Promotions/Impression", HTTP_VERB.POST);

        ////Contact Us
        //public static readonly Endpoint CONTACT_US = new Endpoint("/Odata/Me/ContactUs", HTTP_VERB.POST);

        ////Vouchers
        //public static readonly Endpoint VALIDATE_VOUCHER = new Endpoint("/Odata/Voucher/Validate?reference=", HTTP_VERB.GET);
        //public static readonly Endpoint VOUCHER_GET_PROMO_DETAILS = new Endpoint("/Odata/Voucher/Promotion?reference=", HTTP_VERB.GET);

        // games
        public static readonly Endpoint GET_GAMES = new Endpoint("/games", HTTP_VERB.GET);

        // sessions
        public static readonly Endpoint GET_SESSIONS = new Endpoint("/sessions", HTTP_VERB.GET);
        public static readonly Endpoint CREATE_SESSION = new Endpoint("/sessions", HTTP_VERB.POST);

        // players
        public static readonly Endpoint GET_PLAYERS = new Endpoint("/players", HTTP_VERB.GET);

        public readonly string Url;
        public readonly string UrlAction;
        public readonly HTTP_VERB Verb;
        public string Paramaters;

        public Endpoint(string url, HTTP_VERB verb)
        {
            UrlAction = url;
            Url = BASE_URL + url;
            Verb = verb;
        }

        public string ConstructSomething()
        {
            return Url + Paramaters;
        }
    }

    public enum HTTP_VERB
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}