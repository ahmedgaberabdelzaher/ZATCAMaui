using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.EinvoiceModels;
using ZATCAMAUI.Models.EinvoiceModels.DPModels;
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

             var response = await HttpManager.PostAsync(PageSettings.ZATCABaseURL + $"/v1/vat/lookups", body,false,"",true).ConfigureAwait(false);
            return response;
          
        }
        public async Task<HttpResponseMessage> AddQrData(List<EInvoiceQRModel> qrScanModel)
        {
            ///Old Direct VAT services
            ///
            // var response = await HttpManager.PostAsync(App.VatBaseUrl+"/QRLog/AddQRLogs", qrScanModel).ConfigureAwait(false);
            var body = new QRLogModel();
            body.languageCode = App.IsArabic ? "ar" : "en";
            body.QRLogs = new List<QRLog>();
            var QRLog = new QRLog();
            foreach (var item in qrScanModel)
            {
                QRLog.longitude = item.longitude;
                QRLog.CASignature = item.caSignature;
                QRLog.invalidData = item.InvalidData;
                QRLog.invoiceAmount = item.invoiceAmount;
                QRLog.invoiceHash = item.invoiceHash;
                QRLog.sellerName = item.sellerName;
                QRLog.signature = item.signature;
                QRLog.status = item.Status;
                QRLog.timeStamp = item.timeStamp;
                QRLog.VATAmount = item.vatAmount;
                QRLog.VATNumber = item.vatNumber;
                body.QRLogs.Add(QRLog);
            }
            var response = await HttpManager.PostAsync(PageSettings.ZATCABaseURL + "v1/vat/qr-logs", body).ConfigureAwait(false);

            return response;
        }
    }
}
