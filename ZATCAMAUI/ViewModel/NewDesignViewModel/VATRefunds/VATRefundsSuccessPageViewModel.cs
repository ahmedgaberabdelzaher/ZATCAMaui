using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models.VATRefunds;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds
{
   
    public class VATRefundsSuccessPageViewModel : BaseViewModel
    {
        #region Variable
        public ICommand GoBackClick { get; set; }
        public ICommand GoToDashboard_Tapped { get; set; }

        private VatRefundDisplayDataModel _vatNewReqSummaryData { get; set; }
        public VatRefundDisplayDataModel VatNewReqSummaryData
        {
            get
            {
                return _vatNewReqSummaryData;
            }

            set
            {
                if (_vatNewReqSummaryData == value) return;
                _vatNewReqSummaryData = value;
                OnPropertyChanged("VatNewReqSummaryData");
            }
        }

        #endregion

        public VATRefundsSuccessPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            VatNewReqSummaryData = new VatRefundDisplayDataModel();
        }
    }
}
