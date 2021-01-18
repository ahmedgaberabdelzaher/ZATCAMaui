using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AddNotePage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AttachmentPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATReturnsPage_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using EGAZT.Views.SyncFusionEnabledViews.ICRList;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Entry = Xamarin.Forms.Entry;
namespace EGAZT.Views.SyncFusionEnabledViews.VATReturnsPage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATReturnsPageView : ContentPage
    {
        #region Variable
        public VATReturnsPageViewModel viewModel;
        #endregion
        #region Property
        #endregion
        #region Constructor
        public VATReturnsPageView(VATDeclaration _vATDeclarationInfo)
        {
            try
            {
                viewModel = App.Locator.VATReturnsPageView;
                InitializeComponent();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                ChangeAeroIcon();
                viewModel = App.Locator.VATReturnsPageView;
                viewModel.SelectedIndex = 0;
                this.BindingContext = viewModel;
                SetLTR();
                if (_vATDeclarationInfo.d != null)
                {
                    viewModel.VATDeclarationData = _vATDeclarationInfo;
                }
                if (App.ICRStatus == "E0001" || App.ICRStatus == "E0013")
                {
                    viewModel.IsChangeRegistrationlinkVisible = true;
                }
                else
                {
                    viewModel.IsChangeRegistrationlinkVisible = false;
                }
                // viewModel.IsSwitchToggled = false;
                viewModel.IsFirstTimeGet = true;
                viewModel.IsSwitchToggled = false;
                AddNotePageViewModel.NoteString = string.Empty;
                AddNotePageViewModel.NoteCount = 0;
                viewModel.IsRefundVisible = false;
                viewModel.IsGetAcknowledgementClicked = false;
                viewModel.IsVisibleDropdownForRefund = false;
                viewModel.IsFirstSubmission = true;
                viewModel.IsMoreButtonEnabled = true;
                viewModel.IsRefundNoMsgDisplayed = false;
                viewModel.IsRefundYesMsgDisplayed = false;
                viewModel.IsSwichButtonEnable = false;
                viewModel.IBANList = null;
                viewModel.IBANIDNumberList = null;
                viewModel.SelectedIndex = 0;
                viewModel.SelectedIBANIDNumberPrev = null;
                viewModel.SelectedIBANPrev = null;
                viewModel.SelectedIBANTypePrev = null;
                viewModel.TxtSelectedIBANIDNumber = string.Empty;
                viewModel.TxtSelectedIBAN = string.Empty;
                viewModel.TxtSelectedIBANType = string.Empty;
                setAllCheckbox(false);
                IntilizeAsync();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                viewModel.IsMainButtonEnabled = false;
                // Resources["CheckBoxValidationStyle"] = App.Current.Resources["StyleCheckBoxValidatorGreen"];
            }
            catch (Exception e)
            {
            }
        }
        #endregion
        #region Method
        private void SfTabView_TabItemTapped(object sender, TabItemTappedEventArgs e)
        {
            try
            {
                bool isTrue = CheckMandetoryFields();
                if (e.TabItem.Title == AppResources.ZZInstruction)
                {
                    //No check
                }
                else if (e.TabItem.Title == AppResources.ZZTaxPayerDetails)
                {
                    if (viewModel.IsDeclarationCheckedForInstruction == false)
                    {
                        e.Cancel = true;
                    }
                }
                else if (e.TabItem.Title == AppResources.ZZVATReturnForm)
                {
                    if (!(viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true))
                    {
                        e.Cancel = true;
                    }
                }
                else if (e.TabItem.Title == AppResources.ZZSummary)
                {
                    if (!(viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true && isTrue == true))
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
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
        public void setAllCheckbox(bool bValue)
        {
            viewModel.IsDeclarationCheckedForInstruction = bValue;
            viewModel.IsDeclarationCheckedForSummary = bValue;
            viewModel.IsCheckedTaxPayerDetailsInfo = bValue;
            viewModel.IschkRefundDeclaration = bValue;
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                viewModel.FDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                viewModel.FDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
        }
        public async Task IntilizeAsync()
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            await Task.Run(async () =>
            {
                await viewModel.pageLoad();
                try
                {
                    if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058")
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            viewModel.ManageEnabledAsyncProperty(false);
                            viewModel.SelectedIndex = 0;
                            viewModel.PageSelectedItem = viewModel.VatTabbledPageList[0];
                            //   viewModel.ManageEnabledProperty(false);
                            viewModel.IsCheckedTaxPayerDetailsInfo = true;
                            viewModel.IsDeclarationCheckedForSummary = true;
                            viewModel.IsDeclarationCheckedForInstruction = true;
                            viewModel.IsVATRefunCheckedVisible = true;
                            //viewModel.ButtonName = AppResources.ZVatDownloadForm;
                            viewModel.IsMainButtonEnabled = false;
                            viewModel.IsMainButtonVisible = false;
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            viewModel.ManageEnabledAsyncProperty(true);
                            viewModel.IsMainButtonVisible = true;
                            if (App.ICRStatus == "E0001")
                            {
                                viewModel.IsMainButtonEnabled = false;
                            }
                        });
                        if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
                        {
                            if (App.ICRStatus == "E0056")
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel.IsCheckedTaxPayerDetailsInfo = true;
                                    viewModel.IsDeclarationCheckedForInstruction = true;
                                    viewModel.IsDeclarationCheckEnabled = false;
                                    viewModel.IsTaxPayerCheckEnabled = false;
                                });
                            }
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await viewModel.NavigationSetupForDraft();
                            });
                        }
                    }
                    viewModel.ListOfActionButtonsApplicable = new List<string>();
                    await viewModel.SetButtons(viewModel.VATDeclarationData);
                    if (App.CheckTINStatusPageView != "0045")
                    {
                        onPageLoadCalculation();
                    }
                }
                catch (Exception ex)
                {
                }
            });
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
            //onPageLoadCalculation();
        }
        //private void ClickGestureRecognizer_ClickedForCustomLabel(object sender, EventArgs e)
        //{
        //    Label labelInstrunction = (Label)sender;
        //    if(labelInstrunction.Text== "Instrunction")
        //    {
        //        labelInstrunction.Style = (Style)App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
        //    }
        //    else if(labelInstrunction.Text == "TaxPayer Details")
        //    {
        //        labelInstrunction.Style = (Style)App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
        //    }
        //    else if (labelInstrunction.Text == "VAT Return Form")
        //    {
        //        labelInstrunction.Style = (Style)App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
        //    }
        //    else if (labelInstrunction.Text == "Summary")
        //    {
        //        labelInstrunction.Style = (Style)App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
        //    }
        //}
        //private void OnDeleteAttachmentClicked(object sender, EventArgs e)
        //{
        //    Image arrowImage = sender as Image;
        //    Attachment attachment = (Attachment)arrowImage.BindingContext;
        //    var results =   WebServiceManager.GAZTDeleteVATDeclarationAttachment(attachment.Filename,  viewModel.VATDeclarationData.d.ReturnIdz);
        //}

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //comment because main button remains enabled
          //  viewModel.IsSwichButtonEnable = false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ICRListPageView.AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = true;
            if (Device.RuntimePlatform == Device.iOS)
            {
                viewModel.IsSwitchVisible = true;
            }
            else
            {
                viewModel.IsSwitchVisible = false;
            }
            AttachmentPageViewModel.attachmentSizeVisibility = false;
            if (AddNotePageViewModel.IsComingFromNotePage == true && !string.IsNullOrEmpty(AddNotePageViewModel.NoteString))
            {
                if (App.ICRStatus == "E0001")
                {
                    SetNote();
                }
                if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057" || App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                {
                    Note note = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                    if (note != null)
                    {
                        foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                        {
                            item.Strline = AddNotePageViewModel.NoteString;
                            item.Tdline = AddNotePageViewModel.NoteString;
                        }
                        AddNotePageViewModel.IsComingFromNotePage = false;
                    }
                    else
                    {
                        SetNote();
                    }
                }
                //if(App.ICRStatus == "E0045" && viewModel.IsAmendClicked==true && AddNotePageViewModel.ClearNoteClicked == false)
                //{
                //    int count = viewModel.VATDeclarationData.d.NOTESSet.results.Count;
                //    Note note = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00001").FirstOrDefault();
                //    if (note != null)
                //    {
                //        //foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                //        //{
                //        //    item.DataVersionz = "00001";
                //        //}
                //        Note noteForEdited = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                //        if (noteForEdited != null)
                //        {
                //            foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                //            {
                //                item.Strline = AddNotePageViewModel.NoteString;
                //                item.Tdline = AddNotePageViewModel.NoteString;
                //            }
                //        }
                //        else
                //        {
                //            SetNoteForBilledAndAmend();
                //        }
                //    }
                //    else
                //    {
                //        Note noteForEdited = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                //        if (noteForEdited != null)
                //        {
                //            foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                //            {
                //                item.Strline = AddNotePageViewModel.NoteString;
                //                item.Tdline = AddNotePageViewModel.NoteString;
                //            }
                //        }
                //        else
                //        {
                //            SetNoteForBilledAndAmend();
                //        }
                //    }
                //}
                //if (App.ICRStatus == "E0006" && viewModel.IsAmendClicked == true && AddNotePageViewModel.ClearNoteClicked == false)
                //{
                //    int count = viewModel.VATDeclarationData.d.NOTESSet.results.Count;
                //    Note note = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00001").FirstOrDefault();
                //    if (note != null)
                //    {
                //        //foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                //        //{
                //        //    item.DataVersionz = "00001";
                //        //}
                //        Note noteForEdited = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                //        if (noteForEdited != null)
                //        {
                //            foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                //            {
                //                item.Strline = AddNotePageViewModel.NoteString;
                //                item.Tdline = AddNotePageViewModel.NoteString;
                //            }
                //        }
                //        else
                //        {
                //            SetNoteForBilledAndAmend();
                //        }
                //    }
                //    else
                //    {
                //        Note noteForEdited = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                //        if (noteForEdited != null)
                //        {
                //            foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                //            {
                //                item.Strline = AddNotePageViewModel.NoteString;
                //                item.Tdline = AddNotePageViewModel.NoteString;
                //            }
                //        }
                //        else
                //        {
                //            SetNoteForBilledAndAmend();
                //        }
                //    }
                //}
                if (AddNotePageViewModel.ClearNoteClicked == true)
                {
                    Note note = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                    if (note != null)
                    {
                        foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                        {
                            item.Strline = AddNotePageViewModel.NoteString;
                            item.Tdline = AddNotePageViewModel.NoteString;
                        }
                        AddNotePageViewModel.IsComingFromNotePage = false;
                        AddNotePageViewModel.NoteString = string.Empty;
                    }
                    AddNotePageViewModel.ClearNoteClicked = false;
                }
                AddNotePageViewModel.NoteString = string.Empty;
            }
        }
        public void SetNote()
        {
            viewModel.VATDeclarationData.d.NOTESSet.results = new List<Note>();
            Note objNote = new Note();
            int count = viewModel.VATDeclarationData.d.NOTESSet.results.Count;
            string Url = Constants.QABaseUrlForODataServices + "/sap/opu/odata/SAP/ZDP_VATR_M_SRV/NOTESSet('00" + (count + 1).ToString() + "')";
            objNote.__metadata = new Metadata2();
            objNote.__metadata.id = Url;
            objNote.__metadata.uri = Url;
            objNote.__metadata.type = "ZDP_VATR_M_SRV.NOTES";
            objNote.Notenoz = (count + 1).ToString();
            objNote.DataVersionz = "00000";
            objNote.Refnamez = String.Empty;
            objNote.XInvoicez = String.Empty;
            objNote.XObsoletez = string.Empty;
            objNote.Rcodez = "VATR";
            objNote.ByPusrz = string.Empty;
            objNote.Tdformat = string.Empty;
            objNote.Tdline = string.Empty;
            objNote.Erfusrz = viewModel.VATDeclarationData.d.Gpartz;
            objNote.ByGpartz = viewModel.VATDeclarationData.d.Gpartz;
            objNote.Namez = viewModel.VATDeclarationData.d.Tpnm;
            objNote.AttByz = "TP";
            objNote.Noteno = (count + 1).ToString();
            objNote.Lineno = 1;
            objNote.ElemNo = 0;
            objNote.Strdt = string.Empty;
            objNote.Strtime = string.Empty;
            objNote.Sect = "VAT Return General Note";
            objNote.Strline = AddNotePageViewModel.NoteString;
            objNote.Tdline = AddNotePageViewModel.NoteString;
            viewModel.VATDeclarationData.d.NOTESSet.results.Add(objNote);
            AddNotePageViewModel.IsComingFromNotePage = false;
            //AddNotePageViewModel.NoteString = string.Empty;
        }
        public void SetNoteForBilledAndAmend()
        {
            try
            {
                Note note = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00001").FirstOrDefault();
                if (note != null)
                {
                }
                else
                {
                    viewModel.VATDeclarationData.d.NOTESSet.results = new List<Note>();
                }
                Note objNote = new Note();
                int count = viewModel.VATDeclarationData.d.NOTESSet.results.Count;
                string Url = Constants.QABaseUrlForODataServices + "/sap/opu/odata/SAP/ZDP_VATR_M_SRV/NOTESSet('00" + (count + 1).ToString() + "')";
                objNote.__metadata = new Metadata2();
                objNote.__metadata.id = Url;
                objNote.__metadata.uri = Url;
                objNote.__metadata.type = "ZDP_VATR_M_SRV.NOTES";
                objNote.Notenoz = (count + 1).ToString();
                objNote.DataVersionz = "00000";
                objNote.Refnamez = String.Empty;
                objNote.XInvoicez = String.Empty;
                objNote.XObsoletez = string.Empty;
                objNote.Rcodez = "VATR";
                objNote.ByPusrz = string.Empty;
                objNote.Tdformat = string.Empty;
                objNote.Tdline = string.Empty;
                objNote.Erfusrz = viewModel.VATDeclarationData.d.Gpartz;
                objNote.ByGpartz = viewModel.VATDeclarationData.d.Gpartz;
                objNote.Namez = viewModel.VATDeclarationData.d.Tpnm;
                objNote.AttByz = "TP";
                objNote.Noteno = (count + 1).ToString();
                objNote.Lineno = 1;
                objNote.ElemNo = 0;
                objNote.Strdt = string.Empty;
                objNote.Strtime = string.Empty;
                objNote.Sect = "VAT Return General Note";
                objNote.Strline = AddNotePageViewModel.NoteString;
                objNote.Tdline = AddNotePageViewModel.NoteString;
                viewModel.VATDeclarationData.d.NOTESSet.results.Add(objNote);
                AddNotePageViewModel.IsComingFromNotePage = false;
                //AddNotePageViewModel.NoteString = string.Empty;
            }
            catch (Exception ex)
            {
            }
        }
        private void ClickGestureRecognizer_ClickedForInstructions(object sender, EventArgs e)
        {
            Resources["searchBarStyleForTPDetails"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVATReturnForm"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForSummary"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForInstructions"] = App.Current.Resources["TabbedPageMediumMiniGoldLabelStyle"];
        }
        private void ClickGestureRecognizer_ClickedForTPDetails(object sender, EventArgs e)
        {
            Resources["searchBarStyleForInstructions"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVATReturnForm"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForSummary"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForTPDetails"] = App.Current.Resources["TabbedPageMediumMiniGoldLabelStyle"];
        }
        private void ClickGestureRecognizer_ClickedForVATReturnForm(object sender, EventArgs e)
        {
            Resources["searchBarStyleForInstructions"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForTPDetails"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForSummary"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVATReturnForm"] = App.Current.Resources["TabbedPageMediumMiniGoldLabelStyle"];
        }
        private void ClickGestureRecognizer_ClickedForSummary(object sender, EventArgs e)
        {
            Resources["searchBarStyleForInstructions"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForTPDetails"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForVATReturnForm"] = App.Current.Resources["TabbedPageSmallMiniWhiteLabelStyle"];
            Resources["searchBarStyleForSummary"] = App.Current.Resources["TabbedPageMediumMiniGoldLabelStyle"];
        }
        #endregion
        public void OnPageSelected(object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            SfTabView_SelectionChanged(null, null);
            // CollectionView pagename =(CollectionView)sender;
            //VATDeclarationTabbedPageName previous = (e.PreviousSelection.FirstOrDefault() as VATDeclarationTabbedPageName);
            //VATDeclarationTabbedPageName current = (e.CurrentSelection.FirstOrDefault() as VATDeclarationTabbedPageName);
            //var senderObject = sender;
            //if (current != null)
            //{
            //    if (current.pageName == "Instruction" || current.pageName == "التعليمات")
            //    {
            //        viewModel.InstrunctionClicked();
            //        if (viewModel.IsDeclarationCheckedForInstruction && !((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && (viewModel.IsAmendClicked == false)))
            //        {
            //            viewModel.IsMainButtonEnabled = true;
            //        }
            //        else
            //        {
            //            viewModel.IsMainButtonEnabled = false;
            //        }
            //        // BtnNextStep.IsEnabled = false;
            //        //if (App.ICRStatus == "E0045")
            //        //{
            //        //    viewModel.IsDeclarationCheckedForInstruction = true;
            //        //    viewModel.IsDeclarationCheckedForSummary = true;
            //        //    viewModel.IsCheckedTaxPayerDetailsInfo = true;
            //        //    chkDeclaration.IsChecked = true;
            //        //}
            //        //else
            //        //{
            //        //    viewModel.IsDeclarationCheckedForInstruction = false;
            //        //    chkDeclaration.IsChecked = false;
            //        //}
            //        //  setColor(previous, current);
            //        NewSetColor(senderObject, current);
            //        viewModel.IsFirstTimeGet = false;
            //    }
            //    else if (current.pageName == "TaxPayer Details" || current.pageName == "تفاصيل المكلف")
            //    {
            //        bool value = viewModel.IsCheckedDraftMode();
            //        bool Tvalue = viewModel.IsTabbedValid("02");
            //        if (value && Tvalue)
            //        {
            //            if (viewModel.IsFirstTimeGet)
            //            {
            //                viewModel.IsDeclarationCheckedForInstruction = true;
            //            }
            //        }
            //        if (viewModel.IsDeclarationCheckedForInstruction == true || (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057"))
            //        {
            //            if (viewModel.IsDeclarationCheckedForInstruction == true)
            //            {
            //                viewModel.TaxpayerDetailsClicked();
            //                // setColor(previous, current);
            //                NewSetColor(senderObject, current);
            //            }
            //            else
            //            {
            //                ((CollectionView)sender).SelectedItem = null;
            //            }
            //        }
            //        else
            //        {
            //            if (App.ICRStatus != "E0001")
            //            {
            //                viewModel.IsDeclarationCheckedForInstruction = false;
            //                viewModel.IsMainButtonEnabled = false;
            //            }
            //            // BtnNextStep.IsEnabled = false;
            //            //  chkClearification.IsChecked = false;
            //            ((CollectionView)sender).SelectedItem = null;
            //        }
            //        viewModel.IsFirstTimeGet = false;
            //    }
            //    else if (current.pageName == "VAT Return Form" || current.pageName == "نموذج الإقرار الضريبي")
            //    {
            //        bool value = viewModel.IsCheckedDraftMode();
            //        bool Tvalue = viewModel.IsTabbedValid("03");
            //        if (value && Tvalue)
            //        {
            //            if (viewModel.IsFirstTimeGet)
            //            {
            //                viewModel.IsDeclarationCheckedForInstruction = true;
            //                viewModel.IsCheckedTaxPayerDetailsInfo = true;
            //            }
            //        }
            //        if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true || (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057"))
            //        {
            //            if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true)
            //            {
            //                viewModel.VATReturnFormClicked();
            //                //   setColor(previous, current);
            //                EntryVatAmount.Focus();
            //                NewSetColor(senderObject, current);
            //            }
            //            else
            //            {
            //                ((CollectionView)sender).SelectedItem = null;
            //            }
            //        }
            //        else
            //        {
            //            ((CollectionView)sender).SelectedItem = null;
            //        }
            //        viewModel.IsFirstTimeGet = false;
            //        //else
            //        //{
            //        //    viewModel.PageSelectedItem = viewModel.VatTabbledPageList[0];
            //        //}
            //    }
            //    else if (current.pageName == "Summary" || current.pageName == "ملخص")
            //    {
            //        if (CheckMandetoryFields())
            //        {
            //            try
            //            {
            //            }
            //            catch
            //            {
            //            }
            //            if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true || (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057"))
            //            {
            //                bool value = viewModel.IsCheckedDraftMode();
            //                bool Tvalue = viewModel.IsTabbedValid("04");
            //                if (value && Tvalue)
            //                {
            //                    if (viewModel.IsFirstTimeGet)
            //                    {
            //                        viewModel.IsDeclarationCheckedForInstruction = true;
            //                        viewModel.IsCheckedTaxPayerDetailsInfo = true;
            //                    }
            //                }
            //                if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true)
            //                {
            //                    bool value1 = viewModel.IsCheckedDraftMode();
            //                    if (viewModel.IsMainButtonEnabled == false && (App.ICRStatus == "E0001" || value1))
            //                    {
            //                    }
            //                    else
            //                    {
            //                        viewModel.SummaryClicked();
            //                        // setColor(previous, current);
            //                        NewSetColor(senderObject, current);
            //                    }
            //                    //Add because it will  not navigate in tobefilled and draft mode
            //                    if (App.ICRStatus == "E0001" || value1)
            //                    {
            //                        viewModel.SummaryClicked();
            //                        //  setColor(previous, current);
            //                        NewSetColor(senderObject, current);
            //                    }
            //                    else
            //                    {
            //                        ((CollectionView)sender).SelectedItem = null;
            //                    }
            //                }
            //                else
            //                {
            //                    ((CollectionView)sender).SelectedItem = null;
            //                }
            //            }
            //            else
            //            {
            //                viewModel.IsMainButtonEnabled = false;
            //                viewModel.IsMainButtonEnabled = false;
            //                chkDeclarationForSummary.IsChecked = false;
            //            }
            //            viewModel.IsFirstTimeGet = false;
            //        }
            //        else
            //        {
            //            ((CollectionView)sender).SelectedItem = null;
            //        }
            //    }
            //}
        }
        private void chkDeclaration_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (viewModel.IsDeclarationChecked == false)
            {
                Resources["searchBarStyleForDeclarationChb"] = App.Current.Resources["GAZTGrayLabelStyleForCaptionFont"];
                // BtnNextStep.IsEnabled = false;
            }
            else
            {
                Resources["searchBarStyleForDeclarationChb"] = App.Current.Resources["GAZTGoldLabelStyleForCaptionFont"];
                //  BtnNextStep.IsEnabled = true;
            }
            if (chkDeclaration.IsChecked == true)
            {
                viewModel.IsMainButtonEnabled = false;
                //  BtnNextStep.IsEnabled = true;
            }
            else
            {
                //  BtnNextStep.IsEnabled = false;
            }
        }
        public void setColor(VATDeclarationTabbedPageName previous, VATDeclarationTabbedPageName current)
        {
            if (current != null)
            {
                current.TextColor = Color.FromHex("#c49b2d");
            }
            if (previous != null)
            {
                //Reset the previous to defaulr color
                previous.TextColor = Color.FromHex("#FFFFFF");
            }
        }
        public void NewSetColor(object sender, VATDeclarationTabbedPageName current)
        {
            try
            {
                if (sender != null && current != null)
                {
                    CollectionView collectionView = new CollectionView();
                    collectionView.ItemsSource = ((CollectionView)sender).ItemsSource;
                    //   var a = collectionView.ItemsSource;
                    foreach (var item in collectionView.ItemsSource)
                    {
                        var b = (VATDeclarationTabbedPageName)item;
                        if (b.pageName == current.pageName)
                        {
                            b.TextColor = Color.FromHex("#c49b2d");
                        }
                        else
                        {
                            b.TextColor = Color.FromHex("#FFFFFF");
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
        private async void onMoreOptionClicked(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.ListOfActionButtonsApplicable != null && viewModel.ListOfActionButtonsApplicable.Count() != 0)
                {
                    String action = await DisplayActionSheet("", AppResources.ZZCancel, null, viewModel.ListOfActionButtonsApplicable.ToArray());
                    if (App.IsArabic)
                    {
                        ArButtons buttonId = ArButtons.None;
                        if (!string.IsNullOrEmpty(action))
                        {
                            action = action.Replace(" ", "");
                        }
                        Enum.TryParse(action, out buttonId);
                        switch (buttonId)
                        {
                            case ArButtons.إضافةملاحظات:
                                viewModel.VATReturnAddNote();
                                break;
                            case ArButtons.عرضملاحظات:
                                viewModel.VATReturnGetNotes();
                                break;
                            case ArButtons.المرفقات:
                                viewModel.VATViewAttachments();
                                break;
                            case ArButtons.إلغاء:
                                await viewModel.VATSetReturnVoidAsync();
                                break;
                            case ArButtons.عادةتعيين:
                                await viewModel.VATReturnResetAsync();
                                break;
                            case ArButtons.تعديل:
                                await viewModel.VATReturnAmendAsync();
                                break;
                            case ArButtons.حفظكمسودة:
                                viewModel.IsVATReturnFieldCheckForSaveAsDraft = true;
                                if (CheckMandetoryFields())
                                {
                                    await viewModel.OnSaveDraftClicked();
                                    if(viewModel.IsVisibleSummary)
                                    {
                                        ValidationsForVATRefund();
                                        if(viewModel.IsDeclarationCheckedForSummary == false)
                                        {
                                            viewModel.IsMainButtonEnabled = false;
                                        }
                                    }
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await viewModel._dialogService.ShowMessage(AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields, AppResources.Information);
                                    });
                                }
                                viewModel.IsVATReturnFieldCheckForSaveAsDraft = false;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Buttons buttonId = Buttons.None;
                        if (!string.IsNullOrEmpty(action))
                        {
                            action = action.Replace(" ", "");
                        }
                        Enum.TryParse(action, out buttonId);
                        switch (buttonId)
                        {
                            case Buttons.CreateNotes:
                                viewModel.VATReturnAddNote();
                                break;
                            case Buttons.DisplayNotes:
                                viewModel.VATReturnGetNotes();
                                break;
                            case Buttons.Attachments:
                                viewModel.VATViewAttachments();
                                break;
                            case Buttons.Void:
                                await viewModel.VATSetReturnVoidAsync();
                                break;
                            case Buttons.Reset:
                                await viewModel.VATReturnResetAsync();
                                break;
                            case Buttons.Amend:
                                await viewModel.VATReturnAmendAsync();
                                break;
                            case Buttons.SaveasDraft:
                                viewModel.IsVATReturnFieldCheckForSaveAsDraft = true;
                                if (CheckMandetoryFields())
                                {
                                    await viewModel.OnSaveDraftClicked();
                                    if (viewModel.IsVisibleSummary)
                                    {
                                        ValidationsForVATRefund();
                                        if(viewModel.IschkRefundDeclaration==false || viewModel.IsDeclarationCheckedForSummary == false)
                                        {
                                            viewModel.IsMainButtonEnabled = false;
                                        }
                                    }
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await viewModel._dialogService.ShowMessage(AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields, AppResources.Information);
                                    });
                                }
                                viewModel.IsVATReturnFieldCheckForSaveAsDraft = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public bool isCheckArabic(String arText)
        {
            bool isAllNumeric = true;
            foreach (char letter in arText.ToCharArray())
            {
                if (!((letter >= 46 && letter <= 57) || letter == 44))
                {
                    isAllNumeric = false;
                }
            }
            return isAllNumeric;
        }
        public bool isCheckArabicWithMinus(String arText)
        {
            bool isAllNumeric = true;
            foreach (char letter in arText.ToCharArray())
            {
                if (!((letter >= 46 && letter <= 57) || letter == 44 || letter == 45))
                {
                    isAllNumeric = false;
                }
            }
            return isAllNumeric;
        }
        private void ClickGestureRecognizer_ClickedForVatAmount(object sender, EventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryVatAmount.Text) && EntryVatAmount.Text.Contains(","))
                    //{
                    //    EntryVatAmount.Text = EntryVatAmount.Text.Replace(",", "");
                    //    EntryVatAmount.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) && EntryVatAdjustmentWithSAR.Text.Contains(","))
                    //{
                    //    EntryVatAdjustmentWithSAR.Text = EntryVatAdjustmentWithSAR.Text.Replace(",", "");
                    //    EntryVatAdjustmentWithSAR.TextColor = Color.Black;
                    //}
                    CheckMandetoryFields();
                    // char LastChar = ' ';
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            viewModel.StdsalesVat = viewModel.StandardRatedSalesVatAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.StdsalesAdj);
                            viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
                            viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
                            viewModel.TotalsalesVat = viewModel.StdsalesVat;
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ClickGestureRecognizer_ClickedForAllAmount(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntrySalesGccAmt.Text) && EntrySalesGccAmt.Text.Contains(","))
                    //{
                    //    EntrySalesGccAmt.Text = EntrySalesGccAmt.Text.Replace(",", "");
                    //    EntrySalesGccAmt.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryZerosalesAmt.Text) && EntryZerosalesAmt.Text.Contains(","))
                    //{
                    //    EntryZerosalesAmt.Text = EntryZerosalesAmt.Text.Replace(",", "");
                    //    EntryZerosalesAmt.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryExportsAmt.Text) && EntryExportsAmt.Text.Contains(","))
                    //{
                    //    EntryExportsAmt.Text = EntryExportsAmt.Text.Replace(",", "");
                    //    EntryExportsAmt.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryExemptsalesAmt.Text) && EntryExemptsalesAmt.Text.Contains(","))
                    //{
                    //    EntryExemptsalesAmt.Text = EntryExemptsalesAmt.Text.Replace(",", "");
                    //    EntryExemptsalesAmt.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ClickGestureRecognizer_ClickedForVatAdjustment(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntrySalesGccAdj.Text) && EntrySalesGccAdj.Text.Contains(","))
                    //{
                    //    EntrySalesGccAdj.Text = EntrySalesGccAdj.Text.Replace(",", "");
                    //    EntrySalesGccAdj.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryZerosalesAdj.Text) && EntryZerosalesAdj.Text.Contains(","))
                    //{
                    //    EntryZerosalesAdj.Text = EntryZerosalesAdj.Text.Replace(",", "");
                    //    EntryZerosalesAdj.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryExportsAdj.Text) && EntryExportsAdj.Text.Contains(","))
                    //{
                    //    EntryExportsAdj.Text = EntryExportsAdj.Text.Replace(",", "");
                    //    EntryExportsAdj.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryExemptsalesAdj.Text) && EntryExemptsalesAdj.Text.Contains(","))
                    //{
                    //    EntryExemptsalesAdj.Text = EntryExemptsalesAdj.Text.Replace(",", "");
                    //    EntryExemptsalesAdj.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ClickGestureRecognizer_ClickedForVatAmountForPurchase(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            viewModel.StdpurchasesVat = viewModel.StandardRatedDomesticPurchaseVatAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.StdpurchaseAdj);
                            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ClickGestureRecognizer_ClickedForVatPaidatcustoms(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) && EntryZVatAmountWithSAR.Text.Contains(","))
                    //{
                    //    EntryZVatAmountWithSAR.Text = EntryZVatAmountWithSAR.Text.Replace(",", "");
                    //    EntryZVatAmountWithSAR.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryImportspaidAdj.Text) && EntryImportspaidAdj.Text.Contains(","))
                    //{
                    //    EntryImportspaidAdj.Text = EntryImportspaidAdj.Text.Replace(",", "");
                    //    EntryImportspaidAdj.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            if (viewModel.ResponseVATDeclarationD != null && viewModel.ResponseVATDeclarationD.TpregFg == "X")
                            {
                                viewModel.ImportspaidVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportspaidAdj);
                            }
                            else
                            {
                                viewModel.ImportspaidVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForNonDesignated(viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportspaidAdj);
                            }
                            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            //   viewModel.ResponseVATDeclarationD.ImportspaidVat=viewModel.
        }
        private void ClickGestureRecognizer_ClickedForVatAccounted(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryImportsaccAmt.Text) && EntryImportsaccAmt.Text.Contains(","))
                    //{
                    //    EntryImportsaccAmt.Text = EntryImportsaccAmt.Text.Replace(",", "");
                    //    EntryImportsaccAmt.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryImportsaccAdj.Text) && EntryImportsaccAdj.Text.Contains(","))
                    //{
                    //    EntryImportsaccAdj.Text = EntryImportsaccAdj.Text.Replace(",", "");
                    //    EntryImportsaccAdj.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            viewModel.ImportsaccVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ImportsaccAdj);
                            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ClickGestureRecognizer_ClickedForAllPurchaseAmount(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) && EntryZeropurchaseAmt.Text.Contains(","))
                    //{
                    //    EntryZeropurchaseAmt.Text = EntryZeropurchaseAmt.Text.Replace(",", "");
                    //    EntryZeropurchaseAmt.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) && EntryExemptpurchaseAmt.Text.Contains(","))
                    //{
                    //    EntryExemptpurchaseAmt.Text = EntryExemptpurchaseAmt.Text.Replace(",", "");
                    //    EntryExemptpurchaseAmt.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ClickGestureRecognizer_ClickedForAllPurchaseAdjustment(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) && EntryZeropurchaseAdj.Text.Contains(","))
                    //{
                    //    EntryZeropurchaseAdj.Text = EntryZeropurchaseAdj.Text.Replace(",", "");
                    //    EntryZeropurchaseAdj.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) && EntryExemptpurchaseAdj.Text.Contains(","))
                    //{
                    //    EntryExemptpurchaseAdj.Text = EntryExemptpurchaseAdj.Text.Replace(",", "");
                    //    EntryExemptpurchaseAdj.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ClickGestureRecognizer_ClickedForAllPurchaseVatAmount(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Entry)sender;
                //if (!string.IsNullOrEmpty(EntryStdpurchasesVat.Text) && EntryStdpurchasesVat.Text.Contains(","))
                //{
                //    EntryStdpurchasesVat.Text = EntryStdpurchasesVat.Text.Replace(",", "");
                //    EntryStdpurchasesVat.TextColor = Color.Black;
                //}
                //if (!string.IsNullOrEmpty(EntryImportspaidVat.Text) && EntryImportspaidVat.Text.Contains(","))
                //{
                //    EntryImportspaidVat.Text = EntryImportspaidVat.Text.Replace(",", "");
                //    EntryImportspaidVat.TextColor = Color.Black;
                //}
                //if (!string.IsNullOrEmpty(EntryImportsaccVat.Text) && EntryImportsaccVat.Text.Contains(","))
                //{
                //    EntryImportsaccVat.Text = EntryImportsaccVat.Text.Replace(",", "");
                //    EntryImportsaccVat.TextColor = Color.Black;
                //}
                if (!string.IsNullOrEmpty(senderObj.Text))
                {
                    isArabicChecked = isCheckArabicWithMinus(senderObj.Text);
                }
                if (isArabicChecked)
                {
                    CheckMandetoryFields();
                    //if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                    //{
                    viewModel.TotalpurchaseVat = viewModel.TotalVatAmount(viewModel.StdpurchasesVat, viewModel.ImportspaidVat, viewModel.ImportsaccVat);
                    //}
                }
                else
                {
                    if (senderObj != null && senderObj.Text.Length > 0)
                        senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public async void onPageLoadCalculation()
        {
            viewModel.StdsalesVat = viewModel.StandardRatedSalesVatAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.StdsalesAdj);
            viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
            viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
            viewModel.TotalsalesVat = viewModel.ResponseVATDeclarationD.StdsalesVat;
            viewModel.StdpurchasesVat = viewModel.StandardRatedDomesticPurchaseVatAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.StdpurchaseAdj);
            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
            if (viewModel.ResponseVATDeclarationD.TpregFg == "X")
            {
                viewModel.ImportspaidVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportspaidAdj);
            }
            else
            {
                viewModel.ImportspaidVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForNonDesignated(viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportspaidAdj);
            }
            viewModel.ImportsaccVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ImportsaccAdj);
            viewModel.TotalpurchaseVat = viewModel.TotalVatAmount(viewModel.StdpurchasesVat, viewModel.ImportspaidVat, viewModel.ImportsaccVat);
        }
        private void OnStandardRatedSalesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipStandardRatedSalesAmount;
            String MessageWithPercent = popUp.Message.Replace("5%", viewModel.VATRate002 + "%");
            popUp.Message = MessageWithPercent;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = AppResources.ZVatClickFaqInstructions;
            if(App.IsArabic)
            {
                popUp.Link = "https://www.vat.gov.sa/ar/vat-rate";
            }
            else
            {
                popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            }
            
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
        private void OnStandardRatedSalesAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipStandardRatedSalesAdjustment;
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
        private void OnPrivateHealthcareAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipPrivateHealthcareAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = AppResources.ZLink;
            popUp.Link = "https://www.uqn.gov.sa/articles/1515222747471373200/";
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
        private void OnPrivateHealthcareAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipPrivateHealthcareAdjustment;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = AppResources.ZLink; ;
            popUp.Link = "https://www.uqn.gov.sa/articles/1515222747471373200/";
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
        private void OnZerorateddomesticsalesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipZerorateddomesticsalesAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = AppResources.ZVatClickFaqInstructions;
            if (App.IsArabic)
            {
                popUp.Link = "https://www.vat.gov.sa/ar/vat-rate";
            }
            else
            {
                popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            }
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
        private void OnZerorateddomesticsalesAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipZerorateddomesticsalesAdjustment;
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
        private void OnExportsAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExportsAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = AppResources.ZVatClickFaqInstructions;
            if (App.IsArabic)
            {
                popUp.Link = "https://www.vat.gov.sa/ar/vat-rate";
            }
            else
            {
                popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            }
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
        private void OnExportsAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExportsAdjustment;
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
        private void OnExemptAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExemptAdjustment;
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
        private void OnStandardrateddomesticpurchasesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipStandardrateddomesticpurchasesAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = AppResources.ZVatClickFaqInstructions;
            if (App.IsArabic)
            {
                popUp.Link = "https://www.vat.gov.sa/ar/vat-rate";
            }
            else
            {
                popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            }
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
        private void OnVatImportsVatPaidatcustomsAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipImportssubjecttoVATpaidatcustomsAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = AppResources.ZVatClickFaqInstructions;
            if (App.IsArabic)
            {
                popUp.Link = "https://www.vat.gov.sa/ar/vat-rate";
            }
            else
            {
                popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            }
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
        private void OnVatImportsVatPaidatcustomsAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipImportssubjecttoVATpaidatcustomsAdjustment;
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
        private void OnVatZeroRatedPurchasesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipZeroratedpurchasesAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = AppResources.ZVatClickFaqInstructions;
            if (App.IsArabic)
            {
                popUp.Link = "https://www.vat.gov.sa/ar/vat-rate";
            }
            else
            {
                popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            }
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
        private void OnVatExemptPurchasesAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExemptpurchasesAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = AppResources.ZVatClickFaqInstructions;
            if (App.IsArabic)
            {
                popUp.Link = "https://www.vat.gov.sa/ar/vat-rate";
            }
            else
            {
                popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            }
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
        private void OnVatExemptPurchasesAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExemptpurchasesAdjustment;
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
        private void OnVatcreditcarriedforwardClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipcreditcarriedforwardfrompreviousperiod;
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
        private void OnVatNetdueClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipNetVATdue;
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
        private void OnExemptAmountClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipExemptAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = AppResources.ZVatClickFaqInstructions;
            if (App.IsArabic)
            {
                popUp.Link = "https://www.vat.gov.sa/ar/vat-rate";
            }
            else
            {
                popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            }
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
        private void OnStandardrateddomesticpurchasesAdjustmentClicked(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipStandardrateddomesticpurchasesAdjustment;
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
        private void OnVatImportsSubjectToVatAccountedAmountClickedNew(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipImportssubjecttoVATaccountedAmount;
            popUp.IsLinkAvailable = true;
            popUp.LinkMessage = AppResources.ZVatClickFaqInstructions;
            if (App.IsArabic)
            {
                popUp.Link = "https://www.vat.gov.sa/ar/vat-rate";
            }
            else
            {
                popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            }
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
        private void OnVatImportsSubjectToVatAccountedAdjustmentClickedNew(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipImportssubjecttoVATaccountedAdjustment;
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
        private void OnVatImportsSubjectToVatAccountedVatAmountClickedNew(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipImportssubjecttoVATaccountedVatAmount;
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
        private void OnVatZeroRatedPurchasesAdjustmentClickedNew(object sender, EventArgs e)
        {
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipZeroratedpurchasesAdjustment;
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
        private void OnVatcreditcarriedforwardFromPreviousPeriodClicked(object sender, EventArgs e)
        {
            // < 5,000 >
            PopUp popUp = new PopUp();
            popUp.Message = AppResources.ZToolTipCorrectionsfrompreviousperiod;
            String MessageWithPositiveValue = popUp.Message.Replace("<5,000>", viewModel.CorrectionPeriodAmount);
            String MessageWithNegativeValue = popUp.Message.Replace("<-5,000>", MessageWithPositiveValue);
            popUp.Message = MessageWithNegativeValue;
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
        public bool CheckMandetoryFields()
        {
            bool IsAllEntered = true;
            try
            {
                if ((TabVatReturn.IsVisible == true && viewModel.IsGetAcknowledgementClicked != true) || viewModel.IsVATReturnFieldCheckForSaveAsDraft)
                {
                    if (string.IsNullOrEmpty(EntryVatAmount.Text) || EntryVatAmount.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryVatAmountFrame.HasError = true;
                    }
                    else
                    {
                        EntryVatAmountFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) || EntryVatAdjustmentWithSAR.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryVatAdjustmentWithSARFrame.HasError = true;
                    }
                    else
                    {
                        EntryVatAdjustmentWithSARFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryStdsalesVat.Text) || EntryStdsalesVat.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                    }
                    if (string.IsNullOrEmpty(EntrySalesGccAmt.Text) || EntrySalesGccAmt.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntrySalesGccAmtFrame.HasError = true;
                    }
                    else
                    {
                        EntrySalesGccAmtFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntrySalesGccAdj.Text) || EntrySalesGccAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntrySalesGccAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntrySalesGccAdjFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryZerosalesAmt.Text) || EntryZerosalesAmt.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryZerosalesAmtFrame.HasError = true;
                    }
                    else
                    {
                        EntryZerosalesAmtFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryZerosalesAdj.Text) || EntryZerosalesAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryZerosalesAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntryZerosalesAdjFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryExportsAmt.Text) || EntryExportsAmt.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryExportsAmtFrame.HasError = true;
                    }
                    else
                    {
                        EntryExportsAmtFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryExportsAdj.Text) || EntryExportsAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryExportsAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntryExportsAdjFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryExemptsalesAmt.Text) || EntryExemptsalesAmt.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryExemptsalesAmtFrame.HasError = true;
                    }
                    else
                    {
                        EntryExemptsalesAmtFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryExemptsalesAdj.Text) || EntryExemptsalesAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryExemptsalesAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntryExemptsalesAdjFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) || EntryStdpurchaseAmt.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryStdpurchaseAmtFrame.HasError = true;
                    }
                    else
                    {
                        EntryStdpurchaseAmtFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) || EntryStdpurchaseAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryStdpurchaseAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntryStdpurchaseAdjFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryStdpurchasesVat.Text) || EntryStdpurchasesVat.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                    }
                    if (string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) || EntryZVatAmountWithSAR.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryZVatAmountWithSARFrame.HasError = true;
                    }
                    else
                    {
                        EntryZVatAmountWithSARFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryImportspaidAdj.Text) || EntryImportspaidAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryImportspaidAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntryImportspaidAdjFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryImportspaidVat.Text) || EntryImportspaidVat.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                    }
                    if (string.IsNullOrEmpty(EntryImportsaccAmt.Text) || EntryImportsaccAmt.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryImportsaccAmtFrame.HasError = true;
                    }
                    else
                    {
                        EntryImportsaccAmtFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryImportsaccAdj.Text) || EntryImportsaccAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryImportsaccAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntryImportsaccAdjFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryImportsaccVat.Text) || EntryImportsaccVat.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                    }
                    if (string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) || EntryZeropurchaseAmt.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryZeropurchaseAmtFrame.HasError = true;
                    }
                    else
                    {
                        EntryZeropurchaseAmtFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) || EntryZeropurchaseAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryZeropurchaseAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntryZeropurchaseAdjFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) || EntryExemptpurchaseAmt.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryExemptpurchaseAmtFrame.HasError = true;
                    }
                    else
                    {
                        EntryExemptpurchaseAmtFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) || EntryExemptpurchaseAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryExemptpurchaseAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntryExemptpurchaseAdjFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryPreperiodcorr.Text) || EntryPreperiodcorr.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryPreperiodcorrFrame.HasError = true;
                    }
                    else
                    {
                        if (viewModel.IsGreaterThanFiveT == false)
                        {
                            EntryPreperiodcorrFrame.HasError = false;
                        }
                        else
                        {
                            EntryPreperiodcorrFrame.HasError = true;
                        }
                    }
                    if (string.IsNullOrEmpty(EntryCreditVat.Text) || EntryCreditVat.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                    }
                    if (viewModel.IsGreaterThanFiveT == true)
                    {
                        IsAllEntered = false;
                    }
                    //if (string.IsNullOrEmpty(EntryNetdueVat.Text))
                    //{
                    //    IsAllEntered = false;
                    //}
                    if (IsAllEntered == false)
                    {
                        viewModel.IsMainButtonEnabled = false;
                        // BtnNextStep.IsEnabled = false;
                    }
                    else
                    {
                        // viewModel.IsDeclarationCheckedForInstruction = false;
                        viewModel.IsMainButtonEnabled = true;
                        //  BtnNextStep.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return IsAllEntered;
        }
        //Test    
        private void EntryPreperiodcorr_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryPreperiodcorr.Text) && EntryPreperiodcorr.Text.Contains(","))
                    //{
                    //    EntryPreperiodcorr.Text = EntryPreperiodcorr.Text.Replace(",", "");
                    //   // EntryPreperiodcorr.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabicWithMinus(senderObj.Text);
                    }
                    else
                    {
                        viewModel.IsSwitchToggled = false;
                    }
                    if (isArabicChecked)
                    {
                        CheckMandetoryFields();
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryCreditVat_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryNetdueVat_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void chkDeclarationForSummary_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (viewModel.IsDeclarationCheckedForSummary != true)
            {
                if (TabSummarry.IsVisible == true)
                {
                    CheckBox CheckSummary = (CheckBox)sender;
                    if (CheckSummary.IsChecked == true)
                    {
                        viewModel.IsMainButtonEnabled = true;
                        //  BtnNextStep.IsEnabled = true;
                    }
                    else
                    {
                        viewModel.IsMainButtonEnabled = false;
                        // BtnNextStep.IsEnabled = false;
                    }
                }
            }
            else
            {
                ValidationsForVATRefund();
            }
        }
        private void chkClearification_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (TabTaxPayerDetails.IsVisible == true)
            {
                CheckBox CheckSummary = (CheckBox)sender;
                if (CheckSummary.IsChecked == true)
                {
                    viewModel.IsMainButtonEnabled = true;
                    //  BtnNextStep.IsEnabled = true;
                }
                else
                {
                    viewModel.IsMainButtonEnabled = false;
                    //  BtnNextStep.IsEnabled = false;
                }
            }
        }
        private void EntryVatAmount_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (viewModel.IsVisibleVatReturnForm == true)
                {
                    if (!string.IsNullOrEmpty(EntryVatAmount.Text) && !string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) && EntryVatAmount.Text != "." && EntryVatAdjustmentWithSAR.Text != "." && EntryVatAmount.Text != "," && EntryVatAdjustmentWithSAR.Text != ",")
                    {
                        CheckOneaOneb(Convert.ToDecimal(EntryVatAmount.Text), Convert.ToDecimal(EntryVatAdjustmentWithSAR.Text));
                    }
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryVatAmount.Text);
                    EntryVatAmount.Text = ValueWithComma;
                    EntryVatAmount.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryVatAdjustmentWithSAR_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryVatAmount.Text) && !string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) && EntryVatAmount.Text != "." && EntryVatAdjustmentWithSAR.Text != "." && EntryVatAmount.Text != "," && EntryVatAdjustmentWithSAR.Text != ",")
                {
                    CheckOneaOneb(Convert.ToDecimal(EntryVatAmount.Text), Convert.ToDecimal(EntryVatAdjustmentWithSAR.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryVatAdjustmentWithSAR.Text);
                    EntryVatAdjustmentWithSAR.Text = ValueWithComma;
                    EntryVatAdjustmentWithSAR.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckOneaOneb(decimal EntryVatAmount, decimal EntryVatAdjustmentWithSAR)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                try
                {
                    if (EntryVatAmount == 0 && EntryVatAdjustmentWithSAR > 0)
                    {
                        PopUp Pop = new PopUp();
                        Pop.IsLinkAvailable = false;
                        Pop.Message = AppResources.ZZValidationMessage02_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                        if (App.IsArabic)
                        {
                            Pop.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            Pop.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                    }
                    else
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryVatAmount / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryVatAmount) + EntryVatAmount < EntryVatAdjustmentWithSAR)
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Pop.Message = string.Format(AppResources.ZZValidationMessage01_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntrySalesGccAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntrySalesGccAmt.Text) && !string.IsNullOrEmpty(EntrySalesGccAdj.Text) && EntrySalesGccAmt.Text != "." && EntrySalesGccAdj.Text != "." && EntrySalesGccAmt.Text != "," && EntrySalesGccAdj.Text != ",")
                {
                    CheckTwoaTwob(Convert.ToDecimal(EntrySalesGccAmt.Text), Convert.ToDecimal(EntrySalesGccAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntrySalesGccAmt.Text);
                    EntrySalesGccAmt.Text = ValueWithComma;
                    EntrySalesGccAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntrySalesGccAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntrySalesGccAmt.Text) && !string.IsNullOrEmpty(EntrySalesGccAdj.Text) && EntrySalesGccAmt.Text != "." && EntrySalesGccAdj.Text != "." && EntrySalesGccAmt.Text != "," && EntrySalesGccAdj.Text != ",")
                {
                    CheckTwoaTwob(Convert.ToDecimal(EntrySalesGccAmt.Text), Convert.ToDecimal(EntrySalesGccAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntrySalesGccAdj.Text);
                    EntrySalesGccAdj.Text = ValueWithComma;
                    EntrySalesGccAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckTwoaTwob(decimal EntrySalesGccAmt, decimal EntrySalesGccAdj)
        {
            try
            {
                if (viewModel.IsVisibleVatReturnForm == true)
                {
                    try
                    {
                        if (EntrySalesGccAmt == 0 && EntrySalesGccAdj > 0)
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Pop.Message = AppResources.ZZValidationMessage04_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                        else
                        {
                            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                            // decimal PercentageValue = (EntrySalesGccAmt / 100) * Convert.ToDecimal(Percentage);
                            if (((Convert.ToDecimal(Percentage) / 100) * EntrySalesGccAmt) + EntrySalesGccAmt < EntrySalesGccAdj)
                            {
                                PopUp Pop = new PopUp();
                                Pop.IsLinkAvailable = false;
                                Pop.Message = string.Format(AppResources.ZZValidationMessage03_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                                if (App.IsArabic)
                                {
                                    Pop.FlowDirections = "RightToLeft";
                                }
                                else
                                {
                                    Pop.FlowDirections = "LeftToRight";
                                }
                                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZerosalesAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                Entry Ent = (Entry)sender;
                string Message = string.Empty;
                if (Ent.Id.ToString() == EntryZerosalesAmt.Id.ToString())
                {
                    IGRTSetResult IGRTSetModel = viewModel.CalculationRateIGRTSet.Where(a => a.GrpNo == viewModel.ResponseVATDeclarationD.GrpNo).FirstOrDefault();
                    if (IGRTSetModel != null && IGRTSetModel.RateTrtmt != null)
                    {
                        if (IGRTSetModel.RateTrtmt != "Z")
                        {
                            if (viewModel.ResponseVATDeclarationD.ZerosalesAmt != "." && !viewModel.ResponseVATDeclarationD.ZerosalesAmt.Contains("-") && !string.IsNullOrEmpty(viewModel.ResponseVATDeclarationD.ZerosalesAmt))
                            {
                                if (Convert.ToDouble(viewModel.ResponseVATDeclarationD.ZerosalesAmt) > 0)
                                {
                                    //PopUp popUp = new PopUp();
                                    Message = AppResources.ZZOurrecordsindicatethatyouarenotapartofthezerorated;
                                    //popUp.IsLinkAvailable = false;
                                    //if (App.IsArabic)
                                    //{
                                    //    popUp.FlowDirections = "RightToLeft";
                                    //}
                                    //else
                                    //{
                                    //    popUp.FlowDirections = "LeftToRight";
                                    //}
                                    //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (viewModel.ResponseVATDeclarationD.GrpNo == "00" || viewModel.ResponseVATDeclarationD.GrpNo == "0")
                        {
                            if (viewModel.ResponseVATDeclarationD.ZerosalesAmt != "." && !viewModel.ResponseVATDeclarationD.ZerosalesAmt.Contains("-") && !string.IsNullOrEmpty(viewModel.ResponseVATDeclarationD.ZerosalesAmt))
                            {
                                if (Convert.ToDouble(viewModel.ResponseVATDeclarationD.ZerosalesAmt) > 0)
                                {
                                    //PopUp popUp = new PopUp();
                                    Message = AppResources.ZZOurrecordsindicatethatyouarenotapartofthezerorated;
                                }
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(EntryZerosalesAmt.Text) && !string.IsNullOrEmpty(EntryZerosalesAdj.Text) && EntryZerosalesAmt.Text != "." && EntryZerosalesAdj.Text != "." && EntryZerosalesAmt.Text != "," && EntryZerosalesAdj.Text != ",")
                {
                    CheckThreeaThreeb(Convert.ToDecimal(EntryZerosalesAmt.Text), Convert.ToDecimal(EntryZerosalesAdj.Text), Message);
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryZerosalesAmt.Text);
                    EntryZerosalesAmt.Text = ValueWithComma;
                    EntryZerosalesAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZerosalesAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZerosalesAmt.Text) && !string.IsNullOrEmpty(EntryZerosalesAdj.Text) && EntryZerosalesAmt.Text != "." && EntryZerosalesAdj.Text != "." && EntryZerosalesAmt.Text != "," && EntryZerosalesAdj.Text != ",")
                {
                    CheckThreeaThreeb(Convert.ToDecimal(EntryZerosalesAmt.Text), Convert.ToDecimal(EntryZerosalesAdj.Text), "");
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryZerosalesAdj.Text);
                    EntryZerosalesAdj.Text = ValueWithComma;
                    EntryZerosalesAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckThreeaThreeb(decimal EntryZerosalesAmt, decimal EntryZerosalesAdj, string Massege)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                try
                {
                    StringBuilder Masseges = new StringBuilder();
                    if (!string.IsNullOrEmpty(Massege))
                    {
                        Masseges.Append(Massege);
                        PopUp Pop = new PopUp();
                        Pop.IsLinkAvailable = false;
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryZerosalesAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryZerosalesAmt) + EntryZerosalesAmt < EntryZerosalesAdj)
                        {
                            Masseges.Append(Environment.NewLine);
                            Masseges.Append(Environment.NewLine);
                            Masseges.Append(string.Format(AppResources.ZZValidationMessage06_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]));
                        }
                        Pop.Message = Masseges.ToString();
                        if (Pop.Message.Length > 0)
                        {
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                    else
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryZerosalesAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryZerosalesAmt) + EntryZerosalesAmt < EntryZerosalesAdj)
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Pop.Message = string.Format(AppResources.ZZValidationMessage06_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryExportsAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                string Message = string.Empty;
                Entry Ent = (Entry)sender;
                if (Ent.Id.ToString() == EntryExportsAmt.Id.ToString())
                {
                    if (viewModel.ResponseVATDeclarationD.ExporterFg == "0")
                    {
                        if (viewModel.ResponseVATDeclarationD.ExportsAmt != "." && !viewModel.ResponseVATDeclarationD.ExportsAmt.Contains("-") && !string.IsNullOrEmpty(viewModel.ResponseVATDeclarationD.ExportsAmt))
                        {
                            if (Convert.ToDouble(viewModel.ResponseVATDeclarationD.ExportsAmt) > 0)
                            {
                                // PopUp popUp = new PopUp();
                                Message = AppResources.ZZOurrecordsindicatethatyouarenotmainly;
                                //popUp.IsLinkAvailable = false;
                                //if (App.IsArabic)
                                //{
                                //    popUp.FlowDirections = "RightToLeft";
                                //}
                                //else
                                //{
                                //    popUp.FlowDirections = "LeftToRight";
                                //}
                                //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(EntryExportsAmt.Text) && !string.IsNullOrEmpty(EntryExportsAdj.Text) && EntryExportsAmt.Text != "." && EntryExportsAdj.Text != "." && EntryExportsAmt.Text != "," && EntryExportsAdj.Text != ",")
                {
                    CheckFouraFourb(Convert.ToDecimal(EntryExportsAmt.Text), Convert.ToDecimal(EntryExportsAdj.Text), Message);
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryExportsAmt.Text);
                    EntryExportsAmt.Text = ValueWithComma;
                    EntryExportsAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExportsAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryExportsAmt.Text) && !string.IsNullOrEmpty(EntryExportsAdj.Text) && EntryExportsAmt.Text != "." && EntryExportsAdj.Text != "." && EntryExportsAmt.Text != "," && EntryExportsAdj.Text != ",")
                {
                    CheckFouraFourb(Convert.ToDecimal(EntryExportsAmt.Text), Convert.ToDecimal(EntryExportsAdj.Text), "");
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryExportsAdj.Text);
                    EntryExportsAdj.Text = ValueWithComma;
                    EntryExportsAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckFouraFourb(decimal EntryExportsAmt, decimal EntryExportsAdj, string Massege)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        StringBuilder Masseges = new StringBuilder();
                        if (!string.IsNullOrEmpty(Massege))
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Masseges.Append(Massege);
                            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                            // decimal PercentageValue = (EntryExportsAmt / 100) * Convert.ToDecimal(Percentage);
                            if (((Convert.ToDecimal(Percentage) / 100) * EntryExportsAmt) + EntryExportsAmt < EntryExportsAdj)
                            {
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(string.Format(AppResources.ZZValidationMessage08_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]));
                            }
                            Pop.Message = Masseges.ToString();
                            if (Pop.Message.Length > 0)
                            {
                                if (App.IsArabic)
                                {
                                    Pop.FlowDirections = "RightToLeft";
                                }
                                else
                                {
                                    Pop.FlowDirections = "LeftToRight";
                                }
                                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                            }
                        }
                        else
                        {
                            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                            // decimal PercentageValue = (EntryExportsAmt / 100) * Convert.ToDecimal(Percentage);
                            if (((Convert.ToDecimal(Percentage) / 100) * EntryExportsAmt) + EntryExportsAmt < EntryExportsAdj)
                            {
                                PopUp Pop = new PopUp();
                                Pop.IsLinkAvailable = false;
                                Pop.Message = string.Format(AppResources.ZZValidationMessage08_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                                if (App.IsArabic)
                                {
                                    Pop.FlowDirections = "RightToLeft";
                                }
                                else
                                {
                                    Pop.FlowDirections = "LeftToRight";
                                }
                                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryExemptsalesAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                string Message = string.Empty;
                Entry Ent = (Entry)sender;
                if (Ent.Id.ToString() == EntryExemptsalesAmt.Id.ToString())
                {
                    IGRTSetResult IGRTSetModel = viewModel.CalculationRateIGRTSet.Where(a => a.GrpNo == viewModel.ResponseVATDeclarationD.GrpNo).FirstOrDefault();
                    if (IGRTSetModel != null && IGRTSetModel.RateTrtmt != null)
                    {
                        if (IGRTSetModel.RateTrtmt != "E")
                        {
                            if (viewModel.ResponseVATDeclarationD.ExemptsalesAmt != "." && !viewModel.ResponseVATDeclarationD.ExemptsalesAmt.Contains("-") && !string.IsNullOrEmpty(viewModel.ResponseVATDeclarationD.ExemptsalesAmt))
                            {
                                if (Convert.ToDouble(viewModel.ResponseVATDeclarationD.ExemptsalesAmt) > 0)
                                {
                                    //PopUp popUp = new PopUp();
                                    Message = AppResources.ZZValidationMessage09_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                                    //popUp.IsLinkAvailable = false;
                                    //if (App.IsArabic)
                                    //{
                                    //    popUp.FlowDirections = "RightToLeft";
                                    //}
                                    //else
                                    //{
                                    //    popUp.FlowDirections = "LeftToRight";
                                    //}
                                    //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                }
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(EntryExemptsalesAmt.Text) && !string.IsNullOrEmpty(EntryExemptsalesAdj.Text) && EntryExemptsalesAmt.Text != "." && EntryExemptsalesAdj.Text != "." && EntryExemptsalesAmt.Text != "," && EntryExemptsalesAdj.Text != ",")
                {
                    CheckFiveaFiveb(Convert.ToDecimal(EntryExemptsalesAmt.Text), Convert.ToDecimal(EntryExemptsalesAdj.Text), Message);
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryExemptsalesAmt.Text);
                    EntryExemptsalesAmt.Text = ValueWithComma;
                    EntryExemptsalesAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExemptsalesAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryExemptsalesAmt.Text) && !string.IsNullOrEmpty(EntryExemptsalesAdj.Text) && EntryExemptsalesAmt.Text != "." && EntryExemptsalesAdj.Text != "." && EntryExemptsalesAmt.Text != "," && EntryExemptsalesAdj.Text != ",")
                {
                    CheckFiveaFiveb(Convert.ToDecimal(EntryExemptsalesAmt.Text), Convert.ToDecimal(EntryExemptsalesAdj.Text), "");
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryExemptsalesAdj.Text);
                    EntryExemptsalesAdj.Text = ValueWithComma;
                    EntryExemptsalesAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckFiveaFiveb(decimal EntryExemptsalesAmt, decimal EntryExemptsalesAdj, string Message)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                try
                {
                    StringBuilder Masseges = new StringBuilder();
                    if (!string.IsNullOrEmpty(Message))
                    {
                        PopUp Pop = new PopUp();
                        Masseges.Append(Message);
                        if (viewModel.CalculationRateSetVTTH != null)
                        {
                            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                            //  decimal PercentageValue = (EntryExemptsalesAmt / 100) * Convert.ToDecimal(Percentage);
                            if (((Convert.ToDecimal(Percentage) / 100) * EntryExemptsalesAmt) + EntryExemptsalesAmt < EntryExemptsalesAdj)
                            {
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(string.Format(AppResources.ZZValidationMessage10_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]));
                            }
                            if (Masseges.Length > 0)
                            {
                                Pop.Message = Masseges.ToString();
                                Pop.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    Pop.FlowDirections = "RightToLeft";
                                }
                                else
                                {
                                    Pop.FlowDirections = "LeftToRight";
                                }
                                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                            }
                        }
                    }
                    else
                    {
                        if (viewModel.CalculationRateSetVTTH != null)
                        {
                            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                            //  decimal PercentageValue = (EntryExemptsalesAmt / 100) * Convert.ToDecimal(Percentage);
                            if (((Convert.ToDecimal(Percentage) / 100) * EntryExemptsalesAmt) + EntryExemptsalesAmt < EntryExemptsalesAdj)
                            {
                                PopUp Pop = new PopUp();
                                Pop.IsLinkAvailable = false;
                                Pop.Message = string.Format(AppResources.ZZValidationMessage10_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                                if (App.IsArabic)
                                {
                                    Pop.FlowDirections = "RightToLeft";
                                }
                                else
                                {
                                    Pop.FlowDirections = "LeftToRight";
                                }
                                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }
        public void CheckSixaSixb(decimal LabelTotalsalesAmt, decimal LabelTotalsalesAdj)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //   decimal PercentageValue = (LabelTotalsalesAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * LabelTotalsalesAmt) + LabelTotalsalesAmt < LabelTotalsalesAdj)
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Pop.Message = string.Format(AppResources.ZZValidationMessage11_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryStdpurchaseAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) && !string.IsNullOrEmpty(LabelTotalsalesAmt.Text) && EntryStdpurchaseAmt.Text != "." && LabelTotalsalesAmt.Text != "." && EntryStdpurchaseAmt.Text != "," && LabelTotalsalesAmt.Text != ",")
                {
                    CheckSevenaSixa(Convert.ToDecimal(EntryStdpurchaseAmt.Text), Convert.ToDecimal(LabelTotalsalesAmt.Text));
                }
                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) && !string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) && EntryStdpurchaseAmt.Text != "." && EntryStdpurchaseAdj.Text != "." && EntryStdpurchaseAmt.Text != "," && EntryStdpurchaseAdj.Text != ",")
                {
                    CheckSevenaSevenb(Convert.ToDecimal(EntryStdpurchaseAmt.Text), Convert.ToDecimal(EntryStdpurchaseAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryStdpurchaseAmt.Text);
                    EntryStdpurchaseAmt.Text = ValueWithComma;
                    EntryStdpurchaseAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryStdpurchaseAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) && !string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) && EntryStdpurchaseAmt.Text != "." && EntryStdpurchaseAdj.Text != "." && EntryStdpurchaseAmt.Text != "," && EntryStdpurchaseAdj.Text != ",")
                {
                    CheckSevenaSevenb(Convert.ToDecimal(EntryStdpurchaseAmt.Text), Convert.ToDecimal(EntryStdpurchaseAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryStdpurchaseAdj.Text);
                    EntryStdpurchaseAdj.Text = ValueWithComma;
                    EntryStdpurchaseAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckSevenaSixa(decimal EntryStdpurchaseAmt, decimal LabelTotalsalesAmt)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                if (EntryStdpurchaseAmt > LabelTotalsalesAmt)
                {
                    PopUp Pop = new PopUp();
                    Pop.IsLinkAvailable = false;
                    Pop.Message = AppResources.ZZValidationMessage12_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                    if (App.IsArabic)
                    {
                        Pop.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        Pop.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                }
            }
        }
        public void CheckSevenaSevenb(decimal EntryStdpurchaseAmt, decimal EntryStdpurchaseAdj)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryStdpurchaseAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryStdpurchaseAmt) + EntryStdpurchaseAmt < EntryStdpurchaseAdj)
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Pop.Message = string.Format(AppResources.ZZValidationMessage13_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryZVatAmountWithSAR_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) && !string.IsNullOrEmpty(EntryImportspaidAdj.Text) && EntryZVatAmountWithSAR.Text != "." && EntryImportspaidAdj.Text != "." && EntryZVatAmountWithSAR.Text != "," && EntryImportspaidAdj.Text != ",")
                {
                    CheckEightaEightb(Convert.ToDecimal(EntryZVatAmountWithSAR.Text), Convert.ToDecimal(EntryImportspaidAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryZVatAmountWithSAR.Text);
                    EntryZVatAmountWithSAR.Text = ValueWithComma;
                    EntryZVatAmountWithSAR.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryImportspaidAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) && !string.IsNullOrEmpty(EntryImportspaidAdj.Text) && EntryZVatAmountWithSAR.Text != "." && EntryImportspaidAdj.Text != "." && EntryZVatAmountWithSAR.Text != "," && EntryImportspaidAdj.Text != ",")
                {
                    CheckEightaEightb(Convert.ToDecimal(EntryZVatAmountWithSAR.Text), Convert.ToDecimal(EntryImportspaidAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryImportspaidAdj.Text);
                    EntryImportspaidAdj.Text = ValueWithComma;
                    EntryImportspaidAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckEightaEightb(decimal EntryZVatAmountWithSAR, decimal EntryImportspaidAdj)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryZVatAmountWithSAR / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryZVatAmountWithSAR) + EntryZVatAmountWithSAR < EntryImportspaidAdj)
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Pop.Message = string.Format(AppResources.ZZValidationMessage14_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryImportsaccAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryImportsaccAmt.Text) && !string.IsNullOrEmpty(EntryImportsaccAdj.Text) && EntryImportsaccAmt.Text != "." && EntryImportsaccAdj.Text != "." && EntryImportsaccAmt.Text != "," && EntryImportsaccAdj.Text != ",")
                {
                    CheckNineaNineb(Convert.ToDecimal(EntryImportsaccAmt.Text), Convert.ToDecimal(EntryImportsaccAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryImportsaccAmt.Text);
                    EntryImportsaccAmt.Text = ValueWithComma;
                    EntryImportsaccAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryImportsaccAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryImportsaccAmt.Text) && !string.IsNullOrEmpty(EntryImportsaccAdj.Text) && EntryImportsaccAmt.Text != "." && EntryImportsaccAdj.Text != "." && EntryImportsaccAmt.Text != "," && EntryImportsaccAdj.Text != ",")
                {
                    CheckNineaNineb(Convert.ToDecimal(EntryImportsaccAmt.Text), Convert.ToDecimal(EntryImportsaccAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryImportsaccAdj.Text);
                    EntryImportsaccAdj.Text = ValueWithComma;
                    EntryImportsaccAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckNineaNineb(decimal EntryImportsaccAmt, decimal EntryImportsaccAdj)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryImportsaccAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryImportsaccAmt) + EntryImportsaccAmt < EntryImportsaccAdj)
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Pop.Message = string.Format(AppResources.ZZValidationMessage15_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryZeropurchaseAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) && !string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) && EntryZeropurchaseAmt.Text != "." && EntryZeropurchaseAdj.Text != "." && EntryZeropurchaseAmt.Text != "," && EntryZeropurchaseAdj.Text != ",")
                {
                    CheckTenaTenb(Convert.ToDecimal(EntryZeropurchaseAmt.Text), Convert.ToDecimal(EntryZeropurchaseAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryZeropurchaseAmt.Text);
                    EntryZeropurchaseAmt.Text = ValueWithComma;
                    EntryZeropurchaseAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZeropurchaseAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) && !string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) && EntryZeropurchaseAmt.Text != "." && EntryZeropurchaseAdj.Text != "." && EntryZeropurchaseAmt.Text != "," && EntryZeropurchaseAdj.Text != ",")
                {
                    CheckTenaTenb(Convert.ToDecimal(EntryZeropurchaseAmt.Text), Convert.ToDecimal(EntryZeropurchaseAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryZeropurchaseAdj.Text);
                    EntryZeropurchaseAdj.Text = ValueWithComma;
                    EntryZeropurchaseAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckTenaTenb(decimal EntryZeropurchaseAmt, decimal EntryZeropurchaseAdj)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //   decimal PercentageValue = (EntryZeropurchaseAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryZeropurchaseAmt) + EntryZeropurchaseAmt < EntryZeropurchaseAdj)
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Pop.Message = string.Format(AppResources.ZZValidationMessage16_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryExemptpurchaseAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) && !string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) && EntryExemptpurchaseAmt.Text != "." && EntryExemptpurchaseAdj.Text != "." && EntryExemptpurchaseAmt.Text != "," && EntryExemptpurchaseAdj.Text != ",")
                {
                    CheckElevenaElevenb(Convert.ToDecimal(EntryExemptpurchaseAmt.Text), Convert.ToDecimal(EntryExemptpurchaseAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryExemptpurchaseAmt.Text);
                    EntryExemptpurchaseAmt.Text = ValueWithComma;
                    EntryExemptpurchaseAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExemptpurchaseAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) && !string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) && EntryExemptpurchaseAmt.Text != "." && EntryExemptpurchaseAdj.Text != "." && EntryExemptpurchaseAmt.Text != "," && EntryExemptpurchaseAdj.Text != ",")
                {
                    CheckElevenaElevenb(Convert.ToDecimal(EntryExemptpurchaseAmt.Text), Convert.ToDecimal(EntryExemptpurchaseAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryExemptpurchaseAdj.Text);
                    EntryExemptpurchaseAdj.Text = ValueWithComma;
                    EntryExemptpurchaseAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckElevenaElevenb(decimal EntryExemptpurchaseAmt, decimal EntryExemptpurchaseAdj)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryExemptpurchaseAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryExemptpurchaseAmt) + EntryExemptpurchaseAmt < EntryExemptpurchaseAdj)
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Pop.Message = string.Format(AppResources.ZZValidationMessage17_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        public void CheckSixaTweveb(decimal LabelTotalsalesAmt, decimal LabelTotalpurchaseAmt)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                if (viewModel.CalculationRateSetVTTH != null)
                {
                    string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                    //  decimal PercentageValue = (LabelTotalsalesAmt / 100) * Convert.ToDecimal(Percentage);
                    if (((Convert.ToDecimal(Percentage) / 100) * LabelTotalsalesAmt) + LabelTotalsalesAmt < LabelTotalpurchaseAmt)
                    {
                        PopUp Pop = new PopUp();
                        Pop.IsLinkAvailable = false;
                        Pop.Message = AppResources.ZZValidationMessage18_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                        if (App.IsArabic)
                        {
                            Pop.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            Pop.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                    }
                }
            }
        }
        public void CheckTweveaTweveb(decimal LabelTotalpurchaseAmt, decimal LabelTotalpurchaseAdj)
        {
            if (viewModel.IsVisibleVatReturnForm == true)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (LabelTotalpurchaseAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * LabelTotalpurchaseAmt) + LabelTotalpurchaseAmt < LabelTotalpurchaseAdj)
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Pop.Message = string.Format(AppResources.ZZValidationMessage19_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        public void CheckThirteenaFouteenb(decimal LabelTotaldueVat, decimal EntryPreperiodcorr)
        {
            try
            {
                if (viewModel.CalculationRateSetVTTH != null)
                {
                    string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                    //  decimal PercentageValue = (LabelTotaldueVat / 100) * Convert.ToDecimal(Percentage);
                    if (((Convert.ToDecimal(Percentage) / 100) * LabelTotaldueVat) + LabelTotaldueVat < EntryPreperiodcorr)
                    {
                        //PopUp Pop = new PopUp();
                        //Pop.IsLinkAvailable = false;
                        //Pop.Message = string.Format(AppResources.ZZValidationMessage20_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                    }
                }
            }
            catch
            {
            }
        }
        private void EntryPreperiodcorr_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (EntryPreperiodcorr.Text.Equals("-.") || EntryPreperiodcorr.Text.Equals("."))
                {
                    EntryPreperiodcorr.Text = "0.00";
                    viewModel.IsSwitchToggled = false;
                    return;
                }
                if (!string.IsNullOrEmpty(EntryPreperiodcorr.Text) && EntryPreperiodcorr.Text != "." && EntryPreperiodcorr.Text != "-" && EntryPreperiodcorr.Text != ",")
                {
                    string MinValue = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "001").Select(x => x.MinVal).FirstOrDefault();
                    string MaxValue = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "001").Select(x => x.MaxVal).FirstOrDefault();
                    if (Convert.ToDecimal(EntryPreperiodcorr.Text) <= Convert.ToDecimal(MinValue) || Convert.ToDecimal(EntryPreperiodcorr.Text) >= Convert.ToDecimal(MaxValue))
                    {
                        viewModel._dialogService.ShowMessage(string.Format(AppResources.ZZGeneralMessage_IfCorrectionsGreaterThanEqualToMAxValueAndLessThanEqualToMinValue, MaxValue, MinValue), AppResources.Information);
                    }
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryPreperiodcorr.Text);
                    EntryPreperiodcorr.Text = ValueWithComma;
                    EntryPreperiodcorr.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch
            {
            }
        }
        private void Iban_Changed(object sender, TextChangedEventArgs e)
        {
            bool a = false;
            string allowedchar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            if (!string.IsNullOrEmpty(viewModel.IbanNumberText))
            {
                if (!viewModel.IbanNumberText.All(allowedchar.Contains))
                {
                    viewModel.IbanNumberText = viewModel.IbanNumberText.Remove(viewModel.IbanNumberText.Length - 1);
                }
                else
                {
                    if (viewModel.IbanNumberText.Length > 24)
                    {
                        viewModel.IbanNumberText = viewModel.IbanNumberText.Remove(viewModel.IbanNumberText.Length - 1);
                    }
                    a = UtilityManager.IsIBANValid(viewModel.IbanNumberText);
                }
                viewModel.IsIBANValid = a;
            }
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    ValidationsForVATRefund();
                }
            }
        }
        public void ValidationsForVATRefund()
        {
            bool resultForBilledOrNot = false;
            resultForBilledOrNot = viewModel.IsReturnIsBilledOrAmend();
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    if (viewModel.IsCheckedRefund == true)
                    {
                        if (!string.IsNullOrEmpty(viewModel.IbanNumberText) && viewModel.SelectedIBANType != null && viewModel.SelectedIBANIDNumber != null && viewModel.IsDeclarationCheckedForSummary != false && viewModel.IschkRefundDeclaration != false && viewModel.IsIBANValid == true)
                        {
                            if (!resultForBilledOrNot)
                            {
                                viewModel.IsMainButtonEnabled = true;
                            }
                            else
                            {
                                viewModel.IsMainButtonEnabled = false;
                            }
                        }
                        else
                        {
                            viewModel.IsMainButtonEnabled = false;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(viewModel.TxtSelectedIBAN) && !string.IsNullOrEmpty(viewModel.TxtSelectedIBANType) && !string.IsNullOrEmpty(viewModel.TxtSelectedIBANIDNumber) && viewModel.IsDeclarationCheckedForSummary != false && viewModel.IschkRefundDeclaration != false)
                        {
                            if (!resultForBilledOrNot)
                            {
                                viewModel.IsMainButtonEnabled = true;
                            }
                            else
                            {
                                viewModel.IsMainButtonEnabled = false;
                            }
                        }
                        else
                        {
                            viewModel.IsMainButtonEnabled = false;
                        }
                    }
                }
            }
        }
        private void BPicker2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    ValidationsForVATRefund();
                }
            }
        }
        private void BPicker1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    ValidationsForVATRefund();
                }
            }
        }
        private void BPicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    ValidationsForVATRefund();
                }
            }
        }
        private void Unfocused_IBAN(object sender, FocusEventArgs e)
        {
            try
            {
                try
                {
                    var response = WebServiceManager.GAZTCheckIBAN(viewModel.IbanNumberText);
                    if (response != null)
                    {
                        viewModel.IsIBANValid = true;
                    }
                    else
                    {
                        viewModel.IsIBANValid = false;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            viewModel._dialogService.ShowMessage(AppResources.ZZIBANisincorrect, AppResources.Information);
                        });
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
            catch (Exception ex)
            {
                viewModel.IsIBANValid = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    viewModel._dialogService.ShowMessage(AppResources.ZZIBANisincorrect, AppResources.Information);
                });
            }
        }
        private void chkRefundDeclaration_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ValidationsForVATRefund();
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.vat.gov.sa/en/introduction-to-vat/faq/general-faqs"));
        }
        private void EntryVatAmountFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryVatAmount.Text=="0.00")
                {
                    EntryVatAmount.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(EntryVatAmount.Text) && EntryVatAmount.Text.Contains(","))
                {
                    EntryVatAmount.Text = EntryVatAmount.Text.Replace(",", "");
                    EntryVatAmount.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryVatAmount_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
            //if (EntryVatAmount.TextColor == Color.Red)
            //{
            //    viewModel.IsMainButtonEnabled = false;
            //    EntryVatAmountFrame.HasError = true;
            //}
            //else
            //{
            //    viewModel.IsMainButtonEnabled = true;
            //    EntryVatAmountFrame.HasError = false;
            //}
        }
        private void EntryVatAdjustmentWithSAR_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryVatAdjustmentFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryVatAdjustmentWithSAR.Text=="0.00")
                {
                    EntryVatAdjustmentWithSAR.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) && EntryVatAdjustmentWithSAR.Text.Contains(","))
                {
                    EntryVatAdjustmentWithSAR.Text = EntryVatAdjustmentWithSAR.Text.Replace(",", "");
                    EntryVatAdjustmentWithSAR.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntrySalesGccAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntrySalesGccAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntrySalesGccAmt.Text=="0.00")
                {
                    EntrySalesGccAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntrySalesGccAmt.Text) && EntrySalesGccAmt.Text.Contains(","))
                {
                    EntrySalesGccAmt.Text = EntrySalesGccAmt.Text.Replace(",", "");
                    EntrySalesGccAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntrySalesGccAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntrySalesGccAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntrySalesGccAdj.Text=="0.00")
                {
                    EntrySalesGccAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntrySalesGccAdj.Text) && EntrySalesGccAdj.Text.Contains(","))
                {
                    EntrySalesGccAdj.Text = EntrySalesGccAdj.Text.Replace(",", "");
                    EntrySalesGccAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZerosalesAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryZerosalesAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryZerosalesAmt.Text=="0.00")
                {
                    EntryZerosalesAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryZerosalesAmt.Text) && EntryZerosalesAmt.Text.Contains(","))
                {
                    EntryZerosalesAmt.Text = EntryZerosalesAmt.Text.Replace(",", "");
                    EntryZerosalesAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZerosalesAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryZerosalesAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryZerosalesAdj.Text=="0.00")
                {
                    EntryZerosalesAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryZerosalesAdj.Text) && EntryZerosalesAdj.Text.Contains(","))
                {
                    EntryZerosalesAdj.Text = EntryZerosalesAdj.Text.Replace(",", "");
                    EntryZerosalesAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExportsAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryExportsAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryExportsAmt.Text=="0.00")
                {
                    EntryExportsAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryExportsAmt.Text) && EntryExportsAmt.Text.Contains(","))
                {
                    EntryExportsAmt.Text = EntryExportsAmt.Text.Replace(",", "");
                    EntryExportsAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExportsAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryExportsAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryExportsAdj.Text=="0.00")
                {
                    EntryExportsAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryExportsAdj.Text) && EntryExportsAdj.Text.Contains(","))
                {
                    EntryExportsAdj.Text = EntryExportsAdj.Text.Replace(",", "");
                    EntryExportsAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExemptsalesAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryExemptsalesAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryExemptsalesAmt.Text=="0.00")
                {
                    EntryExemptsalesAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryExemptsalesAmt.Text) && EntryExemptsalesAmt.Text.Contains(","))
                {
                    EntryExemptsalesAmt.Text = EntryExemptsalesAmt.Text.Replace(",", "");
                    EntryExemptsalesAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExemptsalesAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryStdpurchaseAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryStdpurchaseAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryZVatAmountWithSAR_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryImportspaidAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryImportsaccAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryImportsaccAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryZeropurchaseAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryZeropurchaseAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryExemptpurchaseAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryPreperiodcorr_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryExemptpurchaseAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckMandetoryFields();
        }
        private void EntryStdpurchaseAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryStdpurchaseAmt.Text=="0.00")
                {
                    EntryStdpurchaseAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) && EntryStdpurchaseAmt.Text.Contains(","))
                {
                    EntryStdpurchaseAmt.Text = EntryStdpurchaseAmt.Text.Replace(",", "");
                    EntryStdpurchaseAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryStdpurchaseAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryStdpurchaseAdj.Text=="0.00")
                {
                    EntryStdpurchaseAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) && EntryStdpurchaseAdj.Text.Contains(","))
                {
                    EntryStdpurchaseAdj.Text = EntryStdpurchaseAdj.Text.Replace(",", "");
                    EntryStdpurchaseAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZVatAmountWithSARFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryZVatAmountWithSAR.Text=="0.00")
                {
                    EntryZVatAmountWithSAR.Text = String.Empty;
                }

                if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) && EntryZVatAmountWithSAR.Text.Contains(","))
                {
                    EntryZVatAmountWithSAR.Text = EntryZVatAmountWithSAR.Text.Replace(",", "");
                    EntryZVatAmountWithSAR.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryImportspaidAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryImportspaidAdj.Text=="0.00")
                {
                    EntryImportspaidAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryImportspaidAdj.Text) && EntryImportspaidAdj.Text.Contains(","))
                {
                    EntryImportspaidAdj.Text = EntryImportspaidAdj.Text.Replace(",", "");
                    EntryImportspaidAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryImportsaccAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryImportsaccAmt.Text=="0.00")
                {
                    EntryImportsaccAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryImportsaccAmt.Text) && EntryImportsaccAmt.Text.Contains(","))
                {
                    EntryImportsaccAmt.Text = EntryImportsaccAmt.Text.Replace(",", "");
                    EntryImportsaccAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryImportsaccAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryImportsaccAdj.Text=="0.00")
                {
                    EntryImportsaccAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryImportsaccAdj.Text) && EntryImportsaccAdj.Text.Contains(","))
                {
                    EntryImportsaccAdj.Text = EntryImportsaccAdj.Text.Replace(",", "");
                    EntryImportsaccAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZeropurchaseAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryZeropurchaseAmt.Text=="0.00")
                {
                    EntryZeropurchaseAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) && EntryZeropurchaseAmt.Text.Contains(","))
                {
                    EntryZeropurchaseAmt.Text = EntryZeropurchaseAmt.Text.Replace(",", "");
                    EntryZeropurchaseAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZeropurchaseAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {

                if(EntryZeropurchaseAdj.Text=="0.00")
                {
                    EntryZeropurchaseAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) && EntryZeropurchaseAdj.Text.Contains(","))
                {
                    EntryZeropurchaseAdj.Text = EntryZeropurchaseAdj.Text.Replace(",", "");
                    EntryZeropurchaseAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExemptpurchaseAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryExemptpurchaseAmt.Text=="0.00")
                {
                    EntryExemptpurchaseAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) && EntryExemptpurchaseAmt.Text.Contains(","))
                {
                    EntryExemptpurchaseAmt.Text = EntryExemptpurchaseAmt.Text.Replace(",", "");
                    EntryExemptpurchaseAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExemptpurchaseAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryExemptpurchaseAdj.Text=="0.00")
                {
                    EntryExemptpurchaseAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) && EntryExemptpurchaseAdj.Text.Contains(","))
                {
                    EntryExemptpurchaseAdj.Text = EntryExemptpurchaseAdj.Text.Replace(",", "");
                    EntryExemptpurchaseAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryPreperiodcorrFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if(EntryPreperiodcorr.Text=="0.00" || EntryPreperiodcorr.Text=="-0.00")
                {
                    EntryPreperiodcorr.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryPreperiodcorr.Text) && EntryPreperiodcorr.Text.Contains(","))
                {
                    EntryPreperiodcorr.Text = EntryPreperiodcorr.Text.Replace(",", "");
                    EntryPreperiodcorr.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void onIBANDropdownClicked(object sender, EventArgs e)
        {
            BPicker.Focus();
            // IBANDropdownPicker.Focus;
        }
        private void onIbanTypeButtonClicked(object sender, EventArgs e)
        {
            BPicker1.Focus();
        }
        private void onIdNumberButtonClicked(object sender, EventArgs e)
        {
            BPicker2.Focus();
        }
        private void SfTabView_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            int PageName = SfTabView.SelectedIndex;
            if (PageName == 0)
            {
                viewModel.InstrunctionClicked();
                if (viewModel.IsDeclarationCheckedForInstruction && !((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && (viewModel.IsAmendClicked == false)))
                {
                    viewModel.IsMainButtonEnabled = true;
                }
                else
                {
                    viewModel.IsMainButtonEnabled = false;
                }
                // BtnNextStep.IsEnabled = false;
                //if (App.ICRStatus == "E0045")
                //{
                //    viewModel.IsDeclarationCheckedForInstruction = true;
                //    viewModel.IsDeclarationCheckedForSummary = true;
                //    viewModel.IsCheckedTaxPayerDetailsInfo = true;
                //    chkDeclaration.IsChecked = true;
                //}
                //else
                //{
                //    viewModel.IsDeclarationCheckedForInstruction = false;
                //    chkDeclaration.IsChecked = false;
                //}
                //  setColor(previous, current);
                //  NewSetColor(senderObject, current);
                viewModel.IsFirstTimeGet = false;
                if (viewModel.IsGetAcknowledgementClicked == true)
                {
                    viewModel.IsMainButtonEnabled = false;
                }
            }
            else if (PageName == 1)
            {
                bool value = viewModel.IsCheckedDraftMode();
                bool Tvalue = viewModel.IsTabbedValid("02");
                if (value && Tvalue)
                {
                    if (viewModel.IsFirstTimeGet)
                    {
                        viewModel.IsDeclarationCheckedForInstruction = true;
                    }
                }
                if (viewModel.IsDeclarationCheckedForInstruction == true || (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057"))
                {
                    if (viewModel.IsDeclarationCheckedForInstruction == true)
                    {
                        viewModel.TaxpayerDetailsClicked();
                        // setColor(previous, current);
                        //  NewSetColor(senderObject, current);
                    }
                    else
                    {
                        if (viewModel.IsVisibleInstrunction == true)
                        {
                            viewModel.SelectedIndex = 0;
                        }
                        if (viewModel.IsVisibleTaxPayerDetails == true)
                        {
                            viewModel.SelectedIndex = 1;
                        }
                        if (viewModel.IsVisibleVatReturnForm == true)
                        {
                            viewModel.SelectedIndex = 2;
                        }
                        if (viewModel.IsVisibleSummary == true)
                        {
                            viewModel.SelectedIndex = 3;
                        }
                    }
                }
                else
                {
                    if (App.ICRStatus != "E0001")
                    {
                        viewModel.IsDeclarationCheckedForInstruction = false;
                        viewModel.IsMainButtonEnabled = false;
                    }
                    // BtnNextStep.IsEnabled = false;
                    //  chkClearification.IsChecked = false;
                    //  ((CollectionView)sender).SelectedItem = null;
                    if (viewModel.IsVisibleInstrunction == true)
                    {
                        viewModel.SelectedIndex = 0;
                    }
                    if (viewModel.IsVisibleTaxPayerDetails == true)
                    {
                        viewModel.SelectedIndex = 1;
                    }
                    if (viewModel.IsVisibleVatReturnForm == true)
                    {
                        viewModel.SelectedIndex = 2;
                    }
                    if (viewModel.IsVisibleSummary == true)
                    {
                        viewModel.SelectedIndex = 3;
                    }
                }
                viewModel.IsFirstTimeGet = false;
                if (viewModel.IsGetAcknowledgementClicked == true)
                {
                    viewModel.IsMainButtonEnabled = false;
                }
            }
            else if (PageName == 2)
            {
                bool value = viewModel.IsCheckedDraftMode();
                bool Tvalue = viewModel.IsTabbedValid("03");
                if (value && Tvalue)
                {
                    if (viewModel.IsFirstTimeGet)
                    {
                        viewModel.IsDeclarationCheckedForInstruction = true;
                        viewModel.IsCheckedTaxPayerDetailsInfo = true;
                    }
                }
                if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true || (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057"))
                {
                    if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true)
                    {
                        viewModel.VATReturnFormClicked();
                        //   setColor(previous, current);
                        if (viewModel.IsFirstSubmission)
                        {
                            EntryVatAmount.Focus();
                        }
                        else
                        {
                            TabVatReturn.ScrollToAsync(TabVatReturn,ScrollToPosition.Start, true);
                        }
                        //  NewSetColor(senderObject, current);
                    }
                    else
                    {
                        if (viewModel.IsVisibleInstrunction == true)
                        {
                            viewModel.SelectedIndex = 0;
                        }
                        if (viewModel.IsVisibleTaxPayerDetails == true)
                        {
                            viewModel.SelectedIndex = 1;
                        }
                        if (viewModel.IsVisibleVatReturnForm == true)
                        {
                            viewModel.SelectedIndex = 2;
                        }
                        if (viewModel.IsVisibleSummary == true)
                        {
                            viewModel.SelectedIndex = 3;
                        }
                        // ((CollectionView)sender).SelectedItem = null;
                    }
                }
                else
                {
                    if (viewModel.IsVisibleInstrunction == true)
                    {
                        viewModel.SelectedIndex = 0;
                    }
                    if (viewModel.IsVisibleTaxPayerDetails == true)
                    {
                        viewModel.SelectedIndex = 1;
                    }
                    if (viewModel.IsVisibleVatReturnForm == true)
                    {
                        viewModel.SelectedIndex = 2;
                    }
                    if (viewModel.IsVisibleSummary == true)
                    {
                        viewModel.SelectedIndex = 3;
                    }
                    // ((CollectionView)sender).SelectedItem = null;
                }
                viewModel.IsFirstTimeGet = false;
                if (viewModel.IsGetAcknowledgementClicked == true)
                {
                    viewModel.IsMainButtonEnabled = false;
                }
                //else
                //{
                //    viewModel.PageSelectedItem = viewModel.VatTabbledPageList[0];
                //}
            }
            else if (PageName == 3)
            {
                try
                {
                    if (CheckMandetoryFields())
                    {
                        try
                        {
                        }
                        catch
                        {
                        }
                        if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true || (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057"))
                        {
                            bool value = viewModel.IsCheckedDraftMode();
                            bool Tvalue = viewModel.IsTabbedValid("04");
                            if (value && Tvalue)
                            {
                                if (viewModel.IsFirstTimeGet)
                                {
                                    viewModel.IsDeclarationCheckedForInstruction = true;
                                    viewModel.IsCheckedTaxPayerDetailsInfo = true;
                                }
                            }
                            if (viewModel.IsDeclarationCheckedForInstruction == true && viewModel.IsCheckedTaxPayerDetailsInfo == true)
                            {
                                bool value1 = viewModel.IsCheckedDraftMode();
                                if (viewModel.IsMainButtonEnabled == false && (App.ICRStatus == "E0001" || value1))
                                {
                                    //if (viewModel.IsVisibleInstrunction == true)
                                    //{
                                    //    viewModel.SelectedIndex = 0;
                                    //}
                                    //if (viewModel.IsVisibleTaxPayerDetails == true)
                                    //{
                                    //    viewModel.SelectedIndex = 1;
                                    //}
                                    //if (viewModel.IsVisibleVatReturnForm == true)
                                    //{
                                    //    viewModel.SelectedIndex = 2;
                                    //}
                                    //if (viewModel.IsVisibleSummary == true)
                                    //{
                                    //    viewModel.SelectedIndex = 3;
                                    //}
                                }
                                else
                                {
                                    viewModel.IsVisibleSummary = true;
                                    viewModel.ClearPage();
                                    viewModel.SummaryClicked();
                                    // setColor(previous, current);
                                    //   NewSetColor(senderObject, current);
                                }
                                //Add because it will  not navigate in tobefilled and draft mode
                                if (App.ICRStatus == "E0001" || value1)
                                {
                                    viewModel.IsVisibleSummary = true;
                                    viewModel.ClearPage();
                                    viewModel.SummaryClicked();
                                    //  setColor(previous, current);
                                    //   NewSetColor(senderObject, current);
                                }
                                else
                                {
                                    if (viewModel.IsVisibleInstrunction == true)
                                    {
                                        viewModel.SelectedIndex = 0;
                                    }
                                    if (viewModel.IsVisibleTaxPayerDetails == true)
                                    {
                                        viewModel.SelectedIndex = 1;
                                    }
                                    if (viewModel.IsVisibleVatReturnForm == true)
                                    {
                                        viewModel.SelectedIndex = 2;
                                    }
                                    if (viewModel.IsVisibleSummary == true)
                                    {
                                        viewModel.SelectedIndex = 3;
                                    }
                                    //((CollectionView)sender).SelectedItem = null;
                                }
                            }
                            else
                            {
                                if (viewModel.IsVisibleInstrunction == true)
                                {
                                    viewModel.SelectedIndex = 0;
                                }
                                if (viewModel.IsVisibleTaxPayerDetails == true)
                                {
                                    viewModel.SelectedIndex = 1;
                                }
                                if (viewModel.IsVisibleVatReturnForm == true)
                                {
                                    viewModel.SelectedIndex = 2;
                                }
                                if (viewModel.IsVisibleSummary == true)
                                {
                                    viewModel.SelectedIndex = 3;
                                }
                                //((CollectionView)sender).SelectedItem = null;
                            }
                        }
                        else
                        {
                            if (viewModel.IsVisibleInstrunction == true)
                            {
                                viewModel.SelectedIndex = 0;
                            }
                            if (viewModel.IsVisibleTaxPayerDetails == true)
                            {
                                viewModel.SelectedIndex = 1;
                            }
                            if (viewModel.IsVisibleVatReturnForm == true)
                            {
                                viewModel.SelectedIndex = 2;
                            }
                            if (viewModel.IsVisibleSummary == true)
                            {
                                viewModel.SelectedIndex = 3;
                            }
                            viewModel.IsMainButtonEnabled = false;
                            viewModel.IsMainButtonEnabled = false;
                            chkDeclarationForSummary.IsChecked = false;
                        }
                        viewModel.IsFirstTimeGet = false;
                    }
                    else
                    {
                        if (viewModel.IsVisibleInstrunction == true)
                        {
                            viewModel.SelectedIndex = 0;
                        }
                        if (viewModel.IsVisibleTaxPayerDetails == true)
                        {
                            viewModel.SelectedIndex = 1;
                        }
                        if (viewModel.IsVisibleVatReturnForm == true)
                        {
                            viewModel.SelectedIndex = 2;
                        }
                        if (viewModel.IsVisibleSummary == true)
                        {
                            viewModel.SelectedIndex = 3;
                        }
                        // ((CollectionView)sender).SelectedItem = null;
                    }
                    if (viewModel.IsGetAcknowledgementClicked == true)
                    {
                        viewModel.IsMainButtonEnabled = false;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void SelectedIBANChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            //viewModel.TxtIDNumber = string.Empty;
            //EntryName.IsEnabled = true;
            try
            {
                Result2 selectedIBAN = (Result2)e.NewValue;
                viewModel.SelectedIBAN = selectedIBAN;
                viewModel.TxtSelectedIBAN = selectedIBAN.Iban;
            }
            catch (Exception ex)
            {
            }
        }
        private async void SelectedIBANTypeChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            //viewModel.TxtIDNumber = string.Empty;
            //EntryName.IsEnabled = true;
            try
            {
                IBANType selectedIBANType = (IBANType)e.NewValue;
                viewModel.SelectedIBANType = selectedIBANType;
                viewModel.TxtSelectedIBANType = selectedIBANType.Text;
                await viewModel.SetIBANIdNumber();
            }
            catch (Exception ex)
            {
            }
        }
        private void SelectedIBANIDNumberChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            //viewModel.TxtIDNumber = string.Empty;
            //EntryName.IsEnabled = true;
            try
            {
                IBANIDNumber selectedIBANIDNumber = (IBANIDNumber)e.NewValue;
                viewModel.SelectedIBANIDNumber = selectedIBANIDNumber;
                viewModel.TxtSelectedIBANIDNumber = selectedIBANIDNumber.Idnumber;
            }
            catch (Exception ex)
            {
            }
        }
        private void btnIban_Clicked(object sender, EventArgs e)
        {
            BPicker.IsOpen = true;
        }
        private void btnIban1_Clicked(object sender, EventArgs e)
        {
            BPicker1.IsOpen = true;
        }
        private void btnIban2_Clicked(object sender, EventArgs e)
        {
            BPicker2.IsOpen = true;
        }
        private void ChangeRegistrationTapped(object sender, EventArgs e)
        {
            viewModel._dialogService.ShowMessage(AppResources.ZZZChangeRegistationNote, AppResources.ZInstructions);
        }
        private async void Switch_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    ValidationsForVATRefund();

                   
                }
                
            }         
        }
        private async void BPicker1_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            IBANType selectedIBANType = (IBANType)e.NewValue;
            BPicker1.SelectedItem = selectedIBANType;
            viewModel.SelectedIBANType = selectedIBANType;
            viewModel.SelectedIBANTypePrev = selectedIBANType;
            viewModel.TxtSelectedIBANType = selectedIBANType.Text;
            await viewModel.SetIBANIdNumber();
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    ValidationsForVATRefund();
                }
            }
        }
        private void BPicker1_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            BPicker1.SelectedItem = viewModel.SelectedIBANTypePrev;
            viewModel.SelectedIBANType = viewModel.SelectedIBANTypePrev;
            if (viewModel.SelectedIBANTypePrev == null)
            {
                viewModel.TxtSelectedIBANType = string.Empty;
            }
        }
        private void BPicker2_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            IBANIDNumber selectedIBANIDNumber = (IBANIDNumber)e.NewValue;
            BPicker2.SelectedItem = selectedIBANIDNumber;
            viewModel.SelectedIBANIDNumber = selectedIBANIDNumber;
            viewModel.SelectedIBANIDNumberPrev = selectedIBANIDNumber;
            viewModel.TxtSelectedIBANIDNumber = selectedIBANIDNumber.Idnumber;
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    ValidationsForVATRefund();
                }
            }
        }
        private void BPicker2_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            BPicker2.SelectedItem = viewModel.SelectedIBANIDNumberPrev;
            viewModel.SelectedIBANIDNumber = viewModel.SelectedIBANIDNumberPrev;
            if (viewModel.SelectedIBANIDNumberPrev == null)
            {
                viewModel.TxtSelectedIBANIDNumber = string.Empty;
            }
        }
        private void BPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            Result2 selectedIBAN = (Result2)e.NewValue;
            BPicker.SelectedItem = selectedIBAN;
            viewModel.SelectedIBAN = selectedIBAN;
            viewModel.SelectedIBANPrev = selectedIBAN;
            viewModel.TxtSelectedIBAN = selectedIBAN.Iban;
            if (viewModel.IsVisibleSummary == true)
            {
                if (viewModel.IsVisibleDropdownForRefund == true)
                {
                    ValidationsForVATRefund();
                }
            }
        }
        private void BPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            BPicker.SelectedItem = viewModel.SelectedIBANPrev;
            viewModel.SelectedIBAN = viewModel.SelectedIBANPrev;
            if (viewModel.SelectedIBANPrev == null)
            {
                viewModel.TxtSelectedIBAN = string.Empty;
            }
        }

     

      

        private async void btnSwitch_Clicked(object sender, EventArgs e)
        {
            if (!viewModel.IsSwichButtonEnable)
            {
                var result = await this.DisplayAlert(AppResources.Information, AppResources.ZZZRefundEnableMessage, AppResources.ZZZOkayText, AppResources.ZZZCancelText);
                if (result)
                {
                    viewModel.IsSwichButtonEnable = true;
                }
                else
                {
                    viewModel.IsSwichButtonEnable = false;
                }
            }
            else
            {
                viewModel.IsSwichButtonEnable = false;
            }
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void EntryExemptsalesAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (EntryExemptsalesAdj.Text == "0.00")
                {
                    EntryExemptsalesAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryExemptsalesAdj.Text) && EntryExemptsalesAdj.Text.Contains(","))
                {
                    EntryExemptsalesAdj.Text = EntryExemptsalesAdj.Text.Replace(",", "");
                    EntryExemptsalesAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void chkRefundDeclaration_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ValidationsForVATRefund();
        }
    }
    //private void ICvalidation_Clicked(object sender, EventArgs e)
    //{
    //   //if(TabInstruction.IsVisible==true)
    //   // {
    //   //     if(chkDeclaration.IsChecked==false)
    //   //     {
    //   //         Resources["CheckBoxValidationStyle"] = App.Current.Resources["StyleCheckBoxValidatorRed"];
    //   //     }
    //   //     else
    //   //     {
    //   //         Resources["CheckBoxValidationStyle"] = App.Current.Resources["StyleCheckBoxValidatorGreen"];
    //   //     }
    //   // }
    //   //if(TabTaxPayerDetails.IsVisible==true)
    //   // {
    //   //     if (chkClearification.IsChecked == false)
    //   //     {
    //   //         Resources["CheckBoxValidationStyle"] = App.Current.Resources["StyleCheckBoxValidatorRed"];
    //   //     }
    //   //     else
    //   //     {
    //   //         Resources["CheckBoxValidationStyle"] = App.Current.Resources["StyleCheckBoxValidatorGreen"];
    //   //     }
    //   // }
    //}
}

