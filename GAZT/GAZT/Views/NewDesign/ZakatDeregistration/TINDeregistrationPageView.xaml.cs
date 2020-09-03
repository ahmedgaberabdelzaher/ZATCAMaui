using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
    public partial class TINDeregistrationPageView : ContentPage
    {
        TINDeregistrationPageViewModel viewModel;
        public TINDeregistrationPageView(TinDeregistrationResponseModel tinDeregistrationResponseModel)
        {
            InitializeComponent();

            viewModel = App.Locator.TINDeregistrationPageView;

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;
                Console.WriteLine(arg);
            });

            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (sender, arg) =>
            {
                if (arg.PickerId == "DeregDatePicker")
                {
                    viewModel.DeregistrationDate = Convert.ToDateTime(arg.SelectedValue);
                }
                Console.WriteLine(arg);
            });

            viewModel.LoadReasonSet();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        void SfListView_ItemTapped(System.Object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            //try
            //{
            //    foreach (TINDeregistrationModel tINDeregistrationModel in viewModel.OutletDecisionOptions)
            //    {
            //        tINDeregistrationModel.ActiveOutletDecisionOptionsIsSelected = false;
            //    }

            //    var dataItem = e.ItemData as TINDeregistrationModel;
            //    dataItem.ActiveOutletDecisionOptionsIsSelected = true;
            //}
            //catch (Exception ex)
            //{
            //}
        }

        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            TINDeregistrationModel selectedItem = e.AddedItems[0] as TINDeregistrationModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
        }

        void attachmentsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            viewModel.SelectedAttachment = e.AddedItems[0] as TinDeregestrationAttachmentsModel;
            viewModel.SelectedOutletOptionIndex = viewModel.AttachmentsListViewData.IndexOf(viewModel.SelectedAttachment);

            if(viewModel.SelectedAttachment.IsAttachmentAttached == true)
            {
                viewModel.SelectedAttachment.AttachmentName = string.Empty;
                viewModel.SelectedAttachment.IsAttachmentAttached = false;
            }
            else
            {
                attachmentsListView.SelectedItems.Clear();
                viewModel.AddAttachmentEx();
            }
        }   
    }
}
