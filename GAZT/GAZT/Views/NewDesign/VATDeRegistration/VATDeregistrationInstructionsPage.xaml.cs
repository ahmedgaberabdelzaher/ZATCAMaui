using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATDeRegistration
{
    [Preserve(AllMembers = true)]
    public partial class VATDeregistrationInstructionsPage : PopupPage
    {
        VATDeRegistrationInstructionsPageViewModel viewModel;
        public VATDeregistrationInstructionsPage()
        {
            InitializeComponent();
            viewModel = App.Locator.VATDeregistrationInstructionsPage;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            SetLTR();
            //this.FlowDirection = FlowDirection.LeftToRight;
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
    }
}
