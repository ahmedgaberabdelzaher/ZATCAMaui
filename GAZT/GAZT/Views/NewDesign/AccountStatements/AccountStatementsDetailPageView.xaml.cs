using EGAZT.Models;
using EGAZT.Models.AccountStatements;
using EGAZT.NewDesignConverters;
using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.NewDesign.AccountStatements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class AccountStatementsDetailPageView : ContentPage
    {
        MyBills myBills;
        //ASResult aSResult;
        public AccountStatementsDetailPageView(/*ASResult aSResult*/MyBills myBills)
        {
            InitializeComponent();
            SetLTR();
            ChangeAeroIcon();
            this.myBills = myBills;
        }

        private async void backButton_Tapped(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
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
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            LableTaxPeriod.Text = ""+ myBills.PeriodPart1 +" - "+myBills.PeriodPart2;
            LableFbNum.Text = "" + myBills.Fbnum;
            LableDueDate.Text = "" + myBills.FormatedFaedn;
            LableSadadNum.Text = "" + myBills.VTRE2;
            //LableTransactionDate.Text = "" + myBills.FormatedFaedn;
           // LableBillAmount.Text = "" + aSResult.BetrhAmount+" "+AppResources.ZSAR;
            LableCardStatus.Text = "" + myBills.StatusText;
            LableCardTitle.Text = "" + myBills.BillTitle;
            //LableCardSubTitle.Text = "" + aSResult.Desc;



            //string str = myBills.Txt30.ToLower();
            
            // if(str.Length > 0) {

            //    TaxTypeGrid.IsVisible = true;
            //    str = char.ToUpper(str[0]) + str.Substring(1);
            //}
            // else {

            //    TaxTypeGrid.IsVisible = false;
            //}

          

           // LableTaxType.Text = myBills.Abtypt + " - " + str;
            LableTaxType.Text = myBills.Txt30;


            Color color = stringToColor(myBills.StatusText);

            FrameCardStatus.BackgroundColor = color;
            CardAmount.BackgroundColor = color;
            CardAmountRemaining.BackgroundColor = color;

           

           

            if (myBills.IsPartiallyPaidVisibile) {
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
            else {
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
            }

          


        }

        private Color stringToColor(String value)
        {
            Color StatusColor;
            if (value == AppResources.Paid)
            {
                StatusColor = Color.FromHex("#006450");
            }
            else if (value==AppResources.PartiallyPaid)
            {
                StatusColor = Color.Orange;
            }
            else 
            {
                StatusColor = Color.FromHex("#AA0C19");
            }

            return StatusColor;
        }

    }
}