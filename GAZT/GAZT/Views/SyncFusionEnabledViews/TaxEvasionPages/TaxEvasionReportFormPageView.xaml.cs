using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportFormPageView : ContentPage
    {
        private double width = 0;
        private double height = 0;
        TaxEvasionReportFormPageViewModel viewModel;
        public TaxEvasionReportFormPageView(TaxEvasionReport SelectedTaxEvasionListItem)
        {
            try
            {
                InitializeComponent();
                
                viewModel = App.Locator.TaxEvasionReportFormPageView;
               // clearFields();
                viewModel.selectedtaxEList = new TaxEvasionReport();
                viewModel.selectedtaxEList=   SelectedTaxEvasionListItem;
                ChangeAeroIcon();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                viewModel.UploadedDocumentsList = new UploadedDocumentsList();
                //viewModel.TaxEvasionReportTobeUsedToSubmit = new TaxEvasionReportTobeUsedToSubmit();
                MainLayout.Padding = new Thickness(0, 0, 0, 0);
                SetLTR();
                this.BindingContext = viewModel;
                clearFields(); ;
                viewModel.CreateCompanyTypeList();
                viewModel.onPageLoad();//TaxEvasionReport
                viewModel.SelectedCategory = viewModel.selectedtaxEList.ViolationType;
                if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.ReportNumber))
                {
                    btn4.IsEnabled = false;
                    FacilityType_entry.IsEnabled = false; btnFacilityType.IsEnabled = false; ddlFacilityType.IsEnabled = false;
                    btnReportDetailCity.IsEnabled = false; City_entry.IsEnabled = false;
                    CityPicker.IsEnabled = false;
                    CityPickerAR.IsEnabled = false;
                    btnTxtReportDetailRegion.IsEnabled = false; RegionPicker.IsEnabled = false; RegionPickerAR.IsEnabled = false; Region_entry.IsEnabled = false; Attachment_Label.IsVisible = false;
                    Attachment_Label.IsVisible = false; TFSAddress.IsEnabled = false;
                    Attachment_Tmg.IsVisible = false; Attachment_Frm.IsVisible = false; Attachment_Entry.IsVisible = false; Attachment_Tmg.IsEnabled = false;
                    checkBox.IsEnabled = false;
                    viewModel.TName = viewModel.selectedtaxEList.ReporterName; TName.IsEnabled = false;
                    viewModel.TMobNumber = viewModel.selectedtaxEList.ReporterMobileNumber.Remove(0,2); TMobNumber.IsEnabled = false;TMobNumberAr.IsEnabled = false;
                    viewModel.TEmail = viewModel.selectedtaxEList.ReporterEmail; TEmail.IsEnabled = false;
                    viewModel.TFaciName = viewModel.selectedtaxEList.CompanyName; TFaciName.IsEnabled = false;
                    TFaciOwnerName.IsEnabled = false;

                    TFaciMobNo.IsEnabled = false; TFaciMobNoAr.IsEnabled = false; TFaciEmail.IsEnabled = false;
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
                        viewModel.SelectedTaxEvasionRegion = viewModel.RList.Where(x => x.RegionCode == viewModel.selectedtaxEList.RegionCode).FirstOrDefault();
                        viewModel.onSelectedTaxEvasionRegion();
                        if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.CityCode))
                        {
                            viewModel.SelectLCType = viewModel.CList.Where(x => x.CityCode == viewModel.selectedtaxEList.CityCode).FirstOrDefault();
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
                    if (App.TP != null && !string.IsNullOrEmpty(App.TP.Name))
                    {
                        try
                        {
                            viewModel.TName = App.TP.Name;
                        }
                        catch(Exception ex)
                        {

                        }
                        
                    }
                    if (App.TP != null && !string.IsNullOrEmpty(App.TP.Email))
                    { viewModel.TEmail = App.TP.Email; }
                    if (viewModel.selectedtaxEList!=null && viewModel.selectedtaxEList.ReporterMobileNumber != null)
                    {
                        
                        string mobb = viewModel.selectedtaxEList.ReporterMobileNumber;
                        viewModel.TMobNumber = mobb;
                        TMobNumber.IsEnabled = false;
                        TMobNumberAr.IsEnabled = false;
                    }


                }
            }
            catch (Exception ex)
            {

            }
            // viewModel.SelectedCategory = SelectedCat;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                if (App.IsArabic)
                {
                    if (width > height)
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        MainLayout.Padding = new Thickness(40, 0, 40, 0);
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        MainLayout.Padding = new Thickness(0, 0, 0, 0);
                    }
                }

                //reconfigure layout
            }
        }
        //public async Task Test1()
        //{
        //   await Task.Run(() =>
        //    {
        //        viewModel.IsLoading = true;
        //    });


        //    await Task.Run(async () =>
        //    {
        //        await Test();
        //    });
        //    await Task.Run(() =>
        //    {
        //        viewModel.IsLoading = false;
        //    });
        //}

        public async Task Test()
        {
            try
            {
              await Task.Run(() =>
              {
                  viewModel.IsLoading = true;
              });

                await Task.Run(async() =>
                 {
                     viewModel.CreateCompanyTypeList();
                     viewModel.onPageLoad();//TaxEvasionReport
                     viewModel.SelectedCategory = viewModel.selectedtaxEList.ViolationType;
                     if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.ReportNumber))
                     {
                         btn4.IsEnabled = false;
                         FacilityType_entry.IsEnabled = false; btnFacilityType.IsEnabled = false; ddlFacilityType.IsEnabled = false;
                         btnReportDetailCity.IsEnabled = false; City_entry.IsEnabled = false;
                         CityPicker.IsEnabled = false;
                         CityPickerAR.IsEnabled = false;
                         btnTxtReportDetailRegion.IsEnabled = false; RegionPicker.IsEnabled = false; RegionPickerAR.IsEnabled = false; Region_entry.IsEnabled = false; Attachment_Label.IsVisible = false;
                         Attachment_Label.IsVisible = false; TFSAddress.IsEnabled = false;
                         Attachment_Tmg.IsVisible = false; Attachment_Frm.IsVisible = false; Attachment_Entry.IsVisible = false; Attachment_Tmg.IsEnabled = false;
                         checkBox.IsEnabled = false;
                         viewModel.TName = viewModel.selectedtaxEList.ReporterName; TName.IsEnabled = false;
                         viewModel.TMobNumber = viewModel.selectedtaxEList.ReporterMobileNumber; TMobNumber.IsEnabled = false;TMobNumberAr.IsEnabled = false;
                         viewModel.TEmail = viewModel.selectedtaxEList.ReporterEmail; TEmail.IsEnabled = false;
                         viewModel.TFaciName = viewModel.selectedtaxEList.CompanyName; TFaciName.IsEnabled = false;
                         TFaciOwnerName.IsEnabled = false;TFaciMobNoAr.IsEnabled = false; TFaciMobNo.IsEnabled = false; TFaciEmail.IsEnabled = false;
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
                             viewModel.SelectedTaxEvasionRegion = viewModel.RList.Where(x => x.RegionCode == viewModel.selectedtaxEList.RegionCode).FirstOrDefault();
                             viewModel.onSelectedTaxEvasionRegion();
                             if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.CityCode))
                             {
                                 viewModel.SelectLCType = viewModel.CList.Where(x => x.CityCode == viewModel.selectedtaxEList.CityCode).FirstOrDefault();
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
                         if (App.TP != null && !string.IsNullOrEmpty(App.TP.Name))
                         {
                             viewModel.TName = App.TP.Name;
                         }
                         if (App.TP != null && !string.IsNullOrEmpty(App.TP.Email))
                         { viewModel.TEmail = App.TP.Email; }
                         if (App.TP != null && !string.IsNullOrEmpty(App.TP.Mobile))
                         {
                             string mobb = App.TP.Mobile;
                             viewModel.TMobNumber = mobb.Replace("009665", string.Empty);
                         }


                     }
                 });
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch(Exception ex)
            {

            }
           
        }

        private void SetLTR()
        {
            //if (!App.IsArabic)
            //{
            //    this.FlowDirection = FlowDirection.LeftToRight;
            //    viewModel.IsVisiblePickerAr = false;
            //    viewModel.IsVisiblePickerEn = true;


            //}
            //else
            //{
            //    PickerResourceManager.Manager = new ResourceManager("GAZT.SyncfusionControl", Application.Current.GetType().Assembly);
            //    viewModel.IsVisiblePickerAr = true;
            //    viewModel.IsVisiblePickerEn = false;
            //}

            if (App.IsArabic)
            {

                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
                viewModel.IsVisiblePickerAr = true;
                viewModel.IsVisiblePickerEn = false;
                facilityMobileStackLayoutAr.IsVisible = true;
                facilityMobileStackLayout.IsVisible = false;
                reporterMobStackLayoutAr.IsVisible = true;
                reporterMobStackLayout.IsVisible = false;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    FmobcountrycodeAr.Text = "+9665";
                    TmobcountrycodeAr.Text = "+9665";              

                }
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
                viewModel.IsVisiblePickerAr = false;
                viewModel.IsVisiblePickerEn = true;
                facilityMobileStackLayout.IsVisible = true;
                facilityMobileStackLayoutAr.IsVisible = false;
                reporterMobStackLayoutAr.IsVisible = false;
                reporterMobStackLayout.IsVisible = true;
            }

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
                    flag = false; TName.Focus(); FrmName.HasError = true; showFillFeildsMessage();
                }

                else if ( string.IsNullOrEmpty(viewModel.TMobNumber))
                {
                    flag = false;
                    if (App.IsArabic)
                    { TMobNumberAr.Focus(); FrmNumberAr.HasError = true; showFillFeildsMessage(); }
                    else
                    {
                        TMobNumber.Focus(); FrmNumber.HasError = true; showFillFeildsMessage();
                    }
                    
                }
                else if (string.IsNullOrEmpty(TFaciName.Text))
                { flag = false; TFaciName.Focus(); FrmFName.HasError = true; showFillFeildsMessage(); }
                else if (string.IsNullOrEmpty(FacilityType_entry.Text))
                { flag = false; FrmFType.HasError = true; ddlFacilityType.IsOpen = true; showFillFeildsMessage(); }
                else if (string.IsNullOrEmpty(TFDAdress.Text))
                { flag = false; FrmFDAddress.HasError = true; TFDAdress.Focus(); showFillFeildsMessage(); }
                else if (string.IsNullOrEmpty(TFSAddress.Text))
                { flag = false; FrmFSAddress.HasError = true; showFillFeildsMessage(); TFSAddress.Focus(); }
                else if (string.IsNullOrEmpty(TxtTIN.Text) && checkBox.IsChecked == true)
                {  flag = false; TxtTIN.Focus(); FrmTIN.HasError = true; showFillFeildsMessage(); }

                else if (string.IsNullOrEmpty(TReportDetail.Text))
                { flag = false; FrmReportDetail.HasError = true; TReportDetail.Focus(); showFillFeildsMessage(); }
                else if (string.IsNullOrEmpty(TFWType.Text))
                { flag = false; FrmTFW.HasError = true; TFWType.Focus(); showFillFeildsMessage(); }
                else if (string.IsNullOrEmpty(Region_entry.Text))

                {
                    flag = false; frmRegionPicker.HasError = true;
                    if (App.IsArabic)
                    {
                        RegionPickerAR.IsOpen = true;
                    }
                    else
                    {
                        RegionPicker.IsOpen = true;
                    }
                }

                else if (string.IsNullOrEmpty(City_entry.Text))
                {
                    flag = false; FrmCity.HasError = true;
                    showFillFeildsMessage();
                    if (App.IsArabic)
                    {
                        CityPickerAR.IsOpen = true;
                    }
                    else
                    {
                        CityPicker.IsOpen = true;

                    }

                }

                else if (string.IsNullOrEmpty(Date_entry.Text))
                {
                    showFillFeildsMessage();
                    flag = false; FrmDBO.HasError = true; DpDbo.IsOpen = true;
                }

                else
                {
                    if (flag == true)
                    { await viewModel.SubmitCreatedReport(); }
                }

            }
        }

        private void TMobNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (App.IsArabic)
            {
                if (!string.IsNullOrEmpty(TMobNumber.Text))
                {
                    if (TMobNumberAr.Text.Length < 8)
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
                        FrmNumberAr.HasError = true;
                        TMobNumberAr.Text = string.Empty;




                    }

                    else
                    {
                        FrmNumberAr.HasError = false;
                    }
                }


            }
            else
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




                    }

                    else
                    {
                        FrmNumber.HasError = false;
                    }
                }

            }

        }

        private void TFaciMobNo_Unfocused(object sender, FocusEventArgs e)
        {
            if (App.IsArabic)
            {
                if (!string.IsNullOrEmpty(TFaciMobNoAr.Text))
                {
                    if (TFaciMobNoAr.Text.Length < 8)
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
                        FrmFMobNoAr.HasError = true;
                        TFaciMobNoAr.Text = string.Empty;
                        TFaciMobNoAr.Focus();
                    }

                    else
                    {
                        FrmFMobNoAr.HasError = false;
                    }
                }


            }
            else
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
                    popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;


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

                FrmFOName.HasError = false;
            }
           
            


        }

        private void TFaciEmail_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(TFaciEmail.Text))
            {
                bool flag = IsValid(TFaciEmail.Text);
                if (!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;


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
             
                

            }
            else
            { FrmFDAddress.HasError = false; }

        }

        private void TFSAddress_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TFSAddress.Text))
            {
                FrmFSAddress.HasError = true;
            }
            else
            {
                FrmFSAddress.HasError = false;
            }
        }

        private void TxtTIN_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtTIN.Text))
            {
                
                if (TxtTIN.Text.Length < 10 ||  (TxtTIN.Text.Substring(0, 1) != "3"))
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

                else
                { 
                    FrmTIN.HasError = false; 
                }
            }


        }

        private void TID_Unfocused(object sender, FocusEventArgs e)
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
                    FrmID.HasError = false;

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
            {
                FrmReportDetail.HasError = true; 
            
            }
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
          

            try
            {
                //viewModel.SelectedTaxEvasionCompanyType = null;
                //viewModel.SelectedTaxEvasionRegion = null;
                //viewModel.SelectLCType = null;
                //viewModel.UploadedDocumentsListObj = null;
                viewModel.UploadedDocumentsListObj.Clear();
                Attachment_Entry.Text = string.Empty;
                FacilityType_entry.Text = string.Empty;
                Date_entry.Text = string.Empty;
                City_entry.Text = string.Empty;
                Region_entry.Text = string.Empty;
                TName.Text = string.Empty;
                TFaciName.Text = string.Empty;
                TFaciOwnerName.Text = string.Empty;
                TMobNumber.Text = string.Empty;
                TMobNumberAr.Text = string.Empty;
                TFaciMobNo.Text = string.Empty;
                TFaciMobNoAr.Text = string.Empty;
                TEmail.Text = string.Empty;
                TFaciEmail.Text = string.Empty;
                TID.Text = string.Empty;
                TVatNumber.Text = string.Empty;
                TxtTIN.Text = string.Empty;
                viewModel.IsTIN = true;
                viewModel.IsTINVisible = true;
                TReportDetail.Text = string.Empty;
                TFWType.Text = string.Empty;
                TFDAdress.Text = string.Empty;
                TFSAddress.Text = string.Empty;
                //DateLabel.IsVisible = false; DateLabel.IsEnabled = false; DateLabel.Text = string.Empty;

            }
            catch (Exception ex)
            {

            }
                }



        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //your code here;
        
            if (!((viewModel.selectedtaxEList != null) && string.IsNullOrEmpty(viewModel.selectedtaxEList.ReportNumber)))
            {
                viewModel.IsLoading = false;
                //clearFields();

            }
            else
            {
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
                    viewModel.Latitude = lat;
                    viewModel.Longitude = lon;
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

               

        }


        private async void mapView_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            if ((viewModel.selectedtaxEList != null) && string.IsNullOrEmpty(viewModel.selectedtaxEList.ReportNumber))
            {
                //   viewModel.IsLoading = false;
                //clearFields();
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {





                        Position position = new Position(location.Latitude, location.Longitude);
                        MapSpan mapSpan = new MapSpan(position, 0.0001, 0.001);
                        mapView.MoveToRegion(mapSpan);
                        viewModel.Latitude = location.Latitude;
                        //viewModel.TEReportobj.Latitude = location.Latitude.ToString();
                        viewModel.Longitude = location.Longitude;
                    }
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





                Pin pin = new Pin();
                pin.Label = "Your Location";
                pin.Type = PinType.Place;
                pin.Position = new Position(e.Position.Latitude, e.Position.Longitude);
                viewModel.Latitude = e.Position.Latitude;
                viewModel.Longitude = e.Position.Longitude;
                mapView.Pins.Clear();
                mapView.Pins.Add(pin);

            }
            else 
            { 
            
            
            }

            
        }

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
            if (!string.IsNullOrEmpty(Region_entry.Text))
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
            else
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
            
        }

        private void DpDbo_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;

            string month = selectedItem[0].ToString();
            string day = selectedItem[1].ToString();
            string year = selectedItem[2].ToString();

            viewModel.DatePick = day + "/" + month + "/" + year;
            viewModel.DatePickPrev = day + "/" + month + "/" + year;
        }

        private void btn4_Clicked(object sender, EventArgs e)
        {
            DpDbo.IsOpen = true;
        }

        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void ddlFacilityType_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            FacilityCompanyType selectedcompanytyp = (FacilityCompanyType)e.NewValue;
            ddlFacilityType.SelectedItem = selectedcompanytyp;
            viewModel.SelectedTaxEvasionCompanyType = selectedcompanytyp;
            viewModel.SelectedTaxEvasionCompanyTypePrev = selectedcompanytyp;
            viewModel.TxtFType = selectedcompanytyp.Name;

            FrmFType.HasError = false;
        }

        private void RegionPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TERRegion selectedregion = (TERRegion)e.NewValue;
            RegionPicker.SelectedItem = selectedregion;
            viewModel.SelectedTaxEvasionRegion = selectedregion;//selectedregion
            viewModel.SelectedTaxEvasionRegionPrev = selectedregion;//selectedregion
            viewModel.TxtReportDetailRegion = selectedregion.RegionNameEN;
            frmRegionPicker.HasError = false;
            //SelectedTaxEvasionRegion
        }

        private void RegionPickerAR_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TERRegion selectedregion = (TERRegion)e.NewValue;
            RegionPickerAR.SelectedItem = selectedregion;
            viewModel.SelectedTaxEvasionRegion = selectedregion;//selectedregion
            viewModel.SelectedTaxEvasionRegionPrev = selectedregion;//selectedregion
            viewModel.TxtReportDetailRegion = selectedregion.RegionNameAR;
            frmRegionPicker.HasError = false;
        }

        private void CityPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TERCity selectedcity = (TERCity)e.NewValue;
            CityPicker.SelectedItem = selectedcity;
            viewModel.SelectLCType = selectedcity;//selectedregion
            viewModel.SelectLCTypePrev = selectedcity;//selectedregion
            viewModel.TxtReportDetailCity = selectedcity.CityNameEN;
            FrmCity.HasError = false;
        }

        private void CityPickerAR_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TERCity selectedcity = (TERCity)e.NewValue;
            CityPickerAR.SelectedItem = selectedcity;
            viewModel.SelectLCType = selectedcity;//selectedregion
            viewModel.SelectLCTypePrev = selectedcity;//selectedregion
            viewModel.TxtReportDetailCity = selectedcity.CityNameAR;
            FrmCity.HasError = false;
        }

        private void TFaciName_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TFaciName.Text))
            { FrmFName.HasError = true; }
            else 
            {
                FrmFName.HasError = false;
            }
        }

        private void Date_entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Date_entry.Text))
            {
                FrmDBO.HasError = false;
            }

        }

        private void DpDbo_Closed(object sender, EventArgs e)
        {
            var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;

            string month = selectedItem[0].ToString();
            string day = selectedItem[1].ToString();
            string year = selectedItem[2].ToString();

            viewModel.DatePick = day + "/" + month + "/" + year;
        }

        private void showFillFeildsMessage()
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZZPleasefillallthemandatoryfields;


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

        private void ddlFacilityType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectedTaxEvasionCompanyType = viewModel.SelectedTaxEvasionCompanyTypePrev;
        }

        private void RegionPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectedTaxEvasionRegion = viewModel.SelectedTaxEvasionRegionPrev;
        }

        private void RegionPickerAR_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectedTaxEvasionRegion = viewModel.SelectedTaxEvasionRegionPrev;
        }

        private void CityPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectLCType = viewModel.SelectLCTypePrev;
        }

        private void CityPickerAR_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectLCType = viewModel.SelectLCTypePrev;
        }

        private void DpDbo_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.DatePick = viewModel.DatePickPrev;
            if (!string.IsNullOrEmpty(viewModel.DatePickPrev))
            {
                string[] Date = viewModel.DatePickPrev.Split('/');
                ObservableCollection<object> todaycollection = new ObservableCollection<object>();

                //Select today dates

                todaycollection.Add(Date[1]);
                todaycollection.Add(Date[2]);
                todaycollection.Add(Date[0]);

                DpDbo.SelectedItem = todaycollection;

            }
        }

        private void DpDbo_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            FrmDBO.HasError = false;
            try
            {

                var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[0].ToString();
                string day = selectedItem[1].ToString();
                string year = selectedItem[2].ToString();
                viewModel.DatePick = year + "/" + month + "/" + day;
            }
            catch (Exception ex)
            {

            }
        }
    }
}