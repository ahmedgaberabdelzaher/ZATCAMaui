using System.Collections.Specialized;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;

namespace ZATCAMAUI.Views.NewDesign.VatInstalmentPlan
{

    public partial class VatInstalmentPlanListPageView : ContentPage
    {

        #region Variable
        VATInstalmentPlanListViewModel viewModel;


        #endregion

        public VatInstalmentPlanListPageView()
        {
            try
            {
                InitializeComponent();

                viewModel = App.Locator.VatInstalmentPlanListPageView;
                BindingContext = viewModel;
                viewModel.ResetData();
                VATInstalmentDisplayViewPage.DataSource.DisplayItems.CollectionChanged += DisplayItems_CollectionChanged;

            }
            catch (Exception)
            {


            }

        }

        private void DisplayItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (VATInstalmentDisplayViewPage.DataSource.DisplayItems.Count == 0)
            {
                VATInstalmentDisplayViewPage.IsVisible = false;
                NoDataLable.IsVisible = true;
            }
            if (VATInstalmentDisplayViewPage.DataSource.DisplayItems.Count > 0)
            {
                VATInstalmentDisplayViewPage.IsVisible = true;
                NoDataLable.IsVisible = false;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, bool>(this, "ISCallBackFromSuccess");

        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                
                viewModel.EnableVATLandingPage();
                viewModel.AddOutletDecisionOptions();

                MessagingCenter.Subscribe<object, bool>(this, "ISCallBackFromSuccess", async (sender, arg) =>
                {
                    if (arg == true)
                    {
                        viewModel.EnableVAtInstalmentPlan();
                        viewModel.IsLoading = true;
                        await viewModel.GetVATInstalmentPlanList();
                        viewModel.IsLoading = false;
                    }
                    else
                    {

                        viewModel.EnableVATLandingPage();
                        viewModel.AddOutletDecisionOptions();
                    }

                });


                viewModel.NumberOfInstalmentPlans = "" + AppResources.ZakatInstalmetPlan;

               


            }
            catch (Exception)
            {


            }
        }


       

     
    }
}
