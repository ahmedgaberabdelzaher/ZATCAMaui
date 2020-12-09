using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace pdfjs.Interfaces
{
    [Preserve(AllMembers = true)]
    public interface ILocalFileProvider
    {
        Task<string> SaveFileToDisk(Stream stream, string fileName);
    }
}