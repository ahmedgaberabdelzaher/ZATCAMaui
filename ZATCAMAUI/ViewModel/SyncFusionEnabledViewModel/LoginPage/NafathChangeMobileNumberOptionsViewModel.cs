using System;
using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

public class NafathChangeMobileNumberOptionsViewModel : BaseViewModel
{
    public bool IndividualEnabled { get; set; }
    public bool CompanyEnabled { get; set; }
    public ICommand ChangeMobileNumberCommand { get; set; }
    public ICommand CloseCommand { get; set; }

    private string _errorMessage;
    public string ErrorMessage
    {
        get { return _errorMessage; }
        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    private bool _isError;
    public bool IsError
    {
        get { return _isError; }
        set
        {
            _isError = value;
            OnPropertyChanged(nameof(IsError));
        }
    }

    public NafathChangeMobileNumberOptionsViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
    {
        BindCommands();
    }

    private void BindCommands()
    {
        ChangeMobileNumberCommand = new Command(() => ChangeMobileNumberClicked());
        CloseCommand = new Command(async () =>
        {
            IndividualEnabled = false;
            CompanyEnabled = false;
            await MopupService.Instance.PopAsync();
        });
    }

    private async Task ChangeMobileNumberClicked()
    {
        if (!IndividualEnabled && !CompanyEnabled)
        {
            ErrorMessage = AppResources.PleaseSelectAnOption;
            IsError = true;
            return;
        }
        else
        {
            IsError = false;
            ErrorMessage = string.Empty;

            if (IndividualEnabled)
            {
                await MopupService.Instance.PopAsync();
                _navigationService.NavigateTo(App.NafathLoginView, ZATCAConstants.NAFATH_CHANGE_MOBILE_NUMBER);
            }
            else if (CompanyEnabled)
            {
                Dictionary<string, string> d = new Dictionary<string, string>();
                await MopupService.Instance.PopAsync();
                _navigationService.NavigateTo(App.ChangeMobileRequestPageView, d);
            }
            IndividualEnabled = false;
            CompanyEnabled = false;
        }
    }


}
