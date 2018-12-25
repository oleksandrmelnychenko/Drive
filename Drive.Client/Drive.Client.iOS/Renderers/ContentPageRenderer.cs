using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CoreGraphics;
using Drive.Client.iOS.Renderers;
using Drive.Client.Views.Posts;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PostCommentsView), typeof(ContentPageRenderer))]
namespace Drive.Client.iOS.Renderers {
    public class ContentPageRenderer : PageRenderer {
        private UIView activeview;             // Controller that activated the keyboard
        private float _scrollAmount = 0.0f;    // amount to scroll 
        private float offset = 10.0f;          // extra offset
        private bool _keyboardIsShow = false;

        protected override void OnElementChanged(VisualElementChangedEventArgs e) {
            base.OnElementChanged(e);

            //this.View.GestureRecognizers = new UIGestureRecognizer[0]; // Stop dissmis keyboard.
        }

        public override void ViewDidLoad() {
            base.ViewDidLoad();

            UITapGestureRecognizer hideKeyboardGestureRecognizer = new UITapGestureRecognizer(() => View.EndEditing(true));
            View.AddGestureRecognizer(hideKeyboardGestureRecognizer);

            // Keyboard popup
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

            // Keyboard Down
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardDownNotification);
        }

        private void KeyBoardUpNotification(NSNotification notification) {
            // get the keyboard size
            CGRect keyboardRect = UIKeyboard.BoundsFromNotification(notification);

            // Find what opened the keyboard
            activeview = GetActiveView(NativeView);

            if (activeview == null) return;

            // Bottom of the controller = initial position + height + offset    
            var point = GetAbsolutePoint(activeview);
            var bottom = (float)(point.Y + activeview.Frame.Height + offset);

            // Calculate how far we need to scroll
            var scrollAmount = (float)(keyboardRect.Height - (NativeView.Frame.Size.Height - bottom));

            // Perform the scrolling
            if (scrollAmount > 0) {
                _scrollAmount = scrollAmount;
                ScrollTheView(true);
            }

            _keyboardIsShow = true;
        }

        private UIView GetActiveView(UIView nativeView) {
            if (nativeView?.Subviews != null)
                foreach (UIView view in nativeView.Subviews) {
                    if (view.IsFirstResponder) {
                        return view;
                    } else {
                        var subView = GetActiveView(view);
                        if (subView != null)
                            return subView;
                    }
                }

            return null;
        }

        private void KeyBoardDownNotification(NSNotification notification) {
            if (_keyboardIsShow)
                ScrollTheView(false);

            _keyboardIsShow = false;
        }

        private void ScrollTheView(bool move) {
            // scroll the view up or down
            UIView.BeginAnimations(string.Empty, IntPtr.Zero);
            UIView.SetAnimationDuration(0.3);

            try {
                RectangleF frame = (RectangleF)NativeView.Frame;

                if (move) {
                    frame.Y -= _scrollAmount;
                } else {
                    frame.Y = _scrollAmount = 0;
                }

                NativeView.Frame = frame;
            }
            catch (Exception exc) {
            }
            UIView.CommitAnimations();
        }


        public System.Drawing.Point GetAbsolutePoint(UIView element) {
            double x = 0;
            double y = 0;

            var parent = element;
            while (parent != null) {
                x += parent.Frame.X;
                y += parent.Frame.Y;
                parent = parent.Superview;
            }

            return new System.Drawing.Point((int)x, (int)y);
        }
    }
}