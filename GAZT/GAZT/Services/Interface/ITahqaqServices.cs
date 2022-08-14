using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Models.TahqaqModels;

namespace EGAZT.Services.Interface
{
    public interface ITahqaqServices
    {
        Task<HttpResponseMessage> ScanQrCheck(QrScanModel model);
        Task<HttpResponseMessage> GetEInvoiceData(string id);
        Task<HttpResponseMessage> AddQrData(List<EInvoiceQRModel>  qrScanModel);

    }
}
