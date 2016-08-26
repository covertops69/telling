using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Services
{
    public interface IRestService
    {
        Task<T> GetAsync<T>(string url, Dictionary<string, object> parameters = null, Dictionary<string, string> headerValues = null, bool beLoggedInAware = false) where T : new();
        Task<T> PostAsync<T>(string url, Dictionary<string, object> postData = null, Dictionary<string, string> headerValues = null, bool beLoggedInAware = false, int attemptNumber = 0) where T : new();
    }

    public class JsonRestService : IRestService
    {
        const int TIMEOUT_SECONDS = 30;

        //[MvxInject]
        public IConnectivityService ConnectivityService { get; set; }

        protected bool IsConnected
        {
            get
            {
                return ConnectivityService.IsConnected;
            }
        }

        protected void CheckThrowNotConnectedException()
        {
            //try
            //{
            //    if (!IsConnected)
            //    {
            //        throw new NotConnectedException("Failed to connect at JsonRest Service");
            //    }
            //}
            //catch
            //{
            //    throw new NotConnectedException();
            //}
        }

        #region IRestService implementation

        public async Task<T> GetAsync<T>(string url, Dictionary<string, object> parameters = null, Dictionary<string, string> headerValues = null, bool beLoggedInAware = false) where T : new()
        {
            using (var httpClient = CreateHttpClient())
            {
                if (parameters != null && parameters.Count > 0)
                {
                    url = AddUrlParams(url, parameters);
                }

                if (headerValues != null)
                {
                    SetHeaderValues(httpClient, headerValues);
                }

                try
                {
                    var response = await httpClient.GetAsync(url).ConfigureAwait(false);

                    //if (beLoggedInAware)
                    //{
                    //    // very naive way of checking whether the response is HTML, and thus a redirect to the Login screen
                    //    if (response.Content.Headers.ContentType.MediaType.Equals("text/html"))
                    //    {
                    //        throw new NotLoggedInException();
                    //    }
                    //}

                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var result = await Task.Run(() => JsonConvert.DeserializeObject<T>(responseString, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    })).ConfigureAwait(false);

                    return result;
                }
                catch (TaskCanceledException tcex)
                {
                    CheckThrowNotConnectedException();

                    // if cancellation wasn't explicitly requested, it was probably a Timeout
                    if (!tcex.CancellationToken.IsCancellationRequested)
                    {
                        throw new TimeoutException("The connection timed out; please check your internet connection and try again.", tcex);
                    }

                    throw tcex;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<T> PostAsync<T>(string url, Dictionary<string, object> postData = null, Dictionary<string, string> headerValues = null, bool beLoggedInAware = false, int attemptNumber = 0) where T : new()
        {
            using (var httpClient = CreateHttpClient())
            {
                if (headerValues != null)
                {
                    SetHeaderValues(httpClient, headerValues);
                }

                var paramList = new List<KeyValuePair<string, string>>();

                if (postData != null)
                {
                    foreach (var item in postData)
                    {
                        paramList.Add(new KeyValuePair<string, string>(item.Key, item.Value == null ? "" : item.Value.ToString()));
                    }
                }

                //AddDebugHeaders(paramList);

                var content = new FormUrlEncodedContent(paramList.ToArray());

                try
                {
                    //Debug.WriteLine("**** API REQUEST ****");
                    //Debug.WriteLine("API REQUEST URL: " + url);
                    //Debug.WriteLine("API REQUEST PARAMS:");

                    //foreach (var param in paramList)
                    //{
                    //    Debug.WriteLine(param.Key + ": " + param.Value);
                    //}

                    var response = await httpClient.PostAsync(url, content).ConfigureAwait(false);

                    var uri = new Uri(url);

                    //AddResponseCookies(response.Headers, uri);

                    //if (beLoggedInAware)
                    //{
                    //    // very naive way of checking whether the response is HTML, and thus a redirect to the Login screen
                    //    if (response.Content.Headers.ContentType.MediaType.Equals("text/html"))
                    //        throw new NotLoggedInException();
                    //}

                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    //Debug.WriteLine("**** RESPONSE:" + responseString);

                    return await Task.Run(() => JsonConvert.DeserializeObject<T>(responseString)).ConfigureAwait(false);

                    //var response = await Task.Run(() => JsonConvert.DeserializeObject<T>(responseString, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })).ConfigureAwait(false);

                    //                    if (baseResponse.ResultMessageCode.ToLower().Contains("api-lo-001") || baseResponse.ResultMessageCode.ToLower().Contains("api-gfr-003"))
                    //                    {
                    //                        // TODO [After Release 1] :: Refactor to have all ONNET calls use the ONNET service; move this logic there
                    //                        //Make same request that failed again
                    //                        //Increment attempt count until 10
                    //                        if (attemptNumber < ONNET_RETRY_COUNT)
                    //                        {
                    //                            await SecurityManager.CheckAndCreateONNETSessionAsync();
                    //                            var currentAttempt = attemptNumber + 1;
                    //                            await PostAsync<T>(url, postData, headerValues, beLoggedInAware, currentAttempt);
                    //                        }
                    //                        else
                    //                        {
                    //#if DEBUG
                    //                            throw new ONNETSessionLostException("ONNET Session lost.");
                    //#else
                    //                            throw new ONNETSessionLostException("Unable to connect to Telkom network. Please try again later.");
                    //#endif
                    //                        }
                    //                    }

                    //if (!(baseResponse.ResultCode == -1 || baseResponse.ResultCode == 1))
                    //{
                    //    return await Task.Run(() => JsonConvert.DeserializeObject<T>(responseString)).ConfigureAwait(false);
                    //}
                    //else
                    //{
                    //    var friendlyCustomerMessage =
                    //        string.IsNullOrEmpty(baseResponse.FriendlyCustomerMessage) ?
                    //                Constants.FRIENDLY_ERROR_MESSAGE :
                    //                baseResponse.FriendlyCustomerMessage;

                    //    throw new ExceptionWithTelkomResponseCodes(friendlyCustomerMessage)
                    //    {
                    //        ErrorCode = baseResponse.ResultCode
                    //    };
                    //}
                }
                catch (TaskCanceledException tcex)
                {
                    //Debug.WriteLine("**** CANCEL EXCEPTION." + tcex);
                    //// if cancellation wasn't explicitly requested, it was probably a Timeout
                    //if (!tcex.CancellationToken.IsCancellationRequested)
                    //{
                    //    throw new TimeoutException("The connection timed out; please check your internet connection and try again.", tcex);
                    //}

                    throw tcex;
                }
                catch (WebException wex)
                {
                    //Debug.WriteLine("**** WEB EXCEPTION." + wex);

                    CheckThrowNotConnectedException();

                    //try
                    //{
                    //    if (!IsConnected)
                    //    {
                    //        throw new NotConnectedException("Failed to connect at Telkom Service");
                    //    }
                    //    else
                    //    {
                    //        throw;
                    //    }
                    //}
                    //catch
                    //{
                    //    throw new NotConnectedException();
                    //}

                    throw wex;
                }
                catch (JsonException ex)
                {
                    //                    //API is returning unknown json
                    //                    //Debug.WriteLine("**** JSON EXCEPTION." + ex);
                    //#if DEBUG
                    //                    throw;
                    //#else
                    //                    throw new ExceptionWithTelkomResponseCodes(Constants.FRIENDLY_ERROR_MESSAGE)
                    //                    {
                    //                        ErrorCode = 1
                    //                    };
                    //#endif

                    throw ex;
                }
                catch (Exception ex)
                {
                    //                    Debug.WriteLine("**** GENERAL EXCEPTION." + ex);
                    //                    CheckThrowNotConnectedException();

                    //#if DEBUG
                    //                    throw ex;
                    //#else
                    //                    throw new ExceptionWithTelkomResponseCodes(Constants.FRIENDLY_ERROR_MESSAGE)
                    //                    {
                    //                        ErrorCode = 1
                    //                    };
                    //#endif

                    throw ex;
                }
            }
        }

        #endregion

        //static void AddResponseCookies(HttpResponseHeaders responseHeaders, Uri uri)
        //{
        //    var responseHeadersEnumerator = responseHeaders.GetEnumerator();

        //    while (responseHeadersEnumerator.MoveNext())
        //    {
        //        var name = responseHeadersEnumerator.Current.Key;
        //        var value = responseHeadersEnumerator.Current.Value;

        //        if (name == "Set-Cookie")
        //        {
        //            var test = value.GetEnumerator();
        //            test.MoveNext();
        //            var match = Regex.Match(test.Current, "(.+?)=(.+?);");
        //            if (match.Captures.Count > 0)
        //            {
        //                var cookieCache = Mvx.Resolve<ICookieContainerCache>();
        //                var cookieContainer = cookieCache.GetCookieContainer();
        //                if (cookieContainer == null)
        //                {
        //                    cookieContainer = new CookieContainer();
        //                    cookieCache.SetCookieContainer(cookieContainer);
        //                }
        //                cookieCache.GetCookieContainer().Add(uri, new Cookie(match.Groups[1].Value, match.Groups[2].Value, "/", uri.Host));
        //            }
        //        }
        //    }
        //}

        static HttpClient CreateHttpClient(/*CookieContainer cookieContainer = null*/)
        {
            //HttpMessageHandler handler = null;

            //handler = cookieContainer != null ? new TimedHttpClientHandler
            //{
            //    CookieContainer = cookieContainer,

            //    //UseProxy = true, Proxy = new HTTPProxy (new Uri ("http://127.0.0.1:8888"))
            //} : new TimedHttpClientHandler
            //{
            //    CookieContainer = new CookieContainer(),
            //    // UseProxy = true,Proxy = new HTTPProxy (new Uri ("http://127.0.0.1:8888"))
            //};

            var httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(TIMEOUT_SECONDS)
            };

            return httpClient;
        }

        static string AddUrlParams(string baseUrl, Dictionary<string, object> parameters)
        {
            var stringBuilder = new StringBuilder(baseUrl);
            var hasFirstParam = baseUrl.Contains("?");

            foreach (var parameter in parameters)
            {
                var format = hasFirstParam ? "&{0}={1}" : "?{0}={1}";
                if (parameter.Value == null)
                {
                    stringBuilder.AppendFormat(format, Uri.EscapeDataString(parameter.Key), string.Empty);
                }
                else
                {
                    stringBuilder.AppendFormat(format, Uri.EscapeDataString(parameter.Key), Uri.EscapeDataString(parameter.Value.ToString()));
                }

                hasFirstParam = true;
            }

            return stringBuilder.ToString();
        }

        static void SetHeaderValues(HttpClient httpClient, Dictionary<string, string> headerValues)
        {
            httpClient.DefaultRequestHeaders.Clear();

            foreach (KeyValuePair<string, string> keyValuePair in headerValues)
            {
                httpClient.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        //static void AddDebugHeaders(List<KeyValuePair<string, string>> paramList)
        //{
        //    var deviceInfo = Mvx.Resolve<IDeviceInfoService>();
        //    paramList.Add(new KeyValuePair<string, string>("sender", "Deloitte"));
        //    paramList.Add(new KeyValuePair<string, string>("deviceType", deviceInfo.Model));
        //    paramList.Add(new KeyValuePair<string, string>("deviceMake", deviceInfo.Manufacturer));
        //    paramList.Add(new KeyValuePair<string, string>("deviceOS", deviceInfo.OperatingSystem));
        //    paramList.Add(new KeyValuePair<string, string>("deviceOSVersion", deviceInfo.OperatingSystemVersion));
        //    paramList.Add(new KeyValuePair<string, string>("appStoreVersion", deviceInfo.AppVersion));
        //}
    }
}
