using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeNumber
{
    public class NafathChangeMobileNumberSuccessViewModel : BaseViewModel
    {
        public ICommand GotoLoginCommand { get; set; }
        public NafathChangeMobileNumberSuccessViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GotoLoginCommand = new Command(async () => await GotoLogin());
        }

        private async Task GotoLogin()
        {
           await navigateLogin();
        }
    }

}

