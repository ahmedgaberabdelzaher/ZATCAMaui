
using Newtonsoft.Json;
using Mopups.Pages;
using Mopups.Services;
using Syncfusion.Maui.Picker;
using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using Application = Microsoft.Maui.Controls.Application;
using ListView = Microsoft.Maui.Controls.ListView;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
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

                viewModel.VATRegistrationDetailsData = new VATRegistrationDetails();
                viewModel.VATRegistrationDetailsData = sendtoPopup.VATRegistrationDetailsDatatoPopup;
                viewModel.VATRegistrationOtherDetails = new VATRegistrationOtherDetails();
                viewModel.VATRegistrationOtherDetails.d = sendtoPopup.vatRegOthrDetailtoPopup.d;
                viewModel.ELGBL_DOCSet = new List<ResultsItemForElgblDocSet>();
                viewModel.ResultsItemForDOCSet = new List<ResultsItemForElgblDocSet>();
                viewModel.VATRegistrationDetailsForAttach = new VATRegistrationDetails();

                viewModel.VATRegistrationDetailsForAttach = sendtoPopup.VATRegistrationDetailsDatatoPopup;
                viewModel.ELGBL_DOCSet = viewModel.VATRegistrationOtherDetails.d.ELGBL_DOCSet;
                viewModel.ResultsItemForDOCSet = viewModel.ELGBL_DOCSet;
                onPageLoad();

                if (viewModel.ResultsItemForDOCSet != null)
                {
                    viewModel.SelectedAttachmentType = 1;
                    //AttachmentTypePicker.SelectedItem = "1";
                }
                if (viewModel.IsComeForWhichAttachment == WhichAttachment.VATAmendRegistration)
                {
                    viewModel.AttachmentHeaderTitle = AppResources.VATInstalmentsAttachmentTitle;
                    viewModel.TitleIsVisible = true;
                    viewModel.RegAttachmentTitle = false;
                }
                else
                {
                    viewModel.AttachmentHeaderTitle = AppResources.VATRMPAttachments;
                    viewModel.TitleIsVisible = false;
                    viewModel.RegAttachmentTitle = true;

                }
            }
            catch (Exception)
            {


            }

        }
        public FinancialDetailAttachmentPopupPageView(DataToPassTofinancialDetailAttachmentPopup sendtoPopup, WhichAttachment attachment)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.FinancialDetailAttachmentPopupPageView;
                this.BindingContext = viewModel;


                viewModel.VATRegistrationDetailsData = new VATRegistrationDetails();
                viewModel.VATRegistrationDetailsData = sendtoPopup.VATRegistrationDetailsDatatoPopup;
                viewModel.VATRegistrationOtherDetails = new VATRegistrationOtherDetails();
                viewModel.VATRegistrationOtherDetails = sendtoPopup.vatRegOthrDetailtoPopup;
                viewModel.ELGBL_DOCSet = new List<ResultsItemForElgblDocSet>();
                viewModel.ResultsItemForDOCSet = new List<ResultsItemForElgblDocSet>();
                viewModel.VATRegistrationDetailsForAttach = new VATRegistrationDetails();

                viewModel.VATRegistrationDetailsForAttach = sendtoPopup.VATRegistrationDetailsDatatoPopup;
                viewModel.ELGBL_DOCSet = viewModel.VATRegistrationOtherDetails.d.ELGBL_DOCSet;
                viewModel.ResultsItemForDOCSet = viewModel.ELGBL_DOCSet;
                onPageLoad();

                if (viewModel.ResultsItemForDOCSet != null)
                {
                    viewModel.SelectedAttachmentType = 1;
                }

                viewModel.IsComeForWhichAttachment = attachment;

            }
            catch (Exception)
            {


            }

        }
        public void SetPickerFont()
        {
            try
            {
                switch (DeviceInfo.Platform)
                {

                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.iOS:
                        {
                            AttachmentTypePicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            AttachmentTypePicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            AttachmentTypePicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            AttachmentTypePicker.TextStyle.FontFamily = "Somar-SemiBold";

                        }
                        break;
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.Android:
                        {
                            AttachmentTypePicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            AttachmentTypePicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            AttachmentTypePicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            AttachmentTypePicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        }
                        break;
                }
            }
            catch (Exception)
            {


            }

        }
        public void onPageLoad()
        {
            try
            {
                if (viewModel.VATRegistrationDetailsForAttach != null && viewModel.VATRegistrationDetailsForAttach.d != null)
                {

                    if (viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet as List<Attachment>);
                        viewModel.VatAttachmentsList = myCollection;

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
            catch (Exception)
            {

            }

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            try
            {
                MessagingCenter.Send<Object, List<Attachment>>(this, "AttachmentReceived", viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet);
                MessagingCenter.Unsubscribe<object, string>(this, "YesPressedToDeleteFinancialAttachment");
                MessagingCenter.Unsubscribe<object, string>(this, "NoPressedToDeleteFinancialAttachment");


                MessagingCenter.Send<Object, List<ResultsItemForDOCSetforsubmit>>(this, "EligibilitySetAttachmentReceived", viewModel.VATRegistrationDetailsForAttach.d.ELGBL_DOCSet);

            }
            catch (Exception)
            {
            }
        }


        private async void OnDeleteAttachmentClicked(object sender, EventArgs e)
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
                    await MopupService.Instance.PushAsync(new ConfirmationPopUpForVatRegistration(var, "FinancialDetailAttachmentPopupPageView"));
                }
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
            }
        }
        public async Task DeleteAttachment(bool result, VATAttachment attachment)
        {
            try
            {
                viewModel.IsLoading = true;
                if (result)
                {
                    // int indexToReduceTheSize = viewModel.GetDeletedAttachmentIndex(attachment);
                    string results = await WebServiceManager.GAZTDeleteVATDeclarationAttachment(attachment.Filename, attachment.Doguid);
                   await PopToRootPage();
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
                        viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.Remove(listitem);
                        viewModel.VatAttachmentsList.Clear();
                        viewModel.filterList();
                        viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
                        ResultsItemForDOCSetforsubmit _eligibledocset = new
                              ResultsItemForDOCSetforsubmit();
                        _eligibledocset = viewModel.VATRegistrationDetailsForAttach.d.ELGBL_DOCSet.Where(X => X.DmsTp == listitem.Dotyp).FirstOrDefault();
                        viewModel.VATRegistrationDetailsForAttach.d.ELGBL_DOCSet.Remove(_eligibledocset);

                    }
                }
                viewModel.IsLoading = false;
            }
            catch (Exception)
            {


            }
        }
        private void OnDownloadAttachmentClicked(object sender, EventArgs e)
        {
            try
            {
                Image arrowImage = sender as Image;
                VATAttachment attachment = (VATAttachment)arrowImage.BindingContext;

                if (attachment != null)
                {
                    if (false == string.IsNullOrEmpty(attachment.DocUrl))
                        viewModel._navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
                }

            }
            catch (Exception)
            {


            }
        }
        private void DDlIDTypee_OkButtonClicked(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                //TODO
                ResultsItemForElgblDocSet selectedtyp = viewModel.ResultsItemForDOCSet[e.NewValue];

                //AttachmentTypePicker.SelectedItem = selectedtyp;
                //viewModel.SelectedResultsItemForDOCSet = null;
                viewModel.SelectedResultsItemForDOCSet = selectedtyp;
                viewModel.AttachmentTypeTxt = selectedtyp.Txt50;
                viewModel.DocTypeString = selectedtyp.DmsTp;
                viewModel.VatAttachmentsList.Clear();
                viewModel.filterList();
                viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);

            }
            catch (Exception)
            {


            }

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            getYesForDeleteAttachment();

            if (viewModel.IsComeForWhichAttachment == WhichAttachment.VATAmendRegistration)
            {
                viewModel.AttachmentHeaderTitle = AppResources.VATInstalmentsAttachmentTitle;
                viewModel.TitleIsVisible = true;
                viewModel.RegAttachmentTitle = false;
            }
            else
            {
                viewModel.AttachmentHeaderTitle = AppResources.VATRMPAttachments;
                viewModel.TitleIsVisible = false;
                viewModel.RegAttachmentTitle = true;

            }
        }


        public void getYesForDeleteAttachment()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesPressedToDeleteFinancialAttachment", async (sender, arg) =>
                {
                  await  DeleteAttachmentForMessagingCenterCall();
                });
            }
            catch (Exception)
            {


            }
        }

        public async Task DeleteAttachmentForMessagingCenterCall()
        {
            try
            {
                viewModel.IsLoading = true;
                if (viewModel.VATAttachmentObj != null)
                {
                    VATAttachment attachment = viewModel.VATAttachmentObj;

                    if (attachment != null)
                    {
                        await DeleteAttachment(true, attachment);
                    }
                }
                viewModel.IsLoading = false;
            }
            catch (Exception)
            {


            }
        }

      

        private void btn1_Clicked(object sender, EventArgs e)
        {
            try
            {
                AttachmentTypePicker.IsOpen = true;
            }
            catch (Exception)
            {

            }

        }


        private void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            ((ListView)sender).SelectedItem = null;
        }
        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }

        }

        private void Close_Tapped(object sender, EventArgs e)
        {
            MopupService.Instance.PopAsync();
        }


    }
}