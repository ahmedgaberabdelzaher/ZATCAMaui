using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZakatForm5Model;

namespace EGAZT.ViewModel.NewDesignViewModel
{
     public class ZakatForm5PageViewModel : BaseViewModel
    {
        

        #region Constructor
        public ZakatForm5PageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
        #endregion
        

        public ZakatForm5Data _zakatForm5DataResult;
        public ZakatForm5Data ZakatForm5DataResult
        {
            get
            {
                return _zakatForm5DataResult;
            }
            set
            {
                _zakatForm5DataResult = value;
                RaisePropertyChanged("ZakatForm5DataResult");
            }
        }

        private bool _isNoDataLableVisible = false;
        public bool isNoDataLableVisible
        {
            get
            {
                return _isNoDataLableVisible;
            }
            set
            {
                _isNoDataLableVisible = value;
                RaisePropertyChanged("isNoDataLableVisible");
            }
        }


        public string PerslText;
        public string _perslText
        {
            get
            {
                return _perslText;
            }
            set
            {
                _perslText = value;
                RaisePropertyChanged("PerslText");
            }
        }



        #region Method


        
        public async Task LoadZakatForm5Data()
        {
            IsLoading = true;
            ZakatForm5DataResult = null;

            try
            {
                try
                {

                    ZakatForm5DataResult ZakatForm5DataResult = await WebServiceManager.GAZTZakatForm5Data();

                    PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                    if (ZakatForm5DataResult != null)
                    {

                        //Bind values to UI
                        PerslText = ZakatForm5DataResult.PerslText;

                        IsLoading = false;

                    }
                    else
                    {
                        isNoDataLableVisible = true;
                        IsLoading = false;
                    }
                }
                catch (Exception e)
                {

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(e.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                    IsLoading = false;
                }
            }
            catch (InternetException ex)
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
                IsLoading = false;
            }
            IsLoading = false;
        }

        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (App.TP != null)
                        App.TP = null;
                    if (App.PreviousIsArabic)
                    {
                        String langName = "ar-SA";
                        AppResources.Culture = new CultureInfo(langName);
                    }
                    else
                    {
                        String langName = "en-US";
                        AppResources.Culture = new CultureInfo(langName);
                    }
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFLoginPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigationService.NavigateTo(App.SFLoginPageView);
                    _navigation.NavigationStack.ToList().Clear();
                });
            }
        }

        #endregion
    }
}
