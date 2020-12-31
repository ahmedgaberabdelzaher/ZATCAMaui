using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentAmendUpdatePages
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
            //viewModel.cRActivityItem = _activityNavigation.cRActivityItem;
            viewModel.goBackAction = _activityNavigation.goBackAction;
            viewModel.NregActivityList = _activityNavigation.taxPayerDetails?.Nreg_ActivitySet?.results.Where(i => i.Actno == $"{Int16.Parse(_activityNavigation.nextNumber?.Actno):000}").ToList();
            viewModel.CurrentTab = _activityNavigation.openedTab;
            viewModel.PageType = _activityNavigation.openedTab;
            BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            if (viewModel.LicenseData.Count == 4)
            {
                viewModel.AddLicenseEnabled = false;
            }
            else
            {
                viewModel.AddLicenseEnabled = true;

            }
            viewModel.SetUIAvailability();
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
                //Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                //Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel?.OnAppearing();

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel?.OnDisappearing();
        }

        void CREntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            viewModel?.validateCRNumber();
        }

        void CRSwitch_StateChanged(System.Object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {
            Console.WriteLine("CR Main " + CRMainActivity.IsOn);
            if (CRMainActivity?.IsOn == true && viewModel?.NregActivityList?.Count > 0)
            {
                viewModel?.NregActivityList?.ForEach(i => i.Actcat = "S");

                //var _mainItem = viewModel?.NregActivityList?.FirstOrDefault(i => i.Actcat == "M");
                //if (_mainItem != null)
                //{
                //    var confirmPopup = new ZAKATOkCancelPopUpView("Are you sure you want to mark as main activity")
                //    {
                //        CloseWhenBackgroundIsClicked = false
                //    };
                //    confirmPopup.OnSelect = (str) =>
                //    {
                //        CRMainActivity.IsOn = str == "Yes";
                //        if (str == "Yes") {
                //            viewModel?.NregActivityList?.ForEach(i => i.Actcat = "S");
                //        }
                //    };
                //    await PopupNavigation.Instance.PushAsync(confirmPopup);
                //}
            }
        }

        //private async void OnLicenseSelected(object sender, EventArgs e)
        //{
        //    Image ArraowImage = sender as Image;
        //    Nreg_ActivityItem LicenseData = (Nreg_ActivityItem)ArraowImage.BindingContext;
        //    if (LicenseData != null)
        //    {
        //        viewModel.OpenLicenseFormInEditMode(LicenseData);
        //      //  await PopupNavigation.Instance.PushAsync(new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText));

        //    }
        //}


        async void LicenseSwitch_StateChanged(System.Object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {
            Console.WriteLine("License Main " + LicenseMainActivity.IsOn);
            if (LicenseMainActivity?.IsOn == true && viewModel?.NregActivityList?.Count > 0)
            {
                //var _mainItem = viewModel?.NregActivityList?.FirstOrDefault(i => i.Actcat == "M");
                //if (_mainItem != null)
                //{
                //    var confirmPopup = new ZAKATOkCancelPopUpView("Are you sure you want to mark as main activity")
                //    {
                //        CloseWhenBackgroundIsClicked = false
                //    };
                //    confirmPopup.OnSelect = (str) =>
                //    {
                //        LicenseMainActivity.IsOn = str == "Yes";
                //        if (str == "Yes")
                //        {
                viewModel?.NregActivityList?.ForEach(i => i.Actcat = "S");
                //        }
                //    };
                //    await PopupNavigation.Instance.PushAsync(confirmPopup);
                //}
            }
        }

        //void validFromPicker_DateSelected(System.Object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        //{
        //    viewModel.ValidFrom = (e.NewValue as DateTime?)?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
        //}

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

        //void crValidFromPicker_DateSelected(System.Object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        //{
        //    viewModel.CRValidFrom = (e.NewValue as DateTime?)?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
        //}

        void crValidFromPicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
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

        void validFromPicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
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
    }
}