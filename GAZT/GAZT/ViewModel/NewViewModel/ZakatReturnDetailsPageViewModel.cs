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
   public class ZakatReturnDetailsPageViewModel: ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnBillsButtonClicked { get; set; }
        public ICommand OnSalesDetailsClicked { get; set; }
        string ReturnStatus = "2";

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

        private ZakatReturnDetailsD _zakatReturnDetail ;
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

        private ZakatReturnDetails _zakatReturnDetails;
        public ZakatReturnDetails ZakatReturnDetails
        {
            get
            {
                return _zakatReturnDetails;
            }
            set
            {
                _zakatReturnDetails = value;
                RaisePropertyChanged("ZakatReturnDetails");
            }
        }

        private string _releaseOrBillDetailsButtonText;
        public string ReleaseOrBillDetailsButtonText
        {
            get
            {
                return _releaseOrBillDetailsButtonText;
            }
            set
            {
                _releaseOrBillDetailsButtonText = value;
                RaisePropertyChanged("ReleaseOrBillDetailsButtonText");
            }
        }


        

        #endregion

        #region Constructor
        public ZakatReturnDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            OnBillsButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetails);
            });

            OnSalesDetailsClicked = new Xamarin.Forms.Command(async () =>
            {
                _navigationService.NavigateTo(App.SalesDetailsPageView, ZakatReturnDetails);
            });

           

            


        }
        #endregion

        #region Method
        public async Task OnPageLoad(string fbguid)
        {
            try
            {
                SetReleaseOrBillDetailsButtonText();
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                await Task.Run(async () =>
                {
                    ZakatReturnDetails zakatReturnDetails = await WebServiceManager.GAZTGetZAKATReturn(fbguid);
                    ZakatReturnDetails = zakatReturnDetails;
                    ZakatReturnDetail = zakatReturnDetails.d;
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch(Exception ex)
            {

            }
          
        }

        private void SetReleaseOrBillDetailsButtonText()
        {
            try
            {
                if (ReturnStatus.Equals("1"))
                {
                    ReleaseOrBillDetailsButtonText = AppResources.Release;
                }
                else if (ReturnStatus.Equals("2"))
                {
                    ReleaseOrBillDetailsButtonText = AppResources.Bill;
                }
            }
            catch(Exception ex)
            {

            }
           
        }
        #endregion
    }
}
