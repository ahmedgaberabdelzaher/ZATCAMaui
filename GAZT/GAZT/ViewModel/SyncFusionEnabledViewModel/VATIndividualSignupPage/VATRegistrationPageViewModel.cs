using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    public class VATRegistrationPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #region Properties
        private bool _isInstrunctionVisible = false;
        public bool IsInstrunctionVisible
        {
            get
            {
                return _isInstrunctionVisible;
            }
            set
            {
                _isInstrunctionVisible = value;
                RaisePropertyChanged("IsInstrunctionVisible");
            }
        }
        private bool _isTaxPayersVisible = false;
        public bool IsTaxPayersVisible
        {
            get
            {
                return _isTaxPayersVisible;
            }
            set
            {
                _isTaxPayersVisible = value;
                RaisePropertyChanged("IsTaxPayersVisible");
            }
        }
        private bool _isSalesVisible = false;
        public bool IsSalesVisible
        {
            get
            {
                return _isSalesVisible;
            }
            set
            {
                _isSalesVisible = value;
                RaisePropertyChanged("IsSalesVisible");
            }
        }
        private bool _isExpensesVisible = false;
        public bool IsExpensesVisible
        {
            get
            {
                return _isExpensesVisible;
            }
            set
            {
                _isExpensesVisible = value;
                RaisePropertyChanged("IsExpensesVisible");
            }
        }
        private bool _isFinancialVisible = false;
        public bool IsFinancialVisible
        {
            get
            {
                return _isFinancialVisible;
            }
            set
            {
                _isFinancialVisible = value;
                RaisePropertyChanged("IsFinancialVisible");
            }
        }
        private bool _isSummaryVisible = false;
        public bool IsSummaryVisible
        {
            get
            {
                return _isSummaryVisible;
            }
            set
            {
                _isSummaryVisible = value;
                RaisePropertyChanged("IsSummaryVisible");
            }
        }

        private String _currentStep;
        public String CurrentStep
        {
            get
            {
                return _currentStep;
            }
            set
            {
                _currentStep = value;
                RaisePropertyChanged("CurrentStep");
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private VATRegistrationDetails _vATRegistrationDetailsData;
        public VATRegistrationDetails VATRegistrationDetailsData
        {
            get
            {
                return _vATRegistrationDetailsData;
            }
            set
            {
                _vATRegistrationDetailsData = value;
                RaisePropertyChanged("VATRegistrationDetailsData");
            }
        }

        private VATRegistrationOtherDetails _vATRegistrationOtherDetails;
        public VATRegistrationOtherDetails VATRegistrationOtherDetails
        {
            get
            {
                return _vATRegistrationOtherDetails;
            }
            set
            {
                _vATRegistrationOtherDetails = value;
                RaisePropertyChanged("VATRegistrationOtherDetails");
            }
        }



        #endregion

        public VATRegistrationPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

        }

        #region Method

        public void SetVisibility()
        {
            IsInstrunctionVisible = false;
            IsTaxPayersVisible = false;
            IsSalesVisible = false;
            IsExpensesVisible = false;
            IsFinancialVisible = false;
            IsSummaryVisible = false;
        }

        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
        public async Task onPageLoad()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    VATRegistrationDetailsData = null;
                    VATRegistrationOtherDetails = null;
                    VATRegistrationDetails vATRegistration = null;
                    VATRegistrationOtherDetails vATRegistrationOther = null;
                    try
                    {
                        
                        vATRegistration = await WebServiceManager.GAZTGetVATRegistrationData();
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        if (vATRegistration != null && vATRegistration.d != null)
                        {
                            VATRegistrationDetailsData = vATRegistration;
                            vATRegistrationOther = await WebServiceManager.GAZTGetVATRegistrationDataWithButtons(vATRegistration.d.Fbnumz, vATRegistration.d.Officerz, vATRegistration.d.Statusz, vATRegistration.d.TxnTpz, "ZTAX_VT_REG");
                            PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                            if(vATRegistrationOther != null && vATRegistrationOther.d != null)
                            {
                                VATRegistrationOtherDetails = vATRegistrationOther;
                            }
                        }
                       
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                        //   await Task.Run(() =>
                        //   {
                        //  });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
               
            }
            catch (Exception ex)
            {
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
