using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{

    public class VATIndividualSignupPageViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public Command IndividualRegistrationCommand { get; set; }
        public Command EstablishmentSignupCommand { get; set; }


       

        private string _ImageIndividualTile = "vat_tile_listofsignup_W.png";
        public string ImageIndividualTile
        {
            get
            {
                return _ImageIndividualTile;
            }
            set
            {
                if (_ImageIndividualTile == value) return;

                _ImageIndividualTile = value;
                RaisePropertyChanged("ImageIndividualTile");
            }
        }

        private string _ImageIndividualIcon = "vat_new_individual_G.png";
        public string ImageIndividualIcon
        {
            get
            {
                return _ImageIndividualIcon;
            }
            set
            {
                if (_ImageIndividualIcon == value) return;

                _ImageIndividualIcon = value;
                if (_ImageEstimatedIcon.Equals("vat_new_individual_G.png"))
                {

                    IndividualTileColor = (Color)Application.Current.Resources["Primary"];



                }
                else
                {
                    IndividualTileColor = Colors.White;
                }
                RaisePropertyChanged("ImageIndividualIcon");
            }
        }
        private string _ImageEstimatedTile = "vat_tile_listofsignup_W.png";
        public string ImageEstimatedTile
        {
            get
            {
                return _ImageEstimatedTile;
            }
            set
            {
                if (_ImageEstimatedTile == value) return;

                _ImageEstimatedTile = value;
                RaisePropertyChanged("ImageEstimatedTile");
            }
        }
        private string _ImageEstimatedIcon = "vat_new_Establishment_G.png";
        public string ImageEstimatedIcon
        {
            get
            {
                return _ImageEstimatedIcon;
            }
            set
            {
                if (_ImageEstimatedIcon == value) return;

                _ImageEstimatedIcon = value;
                if (_ImageEstimatedIcon.Equals("vat_new_Establishment_G.png"))
                {




                    EstimatedTileColor = Colors.White;
                }
                else
                {
                    EstimatedTileColor = (Color)Application.Current.Resources["Primary"];

                }
                RaisePropertyChanged("ImageEstimatedIcon");
            }
        }
        private Color _IndividualTileColor = (Color)Application.Current.Resources["Primary"];
        public Color IndividualTileColor
        {
            get
            {
                return _IndividualTileColor;
            }
            set
            {
                if (_IndividualTileColor == value) return;

                _IndividualTileColor = value;

                RaisePropertyChanged("IndividualTileColor");
            }
        }
        private Color _EstimatedTileColor = (Color)Application.Current.Resources["Primary"];
        public Color EstimatedTileColor
        {
            get
            {
                return _EstimatedTileColor;
            }
            set
            {
                if (_EstimatedTileColor == value) return;

                _EstimatedTileColor = value;
                RaisePropertyChanged("EstimatedTileColor");
            }
        }



        public VATIndividualSignupPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            IndividualRegistrationCommand = new Command(OnIndividualRegistrationClicked);
            EstablishmentSignupCommand = new Command(OnEstablishmentSignupClicked);
        }
        //EstablishmentSignupCommand
        private async void OnIndividualRegistrationClicked(object obj)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
                ImageIndividualTile = "vat_tile_listofsignup.png";
               
                ImageIndividualIcon = "vat_new_individual.png";
            });

            MainThread.BeginInvokeOnMainThread(() =>
            {
                _navigationService.NavigateTo(App.IndividualRegistrationPageView);
                //_navigationService.NavigateTo(App.StyleTestUIPageView);
                // IsLoading = false;
            });
        }
        private void OnEstablishmentSignupClicked(object obj)
        {

            IsLoading = true;
            ImageEstimatedTile = "vat_tile_listofsignup.png";
            ImageEstimatedIcon = "vat_new_Establishment_W.png";
            EstimatedTileColor = Colors.White;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _navigationService.NavigateTo(App.SignUpForEstablishmentPageView);
                //IsLoading = false;
            });
        }
    }
}
