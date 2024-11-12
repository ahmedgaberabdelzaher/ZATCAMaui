using Mopups.Services;
using Syncfusion.Maui.ListView;
using ZATCAMAUI.ViewModel.NewDesignViewModel.IBanAccManagementsViewModel;
using ZATCAMAUI.Views.NewDesign.Common;
using static ZATCAMAUI.Models.IBanManagementListModel;

namespace ZATCAMAUI.Views.NewDesign.IBanAccountsManagementPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BankAccountManagementPageView : ContentPage
    {
        private BankAccountManagementPageViewModel _viewModel;
        private IbanListSetResult selectedItem;
        public BankAccountManagementPageView(bool navigation = false)
        {
            InitializeComponent();
            _viewModel = App.Locator.BankAccountManagementPageView;
            this.BindingContext = _viewModel;
            _viewModel.isRemove = navigation;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //Check for Large Tax payer or not
            await _viewModel.LoadAllIBanAccounts();

            App.SelectedIBAN = string.Empty;




            MessagingCenter.Subscribe<object, string>(this, "SaveCommandReceived", async (sender, arg) =>
            {
                await MopupService.Instance.PopAsync();
                string message = arg;
                if (message.Equals("DEACTIVATE"))
                {
                    await _viewModel.SummaryConButtonClickedAsync(selectedItem, "D");
                }
                else if (message.Equals("ACTIVATE"))
                {
                    await _viewModel.SummaryConButtonClickedAsync(selectedItem, "A");
                }
                else if (message.Equals("Update"))
                {
                  await  _viewModel._navigationService.NavigateTo(App.GAZTBankAccountAddOrUpdatePageView, _viewModel.IBANAccountData);
                }


            });
        }



        protected override void OnDisappearing()
        {
            base.OnDisappearing();


            MessagingCenter.Unsubscribe<object, string>(this, "SaveCommandReceived");

        }



        
        private async void StatusViewButtons_Tapped(object sender, EventArgs e)
        {
            StackLayout chipView = sender as StackLayout;
            IbanListSetResult IBanSetResultModel = new IbanListSetResult();
            IBanSetResultModel = (IbanListSetResult)chipView.BindingContext;

            App.SelectedIBAN = IBanSetResultModel.Fbnum;
            App.SelectedFbGuid = IBanSetResultModel.FormGuid;

            if (IBanSetResultModel != null && !string.IsNullOrEmpty(IBanSetResultModel.StatusText))
            {
                if (IBanSetResultModel.StatusText.Equals(AppResources.IBanDeactivate))
                {
                    await _viewModel.SummaryConButtonClickedAsync(IBanSetResultModel, "D", 1);
                }
                else if (IBanSetResultModel.StatusText.Equals(AppResources.IBanActivate))
                {
                    await _viewModel.SummaryConButtonClickedAsync(IBanSetResultModel, "A", 1);
                }
                else if (IBanSetResultModel.StatusText.Equals(AppResources.IBANUpdate))
                {
                    _viewModel.IBANAccountData.d.isUpdateFlag = true;
                  await  _viewModel._navigationService.NavigateTo(App.GAZTBankAccountAddOrUpdatePageView, _viewModel.IBANAccountData);
                }
            }

        }
    }
}
