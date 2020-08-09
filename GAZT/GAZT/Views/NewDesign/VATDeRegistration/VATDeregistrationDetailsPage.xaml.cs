using System;
using System.Collections.Generic;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATDeRegistration
{
    public partial class VATDeregistrationDetailsPage : ContentPage
    {
        VATDeRegistrationDetailsPageViewModel viewModel;

        public VATDeregistrationDetailsPage()
        {
            InitializeComponent();
            viewModel = App.Locator.VATDeregistrationDetailsPage;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            VATDeregistrationModel selectedItem = e.AddedItems[0] as VATDeregistrationModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
        }

        void OnDownloadAttachmentClicked()
        {

        }
        void OnDeleteAttachmentClicked()
        {

        }


        void attachmentsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            // VATDeregistrationAttachmentsModel selectedItem = e.AddedItems[0] as VATDeregistrationAttachmentsModel;
            // viewModel.SelectedOutletOptionIndex = viewModel.AttachmentsListViewData.IndexOf(selectedItem);
            viewModel.SelectedAttachment = e.AddedItems[0] as VATDeregistrationAttachmentsModel;
            viewModel.SelectedOutletOptionIndex = viewModel.AttachmentsListViewData.IndexOf(viewModel.SelectedAttachment);

            if (viewModel.SelectedAttachment.IsAttachmentAttached == true)
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

        void btnReasonContinue_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.ReasonContinueBtnClicked();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("Clicked event");
        }

        void outletDocumentOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            VATDeregistrationModel selectedItem = e.AddedItems[0] as VATDeregistrationModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDocumentOptions.IndexOf(selectedItem);
        }
    }
}
