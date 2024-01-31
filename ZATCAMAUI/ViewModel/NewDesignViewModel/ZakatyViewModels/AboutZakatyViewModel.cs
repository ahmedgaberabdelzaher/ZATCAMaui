using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Core.AppConfigurations;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatyViewModels
{
    public class AboutZakatyViewModel : BaseViewModel
    {

        public ICommand GoToZakatyPortalCommand
        {

            get
            {
                return new Command(() =>
                {
                    try
                    {
                        Launcher.OpenAsync(PageSettings.ZakatyPortalURl);
                    }
                    catch (Exception)
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
                        Launcher.OpenAsync(PageSettings.ZakatyPlayStoreURl);
                    }
                    catch (Exception)
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
                       Launcher.OpenAsync(PageSettings.ZakatyAppStoreURl);
                    }
                    catch (Exception)
                    {

                    }

                });
            }
        }

        public AboutZakatyViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
        }
    }
}

