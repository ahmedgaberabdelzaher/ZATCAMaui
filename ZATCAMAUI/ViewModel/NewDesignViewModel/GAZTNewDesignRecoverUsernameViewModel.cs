

using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class GAZTNewDesignRecoverUsernameViewModel : BaseViewModel
    {
        public Command OnLoginButtonClicked { get; set; }
        public GAZTNewDesignRecoverUsernameViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {


            OnLoginButtonClicked = new Command(async () =>
            {

                await Application.Current.MainPage.Navigation.PopModalAsync(true);
            });
        }
    }
}
