using System;
using EGAZT.AppConfigurations;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using System.Windows.Input;

namespace EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels
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
