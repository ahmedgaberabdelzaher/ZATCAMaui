using Foundation;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using UIKit;
using ZATCAMAUI.Platforms.iOS.CustomRenderer;

[assembly: ExportRenderer(typeof(Page), typeof(KeyboardRender))]
namespace ZATCAMAUI.Platforms.iOS.CustomRenderer
{
    public class KeyboardRender : PageRenderer
    {
        private static bool _isEnabled;
        private NSObject _keyboardShowObserver;
        private NSObject _keyboardHideObserver;
        private bool _pageWasShiftedUp;
        private bool _isKeyboardShown;
        private double _viewHeight;
        public static void Init(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (!_isEnabled)
            {
                return;
            }
            RegisterForKeyboardNotifications();
        }
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            UnregisterForKeyboardNotifications();
        }
        private void RegisterForKeyboardNotifications()
        {
            _keyboardShowObserver = _keyboardShowObserver ?? NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardShow);
            _keyboardHideObserver = _keyboardHideObserver ?? NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardHide);
        }
        private void UnregisterForKeyboardNotifications()
        {
            _isKeyboardShown = false;
            if (_keyboardShowObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardShowObserver);
                _keyboardShowObserver.Dispose();
                _keyboardShowObserver = null;
            }
            if (_keyboardHideObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardHideObserver);
                _keyboardHideObserver.Dispose();
                _keyboardHideObserver = null;
            }
        }
        protected virtual void OnKeyboardShow(NSNotification notification)
        {
            if (!IsViewLoaded || _isKeyboardShown) return;
            _isKeyboardShown = true;
            var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
            ShiftPageUp(keyboardFrame.Height);
        }
        private void OnKeyboardHide(NSNotification notification)
        {
            if (!IsViewLoaded) return;
            _isKeyboardShown = false;
            var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
            if (_pageWasShiftedUp)
            {
                ShiftPageDown();
            }
        }
        private void ShiftPageUp(nfloat keyboardHeight)
        {
            var pageFrame = Element.Bounds;
            _viewHeight = pageFrame.Height;
            var newHeight = pageFrame.Height - keyboardHeight;
            Element.LayoutTo(new Rect(pageFrame.X, pageFrame.Y,
               pageFrame.Width, newHeight));
            _pageWasShiftedUp = true;
        }
        private void ShiftPageDown()
        {
            var pageFrame = Element.Bounds;
            var newHeight = _viewHeight;
            Element.LayoutTo(new Rect(pageFrame.X, pageFrame.Y,
             pageFrame.Width, newHeight));
            _pageWasShiftedUp = false;
        }
    }
}
