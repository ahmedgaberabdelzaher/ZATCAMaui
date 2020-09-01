using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentSignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentSignUPPageView : ContentPage
    {
        EstablishmentSignUPPageViewModel viewModel;
        public EstablishmentSignUPPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.EstablishmentSignUPPageView;
            BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void OnEstablishmentTapped(object sender, EventArgs e)
        {
            viewModel.IndividualBackImg = "FP_unselected_tile.png";
            viewModel.EstablishmentBackImg = "FP_selected_tile.png";
            //viewModel._navigationService.NavigateTo(App.SignUpForEstablishmentPageView);
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.SignUpForEstablishmentPageView);

            });
        }

        private void OnIndividualTapped(object sender, EventArgs e)
        {
            viewModel.IndividualBackImg = "FP_selected_tile.png";
            viewModel.EstablishmentBackImg = "FP_unselected_tile.png";

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.IndividualRegistrationPageView);

            });
        }
    }
}