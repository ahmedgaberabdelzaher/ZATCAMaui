using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Models.TahqaqModels;

namespace EGAZT.Services.Interface
{
    public interface ITahqaqServices
    {
        Task<HttpResponseMessage> ScanQrCheck(QrScanModel model);
        Task<Tuple<EinvoiceQRCodeResponse, bool, string>> GetEInvoiceData(int id);

    }
}
