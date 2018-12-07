using Drive.Client.Models.Medias;
using Plugin.Media.Abstractions;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Drive.Client.Services.Media {
    public interface IPickMediaService {

        Task<MediaFile> TakePhotoAsync();

        Task<MediaFile> PickPhotoAsync();

        Task<MediaFile> TakeVideoAsync();

        Task<MediaFile> PickVideoAsync();

        Task<string> ParseStreamToBase64(Stream stream);

        Task<Stream> ExtractStreamFromMediaUrlAsync(string urlPath);

        Task<PickedImage> BuildPickedImageAsync(MediaFile mediaFile);

        Task<PickedImage> BuildPickedImageAsync();

        Task<ImageSource> BuildImageSourceAsync(Stream stream);
    }
}
