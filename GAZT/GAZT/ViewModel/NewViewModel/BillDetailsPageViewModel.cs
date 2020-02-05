using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GAZT.ViewModel.NewViewModel
{
   public class BillDetailsPageViewModel: ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnCopySadadNumberButtonClicked { get; set; }
        public ZakatReturnDetailsD zakatReturnDetailsD { get; set; }

        #endregion

        #region Property

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

        private EstimatedZAKATReturnsSADADNumberResult _estimatedZAKATSADADNumber;
        public EstimatedZAKATReturnsSADADNumberResult EstimatedZAKATSADADNumber
        {
            get
            {
                return _estimatedZAKATSADADNumber;
            }
            set
            {
                _estimatedZAKATSADADNumber = value;
                RaisePropertyChanged("EstimatedZAKATSADADNumber");
            }
        }

        //private string _sopbel;
        //public string Sopbel
        //{
        //    get
        //    {
        //        return _stotamt;
        //    }
        //    set
        //    {
        //        _stotamt = value;
        //        RaisePropertyChanged("Sopbel");
        //    }
        //}

        //private string _sadadid;
        //public string Sadadid
        //{
        //    get
        //    {
        //        return _sadadid;
        //    }
        //    set
        //    {
        //        _sadadid = value;
        //        RaisePropertyChanged("Sadadid");
        //    }
        //}

        //private string _stotamt;
        //public string Stotamt
        //{
        //    get
        //    {
        //        return _sopbel;
        //    }
        //    set
        //    {
        //        _sopbel = value;
        //        RaisePropertyChanged("Stotamt");
        //    }
        //}



        #endregion

        #region Constructor
        public BillDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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


            OnCopySadadNumberButtonClicked = new Xamarin.Forms.Command(async () =>
            {
               await _dialogService.ShowMessage("It has copied sadad payment number",AppResources.Information);
            });


        }
        #endregion

        public async Task OnPageLoad()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async() =>
            {
                EstimatedZAKATReturnsSADADNumber estimatedZAKATReturnsSADADNumber = await WebServiceManager.GAZTGetEstimatedZakatReturnSADADNumber(zakatReturnDetailsD.Fbnum, ZakatReturnDetailsPageViewModel.Fbguid); // Method to get the invoice
                EstimatedZAKATSADADNumber = estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0];
            });

            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }
        #region Method
        #endregion
    }
}
