using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using GAZT.Manager;
using System.Globalization;
using Newtonsoft.Json;
using GAZT.Helper;

namespace GAZT.ViewModel.NewViewModel
{
    public class ChecKTINStatusViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnCloseClick { get; set; }

        public ICommand OnClickLessOrMore { get; set; }
        public DateTime lastTapped;


        private string _TIN = string.Empty;

        public string TIN
        {
            get
            {
                return _TIN;
            }
            set
            {
                _TIN = value;
                RaisePropertyChanged("TIN");
            }
        }


        private string _TINStatus = string.Empty;

        public string TINStatus
        {
            get
            {
                return _TINStatus;
            }
            set
            {
                _TINStatus = value;
                RaisePropertyChanged("TINStatus");
            }
        }

        private string _LastUpdate = string.Empty;
        public string LastUpdate
        {
            get
            {
                return _LastUpdate;
            }
            set
            {
                _LastUpdate = value;
                RaisePropertyChanged("LastUpdate");
            }
        }

        private bool _isVisibleListItems = false;
        public bool IsVisibleListItems
        {
            get
            {
                return _isVisibleListItems;
            }
            set
            {
                _isVisibleListItems = value;
                RaisePropertyChanged("IsVisibleListItems");
            }

        }

        private TINStatus _listTINStatus;

        public TINStatus ListTINStatus
        {
            get
            {
                return _listTINStatus;
            }
            set
            {
                _listTINStatus = value;
                RaisePropertyChanged("ListTINStatus");
            }
        }

        private List<ConsumerRegisteration> _consumerRegisteration;

        public List<ConsumerRegisteration> ConsumerRegisteration
        {
            get
            {
                return _consumerRegisteration;
            }
            set
            {
                _consumerRegisteration = value;
                RaisePropertyChanged("ConsumerRegisteration");
            }
        }

        private string _showLessOrMore= AppResources.ZShowmoredetails;

        public string ShowLessOrMore
        {
            get
            {
                return _showLessOrMore;
            }
            set
            {
                _showLessOrMore = value;
                RaisePropertyChanged("ShowLessOrMore");
            }
        }

        
        public ChecKTINStatusViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            OnCloseClick = new Command(async () =>
            {
                _navigationService.NavigateTo(App.DashboardPageView);
            });

            OnClickLessOrMore = new Command(async () =>
            {
                if (IsVisibleListItems == false)
                {
                    IsVisibleListItems = true;
                    ShowLessOrMore = AppResources.ZShowlessdetails;

                }
                else
                {
                    IsVisibleListItems = false;
                    ShowLessOrMore = AppResources.ZShowmoredetails;
                }
            });
        }

        public async Task OnPageLoad()
        {
            try
            {

                string Lang = UtilityManager.GetLanguageParameter();
                if (lastTapped < DateTime.Now.AddSeconds(-4))
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        ListTINStatus = await WebServiceManager.GAZTGetTinStatus(Lang, App.TP.Tin);
                        PopToRootPage();
                        TIN = ListTINStatus.d.Tin;
                        TINStatus = ListTINStatus.d.StatusText;
                        if (ListTINStatus.d.Udate != null)
                        {
                          
                            if (App.IsArabic)
                            {
                                DateTime dateLU = DateTime.ParseExact(ListTINStatus.d.Udate.ToString(), "dd/MM/yyyy", new CultureInfo("en-US"));
                                LastUpdate = dateLU.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));                               
                                LastUpdate = UtilityManager.ToArabicDate(LastUpdate);
                            }
                            else
                            {
                                DateTime dateLU = DateTime.ParseExact(ListTINStatus.d.Udate.ToString(), "dd/MM/yyyy", new CultureInfo("en-US"));
                                LastUpdate = dateLU.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                              

                            }
                        }

                        ConsumerRegisteration = ListTINStatus.d.ItemSet.results;

                        if (ConsumerRegisteration != null)
                        {
                            if (ListTINStatus.d.ItemSet.results != null)
                            {
                                foreach (ConsumerRegisteration itemCR in ListTINStatus.d.ItemSet.results)
                                {
                                    if (itemCR.Udate != null)
                                    {
                                        if (App.IsArabic)
                                        {
                                            itemCR.Udate = JsonConvert.DeserializeObject<DateTime>(@"""" + itemCR.Udate + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                            itemCR.Udate = UtilityManager.ToArabicDate(itemCR.Udate);
                                        }
                                        else
                                        {
                                            itemCR.Udate = JsonConvert.DeserializeObject<DateTime>(@"""" + itemCR.Udate + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //  await _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Alerts);
                    }
                }

                //List<TINStatus> StatusList = new List<TINStatus>
                //{
                //    new TINStatus{ CRNos="111111111111111",CRStatus="Deactive",LastUpdate="10-10-2019"},
                //     new TINStatus{ CRNos="111111111111111",CRStatus="Deactive",LastUpdate="10-10-2019"},
                //      new TINStatus{ CRNos="111111111111111",CRStatus="Deactive",LastUpdate="10-10-2019"},
                //       new TINStatus{ CRNos="111111111111111",CRStatus="Deactive",LastUpdate="10-10-2019"},
                //        new TINStatus{ CRNos="111111111111111",CRStatus="Deactive",LastUpdate="10-10-2019"},
                //         new TINStatus{ CRNos="111111111111111",CRStatus="Deactive",LastUpdate="10-10-2019"},
                //    new TINStatus{ CRNos="111111122222222",CRStatus="Active",LastUpdate="11-10-2019"}
                //};
                //ListTINStatus = StatusList;
            }
            catch(InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information); 
            }
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {

                var _navigation = Application.Current.MainPage.Navigation;
                _navigation.PopToRootAsync();

            }
        }
    }
}
