using System;
using System.IO;
using System.Threading.Tasks;
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