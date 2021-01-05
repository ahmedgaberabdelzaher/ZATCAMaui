using System;
using System.Collections.Generic;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
    [Preserve(AllMembers = true)]
    public partial class ZakatDeregistrationPageView : ContentPage
    {
        ZakatDeregistrationPageViewModel viewModel;

        public ZakatDeregistrationPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.ZakatDeregistrationPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;
        }
    }
}
