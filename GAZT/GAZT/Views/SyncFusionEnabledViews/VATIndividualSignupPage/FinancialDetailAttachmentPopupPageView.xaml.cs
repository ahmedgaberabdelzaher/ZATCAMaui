using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinancialDetailAttachmentPopupPageView : PopupPage
    {
        FinancialDetailAttachmentPopupPageViewModel viewModel;
        public FinancialDetailAttachmentPopupPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.FinancialDetailAttachmentPopupPageView;
            this.BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetLTR();
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void DDlIDType_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DDlIDType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DDlIDType_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void btn1_Clicked(object sender, EventArgs e)
        {

        }
    }
}