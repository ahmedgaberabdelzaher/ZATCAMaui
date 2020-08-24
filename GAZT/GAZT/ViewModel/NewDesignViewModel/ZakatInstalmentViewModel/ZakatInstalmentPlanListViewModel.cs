using System;
using System.Collections.ObjectModel;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.Models.VATInstalmentModels;
using EGAZT.Models.ZakatInstalationModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel
{
    public class ZakatInstalmentPlanListViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        public ZakatInstalmentPlanListViewModel(INavigationService navigationService, IDialogService dialogService)
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
            //GoBackClick = new Command(async () =>
            //{
            //    _navigationService.GoBack();
            //});



        }

        //public ZakatInstalmentPlanListModel instalmentListModel { get; set; }
        //public ZakatInstalmentPlanListModel InstalmentListModel
        //{
        //    get
        //    {
        //        return instalmentListModel;
        //    }

        //    set
        //    {
        //        if (instalmentListModel == value)
        //        {
        //            return;
        //        }

        //        instalmentListModel = value;
        //        RaisePropertyChanged("InstalmentListModel");
        //    }
        //}


    }
}
