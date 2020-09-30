using GAZT.Droid.DependencyServices;
using Xamarin.Forms;
using GAZT.Helper;
using Android.OS;
using System;
using System.Linq;
using System.IO;
using System;
using System.Linq;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Telephony;
using Android.Provider;
using Android.Util;
using Android.Views;
using Android.Runtime;
using Java.IO;
using Java.Lang;
using B = Android.OS.Build;
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
                catch (System.Exception ex)
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

            var downloadDirectory = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
            var filePath = System.IO.Path.Combine(downloadDirectory, fileName);

            try
            {
                var streamWriter = System.IO.File.Create(filePath);
                streamWriter.Close();
                System.IO.File.WriteAllBytes(filePath, myByte);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

            return filePath;
        }

        public string Model => throw new NotImplementedException();

        public string OperatingSystem => throw new NotImplementedException();

        public string OperatingSystemVersion => throw new NotImplementedException();

        public bool IsSimulator => throw new NotImplementedException();

        public bool IsTablet => throw new NotImplementedException();

        static readonly string[] checks = new[]
        {
            "/system/app/Superuser.apk",
            "/sbin/su",
            "/system/bin/su",
            "/system/xbin/su",
            "/data/local/xbin/su",
            "/data/local/bin/su",
            "/system/sd/xbin/su",
            "/system/bin/failsafe/su",
            "/data/local/su",
            "/su/bin/su"
        };


        //protected virtual bool CheckJailBreakProcess()
        //{
        //    try
        //    {
        //        using (var process = Runtime.GetRuntime().Exec("/system/xbin/which", new[] { "su" }))
        //        {
        //            using (var reader = new BufferedReader(new InputStreamReader(process.InputStream)))
        //            {
        //                if (reader.ReadLine() != null)
        //                    return true;
        //            }
        //        }

        //        return false;
        //    }
        //    catch(System.Exception ex)
        //    {
        //        return false;
        //    }
        //}


        bool IDeviceInfo.IsJailBreakDetected()
        {
            if (checks.Any(System.IO.File.Exists))
                return true;

            if (B.Tags?.Contains("test-keys") ?? false)
                return true;

            //if (this.CheckJailBreakProcess())
            //    return true;

            return false;
        }

        public byte[] GetImagePathByteArray(string filePath)
        {
            byte[] base64Image = null;
            try
            {
                base64Image = System.IO.File.ReadAllBytes(filePath);
                //base64Image = Convert.ToBase64String(imageArray);

            }
            catch (System.Exception e)
            {

            }
            return base64Image;
        }
    }
}
