
using RGPopup.Maui.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.AccountStatements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountStatementsNewFilterPageView : ContentPage
    {
        AccountStatementBillsPageViewModel viewModel;

        public AccountStatementsNewFilterPageView()
        {
            InitializeComponent();
            ChangeFilterArrow();
            viewModel = App.Locator.AccountStatementsFilterPageView;

            BindingContext = viewModel;
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
            viewModel.IsHijriCal = viewModel.MyBillsOriginal != null && viewModel.MyBillsOriginal.Count > 0 ? viewModel.MyBillsOriginal[0].CalTyp != "G" : false;

        }


        public void ChangeFilterArrow()
        {
            if (App.IsArabic)
            {
                Resources["FilterArrow"] = Resources["FilterArrowImageForArabicStyle"];
            }
            else
            {
                Resources["FilterArrow"] = Resources["FilterArrowImageForEnglishStyle"];
            }
        }

        private async void TSDateStartDateClicked(object sender, EventArgs e)
        {
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

        private async void TSDateEndDateClicked(object sender, EventArgs e)
        {
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

        private async void TaxPeriodStartDateClicked(object sender, EventArgs e)
        {
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

        private async void TaxPeriodEndDateClicked(object sender, EventArgs e)
        {
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
                /*await viewModel._dialogService.ShowMessage(message,
                                    AppResources.Information);*/
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(message));

            });

        }


        private void To_Amount_Changed(object sender, TextChangedEventArgs e)
        {
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

        private void TaxPeriodDateNormalCalendar_Closed(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (TaxPeriodDateHijriCalendar.SelectedItem != null)
                    {
                        var selectedItem = TaxPeriodDateHijriCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        //viewModel.FromDate = year + "/" + month + "/" + day;
                        if (viewModel.isTaxPeriodStartDate)
                        {
                            viewModel.TPFromDate = year;
                        }
                        else
                        {
                            viewModel.TPToDate = year;
                        }

                    }
                }
                else
                {
                    if (TaxPeriodDateNormalCalendar.SelectedItem != null)
                    {
                        var selectedItem = TaxPeriodDateNormalCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        //viewModel.FromDate = year + "/" + month + "/" + day;
                        if (viewModel.isTaxPeriodStartDate)
                        {
                            viewModel.TPFromDate = year;
                        }
                        else
                        {
                            viewModel.TPToDate = year;
                        }

                    }
                }



                if (!string.IsNullOrEmpty(viewModel.TPFromDate) && !string.IsNullOrEmpty(viewModel.TPToDate))
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


                        if (DateTime.ParseExact(viewModel.TPFromDate, "yyyy", calCul) > DateTime.ParseExact(viewModel.TPToDate, "yyyy", calCul))
                        {
                            viewModel.TPToDate = "";

                            ShowValidationMessage(AppResources.ACFilterYearValidation);
                        }

                    }
                    catch (Exception)
                    { }


                }


            }
            catch (Exception)
            { }

        }

        void TaxPeriodDateNormalCalendar_OkButtonClicked(System.Object sender, System.EventArgs e)
        {

            if (viewModel.IsHijriCal)
            {
                TxDateHijriCalendar.IsOpen = false;
            }
            else
            {
                TxDateNormalCalendar.IsOpen = false;
            }

            if (viewModel.IsHijriCal)
            {
                TaxPeriodDateHijriCalendar.IsOpen = false;
            }
            else
            {
                TaxPeriodDateNormalCalendar.IsOpen = false;
            }

            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (TaxPeriodDateHijriCalendar.SelectedItem != null)
                    {
                        var selectedItem = TaxPeriodDateHijriCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        if (viewModel.isTaxPeriodStartDate)
                        {
                            viewModel.TPFromDate = year;
                        }
                        else
                        {
                            viewModel.TPToDate = year;
                        }

                    }
                }
                else
                {
                    if (TaxPeriodDateNormalCalendar.SelectedItem != null)
                    {
                        var selectedItem = TaxPeriodDateNormalCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        //viewModel.FromDate = year + "/" + month + "/" + day;

                        if (viewModel.isTaxPeriodStartDate)
                        {
                            viewModel.TPFromDate = year;
                        }
                        else
                        {
                            viewModel.TPToDate = year;
                        }



                    }
                }


                CultureInfo calCul;

                if (viewModel.IsHijriCal)
                {
                    calCul = new CultureInfo("ar-SA");
                }
                else
                {
                    calCul = new CultureInfo("en-US");
                }


                if (DateTime.ParseExact(viewModel.TPFromDate, "yyyy", calCul) > DateTime.ParseExact(viewModel.TPToDate, "yyyy", calCul))
                {
                    viewModel.TPToDate = "";
                    ShowValidationMessage(AppResources.ACFilterYearValidation);

                }


            }
            catch (Exception)
            {


            }
        }

        void TaxPeriodDateNormalCalendar_CancelButtonClicked(System.Object sender, System.EventArgs e)
        {
            if (viewModel.IsHijriCal)
            {
                TxDateHijriCalendar.IsOpen = false;
            }
            else
            {
                TxDateNormalCalendar.IsOpen = false;
            }

            if (viewModel.IsHijriCal)
            {
                TaxPeriodDateHijriCalendar.IsOpen = false;
            }
            else
            {
                TaxPeriodDateNormalCalendar.IsOpen = false;
            }
        }

        void TxDateNormalCalendar_OkButtonClicked(System.Object sender, System.EventArgs e)
        {
            if (viewModel.IsHijriCal)
            {
                TxDateHijriCalendar.IsOpen = false;
            }
            else
            {
                TxDateNormalCalendar.IsOpen = false;
            }

            if (viewModel.IsHijriCal)
            {
                TaxPeriodDateHijriCalendar.IsOpen = false;
            }
            else
            {
                TaxPeriodDateNormalCalendar.IsOpen = false;
            }


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

    }
}