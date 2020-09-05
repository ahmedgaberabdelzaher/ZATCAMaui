using System;
using System.Collections.Generic;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
    public partial class TINDeregistrationCloseIndividualOutletsPageView : PopupPage
    {
        TINDeregistrationCloseIndividualOutletsPageViewModel viewModel;
        public TINDeregistrationCloseIndividualOutletsPageView( string reasonDesc)
        {
            InitializeComponent();

            viewModel = App.Locator.TINDeregistrationCloseIndividualOutletsPageView;
            viewModel.SelectedReason = reasonDesc;

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
           // viewModel.TinDeregistrationData = tinDeregistrationResponseModel;
            this.BindingContext = viewModel;
            
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
        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            TINDeregistrationModel selectedItem = e.AddedItems[0] as TINDeregistrationModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);


           
        }
    }
}
