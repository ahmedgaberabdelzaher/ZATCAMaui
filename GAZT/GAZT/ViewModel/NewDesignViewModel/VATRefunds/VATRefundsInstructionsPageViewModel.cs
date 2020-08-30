using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.VATRefunds;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VATRefunds
{
    public class VATRefundsInstructionsPageViewModel: BaseViewModel
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

            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            IsInstructionsChecked = false;
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

        public async Task ReloadData()
        {
            try
            {
                App.DisplayProgressView();

                VatRefundsDisplayDataModel = await WebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData("");

                App.HideProgressView();

                IsInstructionsVisible = true;

               
            }
            catch (InternetException ex)
            {
                App.HideProgressView();

                try
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsInstructionsVisible = false;
                        
                        await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                        await PopupNavigation.Instance.PopAsync();
                    });

                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
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
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    App.HideProgressView();

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsInstructionsVisible = false;
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PopAsync();
                    });

                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
                }
            }
        }
    }
}
