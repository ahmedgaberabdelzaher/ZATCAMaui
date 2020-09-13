using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EGAZT.Helper
{
    public interface IPrintService
    {
        Task Save(MemoryStream stream, string fileName);
    }
}
