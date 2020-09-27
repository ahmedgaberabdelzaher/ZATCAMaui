using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATReview
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VRInputTDViewAppPageViewApp : ContentPage
    {
        private VatReviewViewModel viewModel;
        public VRInputTDViewAppPageViewApp()
        {
            InitializeComponent();
            
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel = App.Locator.VatReviewView;
            this.BindingContext = viewModel;

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

        private void VRITDAttachItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            viewModel.OpenAttachment(e.ItemData as Attachment);
        }

    }
}