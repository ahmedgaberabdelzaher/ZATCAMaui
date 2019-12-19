using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT
{
    public class ForgotUsernamePasswordViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        
       public ICommand OnSubmitClicked { get; set; }
        #endregion
        #region Property
        private ForgotUserNamePassword _selectedTaxPayerType;
        public ForgotUserNamePassword SelectedTaxPayerType
        {
            get
            {
                return _selectedTaxPayerType;
            }
            set
            {
                _selectedTaxPayerType = value;
                RaisePropertyChanged("SelectedTaxPayerType");

                if (SelectedTaxPayerType != null)
                {
                    SetLayoutVisibilityForSelectedTaxpayerType();
                }
            }
        }

        private List<ForgotCredentialType> _forgotTypeList;
        public List<ForgotCredentialType> ForgotTypeList
        {
            get
            {
                return _forgotTypeList;
            }
            set
            {
                _forgotTypeList = value;
                RaisePropertyChanged("ForgotTypeList");


            }
        }

        private ForgotCredentialType _selectedForgotType;
        public ForgotCredentialType SelectedForgotType
        {
            get
            {
                return _selectedForgotType;
            }
            set
            {
                _selectedForgotType = value;
                RaisePropertyChanged("_selectedForgotType");
                if(SelectedForgotType != null)
                {
                    IsTaxPayerTypeEnable = true;
                    SetLayoutVisibilityForSelectedForgotType();
                }
                else
                {
                    IsTaxPayerTypeEnable = false;
                }


            }
        }

        

        private ForgotUserNamePassword _forgotCredentialType;
        public ForgotUserNamePassword ForgotCredentialType
        {
            get
            {
                return _forgotCredentialType;
            }
            set
            {
                _forgotCredentialType = value;
                RaisePropertyChanged("ForgotCredentialType");
            }
        }

        private List<ForgotUserNamePassword>  _taxpayerTypeList;
        public List<ForgotUserNamePassword>  TaxpayerTypeList
        {
            get
            {
                return _taxpayerTypeList;
            }
            set
            {
                _taxpayerTypeList = value;
                RaisePropertyChanged("TaxpayerTypeList");
            }
        }

        private string _iDNumber;
        public string IDNumber
        {
            get
            {
                return _iDNumber;
            }
            set
            {
                _iDNumber = value;
                RaisePropertyChanged("IDNumber");
            }
        }

        private string _enteredCaptchaValue;
        public string EnteredCaptchaValue
        {
            get
            {
                return _enteredCaptchaValue;
            }
            set
            {
                _enteredCaptchaValue = value;
                RaisePropertyChanged("EnteredCaptchaValue");
            }
        }

        private string _corporateID;
        public string CorporateID
        {
            get
            {
                return _corporateID;
            }
            set
            {
                _corporateID = value;
                RaisePropertyChanged("CorporateID");
            }
        }

        
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged("UserName");
            }
        }

        private bool _isForgotUserNameWithIndividual = true;
        public bool IsForgotUserNameWithIndividual
        {
            get
            {
                return _isForgotUserNameWithIndividual;
            }
            set
            {
                _isForgotUserNameWithIndividual = value;
                RaisePropertyChanged("IsForgotUserName");
            }
        }

        private bool _isForgotUserNameWithCorporate = false;
        public bool IsForgotUserNameWithCorporate
        {
            get
            {
                return _isForgotUserNameWithCorporate;
            }
            set
            {
                _isForgotUserNameWithCorporate = value;
                RaisePropertyChanged("IsForgotUserNameWithCorporate");
            }
        }

        private bool _isForgotPassword = false;
        public bool IsForgotPassword
        {
            get
            {
                return _isForgotPassword;
            }
            set
            {
                _isForgotPassword = value;
                RaisePropertyChanged("IsForgotPassword");
            }
        }

        private bool _isTaxPayerTypeEnable = false;
        public bool IsTaxPayerTypeEnable
        {
            get
            {
                return _isTaxPayerTypeEnable;
            }
            set
            {
                _isTaxPayerTypeEnable = value;
                RaisePropertyChanged("IsTaxPayerTypeEnable");
            }
        }

        
        #endregion

        #region Constructor

        public ForgotUsernamePasswordViewModel(INavigationService navigationService, IDialogService dialogService)
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
            OnSubmitClicked = new Command(async () =>
            {
             // _navigationService.NavigateTo(App.ForgotUsernamePassword);
            });
            
        }
        #endregion Constructor
        #region Method
        public void OnPageLoad()
        {
            try
            {
            List<ForgotUserNamePassword> list = new List<ForgotUserNamePassword>
            {
                new ForgotUserNamePassword{ id = "1" , TaxPayerType = AppResources.Individual},
                new ForgotUserNamePassword{ id = "2" , TaxPayerType = AppResources.Company}

            };
                TaxpayerTypeList = list;

                List<ForgotCredentialType> forgotCredentialListlist = new List<ForgotCredentialType>
            {
                new ForgotCredentialType{ id = "1" , CredentialType = AppResources.ForgotUsername},
                new ForgotCredentialType{ id = "2" , CredentialType = AppResources.Password}

            };
                ForgotTypeList = forgotCredentialListlist;
            }
            catch(Exception ex)
            {

            }


        }

        private void SetLayoutVisibilityForSelectedForgotType()
        {
            if (SelectedForgotType.id.Equals("2"))
            {
                //Device.BeginInvokeOnMainThread(() => {

                    IsTaxPayerTypeEnable = false;
                    SelectedTaxPayerType = null;
                    IsForgotPassword = true;
                    IsForgotUserNameWithIndividual = false;
                    IsForgotUserNameWithCorporate = false;
                //});

            }
            else
            {
                IsTaxPayerTypeEnable = true;
                IsForgotPassword = false;
                IsForgotUserNameWithIndividual = true;
                IsForgotUserNameWithCorporate = false;
            }

        }

        private void SetLayoutVisibilityForSelectedTaxpayerType()
        {
            if (SelectedTaxPayerType.id.Equals("1"))
            {

                IsForgotUserNameWithIndividual = true;
                IsForgotUserNameWithCorporate = false;
            }
            else
            {
                IsForgotUserNameWithIndividual = false;
                IsForgotUserNameWithCorporate = true;
            }
        }
        #endregion
    }
}
