using EGAZT.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.UpdateVatEffectiveDate
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterVatEffectiveDatePageView : ContentPage
    {
        FilterVatEffectiveDatePageViewModel viewModel;
        public FilterVatEffectiveDatePageView()
        {
            InitializeComponent();
            viewModel = App.Locator.FilterVatEffectiveDatePageView;
            ChangeAeroIcon();
            SetLTR();
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
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }

        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        void SortAscending_Tapped(System.Object sender, System.EventArgs e)
        {
        }

        void SortDescending_Tapped(System.Object sender, System.EventArgs e)
        {
        }
    }
}