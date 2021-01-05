using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATRealEstatePage;
using GAZT.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.SyncFusionEnabledViews.VATRealEstatePages
{
    [Preserve(AllMembers = true)]
    public partial class VATRealEstateServicesPage : ContentPage
    {
        VATRealEstateServicesPageViewModel viewModel;

        public VATRealEstateServicesPage()
        {
            InitializeComponent();
            viewModel = App.Locator.VATRealEstateServicesPage;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            viewModel.ImagePropertyTile = "vat_tile_listofsignup_W.png";
            viewModel.ImageRequestTile = "re_Tile_Background_White.png";
            viewModel.ImageTerminationTile = "re_Tile_Background_White.png";

            viewModel.ImagePropertyIcon = "re_Property_Registration_G.png";
            //image_individual_icon.Source = "vat_new_individual.png";
            viewModel.ImageRequestIcon = "re_Request_Verification.png";
            // image_estimated_icon.Source = "vat_new_Establishment_W.png";
            viewModel.ImageTerminationIcon = "re_Termination_Request.png";

            viewModel.PropertyTileColor = Color.FromHex("#006450");
            viewModel.RequestTileColor = Color.FromHex("#006450");
            viewModel.TerminateTileColor = Color.FromHex("#006450");
            //viewModel.IsLoading = false;
            SetLTR();
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                Image_backArrow.Rotation = 0;
            }
            else
            {
                
                Image_backArrow.Rotation = 180;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
       
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.ImagePropertyTile = "vat_tile_listofsignup_W.png";
            viewModel.ImageRequestTile = "re_Tile_Background_White.png";
            viewModel.ImageTerminationTile = "re_Tile_Background_White.png";

            viewModel.ImagePropertyIcon = "re_Property_Registration_G.png";
            //image_individual_icon.Source = "vat_new_individual.png";
            viewModel.ImageRequestIcon = "re_Request_Verification.png";
            // image_estimated_icon.Source = "vat_new_Establishment_W.png";
            viewModel.ImageTerminationIcon = "re_Termination_Request.png";

            viewModel.PropertyTileColor = Color.FromHex("#006450");
            viewModel.RequestTileColor = Color.FromHex("#006450");
            viewModel.TerminateTileColor = Color.FromHex("#006450");
            //viewModel.IsLoading = false;
            //your code here;
            //viewModel.IsLoading = false;
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        private void LoadData()
        {
            viewModel.PopulateServicesData();

        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }
        
    
    }
}
