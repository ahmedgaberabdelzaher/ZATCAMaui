
using Foundation;
using MobileCoreServices;
using UIKit;
using ZATCAMAUI.Platforms.iOS.DependencyServices;

[assembly: Dependency(typeof(ZATCADeviceInfo))]
namespace ZATCAMAUI.Platforms.iOS.DependencyServices
{
    public class ZATCADeviceInfo : Core.Interfaces.IDeviceInfoZATCA
    {
        public string Model => throw new NotImplementedException();

        public string OperatingSystem => throw new NotImplementedException();

        public string OperatingSystemVersion => throw new NotImplementedException();

        public bool IsSimulator => throw new NotImplementedException();

        public bool IsTablet => throw new NotImplementedException();

        public double GetDeviceHeight()
        {
            double height = 0;
            height = (double)UIScreen.MainScreen.Bounds.Height;
            return height;
        }
        public double GetDeviceWidth()
        {
            double width = 0;
            width = (double)UIScreen.MainScreen.Bounds.Width;
            return width;
        }

        public string[] GetAttachmentTypeString()
        {
            string[] filetypes;
            filetypes = new string[] {
                UTType.PDF,
                "org.openxmlformats.wordprocessingml.document",
                "com.microsoft.word.doc",
    "org.openxmlformats.spreadsheetml.sheet",
    "org.openxmlformats.presentationml.presentation",
                UTType.JPEG,
                UTType.PNG,
                UTType.GIF,
                "com.microsoft.excel.xls",
                "com.microsoft.powerpoint.​ppt",
                 UTType.PlainText
                            };

            return filetypes;

        }

        public string[] GetAttachmentTypeStringForAll()
        {
            string[] filetypesForAll = new string[] {
                UTType.PDF,
                "org.openxmlformats.wordprocessingml.document",
                "com.microsoft.word.doc",
    "org.openxmlformats.spreadsheetml.sheet",
    "org.openxmlformats.presentationml.presentation",
                UTType.JPEG,
                UTType.PNG,
                UTType.GIF,
                "com.microsoft.excel.xls",
                "com.microsoft.powerpoint.​ppt",
                 UTType.Text
                            };

            return filetypesForAll;
        }

        public string[] GetAttachmentTypeStringForTaxEvasion()
        {
            string[] filetypesForTaxEvasion = new string[] {
                UTType.PDF,
                "org.openxmlformats.wordprocessingml.document",
                "com.microsoft.word.doc",
                UTType.JPEG,
                 UTType.Text
                            };

            return filetypesForTaxEvasion;
        }

        public string[] GetAttachmentTypeStringForZakat()
        {
            string[] filetypesforZakat = new string[] {
                UTType.PDF,
                "org.openxmlformats.wordprocessingml.document",
                "com.microsoft.word.doc",
    "org.openxmlformats.spreadsheetml.sheet",
                UTType.JPEG,
                "com.microsoft.excel.xls",
                            };
            return filetypesforZakat;
        }

        public string GetDeviceUdid()
        {
            try
            {
                return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
            }
            catch (Exception)
            {

                return "";
            }
        }
        public string GetAttachmentToDownloadsPath(string fileP, string fileX)
        {
            string result = "";

            var PreviewController = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(fileX));
            PreviewController.Delegate = new UIDocumentInteractionControllerDelegateClass(UIApplication.SharedApplication.KeyWindow.RootViewController);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                PreviewController.PresentPreview(true);
            });

            return result;
        }
        public class UIDocumentInteractionControllerDelegateClass : UIDocumentInteractionControllerDelegate
        {
            UIViewController ownerVC;
            public UIDocumentInteractionControllerDelegateClass(UIViewController vc)
            {
                ownerVC = vc;
            }

            public override UIViewController ViewControllerForPreview(UIDocumentInteractionController controller)
            {
                return ownerVC;
            }
            public override UIView ViewForPreview(UIDocumentInteractionController controller)
            {
                return ownerVC.View;
            }
        }

        bool Core.Interfaces.IDeviceInfoZATCA.IsJailBreakDetected()
        {
            try
            {
                var paths = new[]
                                {
                    "/Applications/Cydia.app",
                    "/private/var/lib/cydia",
                    "/private/var/tmp/cydia.log",
                    "/System/Library/LaunchDaemons/com.saurik.Cydia.Startup.plist",
                    "/usr/libexec/sftp-server",
                    "/usr/bin/sshd",
                    "/usr/sbin/sshd",
                    "/Applications/FakeCarrier.app",
                    "/Applications/SBSettings.app",
                    "/Applications/WinterBoard.app",
                };

                return paths.Any(System.IO.File.Exists);
                //return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public byte[] GetImagePathByteArray(string filePath)
        {
            byte[] base64Image = null;

            try
            {
                base64Image = System.IO.File.ReadAllBytes(filePath);
            }
            catch (Exception)
            {



                base64Image = null;
            }

            return base64Image;
        }
    }
}
