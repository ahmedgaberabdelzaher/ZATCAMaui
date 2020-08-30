using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;


namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class DashboardAnonymousMenuPageViewModel : BaseViewModel
    {

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
