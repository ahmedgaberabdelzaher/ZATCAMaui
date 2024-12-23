
using Microsoft.Maui.Controls.PlatformConfiguration;
using Mopups.Services;
using Syncfusion.Maui.Buttons;
using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

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
            ActivitiesPopUpViewModel.DataSent += OnDataReceivedFromScreen2;
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
            ActivitiesPopUpViewModel.DataSent -= OnDataReceivedFromScreen2;
        }

        private void OnDataReceivedFromScreen2(object sender, List<NregMulSet> data)
        {
            viewModel.NregMulActivityList = data;

            if (viewModel.CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
            {
                if (viewModel?.SelectedLicenseItem != null)
                {
                    viewModel?.SelectedCRItem?.activitySet?.Clear();
                    viewModel?.SelectedCRItem?.activitySet?.Concat(data);
                }

            }
            else if (viewModel.CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
            {
                if (viewModel?.SelectedLicenseItem != null)
                {
                    viewModel?.SelectedLicenseItem?.activitySet?.Clear();
                    viewModel?.SelectedLicenseItem?.activitySet?.Concat(data);
                }
            }


        }

        void CR_National_Entry_Unfocused(System.Object sender,FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(viewModel.CRNationalNumber) || !viewModel.CRNationalNumber.StartsWith("7") || viewModel.CRNationalNumber.Length != 10)
            {
                viewModel.CRNationalNumber = string.Empty;
                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZCommercialRegistrationNumbershouddbe10digitswith7starts));
            }
            else
            {
                viewModel?.validateCRNumber(viewModel.CRNationalNumber, true);
            }
        }

        void CREntry_Unfocused(object sender, FocusEventArgs e)
        {
            viewModel?.validateCRNumber(viewModel.CRNumber, true);
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ActivitiesPopUpViewModel.DataSent += OnDataReceivedFromScreen2;


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
