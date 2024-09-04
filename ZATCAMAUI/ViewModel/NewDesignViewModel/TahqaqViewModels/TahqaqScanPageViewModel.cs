using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Greensoft.TlvLib;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.EinvoiceModels;
using ZATCAMAUI.Models.TahqaqModels;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TahqaqViewModels
{
    public class TahqaqScanPageViewModel : BaseViewModel
    {
        string _scanCode;
        public string scanCode { get { return _scanCode; } set { _scanCode = value; OnPropertyChanged(); } }

        string sellerName;
        public string SellerName { get { return sellerName; } set { sellerName = value; OnPropertyChanged(); } }
        string vatNumber;
        public string VatNumber { get { return vatNumber; } set { vatNumber = value; OnPropertyChanged(); } }
        string timeStamp;
        public string TimeStamp { get { return timeStamp; } set { timeStamp = value; OnPropertyChanged(); } }
        double invoiceAmount;
        public double InvoiceAmount { get { return invoiceAmount; } set { invoiceAmount = value; OnPropertyChanged(); } }
        string vatAmount;
        public string VatAmount { get { return vatAmount; } set { vatAmount = value; OnPropertyChanged(); } }


        ITahqaqServices _tahqaqServices;
        public TahqaqScanPageViewModel(INavigationService navigationService, IDialogService dialogService, ITahqaqServices tahqaqServices) : base(navigationService, dialogService)
        {
            _tahqaqServices = tahqaqServices;
            eInvoiceQRModel = new EInvoiceQRModel();
            //  scanCode = "SAA6216738003275";
            //ScanEnvoiceQrCommand.Execute(null);

        }

        string _RegisterStatus;
        public string RegisterStatus { get { return _RegisterStatus; } set { _RegisterStatus = value; OnPropertyChanged(); } }

        bool _IsShowRsltView;
        public bool IsShowRsltView { get { return _IsShowRsltView; } set { _IsShowRsltView = value; OnPropertyChanged(); } }

        bool _IsShowSubmitReport;
        public bool IsShowSubmitReport { get { return _IsShowSubmitReport; } set { _IsShowSubmitReport = value; OnPropertyChanged(); } }


        bool _IsShowFailRsltView;
        public bool IsShowFailRsltView { get { return _IsShowFailRsltView; } set { _IsShowFailRsltView = value; OnPropertyChanged(); } }

        bool _IsShowScanView = true;
        public bool IsShowScanView { get { return _IsShowScanView; } set { _IsShowScanView = value; OnPropertyChanged(); } }

        bool _IsCheckWithCode;
        public bool IsCheckWithCode { get { return _IsCheckWithCode; } set { _IsCheckWithCode = value; OnPropertyChanged(); } }

        Pack _QrRslt;
        public Pack QrRslt { get { return _QrRslt; } set { _QrRslt = value; OnPropertyChanged(); } }

        EInvoiceQRModel _eInvoiceQRModel;
        public EInvoiceQRModel eInvoiceQRModel { get { return _eInvoiceQRModel; } set { _eInvoiceQRModel = value; OnPropertyChanged(); } }

        bool _IsClearedStatusVisible;
        public bool IsClearedStatusVisible { get { return _IsClearedStatusVisible; } set { _IsClearedStatusVisible = value; OnPropertyChanged(); } }



        bool _IsAnalyzing = true;
        public bool IsAnalyzing
        {
            get { return _IsAnalyzing; }
            set
            {

                _IsAnalyzing = value;
                OnPropertyChanged();
            }
        }

        bool _IsScanning = true;
        public bool IsScanning
        {
            get { return _IsScanning; }
            set
            {

                _IsScanning = value;
                OnPropertyChanged();
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
                    _navigationService.NavigateTo("SubmitReportPage");

                });
            }
        }
        public bool IsBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String)
               || !Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None))
                return false;

            try
            {
                try
                {
                    Convert.FromBase64String(base64String);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        int NoofTags;

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
                            code = "ASVaYW1pbCBPcGVyYXRpb25zICYgTWFpbnRlbmFuY2UgQ28gTHRkAg8zMTAxMzY4NDAzMDAwMDMDEzIwMjMtMTItMThUMDY6NDM6MjUEBzExNzYuOTEFBjE1My41MQYsVDI2RjBMYzVvTHpGenZTVjNIU1JLQnIwNSsvQmRmRG93bzU1VjhHNitwOD0HYE1FUUNJRGVnSUw5MStMTHN1c3F5Ukd2djd5cUZ5ZEtsTmQ0UnhXZ3JLQ1c0Vmd5cUFpQk04SDhYaWlMclhrZTZzVm9LeUo0TXRuS2NCZDUyV281VlpRUHZtcVByT1E9PQhYMFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAEouqS1tSXHqT8suzSdB7CJVLlQZnGe8B12TYwC8O4PqJJVEFHOHV3nzdenUmVyRzExqlrGHhfJ1yB+jrEECWyZg==";
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
                            MemoryStream stream = new MemoryStream(byteList);



                            int currentPosition = 1;
                            int TagIndex = 0;
                            int noOfTags = 0;
                            TlvEncoding.ProcessTlvStream(stream,
                            (tag, data) => {
                                var messageAsText = Encoding.UTF8.GetString(data);
                                SetDataToModel(int.Parse(tag.ToString()), messageAsText);
                                NoofTags = (int)tag;
                            });

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
                        catch (Exception )
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

        int tag = 0;

        private void SetDataToModel(int tagNumber, string messageAsText)
        {
            tag = tagNumber;
            switch (tagNumber)
            {
                case 1:
                    {
                        eInvoiceQRModel.sellerName = new string(messageAsText.Trim().Where(c => !char.IsControl(c)).ToArray());
                        SellerName = new string(messageAsText.Where(c => !char.IsControl(c)).ToArray());
                        Debug.WriteLine($"Seller Name {messageAsText}");
                        break;
                    }


                case 2:
                    {
                        eInvoiceQRModel.vatNumber = new string(messageAsText.Where(c => !char.IsControl(c)).ToArray());
                        VatNumber = messageAsText.TrimStart().Trim();
                        Debug.WriteLine($"Vat No {messageAsText.TrimStart()}");
                        break;
                    }


                case 3:
                    {
                        eInvoiceQRModel.timeStamp = new string(messageAsText.Where(c => !char.IsControl(c)).ToArray());


                    }
                    break;

                case 4:
                    {
                        eInvoiceQRModel.invoiceAmount = messageAsText.Trim();
                        Debug.WriteLine($"invoice Amount {messageAsText}");
                        InvoiceAmount = double.Parse(messageAsText.Trim());
                    }
                    break;

                case 5:
                    {
                        eInvoiceQRModel.vatAmount = messageAsText.Trim().Replace(" ", "");
                        Debug.WriteLine($"vat Amount {messageAsText.Trim()}");
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
                        //statements;
                    }
                    break;
            }
        }


        string qrValidation(EInvoiceQRModel qRcodeModelDetails)
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
            else if (qRcodeModelDetails.vatNumber.Replace(" ", "").Length - 1 > 15)
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
            /* else if (vat < 0)
             {
                 result = "VAT Amount is Negative ";
             }*/
            else if (checkValidDate(qRcodeModelDetails.timeStamp.Replace(" ", "")) == false)
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
            result = !regExp.IsMatch(value) ? false : true;
            return result;
        }

        bool checkValidDate(string dateValue)
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
            catch (Exception)
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
                    catch (Exception)
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


        public async Task GetQrData(string vatId)
        {
            try
            {
                IsLoading = true;
                if (NetworkCheck.IsInternet())
                {

                    var data = await _tahqaqServices.GetEInvoiceData(vatId);
                    if (data.IsSuccessStatusCode)
                    {
                        RegisterStatus = AppResources.Registered;
                        IsShowSubmitReport = false;
                    }
                    else if (data.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        RegisterStatus = AppResources.NotRegistered;
                        IsShowSubmitReport = true;
                    }
                    else
                    {
                        RegisterStatus = AppResources.unableToVerify;
                    }


                }
                else
                {
                    RegisterStatus = AppResources.unableToVerify;
                }

                IsLoading = false;
            }
            catch (Exception)
            {

            }
            finally
            {
                IsLoading = false;
            }

        }


        public async Task GetQrDataEradApi(string TinNo)
        {
            try
            {
                IsLoading = true;
                if (NetworkCheck.IsInternet())
                {
                    IsClearedStatusVisible = false;
                    var body = new EradQrBody() { idType = "3", idNumber = TinNo.Replace(" ","") };
                    var res =await _tahqaqServices.GetEInvoiceDataEradAPI(body);
                    var content =await res.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject< DATAPowerBaseResponseResult<VATLookUpD> >(content);
                    var qrResponseData = result.result;
                    if (qrResponseData != null&& qrResponseData.results != null & qrResponseData.results.Count>0)
                    {

                        if (qrResponseData.results[0].EinvEnfStatus == null)
                        {
                            qrResponseData.results[0].EinvEnfStatus = "";
                        }
                        var EInvEnfStatus = qrResponseData.results[0].EinvEnfStatus == "" ? 0 : int.Parse(qrResponseData.results[0].EinvEnfStatus);
                        if (string.IsNullOrEmpty(qrResponseData.results[0].Description))
                        {

                            if (NoofTags == 5 && EInvEnfStatus == 0)
                            {
                                RegistredStatusWithDisplaQRRslt();
                            }
                            else if (NoofTags == 5 && EInvEnfStatus == 1)
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
                            else if (NoofTags == 8)
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
                RegisterStatus = AppResources.NotRegistered;
                IsShowSubmitReport = true;
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

        public async Task AddQRLog(EInvoiceQRModel eInvoiceQRModel)
        {
            IsLoading = true;
            var location = await GetCurrentLocation();
            if (location != null)
            {
                eInvoiceQRModel.latitude = location.Latitude.ToString();
                eInvoiceQRModel.longitude = location.Longitude.ToString();
            }
            var model = new List<EInvoiceQRModel>();
            model.Add(eInvoiceQRModel);
            var data = await _tahqaqServices.AddQrData(model);
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
                ScanDeviceLanguage = App.IsArabic ? "Arabic" : "English",
                ScanDeviceName = DeviceInfo.Name,
                ScanDeviceOS = DeviceInfo.Platform.ToString(),
                ScanDeviceOSVersion = DeviceInfo.VersionString,
                ScanLocation = $"{location.Latitude},{location.Longitude}"
            };
            var data = await _tahqaqServices.ScanQrCheck(model);
            Debug.WriteLine(data.IsSuccessStatusCode);

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
    }
}
