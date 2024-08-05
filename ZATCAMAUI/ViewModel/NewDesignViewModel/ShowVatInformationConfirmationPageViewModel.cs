
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class ShowVatInformationConfirmationPageViewModel : BaseViewModel
    {

        public ShowVatInformationConfirmationPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }

        #region Properties
        private List<HeaderWithInfo> _headerWithInfoList;
        public List<HeaderWithInfo> HeaderWithInfoList
        {
            get
            {
                return _headerWithInfoList;
            }
            set
            {
                if (_headerWithInfoList == value) return;
                _headerWithInfoList = value;
                OnPropertyChanged("HeaderWithInfoList");
            }
        }

        private string _mainString;
        public string MainString
        {
            get
            {
                return _mainString;
            }
            set
            {
                if (_mainString == value) return;
                _mainString = value;
                OnPropertyChanged("MainString");
            }
        }

        private NewDesignPopUp _newDesignPopUp;
        public NewDesignPopUp NewDesignPopUp
        {
            get
            {
                return _newDesignPopUp;
            }
            set
            {
                if (_newDesignPopUp == value) return;

                _newDesignPopUp = value;
                OnPropertyChanged("NewDesignPopUp");
            }
        }

        private string _firstLink = string.Empty;
        public string FirstLink
        {
            get
            {
                return _firstLink;
            }
            set
            {
                if (_firstLink == value) return;
                _firstLink = value;
                OnPropertyChanged("FirstLink");
            }
        }

        private string _firstLinkText = string.Empty;
        public string FirstLinkText
        {
            get
            {
                return _firstLinkText;
            }
            set
            {
                if (_firstLinkText == value) return;
                _firstLinkText = value;
                OnPropertyChanged("FirstLinkText");
            }
        }

        private string _SecondLink = string.Empty;
        public string SecondLink
        {
            get
            {
                return _SecondLink;
            }
            set
            {
                if (_SecondLink == value) return;

                _SecondLink = value;
                OnPropertyChanged("SecondLink");
            }
        }

        private string _SecondLinkText = string.Empty;
        public string SecondLinkText
        {
            get
            {
                return _SecondLinkText;
            }
            set
            {
                if (_SecondLinkText == value) return;
                _SecondLinkText = value;
                OnPropertyChanged("SecondLinkText");
            }
        }
        #endregion

    }
}
