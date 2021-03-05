using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class MyBillsMultiplePayableListViewModel : BaseViewModel
    {

        #region Constructor

        public MyBillsMultiplePayableListViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            
        }
        #endregion

        private ObservableCollection<MyBills> multiplePayableBills;
        public ObservableCollection<MyBills> MultiplePayableBills
        {
            get
            {
                return multiplePayableBills;
            }
            set
            {
                if (multiplePayableBills == value) return;

                multiplePayableBills = value;

                RaisePropertyChanged("MultiplePayableBills");
            }
        }
    }


}
