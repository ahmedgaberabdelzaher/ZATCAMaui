
using Foundation;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatRejectionPopUpViewModel
{
    [Preserve(AllMembers = true)]
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
                OnPropertyChanged("RejectReasonText");
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
