using GAZT.Droid.DependencyServices;
using Xamarin.Forms;
using GAZT.Helper;
using Android.OS;
using System;
using System.IO;

[assembly: Dependency(typeof(DeviceInfo))]
namespace GAZT.Droid.DependencyServices
{
    public class DeviceInfo : IDeviceInfo
    {
        public double GetDeviceHeight()
        {
            double height = 0;
            height = (double)Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.HeightPixels / (double)Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density;
            return height;
        }
        public double GetDeviceWidth()
        {
            double width = 0;
            width = (double)Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.WidthPixels / (double)Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density;
            return width;
        }

        public string[] GetAttachmentTypeString()
        {
            string[] filetypes;
            filetypes = new string[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "image/jpeg", "image/jpg", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "image/png", "application/vnd.ms-powerpoint", "application/vnd.openxmlformats-officedocument.presentationml.presentation", "image/gif", "text/plain" };
            return filetypes;
        }

        public string[] GetAttachmentTypeStringForAll()
        {
            string[] filetypesForAll = new string[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "image/jpeg", "image/jpg", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "image/png", "application/vnd.ms-powerpoint", "application/vnd.openxmlformats-officedocument.presentationml.presentation", "image/gif", "text/plain" };
            return filetypesForAll;
        }


        public string[] GetAttachmentTypeStringForTaxEvasion()
        {
            string[] filetypesForTaxEvasion = new string[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "image/jpeg", "image/jpg", "text/plain" };
            return filetypesForTaxEvasion;
        }

            public string[] GetAttachmentTypeStringForZakat()
        {
            string[] filetypesforZakat = new string[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "image/jpeg", "image/jpg", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };
            return filetypesforZakat;
        }

        public string GetDeviceUdid()
        {
            string id = string.Empty;

            if (!string.IsNullOrWhiteSpace(id))
                return id;

            id = Android.OS.Build.Serial;
            if (string.IsNullOrWhiteSpace(id) || id == Build.Unknown || id == "0")
            {
                try
                {
                    var context = Android.App.Application.Context;
                    id = Android.Provider.Settings.Secure.GetString(context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
                }
                catch (Exception ex)
                {
                    id = "";
                    //Android.Util.Log.Warn("DeviceInfo", "Unable to get id: " + ex.ToString());
                }
            }

            return id;
        }
        public string GetAttachmentToDownloadsPath(string fileName, string fileContents)
        {
            byte[] myByte = System.Text.ASCIIEncoding.Default.GetBytes(fileContents);

            var downloadDirectory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
            var filePath = Path.Combine(downloadDirectory, fileName);

            try
            {
                var streamWriter = File.Create(filePath);
                streamWriter.Close();
                File.WriteAllBytes(filePath, myByte);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

            return filePath;
        }
    }
}
