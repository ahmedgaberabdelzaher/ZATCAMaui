
using Syncfusion.Maui.Buttons;
using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentRegistrationPages
{

    public partial class ActivityItemPage : ContentPage
    {
        private ActivityNavigationModels _activityNavigation;
        private ActivityItemPageViewModel viewModel;
        public ActivityItemPage(ActivityNavigationModels activityNavigation)
        {

            InitializeComponent();
            _activityNavigation = activityNavigation;
            viewModel = App.Locator.ActivityItemPage;
            viewModel.taxPayerDetails = _activityNavigation.taxPayerDetails;
            viewModel.newNumber = _activityNavigation.nextNumber;
            viewModel.validateCR = _activityNavigation.validateCR;
            viewModel.validateLicense = _activityNavigation.validateLicense;
            viewModel.goBackAction = _activityNavigation.goBackAction;
            viewModel.NregActivityList = _activityNavigation.taxPayerDetails?.Nreg_ActivitySet?.Where(i => i.Actno == $"{Int16.Parse(_activityNavigation.nextNumber?.Actno):00000}").ToList();
            viewModel.CurrentTab = _activityNavigation.openedTab;
            BindingContext = viewModel;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
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
            { }
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
