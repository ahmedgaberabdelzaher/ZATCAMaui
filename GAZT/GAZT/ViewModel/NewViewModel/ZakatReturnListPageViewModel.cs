using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class ZakatReturnListPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public EstimatedZakatReturns estimatedZakatReturnsList { get; set; }
        List<EstimatedZakatReturnsResult> myZakatReturnsList = new List<EstimatedZakatReturnsResult>();

        #endregion

        #region Property

        private List<ZAKATStatus> _iCRStatusList;
        public List<ZAKATStatus> ICRStatusList
        {
            get
            {
                return _iCRStatusList;
            }
            set
            {
                _iCRStatusList = value;
                RaisePropertyChanged("ICRStatusList");
            }
        }

        private ZAKATStatus _selectedICRStatus;
        public ZAKATStatus SelectedICRStatus
        {
            get
            {
                return _selectedICRStatus;
            }
            set
            {
                _selectedICRStatus = value;
                if (_selectedICRStatus != null)
                {
                    GetFilteredZAKATICRList(SelectedICRStatus);

                }
                RaisePropertyChanged("SelectedICR");
            }
        }

        private EstimatedZakatReturnsResult _selectedZakatReturn;
        public EstimatedZakatReturnsResult SelectedZakatReturn
        {
            get
            {
                return _selectedZakatReturn;
            }
            set
            {
                _selectedZakatReturn = value;
                RaisePropertyChanged("SelectedZakatReturn");

                if (SelectedZakatReturn != null)
                {
                    _navigationService.NavigateTo(App.ZakatReturnDetailsPageView, SelectedZakatReturn.Fbguid);
                }
            }
        }

        private List<EstimatedZakatReturnsResult> _myZakatReturns;
        public List<EstimatedZakatReturnsResult> MyZakatReturns
        {
            get
            {
                return _myZakatReturns;
            }
            set
            {
                _myZakatReturns = value;
                RaisePropertyChanged("MyZakatReturns");
            }
        }

        private List<ZakatReturnStatus> _zakatReturnStatus;
        public List<ZakatReturnStatus> ZakatReturnStatus
        {
            get
            {
                return _zakatReturnStatus;
            }
            set
            {
                _zakatReturnStatus = value;
                RaisePropertyChanged("ZakatReturnStatus");
            }
        }

        private ZakatReturnStatus _selectedZakatStatus;
        public ZakatReturnStatus SelectedZakatStatus
        {
            get
            {
                return _selectedZakatStatus;
            }
            set
            {
                _selectedZakatStatus = value;
              
                RaisePropertyChanged("SelectedZakatStatus");
            }
        }
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private int _selectedIndex ;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }
        
        #endregion

        #region Constructor
        public ZakatReturnListPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;



            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
        }
        #endregion

        #region Method


        public async Task OnPageLoad()
        {
            GetZAKATICRStatusList();
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async() =>
            {
            try
            {
                estimatedZakatReturnsList = await WebServiceManager.GAZTGetEstimateZakatReturnList();
                PopToRootPage();
                UpdateICRList();
                SelectedIndex = 13;
                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                }
                // MyZakatReturns = estimatedZakatReturnsList.d.listSet.results ;
            });

            //MyZakatReturns = new List<ZakatReturns>();
            //for(int i=0;i<=5;i++)
            //{
            //    ZakatReturns returns1 = new ZakatReturns();
            //    returns1.FiscalYear = "2018";
            //    returns1.ReturnPeriod = "2018/08/08 - 2017/06/07";
            //    returns1.DueDate = "31/01/2020";
            //    returns1.IDNumber = "100000988";
            //    returns1.Status = "Billed";
            //    MyZakatReturns.Add(returns1);
            //}
            //ZakatReturnStatus = new List<ZakatReturnStatus>();

            //ZakatReturnStatus.Add(new Models.ZakatReturnStatus { ID = 1, Value = "ABC1" });
            //ZakatReturnStatus.Add(new Models.ZakatReturnStatus { ID = 2, Value = "ABC2" });
            //ZakatReturnStatus.Add(new Models.ZakatReturnStatus { ID = 3, Value = "ABC3" });
            //ZakatReturnStatus.Add(new Models.ZakatReturnStatus { ID = 4, Value = "ABC4" });

            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }

        private void GetZAKATICRStatusList()
        {
            List<ZAKATStatus> ZAKATStatusListEn = new List<ZAKATStatus>()
            {
                new ZAKATStatus{ Key ="IP011", Value = "Submitted"},
                 new ZAKATStatus{ Key ="IP014", Value = "Billed"},
                  new ZAKATStatus{ Key ="U", Value = "Unsubmitted"},
                   new ZAKATStatus{ Key ="P", Value = "Paid"},
                   new ZAKATStatus{ Key ="I", Value = "Partially paid"},
                 new ZAKATStatus{ Key ="IP015", Value = "In Processing"},
                  new ZAKATStatus{ Key ="IP017", Value = "Parked"},
                   new ZAKATStatus{ Key ="IP019", Value = "Rejected"},
                   new ZAKATStatus{ Key ="IP021", Value = "To Be Approved"},
                 new ZAKATStatus{ Key ="C0021", Value = "To Be Filled & Parked"},
                  new ZAKATStatus{ Key ="ZP017", Value = "Parked in Amendment"},
                   new ZAKATStatus{ Key ="E0089", Value = "GSTC – Escalation In Process"},
                   new ZAKATStatus{ Key ="E0090", Value = "GSTC – Escalation Completed"},
                 new ZAKATStatus{ Key ="ALL", Value = "All"},
                   
            };

            List<ZAKATStatus> ZAKATStatusListAr = new List<ZAKATStatus>()
            {
                new ZAKATStatus{ Key ="IP011", Value = "تم تقديمه"},
                 new ZAKATStatus{ Key ="IP014", Value = "مفوتر"},
                  new ZAKATStatus{ Key ="U", Value = "لم يتم تقديمه"},
                   new ZAKATStatus{ Key ="P", Value = "مسدد"},
                   new ZAKATStatus{ Key ="I", Value = "مسدد جزئياً"},
                 new ZAKATStatus{ Key ="IP015", Value = "في طور المعالجة"},
                  new ZAKATStatus{ Key ="IP017", Value = "محفوظ كمسودة"},
                   new ZAKATStatus{ Key ="IP019", Value = "مرفوض"},
                   new ZAKATStatus{ Key ="IP021", Value = "إنتظار الموافقة"},
                 new ZAKATStatus{ Key ="C0021", Value = "جاهز للتعبئة و الحفظ كمسودة"},
                  new ZAKATStatus{ Key ="ZP017", Value = "محفوظ كمسودة تعديل"},
                   new ZAKATStatus{ Key ="E0089", Value = "الأمانة –قيد التصعيد"},
                   new ZAKATStatus{ Key ="E0090", Value = "الأمانة – انتهاء التصعيد"},
                 new ZAKATStatus{ Key ="ALL", Value = "الجميع"},

            };

            if(App.IsArabic)
            {
                ICRStatusList = ZAKATStatusListAr;
            }
            else
            {
                ICRStatusList = ZAKATStatusListEn;
            }
        }

        private void GetFilteredZAKATICRList(ZAKATStatus selectedICR)
        {
            try
            {
                List<EstimatedZakatReturnsResult> FilteredCRStatusList = new List<EstimatedZakatReturnsResult>();
                if (myZakatReturnsList != null)
                {
                    if (selectedICR.Key.Equals("ALL"))
                    {
                        MyZakatReturns = myZakatReturnsList;
                    }
                    else
                    {
                        for (int i = 0; i < myZakatReturnsList.Count; i++)
                        {
                            if (selectedICR.Key.Equals(myZakatReturnsList[i].Statfg))
                            {
                                FilteredCRStatusList.Add(myZakatReturnsList[i]);
                            }
                        }
                        MyZakatReturns = FilteredCRStatusList;
                    }

                }
            }
            catch(Exception ex)
            {

            }
        }

        private void UpdateICRList()
        {
            List<EstimatedZakatReturnsResult> myZakatReturnsListTemp = new List<EstimatedZakatReturnsResult>();
            myZakatReturnsListTemp = estimatedZakatReturnsList.d.listSet.results;
            for (int i= 0; i< myZakatReturnsListTemp.Count; i++)
            {

                if ((string.Equals(myZakatReturnsListTemp[i].Statfg, "U")))//UnSubmitted_status, "IP011") || string.Equals(_status, "IP014") || 
                {

                    myZakatReturnsListTemp[i].StatusImage = "ic_unsubmitted.png";
                    myZakatReturnsListTemp[i].BorderColour = "#944E22";
                }
                else if (string.Equals(myZakatReturnsListTemp[i].Statfg, "P"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
                {
                    myZakatReturnsListTemp[i].BorderColour = "#005e4b";
                    myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                }
                else if (string.Equals(myZakatReturnsListTemp[i].Statfg, "IP015"))//In processing || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
                {
                    myZakatReturnsListTemp[i].BorderColour = "#c49b2d";
                    myZakatReturnsListTemp[i].StatusImage = "ic_loading.png";
                }
                else if (string.Equals(myZakatReturnsListTemp[i].Statfg, "IP014"))//Build || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
                {
                    myZakatReturnsListTemp[i].BorderColour = "#005e4b";
                    myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                }

                myZakatReturnsList.Add(myZakatReturnsListTemp[i]);
            }
           
            MyZakatReturns = myZakatReturnsList;
        }

        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
        #endregion
    }
}
