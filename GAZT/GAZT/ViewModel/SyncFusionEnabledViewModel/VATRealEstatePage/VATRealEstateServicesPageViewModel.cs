using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATRealEstatePage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class VATRealEstateServicesPageViewModel : ViewModelBase
    {
        private ObservableCollection<eServiceInfo> _eServicesItems = null;
        private ICommand EserviceCommand { get; set; }
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand BackButtonClicked { get; set; }
        public ICommand PropertyRegistrationCommand { get; set; }
        public ICommand RequestVerificationCommand { get; set; }
        public ICommand TerminateRequestCommand { get; set; }


        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        private string _ImagePropertyTile = "vat_tile_listofsignup_W.png";
        public string ImagePropertyTile
        {
            get
            {
                return _ImagePropertyTile;
            }
            set
            {
                _ImagePropertyTile = value;
                RaisePropertyChanged("ImagePropertyTile");
            }
        }

        private string _ImagePropertyIcon = "re_Property_Registration.png";
        public string ImagePropertyIcon
        {
            get
            {
                return _ImagePropertyIcon;
            }
            set
            {
                _ImagePropertyIcon = value;
                if (_ImagePropertyIcon.Equals("re_Property_Registration_G.png"))
                {

                    PropertyTileColor = Color.FromHex("#006450");



                }
                else
                {
                    PropertyTileColor = Color.White;
                }
                RaisePropertyChanged("ImagePropertyIcon");
            }
        }
        private string _ImageRequestTile = "re_Tile_Background_White.png";
        public string ImageRequestTile
        {
            get
            {
                return _ImageRequestTile;
            }
            set
            {
                _ImageRequestTile = value;
                RaisePropertyChanged("ImageRequestTile");
            }
        }

        private string _ImageRequestIcon = "re_Request_Verification_W.png";
        public string ImageRequestIcon
        {
            get
            {
                return _ImageRequestIcon;
            }
            set
            {
                _ImageRequestIcon = value;
                if (_ImageRequestIcon.Equals("re_Request_Verification.png"))
                {




                    RequestTileColor = Color.White;
                }
                else
                {
                    RequestTileColor = Color.FromHex("#006450");

                }
                RaisePropertyChanged("ImageRequestIcon");
            }
        }
        private string _ImageTerminationTile = "re_Tile_Background_White.png";
        public string ImageTerminationTile
        {
            get
            {
                return _ImageTerminationTile;
            }
            set
            {
                _ImageTerminationTile = value;
                RaisePropertyChanged("ImageTerminationTile");
            }
        }
        private string _ImageTerminationIcon = "re_Termination_Request_W.png";
        public string ImageTerminationIcon
        {
            get
            {
                return _ImageTerminationIcon;
            }
            set
            {
                _ImageTerminationIcon = value;
                if (_ImageTerminationIcon.Equals("re_Termination_Request.png"))
                {

                    TerminateTileColor = Color.White;
                }
                else
                {
                    TerminateTileColor = Color.FromHex("#006450");

                }
                RaisePropertyChanged("ImageTerminationIcon");
            }
        }
        private Color _PropertyTileColor = Color.FromHex("#006450");
        public Color PropertyTileColor
        {
            get
            {
                return _PropertyTileColor;
            }
            set
            {
                _PropertyTileColor = value;

                RaisePropertyChanged("PropertyTileColor");
            }
        }
        private Color _RequestTileColor = Color.FromHex("#006450");
        public Color RequestTileColor
        {
            get
            {
                return _RequestTileColor;
            }
            set
            {
                _RequestTileColor = value;
                RaisePropertyChanged("RequestTileColor");
            }
        }
        private Color _TerminateTileColor = Color.FromHex("#006450");
        public Color TerminateTileColor
        {
            get
            {
                return _TerminateTileColor;
            }
            set
            {
                _TerminateTileColor = value;
                RaisePropertyChanged("TerminateTileColor");
            }
        }
        public VATRealEstateServicesPageViewModel(INavigationService navigationService, IDialogService dialogService) //: base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            BackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });

            this.PropertyRegistrationCommand = new Command(this.PropertyRegistrationCommandClicked);
            this.RequestVerificationCommand = new Command(this.RequestVerificationCommandClicked);
            this.TerminateRequestCommand = new Command(this.TerminateRequestCommandClicked);

        }
        private void PropertyRegistrationCommandClicked(object obj)
        {

            ImagePropertyTile = "re_Tile_Background.png";
           
            ImagePropertyIcon = "re_Property_Registration.png";

            PropertyTileColor = Color.White;
          

            Device.BeginInvokeOnMainThread(() =>
            {
                PropertyRegistrationPageViewModel.reServiceName = AppResources.ZVATRealEstatePropertyRegistration;

                _navigationService.NavigateTo(App.PropertyRegistrationPage);
                
            });


        }
        private void  RequestVerificationCommandClicked(object obj)
        {

            ImageRequestTile = "re_Tile_Background_S.png";

            ImageRequestIcon = "re_Request_Verification_W.png";

            RequestTileColor = Color.White;
    

            Device.BeginInvokeOnMainThread(() =>
            {
                PropertyRegistrationPageViewModel.reServiceName = AppResources.ZVATRealEstateRequestVerification;

                _navigationService.NavigateTo(App.PropertyRegistrationPage);
               
            });


        }
        private void TerminateRequestCommandClicked(object obj)
        {

            ImageTerminationTile = "re_Tile_Background_S.png";

            ImageTerminationIcon = "re_Termination_Request_W.png";
            TerminateTileColor = Color.White;

            Device.BeginInvokeOnMainThread(() =>
            {
                PropertyRegistrationPageViewModel.reServiceName = AppResources.ZVATRealEstateTerminationOfRequest;

                _navigationService.NavigateTo(App.PropertyRegistrationPage);
                
            });


        }
        public ObservableCollection<eServiceInfo> eServicesAvailableToTheTP
        {
            get
            {
                return this._eServicesItems;
            }
            set
            {
                this._eServicesItems = value;
                this.RaisePropertyChanged("eServicesAvailableToTheTP");
            }
        }

        public void PopulateServicesData()
        {
            eServicesAvailableToTheTP = new ObservableCollection<eServiceInfo>();
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZVATRealEstatePropertyRegistration, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Payment.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZVATRealEstateRequestVerification, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VerifyRequest.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZVATRealEstateTerminationOfRequest, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Cancel_payment.png" });
        }
    }
}
