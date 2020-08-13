using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EGAZT.Models.VATRefunds;
using EGAZT.ViewModel.NewDesignViewModel.VATRefunds;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATRefunds
{
    public partial class VATRefundsInstructionsPageView : PopupPage
    {
        VATRefundsInstructionsPageViewModel viewModel;

        public VATRefundsInstructionsPageView(ObservableCollection<VATRefundsModel> vATRefundsModel)
        {
            InitializeComponent();
            viewModel = App.Locator.VATRefundsInstructionsPageView;
            viewModel.ReloadData(vATRefundsModel);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;
        }
    }
}
