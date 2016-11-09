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

                var content = new FormUrlEncodedContent(paramList.ToArray());

                try
                {
                    var response = await httpClient.PostAsync(url, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var uri = new Uri(url);
                        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        return await Task.Run(() => JsonConvert.DeserializeObject<T>(responseString)).ConfigureAwait(false);
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
                catch (TaskCanceledException tcex)
                {
                    throw tcex;
                }
                catch (WebException wex)
                {
                    CheckThrowNotConnectedException();

                    throw wex;
                }
                catch (JsonException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        

        static HttpClient CreateHttpClient()
        {
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
    }
}