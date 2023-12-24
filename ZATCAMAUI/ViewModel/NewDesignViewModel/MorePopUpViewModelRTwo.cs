using GalaSoft.MvvmLight.Views;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class MorePopUpViewModelRTwo : BaseViewModel
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
                _vatReturnUIButtons = value;
                RaisePropertyChanged("VatReturnUIButtons");
            }
        }


        #endregion
        public MorePopUpViewModelRTwo(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}
