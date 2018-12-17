using Drive.Client.Exceptions;
using Drive.Client.Helpers;
using Drive.Client.Helpers.Localize;
using Drive.Client.Models.Medias;
using Drive.Client.Resources.Resx;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.RequestProvider {
    public class RequestProvider : IRequestProvider {

        private readonly HttpClient _client;
        private ResourceLoader _resourceLoader;

        /// <summary>
        ///     ctor().
        /// </summary>
        public RequestProvider() {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.LanguageInterface.LocaleId));
            _client.DefaultRequestHeaders.Add("DeviceId", CrossDeviceInfo.Current.Id);

            _resourceLoader = new ResourceLoader();
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
        public async Task<TResult> GetAsync<TResult>(string uri, string accessToken = "", CancellationToken cancellationToken = default(CancellationToken)) =>
              await Task.Run(async () => {
                  TResult result = default(TResult);
                  CheckInternetConnection();
                  SetAccesToken(accessToken);
                  SetLanguage();

                  HttpResponseMessage response = await _client.GetAsync(uri, cancellationToken);

                  await HandleResponse(response);

                  string serialized = await response.Content.ReadAsStringAsync();
                  result = await DeserializeResponse<TResult>(serialized);

                  return result;

              }, cancellationToken);

        /// <summary>
        /// POST.
        /// </summary>
        public async Task<TResult> PostAsync<TResult, TBodyContent>(string uri, TBodyContent bodyContent, string accessToken = "") =>
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

                TResult result = await DeserializeResponse<TResult>(serialized);

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
            where TBodyContent : MediaBase =>
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

                        result = await DeserializeResponse<TResult>(serialized);
                    }
                }

                return result;
            });

        public async Task<TResult> PostFormDataCollectionAsync<TResult, TBodyContent>(string url, TBodyContent attachedData, string accessToken = "")
            where TBodyContent : IEnumerable<MediaBase> =>
             await Task.Run(async () => {
                 TResult result = default(TResult);

                 CheckInternetConnection();
                 SetAccesToken(accessToken);
                 SetLanguage();

                 using (MultipartFormDataContent formDataContent = new MultipartFormDataContent()) {
                     if (attachedData != null) {
                         foreach (var item in attachedData) {

                             ByteArrayContent byteArrayContent = new ByteArrayContent(item.Body);

                             formDataContent.Add(byteArrayContent, "ImageFile", item.Name);
                         }

                         HttpResponseMessage response = await _client.PostAsync(url, formDataContent);

                         await HandleResponse(response);

                         string serialized = await response.Content.ReadAsStringAsync();

                         result = await DeserializeResponse<TResult>(serialized);
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

                result = await DeserializeResponse<TResult>(serialized);

                return result;
            });

        private static async Task<TResult> DeserializeResponse<TResult>(string serialized) =>
           await Task.Run(() => {
               if (serialized != null) {
                   return JsonConvert.DeserializeObject<TResult>(serialized);
               }
               return default(TResult);
           });

        private void CheckInternetConnection() {
            if (!CrossConnectivity.Current.IsConnected)
                throw new ConnectivityException((_resourceLoader.GetString(nameof(AppStrings.ERROR_INTERNET_CONNECTION))).Value);
        }

        private void SetLanguage() {
            _client.DefaultRequestHeaders.AcceptLanguage.Clear();
            _client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(BaseSingleton<GlobalSetting>.Instance.AppInterfaceConfigurations.LanguageInterface.LocaleId));
        }

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
