using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.ViewModel.NewViewModel
{
    public class AddPopPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        #region Property
        private String _popMessage;
        public String PopMessage
        {
            get
            {
                return _popMessage;
            }
            set
            {
                _popMessage = value;
                RaisePropertyChanged("PopMessage");
            }
        }

        private bool _isVisibleLink;
        public bool IsVisibleLink
        {
            get
            {
                return _isVisibleLink;
            }
            set
            {
                _isVisibleLink = value;
                RaisePropertyChanged("IsVisibleLink");
            }
        }

        private string _link;
        public string Link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
                RaisePropertyChanged("Link");
            }
        }

        private string _linkMessage;
        public string LinkMessage
        {
            get
            {
                return _linkMessage;
            }
            set
            {
                _linkMessage = value;
                RaisePropertyChanged("LinkMessage");
            }
        }
        #endregion

        #region Constructor
        public AddPopPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
        #endregion

        #region Method
        #endregion
    }
}
