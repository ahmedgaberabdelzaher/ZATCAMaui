using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;


namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class DashboardAnonymousMenuPageViewModel : BaseViewModel
    {

        private bool _IsLoading=false ;
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private string _appVersion = App.AppVersion;
        public string AppVersion
        {
            get
            {
                return _appVersion;
            }
            set
            {
                _appVersion = value;
                RaisePropertyChanged("AppVersion");
            }
        }

        
        public DashboardAnonymousMenuPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }
    }
}
