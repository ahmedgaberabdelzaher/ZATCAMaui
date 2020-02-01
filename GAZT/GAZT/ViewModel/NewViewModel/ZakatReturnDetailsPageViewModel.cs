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


        
        #endregion

        #region Constructor
        public ZakatReturnDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            OnBillsButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                _navigationService.NavigateTo(App.BillDetailsPageView);
            });

            OnSalesDetailsClicked = new Xamarin.Forms.Command(async () =>
            {
                _navigationService.NavigateTo(App.SalesDetailsPageView);
            });


            
        }
        #endregion

        #region Method
        public async Task OnPageLoad(string fbguid)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async() =>
            {
                ZakatReturnDetails zakatReturnDetails = await WebServiceManager.GAZTGetZAKATReturn(fbguid);
                ZakatReturnDetail = zakatReturnDetails.d;
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }
        #endregion
    }
}
