using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    public class TaxpayerProfileSuccessViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        #endregion

        private string _SuccessTitleLbl;
        public string SuccessTitleLbl
        {
            get { return _SuccessTitleLbl; }
            set
            {
                _SuccessTitleLbl = value;
                RaisePropertyChanged("SuccessTitleLbl");
            }
        }

        private string _ButtonLabelText=AppResources.TPGoToProfile;
        public string ButtonLabelText
        {
            get { return _ButtonLabelText; }
            set
            {
                _ButtonLabelText = value;
                RaisePropertyChanged("ButtonLabelText");
            }
        }

        private string _successCaptionLbl;
        public string successCaptionLbl
        {
            get { return _successCaptionLbl; }
            set
            {
                _successCaptionLbl = value;
                RaisePropertyChanged("successCaptionLbl");
            }
        }

        public TaxpayerProfileSuccessViewModel(INavigationService navigationService)
        {
            if (navigationService == null) { throw new ArgumentNullException("navigationService"); }
            _navigationService = navigationService;
        }
    }
}