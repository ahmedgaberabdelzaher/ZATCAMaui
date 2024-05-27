using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
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
        public BankAccountManagementPageView()
        {
            InitializeComponent();
            ChangeAeroIcon();

            SetLTR();

            _viewModel = App.Locator.BankAccountManagementPageView;
            On<iOS>().SetUseSafeArea(true);
            this.BindingContext = _viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
            //Check for Large Tax payer or not
            await _viewModel.LoadAllIBanAccounts();

            App.SelectedIBAN = string.Empty;


           

            MessagingCenter.Subscribe<object, string>(this, "SaveCommandReceived", async (sender, arg) =>
            {
                await PopupNavigation.Instance.PopAsync();
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
                    _viewModel._navigationService.NavigateTo(App.GAZTBankAccountAddOrUpdatePageView, _viewModel.IBANAccountData);
                }


            });
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void ChangeAeroIcon()
        {
            try
            {
                if (App.IsArabic)
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                }
                else
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                }

            }
            catch (Exception)
            {

            }

        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();


            MessagingCenter.Unsubscribe<object, string>(this, "SaveCommandReceived");

        }



        private async void IBanAccountsListItemTapped(object sender, ItemSelectionChangedEventArgs e)
        {
            var textToDisplayInButton = string.Empty;
            var selectedLv = sender as SfListView;
            selectedItem = (IbanListSetResult)selectedLv.SelectedItem;
            App.SelectedIBAN = selectedItem.Fbnum;
            //await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp("" + selectedItem.Bkext));
            if (selectedItem.VisibleUpdate != null)
            {
                if (selectedItem.VisibleUpdate == "")
                {
                    // Display ActionSheet Radio buttons 
                    if (selectedItem.ActiveIban == "X")
                    {
                        textToDisplayInButton = "Deactivate";
                    }
                    else if (selectedItem.ActiveIban == "")
                    {
                        textToDisplayInButton = "Activate";
                    }
                }
                else
                {
                    //IsEnabled Update or disble update button
                    if (selectedItem.EnableUpdate == "X")
                    {
                        textToDisplayInButton = "Update";
                    }
                    else if (selectedItem.EnableUpdate == "")
                    {
                        textToDisplayInButton = "UpdateDisabled";
                    }
                }
                if (textToDisplayInButton != "UpdateDisabled")
                {
                    var listOfActionButtonsApplicable = new List<string>();
                    listOfActionButtonsApplicable.Add(textToDisplayInButton);
                    await PopupNavigation.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(listOfActionButtonsApplicable));

                }

            }
            var view = sender as SfListView;
            view.SelectedItem = null;
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
                    _viewModel._navigationService.NavigateTo(App.GAZTBankAccountAddOrUpdatePageView, _viewModel.IBANAccountData);
                }
            }

        }
    }
}
