using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.EinvoiceModels;
using EGAZT.Models.SurveyModels;
using EGAZT.Models.TahqaqModels;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Encoders;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.TahqaqViewModels
{
    public class TahqaqScanPageViewModel:BaseViewModel
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


        ITahqaqServices _tahqaqServices;
        public TahqaqScanPageViewModel(INavigationService navigationService, IDialogService dialogService,ITahqaqServices tahqaqServices) : base(navigationService, dialogService)
        {
            _tahqaqServices = tahqaqServices;
            eInvoiceQRModel = new EInvoiceQRModel();
            //  scanCode = "SAA6216738003275";
            //ScanEnvoiceQrCommand.Execute(null);
       
        }

        string _RegisterStatus;
        public string RegisterStatus { get { return _RegisterStatus; } set { _RegisterStatus = value; RaisePropertyChanged(); } }

        bool _IsShowRsltView;
        public bool IsShowRsltView { get { return _IsShowRsltView; } set { _IsShowRsltView = value;  RaisePropertyChanged(); } }

        bool _IsShowSubmitReport;
        public bool IsShowSubmitReport { get { return _IsShowSubmitReport; } set { _IsShowSubmitReport = value; RaisePropertyChanged(); } }


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

        public bool IsBase64(string base64String)
        {//|| base64String.Length % 4 != 0
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
                catch (Exception ex)
                {
                    return false;
                }
             
            }
            catch (Exception exception)
            {
                return false;
                // Handle the exception
            }
            return false;
        }

        int NoofTags;

        public ICommand ScanEnvoiceQrCommand
        {
            get
            {
                return new Command(() =>

                {
                    Device.BeginInvokeOnMainThread(async() =>
                    {
                        try
                        {

                       IsLoading = true;

                        // string code = Result.Text;
                        string code = scanCode;
                            //   code = "AYGO2KfZhNi02LHZg9ipINin2YTYudin2YTZhdmK2Kkg2KfZhNmF2YjYp9ivINin2YTYqNmG2KfYoSDYp9mE2YXYrdiv2YjYr9ipINio2YrZhtmD2LMgfCBUaGUgSW50ZXJuYXRpb25hbCBDby4gZm9yIEJ1aWxkaW5nIE1hdGVyaWFscyBMdGQiQklORVgiLgIPMzAwMjQ1MTk4NzAwMDAzAxQyMDIzLTA3LTEyVDE1OjIxOjQ1WgQGMTcyLjUwBQUyMi41MAYABwAIAAkA";
                           // code = "AT5KYWhleiBJbnRlcm5hdGlvbmFsIENvbXBhbnkgZm9yIEluZm9ybWF0aW9uIFN5c3RlbXMgVGVjaG5vbG9neQIPMzEwMTkxNjI3NDEwMDAzAxMyMDIzLTAxLTA2VDE0OjIyOjA4BAQ5LjAwBQQxLjE3";
                            if (code=="-1")
                        {
                            return;
                        }
                       /* if (code.Length!=15)
                        {
                            // string code = "ASR2YWx1ZXMuYWRkcmVzcy5raXRvcGlTYS50YXhwYXllck5hbWUCDzMxMDQwOTY1NTcwMDAwMwMYMjAyMi0wNy0xOVQxMzozMjo0My40MTFaBAU0Mi4wMAUENS40OA==";
                      IsShowMsgView = true;
                            MessageTxt = AppResources.InValidCode;
                        }*/
                        if (!IsBase64(code))
                        {
                            eInvoiceQRModel.InvalidData = code;
                              await AddQRLog(eInvoiceQRModel);
                                IsLoading = false;
                            IsShowMsgView = true;
                            MessageTxt = AppResources.InvalidQrMessage;
                            return;
                        }
                        //  code = "AUrYtNix2YPYqSDYp9mE2K/YsdmK2LMg2YTZhNiu2K/Zhdin2Kog2KfZhNio2KrYsdmI2YTZitipINmI2KfZhNmG2YLZhNmK2KfYqgIPMzAwMDU2NDYyMzAwMDAzAxQyMDIzLTA1LTEzVDE5OjI1OjU5WgQFNTAuMDIFBDYuNTIGLFhLcyt4M2VrM1JvY21yS2lMdzdhZVZuaitNZDdHRnhML2NjNmk3dmRRRkE9B2BNRVFDSUtNblpOeHlYb3NOTGpKalZPcWQvUDI5WHJxQi95TmJ0ZmQ1Wm5PcGRXVGtBaUJWQUE2eFNTWkxHekFsaGdqcVlyQmFobHZIZzVZTkdHVUFGZW9BTXgyUVpBPT0IWDBWMBAGByqGSM49AgEGBSuBBAAKA0IABI/9OKmqTjHjta6j6JOIz11T1SRSiy9OCaTaepysFnlhzgeii+nknOn8bOYqsvq2xuY6GaPPKBD+7qytEWk6cWgJRjBEAiA2MdHOYnHsV7VtGZFcxuNek53vqGO//1OZO70/oTyZqQIgLF1Vc+ANeI0cqw52ytxWJWLb7KqC+q+wRBckr+6j0hE=";
                        byte[] byteList = Convert.FromBase64String(code);
                        int currentPosition = 1;
                            // NoofTags = byteList.Length;
                            int tagNumber = 0;
                            while (currentPosition<byteList.Length)
                        {
                                // int tagNumber = byteList[currentPosition];
                                tagNumber++;
                            currentPosition++;
                            // Read Length
                            int valueLength = byteList[currentPosition];
                            Debug.WriteLine(valueLength);

                            currentPosition++;
                            // Read Message
                            int lastPosition = currentPosition + valueLength+1;
                            var message = byteList.Skip(currentPosition).Take(lastPosition- (currentPosition + 1));
                            String messageAsText = Encoding.UTF8.GetString(message.ToArray());
                            Debug.WriteLine(messageAsText);
                            // Utf8Decoder().convert(message.toList());
                            currentPosition += valueLength;
                            SetDataToModel(tagNumber, messageAsText);
                                NoofTags = tagNumber;
                               // tagNumber++;

                        }
                            if (NoofTags<5)
                            {
                                currentPosition = 0;
                                tagNumber = 0;
                                while (currentPosition < byteList.Length)
                                {
                                    // int tagNumber = byteList[currentPosition];

                                    currentPosition++;
                                    tagNumber++;
                                    // Read Length
                                    int valueLength = byteList[currentPosition];
                                    Debug.WriteLine(valueLength);

                                    currentPosition++;
                                    // Read Message
                                    int lastPosition = currentPosition + valueLength + 1;
                                    var message = byteList.Skip(currentPosition).Take(lastPosition - (currentPosition + 1));
                                    String messageAsText = Encoding.UTF8.GetString(message.ToArray());
                                    Debug.WriteLine(messageAsText);
                                    // Utf8Decoder().convert(message.toList());
                                    currentPosition += valueLength;
                                    SetDataToModel(tagNumber, messageAsText);
                                    NoofTags = tagNumber;
                                  

                                }
                            }
                       var res= qrValidation(eInvoiceQRModel);
                            if ((NoofTags <= 5 && res!="")|| (NoofTags <= 8 && res != ""))
                            {
                                currentPosition = 0;
                                tagNumber = 0;
                                while (currentPosition < byteList.Length)
                                {
                                    // int tagNumber = byteList[currentPosition];

                                    currentPosition++;
                                    tagNumber++;
                                    // Read Length
                                    int valueLength = byteList[currentPosition];
                                    Debug.WriteLine(valueLength);

                                    currentPosition++;
                                    // Read Message
                                    int lastPosition = currentPosition + valueLength + 1;
                                    var message = byteList.Skip(currentPosition).Take(lastPosition - (currentPosition + 1));
                                    String messageAsText = Encoding.UTF8.GetString(message.ToArray());
                                    Debug.WriteLine(messageAsText);
                                    // Utf8Decoder().convert(message.toList());
                                    currentPosition += valueLength;
                                    SetDataToModel(tagNumber, messageAsText);
                                    NoofTags = tagNumber;


                                }
                                res = qrValidation(eInvoiceQRModel);
                            }
                            if (res=="")
                        {
                                bool isIntegrated = NoofTags == 9 || NoofTags == 8 ? true : false; 
                             await GetQrDataEradApi(eInvoiceQRModel.vatNumber);//1 open qr res // 2 cannot verify  //3 

                                ///Old Scenario
                            /*    IsShowRsltView = true;
                            IsShowScanView = false;
                            await GetQrData(eInvoiceQRModel.vatNumber);*/
                        }
                        else
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.InvalidQrMessage;

                        }
                       
                       IsLoading = false;
                       await AddQRLog(eInvoiceQRModel);

                        }
                        catch (Exception ex)
                        {

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
                       // TimeStamp = messageAsText.Trim();

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
               var culture = CultureInfo.InvariantCulture;
                DateTime myDate = DateTime.Now;
                DateTimeStyles styles = DateTimeStyles.AdjustToUniversal;

                if (DateTime.TryParse(dateValue,culture,styles, out myDate))
                /*if(DateTime.TryParseExact(dateValue, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture,
       DateTimeStyles.AdjustToUniversal, out myDate))*/
               /* if(DateTime.TryParseExact(dateValue, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture,
    DateTimeStyles.AdjustToUniversal, out myDate))*/
                {//2023-05-13T19:25:59Z
                    /* var date = DateTime.TryParseExact(dateValue, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture,
       DateTimeStyles.AdjustToUniversal,out myDate);*/
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
                if (Helper.NetworkCheck.IsInternet())
                {

                var data = await _tahqaqServices.GetEInvoiceData(vatId);
                if (data.IsSuccessStatusCode)
                {
                    RegisterStatus = AppResources.Registered;
                    IsShowSubmitReport = false;
                }
                else if (data.StatusCode==System.Net.HttpStatusCode.BadRequest)
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
                    RegisterStatus= AppResources.unableToVerify;
                }

                IsLoading = false;
            }
            catch (Exception ex)
            {

            }
            finally {
                IsLoading = false;
            }
       
        }

        public async Task GetQrDataEradApi(string TinNo)
        {
            try
            {
                IsLoading = true;
                if (Helper.NetworkCheck.IsInternet())
                {
                    IsClearedStatusVisible = false;
                    var body = new EradQrBody() { IDTYPE="3", IDNUMBER=TinNo};
                    VATLookUp vatLookUp = await WebServiceManager.GAZTGetVATLookUp("A", "3", TinNo);
                    //// Old APi T2
                    // var body = new EradQrBody() { IDTYPE="1",IDNUMBER= "3001720579" };
                   // var data = await _tahqaqServices.GetEInvoiceDataEradAPI(body);
                    if (vatLookUp.d != null)
                    {
                        //   var content =await data.Content.ReadAsStringAsync();
                        ///  var qrResponseData = JsonConvert.DeserializeObject<EradQRResponseModel>(content);
                        var EInvEnfStatus = vatLookUp.d.results[0].EinvEnfStatus==""?0:int.Parse(vatLookUp.d.results[0].EinvEnfStatus);
                        if (string.IsNullOrEmpty(vatLookUp.d.results[0].Description))
                        {
                           
                            if (NoofTags==5&& EInvEnfStatus == 0)
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
                            if (NoofTags==8)
                            {
                                RegistredStatusWithDisplaQRRslt();
                               // IsShowSubmitReport = false;
                            }
                            else
                            {
                                RegistredStatusWithDisplaQRRslt();
                            }
                        }
                        else
                        {
                           /* if (NoofTags == 5)
                            {*/
                                RegisterStatus = AppResources.NotRegistered;
                                IsShowSubmitReport = true;
                                IsShowRsltView = true;
                                IsShowScanView = false;
                           /* }
                           else
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.InvalidQrMessage;
                            }*/
                        }
                  
                    }
                    /*else if (data.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        RegisterStatus = AppResources.NotRegistered;
                        IsShowSubmitReport = true;
                    }*/
                    else
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.unableToVerify;
                        // RegisterStatus = AppResources.unableToVerify;
                    }


                }
                else
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.unableToVerify;
                   // RegisterStatus = AppResources.unableToVerify;
                }

                IsLoading = false;
            }
            catch (Exception ex)
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

        public async Task AddQRLog(EInvoiceQRModel eInvoiceQRModel)
        {
            IsLoading = true;
            var location = await GetCurrentLocation();
            if (location!=null)
            {
      eInvoiceQRModel.latitude = location.Latitude.ToString();
      eInvoiceQRModel.longitude = location.Longitude.ToString();
            }
            var model = new List<EInvoiceQRModel>();
            model.Add(eInvoiceQRModel);
            var data = await _tahqaqServices.AddQrData(model);
            if (data.IsSuccessStatusCode)
            {
              //  RegisterStatus = AppResources.Registered;
            }
            else
            {
               // RegisterStatus = AppResources.NotRegistered;
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
