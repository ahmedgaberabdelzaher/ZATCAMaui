using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    [Preserve(AllMembers = true)]
    public class VATIndividualSignupPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        
        public Command IndividualRegistrationCommand { get; set; }
        public Command EstablishmentSignupCommand { get; set; }


        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private string _ImageIndividualTile = "vat_tile_listofsignup_W.png";
        public string  ImageIndividualTile
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

                    IndividualTileColor =  (Color)Application.Current.Resources["Primary"];
                    


                }
                else
                {
                    IndividualTileColor = Color.White;
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




                    EstimatedTileColor = Color.White;
                }
                else
                {
                    EstimatedTileColor =  (Color)Application.Current.Resources["Primary"];

                }
                RaisePropertyChanged("ImageEstimatedIcon");
            }
        }
        private Color _IndividualTileColor =  (Color)Application.Current.Resources["Primary"];
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
        private Color _EstimatedTileColor =  (Color)Application.Current.Resources["Primary"];
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



        public VATIndividualSignupPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            this.IndividualRegistrationCommand = new Command(this.OnIndividualRegistrationClicked);
            this.EstablishmentSignupCommand = new Command(this.OnEstablishmentSignupClicked);
        }
        //EstablishmentSignupCommand
        private async void OnIndividualRegistrationClicked(object obj)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
                ImageIndividualTile= "vat_tile_listofsignup.png";
                //viewModel.ImageIndividualTile = "vat_tile_listofsignup_W.png";
                //image_estimated_tile.Source = "vat_tile_listofsignup.png";
                //viewModel.ImageEstimatedTile = "vat_tile_listofsignup_W.png";
                ImageIndividualIcon = "vat_new_individual.png";
                //viewModel.ImageIndividualIcon = "vat_new_individual_G.png";
                //// image_estimated_icon.Source = "vat_new_Establishment_W.png";
                //viewModel.ImageEstimatedIcon = "vat_new_Establishment_G.png";
                //viewModel.EstimatedTileColor =  (Color)Application.Current.Resources["Primary"];
                //viewModel.IndividualTileColor =  (Color)Application.Current.Resources["Primary"];
            });
            
            Device.BeginInvokeOnMainThread( () =>
            {
                _navigationService.NavigateTo(App.IndividualRegistrationPageView);
                //_navigationService.NavigateTo(App.StyleTestUIPageView);
                // IsLoading = false;
            });
        }
        private void OnEstablishmentSignupClicked(object obj)
        {
           
                IsLoading = true;
                ImageEstimatedTile= "vat_tile_listofsignup.png";
                ImageEstimatedIcon = "vat_new_Establishment_W.png";
                EstimatedTileColor = Color.White;
            Device.BeginInvokeOnMainThread(() =>
            {
                _navigationService.NavigateTo(App.SignUpForEstablishmentPageView);
                 //IsLoading = false;
            });
        }
    }
}
