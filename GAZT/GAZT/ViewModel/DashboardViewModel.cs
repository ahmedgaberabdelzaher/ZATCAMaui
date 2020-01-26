using System;
using System;
using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;
using GAZT.Models;
using GAZT.Manager;
using System.Threading.Tasks;
using GAZT.Helper;

namespace GAZT
{
    public class
        DashboardViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnMyCertificateClicked { get; set; }
        public ICommand OnBellClicked { get; set; }
        public ICommand OnMyTaxPayerProfileClicked { get; set; }
        public ICommand OnMyBillsClicked { get; set; }
        public Dashboard dashboard { get; set; }
        #endregion
        #region Property

        private string _totalSubmittedReturn;
        public string TotalSubmittedReturn
        {
            get
            {
                return _totalSubmittedReturn;
            }
            set
            {
                _totalSubmittedReturn = value;
                RaisePropertyChanged("TotalSubmittedReturn");
            }
        }

        private string _totalNonSubmittedReturn;
        public string TotalNonSubmittedReturn
        {
            get
            {
                return _totalNonSubmittedReturn;
            }
            set
            {
                _totalNonSubmittedReturn = value;
                RaisePropertyChanged("TotalNonSubmittedReturn");
            }
        }

        private string _totalPaidReturn;
        public string TotalPaidReturn
        {
            get
            {
                return _totalPaidReturn;
            }
            set
            {
                _totalPaidReturn = value;
                RaisePropertyChanged("TotalPaidReturn");
            }
        }

        private string _totalUnapidReturn;
        public string TotalUnapidReturn
        {
            get
            {
                return _totalUnapidReturn;
            }
            set
            {
                _totalUnapidReturn = value;
                RaisePropertyChanged("TotalUnapidReturn");
            }
        }

        private string _totalPartialPaidReturn;
        public string TotalPartialPaidReturn
        {
            get
            {
                return _totalPartialPaidReturn;
            }
            set
            {
                _totalPartialPaidReturn = value;
                RaisePropertyChanged("TotalPartialPaidReturn");
            }
        }

        private string _totalNoofReturns;
        public string TotalNoofReturns
        {
            get
            {
                return _totalNoofReturns;
            }
            set
            {
                _totalNoofReturns = value;
                RaisePropertyChanged("TotalNoofReturns");
            }
        }

        private string _totalOverDueReturn;
        public string TotalOverDueReturn
        {
            get
            {
                return _totalOverDueReturn;
            }
            set
            {
                _totalOverDueReturn = value;
                RaisePropertyChanged("TotalOverDueReturn");
            }
        }

        private string _totalPaidBills;
        public string TotalPaidBills
        {
            get
            {
                return _totalPaidBills;
            }
            set
            {
                _totalPaidBills = value;
                RaisePropertyChanged("TotalPaidBills");
            }
        }

        private string _totalPaidBillsAmount;
        public string TotalPaidBillsAmount
        {
            get
            {
                return _totalPaidBillsAmount;
            }
            set
            {
                _totalPaidBillsAmount = value;
                RaisePropertyChanged("TotalPaidBillsAmount");
            }
        }


        private string _totalUnpaidBills;
        public string TotalUnpaidBills
        {
            get
            {
                return _totalUnpaidBills;
            }
            set
            {
                _totalUnpaidBills = value;
                RaisePropertyChanged("TotalUnpaidBills");
            }
        }

        private string _totalUnpaidBillsAmount;
        public string TotalUnpaidBillsAmount
        {
            get
            {
                return _totalUnpaidBillsAmount;
            }
            set
            {
                _totalUnpaidBillsAmount = value;
                RaisePropertyChanged("TotalUnpaidBillsAmount");
            }
        }

        private string _totalPartialPaidBillsAmount;
        public string TotalPartialPaidBillsAmount
        {
            get
            {
                return _totalPartialPaidBillsAmount;
            }
            set
            {
                _totalPartialPaidBillsAmount = value;
                RaisePropertyChanged("TotalPartialPaidBillsAmount");
            }
        }
        private string _totalPartialPaidBills;
        public string TotalPartialPaidBills
        {
            get
            {
                return _totalPartialPaidBills;
            }
            set
            {
                _totalPartialPaidBills = value;
                RaisePropertyChanged("TotalPartialPaidBills");
            }
        }

        private string _totalUnpaidBillAmount;
        public string TotalUnpaidBillAmount
        {
            get
            {
                return _totalUnpaidBillAmount;
            }
            set
            {
                _totalUnpaidBillAmount = value;
                RaisePropertyChanged("TotalUnpaidBillAmount");
            }
        }

        

        #endregion

        #region Constructor

        public DashboardViewModel(INavigationService navigationService, IDialogService dialogService)
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

            OnMyCertificateClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.MyCertificate);

            });
            OnBellClicked = new Command(async () =>
            {
            });
            OnMyTaxPayerProfileClicked = new Command(async () =>
            {
                _navigationService.NavigateTo(App.TaxPayerProfileView);

            });

            OnMyBillsClicked = new Command(async () =>
            {
                _navigationService.NavigateTo(App.MyBillsView);

            });

           


        }
        #endregion
        #region Method

        public async Task OnPageLoad()
        {

            try
            {
                string lang = UtilityManager.GetLanguageParameter();
                dashboard = WebServiceManager.GAZTGetDashboardData(lang, App.TP.Userid);
                await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                if (dashboard.results != null)
                {
                    TotalSubmittedReturn = dashboard.results[0].RtnTot;
                    TotalNonSubmittedReturn = dashboard.results[0].NrtnTot;
                    TotalPaidReturn = dashboard.results[0].PrtnTot;
                    TotalUnapidReturn = dashboard.results[0].UprtnTot;
                    TotalPartialPaidReturn = dashboard.results[0].PprtnTot;
                    TotalNoofReturns = dashboard.results[0].IcrTot;
                    TotalOverDueReturn = dashboard.results[0].DueIcr;
                    TotalPaidBills = dashboard.results[0].PbillsTot;
                    TotalPaidBillsAmount = dashboard.results[0].PbillsBetrw;
                    TotalUnpaidBills = dashboard.results[0].UpbillsTot;
                    TotalUnpaidBillsAmount = dashboard.results[0].UpbillsBetrw;
                    TotalPartialPaidBills = dashboard.results[0].PrbillsTot;
                    TotalPartialPaidBillsAmount = dashboard.results[0].PrbillsBetrw;
                    TotalUnpaidBillAmount = AppResources.TotalOfBillsUnpaid + dashboard.results[0].UpbillsBetrw;
                }
                else
                {
                    _dialogService.ShowMessageBox("No data available", AppResources.Information);
                }
            }
            catch(InternetException ex)
            {
                _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
            }





		}

        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }
        }

        #endregion
    }
}
