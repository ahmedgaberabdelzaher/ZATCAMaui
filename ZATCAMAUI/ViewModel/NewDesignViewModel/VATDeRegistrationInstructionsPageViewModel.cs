using System.Windows.Input;


using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{
    public class VATDeRegistrationInstructionsPageViewModel : BaseViewModel
    {
        #region Variable
        public ICommand GoBackClick { get; set; }
        #endregion
        private Color _continueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
        public Color ContinueButtonnBackroundColor
        {
            get
            {
                return _continueButtonnBackroundColor;
            }
            set
            {
                if (_continueButtonnBackroundColor == value) return;
                _continueButtonnBackroundColor = value;
                OnPropertyChanged("ContinueButtonnBackroundColor");
            }
        }
        private bool _isInstructionChecked = false;
        public bool IsInstructionChecked
        {
            get
            {
                return _isInstructionChecked;
            }
            set
            {
                _isInstructionChecked = value;

                if (_isInstructionChecked)
                {
                    IsContinueButtonEnable = true;
                }
                else
                {
                    IsContinueButtonEnable = false;

                }

                OnPropertyChanged("IsInstructionChecked");
            }
        }
        private bool _isContinueButtonEnable = false;
        public bool IsContinueButtonEnable
        {
            get
            {
                return _isContinueButtonEnable;
            }
            set
            {
                _isContinueButtonEnable = value;
                if (_isContinueButtonEnable)
                {
                    ContinueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
                }
                else
                {
                    ContinueButtonnBackroundColor = (Color)Application.Current.Resources["ButtonGray"];
                }
                OnPropertyChanged("IsContinueButtonEnabled");
            }
        }
        private bool _isInstructionCheckedEnable = true;
        public bool isInstructionCheckedEnable
        {
            get => _isInstructionCheckedEnable;
            set
            {
                _isInstructionCheckedEnable = value;
                OnPropertyChanged(nameof(isInstructionCheckedEnable));
            }
        }
        public ICommand VATDeregistrationClicked { get; set; }

        public VATDeRegistrationInstructionsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            IsContinueButtonEnable = false;
            VATDeregistrationClicked = new Command(VATDeregistrationTapped);
        }

        public async void VATDeregistrationTapped()
        {
            if (_isInstructionChecked)
            {
                IsContinueButtonEnable = true;

                MessagingCenter.Send(this, "SelectedCheckboxItem", IsInstructionChecked);
                try
                {
                    await MopupService.Instance.PopAsync();
                    _navigationService.NavigateTo(App.VATDeregistrationDetailsPage);
                }
                catch (GAZTUnlockAccountException)
                {
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
