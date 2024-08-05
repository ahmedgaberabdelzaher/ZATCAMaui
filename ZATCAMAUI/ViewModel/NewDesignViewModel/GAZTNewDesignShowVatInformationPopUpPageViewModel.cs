
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class GAZTNewDesignShowVatInformationPopUpPageViewModel : BaseViewModel
    {
        #region Constructor
        public GAZTNewDesignShowVatInformationPopUpPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {


        }
        #endregion

        #region Property
        private List<HeaderWithInfo> _headerWithInfoList;
        public List<HeaderWithInfo> HeaderWithInfoList
        {
            get
            {
                return _headerWithInfoList;
            }
            set
            {
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
                _SecondLinkText = value;
                OnPropertyChanged("SecondLinkText");
            }
        }



        #endregion
    }
}
