using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
﻿using EGAZT.Models;
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

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentRegistrationPage : ContentPage
    {
        EstablishmentRegistrationPageViewModel viewModel;
        public EstablishmentRegistrationPage()
        {
            InitializeComponent();
            SetLTR();
            viewModel = App.Locator.EstablishmentRegistrationPage;
            BindingContext = viewModel;

            MessagingCenter.Subscribe<PickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (sender, arg) => {
                viewModel.DatePickerModel = arg;
                Console.WriteLine(arg);
                //OnAppearing();
            });

            viewModel.SetDefaultDate();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel?.OnAppearing();
            DateMessagingCenterSelector();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void DateMessagingCenterSelector()
        {
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (sender, arg) =>
            {

                if (App.IsArabic)
                {
                    if (arg.DatePickerTitle.Contains("Issue Date"))
                    {
                        viewModel.PassportIssueDate = arg.SelectedValue;
                    }
                    else if (arg.DatePickerTitle.Contains("Expiry Date"))
                    {
                        viewModel.PassportExpireDate = arg.SelectedValue;
                    }
                    else
                    {
                        viewModel.SelectedDOB = arg.SelectedValue;
                    }
                }
                else
                {
                    if (arg.DatePickerTitle.Contains("Issue Date"))
                    {
                        viewModel.PassportIssueDate = arg.SelectedValue;
                    }
                    else if (arg.DatePickerTitle.Contains("Expiry Date"))
                    {
                        viewModel.PassportExpireDate = arg.SelectedValue;
                    }
                    else
                    {
                        viewModel.SelectedDOB = arg.SelectedValue;
                    }
                }
            });

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

        async void dOBDateClicked(System.Object sender, System.EventArgs e)
        {

            GenericDatePickerModel genericPickerModel = new GenericDatePickerModel();
            genericPickerModel.DatePickerTitle = "Select DOB";
            genericPickerModel.PickerId = "DOBDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }

        }

        async void PassportIssueDateClicked(System.Object sender, System.EventArgs e)
        {

            GenericDatePickerModel genericPickerModel = new GenericDatePickerModel();
            genericPickerModel.DatePickerTitle = "Issue Date";
            genericPickerModel.PickerId = "PassportIssueDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }

        }


        

        async void PassportExpiryDateClicked(object sender, EventArgs e)
        {
            GenericDatePickerModel genericPickerModel = new GenericDatePickerModel();
            genericPickerModel.DatePickerTitle = "Expiry Date";
            genericPickerModel.PickerId = "PassportExpityDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
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

        private void OnDOBClicked(object sender, EventArgs e)
        {
            dob.IsOpen = true;
        }

        private void dob_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void dob_Closed(object sender, EventArgs e)
        {
            if (dob.SelectedItem != null)
            {
                var selectedItem = dob.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.SelectedDOB = year + "/" + month + "/" + day;

            }
        }

        private void dob_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            if (dob.SelectedItem != null)
            {
                var selectedItem = dob.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.SelectedDOB = year + "/" + month + "/" + day;

            }
        }

        private void dob_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void OnPassportIssueDateClicked(object sender, EventArgs e)
        {
            PassportIssueDate.IsOpen = true;
        }
        private void PassportIssueDate_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            if (PassportIssueDate.SelectedItem != null)
            {
                var selectedItem = PassportIssueDate.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.PassportIssueDate = year + "/" + month + "/" + day;
            }
        }

        private void PassportIssueDate_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void PassportIssueDate_Closed(object sender, EventArgs e)
        {
            if (PassportIssueDate.SelectedItem != null)
            {
                var selectedItem = PassportIssueDate.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.PassportIssueDate = year + "/" + month + "/" + day;
            }
        }

        private void OnPassportExpiryDateClicked(object sender, EventArgs e)
        {
            PassportExpiryDate.IsOpen = true;
        }

        private void PassportExpiryDate_Closed(object sender, EventArgs e)
        {
            if (PassportExpiryDate.SelectedItem != null)
            {
                var selectedItem = PassportExpiryDate.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.PassportExpireDate = year + "/" + month + "/" + day;
            }
        }

        private void PassportExpiryDate_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            if (PassportExpiryDate.SelectedItem != null)
            {
                var selectedItem = PassportExpiryDate.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.PassportExpireDate = year + "/" + month + "/" + day;
            }
        }

        private void PassportExpiryDate_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }
    }
}
