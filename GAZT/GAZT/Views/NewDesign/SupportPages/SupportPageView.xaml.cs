using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SupportPageView : ContentPage
    {
        SupportPageViewModel viewModel;
        public SupportPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.SupportPageView;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void OnBackArrowTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }
    }
}