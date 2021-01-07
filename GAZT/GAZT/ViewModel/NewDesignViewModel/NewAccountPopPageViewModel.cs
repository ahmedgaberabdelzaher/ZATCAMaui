using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class NewAccountPopPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public static string ValidTypeIban;

        public NewAccountPopPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

        #region Properties
        private string _ibanNumberText;
        public string IbanNumberText
        {
            get
            {
                return _ibanNumberText;
            }
            set
            {
                if (_ibanNumberText == value) return;
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
                if (_isIBANValid == value) return;

                _isIBANValid = value;
                RaisePropertyChanged("IsIBANValid");
            }
        }

        private string _ibanPartOne=string.Empty;
        public string IbanPartOne
        {
            get
            {
                return _ibanPartOne;
            }
            set
            {
                if (_ibanPartOne == value) return;

                _ibanPartOne = value;
                RaisePropertyChanged("IbanPartOne");
            }
        }

        private string _ibanPartTwo = string.Empty;
        public string IbanPartTwo
        {
            get
            {
                return _ibanPartTwo;
            }
            set
            {
                if (_ibanPartTwo == value) return;

                _ibanPartTwo = value;
                RaisePropertyChanged("IbanPartTwo");
            }
        }

        private string _ibanPartThree = string.Empty;
        public string IbanPartThree
        {
            get
            {
                return _ibanPartThree;
            }
            set
            {
                if (_ibanPartThree == value) return;

                _ibanPartThree = value;
                RaisePropertyChanged("IbanPartThree");
            }
        }

        private string _ibanPartFour = string.Empty;
        public string IbanPartFour
        {
            get
            {
                return _ibanPartFour;
            }
            set
            {
                if (_ibanPartFour == value) return;

                _ibanPartFour = value;
                RaisePropertyChanged("IbanPartFour");
            }
        }

        private string _ibanPartFive = string.Empty;
        public string IbanPartFive
        {
            get
            {
                return _ibanPartFive;
            }
            set
            {
                if (_ibanPartFive == value) return;

                _ibanPartFive = value;
                RaisePropertyChanged("IbanPartFive");
            }
        }

        private string _accountText;
        public string AccountText
        {
            get
            {
                return _accountText;
            }
            set
            {
                if (_accountText == value) return;

                _accountText = value;
                RaisePropertyChanged("AccountText");
            }
        }
        #endregion

    }
}
