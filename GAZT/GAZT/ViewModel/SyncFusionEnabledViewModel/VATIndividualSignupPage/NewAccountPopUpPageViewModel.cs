using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    public class NewAccountPopUpPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #region Property
        private string _ibanNumberText;
        public string IbanNumberText
        {
            get
            {
                return _ibanNumberText;
            }
            set
            {
                _ibanNumberText = value;
                RaisePropertyChanged("IbanNumberText");
            }
        }

        private bool _isIBANValid;
        public bool IsIBANValid
        {
            get
            {
                return _isIBANValid;
            }
            set
            {
                _isIBANValid = value;
                RaisePropertyChanged("IsIBANValid");
            }
        }

        private string _ibanPartOne;
        public string IbanPartOne
        {
            get
            {
                return _ibanPartOne;
            }
            set
            {
                _ibanPartOne = value;
                RaisePropertyChanged("IbanPartOne");
            }
        }

        private string _ibanPartTwo;
        public string IbanPartTwo
        {
            get
            {
                return _ibanPartTwo;
            }
            set
            {
                _ibanPartTwo = value;
                RaisePropertyChanged("IbanPartTwo");
            }
        }

        private string _ibanPartThree;
        public string IbanPartThree
        {
            get
            {
                return _ibanPartThree;
            }
            set
            {
                _ibanPartThree = value;
                RaisePropertyChanged("IbanPartThree");
            }
        }

        private string _ibanPartFour;
        public string IbanPartFour
        {
            get
            {
                return _ibanPartFour;
            }
            set
            {
                _ibanPartFour = value;
                RaisePropertyChanged("IbanPartFour");
            }
        }

        private string _ibanPartFive;
        public string IbanPartFive
        {
            get
            {
                return _ibanPartFive;
            }
            set
            {
                _ibanPartFive = value;
                RaisePropertyChanged("IbanPartFive");
            }
        }



        #endregion

        public NewAccountPopUpPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
        }
    }
}
