using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class ShowVatInformationConfirmationPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
 
        public ShowVatInformationConfirmationPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
                _SecondLinkText = value;
                RaisePropertyChanged("SecondLinkText");
            }
        }


        #endregion

    }
}
