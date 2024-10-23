using System;
using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.Nafat
{
    public class NafathPopupPageViewModel : BaseViewModel
    {
        string gulfImage = "vat_tile_listofsignup_W.png";
        public string GulfImage { get { return gulfImage; } set { gulfImage = value; OnPropertyChanged(); } }

        Color gulfTextColor = Colors.CadetBlue;
        public Color GulfTextColor { get { return gulfTextColor; } set { gulfTextColor = value; OnPropertyChanged(); } }

        string citigenImage = "vat_tile_listofsignup.png";
        public string CitigenImage { get { return citigenImage; } set { citigenImage = value; OnPropertyChanged(); } }

        Color citigenTextColor = Colors.White;
        public Color CitigenTextColor { get { return citigenTextColor; } set { citigenTextColor = value; OnPropertyChanged(); } }


        public ICommand TappedGulfCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        App.successMsg = true;
                        GulfImage = "vat_tile_listofsignup.png";
                        GulfTextColor = Colors.White;
                        CitigenImage = "vat_tile_listofsignup_W.png";
                        CitigenTextColor = Colors.CadetBlue;

                        await MopupService.Instance.PopAsync();
                        await _navigationService.NavigateTo(App.IndividualRegistrationPageView, "Gulf");
                    }
                    catch (Exception)
                    {

                    }

                });
            }
        }

        public ICommand TappedCitizenCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        App.successMsg = false;
                        GulfImage = "vat_tile_listofsignup_W.png";
                        GulfTextColor = Colors.CadetBlue;
                        CitigenImage = "vat_tile_listofsignup.png";
                        CitigenTextColor = Colors.White;


                        App.GUIDFrSSO = "";

                        await MopupService.Instance.PopAsync();
                        await _navigationService.NavigateTo(App.NafathLoginView, ZATCAConstants.NAFATH_SIGNUP);
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }


        public NafathPopupPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}

