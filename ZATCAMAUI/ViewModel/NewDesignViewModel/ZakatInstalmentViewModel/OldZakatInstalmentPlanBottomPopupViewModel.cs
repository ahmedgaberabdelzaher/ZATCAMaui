using System.Windows.Input;


using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel
{
    public class OldZakatInstalmentPlanBottomPopupViewModel : BaseViewModel
    {
        #region Variable
        public ICommand GoBackClick { get; set; }
        #endregion

        public ICommand ZakatInstalationClicked { get; set; }

        public OldZakatInstalmentPlanBottomPopupViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackClick = new Command( () =>
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
            }
            catch (GAZTUnlockAccountException ex)
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
