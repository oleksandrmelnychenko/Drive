using Drive.Client.Models.EntityModels.Announcement;
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

        Task<PickedImage> BuildPickedImageAsync();

        Task<AttachedImage> BuildAttachedImageAsync();

        Task<string> ParseStreamToBase64(Stream stream);

        Task<byte[]> ParseStreamToBytesAsync(Stream stream);

        Task<ImageSource> BuildImageSourceAsync(Stream stream);

        Task<Stream> ExtractStreamFromMediaUrlAsync(string urlPath);

        Task<PickedImage> BuildPickedImageAsync(MediaFile mediaFile);
    }
}
