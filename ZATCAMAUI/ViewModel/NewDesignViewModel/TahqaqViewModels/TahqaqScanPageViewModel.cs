using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EinvoiceModels;
using ZATCAMAUI.Models.TahqaqModels;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TahqaqViewModels
{
    public class TahqaqScanPageViewModel : BaseViewModel
    {
        string _scanCode;
        public string scanCode { get { return _scanCode; } set { _scanCode = value; RaisePropertyChanged(); } }

        string sellerName;
        public string SellerName { get { return sellerName; } set { sellerName = value; RaisePropertyChanged(); } }

        string vatNumber;
        public string VatNumber { get { return vatNumber; } set { vatNumber = value; RaisePropertyChanged(); } }

        string timeStamp;
        public string TimeStamp { get { return timeStamp; } set { timeStamp = value; RaisePropertyChanged(); } }

        string invoiceAmount;
        public string InvoiceAmount { get { return invoiceAmount; } set { invoiceAmount = value; RaisePropertyChanged(); } }

        string vatAmount;
        public string VatAmount { get { return vatAmount; } set { vatAmount = value; RaisePropertyChanged(); } }

        public bool FromCheckWithCode { get; set; }

        string _RegisterStatus;
        public string RegisterStatus { get { return _RegisterStatus; } set { _RegisterStatus = value; RaisePropertyChanged(); } }

        bool _IsShowRsltView;
        public bool IsShowRsltView { get { return _IsShowRsltView; } set { _IsShowRsltView = value; RaisePropertyChanged(); } }

        bool _IsShowSubmitReport;
        public bool IsShowSubmitReport { get { return _IsShowSubmitReport; } set { _IsShowSubmitReport = value; RaisePropertyChanged(); } }


        bool _IsShowFailRsltView;
        public bool IsShowFailRsltView { get { return _IsShowFailRsltView; } set { _IsShowFailRsltView = value; RaisePropertyChanged(); } }

        bool _IsShowScanView = true;
        public bool IsShowScanView { get { return _IsShowScanView; } set { _IsShowScanView = value; RaisePropertyChanged(); } }

        bool _IsCheckWithCode;
        public bool IsCheckWithCode { get { return _IsCheckWithCode; } set { _IsCheckWithCode = value; RaisePropertyChanged(); } }

        Pack _QrRslt;
        public Pack QrRslt { get { return _QrRslt; } set { _QrRslt = value; RaisePropertyChanged(); } }

        EInvoiceQRModel _eInvoiceQRModel;
        public EInvoiceQRModel eInvoiceQRModel { get { return _eInvoiceQRModel; } set { _eInvoiceQRModel = value; RaisePropertyChanged(); } }

        bool _IsClearedStatusVisible;
        public bool IsClearedStatusVisible { get { return _IsClearedStatusVisible; } set { _IsClearedStatusVisible = value; RaisePropertyChanged(); } }

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

        int NoofTags;

        int tag = 0;

        ITahqaqServices _tahqaqServices;
        public TahqaqScanPageViewModel(INavigationService navigationService, IDialogService dialogService, ITahqaqServices tahqaqServices) : base(navigationService, dialogService)
        {
            _tahqaqServices = tahqaqServices;
            eInvoiceQRModel = new EInvoiceQRModel();

        }

        #region Commands
        public ICommand ScanCommand
        {
            get
            {
                return new Command(() =>

                {

                    IsAnalyzing = false;
                    IsScanning = false;
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = true;
                        scanCode = Result.Text;
                        await CheckQr();
                        IsLoading = false;

                    });


                });
            }
        }

        public ICommand OpenReportsCommand
        {
            get
            {
                return new Command(() =>

                {

                    _navigationService.NavigateTo(App.TaxEvasionPageWebView);

                });
            }
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
                    catch (Exception)
                    {

                    }

                });
            }
        }

        public ICommand GetDataForCodeCommand
        {
            get
            {
                return new Command(async () =>

                {

                    try
                    {
                        if (!string.IsNullOrEmpty(scanCode))
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
                    catch (Exception)
                    {

                    }

                });
            }
        }

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    scanCode = "";
                    if (IsCheckWithCode || IsShowRsltView || IsShowFailRsltView)
                    {
                        if (FromCheckWithCode)
                        {
                            IsShowRsltView = IsShowFailRsltView = FromCheckWithCode = false;
                            IsCheckWithCode = true;
                            return;
                        }
                        IsCheckWithCode = IsShowRsltView = IsShowFailRsltView = FromCheckWithCode = false;
                        IsShowScanView = true;
                        IsAnalyzing = IsScanning = true;
                        _navigationService.GoBack();

                        return;
                    }
                    _navigationService.GoBack();

                });
            }
        }

        public override ICommand CloseMsgViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsShowMsgView = false;
                    IsValidationError = false;
                    IsScanning = true;
                });
            }
        }

        public ICommand ScanEnvoiceQrCommand
        {
            get
            {
                return new Command(() =>

                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {

                            IsLoading = true;

                            string code = scanCode;

                            if (code == "-1")
                            {
                                return;
                            }

                            if (!IsBase64(code))
                            {
                                eInvoiceQRModel.InvalidData = code;
                                await AddQRLog(eInvoiceQRModel);
                                IsLoading = false;
                                IsShowMsgView = true;
                                MessageTxt = AppResources.InvalidQrMessage;
                                return;
                            }
                            byte[] byteList = Convert.FromBase64String(code);
                            int currentPosition = 1;
                            int TagIndex = 0;
                            int noOfTags = 0;
                            while (currentPosition < byteList.Length)
                            {
                                // Read Length
                                int msgLength = byteList[TagIndex + 1];

                                int nextTagPositionIndex = TagIndex + msgLength + 2;

                                var message = byteList.Take(currentPosition + 1);

                                currentPosition++;

                                if (nextTagPositionIndex == currentPosition)
                                {
                                    noOfTags++;
                                    var messageAsText = Encoding.UTF8.GetString(message.Skip(TagIndex + 1).Take(nextTagPositionIndex).ToArray());
                                    TagIndex = currentPosition;
                                    currentPosition++;

                                    SetDataToModel(noOfTags, messageAsText);
                                    NoofTags = noOfTags;
                                }
                            }

                            var res = qrValidation(eInvoiceQRModel);

                            if (string.IsNullOrWhiteSpace(res))
                            {
                                bool isIntegrated = NoofTags == 9 || NoofTags == 8 ? true : false;
                                await GetQrDataEradApi(eInvoiceQRModel.vatNumber);//1 open qr res // 2 cannot verify  //3 

                            }
                            else
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.InvalidQrMessage;

                            }
                            await AddQRLog(eInvoiceQRModel);

                            IsLoading = false;
                        }
                        catch (Exception)
                        {
                            IsLoading = false;
                            IsScanning = false;
                        }
                        finally
                        {
                            IsLoading = false;
                            IsScanning = false;
                        }
                    });


                });
            }
        }
        #endregion Commands

        #region Methods
        private bool IsBase64(string base64String)
        {
            try
            {
                if (string.IsNullOrEmpty(base64String)
               || !Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None))
                    return false;


                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SetDataToModel(int tagNumber, string messageAsText)
        {
            tag = tagNumber;
            switch (tagNumber)
            {
                case 1:
                    {
                        eInvoiceQRModel.sellerName = messageAsText.Trim();
                        SellerName = messageAsText.Trim();
                        Debug.WriteLine($"Seller Name {messageAsText}");
                        break;
                    }


                case 2:
                    {
                        eInvoiceQRModel.vatNumber = messageAsText.Trim();
                        VatNumber = messageAsText.Trim();
                        Debug.WriteLine($"Vat No {messageAsText}");
                        break;
                    }


                case 3:
                    {
                        eInvoiceQRModel.timeStamp = messageAsText.Trim();

                    }
                    break;

                case 4:
                    {
                        eInvoiceQRModel.invoiceAmount = messageAsText.Trim();
                        Debug.WriteLine($"invoice Amount {messageAsText}");
                        InvoiceAmount = messageAsText.Trim();
                    }
                    break;

                case 5:
                    {
                        eInvoiceQRModel.vatAmount = messageAsText.Trim();
                        Debug.WriteLine($"vat Amount {messageAsText}");
                        VatAmount = messageAsText.Trim();
                        break;
                    }


                case 6:
                    {
                        eInvoiceQRModel.invoiceHash = messageAsText.Trim();
                        Debug.WriteLine($"invoic eHash {messageAsText}");
                        break;
                    }


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
                    }
                    break;
            }
        }

        private string qrValidation(EInvoiceQRModel qRcodeModelDetails)
        {
            var result = "";
            if (string.IsNullOrEmpty(qRcodeModelDetails.sellerName))
            {
                result = "Empty Seller Name";
            }
            else if (string.IsNullOrEmpty(qRcodeModelDetails.vatNumber))
            {
                result = "Empty Vat Number";
            }
            else if (validateNumbers(qRcodeModelDetails.vatNumber) == false)
            {
                result = "VAT numbers is not Valid ";
            }
            else if (qRcodeModelDetails.vatNumber.Length > 15)
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
            else if (double.Parse(qRcodeModelDetails.vatAmount ?? "0") < 0)
            {
                result = "VAT Amount is Negative ";
            }
            else if (checkValidDate(qRcodeModelDetails.timeStamp) == false)
            {

                result = "Date is not Valid";
            }

            return result;
        }

        private bool validateNumbers(string value)
        {
            string pattern = @"(\\.[0-9]+)?$";
            bool result = false;
            Regex regExp = new Regex(pattern);
            result = !regExp.IsMatch(value) ? false : true;
            return result;
        }

        private bool checkValidDate(string dateValue)
        {
            try
            {
                var culture = CultureInfo.InvariantCulture;
                DateTime myDate = DateTime.Now;
                DateTimeStyles styles = DateTimeStyles.AdjustToUniversal;

                if (DateTime.TryParse(dateValue, culture, styles, out myDate))
                {
                    var olddate = DateTime.Parse(dateValue);
                    var olldkind = olddate.Kind;
                    var newkind = myDate.Kind;

                    if (dateValue.Contains("Z"))
                    {
                        myDate = myDate.ToUniversalTime();
                        olddate = myDate;
                    }
                    TimeStamp = olddate.ToString("dd/MM/yyyy HH:mm");
                    return true;

                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<Location> GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                return location;
            }
            catch (FeatureNotSupportedException)
            {
            }
            catch (FeatureNotEnabledException)
            {
            }
            catch (PermissionException)
            {
            }
            catch (Exception)
            {
            }
            return new Location(54.9221801757813, -1.61353372482901);
        }

        private async Task GetQrDataEradApi(string TinNo)
        {
            try
            {
                IsLoading = true;
                if (NetworkCheck.IsInternet())
                {
                    IsClearedStatusVisible = false;
                    var body = new EradQrBody() { IDTYPE = "3", IDNUMBER = TinNo };
                    VATLookUp vatLookUp = await WebServiceManager.GAZTGetVATLookUp("A", "3", TinNo);
                    if (vatLookUp.d != null)
                    {
                        var EInvEnfStatus = vatLookUp.d.results[0].EinvEnfStatus == "" ? 0 : int.Parse(vatLookUp.d.results[0].EinvEnfStatus);
                        if (string.IsNullOrEmpty(vatLookUp.d.results[0].Description))
                        {

                            if (NoofTags == 5 && EInvEnfStatus == 0)
                            {
                                RegistredStatusWithDisplaQRRslt();
                            }
                            if (NoofTags == 5 && EInvEnfStatus == 1)
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.InvalidQrMessage;
                            }
                            else if (NoofTags == 9 && EInvEnfStatus == 1)
                            {
                                RegistredStatusWithDisplaQRRslt();

                            }
                            else if (NoofTags == 8 && EInvEnfStatus == 1)
                            {
                                RegistredStatusWithDisplaQRRslt();
                                IsClearedStatusVisible = true;

                            }
                            if (NoofTags == 8)
                            {
                                RegistredStatusWithDisplaQRRslt();
                            }
                            else
                            {
                                RegistredStatusWithDisplaQRRslt();
                            }
                        }
                        else
                        {
                            RegisterStatus = AppResources.NotRegistered;
                            IsShowSubmitReport = true;
                            IsShowRsltView = true;
                            IsShowScanView = false;
                        }

                    }
                    else
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.unableToVerify;
                    }


                }
                else
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.unableToVerify;
                }

                IsLoading = false;
            }
            catch (Exception)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.unableToVerify;
            }
            finally
            {
                IsLoading = false;
                IsScanning = false;
            }

        }

        private void RegistredStatusWithDisplaQRRslt()
        {
            RegisterStatus = AppResources.Registered;
            IsShowSubmitReport = false;
            IsShowRsltView = true;
            IsShowScanView = false;
        }

        private async Task AddQRLog(EInvoiceQRModel eInvoiceQRModel)
        {
            IsLoading = true;
            var location = await GetCurrentLocation();
            if (location != null)
            {
                eInvoiceQRModel.latitude = location.Latitude.ToString();
                eInvoiceQRModel.longitude = location.Longitude.ToString();
            }
            var model = new List<EInvoiceQRModel>
            {
                eInvoiceQRModel
            };
            await _tahqaqServices.AddQrData(model);
            IsLoading = false;
        }

        private ZXing.Result Result { get; set; }

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
                ScanDeviceLanguage = App.IsArabic ? "Arabic" : "English",
                ScanDeviceName = DeviceInfo.Name,
                ScanDeviceOS = DeviceInfo.Platform.ToString(),
                ScanDeviceOSVersion = DeviceInfo.VersionString,
                ScanLocation = $"{location.Latitude},{location.Longitude}"
            };
            var data = await _tahqaqServices.ScanQrCheck(model);

            if (data.IsSuccessStatusCode)
            {
                var contet = await data.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<QRResponseModel>(contet);
                if (res.IsValid && res.IsPackCode)
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
            IsScanning = false;
        }
        #endregion Methods

    }
}