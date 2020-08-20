using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.ViewModel.NewDesignViewModel
{
   public class RefundAccountPopupPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public RefundAccountPopupPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

        #region Properties
        private VATDeclaration _vATDeclarationDetails;
        public VATDeclaration VATDeclarationDetails
        {
            get
            {
                return _vATDeclarationDetails;
            }
            set
            {
                _vATDeclarationDetails = value;
                RaisePropertyChanged("VATDeclarationDetails");
            }
        }
        #endregion

    }
}
