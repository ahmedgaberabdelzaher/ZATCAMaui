using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentSignUP
{
    [Preserve(AllMembers = true)]
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

        protected override async  void OnAppearing()
        {
            base.OnAppearing();

          
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
              viewModel.IndividualBackImg=  "FP_unselected_tile.png";
              viewModel.EstablishmentBackImg = "FP_unselected_tile.png";
            });
            if (App.IsArabic)
            {
                backArrow.Rotation = 180;
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                backArrow.Rotation = 0;
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            viewModel.PageTitle= AppResources.ZTERNewAccount;
            viewModel.BodyText= AppResources.ZZZZSelectthetypeofEntity;
        }

        private async void OnEstablishmentTapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
                viewModel.IndividualBackImg = "FP_unselected_tile.png";
                viewModel.EstablishmentBackImg = "FP_selected_tile.png";

            });

            //viewModel._navigationService.NavigateTo(App.SignUpForEstablishmentPageView);
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.SignUpForEstablishmentPageView);

            });
        }

        private async  void OnIndividualTapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
                viewModel.IndividualBackImg = "FP_selected_tile.png";
                viewModel.EstablishmentBackImg = "FP_unselected_tile.png";

            });

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.IndividualRegistrationPageView);

            });
        }

        private void OnBackArrowTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }
    }
}