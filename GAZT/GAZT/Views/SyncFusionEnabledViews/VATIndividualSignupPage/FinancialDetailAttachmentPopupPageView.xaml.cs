using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
                viewModel.ResultsItemForDOCSet = new List<ResultsItemForElgblDocSet>();
                viewModel.VATRegistrationDetailsForAttach = new VATRegistrationDetails();
                
                viewModel.VATRegistrationDetailsForAttach= sendtoPopup.VATRegistrationDetailsDatatoPopup;
                viewModel.ELGBL_DOCSet = viewModel.VATRegistrationOtherDetails.d.ELGBL_DOCSet;
                try
                {
                    viewModel.ResultsItemForDOCSet = viewModel.ELGBL_DOCSet.results;
                    onPageLoad();

                    if (viewModel.ResultsItemForDOCSet != null)
                    {
                        viewModel.SelectedAttachmentType = 1;
                        //AttachmentTypePicker.SelectedItem = "1";
                    }
                }
                catch (Exception ex)
                {

                }
                
                          
               
            }
            catch (Exception ex)
            { 
            
            }
            
        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {

                            AttachmentTypePicker.HeaderFontFamily = "SSTArabic-Medium";
                            AttachmentTypePicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            AttachmentTypePicker.SelectedItemFontFamily = "SSTArabic-Medium";
                            AttachmentTypePicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy

                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        AttachmentTypePicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        AttachmentTypePicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        AttachmentTypePicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        AttachmentTypePicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy 

                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void onPageLoad()
        {
            try
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
            catch(Exception ex)
            {

            }
           
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            try
            {
                MessagingCenter.Send<Object, ATTDETSet>(this, "AttachmentReceived", viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet);
                MessagingCenter.Unsubscribe<object, string>(this, "YesPressedToDeleteFinancialAttachment");
                MessagingCenter.Unsubscribe<object, string>(this, "NoPressedToDeleteFinancialAttachment");

                //if (viewModel.VATRegistrationDetailsForAttach != null && viewModel.VATRegistrationDetailsForAttach.d != null && viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet != null && viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results != null)
                //{
                //    foreach (var item in viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results)
                //    {
                //        ResultsItemForElgblDocSet _eligibledocset = new ResultsItemForElgblDocSet();
                //        _eligibledocset.DmsTp = item.Dotyp;
                //        _eligibledocset.DmsTxt = viewModel.ResultsItemForDOCSet.Where(X => X.DmsTp == item.Dotyp).FirstOrDefault().Txt50;
                //        //AttachmentTypeTxt = _selectedResultsItemForDOCSet.Txt50;
                //        //DocTypeString = _selectedResultsItemForDOCSet.DmsTp;
                //        _eligibledocset.TxnTp = "CRE_RGVT";
                //        viewModel.VATRegistrationDetailsForAttach.d.ELGBL_DOCSet.results.Add(_eligibledocset);

                //    }


                //}

                MessagingCenter.Send<Object, ELGBL_DOCSetforsubmit>(this, "EligibilitySetAttachmentReceived", viewModel.VATRegistrationDetailsForAttach.d.ELGBL_DOCSet);
                //viewModel.VATRegistrationDetailsForAttach.d.ELGBL_DOCSet
            }
            catch(Exception ex)
            { 
            
            }
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


                    if (sender != null)
                    {
                        viewModel.VATAttachmentObj = new VATAttachment();
                        Image arrowImage = sender as Image;
                        viewModel.VATAttachmentObj = (VATAttachment)arrowImage.BindingContext;
                        string var = string.Empty;
                        if (App.IsArabic)
                        {
                            var = AppResources.ZZDeleteAttachmentConfirmationText + " " + viewModel.VATAttachmentObj.Filename + " ؟ ";
                        }
                        else
                        {
                            var = AppResources.ZZDeleteAttachmentConfirmationText + " " + viewModel.VATAttachmentObj.Filename + " ? ";
                        }
                        await PopupNavigation.Instance.PushAsync(new ConfirmationPopUpForVatRegistration(var, "FinancialDetailAttachmentPopupPageView"));
                    }

                    //Image arrowImage = sender as Image;
                    //VATAttachment attachment = (VATAttachment)arrowImage.BindingContext;
                    ////if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
                    ////{

                    //if (attachment != null)
                    //{
                    //    var result = await this.DisplayAlert(AppResources.ZZNotification, AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + "?", AppResources.ZZZOkayText, AppResources.ZZCancel);
                    //    DeleteAttachment(result, attachment);
                    //}

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
                    //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
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
                            viewModel.VatAttachmentsList.Clear();
                            viewModel.filterList();
                            viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
                            try
                            {

                                ResultsItemForDOCSetforsubmit _eligibledocset = new
ResultsItemForDOCSetforsubmit();
                                _eligibledocset.DmsTp = listitem.Dotyp;
                                _eligibledocset.DmsTxt = viewModel.ResultsItemForDOCSet.Where(X => X.DmsTp == listitem.Dotyp).FirstOrDefault().Txt50;
                                _eligibledocset.TxnTp = "CRE_RGVT";
                                _eligibledocset.LineNo = 0;
                                _eligibledocset.Mandt = "";
                                _eligibledocset.DataVersion = "";
                                _eligibledocset.FormGuid = "";
                                _eligibledocset.Fbtyp = "";
                                _eligibledocset.RankingOrder = "";
 
                                viewModel.VATRegistrationDetailsForAttach.d.ELGBL_DOCSet.results.Remove(_eligibledocset);
                            }
                            catch(Exception ex)
                            { 
                            
                            }
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
        {try
            {
                Image arrowImage = sender as Image;
                VATAttachment attachment = (VATAttachment)arrowImage.BindingContext;

                if (attachment != null)
                {
                    if(false == String.IsNullOrEmpty(attachment.DocUrl))
                        viewModel._navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
                }

            }
            catch (Exception ex)
            { 
            }
        }
        private void DDlIDTypee_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {

                ResultsItemForElgblDocSet selectedtyp = (ResultsItemForElgblDocSet)e.NewValue;

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



        protected async override void OnAppearing()
        {
            base.OnAppearing();
            getYesForDeleteAttachment();
            getNoForDeleteAttachment();
        }


        public async void getYesForDeleteAttachment()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesPressedToDeleteFinancialAttachment", async (sender, arg) =>
                {
                    DeleteAttachmentForMessagingCenterCall();
                });
            }
            catch (Exception ex)
            {

            }
        }

        public async void DeleteAttachmentForMessagingCenterCall()
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                // Image arrowImage = sender as Image;
                if (viewModel.VATAttachmentObj != null)
                {
                    VATAttachment attachment = viewModel.VATAttachmentObj;
                    //if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
                    //{

                    if (attachment != null)
                    {//ZZNotification
                     // var result = await this.DisplayAlert(AppResources.ZZNotification, AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + "?", AppResources.ZZZOkayText, AppResources.ZZCancel);

                        await DeleteAttachment(true, attachment);
                    }
                }
                //}
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception ex)
            {

            }
        }

        public async void getNoForDeleteAttachment()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "NoPressedToDeleteFinancialAttachment", async (sender, arg) =>
                {


                });
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

              ((Xamarin.Forms.ListView)sender).SelectedItem = null;
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
     
        private    void  Close_Tapped(object sender, EventArgs e)
        {
             PopupNavigation.Instance.PopAsync();
        }

        private void DDlIDType_OkButtonClicked(object sender, EventArgs e)
        {

        }
    }
}