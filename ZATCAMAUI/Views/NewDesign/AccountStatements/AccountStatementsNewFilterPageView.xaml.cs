
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.Views.NewDesign.AccountStatements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountStatementsNewFilterPageView : ContentPage
    {
        AccountStatementBillsPageViewModel viewModel;

        public AccountStatementsNewFilterPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.AccountStatementsFilterPageView;

            BindingContext = viewModel;
            viewModel.TodayDateNormal = null;
            viewModel.TodayDateinHijri = null;
            viewModel.IsHijriCal = false;
            if (viewModel.FromTxAmount != "")
            {
                TxFromAmountEntry.Text = viewModel.FromTxAmount;
            }
            else
            {
                TxFromAmountEntry.Text = "";
            }

            if (viewModel.ToTxAmount != "")
            {
                TxToAmountEntry.Text = viewModel.ToTxAmount;
            }
            else
            {
                TxToAmountEntry.Text = "";
            }
            viewModel.SetDefaultDate();
            viewModel.IsHijriCal = viewModel.MyBillsOriginal != null && viewModel.MyBillsOriginal.Count > 0 ? (viewModel.MyBillsOriginal[0].CalTyp != "G") : false;

        }


        private async void TSDateStartDateClicked(object sender, EventArgs e)//Due Date
        {
            // ClearSecondFields();
            viewModel.isTxStartDate = true;

            if (viewModel.IsHijriCal)
            {
                TxDateHijriCalendar.IsOpen = true;
            }
            else
            {
                TxDateNormalCalendar.IsOpen = true;
            }
        }

        private async void TSDateEndDateClicked(object sender, EventArgs e)//DueDate
        {
            // ClearSecondFields();
            viewModel.isTxStartDate = false;

            if (viewModel.IsHijriCal)
            {
                TxDateHijriCalendar.IsOpen = true;
            }
            else
            {
                TxDateNormalCalendar.IsOpen = true;
            }
        }

        private async void TaxPeriodStartDateClicked(object sender, EventArgs e)//Tax period
        {
            // ClearsNext();
            viewModel.isTaxPeriodStartDate = true;

            if (viewModel.IsHijriCal)
            {
                TaxPeriodDateHijriCalendar.IsOpen = true;
            }
            else
            {
                TaxPeriodDateNormalCalendar.IsOpen = true;
            }
        }

        private async void TaxPeriodEndDateClicked(object sender, EventArgs e)//tax period
        {
            // ClearsNext();
            viewModel.isTaxPeriodStartDate = false;

            if (viewModel.IsHijriCal)
            {
                TaxPeriodDateHijriCalendar.IsOpen = true;
            }
            else
            {
                TaxPeriodDateNormalCalendar.IsOpen = true;
            }
        }

        private void From_Amount_Unfocused(object sender, FocusEventArgs e)
        {
            viewModel.FromTxAmount = TxFromAmountEntry.Text;
        }

        private void From_Amount_Changed(object sender, TextChangedEventArgs e)
        {
            // ClearFirstFileds();
            viewModel.FromTxAmount = TxFromAmountEntry.Text;
        }

        private void To_Amount_Unfocused(object sender, FocusEventArgs e)
        {

            viewModel.ToTxAmount = TxToAmountEntry.Text;
            if (!string.IsNullOrEmpty(viewModel.FromTxAmount) && !string.IsNullOrEmpty(viewModel.ToTxAmount))
            {
                var fromAmount = Convert.ToDouble(viewModel.FromTxAmount);
                var toAmount = Convert.ToDouble(viewModel.ToTxAmount);

                if (fromAmount > toAmount)
                {

                    //viewModel._dialogService.ShowMessage(AppResources.ACFilterAmountValidation,AppResources.Information);

                    ShowValidationMessage(AppResources.ACFilterAmountValidation);
                    viewModel.ToTxAmount = "";
                    TxToAmountEntry.Text = "";
                }

            }

        }

        void ShowValidationMessage(string message)
        {

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));

            });

        }


        private void To_Amount_Changed(object sender, TextChangedEventArgs e)
        {
            //ClearFirstFileds();
            viewModel.ToTxAmount = TxToAmountEntry.Text;
        }


        private void TxDateNormalCalendar_Closed(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (TxDateHijriCalendar.SelectedItem != null)
                    {
                        var selectedItem = TxDateHijriCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        if (viewModel.isTxStartDate)
                        {
                            viewModel.TxFromDate = year + "/" + month + "/" + day;
                        }
                        else
                        {
                            viewModel.TxToDate = year + "/" + month + "/" + day;
                        }
                    }
                }
                else
                {
                    if (TxDateNormalCalendar.SelectedItem != null)
                    {
                        var selectedItem = TxDateNormalCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();

                        if (viewModel.isTxStartDate)
                        {
                            viewModel.TxFromDate = year + "/" + month + "/" + day;
                        }
                        else
                        {
                            viewModel.TxToDate = year + "/" + month + "/" + day;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(viewModel.TxFromDate) && !string.IsNullOrEmpty(viewModel.TxToDate))
                {

                    try
                    {

                        CultureInfo calCul;

                        if (viewModel.IsHijriCal)
                        {
                            calCul = new CultureInfo("ar-SA");
                        }
                        else
                        {
                            calCul = new CultureInfo("en-US");
                        }


                        if (DateTime.ParseExact(viewModel.TxFromDate, "yyyy/MM/dd", calCul) > DateTime.ParseExact(viewModel.TxToDate, "yyyy/MM/dd", calCul))
                        {
                            viewModel.TxToDate = "";

                            ShowValidationMessage(AppResources.ACFilterDateValidation);
                        }

                    }
                    catch (Exception)
                    {
                    }


                }


            }
            catch (Exception)
            {
            }

        }



        private void Clearfields(object sender, EventArgs e)
        {

            viewModel.TodayDateNormal = null;
            viewModel.TodayDateinHijri = null;
            viewModel.IsHijriCal = false;
            viewModel.TxFromDate = "";
            viewModel.TxToDate = "";
            viewModel.TPFromDate = "";
            viewModel.TPToDate = "";
            viewModel.FromTxAmount = "";
            viewModel.ToTxAmount = "";
            viewModel.SetDefaultDate();
            TxFromAmountEntry.Text = "";
            TxToAmountEntry.Text = "";
            viewModel.IsHijriCal = viewModel.MyBillsOriginal != null && viewModel.MyBillsOriginal.Count > 0 ? (viewModel.MyBillsOriginal[0].CalTyp != "G") : false;
        }

        void FromAmount_Tapped(System.Object sender, System.EventArgs e)
        {
            TxFromAmountEntry.Text = "";
            viewModel.FromTxAmount = "";

        }
        void ToAmount_Tapped(System.Object sender, System.EventArgs e)
        {
            TxToAmountEntry.Text = "";
            viewModel.ToTxAmount = "";
        }
    }
}