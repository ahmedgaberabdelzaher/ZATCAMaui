using System;
using System.Windows.Input;
using EGAZT.AppConfigurations;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.ZakatyViewModels
{
    public class AboutZakatyViewModel:BaseViewModel
    {

        public ICommand GoToZakatyPortalCommand
        {

            get
            {
                return new Command(() =>
                {
                    try
                    {
                        Xamarin.Essentials.Launcher.OpenAsync(PageSettings.ZakatyPortalURl);
                    }
                    catch (Exception ex)
                    {

                    }

                });
            }
        }
        public ICommand GoToZakatyPlayStoreCommand
        {

            get
            {
                return new Command(() =>
                {
                    try
                    {
                        Xamarin.Essentials.Launcher.OpenAsync(PageSettings.ZakatyPlayStoreURl);
                    }
                    catch (Exception ex)
                    {

                    }

                });
            }
        }
        public ICommand GoToZakatyAppstoreCommand
        {

            get
            {
                return new Command(() =>
                {
                    try
                    {
                        Xamarin.Essentials.Launcher.OpenAsync(PageSettings.ZakatyAppStoreURl);
                    }
                    catch (Exception ex)
                    {

                    }

                });
            }
        }

        public AboutZakatyViewModel(INavigationService navigationServices,IDialogService dialogService):base(navigationServices,dialogService)
        {
        }
    }
}

