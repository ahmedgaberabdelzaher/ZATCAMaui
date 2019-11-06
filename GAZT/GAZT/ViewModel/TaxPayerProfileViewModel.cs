using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Windows.Input;
namespace GAZT
{
    public class TaxPayerProfileViewModel : ViewModelBase
    {
        #region Variable
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnChangeMobileNumberClicked { get; set; }
        public ICommand OnChangeEmailClicked { get; set; }
        public ICommand OnChangePasswordClicked { get; set; }
        public ICommand OnSubmitButtonClicked { get; set; }
        public ICommand OnVerifyButtonClicked { get; set; }
        public ICommand OnChangeEmailSubmitButtonClicked { get; set; }
        public ICommand OnChangePasswordButtonClicked { get; set; }
        #endregion

        #region Property
        
        private bool _tPProfileVisibility = true;
        public bool TPProfileVisibility
        {
            get
            {
                return _tPProfileVisibility;
            }
            set
            {
                _tPProfileVisibility = value;
                RaisePropertyChanged("TPProfileVisibility");
            }
        }

        private bool _changeMobileNumberLayoutVisibility = false;
        public bool ChangeMobileNumberLayoutVisibility
        {
            get
            {
                return _changeMobileNumberLayoutVisibility;
            }
            set
            {
                _changeMobileNumberLayoutVisibility = value;
                RaisePropertyChanged("ChangeMobileNumberLayoutVisibility");
            }
        }

        private bool _changeEmailLayoutVisibility = false;
        public bool ChangeEmailLayoutVisibility
        {
            get
            {
                return _changeEmailLayoutVisibility;
            }
            set
            {
                _changeEmailLayoutVisibility = value;
                RaisePropertyChanged("ChangeEmailLayoutVisibility");
            }
        }

        private bool _changePasswordayoutVisibility = false;
        public bool ChangePasswordayoutVisibility
        {
            get
            {
                return _changePasswordayoutVisibility;
            }
            set
            {
                _changePasswordayoutVisibility = value;
                RaisePropertyChanged("ChangePasswordayoutVisibility");
            }
        }

        private TaxPayerProfile _TaxPayerProfile = App.TP;
        public TaxPayerProfile TaxPayerProfile
        {
            get
            {
                return _TaxPayerProfile;
            }
            set
            {
                _TaxPayerProfile = value;
                RaisePropertyChanged("TaxPayerProfile");
            }
        }


        #endregion

        #region Constructor

        public TaxPayerProfileViewModel(INavigationService navigationService, IDialogService dialogService)
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

            OnChangeMobileNumberClicked = new RelayCommand(() =>
            {
               TPProfileVisibility = false;
               ChangeMobileNumberLayoutVisibility = true;
            });

            OnChangeEmailClicked = new RelayCommand(() =>
            {
                TPProfileVisibility = false;
                ChangeEmailLayoutVisibility = true;
            });

            OnChangePasswordClicked = new RelayCommand(() =>
            {
                TPProfileVisibility = false;
                ChangePasswordayoutVisibility = true;
            });

            OnSubmitButtonClicked = new RelayCommand(() =>
            {
                //_navigationService.NavigateTo(App.DashboardView);
                ChangeMobileNumberLayoutVisibility = false;
                TPProfileVisibility = true;

            });

            OnVerifyButtonClicked = new RelayCommand(() =>
            {
                bool IsNavigatingFromLogin = false;
                _navigationService.NavigateTo(App.OTPView, IsNavigatingFromLogin);

            });

            OnChangeEmailSubmitButtonClicked = new RelayCommand(() =>
            {
                ChangeEmailLayoutVisibility = false;
                TPProfileVisibility = true;
                //bool IsNavigatingFromLogin = false;
                //_navigationService.NavigateTo(App.OTPView, IsNavigatingFromLogin);

            });

            OnChangePasswordButtonClicked = new RelayCommand(() =>
            {
                ChangePasswordayoutVisibility = false;
                TPProfileVisibility = true;
                //bool IsNavigatingFromLogin = false;
                //_navigationService.NavigateTo(App.OTPView, IsNavigatingFromLogin);

            });

        }

        #endregion

        #region Method

        public void OnPageLoad()
        {
            TPProfileVisibility = true;
            ChangePasswordayoutVisibility = false;
            ChangeMobileNumberLayoutVisibility = false;
            ChangeEmailLayoutVisibility = false;
        }
        #endregion
    }
}
