using Drive.Client.Models.EntityModels.Search;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace Drive.Client.Models.DataItems.Vehicle {
    public class CognitiveRequestDataItem : BaseRequestDataItem {
        CognitiveRequest _cognitiveRequest;
        public CognitiveRequest CognitiveRequest {
            get { return _cognitiveRequest; }
            set { SetProperty(ref _cognitiveRequest, value); }
        }

        public ICommand ShowImageCommand => new Command(() => OnShowImage());

        private void OnShowImage() {
            try {
                var browser = new PhotoBrowser();
                List<Photo> photos = new List<Photo>();

                photos.Add(new Photo { URL = CognitiveRequest.ImageUrl });

                browser.Photos = photos;
                browser.ActionButtonPressed = (x) => {
                    PhotoBrowser.Close();
                };
                browser.Show();
            }
            catch (Exception ex) {
                Debug.WriteLine($"ERROR:{ex.Message}");
            }
        }
    }
}
