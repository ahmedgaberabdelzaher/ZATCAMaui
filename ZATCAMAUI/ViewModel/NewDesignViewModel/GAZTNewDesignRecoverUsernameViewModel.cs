

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
                var _navigation = Application.Current.MainPage.Navigation;
                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.GAZTNewDesignForgotPasswordPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }
                navigationService.GoBack();
            });
        }
    }
}
