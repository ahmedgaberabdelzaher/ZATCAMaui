using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.ContractRelease;
using EGAZT.Views.NewDesign.GenericPickers;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ContractReleasePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractReleasePageView : ContentPage
    {
        ContractReleaseViewModel viewModel;

        public ContractReleasePageView()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

            //App.IsArabic = false;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel = App.Locator.ContractReleasePageView;
            this.BindingContext = viewModel;

            Task.Run(async () =>
            {
                viewModel.IsLoading = true;
                await GetContractReleaseData();

            });
            viewModel.ResetData();
            viewModel.showInstructionDialog();
        }

        public async Task GetContractReleaseData()
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    await viewModel.OnPageLoad();

                });
                //await Task.Run(() =>
                //{
                //    viewModel.IsLoading = false;
                //});
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem",
                (sender, arg) =>
                {
                    if (viewModel.fromDatePicker)
                    {
                        viewModel.FromDate = Convert.ToDateTime(arg.SelectedValue);
                    }
                    else
                    {
                        viewModel.ToDate = Convert.ToDateTime(arg.SelectedValue);
                    }
                });

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                viewModel.PickerModel = arg;
                viewModel.updatePicker();
                Console.WriteLine(arg);
                OnAppearing();
            });

            Xamarin.Forms.MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
            {
                if (arg != null)
                {
                    viewModel.PopulateAttachments(arg.results);
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            MessagingCenter.Unsubscribe<object, Attachments>(this, "AttachmentReceived");

        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        private void HandleAmountReleaseTextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                viewModel._amountToRelease = Double.Parse(e.NewTextValue);


            }
            catch (Exception ex)
            {

            }

            viewModel.MakeCalculations();
        }

        private void HandleTotalAmount(object sender, TextChangedEventArgs e)
        {


        }

        private void ContractAttach_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {

        }

        private void invoiceAttachments_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {

        }

        private void RemarksTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.Remarks = e.NewTextValue;
        }

        private void DetailDescriptionTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.DetailDescription = e.NewTextValue;
        }

        private void ContactPersonNameTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.ContactPersonName = e.NewTextValue;
            viewModel.EnableDeclarationContinue();
        }

        private void DesignationTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.Designation = e.NewTextValue;
            viewModel.EnableDeclarationContinue();
        }

        private void ContractNameTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.ContractName = e.NewTextValue;
        }

        private void ContractNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.ContractNumber = e.NewTextValue;
        }

        private void ContractNameUnfocused(object sender, FocusEventArgs e)
        {
            viewModel.ContractName = ContractNameText.Text;
        }

        private void ContractNumberUnfocused(object sender, FocusEventArgs e)
        {
            viewModel.ContractNumber = ContractNumberText.Text;
        }

        private void TotalAmountUnFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.ContractTotalAmount = Double.Parse(ContractTotalAmountText.Text);
                viewModel.MakeCalculations();
            }
            catch (Exception ex)
            {

            }

        }

        private void AmountToReleaseUnfocused(object sender, FocusEventArgs e)
        {
            try
            {

                if (viewModel.ContractTotalAmount < Double.Parse(AmountoReleaseTxt.Text))
                {
                    viewModel._dialogService.ShowMessageBox(AppResources.CRTotalAmountRequirdtoReleasemustbelesstotalamountofcontract, AppResources.Information);
                    //return;
                }
                viewModel.AmountToRelease = Double.Parse(AmountoReleaseTxt.Text);
                viewModel.MakeCalculations();
            }
            catch (Exception ex)
            {

            }

        }
    }
}