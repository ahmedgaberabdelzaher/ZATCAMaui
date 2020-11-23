using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class BaseViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        private bool _isLoading = true;
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


        private FlowDirection _FlowDirect = FlowDirection.RightToLeft;
        public FlowDirection FlowDirect
        {
            get
            {
                return _FlowDirect;
            }
            set
            {
                _FlowDirect = value;
                RaisePropertyChanged("FlowDirect");
            }
        }


        public BaseViewModel(INavigationService navigationService, IDialogService dialogService)
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
            FlowDirect = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }

        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFLoginPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    //_navigationService.NavigateTo(App.SFLoginPageView);
                    //_navigation.NavigationStack.ToList().Clear();

                    _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                    _navigation.NavigationStack.ToList().Clear();
                });
            }
        }

        protected FlowDirection GetFlowDirectionToApply()
        {
            if (App.IsArabic)
                return FlowDirection.LeftToRight;
            else
                return FlowDirection.RightToLeft;
        }
    }
}
