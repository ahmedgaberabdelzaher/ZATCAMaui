using EGAZT.Models.EnumModels;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{
    public class SignUpForEstablishmentPageViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #region Variable
        private EstablishmentSignUPTabEnum _currentTab = EstablishmentSignUPTabEnum.EstablishmentAccount;
        public EstablishmentSignUPTabEnum currentTab
        {
            get => _currentTab;
            private set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
            }
        }
        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                _currenrIndex = value;
                RaisePropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 4;
        #endregion

        #region Commands
        public ICommand OnNextButtonClick { get; private set; }
        public ICommand OnBackButtonClick { get; private set; }
        #endregion

        #region Propetry
        public string _PageTitle = "Establishment Account";
        public string PageTitle
        {
            get
            {
                return _PageTitle;
            }
            set
            {
                _PageTitle = value;
                RaisePropertyChanged("PageTitle");
            }
        }

        public string _BodyText = AppResources.ZZZZCompletethebelowdetails;
        public string BodyText
        {
            get
            {
                return _BodyText;
            }
            set
            {
                _BodyText = value;
                RaisePropertyChanged("BodyText");
            }
        }
        #endregion

        #region Constructor
        public SignUpForEstablishmentPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            OnNextButtonClick = new Command(() => navigateToNext());
            OnBackButtonClick = new Command(() => navigateBack());
        }
        #endregion

        #region Methods
        private void navigateToNext()
        {
            switch (currentTab)
            {
                case EstablishmentSignUPTabEnum.EstablishmentAccount:
                    PageTitle = AppResources.VerificationCode;
                    BodyText = AppResources.NDPleaseEnterVerificationSenttomobile;
                    currentTab = EstablishmentSignUPTabEnum.VerificationCode;
                    break;

                case EstablishmentSignUPTabEnum.VerificationCode:
                    PageTitle = AppResources.ZSummary;
                    BodyText = AppResources.VATRReviewInformation;

                    currentTab = EstablishmentSignUPTabEnum.Summary;
                    break;

                case EstablishmentSignUPTabEnum.Summary:
                    PageTitle = AppResources.Password;
                    BodyText = AppResources.CreateASecurePassword;
                    currentTab = EstablishmentSignUPTabEnum.Password;
                    break;
            }
        }


        private void navigateBack()
        {
            switch (currentTab)
            {
                case EstablishmentSignUPTabEnum.Password:
                    currentTab = EstablishmentSignUPTabEnum.Summary;
                    break;

                case EstablishmentSignUPTabEnum.Summary:
                    currentTab = EstablishmentSignUPTabEnum.VerificationCode;
                    break;

                case EstablishmentSignUPTabEnum.VerificationCode:
                    currentTab = EstablishmentSignUPTabEnum.EstablishmentAccount;
                    break;
            }
        }

        #endregion
    }
}