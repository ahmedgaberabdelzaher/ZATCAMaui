using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATDeRegistration
{
    public partial class VATDeregistrationInstructionsPage : PopupPage
    {
        VATDeRegistrationInstructionsPageViewModel viewModel;
        public VATDeregistrationInstructionsPage()
        {
            InitializeComponent();
            viewModel = App.Locator.VATDeregistrationInstructionsPage;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;
        }
 
    }
}
