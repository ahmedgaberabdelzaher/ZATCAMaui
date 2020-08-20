using System;
using System.Collections.Generic;
using EGAZT.Models.VATRefunds;
using EGAZT.ViewModel.NewDesignViewModel.VATRefunds;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATRefunds
{
    public partial class VATRefundsListPageView : ContentPage
    {
        VATRefundListPageViewModel viewModel;
        public VATRefundsListPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundsListPageView;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.PopulateVATRefundsList();
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

        void vatRefundDetailsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            try
            {
                VatRefHeaderSetResult selectedItem = e.AddedItems[0] as VatRefHeaderSetResult;
                viewModel.SelectionChanged(selectedItem);


                if(selectedItem.Status == AppResources.VATRefundsStatusDraft)
                {
                    viewModel.VatRefundsListResultModel.IsEditable = true;
                    viewModel._navigationService.NavigateTo(App.VATRefundsNewRequestPageView, viewModel.VatRefundsListResultModel);
                }
                else
                {
                    viewModel.VatRefundsListResultModel.IsEditable = false;
                    viewModel._navigationService.NavigateTo(App.VATRefundDetailsPageView, viewModel.VatRefundsListResultModel);
                }

                var view = sender as SfListView;
                view.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void NewRefundRequest_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                //await viewModel.ReloadData();
                viewModel._navigationService.NavigateTo(App.VATRefundsNewRequestPageView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
