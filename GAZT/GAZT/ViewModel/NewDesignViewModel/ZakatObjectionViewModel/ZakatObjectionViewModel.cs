using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.ZakatObjectionViewModel
{
    public class ZakatObjectionViewModel : BaseViewModel
    {
        #region Enums

        enum PagesEnum
        {
            ReviewReason,
            ReviewDetails,
            SecurityPayments,
            Declaration,
            Summary
        }

        public enum PickerEnum
        {
            ReviewReason,
            ReviewSubReason,
            ApplicationReferenceNumber,
            IDType,
        }

        #endregion

        #region Commands

        public ICommand ReviewReasonConBtnTapped { get; set; }
        public ICommand ReviewDetailsConBtnTapped { get; set; }
        public ICommand SecurityPaymentConBtnTapped { get; set; }
        public ICommand DeclarationConBtnTapped { get; set; }
        public ICommand SummaryConBtnTapped { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand GoBackClick { get; set; }
    
        #endregion
        
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        int selectedPage = (int) PagesEnum.ReviewReason;

        private bool _isBackVisible = false;

        public bool IsBackVisible
        {
            get { return _isBackVisible; }
            set
            {
                _isBackVisible = value;
                RaisePropertyChanged("IsBackVisible");
            }
        }

        private bool _isLoading = false;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        public ZakatObjectionViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            CloseClick = new Command(async () => { _navigationService.GoBack(); });

            GoBackClick = new Command(async () => { BackNavigations(); });

        }
        
        private void BackNavigations()
        {
            switch (selectedPage)
            {
                /*case (int) PagesEnum.ReviewDetails:
                    EnableReviewReasonView();
                    break;
                case (int) PagesEnum.SecurityPayments:
                    EnableReviewDetailsView();
                    break;
                case (int) PagesEnum.Declaration:
                    if (isSecurityPaymentsTabVisible)
                    {
                        EnableSecurityPaymentsView();    
                    }
                    else
                    {
                        EnableReviewDetailsView();
                    }
                    
                    break;
                case (int) PagesEnum.Summary:
                    EnableDeclarationView();
                    break;
                    */

            }
        }
    }
}