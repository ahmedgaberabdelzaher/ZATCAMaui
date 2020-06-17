using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using GAZT.Helper;
using GAZT.iOS.DependencyServices;
using Xamarin.Forms;
using MobileCoreServices;

[assembly: Dependency(typeof(DeviceInfo))]
namespace GAZT.iOS.DependencyServices
{
    public class DeviceInfo : IDeviceInfo
    {
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
            string[]  filetypesforZakat = new string[] {
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
            catch(Exception ex)
            {
                return "";
            }
        }
        public string GetAttachmentToDownloadsPath(string fileP, string fileX)
        {
            string result = "";

            return result;
        }
    }
}
