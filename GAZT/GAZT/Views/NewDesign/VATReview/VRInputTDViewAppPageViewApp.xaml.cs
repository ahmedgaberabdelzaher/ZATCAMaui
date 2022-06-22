using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATReview
{
    [Preserve(AllMembers = true)]
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
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
            viewModel = App.Locator.VatReviewView;
            this.BindingContext = viewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

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
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        private void VRITDAttachItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            try { 
            viewModel.OpenAttachment(e.ItemData as Attachment);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace.ToString());
                Console.WriteLine(ex.Message);
            }
        }

    }
}