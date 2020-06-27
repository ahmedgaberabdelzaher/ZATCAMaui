using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATIndividualSignupPageView : ContentPage
    {
        VATIndividualSignupPageViewModel viewModel;
        public VATIndividualSignupPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATIndividualSignupPageView;
            this.BindingContext = viewModel;
            SetLTR();
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