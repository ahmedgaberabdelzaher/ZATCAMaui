using Maui.GoogleMaps;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionViewModels;

namespace ZATCAMAUI.Views.NewDesign.TAXEvasionPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportDetailPageView : ContentPage
    {
        TaxEvasionReportDetailPageViewModel viewModel;
        double Latitude = 24.774265;

        public TaxEvasionReportDetailPageView(TaxEvasionReportDetails SelectedTaxEvasionListItem)
        {
            InitializeComponent();

            viewModel = App.Locator.TaxEvasionReportDetailPageView;
            BindingContext = viewModel;
            viewModel.SelectedTaxEvasionListItem = SelectedTaxEvasionListItem;
            On<iOS>().SetUseSafeArea(true);
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

    }
}