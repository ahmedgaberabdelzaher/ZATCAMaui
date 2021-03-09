using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VatReview
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReviewBillViewBottomPopUpPageView : PopupPage
    {
        private VatReviewViewModel _viewModel;
        public VatReviewBillViewBottomPopUpPageView()
        {
            InitializeComponent();
            _viewModel = App.Locator.VatReviewView;
            this.BindingContext = _viewModel;
            SetLTR();
        }
        
        private void SetLTR()
        {
            if (!App.IsArabic)
            {

                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {

                this.FlowDirection = FlowDirection.RightToLeft;

            }
        }

        private void CloseTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();

        }
    }
}