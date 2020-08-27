using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.ContractRelease;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;
using static EGAZT.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails;

namespace EGAZT.ViewModel.NewDesignViewModel.ContractRelease
{
    public class ContractReleaseListViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand RequestContractReleaseBtnTapped { get; set; }

        public ObservableCollection<ContractReLeaseApplicationFormModel.ContractResult> _contractListViewData { get; set; }

        public ObservableCollection<ContractReLeaseApplicationFormModel.ContractResult> ContractListViewData
        {
            get { return _contractListViewData; }

            set
            {
                if (_contractListViewData == value)
                {
                    return;
                }

                _contractListViewData = value;
                RaisePropertyChanged("ContractListViewData");
            }
        }

        private bool _summaryVisible = false;

        public bool SummaryVisible
        {
            get { return _summaryVisible; }
            set
            {
                _summaryVisible = value;
                RaisePropertyChanged("SummaryVisible");
            }
        }

        private bool _isLoading = false;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private bool _isContractListsVisible = false;

        public bool IsContractListsVisible
        {
            get { return _isContractListsVisible; }
            set
            {
                _isContractListsVisible = value;
                RaisePropertyChanged("IsContractListsVisible");
            }
        }

        private bool _isBackButtonVisible = false;

        public bool IsBackButtonVisible
        {
            get { return _isBackButtonVisible; }
            set
            {
                _isBackButtonVisible = value;
                RaisePropertyChanged("IsBackButtonVisible");
            }
        }

        private ContractReLeaseApplicationFormModel _contractReLeaseApplicationFormModel;
        public ContractReLeaseApplicationFormModel cRApplicationFormData
        {
            get
            {
                return _contractReLeaseApplicationFormModel;
            }
            set
            {
                _contractReLeaseApplicationFormModel = value;
                RaisePropertyChanged("cRApplicationFormData");
            }
        }


        private ContractReLeaseApplicationFormModel.ListSet _contractReLeaseListSet;
        public ContractReLeaseApplicationFormModel.ListSet ContractReLeaseListSet
        {
            get
            {
                return _contractReLeaseListSet;
            }
            set
            {
                _contractReLeaseListSet = value;
                RaisePropertyChanged("ContractReLeaseListSet");
            }
        }

        public ContractReleaseListViewModel(INavigationService navigationService, IDialogService dialogService) : base(
            navigationService, dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            CloseClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });
            GoBackClick = new Command(async () =>
            {
                EnableContractListView();
            });
            RequestContractReleaseBtnTapped = new Command(async () =>
            {
                _navigationService.NavigateTo(App.ContractReleasePageView);
            });
        }

        private void EnableContractListView()
        {
            IsBackButtonVisible = false;
            IsContractListsVisible = true;
            SummaryVisible = false;
        }

        private void EnableSummaryView()
        {
            IsBackButtonVisible = true;
            IsContractListsVisible = false;
            SummaryVisible = false;
        }

        private void PopulateContractList()
        {
            var contracts = new ObservableCollection<ContractReLeaseApplicationFormModel.ContractResult>();

            foreach (var contract in ContractReLeaseListSet.results)
            {
                contracts.Add(contract);
            }

            ContractListViewData = contracts;
        }

        public void ResetData()
        {
            EnableContractListView();
        }

        public void UpdateDataToUI()
        {
            PopulateContractList();
            // ContractReLeaseListSet.results[0]
        }

        #region OnPageLoad
        public async Task OnPageLoad()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    cRApplicationFormData = null;
                    ContractReLeaseListSet = null;


                    try
                    {
                        cRApplicationFormData = await WebServiceManager.GetContractReleaseList("", "", "", "");

                        //cRApplicationFormData = await WebServiceManager.GAZTGetCRApplicationFormData("", App.LoginDataRetrieved.Euser, 
                        //    App.LoginDataRetrieved.FbGuid, App.LoginDataRetrieved.Euser);
                        if (cRApplicationFormData != null && cRApplicationFormData.d != null)
                        {
                            ContractReLeaseListSet = cRApplicationFormData.d.ListSet;

                            //await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatInstructions, checkBoxString: AppResources.VatInstructionsCheckBoxDesc, continueString: AppResources.VatInstalmetPlanTitle,
                            //_dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
                            //    .Instructions));

                            // BindVATSelectionView();
                            //BindBillsListView();


                            UpdateDataToUI();

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });

                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        #endregion

    }
}
