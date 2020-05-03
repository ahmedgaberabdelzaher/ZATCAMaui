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
using GAZT.Helper;
namespace GAZT.Droid
{
    public  class FileHelperService : IFileHelperService
    {
        public MemoryStream GetFileStream()
        {
            string path = Android.OS.Environment.ExternalStorageDirectory.Path;
            string filePath = Path.Combine(path, "GIT_Succinctly.pdf");
            return new MemoryStream(File.ReadAllBytes(filePath));
        }
        //Will not be called for Android
        public Task<MemoryStream> GetFileStreamAsync()
        {
            throw new NotImplementedException();
        }
        Task<MemoryStream> IFileHelperService.GetFileStreamAsync()
        {
            throw new NotImplementedException();
        }
    }
}