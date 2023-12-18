using ZATCAMAUI.Models.EinvoiceModels;
using ZATCAMAUI.Models.TahqaqModels;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface ITahqaqServices
    {
        Task<HttpResponseMessage> ScanQrCheck(QrScanModel model);
        Task<HttpResponseMessage> GetEInvoiceData(string id);
        Task<HttpResponseMessage> AddQrData(List<EInvoiceQRModel> qrScanModel);
        Task<HttpResponseMessage> GetEInvoiceDataEradAPI(EradQrBody body);

    }
}
