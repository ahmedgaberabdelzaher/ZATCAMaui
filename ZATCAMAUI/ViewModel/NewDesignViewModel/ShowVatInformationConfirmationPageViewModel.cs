using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class ShowVatInformationConfirmationPageViewModel : BaseViewModel
    {

        public ShowVatInformationConfirmationPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
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
                RaisePropertyChanged("HeaderWithInfoList");
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
                RaisePropertyChanged("MainString");
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
                RaisePropertyChanged("NewDesignPopUp");
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
                RaisePropertyChanged("FirstLink");
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
                RaisePropertyChanged("FirstLinkText");
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
                RaisePropertyChanged("SecondLink");
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
                RaisePropertyChanged("SecondLinkText");
            }
        }
        #endregion

    }
}
