using System;
using System.Diagnostics;
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
            scanCode = "SAA6216738003275";
       
        }

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
                    Debug.WriteLine(IsAnalyzing);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = true;
                        scanCode = Result.Text;
                        Debug.WriteLine(scanCode);
                        await CheckQr();
                        IsLoading = false;
                       
                    });

                   
                });
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


        public ZXing.Result Result { get; set; }
        public async Task CheckQr()
        {
            IsLoading = true;
            var location = await GetCurrentLocation();
            var model = new QrScanModel()
            {
                ScanCode = scanCode,
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
