


using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{

    public class TaxpayerProfileSuccessViewModel : BaseViewModel
    {
        #region Variable
        public int TPProfileSuccessId;
        #endregion

        private string _SuccessTitleLbl;
        public string SuccessTitleLbl
        {
            get { return _SuccessTitleLbl; }
            set
            {
                _SuccessTitleLbl = value;
                OnPropertyChanged("SuccessTitleLbl");
            }
        }

        private string _ButtonLabelText = AppResources.TPGoToProfile;
        public string ButtonLabelText
        {
            get { return _ButtonLabelText; }
            set
            {
                _ButtonLabelText = value;
                OnPropertyChanged("ButtonLabelText");
            }
        }

        private string _successCaptionLbl;
        public string successCaptionLbl
        {
            get { return _successCaptionLbl; }
            set
            {
                _successCaptionLbl = value;
                OnPropertyChanged("successCaptionLbl");
            }
        }

        public TaxpayerProfileSuccessViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}