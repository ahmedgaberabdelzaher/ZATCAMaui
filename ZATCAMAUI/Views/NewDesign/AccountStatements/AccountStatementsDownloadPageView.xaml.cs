using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.AccountStatements;
using ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

namespace ZATCAMAUI.Views.NewDesign.AccountStatements
{

    public partial class AccountStatementsDownloadPageView : ContentPage
    {
        AccountStatementsPageViewModel viewModel;
        public AccountStatementsDownloadPageView(DataForDownloadPage Data)
        {
            InitializeComponent();
            viewModel = App.Locator.AccountStatementsPageView;
            BindingContext = viewModel;

            viewModel.GroupedDataForDownload = Data.GroupedDataForDownload;
            viewModel.ASTaxpayerSelectedValues = new ASTaxpayerSelectedValues();
            viewModel.ASTaxpayerSelectedValues = Data.ASTaxpayerSelectedValues;
            viewModel.IsNormalListDownloadPage = Data.isNormalList;
            viewModel.StatementsLineItemsDownloadPage = Data.StatementsLineItems;

            viewModel.FromDate = AppResources.ASAccountStatementFrom;
            viewModel.ToDate = AppResources.ASAccountStatementTo;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (sender, arg) =>
            {
                if (App.IsArabic)
                {
                    if (arg.PickerId == "StartDateTypePicker")
                    {
                        viewModel.FromDate = Convert.ToDateTime(arg.SelectedValue).ToShortDateString();
                        viewModel.FromDateDownloadPage = Convert.ToDateTime(arg.SelectedValue);
                    }
                    else if (arg.PickerId == "EndDateTypePicker")
                    {
                        viewModel.ToDate = Convert.ToDateTime(arg.SelectedValue).ToShortDateString();
                        viewModel.ToDateDownloadPage = Convert.ToDateTime(arg.SelectedValue);
                    }
                }
                else
                {
                    if (arg.DatePickerTitle.Contains(AppResources.ASAccountStatementFrom))
                    {
                        viewModel.FromDate = Convert.ToDateTime(arg.SelectedValue).ToShortDateString();
                    }
                    else if (arg.DatePickerTitle.Contains(AppResources.ASAccountStatementTo))
                    {
                        viewModel.ToDate = Convert.ToDateTime(arg.SelectedValue).ToShortDateString();

                    }
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
        }





        async void StartDateClicked(object sender, EventArgs e)
        {

            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.ASAccountStatementFrom;
            genericDatePickerModel.PickerId = "StartDateTypePicker";
            try
            {
                await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel, true));
            }
            catch (GAZTUnlockAccountException)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    viewModel._navigationService.GoBack();
                });
            }


        }
        async void EndDateClicked(object sender, EventArgs e)
        {

            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.ASAccountStatementTo;
            genericDatePickerModel.PickerId = "EndDateTypePicker";
            try
            {
                await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel, true));
            }
            catch (GAZTUnlockAccountException)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    viewModel._navigationService.GoBack();
                });
            }
        }
    }
}
