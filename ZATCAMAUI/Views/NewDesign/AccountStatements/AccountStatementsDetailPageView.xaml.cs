using System.Collections.ObjectModel;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.AccountDetails;
using ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements;
using static ZATCAMAUI.Models.AccountDetails.AccoungtDetails;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.AccountStatements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class AccountStatementsDetailPageView : ContentPage
    {
        MyBills myBills;
        AccountStatementDetailPageViewModel viewModel;
        public AccountStatementsDetailPageView(MyBills myBills, AccoungtDetails details_bills)
        {

            viewModel = App.Locator.AccPageDetailVM;
            this.BindingContext = viewModel;

            viewModel.accoungtDetails = details_bills;

            InitializeComponent();
            this.myBills = myBills;
            this.reload();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LableTaxPeriod.Text = "" + myBills.PeriodPart1 + " - " + myBills.PeriodPart2;
            LableFbNum.Text = AppResources.ASFBNum + " : " + myBills.Fbnum;
            LableDueDate.Text = "" + myBills.ACSFormatedFaedn;
            LableSadadNum.Text = "" + myBills.VTRE2;
            LableCardStatus.Text = "" + myBills.PymtStatus;
            LableCardTitle.Text = "" + myBills.BillTitle;
            LableTaxType.Text = myBills.Txt30;


            Color color = stringToColor(myBills.StatusText);

            FrameCardStatus.BackgroundColor = color;
            CardAmount.BackgroundColor = color;
            CardAmountRemaining.BackgroundColor = color;
            LableCardStatus.TextColor = color;

            if (myBills.IsPartiallyPaidVisibile)
            {
                LableAmountTitle.Text = AppResources.MyBillsPaidAmount;
                GridAmountRemaining.IsVisible = true;

                var paidAmount = "";
                if (myBills.TotalPaidAmt != null && !string.IsNullOrEmpty(myBills.TotalPaidAmt.ToString()))
                {
                    paidAmount = UtilityManager.GetCommaSeparatedAmount(myBills.TotalPaidAmt.ToString());
                }
                else
                {
                    paidAmount = "0.00";
                }
                var remainingAmount = "";
                if (myBills.TotalRemainingAmount != null && !string.IsNullOrEmpty(myBills.TotalRemainingAmount.ToString()))
                {
                    remainingAmount = UtilityManager.GetCommaSeparatedAmount(myBills._totalRemainingAmount.ToString());
                }
                else
                {
                    remainingAmount = "0.00";
                }

                RemainingAmount.Text = remainingAmount + " " + AppResources.ZSAR;
                LableAmount.Text = paidAmount + " " + AppResources.ZSAR;
            }
            else
            {
                LableAmountTitle.Text = AppResources.ZAmount;
                var paidAmount = "";
                if (myBills.TestDueAmount != null && !string.IsNullOrEmpty(myBills.TestDueAmount.ToString()))
                {
                    paidAmount = UtilityManager.GetCommaSeparatedAmount(myBills.TestDueAmount.ToString());
                }
                else
                {
                    paidAmount = "0.00";
                }
                LableAmount.Text = paidAmount + " " + AppResources.ZSAR;

                if (myBills.StatusText == AppResources.UnPaid)
                {

                    LableAmount.TextColor = (Color)Application.Current.Resources["ErrorColor"];
                    LableAmountTitle.TextColor = (Color)Application.Current.Resources["ErrorColor"];
                }

            }
            this.reload();

        }
        public void reload()
        {
            viewModel.oBJDTLSets = new ObservableCollection<Result_Obj>(viewModel.accoungtDetails.d.OBJ_DTLSet);
            viewModel.RETDTLSets = new ObservableCollection<Result_RET>(viewModel.accoungtDetails.d.RET_DTLSet);
            viewModel.instDTLSET = new ObservableCollection<Result_InST>(viewModel.accoungtDetails.d.INSTL_DTLSet);
            viewModel.billDetails = new ObservableCollection<Result_Bill>(viewModel.accoungtDetails.d.BILL_DTLSet);


            ObjectionDetails.IsVisible = viewModel.isObjectionDetailsVisible;
            ReturnDetails.IsVisible = viewModel.isRetunVisible;
            ISTPlanDetails.IsVisible = viewModel.isInstalmentDetailsVisible;
            InstPlanOBDetials.IsVisible = viewModel.isBIllDetialsVisble;
        }
        private Color stringToColor(string value)
        {
            Color StatusColor;
            if (value == AppResources.Paid)
            {
                StatusColor = (Color)Application.Current.Resources["SuccessColor"];
            }
            else if (value == AppResources.PartiallyPaid)
            {
                StatusColor = (Color)Application.Current.Resources["Partial"];
            }
            else
            {
                StatusColor = (Color)Application.Current.Resources["ErrorColor"];
            }

            return StatusColor;
        }

    }
}