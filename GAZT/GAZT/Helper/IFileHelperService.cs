using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace GAZT.Helper
{
    [Preserve(AllMembers = true)]
    public  interface IFileHelperService
    {
        MemoryStream GetFileStream();
        //Gets the file stream in UWP
        Task<MemoryStream> GetFileStreamAsync();
    }
}
