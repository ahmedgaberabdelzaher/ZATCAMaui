using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace GAZT.ViewModel.NewViewModel
{
    public class CertificateListViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnMyCertificateClicked { get; set; }
        #endregion

        #region Property
        //private List<BillReturn> _billReturn;
        //public List<BillReturn> BillReturn
        //{
        //    get
        //    {
        //        return _billReturn;
        //    }
        //    set
        //    {
        //        _billReturn = value;
        //        RaisePropertyChanged("BillReturn");
        //    }
        //}

        #endregion

        #region Constructor
        public CertificateListViewModel(INavigationService navigationService, IDialogService dialogService)
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

            OnMyCertificateClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.NavigateTo(App.MyCertificate);

            });
            #endregion

            #region Method
            #endregion
        }
    }
}
