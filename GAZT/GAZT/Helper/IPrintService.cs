using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace EGAZT.Helper
{
    [Preserve(AllMembers = true)]
    public interface IPrintService
    {
        Task Save(MemoryStream stream, string fileName);
    }
}
