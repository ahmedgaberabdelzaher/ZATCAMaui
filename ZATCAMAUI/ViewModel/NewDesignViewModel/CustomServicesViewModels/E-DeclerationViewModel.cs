using GalaSoft.MvvmLight.Views;
using System.Windows.Input;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Helper;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class E_DeclerationViewModel : BaseViewModel
    {
        string eFormWebViewUrl;
        public string EFormWebViewUrl { get { return eFormWebViewUrl; } set { eFormWebViewUrl = value; RaisePropertyChanged(); } }
        string eCheckWebViewUrl;
        public string ECheckWebViewUrl { get { return eCheckWebViewUrl; } set { eCheckWebViewUrl = value; RaisePropertyChanged(); } }
        public E_DeclerationViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
            EFormWebViewUrl = PageSettings.GetNewEDeclarationLinks();
            ECheckWebViewUrl = PageSettings.GetPreviousEDeclarationLink();
        }

        public async Task DownlOadFile(string url)
        {
            IsLoading = true;
            await Task.Delay(1000);
            DownloadFile downloadFile = new DownloadFile();
            var file = url.Split('.');
            var extntion = file[file.Length - 1];
            await downloadFile.DownloadFileAsync($"{url}", extntion, _dialogService);
            IsLoading = false;
        }

        public ICommand NavigateToNewEDeclerationCommand
        {
            get
            {
                return new Command(() =>
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("CreateE_Declaration", "OnCreateE_DeclarationCommand", "Create E_Declaration");
                    _navigationService.NavigateTo("CreateE_Declaration");
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand NavigateToPreviousEDeclerationCommand
        {
            get
            {
                return new Command(() =>
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ReiewPreviousDeclerations", "OnReiewPreviousDeclerationsCommand", "ReiewPreviousDeclerations");
                    _navigationService.NavigateTo("ReiewPreviousDeclerations");
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                });
            }
        }

    }
}
