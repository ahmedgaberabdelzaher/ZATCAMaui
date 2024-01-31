using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using Syncfusion.Maui.Buttons;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATgoodsOnprofit;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.VATgoodsOnprofit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class NewYesorNoPageView : ContentPage
    {
        NewYesorNoPageViewModel viewModel;

        public NewYesorNoPageView()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.NewYesorNoView;
                BindingContext = viewModel;
                On<iOS>().SetUseSafeArea(true);
                SetLTR();
                ChangeAeroIcon();
                MakeFalse();
            }
            catch (Exception)
            {

            }
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                FlowDirection = FlowDirection.RightToLeft;
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
            Padding = safeInsets;

        }
        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            if (YesQ1.IsChecked == false && NoQ1.IsChecked == false)
            {
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZprofitsOnGoodsM02Vaidation));
                viewModel.rq1 = (Color)Application.Current.Resources["Red"];
                return;
            }


            if (viewModel.QA1 == "R" || viewModel.QA1 == "r")
            {
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZProfitOnGoodsQ1M01Validation));
                // IsLoading = false;
                return;
            }


            if (viewModel.basedonQ1 == true)
            {
                if (Yes.IsChecked == false && No.IsChecked == false)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZprofitsOnGoodsM02Vaidation));
                    viewModel.rq2 = (Color)Application.Current.Resources["Red"];
                    return;
                }
            }
            viewModel.callSubmit();
        }
        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            ShowAlertPopup(AppResources.ZProfitOnGoodsConfrimationMsg);
        }

        void backButton_Tapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }



        private async void ShowAlertPopup(string _message)
        {
            var alertResult = await DisplayAlert(AppResources.Information, _message, AppResources.ZProfitOnGoodsConfrimationOk, AppResources.ZprofitsOnGoodscancel);
            if (!alertResult)
            {

            }
            else
            {
                try
                {
                    MakeFalse();
                    viewModel._navigationService.GoBack();
                }
                catch (Exception)
                {

                }
            }
        }



        void MakeFalse()
        {

            try
            {
                Yes.IsChecked = false;
                YesQ1.IsChecked = false;
                No.IsChecked = false;
                NoQ1.IsChecked = false;

                viewModel.IsYesQ1Checked = false;
                viewModel.IsNoQ1Checked = false;
                viewModel.IsYesQ2Checked = false;
                viewModel.IsNoQ2Checked = false;
            }
            catch (Exception)
            {

            }
        }

        void YesQ1_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
        {
            try
            {
                SfRadioButton button = sender as SfRadioButton;
                if (button.Text == AppResources.ZProfitOnGoodsQOptionsYes && YesQ1.IsChecked == true)
                {
                    viewModel.QA1 = "X";
                    viewModel.basedonQ1 = true;
                    viewModel.rq1 = (Color)Application.Current.Resources["DarkGrayTextColor"];

                }
                else if (button.Text == AppResources.ZProfitOnGoodsQOptionsNo && NoQ1.IsChecked == true)
                {
                    viewModel.QA1 = "R";
                    viewModel.basedonQ1 = false;
                    viewModel.rq1 = (Color)Application.Current.Resources["DarkGrayTextColor"];
                }
                else
                {

                }

            }
            catch (Exception)
            {

            }
        }

        void Yes_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
        {
            try
            {
                SfRadioButton button = sender as SfRadioButton;
                if (button.Text == AppResources.ZProfitOnGoodsQOptionsYes && Yes.IsChecked == true)
                {
                    viewModel.QA2 = "X";
                    viewModel.rq2 = (Color)Application.Current.Resources["DarkGrayTextColor"];
                }
                else if (button.Text == AppResources.ZProfitOnGoodsQOptionsNo && No.IsChecked == true)
                {
                    viewModel.QA2 = "R";
                    viewModel.rq2 = (Color)Application.Current.Resources["DarkGrayTextColor"];
                }
            }

            catch (Exception)
            {

            }
        }


    }
}

