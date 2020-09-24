using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.GenericPickers;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using GAZT.Models;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using System.Collections.ObjectModel;
using System.Globalization;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentRegistrationPage : ContentPage
    {
        EstablishmentRegistrationPageViewModel viewModel;
        public EstablishmentRegistrationPage()
        {
            InitializeComponent();
            ChangeAeroIcon();
            SetLTR();
            viewModel = App.Locator.EstablishmentRegistrationPage;
            viewModel.currentTab = EstablishmentRegistrationTabsEnum.RegistrationType;
            viewModel.CurrentIndex = (int)EstablishmentRegistrationTabsEnum.RegistrationType;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (viewModel.currentTab == EstablishmentRegistrationTabsEnum.RegistrationType)
            {
                viewModel.currentTab = EstablishmentRegistrationTabsEnum.RegistrationType;
                viewModel.CurrentIndex = (int)EstablishmentRegistrationTabsEnum.RegistrationType;
            }
            viewModel?.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
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

        void dOBDateClicked(System.Object sender, System.EventArgs e)
        {
            if (viewModel?.DatePickerInGregorian == true)
            {
                dobPicker.IsOpen = true;
            }
            else
            {
                dobHijiriPicker.IsOpen = true;
            }
        }

        void PassportIssueDateClicked(System.Object sender, System.EventArgs e)
        {
            if (viewModel?.DatePickerInGregorian == true)
            {
                passportIssuePicker.IsOpen = true;
            }
            else
            {
                passportIssueHijiriPicker.IsOpen = true;
            }
        }

        void PassportExpiryDateClicked(object sender, EventArgs e)
        {
            if (viewModel?.DatePickerInGregorian == true)
            {
                passportExpiryPicker.IsOpen = true;
            }
            else
            {
                passportExpiryHijiriPicker.IsOpen = true;
            }
        }

        void SfChipGroup_SelectionChanged(System.Object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            try
            {
                var index = TabSfChipGroup.ItemsSource.IndexOf(e.AddedItem);
                TabScrollView.ScrollToAsync(TabSfChipGroup.ChipLayout.Children.ElementAtOrDefault(index), ScrollToPosition.MakeVisible, true);
            }
            catch (Exception) { }
        }

        async void TapRentDeleteGestureRecognizer_Tapped(Object sender, EventArgs e)
        {
            var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText);
            confirmPopup.OnSelect = (str) =>
            {
                if (str == "Yes")
                {
                    var item = sender as Image;
                    var data = item.BindingContext as Attachment;
                    viewModel.OnRentAttachmentDeleteButtonTapped(data);
                }
            };
            await PopupNavigation.Instance.PushAsync(confirmPopup);
        }

        async void TapPassportDeleteGestureRecognizer_Tapped(Object sender, EventArgs e)
        {
            var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText);
            confirmPopup.OnSelect = (str) =>
            {
                if (str == "Yes")
                {
                    var item = sender as Image;
                    var data = item.BindingContext as Attachment;
                    viewModel.OnPassportAttachmentDeleteButtonTapped(data);
                }
            };
            await PopupNavigation.Instance.PushAsync(confirmPopup);
        }

        //private void dobPicker_DateSelected(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        //{
        //    viewModel.SelectedDOB = (e.NewValue as DateTime?)?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
        //}

        //private void PassportIssueDatePicker_DateSelected(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        //{
        //    viewModel.PassportIssueDate = (e.NewValue as DateTime?)?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
        //}

        //private void PassportExpiryDatePicker_DateSelected(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        //{
        //    viewModel.PassportExpireDate = (e.NewValue as DateTime?)?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
        //}

        void SfChipGroup_SelectionChanging(System.Object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangingEventArgs e)
        {
            try
            {
                var tt = e.AddedItem as string;
                EstablishmentRegistrationTabsEnum newselectedTab = getEnumFromChipsLabel(tt);
                if (!string.IsNullOrWhiteSpace(tt) && (int)newselectedTab >= (int)viewModel?.currentTab)
                {
                    e.Cancel = true;
                }
                else
                {
                    viewModel.currentTab = newselectedTab;
                }
            }catch(Exception){ }
        }

        private EstablishmentRegistrationTabsEnum getEnumFromChipsLabel(string label)
        {
            if (label.Equals(AppResources.ESTTaxpayerPersonalDetailsTabTitleLabel)){
                return EstablishmentRegistrationTabsEnum.TaxpayerDetail;
            }
            if (label.Equals(AppResources.ESTPassportDetailsTabTitleLabel))
            {
                return EstablishmentRegistrationTabsEnum.PassportDetails;
            }
            if (label.Equals(AppResources.ESTOutletsTabTitleLabel))
            {
                return EstablishmentRegistrationTabsEnum.Outlets;
            }
            if (label.Equals(AppResources.VATRFinancialDetails))
            {
                return EstablishmentRegistrationTabsEnum.FinancialDetail;
            }
            if (label.Equals(AppResources.ZVatSummary))
            {
                return EstablishmentRegistrationTabsEnum.Declaration;
            }
            return EstablishmentRegistrationTabsEnum.RegistrationType;
        }

        void dobPicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
            if (viewModel?.DatePickerInGregorian == true)
            {
                selectedItem = dobPicker.SelectedItem as ObservableCollection<object>;
            }
            else
            {
                selectedItem = dobHijiriPicker.SelectedItem as ObservableCollection<object>;
            }
            viewModel.SelectedDOB = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
        }

        void passportIssuePicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
            if (viewModel?.DatePickerInGregorian == true)
            {
                selectedItem = passportIssuePicker.SelectedItem as ObservableCollection<object>;
            }
            else
            {
                selectedItem = passportIssueHijiriPicker.SelectedItem as ObservableCollection<object>;
            }
            viewModel.PassportIssueDate = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
        }

        void passportExpiryPicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
            if (viewModel?.DatePickerInGregorian == true)
            {
                selectedItem = passportExpiryPicker.SelectedItem as ObservableCollection<object>;
            }
            else
            {
                selectedItem = passportExpiryHijiriPicker.SelectedItem as ObservableCollection<object>;
            }
            viewModel.PassportExpireDate = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
        }
    }
}
