using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    public class IndividualRegistrationPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnContinueButtonClick { get; set; }
        public int currentStep { get; set; }



        #region Properties

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


        private bool _individualRegistrationView = true;
        public bool IndividualRegistrationView
        {
            get
            {
                return _individualRegistrationView;
            }
            set
            {
                _individualRegistrationView = value;
                RaisePropertyChanged("IndividualRegistrationView");
            }
        }

        private bool _nationalAddressView = false;
        public bool NationalAddressView
        {
            get
            {
                return _nationalAddressView;
            }
            set
            {
                _nationalAddressView = value;
                RaisePropertyChanged("NationalAddressView");
            }
        }

        private bool _contactInformationView = false;
        public bool ContactInformationView
        {
            get
            {
                return _contactInformationView;
            }
            set
            {
                _contactInformationView = value;
                RaisePropertyChanged("ContactInformationView");
            }
        }


        private bool _summeryView = false;
        public bool SummeryView
        {
            get
            {
                return _summeryView;
            }
            set
            {
                _summeryView = value;
                RaisePropertyChanged("SummeryView");
            }
        }

        private bool _passwordView = false;
        public bool PasswordView
        {
            get
            {
                return _passwordView;
            }
            set
            {
                _passwordView = value;
                RaisePropertyChanged("PasswordView");
            }
        }



        public Color _BoxColorOne;
        public Color BoxColorOne
        {
            get { return _BoxColorOne; }
            set
            {
                _BoxColorOne = value;
                RaisePropertyChanged("BoxColorOne");
            }
        }
        public Color _BoxColorTwo;
        public Color BoxColorTwo
        {
            get { return _BoxColorTwo; }
            set
            {
                _BoxColorTwo = value;
                RaisePropertyChanged("BoxColorTwo");
            }
        }
        public Color _BoxColorThree;
        public Color BoxColorThree
        {
            get { return _BoxColorThree; }
            set
            {
                _BoxColorThree = value;
                RaisePropertyChanged("BoxColorThree");
            }
        }

        public Color _BoxColorFour;
        public Color BoxColorFour
        {
            get { return _BoxColorFour; }
            set
            {
                _BoxColorFive = value;
                RaisePropertyChanged("BoxColorFour");
            }
        }

        public Color _BoxColorFive;
        public Color BoxColorFive
        {
            get { return _BoxColorFive; }
            set
            {
                _BoxColorFive = value;
                RaisePropertyChanged("BoxColorFive");
            }
        }

        public List<SignUpIdType> idTypeList;
        public List<SignUpIdType> IdTypeList
        {
            get
            {
                return _BoxColorFive;
            }
            set
            {
                idTypeList = value;
                RaisePropertyChanged("IdTypeList");
            }
        }
        
        #endregion

        #region Constructor
        public IndividualRegistrationPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            OnContinueButtonClick = new Xamarin.Forms.Command(async () =>
            {
                SetFormVisibility();
            });
        }
        #endregion

        #region Method
        public void ClearData()
        {

        }

        public async Task OnPageLoad()
        {
            currentStep = 1;
            GetSignUpIdType();
            string CaseId = await WebServiceManager.GAZTGetVATSignUpCaseId();// working
            string aaa = await WebServiceManager.GAZTVATSignUpValidateIDTypes("ZS0015", "1048089609", "19650224");
            var dd = await WebServiceManager.GAZTGetVATSignUpCityListForSignup();

            


        }

        public void SetFormVisibility()
        {
            try
            {
                if (currentStep == 1)
                {
                    IndividualRegistrationView = false;
                    NationalAddressView = true;
                    currentStep++;
                }
                else if (currentStep == 2)
                {
                    NationalAddressView = false;
                    ContactInformationView = true;
                    currentStep++;
                }
                else if (currentStep == 3)
                {
                    ContactInformationView = false;
                    SummeryView = true;

                    currentStep++;
                }
                else if (currentStep == 4)
                {
                    SummeryView = false;
                    PasswordView = true;
                    currentStep++;
                }
                else if (currentStep == 5)
                {
                    PasswordView = false;
                    currentStep = 1;
                    _navigationService.NavigateTo(App.RegistrationSuccessfulPageView);
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        public void GetSignUpIdType()
        {
            List<SignUpIdType> signUpIdTypeList = new List<SignUpIdType>{
           new SignUpIdType {ID = "ZS0015",Name = AppResources.NationaID},
                      new SignUpIdType {ID = "ZS0017",Name = AppResources.ZZIqamaID},
                                            new SignUpIdType {ID = "ZS0018",Name = AppResources.ZZGCCID},


            };

            List<SignUpIdType> list = new List<SignUpIdType>();
            list = signUpIdTypeList;

        }
    }
}
