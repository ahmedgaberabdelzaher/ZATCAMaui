using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
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
                viewModel.VATRegistrationOtherDetails = sendtoPopup.vatRegOthrDetailtoPopup;
                viewModel.ELGBL_DOCSet = new ELGBL_DOCSet();
                viewModel.ResultsItemForDOCSet = new List<ResultsItemForElgblDocSet>();
                viewModel.VATRegistrationDetailsForAttach = new VATRegistrationDetails();

                viewModel.VATRegistrationDetailsForAttach = sendtoPopup.VATRegistrationDetailsDatatoPopup;
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
                catch (Exception)
                {


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
                viewModel.ELGBL_DOCSet = new ELGBL_DOCSet();
                viewModel.ResultsItemForDOCSet = new List<ResultsItemForElgblDocSet>();
                viewModel.VATRegistrationDetailsForAttach = new VATRegistrationDetails();

                viewModel.VATRegistrationDetailsForAttach = sendtoPopup.VATRegistrationDetailsDatatoPopup;
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
                catch (Exception)
                {


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
                switch (Device.RuntimePlatform)
                {

                    case Device.iOS:
                        {
                            AttachmentTypePicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            AttachmentTypePicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            AttachmentTypePicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            AttachmentTypePicker.TextStyle.FontFamily = "Somar-SemiBold";

                        }
                        break;
                    case Device.Android:
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
                //viewModel.IsComeFromForAttachment = VATRegistrationPageViewModel.IsComeFromForAttachment;
                if (viewModel.VATRegistrationDetailsForAttach != null && viewModel.VATRegistrationDetailsForAttach.d != null)
                {

                    // viewModel.VATRegistrationDetailsForAttach = vATRegistrationDetails;
                    //SetDocType();
                    if (viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results as List<Attachment>);
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
                MessagingCenter.Send<object, ATTDETSet>(this, "AttachmentReceived", viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet);
                MessagingCenter.Unsubscribe<object, string>(this, "YesPressedToDeleteFinancialAttachment");
                MessagingCenter.Unsubscribe<object, string>(this, "NoPressedToDeleteFinancialAttachment");


                MessagingCenter.Send<object, ELGBL_DOCSetforsubmit>(this, "EligibilitySetAttachmentReceived", viewModel.VATRegistrationDetailsForAttach.d.ELGBL_DOCSet);

            }
            catch (Exception)
            {
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
                        await MopupService.Instance.PushAsync(new ConfirmationPopUpForVatRegistration(var, "FinancialDetailAttachmentPopupPageView"));
                    }


                }
                catch (Exception)
                {


                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
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
                                try
                                {
                                    _eligibledocset = viewModel.VATRegistrationDetailsForAttach.d.ELGBL_DOCSet.results.Where(X => X.DmsTp == listitem.Dotyp).FirstOrDefault();
                                    viewModel.VATRegistrationDetailsForAttach.d.ELGBL_DOCSet.results.Remove(_eligibledocset);
                                }
                                catch
                                {

                                }




                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                });
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
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
            getNoForDeleteAttachment();

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


        public async void getYesForDeleteAttachment()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesPressedToDeleteFinancialAttachment", async (sender, arg) =>
                {
                    DeleteAttachmentForMessagingCenterCall();
                });
            }
            catch (Exception)
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
                if (viewModel.VATAttachmentObj != null)
                {
                    VATAttachment attachment = viewModel.VATAttachmentObj;

                    if (attachment != null)
                    {
                        await DeleteAttachment(true, attachment);
                    }
                }
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception)
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
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }

        }

        private void Close_Tapped(object sender, EventArgs e)
        {
            MopupService.Instance.PopAsync();
        }


    }
}