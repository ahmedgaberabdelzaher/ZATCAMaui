using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;

namespace EGAZT
{
    public class VATDeclarationAttachmentPageViewModel : ViewModelBase
    {
        #region Variables
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnInvoiceClicked { get; set; }
        #endregion

        #region Property
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

        #endregion

        #region Constructor
        public VATDeclarationAttachmentPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            //=======================start==================================================
            //OnInvoiceClicked = new Xamarin.Forms.Command(() =>
            //{
            //    OnDownLoadInvoiceClicked();
            //});
        }
        #endregion

        #region Method
        #endregion

    }
}
