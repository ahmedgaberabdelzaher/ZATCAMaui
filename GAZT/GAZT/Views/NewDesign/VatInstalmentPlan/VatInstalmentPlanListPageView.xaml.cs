using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.ViewModel.NewDesignViewModel.InstalmentPlanViewModel;
using EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using static EGAZT.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails;
using static EGAZT.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails.DisplayInstallmentAgreementSchedulePlan;

namespace EGAZT.Views.NewDesign.InstalmentPlan
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

                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.VatInstalmentPlanListPageView;
                this.BindingContext = viewModel;


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

        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            InstalmentPlanModel selectedItem = e.AddedItems[0] as InstalmentPlanModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);

            if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 0)
            {
                viewModel.EnableVAtInstalmentPlan();
            }
            else if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 1)
            {
                viewModel.EnableDisplayInstalment();
                viewModel.GetVATDisplaySchedule();
            }


        }

        private void SummaryattachmentsListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as Result31;
            var index = viewModel.RequestForInstalmentPlanList.IndexOf(item);
            viewModel.GetDetailsClicked(index);
            viewModel.EnableVAtInstalmentSummary();
        }

        private void DisplayListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as VtiaIaSetResult;
            var index = viewModel.RequestForScheduleList.IndexOf(item);
            viewModel.GetDisplayDetailsClicked(index);


            viewModel.EnableDisplayDetails();
        }
    }
}
