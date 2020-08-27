using System;
using EGAZT.Models.ContractRelease;
using EGAZT.ViewModel.NewDesignViewModel.ContractRelease;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace EGAZT.Views.NewDesign.ContractReleasePages
{
    public partial class ContractReleaseListPageView : ContentPage
    {

        #region Variable
        ContractReleaseListViewModel viewModel;

        #endregion

        public ContractReleaseListPageView()
        {
            try
            {
                InitializeComponent();

                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ContractReleasePageListView;

                this.BindingContext = viewModel;

                viewModel.ResetData();
                viewModel.OnPageLoad();
            }
            catch (Exception ex)
            {

            }
        }



        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

        }

        private void ContractsList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as ContractReLeaseApplicationFormModel.ContractResult;
            viewModel.GetContractReleaseSummaryData(item);
        }
    }
}
