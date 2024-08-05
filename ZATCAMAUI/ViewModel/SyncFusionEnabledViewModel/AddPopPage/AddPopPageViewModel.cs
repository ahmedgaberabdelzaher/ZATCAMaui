

using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AddPopPage
{

    public class AddPopPageViewModel : BaseViewModel
    {
        #region Variable
        public ICommand onLinkClicked { get; set; }
        #endregion
        #region Property
        private string _popMessage;
        public string PopMessage
        {
            get
            {
                return _popMessage;
            }
            set
            {
                _popMessage = value;
                OnPropertyChanged("PopMessage");
            }
        }
        private string _headerText;
        public string HeaderText
        {
            get
            {
                return _headerText;
            }
            set
            {
                _headerText = value;
                OnPropertyChanged("HeaderText");
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
                OnPropertyChanged("IsVisibleLink");
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
                OnPropertyChanged("Link");
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
                OnPropertyChanged("LinkMessage");
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
                OnPropertyChanged("IsBold");
            }
        }
        private string _iSRed = "{StaticResource ForgotPasswordGrayTextColor}";
        public string IsRed
        {
            get
            {
                return _iSRed;
            }
            set
            {
                _iSRed = value;
                OnPropertyChanged("IsRed");
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
                OnPropertyChanged("FlowDirections");
            }
        }
        #endregion
        #region Constructor
        public AddPopPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            onLinkClicked = new Command( async() =>
            {
                await Browser.Default.OpenAsync(new Uri(Link));
            });
        }
        #endregion
    }
}
