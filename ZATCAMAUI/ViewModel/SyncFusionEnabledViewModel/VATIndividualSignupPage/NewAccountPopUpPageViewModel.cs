

using Mopups.Services;
using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{

    public class NewAccountPopUpPageViewModel : BaseViewModel
    {
        public static string ValidTypeIban;

        public ICommand GoButtonClick { get; set; }

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
                OnPropertyChanged("IbanNumberText");
            }
        }

        private bool _CloseButtonVisible = false;
        public bool CloseButtonVisible
        {
            get
            {
                return _CloseButtonVisible;
            }
            set
            {
                _CloseButtonVisible = value;
                OnPropertyChanged("CloseButtonVisible");
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
                OnPropertyChanged("IsIBANValid");
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
                OnPropertyChanged("IbanPartOne");
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
                OnPropertyChanged("IbanPartTwo");
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
                OnPropertyChanged("IbanPartThree");
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
                OnPropertyChanged("IbanPartFour");
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
                _accountText = value;
                OnPropertyChanged("AccountText");
            }
        }




        #endregion

        public NewAccountPopUpPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoButtonClick = new Command(() =>
            {
                MopupService.Instance.PopAsync();
            });
        }
    }
}
