using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using RGPopup.Maui.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.VATRefunds;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds
{
    public class VATRefundsInstructionsPageViewModel : BaseViewModel
    {
        #region Variable
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
                if (_vatRefundsModel == value) return;
                _vatRefundsModel = value;
                RaisePropertyChanged("VATRefundsModel");
            }
        }

        public bool _isInstructionsChecked = false;
        public bool IsInstructionsChecked
        {
            get
            {
                return _isInstructionsChecked;
            }

            set
            {
                if (_isInstructionsChecked == value) return;

                _isInstructionsChecked = value;
                RaisePropertyChanged("IsInstructionsChecked");
            }
        }

        public bool _isInstructionsVisible;
        public bool IsInstructionsVisible
        {
            get
            {
                return _isInstructionsVisible;
            }

            set
            {
                if (_isInstructionsVisible == value) return;

                _isInstructionsVisible = value;
                RaisePropertyChanged("IsInstructionsVisible");
            }
        }

        private VatRefundDisplayDataModel _vatRefundsDisplayDataModel = null;
        public VatRefundDisplayDataModel VatRefundsDisplayDataModel
        {
            get
            {
                return _vatRefundsDisplayDataModel;
            }

            set
            {
                if (_vatRefundsDisplayDataModel == value) return;

                _vatRefundsDisplayDataModel = value;
                RaisePropertyChanged("VatRefundsDisplayDataModel");
            }
        }

        public VATRefundsInstructionsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            IsInstructionsChecked = false;
            VATRefundInstructionsConfirmedBtnClicked = new Command(VATRefundInstructionsConfirmedBtnTapped);
        }

        public async void VATRefundInstructionsConfirmedBtnTapped()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
                MessagingCenter.Send<object, string>(this, "InstructionsConfirmed", "NavigateToNewRequestPageView");
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

        public async Task ReloadData()
        {
            try
            {
                App.DisplayProgressView();
                VatRefundsDisplayDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData("");
                App.HideProgressView();
                IsInstructionsVisible = true;


            }
            catch (InternetException ex)
            {


                App.HideProgressView();

                try
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsInstructionsVisible = false;

                        await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                        await PopupNavigation.Instance.PopAsync();
                    });

                }
                catch (Exception)
                {
                }
            }
            catch (GAZTErrorException ex)
            {
                App.HideProgressView();

                string message = ex.Message;

                try
                {
                    IsInstructionsVisible = false;

                    PopUp popUp = new PopUp();
                    StringBuilder PopMsg = new StringBuilder();

                    popUp.Message = message;
                    popUp.HeaderText = AppResources.Information;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }

                    await _dialogService.ShowMessage(message, AppResources.Information);
                    await PopupNavigation.Instance.PopAsync();
                }
                catch (Exception)
                {
                }
            }
            catch (Exception)
            {


                try
                {
                    App.HideProgressView();

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsInstructionsVisible = false;
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PopAsync();
                    });

                }
                catch (Exception)
                {
                }
            }
        }
    }
}
