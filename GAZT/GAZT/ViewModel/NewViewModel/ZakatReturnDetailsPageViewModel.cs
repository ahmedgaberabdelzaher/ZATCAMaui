using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
        public static bool IsAmendButtonPressed = false;
        public static string Fbguid  { get; set; }

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
               
            });

            OnSalesDetailsClicked = new Xamarin.Forms.Command(async () =>
            {
                IsAmendButtonPressed = false;
                _navigationService.NavigateTo(App.SalesDetailsPageView, ZakatReturnDetails);
            });

        }
        #endregion

        #region Method

        public async Task OnReleaseOrBillsClicked()
        {
            if (ZakatReturnDetails.d.Statusz.Equals("E0001") || ZakatReturnDetails.d.Statusz.Equals("IP011"))
            {// Call the Post API to release and if response is true then set the Button Name as bills and after tapping on that user needs to be navigated to Bills page 
                await ReleaseEstimateZakatReturn();
            }
            else if (ZakatReturnDetails.d.Statusz.Equals("IP014") || ZakatReturnDetails.d.Statusz.Equals("E0002"))
            {
                IsAmendButtonPressed = true;
                _navigationService.NavigateTo(App.SalesDetailsPageView, ZakatReturnDetails);
            }
            else if (ReleaseOrBillDetailsButtonText.Equals("Bills") || ReleaseOrBillDetailsButtonText.Equals("الفواتير"))
            {
                _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetail);
            }
            else if (ZakatReturnDetails.d.Statusz.Equals("E0004") || ZakatReturnDetails.d.Statusz.Equals("E0003"))//Whent the Return is already Ameded by Taxpayer(E0004), and When the return is released but not Amended yet(E0003)
            {
                _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetail);
            }
            else
            {
                _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetail);
            }
        }
        public async Task OnPageLoad(string fbguid)
        {
            try
            {
                Fbguid = fbguid;
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                await Task.Run(async () =>
                {
                   ZakatReturnDetails zakatReturnDetails = await WebServiceManager.GAZTGetZAKATReturn(fbguid);
                //  var res =   WebServiceManager.GAZTGetEstimatedZakatReturnSADADNumber(zakatReturnDetails.d.Fbnum, fbguid); // Method to get the invoice
                    //  EsimatedZAKATReturnsButtonSets esimatedZAKATReturnsButtonSets = await WebServiceManager.GAZTGetZAKATReturnButtonSet();
                    ZakatReturnDetails = zakatReturnDetails;
                    ZakatReturnDetail = zakatReturnDetails.d;
                    SetReleaseOrBillDetailsButtonText(ZakatReturnDetails.d.Statusz);
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

        private void SetReleaseOrBillDetailsButtonText(string ButtonStatus)
        {
            try
            {
                if (ButtonStatus.Equals("E0001") || ButtonStatus.Equals("IP011"))
                {
                    ReleaseOrBillDetailsButtonText = AppResources.Release;
                }
                else if (ButtonStatus.Equals("IP014") || ButtonStatus.Equals("E0002") )
                {
                    ReleaseOrBillDetailsButtonText = AppResources.AmendTheReturn;
                }
                else if (ButtonStatus.Equals("E0004") || ButtonStatus.Equals("E0003") || ButtonStatus.Equals("E0008"))//Whent the Return is already Ameded by Taxpayer(E0004), and When the return is released but not Amended yet(E0003)
                {
                    //ButtonStatus.Equals("E0008") This has been varified by using Code
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
                else if(ButtonStatus.Equals("E0005"))//In Processing
                {
                    ReleaseOrBillDetailsButtonText = AppResources.BillDetails;
                }
            }
            catch(Exception ex)
            {

            }
           
        }

        public async Task ReleaseEstimateZakatReturn()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                ZakatReturnDetails _zakatReturnDetails =await WebServiceManager.GAZTSaveZakatReturnData(ZakatReturnDetails,"59");
                if(_zakatReturnDetails != null && _zakatReturnDetails.d != null)
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        await _dialogService.ShowMessageBox("Released Successfully.", "SUCCESS");
                    });
                    // 
                }
                ZakatReturnDetails zakatReturnDetails = await WebServiceManager.GAZTGetZAKATReturn(Fbguid);
                //  EsimatedZAKATReturnsButtonSets esimatedZAKATReturnsButtonSets = await WebServiceManager.GAZTGetZAKATReturnButtonSet();
                ZakatReturnDetails = zakatReturnDetails;
                ZakatReturnDetail = zakatReturnDetails.d;
                SetReleaseOrBillDetailsButtonText(ZakatReturnDetails.d.Statusz);
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }

      

        #endregion
    }
}
