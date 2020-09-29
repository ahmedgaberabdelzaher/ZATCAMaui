using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.ViewModel.NewDesignViewModel.VATServicesPageViewModel
{
    public class VATServicesPageViewModel : BaseViewModel
    {
        public bool IsVATAmendVisible { get => App.LoginDataRetrieved.VtReg == "X" ? true : false; }
        public bool IsVATReactivationVisible { get => App.LoginDataRetrieved.VtReg == "R" ? true : false; }
        #region Constructor
        public VATServicesPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }
        #endregion
    }
}
