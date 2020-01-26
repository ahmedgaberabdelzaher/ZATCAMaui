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
            SalesDetailsList = new List<SalesDetails>();

            List<SalesDetails> SalesDetailsDummyList = new List<SalesDetails>();

            SalesDetails salesDetails1 = new SalesDetails();
            salesDetails1.SalesType = "Total VAT Sales";
            salesDetails1.InformationFromPartie = "0000000000";
            salesDetails1.EstimateSales = "000000000000.00";
            SalesDetailsDummyList.Add(salesDetails1);

            SalesDetails salesDetails2 = new SalesDetails();
            salesDetails2.SalesType = "Average number of labour";
            salesDetails2.InformationFromPartie = "0000000000";
            salesDetails2.EstimateSales = "000000000000.00";
            SalesDetailsDummyList.Add(salesDetails2);

            SalesDetails salesDetails3 = new SalesDetails();
            salesDetails3.SalesType = "Imports value";
            salesDetails3.InformationFromPartie = "0000000000";
            salesDetails3.EstimateSales = "000000000000.00";
            SalesDetailsDummyList.Add(salesDetails3);

            SalesDetails salesDetails4 = new SalesDetails();
            salesDetails4.SalesType = "Sales form point of sales";
            salesDetails4.InformationFromPartie = "0000000000";
            salesDetails4.EstimateSales = "000000000000.00";
            SalesDetailsDummyList.Add(salesDetails4);

            SalesDetails salesDetails5 = new SalesDetails();
            salesDetails5.SalesType = "Contracts form ETIMAD system";
            salesDetails5.InformationFromPartie = "0000000000";
            salesDetails5.EstimateSales = "000000000000.00";
            SalesDetailsDummyList.Add(salesDetails5);

            SalesDetails salesDetails6 = new SalesDetails();
            salesDetails6.SalesType = "Exports value";
            salesDetails6.InformationFromPartie = "0000000000";
            salesDetails6.EstimateSales = "000000000000.00";
            SalesDetailsDummyList.Add(salesDetails6);

            SalesDetails salesDetails7 = new SalesDetails();
            salesDetails7.SalesType = "Purchase value";
            salesDetails7.InformationFromPartie = "0000000000";
            salesDetails7.EstimateSales = "000000000000.00";
            SalesDetailsDummyList.Add(salesDetails7);

            SalesDetails salesDetails8 = new SalesDetails();
            salesDetails8.SalesType = "Capital amount";
            salesDetails8.InformationFromPartie = "0000000000";
            salesDetails8.EstimateSales = "000000000000.00";
            SalesDetailsDummyList.Add(salesDetails8);

            SalesDetailsList = SalesDetailsDummyList;

          
        }
        #endregion
    }
}
