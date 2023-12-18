using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.EinvoiceModels;
using ZATCAMAUI.Models.TahqaqModels;

namespace ZATCAMAUI.Core.Services.Classes
{
    public class TahqaqServices : ITahqaqServices
    {

        public async Task<HttpResponseMessage> ScanQrCheck(QrScanModel model)
        {
            var response = await HttpManager.PostAsync(PageSettings.TahqaqBaseURl + $"scancode/savedetails", model, true).ConfigureAwait(false);
            return response;
        }

        public async Task<HttpResponseMessage> GetEInvoiceData(string id)
        {
            var response = await HttpManager.PostAsync(App.VatBaseUrl + $"/Report/QRCodeRead?id={id}", new QrScanModel() { ScanCode = "" }).ConfigureAwait(false);
            return response;
        }
        public async Task<HttpResponseMessage> GetEInvoiceDataEradAPI(EradQrBody body)
        {
            var response = await HttpManager.PostAsync(PageSettings.EinvoiceBaseURl + $"/RESTAdapter/T2/TAXPAYER", body, false, "", true).ConfigureAwait(false);
            return response;
        }
        public async Task<HttpResponseMessage> AddQrData(List<EInvoiceQRModel> qrScanModel)
        {
            var response = await HttpManager.PostAsync(App.VatBaseUrl + "/QRLog/AddQRLogs", qrScanModel).ConfigureAwait(false);
            return response;
        }
    }
}
