using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Drive.Client.Services.RequestProvider {
    public class RequestProvider : IRequestProvider {

        private static readonly string DEVICE_ID_HEADER_KEY = "DeviceId";

        private readonly HttpClient _client;

        public RequestProvider() {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ///
            /// TODO: temporary implementation
            /// 
            _client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("uk-UA"));
        }

        /// <summary>
        /// TODO: implement Base request/response models
        /// </summary>
        public async Task<TResult> GetAsync<TResult>(string uri) {
            //  Check internet connection.
            if (!CrossConnectivity.Current.IsConnected) throw new ConnectivityException(AppConsts.ERROR_INTERNET_CONNECTION);

            HttpResponseMessage response = await _client.GetAsync(uri);

            await HandleResponse(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized));

            return result;
        }

        /// <summary>
        /// TODO: implement Base request/response models
        /// </summary>
        public Task<TResponseValue> PostAsync<TResponseValue, TBodyContent>(string uri, TBodyContent bodyContent) =>
            Task<TResponseValue>.Run(async () => {
                if (!CrossConnectivity.Current.IsConnected) throw new ConnectivityException(AppConsts.ERROR_INTERNET_CONNECTION);

                HttpContent content = null;

                if (bodyContent != null) {
                    string jObject = JsonConvert.SerializeObject(bodyContent);

                    content = new StringContent(jObject);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }

                HttpResponseMessage response = await _client.PostAsync(uri, content);

                await HandleResponse(response);

                string serialized = await response.Content.ReadAsStringAsync();

                TResponseValue result = JsonConvert.DeserializeObject<TResponseValue>(serialized);

                return result;
            });

        private async Task HandleResponse(HttpResponseMessage response) {
            if (!response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized) {
                    throw new ServiceAuthenticationException(content);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, content);
            }
        }
    }
}
