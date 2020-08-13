using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models.VATRefunds;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VATRefunds
{

    public class VATRefundListPageViewModel:BaseViewModel
    {
        #region Commands

        public ICommand GoBackBtnTapped { get; set; }
        public ICommand CloseBtnTapped { get; set; }

        #endregion

        public ObservableCollection<VATRefundsModel> _vatRefundsModel { get; set; }
        public ObservableCollection<VATRefundsModel> VATRefundsModel
        {
            get
            {
                return _vatRefundsModel;
            }

            set
            {

                _vatRefundsModel = value;
                RaisePropertyChanged("VATRefundsModel");
            }
        }

        public ObservableCollection<VATRefundsReturnsModel> VATRefundsReturnsModel { get; set; }
        public ObservableCollection<VATRefundsReturnsModel> _vatRefundsReturnsModel
        {
            get
            {
                return _vatRefundsReturnsModel;
            }

            set
            {

                _vatRefundsReturnsModel = value;
                RaisePropertyChanged("VATRefundsReturnsModel");
            }
        }

        public VATRefundListPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            GoBackBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            PopulateVATRefundsList();

            //GoBackBtnTapped = new Command(this.GoBackBtnClicked);
            //ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);
            //OutletContinueBtnTapped = new Command(this.OutletContinueBtnClicked);
            //AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            //DeclarationContinueBtnTapped = new Command(this.DeclarationContinueBtnClicked);
            //SummaryContinueBtnTapped = new Command(this.SummaryContinueBtnClicked);
            //OnTinRegisrtationReasonDateTapped = new Command(this.OnTinRegisrtationReasonDateClicked);
            //OnTinRegistrationReasonTapped = new Command(this.OnTinRegisrtationReasonClicked);
            //TinDeregistrationModel = new TINDeregistrationModel();
            //SelectedOutletOption = new TINDeregistrationModel();

            //AddOutletDecisionOptions();
            //PopulateAttachmentsListViewTemplate();
            //PopulateSummaryReasonData();
            //PopulateSummaryDeclarationData();
            //EnableReasonView();
        }

        public void PopulateVATRefundsList()
        {
            VATRefundsReturnsModel = new ObservableCollection<VATRefundsReturnsModel>();
            VATRefundsReturnsModel.Add(new VATRefundsReturnsModel
            {
                ReturnPeriod = "01 Jan 1995",
                CreditBalance = "200,000.00 SAR",
                ReassessedBalance = "100,000.00 SAR",
                Offset = "200,000.00 SAR",
                NetCreditBalance = "100,000.00 SAR",
                Status = "Transferred",
                LastStatusChange = "15 Feb 2019"
            });
            VATRefundsReturnsModel.Add(new VATRefundsReturnsModel
            {
                ReturnPeriod = "01 Jan 1995",
                CreditBalance = "200,000.00 SAR",
                ReassessedBalance = "100,000.00 SAR",
                Offset = "200,000.00 SAR",
                NetCreditBalance = "100,000.00 SAR",
                Status = "Transferred",
                LastStatusChange = "15 Feb 2019"
            });

            VATRefundsModel = new ObservableCollection<VATRefundsModel>();
            VATRefundsModel.Add(new VATRefundsModel
            {
                Title = AppResources.ZZVAT,
                RefernceNumber = "1234567890",
                Status = AppResources.VATRefundsStatusRefunded,
                ReassesmentAmount = "1,000 SAR",
                TotalOffset = "500.00 SAR",
                NetCreditBalance = "2,000 SAR",
                RequestDate = "01 Jan 1995",
                BankDetails = new VATRefundsBankDetailsModel
                {
                    IDType = AppResources.NationaID,
                    IDNumber = "Q1234567",
                    IBAN = "SA03 80000 12345",
                    BankName =  "Al Rajhi Bank"
                },
                VATReturns = VATRefundsReturnsModel
            });
            VATRefundsModel.Add(new VATRefundsModel
            {
                Title = AppResources.ZZVAT,
                RefernceNumber = "1234567890",
                Status = AppResources.VATRefundsStatusInProcess,
                ReassesmentAmount = "1,000 SAR",
                TotalOffset = "500.00 SAR",
                NetCreditBalance = "2,000 SAR",
                RequestDate = "01 Jan 1995",
                BankDetails = new VATRefundsBankDetailsModel
                {
                    IDType = AppResources.NationaID,
                    IDNumber = "Q1234567",
                    IBAN = "SA03 80000 12345",
                    BankName = "Al Rajhi Bank"
                },
                VATReturns = VATRefundsReturnsModel
            });
            VATRefundsModel.Add(new VATRefundsModel
            {
                Title = AppResources.ZZVAT,
                RefernceNumber = "1234567890",
                Status = AppResources.VATRefundsStatusRefunded,
                ReassesmentAmount = "1,000 SAR",
                TotalOffset = "500.00 SAR",
                NetCreditBalance = "2,000 SAR",
                RequestDate = "01 Jan 1995",
                BankDetails = new VATRefundsBankDetailsModel
                {
                    IDType = AppResources.NationaID,
                    IDNumber = "Q1234567",
                    IBAN = "SA03 80000 12345",
                    BankName = "Al Rajhi Bank"
                },
                VATReturns = VATRefundsReturnsModel
            });
        }

        public void PopulateRefundAmounts()
        {
            
        }
    }
}
