using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel
{
    public class InstructionsBottomPopUpViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public enum DialogType
        {
            Instructions,
            TermsConditions
        }

        private string _description = "";
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        private bool _isInstuctionsChecked = false;
        public bool IsInstuctionsChecked
        {
            get
            {
                return _isInstuctionsChecked;
            }
            set
            {
                _isInstuctionsChecked = value;
                RaisePropertyChanged("IsInstuctionsChecked");
            }
        }

        private bool _isTermsChecked = false;
        public bool IsTermsChecked
        {
            get
            {
                return _isTermsChecked;
            }
            set
            {
                _isTermsChecked = value;
                RaisePropertyChanged("IsTermsChecked");
            }
        }

        private bool _isInstructions = false;
        public bool IsInstructions
        {
            get
            {
                return _isInstructions;
            }
            set
            {
                _isInstructions = value;
                RaisePropertyChanged("IsInstructions");
            }
        }

        private bool _isTerms = false;
        public bool IsTerms
        {
            get
            {
                return _isTerms;
            }
            set
            {
                _isTerms = value;
                RaisePropertyChanged("IsTerms");
            }
        }

        private string _checkBoxDescription = "";
        public string CheckBoxDescription
        {
            get
            {
                return _checkBoxDescription;
            }
            set
            {
                _checkBoxDescription = value;
                RaisePropertyChanged("CheckBoxDescription");
            }
        }

        private string _buttonTitle = "";
        public string ButtonTitle
        {
            get
            {
                return _buttonTitle;
            }
            set
            {
                _buttonTitle = value;
                RaisePropertyChanged("ButtonTitle");
            }
        }


        public void EnableCheckboxContinue()
        {
            if (IsInstuctionsChecked || IsTermsChecked)
            {
                IsContinueEnabled = true;
            }
            else
            {
                IsContinueEnabled = false;
            }
        }

        private bool _isContinueEnabled = false;
        public bool IsContinueEnabled
        {
            get { return _isContinueEnabled; }
            set
            {
                _isContinueEnabled = value;
                ContinueBackGroundColor = Color.FromHex(_isContinueEnabled ? "#d49504" : "#9EA4A9");

                RaisePropertyChanged("IsBillContinueEnabled");
            }
        }
        private Color _continueBackGroundColor = Color.FromHex("#d49504");
        public Color ContinueBackGroundColor
        {
            get
            {
                return _continueBackGroundColor;
            }
            set
            {
                if (_continueBackGroundColor == value)
                {
                    return;
                }
                _continueBackGroundColor = value;
                RaisePropertyChanged("ContinueBackGroundColor");
            }
        }



        public ICommand Close_Tapped { get; set; }

        public ICommand TermsContinueClick { get; set; }

        public ICommand InstructionsContinueClick { get; set; }

        public InstructionsBottomPopUpViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            TermsContinueClick = new Command(async () =>
            {


                if (_isTermsChecked)
                {
                    InstructionsContinue();
                    await PopupNavigation.Instance.PopAsync();
                }
                else
                {

                    await _dialogService.ShowMessage(AppResources.BPTermsAndConditionsAlert, AppResources.Information);
                }
            });

            InstructionsContinueClick = new Command(async () =>
            {


                if (_isInstuctionsChecked)
                {
                    InstructionsContinue();
                    await PopupNavigation.Instance.PopAsync();
                }
                else
                {

                    await _dialogService.ShowMessage(AppResources.BPInstructionsAndConditionsAlert, "Alert");
                }
            });

            Close_Tapped = new Command(async () =>
            {
                await PopupNavigation.Instance.PopAsync();
            });


            void InstructionsContinue()
            {

                if (IsInstructions)
                {
                    if (IsInstuctionsChecked)
                    {
                        MessagingCenter.Send<Object, Boolean>(this, "InstructionsContinue", true);
                    }

                    else
                    {
                        MessagingCenter.Send<Object, Boolean>(this, "InstructionsContinue", false);
                    }
                    if (CheckBoxDescription.Equals(AppResources.VatTermsCheckBoxDesc))
                    {
                        MessagingCenter.Send<Object, Boolean>(this, "TermsContinueSecond", true);
                    }
                }
                else if (IsTerms)
                {
                    if (IsTermsChecked)
                    {
                        MessagingCenter.Send<Object, Boolean>(this, "TermsContinue", true);

                        if (CheckBoxDescription.Equals(AppResources.VatTermsCheckBoxDesc))
                        {
                            MessagingCenter.Send<Object, Boolean>(this, "TermsContinueSecond", true);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Object, Boolean>(this, "TermsContinue", false);
                    }
                }

            }

        }
    }
}