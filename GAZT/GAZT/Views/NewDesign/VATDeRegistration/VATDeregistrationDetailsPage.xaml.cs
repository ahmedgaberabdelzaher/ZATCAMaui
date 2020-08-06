using System;
using System.Collections.Generic;
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
            viewModel = App.Locator.VATDeRegistrationDetailsPage;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;
        }

        void attachmentsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
          
        }
        void OnDownloadAttachmentClicked()
        {

        }
        void OnDeleteAttachmentClicked()
        {

        }

    }
}
