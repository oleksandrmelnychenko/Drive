using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Models.Medias;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.RequestProvider {
    public interface IRequestProvider {
        Task<TResult> GetAsync<TResult>(string uri, string accessToken = "", CancellationToken cancellationToken = default(CancellationToken));

        Task<TResult> PostAsync<TResult, TBodyContent>(string url, TBodyContent bodyContent, string accessToken = "");

        Task<TResult> PostFormDataAsync<TResult, TBodyContent>(string url, TBodyContent bodyContent, string accessToken = "")
            where TBodyContent : MediaBase;

        Task<TResult> PutAsync<TResult, TBodyContent>(string url, TBodyContent bodyContent, string accessToken = "");

        Task<TResult> PostFormDataCollectionAsync<TResult, TBodyContent>(string url, TBodyContent attachedData, string accessToken = "")
            where TBodyContent : IEnumerable<MediaBase>;
    }
}
