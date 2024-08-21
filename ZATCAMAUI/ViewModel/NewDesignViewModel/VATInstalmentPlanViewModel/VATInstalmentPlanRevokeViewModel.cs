using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models.VATInstalmentModels;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel
{
    public class VATInstalmentPlanRevokeViewModel : BaseViewModel
    {

        private ReqVatInstalmentPlanResponse rEQVatInstalmentPlanResponse { get; set; }
        public ReqVatInstalmentPlanResponse REQVatInstalmentPlanResponse
        {
            get
            {
                return rEQVatInstalmentPlanResponse;
            }
            set
            {
                if (rEQVatInstalmentPlanResponse == value) return;
                rEQVatInstalmentPlanResponse = value;
                OnPropertyChanged("REQVatInstalmentPlanResponse");
            }
        }

        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public VATInstalmentPlanRevokeViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService,dialogService)
        {
            IsArabic = App.IsArabic;

            CloseClick = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });

            GoBackClick = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });
        }
        private bool _isArabic = false;
        public bool IsArabic
        {
            get
            {
                return _isArabic;
            }
            set
            {
                if (_isArabic == value) return;
                _isArabic = value;
                OnPropertyChanged("IsArabic");
            }
        }
        private ObservableCollection<VATRevokeUiListModel> vATInstalmentRevokeList = new ObservableCollection<VATRevokeUiListModel>();
        public ObservableCollection<VATRevokeUiListModel> VATInstalmentRevokeList
        {
            get
            {
                return vATInstalmentRevokeList;
            }
            set
            {
                if (vATInstalmentRevokeList == value)
                {
                    return;
                }
                vATInstalmentRevokeList = value;
                OnPropertyChanged("VATInstalmentRevokeList");
            }
        }

        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }

        public void BindVatInstalments()
        {
            if (rEQVatInstalmentPlanResponse.d.RevokeListSet != null)
            {
                foreach (var revokeInstalmentListModel in rEQVatInstalmentPlanResponse.d.RevokeListSet)
                {

                    VATInstalmentRevokeList.Add(new VATRevokeUiListModel()
                    {
                        Fbnum = revokeInstalmentListModel.Fbnum,
                        DpAmt = revokeInstalmentListModel.DpAmt,
                        SubmitDt = revokeInstalmentListModel.SubmitDt,
                        DueAmt = revokeInstalmentListModel.DueAmt,
                        PlanDur = revokeInstalmentListModel.PlanDur,
                        TotAmt = revokeInstalmentListModel.TotAmt,

                    });
                }

            }

        }

        public async Task GetVATInstalmentPlanList()
        {
            try
            {
                IsLoading = true;
                await Task.Run(async () =>
                {

                    IsLoading = true;
                    try
                    {

                        rEQVatInstalmentPlanResponse = await VATInstalationPlanWebServiceManager.GetRequestToVATInstalmentData();

                        PopToRootPage();


                        if (rEQVatInstalmentPlanResponse != null && rEQVatInstalmentPlanResponse.d != null)
                        {
                            BindVatInstalments();
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }



                        IsLoading = false;
                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });

                    }
                });
                IsLoading = false;

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
    }
}

