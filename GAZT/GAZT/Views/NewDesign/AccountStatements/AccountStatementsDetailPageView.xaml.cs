using EGAZT.Models.AccountStatements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.AccountStatements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class AccountStatementsDetailPageView : ContentPage
    {
        ASResult aSResult;
        public AccountStatementsDetailPageView(ASResult aSResult)
        {
            InitializeComponent();
            SetLTR();
            ChangeAeroIcon();
            this.aSResult = aSResult;
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

            LableTaxPeriod.Text = ""+aSResult.PeriodTxt;
            LableFbNum.Text = "" + aSResult.Fbnum;
            LableDueDate.Text = "" + aSResult.FormattedBldat2;
            LableSadadNum.Text = "" + aSResult.Opbel;
            LableTransactionDate.Text = "" + aSResult.FormattedBldat;
           // LableBillAmount.Text = "" + aSResult.BetrhAmount+" "+AppResources.ZSAR;
            LableCardStatus.Text = "" + aSResult.StatusDesc;
            LableCardTitle.Text = "" + aSResult.TaxtypeDesc;
            //LableCardSubTitle.Text = "" + aSResult.Desc;

            Color color = stringToColor(aSResult.Status);

            FrameCardStatus.BackgroundColor = color;
            CardAmount.BackgroundColor = color;

            LableAmount.Text = "" +aSResult.BetrhAmount + " "+AppResources.ZSAR;
        }

        private Color stringToColor(String value)
        {
            Color StatusColor;
            if (value.ToString() == "1" || value.ToString() == "4")
            {
                StatusColor = Color.FromHex("#FCE087");
            }
            else if (value.ToString() == "2" || value.ToString() == "8" || value.ToString() == "9")
            {
                StatusColor = Color.FromHex("#99C97B");
            }
            else if (value.ToString().Trim() == "3")
            {
                StatusColor = Color.FromHex("#E52027");
            }
            else if (value.ToString() == "5" || value.ToString() == "7")
            {
                StatusColor = Color.FromHex("#39679A");
            }
            else if (value.ToString() == "6")
            {
                StatusColor = Color.FromHex("#999999");
            }
            else
            {
                StatusColor = Color.FromHex("#D99A29");
            }
            return StatusColor;
        }

    }
}