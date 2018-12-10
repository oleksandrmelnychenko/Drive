using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Views.Templates.ViewCells.Post.Content;
using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    public class EnumToViewConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            if (!(value is AnnounceType postType)) return null;

            PostBaseContentView view = null;

            switch (postType) {
                case AnnounceType.Text:
                    view = new TextPostView();
                    break;
                case AnnounceType.Video:
                    Debugger.Break();
                    break;
                case AnnounceType.Image:
                    view = new MediaPostView();
                    break;
                default:
                    Debugger.Break();
                    break;
            }

            return view;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
