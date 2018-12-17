using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Models.Medias;
using Microsoft.AppCenter.Crashes;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Drive.Client.Services.Media {
    public class PickMediaService : IPickMediaService {

        private static readonly string IMAGE_ATTACHED_MEDIA_DIRECTORY_NAME = "attached_image_media";

        private static readonly string VIDEO_ATTACHED_MEDIA_DIRECTORY_NAME = "attached_video_media";

        public async Task<MediaFile> TakePhotoAsync() {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
                return null;
            }

            MediaFile file = null;
            try {
                file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions {
                    PhotoSize = PhotoSize.Custom,
                    CompressionQuality = 92,
                    Name = string.Format("{0:ddMMyyHmmss}.jpg", DateTime.Now),
                    Directory = IMAGE_ATTACHED_MEDIA_DIRECTORY_NAME
                });
            }
            catch (Exception) {
                Debugger.Break();
            }

            return file;
        }

        public async Task<MediaFile> PickPhotoAsync() {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported) return null;

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 50
            });

            if (file == null) return null;

            return file;
        }

        public async Task<PickedImage> BuildPickedImageAsync() {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported) return null;

            PickedImage pickedImage = null;

            using (var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Custom, CompressionQuality = 50 })) {
                if (file == null) return null;

                try {
                    Stream stream = file.GetStream();

                    pickedImage = new PickedImage {
                        Name = Path.GetFileName(file.Path),
                        Body = await ParseStreamToBytesAsync(stream)
                    };
                }
                catch (Exception) {
                    pickedImage = null;
                }
            }
            return pickedImage;
        }

        public async Task<AttachedImage> BuildAttachedImageAsync() {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported) return null;

            AttachedImage attachedImage = null;

            using (var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Custom, CompressionQuality = 50 })) {
                if (file == null) return null;

                try {
                    Stream stream = file.GetStream();
                    
                    attachedImage = new AttachedImage {
                        MediaPresentation = ImageSource.FromFile(file.Path),
                        Name = Path.GetFileName(file.Path),
                        Body = await ParseStreamToBytesAsync(stream)
                    };
                }
                catch (Exception) {
                    attachedImage = null;
                }
            }

            return attachedImage;
        }

        public async Task<MediaFile> TakeVideoAsync() {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported) {
                return null;
            }

            MediaFile file = null;
            try {
                file = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions {
                    Quality = VideoQuality.Medium,
                    CompressionQuality = 1,
                    DesiredLength = new TimeSpan(0, 1, 0),
                    Directory = VIDEO_ATTACHED_MEDIA_DIRECTORY_NAME
                });
            }
            catch (Exception) {
                Debugger.Break();
            }
            return file;
        }

        public async Task<MediaFile> PickVideoAsync() {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickVideoSupported) return null;

            var file = await CrossMedia.Current.PickVideoAsync();

            if (file == null) return null;

            return file;
        }

        //public Task<PickedImage> BuildPickedImageAsync(MediaFile mediaFile) =>
        //    Task.Run(async () => {
        //        PickedImage pickedImage = null;
        //        try {
        //            pickedImage = new PickedImage {
        //                Name = Path.GetFileName(mediaFile.Path),
        //                Body = await ParseStreamToBytesAsync(mediaFile.GetStream())
        //            };
        //        }
        //        catch (Exception) {
        //            pickedImage = null;
        //        }
        //        return pickedImage;
        //    });

        public Task<string> ParseStreamToBase64(Stream stream) =>
            Task.Run(() => {
                string base64string = "";

                try {
                    stream.Position = 0;

                    byte[] bytes;

                    using (BinaryReader reader = new BinaryReader(stream)) {
                        bytes = reader.ReadBytes((int)stream.Length);
                        base64string = Convert.ToBase64String(bytes);
                    }
                }
                catch (Exception) {
                    base64string = "";
                }

                return base64string;
            });

        public Task<Stream> ExtractStreamFromMediaUrlAsync(string urlPath) =>
            Task.Run(() => {
                Stream stream = null;
                try {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlPath);
                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    stream = new MemoryStream();
                    httpWebResponse.GetResponseStream().CopyTo(stream);

                    httpWebResponse.Dispose();
                }
                catch (Exception) {
                    stream = null;
                }
                return stream;
            });

        public Task<ImageSource> BuildImageSourceAsync(Stream stream) =>
            Task<ImageSource>.Run(async () => {
                ImageSource imageSource = null;

                try {
                    stream.Position = 0;

                    byte[] binary = await ParseStreamToBytesAsync(stream);
                    MemoryStream memoryStream = new MemoryStream(binary);

                    imageSource = ImageSource.FromStream(() => memoryStream);
                }
                catch (Exception exc) {
                    Debugger.Break();

                    Crashes.TrackError(exc);
                    imageSource = null;
                }

                return imageSource;
            });

        public Task<byte[]> ParseStreamToBytesAsync(Stream stream) =>
           Task.Run(() => {
               byte[] bytes = default(byte[]);

               try {
                   stream.Position = 0;

                   using (BinaryReader reader = new BinaryReader(stream)) {
                       bytes = reader.ReadBytes((int)stream.Length);
                   }
               }
               catch (Exception ex) {
                   Debugger.Break();
               }

               return bytes;
           });
    }
}
