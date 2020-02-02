using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
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
        public ICommand OnSubmitButtonClicked { get; set; }
      


        public ZakatReturnDetails zakatReturnDetailsD { get; set; }



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
                    _navigationService.NavigateTo(App.AmendSalesDetailsPageView, SelectedSalesDetails);
                }
                RaisePropertyChanged("SelectedSalesDetails");
            }
        }

        private ZakatReturnDetails _zakatReturnDetail;
        public ZakatReturnDetails ZakatReturnDetail
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

        private bool _amedmentButtonVisibility = true;
        public bool AmedmentButtonVisibility 
        {
            get
            {
                return _amedmentButtonVisibility;
            }
            set
            {
                _amedmentButtonVisibility = value;
                RaisePropertyChanged("AmedmentButtonVisibility");
            }
        }

        private bool _submitButtonVisibility = false;
        public bool SubmitButtonVisibility
        {
            get
            {
                return _submitButtonVisibility;
            }
            set
            {
                _submitButtonVisibility = value;
                RaisePropertyChanged("SubmitButtonVisibility");
            }
        }

        private bool _checkBoxStatus = false;
        public bool CheckBoxStatus
        {
            get
            {
                return _checkBoxStatus;
            }
            set
            {
                _checkBoxStatus = value;
                RaisePropertyChanged("CheckBoxStatus");
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
                    AmedmentButtonVisibility = false;
                    SubmitButtonVisibility = true;
                   _navigationService.NavigateTo(App.AmendSalesDetailsPageView);
                }
                catch(Exception ex)
                {

                }
            });

            OnSubmitButtonClicked = new Command(async () =>
            {
                if(CheckBoxStatus)
                {
                    ZakatReturnDetails zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(zakatReturnDetailsD);
                }
                else
                {
                    // show message
                }
            });
            
        }
        #endregion

        #region Method
        public void onPageLoad()
        {
            try
            {
                ZakatReturnDetail = zakatReturnDetailsD;
                Persl = ZakatReturnDetail.d.Persl;
                Abrzu = ZakatReturnDetail.d.Abrzu;
                Abrzo = ZakatReturnDetail.d.Abrzo;
                Fbnum = ZakatReturnDetail.d.Fbnum;
                Estsl = ZakatReturnDetail.d.Estsl;
                SalesDetailsList = new List<SalesDetails>();

                List<SalesDetails> SalesDetailsDummyList = new List<SalesDetails>();

                SalesDetails salesDetails1 = new SalesDetails();
                salesDetails1.SalesType = "Total VAT Sales";
                salesDetails1.InformationFromPartie = ZakatReturnDetail.d.TvtslResn;
                salesDetails1.EstimateSales = ZakatReturnDetail.d.TvtslE;
                salesDetails1.SelectedEditFieldId = "1";


                SalesDetailsDummyList.Add(salesDetails1);

                SalesDetails salesDetails2 = new SalesDetails();
                salesDetails2.SalesType = "Average number of labour";
                salesDetails2.InformationFromPartie = string.IsNullOrEmpty(ZakatReturnDetail.d.LabnoI) ? "0.00" : ZakatReturnDetail.d.LabnoI; // ZakatReturnDetail.d.LabnoI;
                salesDetails2.EstimateSales = string.IsNullOrEmpty(ZakatReturnDetail.d.LabnoE) ? "0.00" : ZakatReturnDetail.d.LabnoI; //ZakatReturnDetail.d.LabnoE;
                salesDetails2.SelectedEditFieldId = "2";
                SalesDetailsDummyList.Add(salesDetails2);

                SalesDetails salesDetails3 = new SalesDetails();
                salesDetails3.SalesType = "Imports value";
                salesDetails3.InformationFromPartie = string.IsNullOrEmpty(ZakatReturnDetail.d.ImpvalI) ? "0.00" : ZakatReturnDetail.d.ImpvalI; // ZakatReturnDetail.d.ImpvalI;
                salesDetails3.EstimateSales = string.IsNullOrEmpty(ZakatReturnDetail.d.ImpvalE) ? "0.00" : ZakatReturnDetail.d.ImpvalE; // ZakatReturnDetail.d.ImpvalE;
                salesDetails3.SelectedEditFieldId = "3";
                SalesDetailsDummyList.Add(salesDetails3);

                SalesDetails salesDetails4 = new SalesDetails();
                salesDetails4.SalesType = "Sales form point of sales";
                salesDetails4.InformationFromPartie = string.IsNullOrEmpty(ZakatReturnDetail.d.TvtslResn) ? "0.00" : ZakatReturnDetail.d.TvtslResn; // ZakatReturnDetail.d.TvtslResn;
                salesDetails4.EstimateSales = string.IsNullOrEmpty(ZakatReturnDetail.d.TvtslResn) ? "0.00" : ZakatReturnDetail.d.TvtslResn; // ZakatReturnDetail.d.TvtslResn;
                salesDetails4.SelectedEditFieldId = "4";
                SalesDetailsDummyList.Add(salesDetails4);

                SalesDetails salesDetails5 = new SalesDetails();
                salesDetails5.SalesType = "Contracts form ETIMAD system";
                salesDetails5.InformationFromPartie = string.IsNullOrEmpty(ZakatReturnDetail.d.EtimadI) ? "0.00" : ZakatReturnDetail.d.EtimadI; //ZakatReturnDetail.d.EtimadI;
                salesDetails5.EstimateSales = string.IsNullOrEmpty(ZakatReturnDetail.d.Estsl) ? "0.00" : ZakatReturnDetail.d.Estsl; //ZakatReturnDetail.d.Estsl;
                salesDetails5.SelectedEditFieldId = "5";
                SalesDetailsDummyList.Add(salesDetails5);

                SalesDetails salesDetails6 = new SalesDetails();
                salesDetails6.SalesType = "Exports value";
                salesDetails6.InformationFromPartie = string.IsNullOrEmpty(ZakatReturnDetail.d.ExamtResn) ? "0.00" : ZakatReturnDetail.d.ExamtResn; //ZakatReturnDetail.d.ExamtResn;
                salesDetails6.EstimateSales = string.IsNullOrEmpty(ZakatReturnDetail.d.ExamtI) ? "0.00" : ZakatReturnDetail.d.ExamtI; // ZakatReturnDetail.d.ExamtI;
                salesDetails6.SelectedEditFieldId = "6";
                SalesDetailsDummyList.Add(salesDetails6);

                SalesDetails salesDetails7 = new SalesDetails();
                salesDetails7.SalesType = "Purchase value";
                salesDetails7.InformationFromPartie = string.IsNullOrEmpty(ZakatReturnDetail.d.PramtI) ? "0.00" : ZakatReturnDetail.d.PramtI; // ZakatReturnDetail.d.PramtI;
                salesDetails7.EstimateSales = string.IsNullOrEmpty(ZakatReturnDetail.d.PramtE) ? "0.00" : ZakatReturnDetail.d.PramtE; // ZakatReturnDetail.d.PramtE;
                salesDetails7.SelectedEditFieldId = "7";

                SalesDetailsDummyList.Add(salesDetails7);

                SalesDetails salesDetails8 = new SalesDetails();
                salesDetails8.SalesType = "Capital amount";
                salesDetails8.InformationFromPartie = "Missing";// ZakatReturnDetail.TvtslResn;
                salesDetails8.EstimateSales = "Missing";// ZakatReturnDetail.TvtslResn;
                SalesDetailsDummyList.Add(salesDetails8);
                salesDetails8.SelectedEditFieldId = "8";

                SalesDetailsList = SalesDetailsDummyList;
            }
            catch(Exception ex)
            {

            }
            

          
        }
        #endregion
    }
}
