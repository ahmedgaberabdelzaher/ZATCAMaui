using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.ViewModel.NewDesignViewModel.ZakatRejectionPopUpViewModel
{
    public class ZakatRejectionReasonPopupViewModel : BaseViewModel
    {
        public string _rejectReasonText = string.Empty;
        public string RejectReasonText
        {
            get
            {
                return _rejectReasonText;
            }
            set
            {
                if (_rejectReasonText == value) return;

                _rejectReasonText = value;
                RaisePropertyChanged("RejectReasonText");
            }
        }

        public ZakatRejectionReasonPopupViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
    }
}
