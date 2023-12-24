using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Platforms.Android.DependencyServices;
using Environment = Android.OS.Environment;
[assembly: Dependency(typeof(LocalFileProvider))]
namespace ZATCAMAUI.Platforms.Android.DependencyServices
{
    public class LocalFileProvider : ILocalFileProvider
    {
        private readonly string _rootDir = Path.Combine(Environment.ExternalStorageDirectory.Path, "pdfjs");
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
            catch (Exception)
            {
            }
            return filePath;
        }
    }
}
