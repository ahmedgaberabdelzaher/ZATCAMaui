using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models.VATRefunds;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VATRefunds
{
    public class VATRefundDetailsPageViewModel: ViewModelBase
    {
        #region Commands

        public ICommand GoBackBtnTapped { get; set; }
        public ICommand CloseBtnTapped { get; set; }
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #endregion

        public VATRefundsModel _vatRefundsModel { get; set; }
        public VATRefundsModel VATRefundsModel
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

        private ObservableCollection<VATRefundsReturnsModel> _vatReturns { get; set; }
        public ObservableCollection<VATRefundsReturnsModel> VATReturns
        {
            get
            {
                return _vatReturns;
            }

            set
            {

                _vatReturns = value;
                RaisePropertyChanged("VATReturns");
            }
        }

        public string _ReassesmentAmount { get; set; }
        public string ReassesmentAmount
        {
            get
            {
                return ReassesmentAmount;
            }

            set
            {

                ReassesmentAmount = value;
                RaisePropertyChanged("ReassesmentAmount");
            }
        }

        public string _totalAmount { get; set; }
        public string TotalAmount
        {
            get
            {
                return _totalAmount;
            }

            set
            {

                _totalAmount = value;
                RaisePropertyChanged("TotalAmount");
            }
        }


        public string _netCreditBalance { get; set; }
        public string NCB
        {
            get
            {
                return _netCreditBalance;
            }

            set
            {

                _netCreditBalance = value;
                RaisePropertyChanged("NCB");
            }
        }

        public string _bankName { get; set; }
        public string BankName
        {
            get
            {
                return _bankName;
            }

            set
            {

                _bankName = value;
                RaisePropertyChanged("BankName");
            }
        }

        public string _idNumber { get; set; }
        public string IdNumber
        {
            get
            {
                return _idNumber;
            }

            set
            {

                _idNumber = value;
                RaisePropertyChanged("IdNumber");
            }
        }

        public string _idType { get; set; }
        public string IdType
        {
            get
            {
                return _idType;
            }

            set
            {

                _idType = value;
                RaisePropertyChanged("IdType");
            }
        }

        public string _iban { get; set; }
        public string Iban
        {
            get
            {
                return _iban;
            }

            set
            {

                _iban = value;
                RaisePropertyChanged("Iban");
            }
        }

        public VATRefundDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            GoBackBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });

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

        public void ReloadData(VATRefundsModel vATRefundsModel)
        {
            VATRefundsModel = new VATRefundsModel();
            VATRefundsModel = vATRefundsModel;
            VATReturns = new ObservableCollection<VATRefundsReturnsModel>();
            VATReturns = vATRefundsModel.VATReturns;
        }
    }
}
