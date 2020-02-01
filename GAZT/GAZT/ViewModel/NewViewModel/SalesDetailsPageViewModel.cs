using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class SalesDetailsPageViewModel : ViewModelBase
    {
        #region Variable
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        //  public ICommand OnBillsButtonClicked { get; set; }
        public ICommand OnAcceptReturnButtonClicked { get; set; }
        public ICommand OnAmendReturnButtonClicked { get; set; }
        public ZakatReturnDetailsD zakatReturnDetailsD { get; set; }



        #endregion

        #region Property
        private SalesDetails _selectedSalesDetails;
        public SalesDetails SelectedSalesDetails
        {
            get
            {
                return _selectedSalesDetails;
            }
            set
            {
                _selectedSalesDetails = value;
                if (_selectedSalesDetails != null)
                {
                    _navigationService.NavigateTo(App.AmendSalesDetailsPageView);
                }
                RaisePropertyChanged("SelectedSalesDetails");
            }
        }

        private ZakatReturnDetailsD _zakatReturnDetail;
        public ZakatReturnDetailsD ZakatReturnDetail
        {
            get
            {
                return _zakatReturnDetail;
            }
            set
            {
                _zakatReturnDetail = value;
                RaisePropertyChanged("ZakatReturnDetail");
            }
        }

        private List<SalesDetails> _SalesDetailsList;
        public List<SalesDetails> SalesDetailsList
        {
            get
            {
                return _SalesDetailsList;
            }
            set
            {
                _SalesDetailsList = value;
                RaisePropertyChanged("SalesDetailsList");
            }
        }

        private string _persl;
        public string Persl
        {
            get
            {
                return _persl;
            }
            set
            {
                _persl = value;
                RaisePropertyChanged("Persl");
            }
        }

        private string _abrzu;
        public string Abrzu
        {
            get
            {
                return _abrzu;
            }
            set
            {
                _abrzu = value;
                RaisePropertyChanged("Abrzu");
            }
        }

        private string _abrzo;
        public string Abrzo
        {
            get
            {
                return _abrzo;
            }
            set
            {
                _abrzo = value;
                RaisePropertyChanged("Abrzo");
            }
        }

        private string _fbnum;
        public string Fbnum
        {
            get
            {
                return _fbnum;
            }
            set
            {
                _fbnum = value;
                RaisePropertyChanged("Fbnum");
            }
        }

        private string _estsl;
        public string Estsl
        {
            get
            {
                return _estsl;
            }
            set
            {
                _estsl = value;
                RaisePropertyChanged("Estsl");
            }
        }



        #endregion

        #region Constructor
        public SalesDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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


            OnAcceptReturnButtonClicked = new Command(async () =>
            {
                _navigationService.NavigateTo(App.BillDetailsPageView);
            });

            OnAmendReturnButtonClicked = new Command(async () =>
            {
                try
                {
                    _navigationService.NavigateTo(App.AmendSalesDetailsPageView);
                }
                catch(Exception ex)
                {

                }
            });
        }
        #endregion

        #region Method
        public void onPageLoad()
        {
            ZakatReturnDetail = zakatReturnDetailsD;
            Persl = ZakatReturnDetail.Persl;
Abrzu = ZakatReturnDetail.Abrzu;
            Abrzo = ZakatReturnDetail.Abrzo;
            Fbnum = ZakatReturnDetail.Fbnum;
            Estsl = ZakatReturnDetail.Estsl;
            SalesDetailsList = new List<SalesDetails>();

            List<SalesDetails> SalesDetailsDummyList = new List<SalesDetails>();

            SalesDetails salesDetails1 = new SalesDetails();
            salesDetails1.SalesType = "Total VAT Sales";
            salesDetails1.InformationFromPartie = ZakatReturnDetail.TvtslResn;
            salesDetails1.EstimateSales = ZakatReturnDetail.TvtslE;
            SalesDetailsDummyList.Add(salesDetails1);

            SalesDetails salesDetails2 = new SalesDetails();
            salesDetails2.SalesType = "Average number of labour";
            salesDetails2.InformationFromPartie = ZakatReturnDetail.LabnoI;
            salesDetails2.EstimateSales = ZakatReturnDetail.LabnoE;
            SalesDetailsDummyList.Add(salesDetails2);

            SalesDetails salesDetails3 = new SalesDetails();
            salesDetails3.SalesType = "Imports value";
            salesDetails3.InformationFromPartie = ZakatReturnDetail.ImpvalI;
            salesDetails3.EstimateSales = ZakatReturnDetail.ImpvalE;
            SalesDetailsDummyList.Add(salesDetails3);

            SalesDetails salesDetails4 = new SalesDetails();
            salesDetails4.SalesType = "Sales form point of sales";
            salesDetails4.InformationFromPartie = ZakatReturnDetail.TvtslResn;
            salesDetails4.EstimateSales = ZakatReturnDetail.TvtslResn;
            SalesDetailsDummyList.Add(salesDetails4);

            SalesDetails salesDetails5 = new SalesDetails();
            salesDetails5.SalesType = "Contracts form ETIMAD system";
            salesDetails5.InformationFromPartie = ZakatReturnDetail.EtimadI;
            salesDetails5.EstimateSales = ZakatReturnDetail.Estsl;
            SalesDetailsDummyList.Add(salesDetails5);

            SalesDetails salesDetails6 = new SalesDetails();
            salesDetails6.SalesType = "Exports value";
            salesDetails6.InformationFromPartie = ZakatReturnDetail.ExamtResn;
            salesDetails6.EstimateSales = ZakatReturnDetail.ExamtI;
            SalesDetailsDummyList.Add(salesDetails6);

            SalesDetails salesDetails7 = new SalesDetails();
            salesDetails7.SalesType = "Purchase value";
            salesDetails7.InformationFromPartie = ZakatReturnDetail.PramtI;
            salesDetails7.EstimateSales = ZakatReturnDetail.PramtE;
            SalesDetailsDummyList.Add(salesDetails7);

            SalesDetails salesDetails8 = new SalesDetails();
            salesDetails8.SalesType = "Capital amount";
            salesDetails8.InformationFromPartie = "Missing";// ZakatReturnDetail.TvtslResn;
            salesDetails8.EstimateSales = "Missing";// ZakatReturnDetail.TvtslResn;
            SalesDetailsDummyList.Add(salesDetails8);

            SalesDetailsList = SalesDetailsDummyList;

          
        }
        #endregion
    }
}
