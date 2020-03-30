using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPageView : ContentPage
    {

        #region Variable

        ChangePasswordPageViewModel viewModel;
        #endregion

        #region Constructor
        public ChangePasswordPageView(ComingToOTPVerificationScreenFrom navigateTo)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            viewModel = App.Locator.ChangePasswordPageView;
            ChangeAeroIcon();
            SetLTR();
            this.BindingContext = viewModel;           
            viewModel.NavigateToOtpForEmailEnum = navigateTo;
            viewModel.OnPageLoad();

        }
        #endregion

        #region Method

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
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ChangeAeroIcon();
            viewModel.PasswordVisibilityForNewPassword = true;
            viewModel.PasswordVisibilityForOldPassword = true;
            viewModel.PasswordVisibilityForRetypePassword =true;
            for (int index = 0; index < Navigation.NavigationStack.Count; index++)
            {
                Page pg = Navigation.NavigationStack[index];
                if (pg.GetType() == typeof(OTPPageView))
                {
                    Navigation.RemovePage(pg);
                }
            }
        }
        public void OnPasswordVisibilityClickedForNewPassword(object sender, EventArgs args)
        {
            viewModel.PasswordVisibilityForNewPassword = !viewModel.PasswordVisibilityForNewPassword;
        }

        public void OnPasswordVisibilityClickedForOldPassword(object sender, EventArgs args)
        {
            viewModel.PasswordVisibilityForOldPassword = !viewModel.PasswordVisibilityForOldPassword;
        }
        
        public void OnPasswordVisibilityClickedForRetypePassword(object sender, EventArgs args)
        {
            viewModel.PasswordVisibilityForRetypePassword = !viewModel.PasswordVisibilityForRetypePassword;
        }

        public void OnPasswordFocused(object sender, EventArgs args)
        {
            Password.Unfocus();
        }
        #endregion


    }
}