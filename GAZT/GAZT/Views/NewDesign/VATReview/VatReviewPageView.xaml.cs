using EGAZT.Models;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel;
using EGAZT.Views.NewDesign.GenericPickers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace EGAZT.Views.NewDesign.VatReview
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReviewPageView : ContentPage, VatReviewInterface
    {
        private VatReviewViewModel viewModel;

        public VatReviewPageView()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel = App.Locator.VatReviewView;
            this.BindingContext = viewModel;
            viewModel.vRInterface = this;

            viewModel.ResetData();

            viewModel.VatReviewReasonDropDownData();
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                //viewModel.PickerModel = arg;
                //viewModel.updatePicker();
                viewModel.updatePickerData(arg);
            });
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem",
                (sender, arg) =>
                {

                    viewModel.PickedDate = arg.SelectedValue;
                    viewModel.ValidateIdNumber();
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

        private void Security_Type_ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedITem = e.ItemData as VatReviewViewModel.SelectionModel;
            if (selectedITem.SelectionTitle.Equals(AppResources.VRSADAD))
            {
                viewModel.EnableSadadSecurityView();
            }
            else if (selectedITem.SelectionTitle.Equals(AppResources.VRBANKGURANTEE))
            {
                viewModel.EnablebankGuranteeSecurityView();
            }
        }

        private void Report_Details_UnFocused(object sender, FocusEventArgs e)
        {
            viewModel.ReportDetails = Report_Details_Tx.Text;
            viewModel.EnableReviewDetailsConButton();
        }

        private void ContactPersonTextUnFocus(object sender, FocusEventArgs e)
        {
            viewModel.ContactPersonName = ContactPersonEntry.Text;
            viewModel.EnableDeclarationConButton();
        }

        private void IdNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel._idNumber = e.NewTextValue;
        }

        private void OnIDNumberFocusChanged(object sender, FocusEventArgs e)
        {
            viewModel.ValidateIdNumber();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            viewModel.EnableSecurityPaymentsConButton();
        }

        private void DecCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            viewModel.EnableDeclarationConButton();
        }
        private void Dispute_Details_UnFocused(object sender, FocusEventArgs e)
        {
            viewModel.DisputeDetailsDesc = Dispute_Details_Tx.Text;
            viewModel.EnableReviewDetailsConButton();
        }
        private void RRAmountUnfocused(object sender, FocusEventArgs e)
        {
            viewModel.requestedReviewAmount = rrAmountTxt.Text;
            viewModel.EnableReviewDetailsConButton();
            viewModel.FetchSecurityAmount();

        }
        private void Dispute_Amount_ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedITem = e.ItemData as VatReviewViewModel.SelectionModel;
            if (selectedITem.SelectionTitle.Equals(AppResources.VRInfull))
            {
                viewModel.IsRRAmountEdit = false;
                viewModel.VRRequesttoReviewtheAmountValue = AppResources.VRInfull;
            }
            else if (selectedITem.SelectionTitle.Equals(AppResources.VRInpartial))
            {
                viewModel.IsRRAmountEdit = true;
                viewModel.FetchSecurityAmount();
                viewModel.VRRequesttoReviewtheAmountValue = AppResources.VRInpartial;
            }

        }

        public void SelectDefaultPaymentOption()
        {
            DisputeAmountListView.SelectedItem = viewModel.DisputeAmountPaymentOptions[0];
        }
    }

    public interface VatReviewInterface
    {
        void SelectDefaultPaymentOption();
    }
}