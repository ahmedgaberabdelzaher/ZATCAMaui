using System;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VATRefunds
{
    public class VATRefundsSuccessPageViewModel:ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
        public ICommand GoToDashboard_Tapped { get; set; }

        #endregion

        public VATRefundsSuccessPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }

            _navigationService = navigationService;

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            _dialogService = dialogService;

            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            GoToDashboard_Tapped = new Command(this.GoToDashboard_Clicked);
        }

        public void GoToDashboard_Clicked()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var _navigation = Application.Current.MainPage.Navigation;
                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.GAZTNewDesignDashBoardPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }
                _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                _navigation.NavigationStack.ToList().Clear();
                //var _navigation = Application.Current.MainPage.Navigation;
                //_navigation.PopToRootAsync();
            });
        }
    }
}
