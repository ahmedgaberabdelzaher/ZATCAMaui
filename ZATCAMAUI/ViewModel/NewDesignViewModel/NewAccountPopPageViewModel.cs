

using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class NewAccountPopPageViewModel : BaseViewModel
    {
        public static string ValidTypeIban;

        public NewAccountPopPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
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
                OnPropertyChanged("IbanNumberText");
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
                OnPropertyChanged("IsIBANValid");
            }
        }

        private string _ibanPartOne = string.Empty;
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
                OnPropertyChanged("IbanPartOne");
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
                OnPropertyChanged("IbanPartTwo");
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
                OnPropertyChanged("IbanPartThree");
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
                OnPropertyChanged("IbanPartFour");
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
                OnPropertyChanged("IbanPartFive");
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
                OnPropertyChanged("AccountText");
            }
        }
        #endregion

    }
}
