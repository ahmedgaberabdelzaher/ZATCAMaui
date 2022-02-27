using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLoginPage_ViewModel;
using GAZT.Models;
using Syncfusion.SfPicker.XForms;
using System;
using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.EmailEntry
{
    /// <summary>
    /// View used to show the email entry with validation status.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailEntry
    {
        SFLoginPageViewModel viewModel;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailEntry" /> class.
        /// </summary>
        public EmailEntry()
        {
            InitializeComponent();
            this.BindingContext = viewModel = App.Locator.SFLoginPageView;
            SetPickerFont();
            SetLTR();
            //TinsPicker

        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                            if (App.IsArabic)
                            {
                                TinsPicker.HeaderFontFamily = "Somar-SemiBold";
                                TinsPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                                TinsPicker.SelectedItemFontFamily = "Somar-SemiBold";
                                TinsPicker.UnSelectedItemFontFamily = "Somar-SemiBold";
                            }
                            else
                            {
                                TinsPicker.HeaderFontFamily = "Somar-SemiBold";
                                TinsPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                                TinsPicker.SelectedItemFontFamily = "Somar-SemiBold";
                                TinsPicker.UnSelectedItemFontFamily = "Somar-SemiBold";
                            }
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        TinsPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        TinsPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        TinsPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        TinsPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }


        //protected  override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    SetLTR();
        //}
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
        }
        private void TINs_Clicked(object sender, System.EventArgs e)
        {
            TinsPicker.IsOpen = true;
        }
        private void Email_UnFocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            bool isNumber = false;
            bool isEmailValid = false;
            isNumber = IsEnglishNumber(Email.Text);
            if (!isNumber)
            {
                isEmailValid = CheckValidEmail(Email.Text);
                if (!isEmailValid)
                {
                    EmailInputLayout.HasError = true;
                    //EmailInputLayout.ShowHint = true;
                }
                else
                {
                    EmailInputLayout.HasError = false;
                    MessagingCenter.Send("TinList", "TinList");
                    //EmailInputLayout.ShowHint = false;
                }
            }
            else
            {
                EmailInputLayout.HasError = false;
                //EmailInputLayout.ShowHint = false;
            }
        }
        private static bool CheckValidEmail(string email)
        {
            bool isEmailValid = false;
            if (!string.IsNullOrEmpty(email))
            {
                var regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                isEmailValid = regex.IsMatch(email) && !email.EndsWith(".");
            }
            return isEmailValid;
        }
        public static bool IsEnglishNumber(String arText)
        {
            bool isAllNumeric = true;
            if (!string.IsNullOrEmpty(arText))
            {
                foreach (char letter in arText.ToCharArray())
                {
                    if (!(letter >= 48 && letter <= 57))
                    {
                        isAllNumeric = false;
                    }
                }
            }
            return isAllNumeric;
        }
        private void TinsPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TIN selectedtin = (TIN)e.NewValue;
            TinsPicker.SelectedItem = selectedtin;//TINID
            viewModel.SelectedTinId = selectedtin;//selectedregion
            viewModel.SelectedTinIdPrev = selectedtin;//selectedregion
            viewModel.TINID = selectedtin.Tin;
        }
        private void TinsPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TinsPicker.SelectedItem = viewModel.SelectedTinIdPrev;//TINID
            viewModel.SelectedTinId = viewModel.SelectedTinIdPrev;//selectedregion
            if (viewModel.SelectedTinIdPrev == null)
            {
                viewModel.TINID = string.Empty;
            }
        }
    }
}