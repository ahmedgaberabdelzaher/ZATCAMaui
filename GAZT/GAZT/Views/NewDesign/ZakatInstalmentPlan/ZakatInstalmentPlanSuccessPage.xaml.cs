using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentPlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ZakatInstalmentPlanSuccessPage : ContentPage
    {
        ZakatInstalmentPlanViewModel viewModel;
        public ZakatInstalmentPlanSuccessPage()
        {
            InitializeComponent();
            SetLTR();
            viewModel = App.Locator.ZakatInstalmentPlanPageView;
            this.BindingContext = viewModel;
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

    }


}