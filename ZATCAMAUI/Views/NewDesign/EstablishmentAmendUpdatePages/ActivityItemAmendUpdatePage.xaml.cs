using Syncfusion.Maui.Buttons;
using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentAmendUpdatePages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityItemAmendUpdatePage : ContentPage
    {
        private ActivityNavigationModels _activityNavigation;
        private ActivityItemAmendUpdatePageViewModel viewModel;
        public ActivityItemAmendUpdatePage(ActivityNavigationModels activityNavigation)
        {
            InitializeComponent();
            _activityNavigation = activityNavigation;
            viewModel = App.Locator.ActivityItemAmendUpdatePageView;
            viewModel.taxPayerDetails = _activityNavigation.taxPayerDetails;
            viewModel.newNumber = _activityNavigation.nextNumber;
            viewModel.validateCR = _activityNavigation.validateCR;
            viewModel.validateLicense = _activityNavigation.validateLicense;
            viewModel.goBackAction = _activityNavigation.goBackAction;
            viewModel.NregActivityList = _activityNavigation.taxPayerDetails?.Nreg_ActivitySet?.Where(i => i.Actno == $"{Int16.Parse(_activityNavigation.nextNumber?.Actno):000}").ToList();
            viewModel.CurrentTab = _activityNavigation.openedTab;
            viewModel.PageType = _activityNavigation.openedTab;
            BindingContext = viewModel;
            if (viewModel.LicenseData.Count == 4)
            {
                viewModel.AddLicenseEnabled = false;
            }
            else
            {
                viewModel.AddLicenseEnabled = true;

            }
            viewModel.SetUIAvailability();
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {

                viewModel.PickerModel = arg;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetPickerFont();
            viewModel?.OnAppearing();

        }

        public void SetPickerFont()
        {
            try
            {
                switch (DeviceInfo.Platform)
                {

                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.iOS:
                        {
                            validFromPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            validFromPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            validFromPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            validFromPicker.TextStyle.FontFamily = "Somar-SemiBold";

                            validFromHijiriPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            validFromHijiriPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            validFromHijiriPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            validFromHijiriPicker.TextStyle.FontFamily = "Somar-SemiBold";

                            crValidFromPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            crValidFromPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            crValidFromPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            crValidFromPicker.TextStyle.FontFamily = "Somar-SemiBold";

                            crValidFromHijiriPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            crValidFromHijiriPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            crValidFromHijiriPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            crValidFromHijiriPicker.TextStyle.FontFamily = "Somar-SemiBold";
                        }
                        break;
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.Android:
                        {

                            validFromPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            validFromPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            validFromPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            validFromPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                            validFromHijiriPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            validFromHijiriPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            validFromHijiriPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            validFromHijiriPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                            crValidFromPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            crValidFromPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            crValidFromPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            crValidFromPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";


                            crValidFromHijiriPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            crValidFromHijiriPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            crValidFromHijiriPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            crValidFromHijiriPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        }
                        break;
                }
            }
            catch (Exception)
            {



            }

        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel?.OnDisappearing();
        }

        void CREntry_Unfocused(object sender, FocusEventArgs e)
        {
            viewModel?.validateCRNumber();
        }

        void CRSwitch_StateChanged(object sender, SwitchStateChangedEventArgs e)
        {
            if (CRMainActivity?.IsOn == true && viewModel?.NregActivityList?.Count > 0)
            {
                viewModel?.NregActivityList?.ForEach(i => i.Actcat = "S");
            }
        }

        void LicenseSwitch_StateChanged(object sender, SwitchStateChangedEventArgs e)
        {
            if (LicenseMainActivity?.IsOn == true && viewModel?.NregActivityList?.Count > 0)
            {
                viewModel?.NregActivityList?.ForEach(i => i.Actcat = "S");
            }
        }

        void validFromButtonClick(object sender, EventArgs e)
        {
            if (viewModel?.EnableInputFields == true)
            {
                if (viewModel?.taxPayerDetails?.Caltp == "Gregorian")
                {
                    validFromPicker.IsOpen = true;
                }
                else
                {
                    validFromHijiriPicker.IsOpen = true;
                }
            }
        }

        void crValidFromButtonClick(object sender, EventArgs e)
        {
            if (viewModel?.EnableInputFields == true)
            {
                if (viewModel?.taxPayerDetails?.Caltp == "Gregorian")
                {
                    crValidFromPicker.IsOpen = true;
                }
                else
                {
                    crValidFromHijiriPicker.IsOpen = true;
                }
            }
        }

        void crValidFromPicker_Closed(object sender, EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;

            try
            {
                if (viewModel?.taxPayerDetails?.Caltp == "Gregorian")
                {
                    selectedItem = crValidFromPicker.SelectedItem as ObservableCollection<object>;
                    viewModel.DisplayCRValidFrom = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
                    viewModel.CRValidFrom = viewModel?.DisplayCRValidFrom;
                }
                else
                {
                    selectedItem = crValidFromHijiriPicker.SelectedItem as ObservableCollection<object>;
                    viewModel.DisplayCRValidFrom = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
                    DateTime.TryParseExact(viewModel?.DisplayCRValidFrom, "yyyy/MM/dd", new CultureInfo("ar-sa"), DateTimeStyles.None, out DateTime _crvalidFrom);
                    viewModel.CRValidFrom = _crvalidFrom.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                }
            }
            catch (Exception)
            {
            }
        }

        void validFromPicker_Closed(object sender, EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
            try
            {
                if (viewModel?.taxPayerDetails?.Caltp == "Gregorian")
                {
                    selectedItem = validFromPicker.SelectedItem as ObservableCollection<object>;
                    viewModel.DisplayValidFrom = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
                    viewModel.ValidFrom = viewModel?.DisplayValidFrom;
                }
                else
                {
                    selectedItem = validFromHijiriPicker.SelectedItem as ObservableCollection<object>;
                    viewModel.DisplayValidFrom = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
                    DateTime.TryParseExact(viewModel?.DisplayValidFrom, "yyyy/MM/dd", new CultureInfo("ar-sa"), DateTimeStyles.None, out DateTime _validFrom);
                    viewModel.ValidFrom = _validFrom.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                }
            }
            catch (Exception)
            {
            }
        }
    }
}