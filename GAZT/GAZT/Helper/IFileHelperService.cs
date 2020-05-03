using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
namespace GAZT.Helper
{
   public  interface IFileHelperService
    {
        MemoryStream GetFileStream();
        //Gets the file stream in UWP
        Task<MemoryStream> GetFileStreamAsync();
    }
}
