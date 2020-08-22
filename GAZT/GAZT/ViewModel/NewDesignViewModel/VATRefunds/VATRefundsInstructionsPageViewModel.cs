using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models.VATRefunds;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VATRefunds
{
    public class VATRefundsInstructionsPageViewModel: ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
        #endregion

        public ICommand VATRefundInstructionsConfirmedBtnClicked { get; set; }

        public ObservableCollection<VATRefundsModel> _vatRefundsModel { get; set; }
        public ObservableCollection<VATRefundsModel> VATRefundsModel
        {
            get
            {
                return _vatRefundsModel;
            }

            set
            {

                _vatRefundsModel = value;
                RaisePropertyChanged("VATRefundsModel");
            }
        }

        public VATRefundsInstructionsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            VATRefundInstructionsConfirmedBtnClicked = new Command(this.VATRefundInstructionsConfirmedBtnTapped);
        }

        public async void VATRefundInstructionsConfirmedBtnTapped()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
                MessagingCenter.Send<Object, string>(this, "InstructionsConfirmed", "NavigateToNewRequestPageView");
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
    }
}
