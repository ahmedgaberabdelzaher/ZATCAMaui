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
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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
            dobPicker.MaximumDate = DateTime.Now;
            dobPicker.IsOpen = true;
        }

        void PassportIssueDateClicked(System.Object sender, System.EventArgs e)
        {
            PassportIssueDatePicker.MaximumDate = DateTime.Now;
            PassportIssueDatePicker.IsOpen = true;
        }

        void PassportExpiryDateClicked(object sender, EventArgs e)
        {
            PassportExpiryDatePicker.MinimumDate = DateTime.Now;
            PassportExpiryDatePicker.IsOpen = true;
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

        private void dobPicker_DateSelected(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            viewModel.SelectedDOB = (e.NewValue as DateTime?)?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
        }

        private void PassportIssueDatePicker_DateSelected(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            viewModel.PassportIssueDate = (e.NewValue as DateTime?)?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
        }

        private void PassportExpiryDatePicker_DateSelected(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            viewModel.PassportExpireDate = (e.NewValue as DateTime?)?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
        }
    }
}
