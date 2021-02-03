using EGAZT.Models.EnumModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class PaymnetProcessWebviewViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        private bool _isLoading = false;
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

        #region Constructor

        public PaymnetProcessWebviewViewModel(INavigationService navigationService, IDialogService dialogService)
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
            

        }

       
        #endregion


       

    }
}
