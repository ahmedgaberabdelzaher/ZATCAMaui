using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    [Preserve(AllMembers = true)]
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
            viewModel.NregActivityList = _activityNavigation.taxPayerDetails?.Nreg_ActivitySet?.results.Where(i => i.Actno == $"{Int16.Parse(_activityNavigation.nextNumber?.Actno):00000}").ToList();
            viewModel.CurrentTab = _activityNavigation.openedTab;
            BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
        }
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
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
            this.Padding = safeInsets;

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        void CREntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            viewModel?.validateCRNumber();
        }

        void CRSwitch_StateChanged(System.Object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {
           
            if (CRMainActivity?.IsOn == true && viewModel?.NregActivityList?.Count > 0)
            {
                viewModel?.NregActivityList?.ForEach(i => i.Actcat = "S");
            }
        }

        void LicenseSwitch_StateChanged(System.Object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {
            
            if (LicenseMainActivity?.IsOn == true && viewModel?.NregActivityList?.Count > 0)
            {
                viewModel?.NregActivityList?.ForEach(i => i.Actcat = "S");
            }
        }

        void validFromButtonClick(System.Object sender, System.EventArgs e)
        {
            if (viewModel?.EnableInputFields == true)
            {
                if (viewModel?.taxPayerDetails?.Caltp == "G")
                {
                    validFromPicker.IsOpen = true;
                }
                else
                {
                    validFromHijiriPicker.IsOpen = true;
                }
            }
        }

        void crValidFromButtonClick(System.Object sender, System.EventArgs e)
        {
            if (viewModel?.EnableInputFields == true)
            {
                if (viewModel?.taxPayerDetails?.Caltp == "G")
                {
                    crValidFromPicker.IsOpen = true;
                }
                else
                {
                    crValidFromHijiriPicker.IsOpen = true;
                }
            }
        }

        void crValidFromPicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
            try { 
            if (viewModel?.taxPayerDetails?.Caltp == "G")
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

        void validFromPicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
            try { 
            if (viewModel?.taxPayerDetails?.Caltp == "G")
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
