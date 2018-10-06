using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.DeviceInfo;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Drive.Client.Services.RequestProvider {
    public class RequestProvider : IRequestProvider {

        private readonly HttpClient _client;

        public RequestProvider() {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ///
            /// TODO: temporary implementation
            /// 
            _client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("uk-UA"));
            _client.DefaultRequestHeaders.Add("DeviceId", CrossDeviceInfo.Current.Id);
        }

        /// <summary>
        /// Reset access token.
        /// </summary>
        public void ClientTokenReset() {
            if (_client != null)
                _client.DefaultRequestHeaders.Authorization = null;
        }

        /// <summary>
        /// TODO: implement Base request/response models
        /// </summary>
        public async Task<TResult> GetAsync<TResult>(string uri, string accessToken = "") {
            //  Check internet connection.
            if (!CrossConnectivity.Current.IsConnected) throw new ConnectivityException(AppConsts.ERROR_INTERNET_CONNECTION);

            SetAccesToken(accessToken);

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
        public async Task<TResponseValue> PostAsync<TResponseValue, TBodyContent>(string uri, TBodyContent bodyContent, string accessToken = "") =>
            await Task.Run(async () => {
                if (!CrossConnectivity.Current.IsConnected) throw new ConnectivityException(AppConsts.ERROR_INTERNET_CONNECTION);
                HttpContent content = null;

                SetAccesToken(accessToken);

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

        private void SetAccesToken(string accessToken) {
            if (_client.DefaultRequestHeaders.Authorization == null) {
                if (!string.IsNullOrEmpty(accessToken)) {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
            } else {
                if (!(string.IsNullOrEmpty(accessToken)) && _client.DefaultRequestHeaders.Authorization.Parameter != accessToken) {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
            }
        }

        private async Task HandleResponse(HttpResponseMessage response) {
            if (!response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized) {

                    ClientTokenReset();

                    throw new ServiceAuthenticationException(content);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, content);
            }
        }
    }
}
