using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class ZAKATReturnDetailsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnBackButtonClicked { get; set; }

        #region Property
        //public List<ReturnTypes> _TaxTypeForFilter = null;
        //public List<ReturnTypes> TaxTypeForFilter
        //{
        //    get
        //    {
        //        return _TaxTypeForFilter;
        //    }
        //    set
        //    {
        //        _TaxTypeForFilter = value;
        //        RaisePropertyChanged("TaxTypeForFilter");
        //    }
        //}
        #endregion

        #region Constructor
        public ZAKATReturnDetailsViewModel(INavigationService navigationService, IDialogService dialogService)
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
            //OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            //{
            //    _navigationService.GoBack();
            //});

        }
        #endregion


        #region Method

        #endregion

    }
}
