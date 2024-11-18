using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ItemTappedEventArgs = Syncfusion.Maui.ListView.ItemTappedEventArgs;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReviewPageView : ContentPage
    {
        private VatReviewViewModel viewModel;

        public VatReviewPageView()
        {
            try
            {
                InitializeComponent();

                viewModel = App.Locator.VatReviewView;
                this.BindingContext = viewModel;


            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert(ex.Message, ex.StackTrace, "cancel");
            }


        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            MessagingCenter.Unsubscribe<object, AttachmentsList>(this, "AttachmentReceived");

            MessagingCenter.Unsubscribe<object, int>(this, "draftRequest");
            MessagingCenter.Unsubscribe<object, int>(this, "draftSecurity");

            MessagingCenter.Unsubscribe<object, string>(this, "SaveCommandReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "YesReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "NoReceived");

        }

        private void Report_Details_Tx_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                viewModel.charCountReportDetails = Report_Details_Tx.Text.Length + "/" + 1000;
                viewModel.ReportDetails = Report_Details_Tx.Text;
                viewModel.EnableReportDetailsConButton();
            }
            catch (Exception)
            {


            }
        }



        private void SADAD_CheckBox_CheckedChanged(object sender, Boolean e)
        {
            viewModel.EnableSecurityPaymentsConButton();

            if (!string.IsNullOrEmpty(viewModel.SADADNumber))
            {
                return;
            }

            if (e)
            {
                viewModel.ShowSadadGenerateButton();
            }
            else
            {
                viewModel.HideSadadGenerateButton();
            }
        }
        private void Dispute_Details_Tx_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.charCountDisputeDetails = Dispute_Details_Tx.Text.Length + "/" + 1000;
        }


        private void Security_Type_ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedITem = e.DataItem as VatReviewViewModel.SelectionModel;
                if (selectedITem.SelectionTitle.Equals(AppResources.VRSADAD))
                {
                    viewModel.EnableSadadSecurityView();
                }
                else if (selectedITem.SelectionTitle.Equals(AppResources.VRBANKGURANTEE))
                {
                    viewModel.EnablebankGuranteeSecurityView();

                }

                viewModel.EnableSecurityPaymentsConButton();
            }
            catch (Exception)
            {


            }
        }

        private void Report_Details_UnFocused(object sender, FocusEventArgs e)
        {
            viewModel.ReportDetails = Report_Details_Tx.Text;
            viewModel.EnableReportDetailsConButton();
        }
        private void LateFiling_Details_UnFocused(object sender, FocusEventArgs e)
        {
            viewModel.LateFlngDetails = LateFiling_Details_Txx.Text;
            viewModel.EnableLateFilingsDetailsConButton();
        }


        private void ContactPersonTextUnFocus(object sender, FocusEventArgs e)
        {
            viewModel.ContactPersonName = ContactPersonEntry.Text;
            viewModel.EnableDeclarationConButton();
        }

        private void IdNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                viewModel._idNumber = e.NewTextValue;
            }
            catch (Exception)
            {


            }
        }


        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            viewModel.EnableSecurityPaymentsConButton();
        }

        private void DecCheckBox_CheckedChanged(object sender, Boolean e)
        {
            viewModel.EnableDeclarationConButton();
        }

        private void DecCheckBox_CheckedChanged1(object sender, Boolean e)
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
            viewModel.RequestedReviewAmount = UtilityManager.GetCommaSeparatedAmount(rrAmountTxt.Text.ToString());

            if (!string.IsNullOrEmpty(viewModel.RequestedReviewAmount) && !string.IsNullOrEmpty(viewModel.TotalTaxLiability) &&
                Double.Parse(viewModel.RequestedReviewAmount) > Double.Parse(viewModel.TotalTaxLiability))
            {
                viewModel.RequestedReviewAmount = UtilityManager.GetCommaSeparatedAmount(viewModel.TotalTaxLiability);
            }
            viewModel.EnableReviewDetailsConButton();
            viewModel.FetchSecurityAmount();

        }
        private void Dispute_Amount_ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedITem = e.DataItem as VatReviewViewModel.SelectionModel;
                if (selectedITem.SelectionTitle.Equals(AppResources.VRInfull))
                {
                    viewModel.IsRRAmountEdit = false;
                    viewModel.VRRequesttoReviewtheAmountValue = AppResources.VRInfull;
                    viewModel.RequestedReviewAmount = UtilityManager.GetCommaSeparatedAmount(viewModel.TotalTaxLiability.ToString());
                    viewModel.FetchSecurityAmount();
                }
                else if (selectedITem.SelectionTitle.Equals(AppResources.VRInpartial))
                {
                    viewModel.IsRRAmountEdit = true;
                    viewModel.FetchSecurityAmount();
                    viewModel.VRRequesttoReviewtheAmountValue = AppResources.VRInpartial;
                }
            }
            catch (Exception)
            {


            }
        }

       

        private void LateFiling_Details_Tx_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.charCountLateFilingDetails = LateFiling_Details_Txx.Text.Length + "/" + 3000;
            viewModel.LateFlngDetails = LateFiling_Details_Txx.Text;

            viewModel.EnableLateFilingsDetailsConButton();

        }
    }
}