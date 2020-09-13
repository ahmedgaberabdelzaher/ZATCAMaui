using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EGAZT.Helper;
using Java.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(GAZT.Droid.AndroidDownloader))]
namespace GAZT.Droid
{
  public class AndroidDownloader : IPrintService
    {
        public async Task Save(MemoryStream stream, string fileName)
        {
            var context = Android.App.Application.Context;
            string root = null;
            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
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
            catch (Exception e)
            {
            }

        }
    }
}