using System.Windows.Input;

using Mopups.Services;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.Instructions
{

    public class InstructionsBottomPopUpViewModel : BaseViewModel
    {
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
                OnPropertyChanged("Description");
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
                OnPropertyChanged("IsInstuctionsChecked");
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
                OnPropertyChanged("IsTermsChecked");
            }
        }

        private bool _isCheckboxEditable = true;
        public bool IsCheckboxEditable
        {
            get
            {
                return _isCheckboxEditable;
            }
            set
            {
                _isCheckboxEditable = value;
                OnPropertyChanged("IsCheckboxEditable");
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
                OnPropertyChanged("IsInstructions");
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
                OnPropertyChanged("IsTerms");
            }
        }

        private bool _isCancelButtonVisible = false;
        public bool IsCancelButtonVisible
        {
            get
            {
                return _isCancelButtonVisible;
            }
            set
            {
                _isCancelButtonVisible = value;
                OnPropertyChanged("IsCancelButtonVisible");
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
                OnPropertyChanged("CheckBoxDescription");
            }
        }

        private string _buttonTitle = string.Empty;
        public string ButtonTitle
        {
            get
            {
                return _buttonTitle;
            }
            set
            {
                _buttonTitle = value;
                OnPropertyChanged("ButtonTitle");
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
                ContinueBackGroundColor = _isContinueEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"];

                OnPropertyChanged("IsBillContinueEnabled");
            }
        }
        private Color _continueBackGroundColor = (Color)Application.Current.Resources["Secondary"];
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
                OnPropertyChanged("ContinueBackGroundColor");
            }
        }



        public ICommand Close_Tapped { get; set; }

        public ICommand TermsContinueClick { get; set; }

        public ICommand InstructionsContinueClick { get; set; }

        public InstructionsBottomPopUpViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            TermsContinueClick = new Command(async () =>
            {
                if (_isTermsChecked)
                {
                    InstructionsContinue();
                    await MopupService.Instance.PopAsync();
                }
                else
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.BPTermsAndConditionsAlert));

                }
            });

            InstructionsContinueClick = new Command(async () =>
            {


                if (_isInstuctionsChecked)
                {
                    await MopupService.Instance.PopAsync();
                    InstructionsContinue();
                }
                else
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.BPInstructionsAndConditionsAlert));
                }
            });

            Close_Tapped = new Command(async () =>
            {
                await MopupService.Instance.PopAsync();
            });


            void InstructionsContinue()
            {

                if (IsInstructions)
                {
                    if (IsInstuctionsChecked)
                    {
                        MessagingCenter.Send<object, bool>(this, "InstructionsContinue", true);
                    }

                    else
                    {
                        MessagingCenter.Send<object, bool>(this, "InstructionsContinue", false);
                    }
                    if (CheckBoxDescription.Equals(AppResources.VatTermsCheckBoxDesc))
                    {
                        MessagingCenter.Send<object, bool>(this, "TermsContinueSecond", true);
                    }
                }
                else if (IsTerms)
                {
                    if (IsTermsChecked)
                    {
                        MessagingCenter.Send<object, bool>(this, "TermsContinue", true);

                        if (CheckBoxDescription.Equals(AppResources.VatTermsCheckBoxDesc))
                        {
                            MessagingCenter.Send<object, bool>(this, "TermsContinueSecond", true);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<object, bool>(this, "TermsContinue", false);
                    }
                }

            }

        }
    }
}