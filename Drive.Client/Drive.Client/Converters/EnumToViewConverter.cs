using Drive.Client.Models.Identities.Posts;
using Drive.Client.Views.Templates.ViewCells.Post.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Drive.Client.Converters {
    public class EnumToViewConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            if (!(value is PostType postType)) return null;

            PostBaseContentView view = null;

            switch (postType) {
                case PostType.TextPost:
                    view = new TextPostView();
                    break;
                case PostType.MediaPost:
                    view = new MediaPostView();
                    break;
                case PostType.LinkPost:
                    Debugger.Break();
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
