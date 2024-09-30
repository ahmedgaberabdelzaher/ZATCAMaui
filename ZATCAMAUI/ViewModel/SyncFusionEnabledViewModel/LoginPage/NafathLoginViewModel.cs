using System;
using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

public class NafathLoginViewModel : BaseViewModel
{

    private string _nafathId;
    public string navigation;

    public string NafathId
    {
        get { return _nafathId; }
        set { _nafathId = value; }
    }

    private string _error;
    public string Error
    {
        get { return _error; }
        set
        {
            _error = value;
            OnPropertyChanged(nameof(Error));
        }
    }

    public LocationServicesModel LocationData { get; set; } = new LocationServicesModel();

    public ICommand LoginCommand { get; set; }
    public NafathLoginViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
    {
        LoginCommand = new Command(async () => await LoginClicked());
    }

    private async Task LoginClicked()
    {
        try
        {
            IsLoading = true;
            if (validateId(NafathId))
            {
                var result = await Login();
                if (result != null && result.result != null)
                {
                    if (!string.IsNullOrEmpty(result.result.randomNumber))
                    {
                        result.navigation = navigation;
                        _navigationService.NavigateTo(App.NafathAuthenticationView, result);

                        var _navigation = Application.Current.MainPage.Navigation;

                        foreach (var item in _navigation.NavigationStack)
                        {
                            if (item.GetType().Name == App.NafathLoginView)
                            {
                                _navigation.RemovePage(item);
                                break;
                            }
                        }

                    }
                    else
                    {
                        await _dialogService.ShowMessage(result.result.statusDescription, AppResources.ZZSomethingwentwrong);
                    }
                }
                else
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                }
            }
            IsLoading = false;
        }
        catch (Exception ex)
        {
            IsLoading = false;
        }
    }

    bool validateId(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            Error = AppResources.PleaseEnterYourNationalId;
            return false;
        }
        else if (id.Length != 10)
        {
            Error = AppResources.ZZNationalIDlengthis10digit;
            return false;
        }
        else if (!id.StartsWith("1"))
        {
            Error = AppResources.ZZNationalIDstartswith1;
            return false;
        }
        Error = string.Empty;
        return true;
    }


    private async Task<NafathLoginResponseModel> Login()
    {
        NafathLoginRequestModel model = new NafathLoginRequestModel()
        {
            Idnumber = NafathId,
            Inpchz = App.IncomingChannel,
            Langz = WebServiceManager.GetLangZParameterAREN(),
            processType = navigation
        };

        var result = await WebServiceManager.NafathLogin(model);
        result.result.idNumber = NafathId;
        return result;
    }
}
