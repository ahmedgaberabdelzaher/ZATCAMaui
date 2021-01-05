using System;
using System.Windows.Input;
using EGAZT.Models.VATRefunds;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.VATRefunds
{
    [Preserve(AllMembers = true)]
    public class VATRefundsSuccessPageViewModel:ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
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
                RaisePropertyChanged("VatNewReqSummaryData");
            }
        }

        #endregion

        public VATRefundsSuccessPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }

            _navigationService = navigationService;

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            _dialogService = dialogService;

            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            VatNewReqSummaryData = new VatRefundDisplayDataModel();
        }
    }
}
