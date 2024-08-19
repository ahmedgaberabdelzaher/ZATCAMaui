using Android.OS;
using static Android.Provider.Settings;
using Environment = Android.OS.Environment;
using Application = Android.App.Application;
using B = Android.OS.Build;
using ZATCAMAUI.Platforms.Android.DependencyServices;

[assembly: Dependency(typeof(ZATCADeviceInfo))]
namespace ZATCAMAUI.Platforms.Android.DependencyServices
{
    public class ZATCADeviceInfo : Core.Interfaces.IDeviceInfoZATCA
    {
        public double GetDeviceHeight()
        {
            double height = 0;
            height = (double)DeviceDisplay.Current.MainDisplayInfo.Height / (double)DeviceDisplay.Current.MainDisplayInfo.Density;
            return height;
        }
        public double GetDeviceWidth()
        {
            double width = 0;
            width = (double)DeviceDisplay.Current.MainDisplayInfo.Width / (double)DeviceDisplay.Current.MainDisplayInfo.Density;
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

            id = Build.Serial;
            if (string.IsNullOrWhiteSpace(id) || id == Build.Unknown || id == "0")
            {
                try
                {
                    var context = Application.Context;
                    id = Secure.GetString(context.ContentResolver, Secure.AndroidId);
                }
                catch (System.Exception)
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

            var downloadDirectory = System.IO.Path.Combine(Environment.ExternalStorageDirectory.AbsolutePath, Environment.DirectoryDownloads);
            var filePath = System.IO.Path.Combine(downloadDirectory, fileName);

            try
            {
                var streamWriter = System.IO.File.Create(filePath);
                streamWriter.Close();
                System.IO.File.WriteAllBytes(filePath, myByte);
            }
            catch (System.Exception)
            {
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


        bool Core.Interfaces.IDeviceInfoZATCA.IsJailBreakDetected()
        {
            if (checks.Any(System.IO.File.Exists))
                return true;

            if (B.Tags?.Contains("test-keys") ?? false)
                return true;

            return false;
        }

        public byte[] GetImagePathByteArray(string filePath)
        {
            byte[] base64Image = null;
            try
            {
                base64Image = System.IO.File.ReadAllBytes(filePath);

            }
            catch (System.Exception)
            {

            }
            return base64Image;
        }
    }
}
