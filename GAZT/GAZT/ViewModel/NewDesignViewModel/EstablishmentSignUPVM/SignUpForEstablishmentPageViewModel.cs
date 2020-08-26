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
        private EstablishmentSignUPTabEnum _currentTab = EstablishmentSignUPTabEnum.TermsAndConditions;
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

        public void setCurrentTab()
        {
            currentTab = EstablishmentSignUPTabEnum.TermsAndConditions;
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
                else if (MarkComplete==true && _currenrIndex < MaxIndex)
                {
                    MarkComplete = false;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 5;
        #endregion

        #region Commands
        public ICommand OnNextButtonClick { get; private set; }
        public ICommand OnBackButtonClick { get; private set; }
        #endregion

        #region Propetry
        private bool _isDeclarationCheckEnabled = false;
        public bool IsDeclarationCheckEnabled
        {
            get
            {
                return _isDeclarationCheckEnabled;
            }
            set
            {
                _isDeclarationCheckEnabled = value;
                RaisePropertyChanged("IsDeclarationCheckEnabled");
            }
        }

        private bool _isMainButtonEnabled = false;
        public bool IsMainButtonEnabled
        {
            get
            {
                return _isMainButtonEnabled;
            }
            set
            {
                if (value == true)
                {
                }
                _isMainButtonEnabled = value;
                //OnStepButtonClicked.ChangeCanExecute();
                RaisePropertyChanged("IsMainButtonEnabled");
            }
        }



        public string _PageTitle = AppResources.ZVatTermsAndConditions;
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

        public string _BodyText;
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

        public string _NextBTN = AppResources.ZZProceedtoindividualSignup;
        public string NextBTN
        {
            get
            {
                return _NextBTN;
            }
            set
            {
                _NextBTN = value;
                RaisePropertyChanged("NextBTN");
            }
        }

        public string _ImgBackgroundYes = "re_Property_Tile_Background_White";
        public string ImgBackgroundYes
        {
            get
            {
                return _ImgBackgroundYes;
            }
            set
            {
                _ImgBackgroundYes = value;
                RaisePropertyChanged("ImgBackgroundYes");
            }
        }

        public string _ImgBackgroundNo = "re_Tile_Background";
        public string ImgBackgroundNo
        {
            get
            {
                return _ImgBackgroundNo;
            }
            set
            {
                _ImgBackgroundNo = value;
                RaisePropertyChanged("ImgBackgroundNo");
            }
        }

        public string _ImgBackgroundCRNubmer = "FP_selected_tile";
        public string ImgBackgroundCRNubmer
        {
            get
            {
                return _ImgBackgroundCRNubmer;
            }
            set
            {
                _ImgBackgroundCRNubmer = value;
                RaisePropertyChanged("ImgBackgroundCRNubmer");
            }
        }

        public string _ImgBackgroundLicenseNubmer = "FP_unselected_tile";
        public string ImgBackgroundLicenseNubmer
        {
            get
            {
                return _ImgBackgroundLicenseNubmer;
            }
            set
            {
                _ImgBackgroundLicenseNubmer = value;
                RaisePropertyChanged("ImgBackgroundLicenseNubmer");
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
                case EstablishmentSignUPTabEnum.TermsAndConditions:
                        PageTitle = AppResources.ZZZIndividualInformation;
                        BodyText = AppResources.ZZZZCompletethebelowdetails;
                        NextBTN = AppResources.ZZZZContinue;
                        currentTab = EstablishmentSignUPTabEnum.IndividualInformation;
                    break;

                case EstablishmentSignUPTabEnum.IndividualInformation:
                    PageTitle = AppResources.ZZZBusinessInformation;
                    BodyText = AppResources.ZZZZCompletethebelowdetails;
                    currentTab = EstablishmentSignUPTabEnum.BusinessInformation;
                    break;

                case EstablishmentSignUPTabEnum.BusinessInformation:
                    PageTitle = AppResources.ZZZContactInformation;
                    BodyText = AppResources.ZZZZCompletethebelowdetails;
                    
                    currentTab = EstablishmentSignUPTabEnum.ContactInformation;
                    break;

                case EstablishmentSignUPTabEnum.ContactInformation:
                    PageTitle = AppResources.VerificationCode;
                    BodyText = AppResources.ZZPleaseenteraccessCode;
                    NextBTN = AppResources.ZZZZContinue;
                    currentTab = EstablishmentSignUPTabEnum.EmailVerification;
                    break;

                //case EstablishmentSignUPTabEnum.EmailVerification:
                //    //PageTitle = "Mobile Verification";
                //    //BodyText = AppResources.ZZZZCompletethebelowdetails;
                //    NextBTN = AppResources.ZZZZContinue;
                //    currentTab = EstablishmentSignUPTabEnum.MobileVerification;
                //    break;

                //case EstablishmentSignUPTabEnum.MobileVerification:
                //    PageTitle = AppResources.Password;
                //    BodyText = AppResources.CreateASecurePassword;
                //    NextBTN = AppResources.Confirm;
                    
                //    currentTab = EstablishmentSignUPTabEnum.Password;
                //    break;
            }
        }

        
        private void navigateBack()
        {
            switch (currentTab)
            {
                //case EstablishmentSignUPTabEnum.Password:
                //    PageTitle = AppResources.VerificationCode;
                //    BodyText = AppResources.ZZPleaseenteraccessCode;
                //    NextBTN = AppResources.ZZZZContinue;
                //    currentTab = EstablishmentSignUPTabEnum.MobileVerification;
                //    break;

                //case EstablishmentSignUPTabEnum.MobileVerification:
                //    //PageTitle = "Email Verification";
                //    //BodyText = AppResources.ZZZZCompletethebelowdetails;
                //    //NextBTN = AppResources.Confirm;
                //    currentTab = EstablishmentSignUPTabEnum.EmailVerification;
                //    break;

                case EstablishmentSignUPTabEnum.EmailVerification:
                    PageTitle = AppResources.ZZZContactInformation;
                    BodyText = AppResources.ZZZZCompletethebelowdetails;
                    //NextBTN = AppResources.Confirm;
                    currentTab = EstablishmentSignUPTabEnum.ContactInformation;
                    break;

                case EstablishmentSignUPTabEnum.ContactInformation:
                    PageTitle = AppResources.ZZZBusinessInformation;
                    BodyText = AppResources.ZZZZCompletethebelowdetails;
                    //NextBTN = AppResources.ZZZZContinue;
                    currentTab = EstablishmentSignUPTabEnum.BusinessInformation;
                    break;

                case EstablishmentSignUPTabEnum.BusinessInformation:
                    PageTitle = AppResources.ZZZIndividualInformation;
                    BodyText = AppResources.ZZZZCompletethebelowdetails;
                    currentTab = EstablishmentSignUPTabEnum.IndividualInformation;
                    break;

                case EstablishmentSignUPTabEnum.IndividualInformation:
                    PageTitle = AppResources.ZVatTermsAndConditions;
                    BodyText = "";
                    NextBTN = AppResources.ZZProceedtoindividualSignup;
                    currentTab = EstablishmentSignUPTabEnum.TermsAndConditions;
                    break;
            }
        }

        #endregion
    }
}