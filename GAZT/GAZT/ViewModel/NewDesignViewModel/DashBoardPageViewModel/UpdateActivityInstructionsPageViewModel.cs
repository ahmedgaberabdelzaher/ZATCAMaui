using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using System.Windows.Input;
using GAZT.Manager;
using EGAZT.Models;

namespace EGAZT.ViewModel.NewDesignViewModel.DashBoardPageViewModel
{
    public class UpdateActivityInstructionsPageViewModel : ViewModelBase
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
                RaisePropertyChanged("ContinueButtonnBackroundColor");
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

                RaisePropertyChanged("IsInstructionChecked");
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
                RaisePropertyChanged("responseMessage");

               
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
                RaisePropertyChanged("IsContinueButtonEnabled");
            }
        }
        private bool _isInstructionCheckedEnable = true;
        public bool isInstructionCheckedEnable
        {
            get => _isInstructionCheckedEnable;
            set
            {
                _isInstructionCheckedEnable = value;
                RaisePropertyChanged(nameof(isInstructionCheckedEnable));
            }
        }
        public ICommand UpdateActivityClicked { get; set; }

        public UpdateActivityInstructionsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            IsContinueButtonEnable = false;
            UpdateActivityClicked = new Command(this.AcceptClicked);
        }

        public async void AcceptClicked()
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

                    DashBoardUpdateViewResponseModel responsne =await WebServiceManager.getTaxPayerActivityUpdateStatusAfterTermsChecked(activityUpdateModel);
                    await PopupNavigation.Instance.PopAsync();
                   // _navigationService.NavigateTo(App.VATDeregistrationDetailsPage);
                }
                catch (GAZTUnlockAccountException ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

