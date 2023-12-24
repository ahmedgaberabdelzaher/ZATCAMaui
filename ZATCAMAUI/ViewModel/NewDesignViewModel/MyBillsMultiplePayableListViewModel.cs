using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

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
