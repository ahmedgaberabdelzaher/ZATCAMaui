

using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{
    public class MorePopUpPageViewModel : BaseViewModel
    {
        #region Properties
        private List<string> _vatReturnUIButtons;
        public List<string> VatReturnUIButtons
        {
            get
            {
                return _vatReturnUIButtons;
            }
            set
            {
                if (_vatReturnUIButtons == value) return;
                _vatReturnUIButtons = value;
                OnPropertyChanged("VatReturnUIButtons");
            }
        }


        #endregion

        public MorePopUpPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}
