using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Syncfusion.DataSource;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinancialDetailAttachmentPopupPageView : PopupPage
    {
        FinancialDetailAttachmentPopupPageViewModel viewModel;
        public FinancialDetailAttachmentPopupPageView(DataToPassTofinancialDetailAttachmentPopup sendtoPopup)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.FinancialDetailAttachmentPopupPageView;
                this.BindingContext = viewModel;
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                SetLTR();
                viewModel.VATRegistrationDetailsData = new VATRegistrationDetails();
                viewModel.VATRegistrationDetailsData = sendtoPopup.VATRegistrationDetailsDatatoPopup;
                viewModel.VATRegistrationOtherDetails = new VATRegistrationOtherDetails();
                viewModel.VATRegistrationOtherDetails = sendtoPopup.vatRegOthrDetailtoPopup;
                viewModel.ELGBL_DOCSet = new ELGBL_DOCSet();
                viewModel.ResultsItemForDOCSet = new List<ResultsItemForDOCSet>();
                viewModel.VATRegistrationDetailsForAttach = new VATRegistrationDetails();
                viewModel.VATRegistrationDetailsForAttach= sendtoPopup.VATRegistrationDetailsDatatoPopup;
                viewModel.ELGBL_DOCSet = viewModel.VATRegistrationOtherDetails.d.ELGBL_DOCSet;
                viewModel.ResultsItemForDOCSet = viewModel.ELGBL_DOCSet.results;
                onPageLoad();
                
                if (viewModel.ResultsItemForDOCSet != null)
                {
                    viewModel.SelectedResultsItemForDOCSet = viewModel.ResultsItemForDOCSet.FirstOrDefault();
                    AttachmentTypePicker.SelectedItem = "1";
                }
               
               
            }
            catch (Exception ex)
            { 
            
            }
            
        }
        public void onPageLoad()
        {
            //viewModel.IsComeFromForAttachment = VATRegistrationPageViewModel.IsComeFromForAttachment;
            if (viewModel.VATRegistrationDetailsForAttach != null && viewModel.VATRegistrationDetailsForAttach.d != null)
            {

               // viewModel.VATRegistrationDetailsForAttach = vATRegistrationDetails;
                //SetDocType();
                if (viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results.Count != 0)
                {
                    ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results as List<Attachment>);
                    viewModel.VatAttachmentsList = myCollection;
                    int AttachmentCount = 0;
                    foreach (var item in viewModel.VatAttachmentsList)
                    {
                        if (item.Erfdt != null && item.Erftm != null)
                        {
                            item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        }
                    }
                    viewModel.filterList();
                    viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
                }
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send<Object, ATTDETSet>(this, "AttachmentReceived", viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet);
            //comment because main button remains enabled
            //  viewModel.IsSwichButtonEnable = false;
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
        }

        private async void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    Image arrowImage = sender as Image;
                    VATAttachment attachment = (VATAttachment)arrowImage.BindingContext;
                    //if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
                    //{

                    if (attachment != null)
                    {
                        var result = await this.DisplayAlert(AppResources.ZZDELETEFILE, AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + "?", AppResources.ZZZOkayText, AppResources.ZZCancel);
                        DeleteAttachment(result, attachment);
                    }

                    //}
                }
                catch (Exception ex)
                {

                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
        public async Task DeleteAttachment(bool result, VATAttachment attachment)
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await Task.Run(() =>
                {
                    if (result)
                    {
                        // int indexToReduceTheSize = viewModel.GetDeletedAttachmentIndex(attachment);
                        string results = WebServiceManager.GAZTDeleteVATDeclarationAttachment(attachment.Filename, attachment.Doguid);
                        PopToRootPage();
                        if (results == "X")
                        {
                            Attachment listitem = (from itm in viewModel.VatAttachmentsList
                                                   where itm.Doguid == attachment.Doguid.ToString()
                                                   select itm)
                                            .FirstOrDefault<Attachment>();

                            VATAttachment listitemTwo = (from itm in viewModel.AttachmentList
                                                         where itm.Doguid == attachment.Doguid.ToString()
                                                         select itm)
                                            .FirstOrDefault<VATAttachment>();

                            viewModel.VatAttachmentsList.Remove(listitem);
                            viewModel.AttachmentList.Remove(listitemTwo);
                            viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results.Remove(listitem);
                            //if (indexToReduceTheSize != -1)
                            // viewModel.ReduceTotalAttachmentSize(indexToReduceTheSize);
                        }
                    }
                });
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception ex)
            {
            }
        }
        private void OnDownloadAttachmentClicked(object sender, EventArgs e)
        {

        }
        private void DDlIDType_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {

                ResultsItemForDOCSet selectedtyp = (ResultsItemForDOCSet)e.NewValue;

                AttachmentTypePicker.SelectedItem = selectedtyp;
                //viewModel.SelectedResultsItemForDOCSet = null;
                viewModel.SelectedResultsItemForDOCSet = selectedtyp;
                viewModel.AttachmentTypeTxt = selectedtyp.Txt50;
                viewModel.DocTypeString = selectedtyp.DmsTp;
                viewModel.VatAttachmentsList.Clear();
                viewModel.filterList();
                viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
                
            }
            catch (Exception ex)
            {



              


            }

        }













        private void DDlIDType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
 
        }

        private void DDlIDType_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try { 
           // SelectedResultsItemForDOCSet
                 // ResultsItemForDOCSet selectedtyp = (ResultsItemForDOCSet)e.NewValue;

            //AttachmentTypePicker.SelectedItem = selectedtyp;
            //viewModel.SelectedResultsItemForDOCSet = null;
            //viewModel.SelectedResultsItemForDOCSet = selectedtyp;
            //viewModel.AttachmentTypeTxt = selectedtyp.Txt50;
            }
            catch (Exception ex) 
            {

            }
            }

        private void btn1_Clicked(object sender, EventArgs e)
        {
            try
            {
                AttachmentTypePicker.IsOpen = true;
            }
            catch (Exception ex)
            { 
            
            }
            
        }

        private void btnAddAccount_Clicked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Xamarin.Forms.Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
     
        }
        public string generatejson()
        {

            string json = "\"{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/VRNHSet(Euser='00000000000000000000',Fbguid='',Fbnumz='60000156585',Gpartz='3102439536',Langz='EN',Officerz='',PortalUsrz='',TxnTpz='CRE_RGVT')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/VRNHSet(Euser='00000000000000000000',Fbguid='',Fbnumz='60000156585',Gpartz='3102439536',Langz='EN',Officerz='',PortalUsrz='',TxnTpz='CRE_RGVT')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.VRNH\\\"},\\\"AgrFg\\\":\\\"1\\\",\\\"Decname\\\":\\\"\\\",\\\"SmartReg\\\":\\\"\\\",\\\"Source\\\":\\\"Individuals VAT Registration\\\",\\\"ConfTaxDt\\\":null,\\\"CrNm\\\":\\\"Onkar .\\\",\\\"CrNo\\\":\\\"132334324324324\\\",\\\"CrStdt\\\":\\\"\\\\/Date(1514757515000)\\\\/\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"Decconno\\\":\\\"\\\",\\\"Decdate\\\":null,\\\"Decdesignation\\\":\\\"\\\",\\\"Decfg\\\":\\\"\\\",\\\"DecidNo\\\":\\\"\\\",\\\"DecidTy\\\":\\\"\\\",\\\"Euser\\\":\\\"00000000000000000000\\\",\\\"ExAttch\\\":\\\"\\\",\\\"ExFg\\\":\\\"0\\\",\\\"Fbguid\\\":\\\"\\\",\\\"Fbnumz\\\":\\\"60000156585\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36938A\\\",\\\"Formprocz\\\":\\\"ZTAX_VT_REG\\\",\\\"FutureDt\\\":null,\\\"GlobalCalTy\\\":\\\"\\\",\\\"GoLiveDt\\\":\\\"\\\\/Date(1514757515000)\\\\/\\\",\\\"Gpartz\\\":\\\"3102439536\\\",\\\"Iban\\\":\\\"\\\",\\\"ImAttch\\\":\\\"\\\",\\\"ImFg\\\":\\\"1\\\",\\\"Langz\\\":\\\"EN\\\",\\\"Mandt\\\":\\\"330\\\",\\\"NewRegTy\\\":\\\"\\\",\\\"NewRegTyFrDt\\\":null,\\\"Officerz\\\":\\\"\\\",\\\"Operationz\\\":\\\"05\\\",\\\"OptIban\\\":\\\"SA5655000000097072800149\\\",\\\"PortalUsrz\\\":\\\"\\\",\\\"ReaFg\\\":\\\"\\\",\\\"Reason\\\":\\\"\\\",\\\"RegTy\\\":\\\"N\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"ReturnIdz\\\":\\\"005056B1F8FB1EDAB0C4095D6097928C\\\",\\\"Statusz\\\":\\\"E0013\\\",\\\"StepNumberz\\\":\\\"2\\\",\\\"Stp2Cbbox\\\":\\\"\\\",\\\"Stp3Cbbox\\\":\\\"\\\",\\\"Stp4Cbbox1\\\":\\\"\\\",\\\"Stp4Cbbox2\\\":\\\"\\\",\\\"TinNm\\\":\\\"Onkar .\\\",\\\"ToSflg\\\":\\\"\\\",\\\"TxnTpz\\\":\\\"CRE_RGVT\\\",\\\"UserTypz\\\":\\\"TP\\\",\\\"VatDt\\\":null,\\\"VatTaxDt\\\":\\\"\\\\/Date(1594763915000)\\\\/\\\",\\\"QUESLISTSet\\\":[],\\\"IBANSet\\\":[],\\\"ATTDETSet\\\":[],\\\"QUESTIONSSet\\\":[{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('001')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('001')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":1,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"001\\\",\\\"QoptNo\\\":\\\"011\\\",\\\"QoptTxt\\\":\\\"Less than 187,500 SAR\\\",\\\"QoptAns\\\":\\\"1\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('001')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('001')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":2,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"001\\\",\\\"QoptNo\\\":\\\"012\\\",\\\"QoptTxt\\\":\\\"Greater than 187,500 until 375,000 SAR\\\",\\\"QoptAns\\\":\\\"0\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('001')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('001')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":3,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"001\\\",\\\"QoptNo\\\":\\\"013\\\",\\\"QoptTxt\\\":\\\"Greater than 375,000 until 1,000,000 SAR\\\",\\\"QoptAns\\\":\\\"0\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('001')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('001')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":4,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"001\\\",\\\"QoptNo\\\":\\\"014\\\",\\\"QoptTxt\\\":\\\"Greater than 1,000,000 until 40,000,000 SAR\\\",\\\"QoptAns\\\":\\\"0\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('001')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('001')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":5,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"001\\\",\\\"QoptNo\\\":\\\"015\\\",\\\"QoptTxt\\\":\\\"Greater than 40,000,000 SAR\\\",\\\"QoptAns\\\":\\\"0\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('002')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('002')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":6,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"002\\\",\\\"QoptNo\\\":\\\"021\\\",\\\"QoptTxt\\\":\\\"Less than 187,500 SAR\\\",\\\"QoptAns\\\":\\\"1\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('002')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('002')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":7,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"002\\\",\\\"QoptNo\\\":\\\"022\\\",\\\"QoptTxt\\\":\\\"Greater than 187,500 until 375,000 SAR\\\",\\\"QoptAns\\\":\\\"0\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('002')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('002')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":8,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"002\\\",\\\"QoptNo\\\":\\\"023\\\",\\\"QoptTxt\\\":\\\"Greater than 375,000 until 1,000,000 SAR\\\",\\\"QoptAns\\\":\\\"0\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('002')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('002')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":9,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"002\\\",\\\"QoptNo\\\":\\\"024\\\",\\\"QoptTxt\\\":\\\"Greater than 1,000,000 until 40,000,000 SAR\\\",\\\"QoptAns\\\":\\\"0\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('002')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('002')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":10,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"002\\\",\\\"QoptNo\\\":\\\"025\\\",\\\"QoptTxt\\\":\\\"Greater than 40,000,000 SAR\\\",\\\"QoptAns\\\":\\\"0\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('003')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('003')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":11,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"003\\\",\\\"QoptNo\\\":\\\"031\\\",\\\"QoptTxt\\\":\\\"Less than 187,500 SAR\\\",\\\"QoptAns\\\":\\\"1\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('003')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('003')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":12,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"003\\\",\\\"QoptNo\\\":\\\"032\\\",\\\"QoptTxt\\\":\\\"Greater than or equal to 187,500 SAR\\\",\\\"QoptAns\\\":\\\"0\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('004')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('004')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":13,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"004\\\",\\\"QoptNo\\\":\\\"041\\\",\\\"QoptTxt\\\":\\\"Less than 187,500 SAR\\\",\\\"QoptAns\\\":\\\"1\\\"},{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('004')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/QUESTIONSSet('004')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.QUESTIONS\\\"},\\\"Mandt\\\":\\\"330\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36738A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":14,\\\"RankingOrder\\\":\\\"99\\\",\\\"ResidencyTy\\\":\\\"R\\\",\\\"QueNo\\\":\\\"004\\\",\\\"QoptNo\\\":\\\"042\\\",\\\"QoptTxt\\\":\\\"Greater than or equal to 187,500 SAR\\\",\\\"QoptAns\\\":\\\"0\\\"}],\\\"CONTACT_PERSONSet\\\":[{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/CONTACT_PERSONSet(1)\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/CONTACT_PERSONSet(1)\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.CONTACT_PERSON\\\"},\\\"TransactionType\\\":\\\"\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36338A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":1,\\\"RankingOrder\\\":\\\"99\\\",\\\"Srcidentify\\\":\\\"T002\\\",\\\"Gpart\\\":\\\"\\\",\\\"Enddt\\\":\\\"\\\\/Date(253402207115000)\\\\/\\\",\\\"Contacttp\\\":\\\"\\\",\\\"Defaultfg\\\":false,\\\"Startdt\\\":null,\\\"Firstnm\\\":\\\"\\\",\\\"Lastnm\\\":\\\"\\\",\\\"Relationtp\\\":\\\"\\\",\\\"Fathernm\\\":\\\"\\\",\\\"Grandfathernm\\\":\\\"\\\",\\\"Familynm\\\":\\\"\\\",\\\"Dobdt\\\":null,\\\"StartdtC\\\":\\\"\\\",\\\"Type\\\":\\\"\\\",\\\"Idnumber\\\":\\\"\\\",\\\"Title\\\":\\\"\\\",\\\"Initials\\\":\\\"\\\"}],\\\"ELGBL_DOCSet\\\":[{\\\"__metadata\\\":{\\\"id\\\":\\\"HTTPS://SAPGATEWAYQA.GAZT.GOV.SA/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/ELGBL_DOCSet(0)\\\",\\\"uri\\\":\\\"HTTPS://SAPGATEWAYQA.GAZT.GOV.SA/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/ELGBL_DOCSet(0)\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.ELGBL_DOC\\\"},\\\"FormGuid\\\":\\\"\\\",\\\"DataVersion\\\":\\\"\\\",\\\"Mandt\\\":\\\"\\\",\\\"LineNo\\\":0,\\\"RankingOrder\\\":\\\"\\\",\\\"Fbtyp\\\":\\\"\\\",\\\"TxnTp\\\":\\\"CHG_RGVT\\\",\\\"DmsTp\\\":\\\"ZVTF\\\",\\\"DmsTxt\\\":\\\"Audited Reports\\\"}],\\\"CONTACTDTSet\\\":[{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/CONTACTDTSet(1)\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/CONTACTDTSet(1)\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.CONTACTDT\\\"},\\\"TransactionType\\\":\\\"\\\",\\\"FormGuid\\\":\\\"005056B1F8FB1EDAB0C486374A36138A\\\",\\\"DataVersion\\\":\\\"00001\\\",\\\"LineNo\\\":1,\\\"RankingOrder\\\":\\\"99\\\",\\\"Srcidentify\\\":\\\"T002\\\",\\\"Consnumber\\\":\\\"000\\\",\\\"Begda\\\":null,\\\"Endda\\\":\\\"\\\\/Date(253402207115000)\\\\/\\\",\\\"TelNumber\\\":\\\"\\\",\\\"R3User\\\":\\\"\\\",\\\"SmtpAddr\\\":\\\"\\\",\\\"MobNumber\\\":\\\"\\\"}],\\\"NOTESSet\\\":[],\\\"QUESCONFIG_MSet\\\":{\\\"__deferred\\\":{\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/VRNHSet(Euser='00000000000000000000',Fbguid='',Fbnumz='60000156585',Gpartz='3102439536',Langz='EN',Officerz='',PortalUsrz='',TxnTpz='CRE_RGVT')/QUESCONFIG_MSet\\\"}},\\\"ADDRESSSet\\\":[{\\\"__metadata\\\":{\\\"id\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/ADDRESSSet('12220024')\\\",\\\"uri\\\":\\\"https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RG_SRV/ADDRESSSet('12220024')\\\",\\\"type\\\":\\\"ZDP_VAT_NW_RG_SRV.ADDRESS\\\"},\\\"Addrnumber\\\":\\\"12220024\\\",\\\"City\\\":\\\"\\\",\\\"Quarter\\\":\\\"\\\",\\\"PostalCd\\\":\\\"00000\\\",\\\"Street\\\":\\\"\\\",\\\"AdditionalNo\\\":\\\"\\\",\\\"BuildingNo\\\":\\\"\\\",\\\"Region\\\":\\\"\\\",\\\"RegionDesc\\\":\\\"\\\"}]}\"";
            return json;
        }



    }
}