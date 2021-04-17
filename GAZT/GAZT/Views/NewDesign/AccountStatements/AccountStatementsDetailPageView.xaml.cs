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
            LableTransactionDate.Text = "" + myBills.FormatedFaedn;
           // LableBillAmount.Text = "" + aSResult.BetrhAmount+" "+AppResources.ZSAR;
            LableCardStatus.Text = "" + myBills.StatusText;
            LableCardTitle.Text = "" + myBills.Txt30;
            //LableCardSubTitle.Text = "" + aSResult.Desc;

            Color color = stringToColor(myBills.StatusText);

            FrameCardStatus.BackgroundColor = color;
            CardAmount.BackgroundColor = color;
            CardAmountRemaining.BackgroundColor = color;

            var paidAmount = "";
            if (myBills.Paidamt != null && !string.IsNullOrEmpty(myBills.Paidamt.ToString()))
            {
                paidAmount = UtilityManager.GetCommaSeparatedAmount(myBills.Paidamt.ToString());
            }
            else
            {
                paidAmount = "0.00";
            }
            var remainingAmount = "";
            if (myBills.RemainingAmount != null && !string.IsNullOrEmpty(myBills.RemainingAmount.ToString()))
            {
                remainingAmount = UtilityManager.GetCommaSeparatedAmount(myBills.RemainingAmount.ToString());
            }
            else
            {
                remainingAmount = "0.00";
            }

            RemainingAmount.Text = remainingAmount + " " + AppResources.ZSAR;
            LableAmount.Text = paidAmount + " "+AppResources.ZSAR;
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