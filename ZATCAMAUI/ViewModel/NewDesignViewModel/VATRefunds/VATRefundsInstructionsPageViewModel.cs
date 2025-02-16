using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
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
                OnPropertyChanged("VATRefundsModel");
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
                OnPropertyChanged("IsInstructionsChecked");
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
                OnPropertyChanged("IsInstructionsVisible");
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
                OnPropertyChanged("VatRefundsDisplayDataModel");
            }
        }

        public VATRefundsInstructionsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            

            IsInstructionsChecked = false;
            VATRefundInstructionsConfirmedBtnClicked = new Command(async () => await VATRefundInstructionsConfirmedBtnTapped());
        }

        public async Task VATRefundInstructionsConfirmedBtnTapped()
        {
            try
            {
                await MopupService.Instance.PopAsync();
                MessagingCenter.Send<object, string>(this, "InstructionsConfirmed", "NavigateToNewRequestPageView");
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public async Task ReloadData()
        {
            try
            {
                IsLoading = true;
                VatRefundsDisplayDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData("");

                IsLoading = false;
                IsInstructionsVisible = true;
            }
            catch (GAZTErrorException ex)
            {
                await MopupService.Instance.PopAsync();
                await UtilityManager.HandleExceptionMessage(ex.Message, false, isPopStack:false);
            }
            catch (GAZTNetworkConnectivityIssueException)
            {
                await UtilityManager.HandleExceptionMessage(AppResources.NetworkConnectivityIssue, true, _navigationService);
            }
            catch (InternetException)
            {
                await UtilityManager.HandleExceptionMessage(AppResources.ZZInternetConnectionMessage, false);
            }
            catch (Exception)
            {
                await UtilityManager.HandleExceptionMessage(AppResources.Somethingwentwrong, true, _navigationService);
            }
            finally
            {
                //IsInstructionsVisible = false;
                IsLoading = false;
            }
        }
    }
}
