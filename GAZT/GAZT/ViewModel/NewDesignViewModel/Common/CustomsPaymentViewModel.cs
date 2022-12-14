using System;
using EGAZT.AppConfigurations;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.Common
{
    public class CustomsPaymentViewModel:BaseViewModel
    {
        string pageURL;
        public string PageURL { get { return pageURL; } set { pageURL = value; RaisePropertyChanged(); } }



        public CustomsPaymentViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
         
        }
    }
}

