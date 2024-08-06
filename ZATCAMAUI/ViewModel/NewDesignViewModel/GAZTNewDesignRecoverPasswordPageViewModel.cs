

using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class GAZTNewDesignRecoverPasswordPageViewModel : BaseViewModel
    {
        public Command OnLoginButtonClicked { get; set; }
        public GAZTNewDesignRecoverPasswordPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}
