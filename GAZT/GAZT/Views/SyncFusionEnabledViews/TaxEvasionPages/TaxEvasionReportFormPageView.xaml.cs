using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportFormPageView : ContentPage
    {

        TaxEvasionReportFormPageViewModel viewModel;


        public TaxEvasionReportFormPageView(TaxEvasionReportList SelectedTaxEvasionListItem)
        {
            try
            {
                viewModel = App.Locator.TaxEvasionReportFormPageView;
                InitializeComponent();
                //CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                //PickerResourceManager.Manager = new ResourceManager("GAZT.TestPicker", Application.Current.GetType().Assembly);
                SetLTR();
                this.BindingContext = viewModel;
                clearFields();
                SetLTR();
            }
            catch (Exception ex)
            {

            }
            //mapset();

            viewModel.CreateCompanyTypeList();
            //selectedtaxEList
            viewModel.onPageLoad();
            viewModel.selectedtaxEList = SelectedTaxEvasionListItem;
            viewModel.SelectedCategory = SelectedTaxEvasionListItem.ViolationType;


            //TName.Text = selectedtaxEList.ReporterName;
            if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.ReportNumber))
            {


                viewModel.TName = viewModel.selectedtaxEList.ReporterName; TName.IsEnabled = false;
                viewModel.TMobNumber = viewModel.selectedtaxEList.ReporterMobileNumber; TMobNumber.IsEnabled = false;
                viewModel.TEmail = viewModel.selectedtaxEList.ReporterEmail; TEmail.IsEnabled = false;
                viewModel.TFaciName = viewModel.selectedtaxEList.CompanyName; TFaciName.IsEnabled = false;
                //viewModel.TFaciMobNo= selectedtaxEList.
                // DateLabel.IsVisible = true; DateLabel.IsEnabled = false; DateLabel.Text = viewModel.selectedtaxEList.ReceivedDate; DpDbo.IsEnabled = false; DpDbo.IsVisible = false;

                /*viewModel.SelectedTaxEvasionCompanyType.Id= selectedtaxEList.c*/              ///*viewModel.TFaciOwnerName=selectedtaxEList.*/
                TFaciOwnerName.IsEnabled = false; TFaciMobNo.IsEnabled = false; TFaciEmail.IsEnabled = false;
                RegionPicker.IsEnabled = false;
                RegionPickerAR.IsEnabled = false; CityPicker.IsEnabled = false; CityPickerAR.IsEnabled = false;
                
                viewModel.TFDAdress = viewModel.selectedtaxEList.District; TFDAdress.IsEnabled = false;
                viewModel.TFWType = viewModel.selectedtaxEList.WorkType; TFWType.IsEnabled = false;
                viewModel.TFSAddress = viewModel.selectedtaxEList.CompanyAddress; TFDAdress.IsEnabled = false;
                viewModel.IsSubmitButtonEnable = false; submit_btnmane.IsEnabled = false; submit_btnmane.BackgroundColor = Color.Gray;
                
                viewModel.TReportDetail = viewModel.selectedtaxEList.ReportDetails; TReportDetail.IsEnabled = false;
                viewModel.TVatNumber = viewModel.selectedtaxEList.VATNumber; TVatNumber.IsEnabled = false;
                RegionPicker.IsEnabled = false; RegionPickerAR.IsEnabled = false; CityPicker.IsEnabled = false; CityPickerAR.IsEnabled = false; btnFacilityType.IsEnabled = false;
                if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.RegionCode))
                {
                    viewModel.SelectedTaxEvasionRegion = viewModel.RList.Where(x => x.RegionCode == SelectedTaxEvasionListItem.RegionCode).FirstOrDefault();
                    viewModel.onSelectedTaxEvasionRegion();
                    if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.CityCode))
                    { viewModel.SelectLCType = viewModel.CList.Where(x => x.CityCode == SelectedTaxEvasionListItem.CityCode).FirstOrDefault();
                    }

                }
                if (!(string.IsNullOrEmpty(viewModel.selectedtaxEList.Latitude) && string.IsNullOrEmpty(viewModel.selectedtaxEList.Longitude)))
                {
                    Position position = new Position(Convert.ToDouble(viewModel.selectedtaxEList.Latitude), Convert.ToDouble(viewModel.selectedtaxEList.Longitude));
                    MapSpan mapSpan = new MapSpan(position, 0.0001, 0.001);
                    mapView.MoveToRegion(mapSpan);
                    Pin pin = new Pin();
                    pin.Label = "Report Location";
                    pin.Type = PinType.Place;
                    pin.Position = new Position(Convert.ToDouble(viewModel.selectedtaxEList.Latitude), Convert.ToDouble(viewModel.selectedtaxEList.Longitude));
                    mapView.Pins.Clear();
                    mapView.Pins.Add(pin);

                }
                mapView.IsEnabled = false;
                //viewModel.SelectedTaxEvasionRegion = selectedtaxEList;
                //viewModel.SelectLCType.CityCode = selectedtaxEList.CityCode;
                //viewModel.SelectLCType.Latitude = selectedtaxEList.Latitude;

                //if (!App.IsArabic)
                //{ viewModel.SelectLCType.CityNameEN = selectedtaxEList.CityNameEn; }
                //else {  viewModel.SelectLCType.CityNameAR = selectedtaxEList.CityNameAr;}



                if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.TIN))
                { viewModel.IsTIN = true; viewModel.TxtTIN = viewModel.selectedtaxEList.TIN; viewModel.IsTINVisible = true; }
                else
                {
                    viewModel.IsTIN = false;
                }
                TxtTIN.IsEnabled = false; IsTIN.IsEnabled = false;
                viewModel.TID = viewModel.selectedtaxEList.ID; TID.IsEnabled = false;







            }
            else
            {

            }

            // viewModel.SelectedCategory = SelectedCat;
        }



        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                viewModel.IsVisiblePickerAr = false;
                viewModel.IsVisiblePickerEn = true;


            }
            else
            {
                PickerResourceManager.Manager = new ResourceManager("GAZT.TestPicker", Application.Current.GetType().Assembly);
                viewModel.IsVisiblePickerAr = true;
                viewModel.IsVisiblePickerEn = false;


            }
            //if (App.IsArabic)
            //{

            //    this.FlowDirection = FlowDirection.RightToLeft;
            //    CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
            //    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
            //    PickerResourceManager.Manager = new ResourceManager("GAZT.TestPicker", Application.Current.GetType().Assembly);
            //        viewModel.IsVisiblePickerAr = true;
            //        viewModel.IsVisiblePickerEn = false;

            //}
            //else
            //{
            //    viewModel.IsVisiblePickerAr = false;
            //   viewModel.IsVisiblePickerEn = true;
            //    this.FlowDirection = FlowDirection.LeftToRight;
            //    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
            //    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
            //}

        }






        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                RegionPickerAR.Focus();



            }
            else
            {
                RegionPicker.Focus();

            }



        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            if (App.IsArabic)
            {
                CityPickerAR.Focus();
                viewModel.IsVisiblePickerAr = true;
                viewModel.IsVisiblePickerEn = false;
            }
            else
            {
                CityPicker.Focus();

            }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(() => { viewModel.IsLoading = true; });

            await AddReport();

            Device.BeginInvokeOnMainThread(() => { viewModel.IsLoading = false; });

            //viewModel.SubmitCreatedReport();

        }

        public async Task AddReport()
        {
            if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.ReportNumber))
            {
                submit_btnmane.BackgroundColor = Color.Gray;
            }
            else
            {





                bool flag = true;

                if (string.IsNullOrEmpty(TName.Text))
                {
                    flag = false; TName.Focus();
                }

                else if (string.IsNullOrEmpty(TMobNumber.Text))
                { flag = false; TMobNumber.Focus(); }
                //else if (string.IsNullOrEmpty(TEmail.Text))
                //{ flag = false; FrmEmail.Focus(); }
                else if (string.IsNullOrEmpty(TFaciName.Text))
                { flag = false; FrmFName.Focus(); }
                //else if (string.IsNullOrEmpty(TFaciOwnerName.Text))
                //{ flag = false; FrmFOName.Focus(); }
                //else if (string.IsNullOrEmpty(TFaciMobNo.Text))
                //{ flag = false; FrmFMobNo.Focus(); }
                //else if (string.IsNullOrEmpty(TFaciEmail.Text))
                //{ flag = false; FrmFEmail.Focus(); }
                else if (string.IsNullOrEmpty(TFDAdress.Text))
                { flag = false; FrmFDAddress.Focus(); }
                else if (string.IsNullOrEmpty(TFSAddress.Text))
                { flag = false; FrmFSAddress.Focus(); }
                else if (string.IsNullOrEmpty(TxtTIN.Text) && string.IsNullOrEmpty(TID.Text))
                { flag = false; FrmTIN.Focus();viewModel.IsTINVisible = true; viewModel.IsTIN = true; }
                else if (string.IsNullOrEmpty(TReportDetail.Text))
                { flag = false; FrmReportDetail.Focus(); }
                else if (string.IsNullOrEmpty(TFWType.Text))
                { flag = false; FrmTFW.Focus(); }
                else if (string.IsNullOrEmpty(TReportDetail.Text))
                { flag = false; FrmReportDetail.Focus(); FrmReportDetail.HasError = true; }

                else
                {
                    if (flag == true)
                    { await viewModel.SubmitCreatedReport(); }
                }

            }
        }

        private void TMobNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(TMobNumber.Text))
            {
                if (TMobNumber.Text.Length < 8)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZInvalidMobileNoError;


                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    FrmNumber.HasError = true;
                    TMobNumber.Text = string.Empty;
                    TMobNumber.Focus();
                }

                else
                {
                    FrmNumber.HasError = false;
                }
            }

        }

        private void TFaciMobNo_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(TFaciMobNo.Text))
            {
                if (TFaciMobNo.Text.Length < 8)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZInvalidMobileNoError;


                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    FrmFMobNo.HasError = true;
                    TFaciMobNo.Text = string.Empty;
                    TFaciMobNo.Focus();
                }

                else
                {
                    FrmFMobNo.HasError = false;
                }
            }

        }

        private void TName_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TName.Text))
            {
                FrmName.HasError = true;

                TName.Text = string.Empty;
            }
            else
            { FrmName.HasError = false; }


        }

        private void TEmail_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(TEmail.Text))
            {
                bool flag = IsValid(TEmail.Text);
                if (!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZEmailAddressdoesnotmatchwithvalueinMinistryofCommerce;


                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    FrmEmail.HasError = true;
                    TEmail.Text = string.Empty;
                }
                else
                {
                    FrmEmail.HasError = false;
                }
            }


        }
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void TFaciOwnerName_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TFaciOwnerName.Text))
            {
                FrmFOName.HasError = true;
                TFaciOwnerName.Text = string.Empty;
                TFaciOwnerName.Focus();
            }
            else
            { FrmFOName.HasError = false; }


        }

        private void TFaciEmail_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(TFaciEmail.Text))
            {
                bool flag = IsValid(TFaciEmail.Text);
                if (!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZEmailAddressdoesnotmatchwithvalueinMinistryofCommerce;


                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    FrmFEmail.HasError = true;
                    TFaciEmail.Text = string.Empty;
                }
                else
                {
                    FrmFEmail.HasError = false;
                }
            }



        }

        private void TFDAdress_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TFDAdress.Text))
            {
                FrmFDAddress.HasError = true;
                TFDAdress.Text = string.Empty;
                TFDAdress.Focus();

            }
            else
            { FrmFDAddress.HasError = false; }

        }

        private void TFSAddress_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void TxtTIN_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtTIN.Text))
            {
                if (TxtTIN.Text.Length < 10)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZInvalidTinNumber;


                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    FrmTIN.HasError = true;
                    TxtTIN.Text = string.Empty;
                    TxtTIN.Focus();


                    FrmTIN.HasError = true;
                    TxtTIN.Text = string.Empty;
                    TxtTIN.Focus();

                }
                else { FrmTIN.HasError = false; }
            }


        }

        private void TID_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtTIN.Text))
            {
                if (!string.IsNullOrEmpty(TID.Text))
                {
                    if (TID.Text.Length < 10)
                    {
                        FrmID.HasError = true;
                        TID.Focus();

                    }
                    else
                    {
                        FrmTIN.HasError = false;

                    }


                }



            }
            else
            {

                FrmID.HasError = false;

            }

        }

        private void TVatNumber_Unfocused(object sender, FocusEventArgs e)
        {
            {
                if (!string.IsNullOrEmpty(TVatNumber.Text))
                {
                    if (TVatNumber.Text.Length < 15)
                    {
                        PopUp popUp = new PopUp();
                        popUp.Message = AppResources.ZInvalidVatNumber;


                        popUp.IsLinkAvailable = false;

                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        FrmVAT.HasError = true;
                        TVatNumber.Text = string.Empty;



                    }
                    else
                    {
                        FrmVAT.HasError = false;

                    }


                }

            }
        }

        private void TReportDetail_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TReportDetail.Text))
            { FrmReportDetail.HasError = true; }
            else
            {


                FrmReportDetail.HasError = false;
            }

        }

        private void TFWType_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TFWType.Text))
            {
                FrmTFW.HasError = true;
                TFWType.Focus();
            }
            else
            {
                FrmTFW.HasError = false;
            }


        }
        public void clearFields()
        {
            TName.Text = string.Empty; TFaciName.Text = string.Empty; TFaciOwnerName.Text = string.Empty;
            TMobNumber.Text = string.Empty; TFaciMobNo.Text = string.Empty;
            TEmail.Text = string.Empty; TFaciEmail.Text = string.Empty;
            TID.Text = string.Empty; TVatNumber.Text = string.Empty; TxtTIN.Text = string.Empty;
            viewModel.IsTIN = true; viewModel.IsTINVisible = true;
            TReportDetail.Text = string.Empty; TFWType.Text = string.Empty;
            TFDAdress.Text = string.Empty; TFSAddress.Text = string.Empty;
            // DateLabel.IsVisible = false; DateLabel.IsEnabled = false; DateLabel.Text = string.Empty;
        }



        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //your code here;

            double lat = 00.00, lon = 00.00;
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    lat = location.Latitude;
                    lon = location.Longitude;
                }


                Position position = new Position(lat, lon);
                MapSpan mapSpan = new MapSpan(position, 0.0001, 0.001);
                mapView.MoveToRegion(mapSpan);
                viewModel.TEReportobj.Latitude = lat.ToString();
                viewModel.TEReportobj.Longitude = lon.ToString();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

        }


        private void mapView_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            Pin pin = new Pin();
            pin.Label = "Your Location";
            pin.Type = PinType.Place;
            pin.Position = new Position(e.Position.Latitude, e.Position.Longitude);
            viewModel.TEReportobj.Latitude = e.Position.Latitude.ToString();
            viewModel.TEReportobj.Longitude = e.Position.Longitude.ToString();
            mapView.Pins.Clear();
            mapView.Pins.Add(pin);
        }

        //    private async void mapset()
        //    {
        //        double lat=00.00, lon=00.00;
        //        try
        //        {
        //            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
        //            var location = await Geolocation.GetLocationAsync(request);

        //            if (location != null)
        //            {
        //                lat = location.Latitude;
        //                lon = location.Longitude;
        //            }


        //            Position position = new Position(lat, lon);
        //            MapSpan mapSpan = new MapSpan(position, 0.0001, 0.001);
        //            mapView.MoveToRegion(mapSpan);
        //            viewModel.TEReportobj.Latitude = lat.ToString();
        //            viewModel.TEReportobj.Longitude = lon.ToString();
        //        }
        //        catch (FeatureNotSupportedException fnsEx)
        //        {
        //            // Handle not supported on device exception
        //        }
        //        catch (FeatureNotEnabledException fneEx)
        //        {
        //            // Handle not enabled on device exception
        //        }
        //        catch (PermissionException pEx)
        //        {
        //            // Handle permission exception
        //        }
        //        catch (Exception ex)
        //        {
        //            // Unable to get location
        //        }
        //        //Pin pin = new Pin
        //        //{
        //        //    Label = "Your Location",

        //        //    Type = PinType.Place,
        //        //    Position = new Position(lat, lon)
        //        //};




        //        // Map map = new Map(mapSpan);




        //}

        private void btnFacilityType_Clicked(object sender, EventArgs e)
        {
            ddlFacilityType.IsOpen = true;
        }

        private void btnTxtReportDetailRegion_Clicked(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                RegionPickerAR.IsOpen = true;
            }
            else
            {
                RegionPicker.IsOpen = true;
            }

        }

        private void btnReportDetailCity_Clicked(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                CityPickerAR.IsOpen = true;
            }
            else
            {
                CityPicker.IsOpen = true;

            }
        }

        private void DpDbo_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;

            string month = selectedItem[0].ToString();
            string day = selectedItem[1].ToString();
            string year = selectedItem[2].ToString();

            viewModel.DatePick = day + "/" + month + "/" + year;
        }

        private void btn4_Clicked(object sender, EventArgs e)
        {
            DpDbo.IsOpen = true;
        }

        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {

        }
    }
}