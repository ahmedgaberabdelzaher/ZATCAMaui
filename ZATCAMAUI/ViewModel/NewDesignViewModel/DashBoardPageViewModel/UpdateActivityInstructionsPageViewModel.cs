using System.Windows.Input;

using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.DashBoardPageViewModel
{
    
    public class UpdateActivityInstructionsPageViewModel : BaseViewModel
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
        #endregion
        private Color _continueButtonnBackroundColor = Color.FromHex("#d49504");
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

        string _responseMessage = string.Empty;
        public string responseMessage
        {
            get
            {
                return _responseMessage;
            }
            set
            {
                if (_responseMessage == value) return;
                _responseMessage = value;
                OnPropertyChanged("responseMessage");

               
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
                    ContinueButtonnBackroundColor = Color.FromHex("#d49504");
                }
                else
                {
                    ContinueButtonnBackroundColor = Color.FromHex("#9EA4A9");
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
        public ICommand UpdateActivityClicked { get; set; }

        public UpdateActivityInstructionsPageViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService, dialogService)
        {
            
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            IsContinueButtonEnable = false;
            UpdateActivityClicked = new Command(async () => await AcceptClicked());
        }

        public async Task AcceptClicked()
        {
            if (_isInstructionChecked)
            {
                IsContinueButtonEnable = true;

                MessagingCenter.Send<UpdateActivityInstructionsPageViewModel, bool>(this, "SelectedActivityUpdateCheckbox", IsInstructionChecked);
                try
                {

                    TPUpdateActivityModel activityUpdateModel = new TPUpdateActivityModel();
                    activityUpdateModel.Taxpayer = App.LoginDataRetrieved.TIN;
                    activityUpdateModel.Flag = true;

                    IsLoading = true;
                    DashBoardUpdateResponseModel responsne =await WebServiceManager.getTaxPayerActivityUpdateStatusAfterTermsChecked(activityUpdateModel);
                    IsLoading = false;
                    await MopupService.Instance.PopAsync();
                }
                catch (GAZTVATRegistrationInProcessException exs)
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(exs.Message, AppResources.Information);
                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                }
            }
        }
    }
}

