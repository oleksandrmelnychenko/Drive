using System.Threading.Tasks;

namespace Drive.Client.Services.RequestProvider {
    public interface IRequestProvider {
        Task<TResult> GetAsync<TResult>(string uri, string accessToken = "");

        Task<TResponseValue> PostAsync<TResponseValue, TBodyContent>(string uri, TBodyContent bodyContent, string accessToken = "");
    }
}
