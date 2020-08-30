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

        

        void TapRentDeleteGestureRecognizer_Tapped(Object sender, EventArgs e)
        {
            var item = sender as Image;
            var data = item.BindingContext as Attachment;
            viewModel.OnRentAttachmentCloseTapped(data);
        }



        void TapPassportDeleteGestureRecognizer_Tapped(Object sender, EventArgs e)
        {
            var item = sender as Image;
            var data = item.BindingContext as Attachment;
            viewModel.OnPassportCloseButtonTapped(data);
        }

    }
}
