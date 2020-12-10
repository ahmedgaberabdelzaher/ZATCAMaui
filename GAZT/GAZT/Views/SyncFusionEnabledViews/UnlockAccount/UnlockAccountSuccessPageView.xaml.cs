using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.UnlockAccount;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.SyncFusionEnabledViews.UnlockAccount
{
    [Preserve(AllMembers = true)]
    public partial class UnlockAccountSuccessPageView : ContentPage
    {
        UnlockAccountSuccessPageViewModel viewModel;

        public UnlockAccountSuccessPageView(string PasswordChangedSuccessfully)
        {
            InitializeComponent();

            viewModel = App.Locator.UnlockAccountSuccessPageViewModel;
            this.BindingContext = viewModel;
            viewModel.PasswordChangedSuccessfully = PasswordChangedSuccessfully;
            SetLTR();
            ChangeAeroIcon();
        }

        void btnLogin_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.PopToRootPage();
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

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                //Image_backArrow.Rotation = 0;
                //Label_MobileInitialAr.IsVisible = false;
                //Label_MobileInitialEng.IsVisible = true;
                //TINEntry.HorizontalTextAlignment = TextAlignment.Start;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                //Image_backArrow.Rotation = 180;
                //TINEntry.HorizontalTextAlignment = TextAlignment.End;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                //Label_MobileInitialAr.IsVisible = true;
                //Label_MobileInitialEng.IsVisible = false;
            }
        }
    }
}
