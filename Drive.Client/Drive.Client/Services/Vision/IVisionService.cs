using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drive.Client.Services.Vision {
    public interface IVisionService {
        Task<List<string>> AnalyzeImageForText(MediaFile file);
    }
}
