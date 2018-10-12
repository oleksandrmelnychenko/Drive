using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.Medias;
using Drive.Client.Resources.Resx;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.DeviceInfo;
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
            _client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.LanguageInterface.LocaleId));
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
        /// GET.
        /// </summary>
        public async Task<TResult> GetAsync<TResult>(string uri, string accessToken = "") {
            CheckInternetConnection();
            SetAccesToken(accessToken);
            SetLanguage();

            HttpResponseMessage response = await _client.GetAsync(uri);

            await HandleResponse(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized));

            return result;
        }

        private static void CheckInternetConnection() {
            if (!CrossConnectivity.Current.IsConnected)
                throw new ConnectivityException((ResourceLoader.Instance.GetString(nameof(AppStrings.ERROR_INTERNET_CONNECTION))).Value);
        }

        /// <summary>
        /// POST.
        /// </summary>
        public async Task<TResponseValue> PostAsync<TResponseValue, TBodyContent>(string uri, TBodyContent bodyContent, string accessToken = "") =>
            await Task.Run(async () => {
                HttpContent content = null;

                CheckInternetConnection();
                SetAccesToken(accessToken);
                SetLanguage();

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

        /// <summary>
        /// POST form-data.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TBodyContent"></typeparam>
        /// <param name="uri"></param>
        /// <param name="bodyContent"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<TResult> PostFormDataAsync<TResult, TBodyContent>(string uri, TBodyContent bodyContent, string accessToken = "")
            where TBodyContent : PickedMediaBase =>
            await Task.Run(async () => {
                TResult result = default(TResult);

                CheckInternetConnection();
                SetAccesToken(accessToken);
                SetLanguage();

                using (MultipartFormDataContent formDataContent = new MultipartFormDataContent()) {
                    if (bodyContent != null) {
                        ByteArrayContent byteArrayContent = new ByteArrayContent(bodyContent.Body);

                        formDataContent.Add(byteArrayContent, "ImageFile", bodyContent.Name);

                        HttpResponseMessage response = await _client.PostAsync(uri, formDataContent);

                        await HandleResponse(response);

                        string serialized = await response.Content.ReadAsStringAsync();

                        result = JsonConvert.DeserializeObject<TResult>(serialized);
                    }
                }

                return result;
            });

        /// <summary>
        /// PUT.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="url"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<TResult> PutAsync<TResult, TBodyContent>(string url, TBodyContent bodyContent, string accessToken = "") =>
            await Task.Run(async () => {
                HttpContent content = null;
                TResult result = default(TResult);

                CheckInternetConnection();
                SetAccesToken(accessToken);
                SetLanguage();

                if (bodyContent != null) {
                    content = new StringContent(JsonConvert.SerializeObject(bodyContent));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
                HttpResponseMessage response = await _client.PutAsync(url, content);

                await HandleResponse(response);

                string serialized = await response.Content.ReadAsStringAsync();

                result = await Task.Run(() =>
                    JsonConvert.DeserializeObject<TResult>(serialized));

                return result;
            });

        private void SetLanguage() {
            _client.DefaultRequestHeaders.AcceptLanguage.Clear();
            _client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.LanguageInterface.LocaleId));
        }

        private void SetAccesToken(string accessToken) {
            if (_client.DefaultRequestHeaders.Authorization == null) {
                if (!string.IsNullOrEmpty(accessToken)) {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
            }
            else {
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
