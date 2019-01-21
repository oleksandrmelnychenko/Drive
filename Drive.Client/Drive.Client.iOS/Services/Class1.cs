using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using Drive.Client.Services;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Interface1))]
namespace Drive.Client.iOS.Services {
    public class Class1 : Interface1 {

        private static readonly string _IMAGE_MEDIA_TYPE = "public.image";
        private static readonly string _MOVIE_MEDIA_TYPE = "public.movie";
        public static readonly int IMAGE_WIDTH_RESTRICTION = 360;
        public static readonly int IMAGE_HEIGHT_RESTRICTION = 640;

        //public Task<string> GetPhotoAsync() {
        //    string imageBase64 = null;
        //    Task<string> task = new Task<string>(() => imageBase64);

        //    try {
        //        UIViewController topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        //        while (topController.PresentedViewController != null) {
        //            topController = topController.PresentedViewController;
        //        }

        //        //
        //        // Action sheet
        //        //
        //        UIAlertController actionSheet =
        //            UIAlertController.Create("Pick photo.", "Pick photo with:", UIAlertControllerStyle.ActionSheet);

        //        //
        //        // Photo galery action
        //        //
        //        actionSheet.AddAction(UIAlertAction.Create("Photo galery", UIAlertActionStyle.Default, (action) => {
        //            UIImagePickerController imagePicker = new UIImagePickerController();
        //            imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
        //            imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

        //            imagePicker.FinishedPickingMedia += (sender, args) => {
        //                if (args?.OriginalImage != null) {
        //                    //
        //                    // Figure out how much to scale down by
        //                    //
        //                    int inSampleSize = GetInSampleSize(args.OriginalImage.Size.Width, args.OriginalImage.Size.Height);

        //                    UIImage originalImage = args.OriginalImage.Scale(new CGSize(args.OriginalImage.Size.Width / inSampleSize, args.OriginalImage.Size.Height / inSampleSize));
        //                    imageBase64 = originalImage.AsJPEG().GetBase64EncodedString(NSDataBase64EncodingOptions.EndLineWithLineFeed);
        //                }
        //                topController.DismissModalViewController(true);
        //                task.Start();
        //            };

        //            imagePicker.Canceled += (sender, args) => {
        //                topController.DismissModalViewController(true);
        //                task.Start();
        //            };

        //            topController.PresentModalViewController(imagePicker, true);
        //        }));

        //        //
        //        // Camera action
        //        //
        //        actionSheet.AddAction(UIAlertAction.Create("Camera", UIAlertActionStyle.Default, (action) => {
        //            if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera)) {
        //                UIImagePickerController imagePicker = new UIImagePickerController();
        //                imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
        //                imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.Camera);

        //                imagePicker.FinishedPickingMedia += (sender, args) => {
        //                    if (args?.OriginalImage != null) {
        //                        //
        //                        // Figure out how much to scale down by
        //                        //
        //                        int inSampleSize = GetInSampleSize(args.OriginalImage.Size.Width, args.OriginalImage.Size.Height);

        //                        UIImage originalImage = args.OriginalImage.Scale(new CGSize(args.OriginalImage.Size.Width / inSampleSize, args.OriginalImage.Size.Height / inSampleSize));
        //                        imageBase64 = originalImage.AsJPEG().GetBase64EncodedString(NSDataBase64EncodingOptions.EndLineWithLineFeed);
        //                    }
        //                    topController.DismissModalViewController(true);
        //                    task.Start();
        //                };

        //                imagePicker.Canceled += (sender, args) => {
        //                    topController.DismissModalViewController(true);
        //                    task.Start();
        //                };

        //                topController.PresentModalViewController(imagePicker, true);
        //            }
        //            else {
        //                UIAlertController alert = UIAlertController.Create("Warning", "Your device don't have camera", UIAlertControllerStyle.Alert);
        //                alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (alertAction) => {
        //                    imageBase64 = null;
        //                    task.Start();
        //                }));

        //                topController.PresentViewController(alert, true, null);
        //            }
        //        }));


        //        UIPopoverPresentationController presentationPopover = actionSheet.PopoverPresentationController;
        //        if (presentationPopover != null) {
        //            presentationPopover.SourceView = topController.View;
        //            presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
        //        }

        //        //
        //        // Display the alert
        //        //
        //        topController.PresentViewController(actionSheet, true, null);
        //    }
        //    catch (Exception) {
        //        imageBase64 = null;
        //        task.Start();
        //    }

        //    return task;
        //}

        public Task<string> GetPhotoAsync() {
            string imageBase64 = null;
            Task<string> task = new Task<string>(() => imageBase64);

            try {
                UIViewController topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
                while (topController.PresentedViewController != null) {
                    topController = topController.PresentedViewController;
                }

                UIImagePickerController imagePicker = null;
                if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera)) {
                    imagePicker = new UIImagePickerController();
                    imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
                    imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.Camera);

                    imagePicker.FinishedPickingMedia += (sender, args) => {
                        if (args?.OriginalImage != null) {
                            //
                            // Figure out how much to scale down by
                            //
                            int inSampleSize = GetInSampleSize(args.OriginalImage.Size.Width, args.OriginalImage.Size.Height);

                            UIImage originalImage = args.OriginalImage.Scale(new CGSize(args.OriginalImage.Size.Width / inSampleSize, args.OriginalImage.Size.Height / inSampleSize));
                            imageBase64 = originalImage.AsJPEG().GetBase64EncodedString(NSDataBase64EncodingOptions.EndLineWithLineFeed);
                        }
                        topController.DismissModalViewController(true);
                        task.Start();
                    };

                    imagePicker.Canceled += (sender, args) => {
                        topController.DismissModalViewController(true);
                        task.Start();
                    };

                    topController.PresentModalViewController(imagePicker, true);
                }
            }
            catch (Exception) {
                imageBase64 = null;
                task.Start();
            }

            return task;
        }

        private int GetInSampleSize(nfloat srcWidth, nfloat srcHeight) {
            int inSampleSize = 1;

            if (srcHeight >= IMAGE_HEIGHT_RESTRICTION || srcWidth >= IMAGE_WIDTH_RESTRICTION) {
                if (srcHeight >= IMAGE_HEIGHT_RESTRICTION) {
                    inSampleSize = (int)Math.Round(srcHeight / IMAGE_HEIGHT_RESTRICTION);
                }
                else {
                    inSampleSize = (int)Math.Round(srcWidth / IMAGE_WIDTH_RESTRICTION);
                }
            }

            return inSampleSize;
        }
    }
}