using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel
{

    public class VATInstalmentPlanBottomPopupViewModel : BaseViewModel
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
        #endregion

        public ICommand ZakatInstalationClicked { get; set; }

        public VATInstalmentPlanBottomPopupViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            ZakatInstalationClicked = new Command(ZakatInstalationTapped);
        }

        public async void ZakatInstalationTapped()
        {
            try
            {
                await MopupService.Instance.PopAsync();
                // _navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);
            }
            catch (GAZTUnlockAccountException)
            {

            }
            catch (InternetException ex)
            {
               MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
    }

}
