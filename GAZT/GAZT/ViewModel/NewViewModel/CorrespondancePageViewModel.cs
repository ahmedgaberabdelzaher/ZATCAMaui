using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;

namespace GAZT.ViewModel.NewViewModel
{
    public class CorrespondancePageViewModel : ViewModelBase
    {
        #region Properties
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand onZakatLabelClicked { get; set; }
        public ICommand onVATLabelClicked { get; set; }
        public ICommand onETLabelClicked { get; set; }
        public ICommand onCITLabelClicked { get; set; }
        private List<CorrespondanceModel> _listVATCorrespondance;
        public List<CorrespondanceModel> ListVATCorrespondance
        {
            get
            {
                return _listVATCorrespondance;
            }
            set
            {
                _listVATCorrespondance = value;
                RaisePropertyChanged("ListVATCorrespondance");
            }
        }

        private List<CorrespondanceModel> _listZAKATCorrespondance;
        public List<CorrespondanceModel> ListZAKATCorrespondance
        {
            get
            {
                return _listZAKATCorrespondance;
            }
            set
            {
                _listZAKATCorrespondance = value;
                RaisePropertyChanged("ListZAKATCorrespondance");
            }
        }

        private List<CorrespondanceModel> _listETCorrespondance;
        public List<CorrespondanceModel> ListETCorrespondance
        {
            get
            {
                return _listETCorrespondance;
            }
            set
            {
                _listETCorrespondance = value;
                RaisePropertyChanged("ListETCorrespondance");
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

        private bool _isZakatVisible = false;
        public bool IsZakatVisible
        {
            get
            {
                return _isZakatVisible;
            }
            set
            {
                _isZakatVisible = value;
                RaisePropertyChanged("IsZakatVisible");
            }
        }

        private bool _isVATVisible = false;
        public bool IsVATVisible
        {
            get
            {
                return _isVATVisible;
            }
            set
            {
                _isVATVisible = value;
                RaisePropertyChanged("IsVATVisible");
            }
        }

        private bool _isETVisible = false;
        public bool IsETVisible
        {
            get
            {
                return _isETVisible;
            }
            set
            {
                _isETVisible = value;
                RaisePropertyChanged("IsETVisible");
            }
        }

        private string _zakatCountDisplay = string.Empty;
        public string ZakatCountDisplay
        {
            get
            {
                return _zakatCountDisplay;
            }
            set
            {
                _zakatCountDisplay   = value;
                RaisePropertyChanged("ZakatCountDisplay");
            }
        }

        private string _vATCountDisplay = string.Empty;
        public string VATCountDisplay
        {
            get
            {
                return _vATCountDisplay;
            }
            set
            {
                _vATCountDisplay = value;
                RaisePropertyChanged("VATCountDisplay");
            }
        }

        private string _eTCountDisplay = string.Empty;
        public string ETCountDisplay
        {
            get
            {
                return _eTCountDisplay;
            }
            set
            {
                _eTCountDisplay = value;
                RaisePropertyChanged("ETCountDisplay");
            }
        }
        #endregion

        #region Constructor
        public CorrespondancePageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            onZakatLabelClicked = new Xamarin.Forms.Command(async () =>
            {
                IsZakatVisible = true;
                IsVATVisible = false;
                IsETVisible = false;
            });
            onVATLabelClicked = new Xamarin.Forms.Command(async () =>
            {
                IsZakatVisible = false;
                IsVATVisible = true;
                IsETVisible = false;
            });
            onETLabelClicked = new Xamarin.Forms.Command(async () =>
            {
                IsZakatVisible = false;
                IsVATVisible = false;
                IsETVisible = true;
            });
        }
        #endregion

        #region Methods
        public void onPageLoad()
        {
            CorrespondenceRootObject ZakatCorres = new CorrespondenceRootObject();

            ZakatCorres = WebServiceManager.GAZTGetZakatCorrespondece();
            List<CorrespondanceModel> ZakatCo = new List<CorrespondanceModel>();
            ZakatCountDisplay ="Zakat("+ ZakatCorres.d.results.Count+")";
            foreach (CorrespondenceResult itemZakat in ZakatCorres.d.results)
            {
                CorrespondanceModel childZakat = new CorrespondanceModel();
                childZakat.Title = itemZakat.Descript;
                childZakat.RefNumber = itemZakat.LetterNum;
                DateTime? BegDate = itemZakat.Begdaz;
                DateTime? endDate = itemZakat.Enddaz;
                if (itemZakat.Zzfav == "1")
                {
                    childZakat.IsFav = true;
                    childZakat.FavImg = "ic_save_golden.png";
                }
                else
                {
                    childZakat.IsFav = false;
                    childZakat.FavImg = "ic_save_Gray.png";
                }
                string StartDate = string.Empty;
                string EndDate = string.Empty;
                if (App.IsArabic)
                {
                    if (BegDate != null)
                    {

                        StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                        StartDate = UtilityManager.ToArabicDate(StartDate);
                    }
                    if (endDate != null)
                    {
                        EndDate = Convert.ToDateTime(endDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        EndDate = UtilityManager.ToArabicDate(EndDate);
                    }
                }
                else
                {
                    if (BegDate != null)
                    {
                       StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));



                    }
                    if (endDate != null)
                    {
                       EndDate = Convert.ToDateTime(endDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    }
                }
                childZakat.DateAndTime = StartDate + " - " + EndDate;
                ZakatCo.Add(childZakat);
            }
            ListZAKATCorrespondance = ZakatCo;



            CorrespondenceRootObject VATCorres = new CorrespondenceRootObject();

            VATCorres = WebServiceManager.GAZTGetVATCorrespondece();
            List<CorrespondanceModel> VATCo = new List<CorrespondanceModel>();
            VATCountDisplay = "VAT(" + VATCorres.d.results.Count + ")";
            foreach (CorrespondenceResult itemVAT in VATCorres.d.results)
            {
                CorrespondanceModel childVAT = new CorrespondanceModel();
                childVAT.Title = itemVAT.Descript;
                childVAT.RefNumber = itemVAT.LetterNum;
                DateTime? BegDate = itemVAT.Begdaz;
                DateTime? endDate = itemVAT.Enddaz;
                if (itemVAT.Zzfav == "1")
                {
                    childVAT.IsFav = true;
                    childVAT.FavImg = "ic_save_golden.png";
                }
                else
                {
                    childVAT.IsFav = false;
                    childVAT.FavImg = "ic_save_Gray.png";
                }
                string StartDate = string.Empty;
                string EndDate = string.Empty;
                if (App.IsArabic)
                {
                    if (BegDate != null)
                    {

                        StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                        StartDate = UtilityManager.ToArabicDate(StartDate);
                    }
                    if (endDate != null)
                    {
                        EndDate = Convert.ToDateTime(endDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        EndDate = UtilityManager.ToArabicDate(EndDate);
                    }
                }
                else
                {
                    if (BegDate != null)
                    {
                        StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));



                    }
                    if (endDate != null)
                    {
                        EndDate = Convert.ToDateTime(endDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    }
                }
                childVAT.DateAndTime = StartDate + " - " + EndDate;
                VATCo.Add(childVAT);
            }
            ListVATCorrespondance = VATCo;


            CorrespondenceRootObject ETCorres = new CorrespondenceRootObject();

            ETCorres = WebServiceManager.GAZTGetETCorrespondece();
            List<CorrespondanceModel> ETCo = new List<CorrespondanceModel>();
            ETCountDisplay = "ET(" + ETCorres.d.results.Count + ")";
            foreach (CorrespondenceResult itemET in ETCorres.d.results)
            {
                CorrespondanceModel childET = new CorrespondanceModel();
                childET.Title = itemET.Descript;
                childET.RefNumber = itemET.LetterNum;
                DateTime? BegDate = itemET.Begdaz;
                DateTime? endDate = itemET.Enddaz;
                if (itemET.Zzfav == "1")
                {
                    childET.IsFav = true;
                    childET.FavImg = "ic_save_golden.png";
                }
                else
                {
                    childET.IsFav = false;
                    childET.FavImg = "ic_save_Gray.png";
                }
                string StartDate = string.Empty;
                string EndDate = string.Empty;
                if (App.IsArabic)
                {
                    if (BegDate != null)
                    {

                        StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                        StartDate = UtilityManager.ToArabicDate(StartDate);
                    }
                    if (endDate != null)
                    {
                        EndDate = Convert.ToDateTime(endDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        EndDate = UtilityManager.ToArabicDate(EndDate);
                    }
                }
                else
                {
                    if (BegDate != null)
                    {
                        StartDate = Convert.ToDateTime(BegDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));



                    }
                    if (endDate != null)
                    {
                        EndDate = Convert.ToDateTime(endDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    }
                }
                childET.DateAndTime = StartDate + " - " + EndDate;
                ETCo.Add(childET);
            }
            ListETCorrespondance = ETCo;

            IsZakatVisible = true;
            IsVATVisible = false;
            IsETVisible = false;
        }
        #endregion
    }
}
