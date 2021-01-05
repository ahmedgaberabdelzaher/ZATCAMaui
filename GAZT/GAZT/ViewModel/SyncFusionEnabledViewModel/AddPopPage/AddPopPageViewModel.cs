using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.AddPopPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class AddPopPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
       public ICommand onLinkClicked { get; set; }
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
        private String _headerText;
        public String HeaderText
        {
            get
            {
                return _headerText;
            }
            set
            {
                _headerText = value;
                RaisePropertyChanged("HeaderText");
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
        private string _iSBold = "Bold";
        public string IsBold
        {
            get
            {
                return _iSBold;
            }
            set
            {
                _iSBold = value;
                RaisePropertyChanged("IsBold");
            }
        }
        private string _iSRed = "#7D858D";
        public string IsRed
        {
            get
            {
                return _iSRed;
            }
            set
            {
                _iSRed = value;
                RaisePropertyChanged("IsRed");
            }
        }
        private string _flowDirections = "RightToLeft";
        public string FlowDirections
        {
            get
            {
                return _flowDirections;
            }
            set
            {
                _flowDirections = value;
                RaisePropertyChanged("FlowDirections");
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
            onLinkClicked = new Xamarin.Forms.Command(async () =>
            {
                Device.OpenUri(new Uri(Link));
            });
        }
        #endregion
        #region Method
        #endregion
    }
}
