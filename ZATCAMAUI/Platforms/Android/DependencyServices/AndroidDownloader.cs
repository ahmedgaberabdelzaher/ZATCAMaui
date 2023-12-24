using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Platforms.Android.DependencyServices;
using Environment = Android.OS.Environment;
using Application = Android.App.Application;
using Java.IO;
using Android.Widget;

[assembly: Dependency(typeof(AndroidDownloader))]
namespace ZATCAMAUI.Platforms.Android.DependencyServices
{
    public class AndroidDownloader : IPrintService
    {
        public async Task Save(MemoryStream stream, string fileName)
        {
            var context = Application.Context;
            string root = null;
            if (Environment.IsExternalStorageEmulated)
            {
                root = Environment.ExternalStorageDirectory.ToString();
            }
            else
                root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/Documents");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            if (file.Exists()) file.Delete();

            try
            {
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(stream.ToArray());
                outs.Flush();
                outs.Close();
                Toast.MakeText(context, "Downloaded File:" + file.Path, ToastLength.Long).Show();
            }
            catch (Exception)
            {
            }

        }
    }
}
