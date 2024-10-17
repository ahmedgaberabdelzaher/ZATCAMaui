using CoreGraphics;
using Foundation;
using MobileCoreServices;
using UIKit;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Platforms.iOS.DependencyServices;

[assembly: Dependency(typeof(IOSDownloader))]
namespace ZATCAMAUI.Platforms.iOS.DependencyServices
{
    public class IOSDownloader : IPrintService
    {
        const double LONG_DELAY = 3.5;
        const double SHORT_DELAY = 2.0;

        NSTimer alertDelay;
        UIAlertController alert;
        // private readonly string _rootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Documents");


        private readonly string _rootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library");

        string filePath = "";
        public  void Save(MemoryStream stream, string fileName)
        {
            if (!Directory.Exists(_rootDir))
                Directory.CreateDirectory(_rootDir);

            filePath = Path.Combine(_rootDir, fileName);

            //using (var memoryStream = new MemoryStream())
            //{
            //    await stream.CopyToAsync(memoryStream);
            //    File.WriteAllBytes(filePath, stream.ToArray());
            //}



            File.WriteAllBytes(filePath, stream.ToArray());

            ShowDocsPicker();


            //Message("Downloaded File:" + filePath);
        }

        public UIViewController GetCurrentUIController()
        {
            UIViewController viewController;
            var window = UIApplication.SharedApplication.KeyWindow;
            if (window == null)
            {
                return null;
            }

            if (window.RootViewController.PresentedViewController == null)
            {
                window = UIApplication.SharedApplication.Windows
                         .First(i => i.RootViewController != null &&
                                     i.RootViewController.GetType().FullName
                                     .Contains(typeof(Platform).FullName));
            }

            viewController = window.RootViewController;

            while (viewController.PresentedViewController != null)
            {
                viewController = viewController.PresentedViewController;
            }

            return viewController;
        }



        private void ShowDocsPicker()
        {
            try
            {
                UIDocumentInteractionController documentController = new UIDocumentInteractionController();
                documentController.Url = new NSUrl(filePath, false);
                string fileExtension = Path.GetExtension(filePath).Substring(1);
                string uti = UTType.CreatePreferredIdentifier(UTType.TagClassFilenameExtension.ToString(), fileExtension, null);
                documentController.Uti = uti;

                UIView presentingView = UIApplication.SharedApplication.KeyWindow.RootViewController.View;
                documentController.PresentOpenInMenu(CGRect.Empty, presentingView, true);
            }
            catch (Exception)
            {
                //Exception Logging


            }
        }


        public void Message(string message)
        {
            // ShowAlert(message, LONG_DELAY);
        }
        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                dismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }
        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}
