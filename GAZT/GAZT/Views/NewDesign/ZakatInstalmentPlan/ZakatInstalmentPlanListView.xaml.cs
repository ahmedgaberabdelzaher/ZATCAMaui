using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.ViewModel.NewDesignViewModel.InstalmentPlanViewModel;
using EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.InstalmentPlan
{
    public partial class ZakatInstalmentPlanListPageView : ContentPage
    {



        #region Variable
        ZakatInstalmentPlanListViewModel viewModel;


        #endregion

        public ZakatInstalmentPlanListPageView()
        {
            try
            {
                InitializeComponent();

                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ZakatInstalmentPlanListPageView;
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




    }
}
