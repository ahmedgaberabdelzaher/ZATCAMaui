using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportDetailPageView : ContentPage
    {
        TaxEvasionReportDetailPageViewModel viewModel;
        double Latitude = 24.774265;
       
        public TaxEvasionReportDetailPageView(TaxEvasionReportDetails SelectedTaxEvasionListItem)
        {
            InitializeComponent();

            viewModel = App.Locator.TaxEvasionReportDetailPageView;
            this.BindingContext = viewModel;
            viewModel.SelectedTaxEvasionListItem = SelectedTaxEvasionListItem;
            SetLTR();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            ChangeAeroIcon();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetLocationToMap();
        }
        private void SetLocationToMap()
        {
            try
            {
                
                double lat = 24.7136, lon = 46.6753;
                try
                {
                    if (viewModel.SelectedTaxEvasionListItem != null)
                    {
                        if (viewModel.SelectedTaxEvasionListItem.Location != null)
                        {
                           
                            string[] Reportlocation = viewModel.SelectedTaxEvasionListItem.Location.Split(',');
                            var currentCulture = System.Globalization.CultureInfo.InstalledUICulture;
                            var numberFormat = (System.Globalization.NumberFormatInfo)currentCulture.NumberFormat.Clone();
                            numberFormat.NumberDecimalSeparator = ".";

                            double number = double.Parse("22.1", numberFormat);
                            lat = Convert.ToDouble(Reportlocation[0]);
                            lon = Convert.ToDouble(Reportlocation[1]);

                        }
                        if (viewModel.SelectedTaxEvasionListItem.Latitude != null)
                        {
                           
                          
                        }


                    }

                    Position position = new Position(lat, lon);
                    MapSpan mapSpan = new MapSpan(position, 0.0001, 0.001);
                    mapView.MoveToRegion(mapSpan);
                    Pin pin = new Pin();
                    pin.Label = "Report Location";
                    pin.Type = PinType.Place;
                    pin.Position = position;
                    mapView.Pins.Add(pin);//new Position(Convert.ToDouble(viewModel.selectedtaxEList.Latitude), Convert.ToDouble(viewModel.selectedtaxEList.Longitude));
                }
                catch (FeatureNotSupportedException)
                {
                    // Handle not supported on device exception
                }
                catch (FeatureNotEnabledException)
                {
                    // Handle not enabled on device exception
                }
                catch (PermissionException)
                {
                    // Handle permission exception
                }
                catch (Exception)
                {
                    // Unable to get location
                    
                    
                }
                //}
            }
            catch (Exception)
            {
                
                
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

    }
}