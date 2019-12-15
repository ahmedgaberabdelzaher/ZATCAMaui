using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT
{
    public class ForgotUsernamePasswordViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        //  public ICommand OnLoginButtonClicked { get; set; }
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

        private List<ForgotCredentialType> _selectedForgotType;
        public List<ForgotCredentialType> SelectedForgotType
        {
            get
            {
                return _selectedForgotType;
            }
            set
            {
                _selectedForgotType = value;
                RaisePropertyChanged("_selectedForgotType");


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
        }
        #endregion Constructor
        #region Method
        public void OnPageLoad()
        {
            try
            {
            List<ForgotUserNamePassword> list = new List<ForgotUserNamePassword>
            {
                new ForgotUserNamePassword{ id = "1" , TaxPayerType = "Individual/Personal Business"},
                new ForgotUserNamePassword{ id = "2" , TaxPayerType = "Corporate"}

            };
                TaxpayerTypeList = list;

                List<ForgotCredentialType> forgotCredentialListlist = new List<ForgotCredentialType>
            {
                new ForgotCredentialType{ id = "1" , CredentialType = "Forgot Usernmae"},
                new ForgotCredentialType{ id = "2" , CredentialType = "Forgot Password"}

            };
                ForgotTypeList = forgotCredentialListlist;
            }
            catch(Exception ex)
            {

            }


        }
        #endregion
    }
}
