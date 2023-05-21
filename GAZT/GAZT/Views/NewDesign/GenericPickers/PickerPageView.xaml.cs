using System;
using System.Collections.Generic;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.GenericPickers;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.GenericPickers
{
    [Preserve(AllMembers = true)]
    public partial class PickerPageView : PopupPage
    {
        PickerPageViewModel viewModel;
        public PickerPageView(List<string> _pickerSource)
        {
            InitializeComponent();

            viewModel = App.Locator.PickerPageView;
            viewModel.PickerItemSource = _pickerSource;
            ChangeAeroIcon();
            SetLTR();
           // SetPickerFont();
            //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            

            this.BindingContext = viewModel;

        }


        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                            PickerDoneButton.FontFamily = "Somar-SemiBold";
                            PickerCancelButton.FontFamily = "Somar-SemiBold";

                            PickerTitle.FontFamily = "Somar-SemiBold";

                            genericPicker.HeaderFontFamily = "Somar-SemiBold";
                            genericPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            genericPicker.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        //PickerDoneButton.FontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        //PickerTitle.FontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        //PickerCancelButton.FontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";

                        //genericPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";
                        //genericPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        //genericPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy
                        PickerDoneButton.FontFamily = "Somar-SemiBold";
                        PickerCancelButton.FontFamily = "Somar-SemiBold";

                        PickerTitle.FontFamily = "Somar-SemiBold";

                        genericPicker.HeaderFontFamily = "Somar-SemiBold";
                        genericPicker.SelectedItemFontFamily = "Somar-SemiBold";
                        genericPicker.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }

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
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        public PickerPageView(GenericPickerModel _pickerSource)
        {
            InitializeComponent();

            viewModel = App.Locator.PickerPageView;
            viewModel.DataSource = _pickerSource;
            viewModel.PickerItemSource = viewModel.DataSource.PickerData;
            viewModel.PickerTitle = viewModel.DataSource.PickerTitle;
            if (viewModel.DataSource.SelectedValue != null)
            {
                viewModel.SelectedItem = viewModel.DataSource.SelectedValue;
                genericPicker.SelectedItem = viewModel.SelectedItem;
            }
            this.BindingContext = viewModel;
           // SetPickerFont();
        }

        void genericPicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.DataSource.SelectedValue = e.NewValue.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void PopupClose_Clicked(object sender, EventArgs e)
        {

            PopupNavigation.Instance.PopAsync();

            try
            {
                MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", viewModel.DataSource);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void PopupCancel_Clicked(object sender, EventArgs e)
        {

            PopupNavigation.Instance.PopAsync();

        }

        void PopupPage_BackgroundClicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", viewModel.DataSource);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }
    }
}
