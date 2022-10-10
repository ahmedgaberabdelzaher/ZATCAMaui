using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.TahqaqModels;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.TahqaqViewModels
{
    public class TahqaqScanPageViewModel:BaseViewModel
    {
        string _scanCode;
        public string scanCode { get { return _scanCode; } set { _scanCode = value; RaisePropertyChanged(); } }
        ITahqaqServices _tahqaqServices;
        public TahqaqScanPageViewModel(INavigationService navigationService, IDialogService dialogService,ITahqaqServices tahqaqServices) : base(navigationService, dialogService)
        {
            _tahqaqServices = tahqaqServices;
            eInvoiceQRModel = new EInvoiceQRModel();
            //  scanCode = "SAA6216738003275";
            ScanEnvoiceQrCommand.Execute(null);
       
        }

        string _RegisterStatus;
        public string RegisterStatus { get { return _RegisterStatus; } set { _RegisterStatus = value; RaisePropertyChanged(); } }

        bool _IsShowRsltView;
        public bool IsShowRsltView { get { return _IsShowRsltView; } set { _IsShowRsltView = value;  RaisePropertyChanged(); } }

        bool _IsShowFailRsltView;
        public bool IsShowFailRsltView { get { return _IsShowFailRsltView; } set { _IsShowFailRsltView = value; RaisePropertyChanged(); } }

        bool _IsShowScanView=true;
        public bool IsShowScanView { get { return _IsShowScanView; } set { _IsShowScanView = value; RaisePropertyChanged(); } }

        bool _IsCheckWithCode ;
        public bool IsCheckWithCode { get { return _IsCheckWithCode; } set { _IsCheckWithCode = value; RaisePropertyChanged(); } }

        Pack _QrRslt;
        public Pack QrRslt { get { return _QrRslt; } set { _QrRslt = value; RaisePropertyChanged(); } }

        EInvoiceQRModel _eInvoiceQRModel;
        public EInvoiceQRModel eInvoiceQRModel { get { return _eInvoiceQRModel; } set { _eInvoiceQRModel = value; RaisePropertyChanged(); } }
       


        bool _IsAnalyzing = true;
        public bool IsAnalyzing
        {
            get { return _IsAnalyzing; }
            set
            {

                _IsAnalyzing = value;
                RaisePropertyChanged();
            }
        }

         bool _IsScanning = true;
        public bool IsScanning
        {
            get { return _IsScanning; }
            set
            {

                _IsScanning = value;
                RaisePropertyChanged();
            }
        }


        public ICommand ScanCommand
        {
            get
            {
                return new Command(() =>

                {

                    IsAnalyzing = false;
                    IsScanning = false;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = true;
                        scanCode = Result.Text;
                        await CheckQr();
                        IsLoading = false;
                       
                    });

                   
                });
            }
        }

        public ICommand ScanEnvoiceQrCommand
        {
            get
            {
                return new Command(() =>

                {
                    Device.BeginInvokeOnMainThread(async() =>
                    {
                       IsLoading = true;

                        // string code = Result.Text;

                       string code = "ASR2YWx1ZXMuYWRkcmVzcy5raXRvcGlTYS50YXhwYXllck5hbWUCDzMxMDQwOTY1NTcwMDAwMwMYMjAyMi0wNy0xOVQxMzozMjo0My40MTFaBAU0Mi4wMAUENS40OA==";
                       Debug.WriteLine(code);
                        byte[] byteList = Convert.FromBase64String(code);
                        int currentPosition = 0;
                        while (currentPosition<byteList.Length)
                        {
                            int tagNumber = byteList[currentPosition];
                           
                            currentPosition++;
                            // Read Length
                            int valueLength = byteList[currentPosition];
                            Debug.WriteLine(valueLength);

                            currentPosition++;
                            // Read Message
                            int lastPosition = currentPosition + valueLength;
                            var message = byteList.Skip(currentPosition+1).Take(lastPosition- (currentPosition + 1));
                            String messageAsText = Encoding.UTF8.GetString(message.ToArray());
                            Debug.WriteLine(messageAsText);
                            // Utf8Decoder().convert(message.toList());
                            currentPosition += valueLength;
                            SetDataToModel(tagNumber, messageAsText);
                            IsShowRsltView = true;
                            IsShowScanView = false;
                          // await GetQrData(int.Parse(eInvoiceQRModel.vatNumber));
                            IsLoading = false;
                        }
                       
                       
                    });

                   
                });
            }
        }
       
        private void SetDataToModel(int tagNumber, string messageAsText)
        {
            switch (tagNumber)
            {
                case 1:
                    {
                        eInvoiceQRModel.sellerName = messageAsText.Trim();
                        Debug.WriteLine($"Seller Name {messageAsText}");

                    }
                    break;

                case 2:
                    {
                        eInvoiceQRModel.vatNumber = messageAsText.Trim();
                        Debug.WriteLine($"Vat No {messageAsText}");
                    }
                    break;

                case 3:
                    {
                        eInvoiceQRModel.timeStamp = messageAsText.Trim();

                    }
                    break;

                case 4:
                    {
                        eInvoiceQRModel.invoiceAmount = messageAsText.Trim();
                        Debug.WriteLine($"invoice Amount {messageAsText}");
                    }
                    break;

                case 5:
                    {
                        eInvoiceQRModel.vatAmount = messageAsText.Trim();
                        Debug.WriteLine($"vat Amount {messageAsText}");
                    }
                    break;

                case 6:
                    {
                        eInvoiceQRModel.invoiceHash = messageAsText.Trim();
                        Debug.WriteLine($"invoic eHash {messageAsText}");
                    }
                    break;

                case 7:
                    {
                        eInvoiceQRModel.ecdsapublicKey = messageAsText.Trim();
                        Debug.WriteLine($"ecdsapublicKey {messageAsText}");
                    }
                    break;

                case 8:
                    {
                        eInvoiceQRModel.signature = messageAsText.Trim();
                    }
                    break;

                case 9:
                    {
                        eInvoiceQRModel.caSignature = messageAsText.Trim();
                    }
                    break;

                default:
                    {
                        //statements;
                    }
                    break;
            }
        }


        string qrValidation(EInvoiceQRModel qRcodeModelDetails)
        {
            var result = "";
            if (String.IsNullOrEmpty(qRcodeModelDetails.sellerName))
            {
                result = "Empty Seller Name";
            }
            else if(String.IsNullOrEmpty(qRcodeModelDetails.vatNumber))
            {
                result = "Empty Vat Number";
            }
            else if (validateNumbers(qRcodeModelDetails.vatNumber) == false)
            {
                result = "VAT numbers is not Valid ";
            }
            else if (qRcodeModelDetails.vatNumber.Length>15)
            {
                result = "VAT numbers more  than 10 digit";
            }
            else if (qRcodeModelDetails.invoiceAmount.Contains(" "))
            {
                result = "invoiceAmount is contains white Spaces";
            }
            else if (validateNumbers(qRcodeModelDetails.vatAmount) == false)
            {
                result = "VAT Amount not Valid ";
            }
            else if (double.Parse(qRcodeModelDetails.vatAmount) < 0)
            {
                result = "VAT Amount is Negative ";
            }
            else if (checkValidDate(qRcodeModelDetails.timeStamp) == false)
            {
                result = "Date is not Valid";
            }
            return result;
        }

        bool validateNumbers(string value)
        {
            string pattern = @"(\\.[0-9]+)?$";
            bool result = false;
            Regex regExp = new Regex(pattern);
           result= !regExp.IsMatch(value) ?  false : true;
            return result;
        }

        bool compareTwoDates(string dateValue)
        {
          
            var now =DateTime.Now;
           var tempDate = DateTime.Parse(dateValue);
            if (tempDate.Date<now.Date)
            {
                return false;
            }
            return true;
        }

        bool checkValidDate(string dateValue)
        {
            try
            {
               var culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTime myDate = DateTime.Now;
                DateTimeStyles styles = DateTimeStyles.None;

                if (DateTime.TryParse("2/30/2010", culture, styles, out myDate))
                {
                    return true;

                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        async Task<Location> GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
               var cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                return location;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
            }
            catch (FeatureNotEnabledException fneEx)
            {
            }
            catch (PermissionException pEx)
            {
            }
            catch (Exception ex)
            {
            }
            return new Location(54.9221801757813, -1.61353372482901);
        }

        public ICommand EnterCodeCommand
        {
            get
            {
                return new Command(() =>

                {

                    try
                    {
                        scanCode = "";
                        IsShowScanView = false;
                        IsCheckWithCode = true;
                    }
                    catch (Exception ex)
                    {

                    }
                   
                });
            }
        }

        public ICommand GetDataForCodeCommand
        {
            get
            {
                return new Command(async() =>

                {

                    try
                    {
                        if (!String.IsNullOrEmpty(scanCode))
                        {

                        FromCheckWithCode = true;
                       await CheckQr();
                        }
                        else
                        {

                            MessageTxt = AppResources.PleaseEnterQRCodeHer;
                            IsShowMsgView = true;
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                });
            }
        }
        public bool FromCheckWithCode { get; set; }
        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    scanCode = "";
                    if (IsCheckWithCode|| IsShowRsltView||IsShowFailRsltView)
                    {
                        if (FromCheckWithCode)
                        {
                            IsShowRsltView = IsShowFailRsltView= FromCheckWithCode = false;
                            IsCheckWithCode = true;
                            return;
                        }
                        IsCheckWithCode=IsShowRsltView=IsShowFailRsltView= FromCheckWithCode = false;
                         IsShowScanView = true;
                        IsAnalyzing = IsScanning = true;


                        return;
                    }
                    _navigationService.GoBack();
                   
                });
            }
        }


        public async Task GetQrData(int vatId)
        {
            IsLoading = true;
            var location = await GetCurrentLocation();
  
            var data = await _tahqaqServices.GetEInvoiceData(vatId);
            if (data.Item2)
            {
                RegisterStatus = AppResources.Registered;
            }
            else
            {
                RegisterStatus= AppResources.NotRegistered;
            }
            IsLoading = false;
        }

        public ZXing.Result Result { get; set; }
        public async Task CheckQr()
        {
            IsLoading = true;
            var location = await GetCurrentLocation();
            var model = new QrScanModel()
            {
                ScanCode = scanCode.ToUpper(),
                ScanCodeType = "",
                ScanCustomerId = 1,
                ScanDateTime = DateTime.Now,
                ScanDeviceId = "9DF468D3-C8914EFE-BC19-121E35AB598",
                ScanDeviceLanguage =App.IsArabic?"Arabic":"English",
                ScanDeviceName = Xamarin.Essentials.DeviceInfo.Name,
                ScanDeviceOS = Xamarin.Essentials.DeviceInfo.Platform.ToString(),
                ScanDeviceOSVersion = Xamarin.Essentials.DeviceInfo.VersionString,
                ScanLocation = $"{location.Latitude},{location.Longitude}"
            };
            var data = await _tahqaqServices.ScanQrCheck(model);
            Debug.WriteLine(data.IsSuccessStatusCode);

            if (data.IsSuccessStatusCode)
            {
                var contet = await data.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<QRResponseModel>(contet);
                if (res.IsValid&& res.IsPackCode)
                {
                    IsShowRsltView = true;
                    IsShowScanView = false;
             
                    QrRslt = res.pack;

                }
                else
                {
                    IsShowFailRsltView = true;
                    IsShowScanView = false;
                    Debug.WriteLine(IsShowRsltView);
                }
            }
            IsLoading = false;
        }



    }
}
