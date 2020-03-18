using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionReportFormPageView : ContentPage
    {

        TaxEvasionReportFormPageViewModel viewModel;
        

        public TaxEvasionReportFormPageView(TaxEvasionReportList SelectedTaxEvasionListItem)
        {

            viewModel = App.Locator.TaxEvasionReportFormPageView;
            InitializeComponent();
            SetLTR();
            this.BindingContext = viewModel;


            viewModel.CreateCompanyTypeList();
            //selectedtaxEList
            viewModel.onPageLoad();
            viewModel.selectedtaxEList = SelectedTaxEvasionListItem;
            viewModel.SelectedCategory =SelectedTaxEvasionListItem.ViolationType;
            //TName.Text = selectedtaxEList.ReporterName;
            if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.ReportNumber))
            {

                viewModel.TName = viewModel.selectedtaxEList.ReporterName; TName.IsEnabled = false;
                viewModel.TMobNumber = viewModel.selectedtaxEList.ReporterMobileNumber; TMobNumber.IsEnabled = false;
                viewModel.TEmail = viewModel.selectedtaxEList.ReporterEmail; TEmail.IsEnabled = false;
                viewModel.TFaciName = viewModel.selectedtaxEList.CompanyName; TFaciName.IsEnabled = false;
                //viewModel.TFaciMobNo= selectedtaxEList.
                /*viewModel.SelectedTaxEvasionCompanyType.Id= selectedtaxEList.c*/              ///*viewModel.TFaciOwnerName=selectedtaxEList.*/
                TFaciOwnerName.IsEnabled = false; TFaciMobNo.IsEnabled = false; TFaciEmail.IsEnabled = false;
                RegionPicker.IsEnabled = false;RegionPickerAR.IsEnabled = false;CityPicker.IsEnabled = false;CityPickerAR.IsEnabled = false;
                viewModel.TFDAdress = viewModel.selectedtaxEList.District; TFDAdress.IsEnabled = false;
                viewModel.TFWType = viewModel.selectedtaxEList.WorkType; TFWType.IsEnabled = false;
                viewModel.TFSAddress = viewModel.selectedtaxEList.CompanyAddress; TFDAdress.IsEnabled = false;
                viewModel.IsSubmitButtonEnable = false; submit_btnmane.IsEnabled = false; submit_btnmane.BackgroundColor = Color.Gray;
                viewModel.TReportDetail = viewModel.selectedtaxEList.ReportDetails; TReportDetail.IsEnabled = false;
                viewModel.TVatNumber = viewModel.selectedtaxEList.VATNumber; TVatNumber.IsEnabled = false;
                RegionPicker.IsEnabled = false; RegionPickerAR.IsEnabled = false; CityPicker.IsEnabled = false; CityPickerAR.IsEnabled = false;
                if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.RegionCode))
                {
                    viewModel.SelectedTaxEvasionRegion = viewModel.RList.Where(x => x.RegionCode == SelectedTaxEvasionListItem.RegionCode).FirstOrDefault();
                    viewModel.onSelectedTaxEvasionRegion();
                    if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.CityCode))
                    { viewModel.SelectLCType = viewModel.CList.Where(x => x.CityCode == SelectedTaxEvasionListItem.CityCode).FirstOrDefault(); }
                
                }

                
                //viewModel.SelectedTaxEvasionRegion = selectedtaxEList;
                //viewModel.SelectLCType.CityCode = selectedtaxEList.CityCode;
                //viewModel.SelectLCType.Latitude = selectedtaxEList.Latitude;

                //if (!App.IsArabic)
                //{ viewModel.SelectLCType.CityNameEN = selectedtaxEList.CityNameEn; }
                //else {  viewModel.SelectLCType.CityNameAR = selectedtaxEList.CityNameAr;}



                if (!string.IsNullOrEmpty(viewModel.selectedtaxEList.TIN))
                { viewModel.IsTIN = true; viewModel.TxtTIN = viewModel.selectedtaxEList.TIN; viewModel.IsTINVisible = true; }
                else {
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

        public void clearData()
        {

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
                viewModel.IsVisiblePickerAr = true;
                viewModel.IsVisiblePickerEn = false;


            }
        }






        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (App.IsArabic)
            { RegionPickerAR.Focus();



            }
            else
            { RegionPicker.Focus();

            }



        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            if (App.IsArabic)
            { CityPickerAR.Focus();
                viewModel.IsVisiblePickerAr = true;
                viewModel.IsVisiblePickerEn = false;
            }
            else
            { CityPicker.Focus();

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
                else if (string.IsNullOrEmpty(TEmail.Text))
                { flag = false; FrmEmail.Focus(); }
                else if (string.IsNullOrEmpty(TFaciName.Text))
                { flag = false; FrmFName.Focus(); }
                else if (string.IsNullOrEmpty(TFaciOwnerName.Text))
                { flag = false; FrmFOName.Focus(); }
                else if (string.IsNullOrEmpty(TFaciMobNo.Text))
                { flag = false; FrmFMobNo.Focus(); }
                else if (string.IsNullOrEmpty(TFaciEmail.Text))
                { flag = false; FrmFEmail.Focus(); }
                else if (string.IsNullOrEmpty(TFDAdress.Text))
                { flag = false; FrmFDAddress.Focus(); }
                else if (string.IsNullOrEmpty(TFSAddress.Text))
                { flag = false; FrmFSAddress.Focus(); }
                else if (string.IsNullOrEmpty(TxtTIN.Text) && string.IsNullOrEmpty(TID.Text))
                { flag = false; FrmID.Focus(); }
                else if (string.IsNullOrEmpty(TReportDetail.Text))
                { flag = false; FrmReportDetail.Focus(); }
                else if (string.IsNullOrEmpty(TFWType.Text))
                { flag = false; FrmTFW.Focus(); }

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
                if (TMobNumber.Text.Length<8)
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
                    FrmNumber.BorderColor = Color.Red;
                    TMobNumber.Text = string.Empty;
                    TMobNumber.Focus();
                }
               
                else
                {
                    FrmNumber.BorderColor = Color.FromHex("#B1B1B1");
                }
            }

        }

        private void TFaciMobNo_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(TFaciMobNo.Text))
            {
                if (TFaciMobNo.Text.Length< 8)
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
                    FrmFMobNo.BorderColor = Color.Red;
                    TFaciMobNo.Text = string.Empty;
                    TFaciMobNo.Focus();
                }

                else
                {
                    FrmFMobNo.BorderColor = Color.FromHex("#B1B1B1");
                }
            }

        }

        private void TName_Unfocused(object sender, FocusEventArgs e)
        { if (string.IsNullOrEmpty(TName.Text))
            { FrmName.BorderColor = Color.Red;

                TName.Text = string.Empty;
            }
            else
            { FrmName.BorderColor = Color.FromHex("#B1B1B1"); }


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
                    FrmEmail.BorderColor = Color.Red;
                    TEmail.Text = string.Empty;
                }
                else
                {
                    FrmEmail.BorderColor = Color.FromHex("#B1B1B1");
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
        { if (string.IsNullOrEmpty(TFaciOwnerName.Text))
            {
                FrmFOName.BorderColor = Color.Red;
                TFaciOwnerName.Text = string.Empty;
                TFaciOwnerName.Focus();
            }
            else
            { FrmFOName.BorderColor = Color.FromHex("#B1B1B1"); }


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
                    FrmFEmail.BorderColor = Color.Red;
                    TFaciEmail.Text = string.Empty;
                }
                else
                {
                    FrmFEmail.BorderColor = Color.FromHex("#B1B1B1");
                }
            }



        }

        private void TFDAdress_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(TFDAdress.Text))
            {
                FrmFDAddress.BorderColor = Color.Red;
                TFDAdress.Text = string.Empty;
                TFDAdress.Focus();

            }
            else
            { FrmFDAddress.BorderColor = Color.FromHex("#B1B1B1"); }

        }

        private void TFSAddress_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void TxtTIN_Unfocused(object sender, FocusEventArgs e)
        { if (!string.IsNullOrEmpty(TxtTIN.Text))
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
                    FrmTIN.BorderColor = Color.Red;
                    TxtTIN.Text = string.Empty;
                    TxtTIN.Focus();


                    FrmTIN.BorderColor = Color.Red;
                    TxtTIN.Text = string.Empty;
                    TxtTIN.Focus();

                }
                else { FrmTIN.BorderColor = Color.FromHex("#B1B1B1"); }
            }


        }

        private void TID_Unfocused(object sender, FocusEventArgs e)
        { if (string.IsNullOrEmpty(TxtTIN.Text))
            {
                if (!string.IsNullOrEmpty(TID.Text))
                { if (TID.Text.Length < 10)
                    {
                        FrmID.BorderColor = Color.Red;
                        TID.Focus();

                    }
                    else
                    {
                        FrmTIN.BorderColor = Color.FromHex("#B1B1B1");

                    }


                }



            }
            else
            {

                FrmID.BorderColor = Color.FromHex("#B1B1B1");

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
                        FrmVAT.BorderColor = Color.Red;
                        TVatNumber.Text = string.Empty;
                        
                       

                    }
                    else
                    {
                        FrmVAT.BorderColor = Color.FromHex("#B1B1B1");

                    }


                }

            }
        }

            private void TReportDetail_Unfocused(object sender, FocusEventArgs e)
            {
                if (string.IsNullOrEmpty(TReportDetail.Text))
                { FrmReportDetail.BorderColor = Color.Red; }
                else {


                    FrmReportDetail.BorderColor = Color.FromHex("#B1B1B1");
                }

            }

            private void TFWType_Unfocused(object sender, FocusEventArgs e)
            {
                if (string.IsNullOrEmpty(TFWType.Text))
                { FrmTFW.BorderColor = Color.Red;
                    TFWType.Focus();
                }
                else
                {
                    FrmTFW.BorderColor = Color.FromHex("#B1B1B1");
                }


            }
        }
    } 