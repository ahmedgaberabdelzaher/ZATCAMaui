using System;
using System.IO;
using System.Threading.Tasks;
using pdfjs.Droid.PlatformSpecifics;
using pdfjs.Interfaces;
using Xamarin.Forms;
[assembly: Dependency(typeof(LocalFileProvider))]
namespace pdfjs.Droid.PlatformSpecifics
{
    public class LocalFileProvider : ILocalFileProvider
    {
        private readonly string _rootDir = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "pdfjs");
        public async Task<string> SaveFileToDisk(Stream pdfStream, string fileName)
        {
            string filePath = null;
            try
            {
                if (!Directory.Exists(_rootDir))
                    Directory.CreateDirectory(_rootDir);
                filePath = Path.Combine(_rootDir, fileName);
                using (var memoryStream = new MemoryStream())
                {
                    await pdfStream.CopyToAsync(memoryStream);
                    File.WriteAllBytes(filePath, memoryStream.ToArray());
                }
                return filePath;
            }
            catch(Exception)
            {
            }
            return filePath;
        }
    }
}