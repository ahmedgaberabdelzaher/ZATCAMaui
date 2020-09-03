using System;
using System.Collections.Generic;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
    public partial class ZakatRegistrationDetailsListPageView : ContentPage
    {
        ZakatRegistrationDetailsListPageViewModel viewModel;

        

        public ZakatRegistrationDetailsListPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.ZakatRegistrationDetailsListPageView;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
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

        public void registrationDetailsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            try
            {
                viewModel.GetNewTinDeregistrationDataCliked();
                var view = sender as SfListView;
                view.SelectedItem = null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
