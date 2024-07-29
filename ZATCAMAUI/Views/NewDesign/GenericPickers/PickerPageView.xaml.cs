using Mopups.Pages;
using Mopups.Services;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.GenericPickers;

namespace ZATCAMAUI.Views.NewDesign.GenericPickers
{

    public partial class PickerPageView : PopupPage
    {
        PickerPageViewModel viewModel;
        public PickerPageView(List<string> _pickerSource)
        {
            InitializeComponent();

            viewModel = App.Locator.PickerPageView;
            viewModel.PickerItemSource = _pickerSource;
            ChangeAeroIcon();

            this.BindingContext = viewModel;

        }


        public void SetPickerFont()
        {
            try
            {
                switch (DeviceInfo.Platform)
                {

                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.iOS:
                        PickerDoneButton.FontFamily = "Somar-SemiBold";
                        PickerCancelButton.FontFamily = "Somar-SemiBold";

                        PickerTitle.FontFamily = "Somar-SemiBold";

                        genericPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                        genericPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                        genericPicker.TextStyle.FontFamily = "Somar-SemiBold";

                        break;
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.Android:

                        PickerDoneButton.FontFamily = "Somar-SemiBold";
                        PickerCancelButton.FontFamily = "Somar-SemiBold";

                        PickerTitle.FontFamily = "Somar-SemiBold";

                        genericPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                        genericPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                        genericPicker.TextStyle.FontFamily = "Somar-SemiBold";

                        break;
                }
            }
            catch (Exception)
            {


            }

        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }

        public PickerPageView(GenericPickerModel _pickerSource)
        {
            try
            {
                InitializeComponent();

                viewModel = App.Locator.PickerPageView;
                viewModel.DataSource = _pickerSource;
                viewModel.PickerItemSource = viewModel.DataSource.PickerData;
                viewModel.PickerTitle = viewModel.DataSource.PickerTitle;
                if (viewModel.DataSource.SelectedValue != null)
                {
                    viewModel.SelectedItem = viewModel.DataSource.SelectedValue;
                    //genericPicker.Columns[0].SelectedIndex = int.Parse(viewModel.SelectedItem);
                }
                this.BindingContext = viewModel;
                // SetPickerFont();
            }
            catch (Exception)
            {
            }
           
        }

        void genericPicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                viewModel.DataSource.SelectedValue = viewModel.DataSource.PickerData[e.NewValue];

            }
            catch (Exception)
            {



            }
        }

        private void PopupClose_Clicked(object sender, EventArgs e)
        {

            MopupService.Instance.PopAsync();

            try
            {
                MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", viewModel.DataSource);

            }
            catch (Exception)
            {



            }
        }

        private void PopupCancel_Clicked(object sender, EventArgs e)
        {

            MopupService.Instance.PopAsync();

        }

        void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", viewModel.DataSource);
            }
            catch (Exception)
            {



            }
        }
    }
}
