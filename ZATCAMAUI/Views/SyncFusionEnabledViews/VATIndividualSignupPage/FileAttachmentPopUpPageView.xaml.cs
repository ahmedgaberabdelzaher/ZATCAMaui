using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Newtonsoft.Json;
using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
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
    public partial class FileAttachmentPopUpPageView : PopupPage
    {
        FileAttachmentPopUpPageViewModel viewModel;
        public FileAttachmentPopUpPageView(VATRegistrationDetails vATRegistrationDetails)
        {
            InitializeComponent();
            viewModel = App.Locator.FileAttachmentPopUpPageView;
            this.BindingContext = viewModel;
            SetLTR();
            viewModel.AttachmentList = new ObservableCollection<VATAttachment>();
            viewModel.VatAttachmentsList = new ObservableCollection<Attachment>();
            onPageLoad(vATRegistrationDetails);


        }
        public FileAttachmentPopUpPageView(VATRegistrationDetails vATRegistrationDetails, WhichAttachment attachment, bool isImporter)
        {
            InitializeComponent();
            viewModel = App.Locator.FileAttachmentPopUpPageView;
            this.BindingContext = viewModel;
            On<iOS>().SetUseSafeArea(true);
            SetLTR();
            viewModel.AttachmentList = new ObservableCollection<VATAttachment>();
            viewModel.VatAttachmentsList = new ObservableCollection<Attachment>();
            viewModel.IsComeForWhichAttachment = attachment;
            viewModel.isImporter = isImporter;

            onPageLoad(vATRegistrationDetails);


        }
        public void onPageLoad(VATRegistrationDetails vATRegistrationDetails)
        {
            viewModel.IsComeFromForAttachment = VATRegistrationPageViewModel.IsComeFromForAttachment;
            if (vATRegistrationDetails != null && vATRegistrationDetails.d != null)
            {
                viewModel.VATRegistrationDetailsForAttach = vATRegistrationDetails;
                SetDocType();
                if (viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet != null && viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results != null)
                {
                    if (viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results as List<Attachment>);
                        viewModel.VatAttachmentsList = myCollection;

                        try
                        {
                            foreach (var item in viewModel.VatAttachmentsList)
                            {
                                if (item.Erfdt != null && item.Erftm != null)
                                {
                                    item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                    item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                }
                            }
                        }
                        catch (Exception)
                        {


                        }

                        viewModel.filterList();
                        viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
                    }
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send<object, ATTDETSet>(this, "AttachmentReceived", viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet);
            MessagingCenter.Unsubscribe<object, string>(this, "YesPressedToDeleteAttachment");
            MessagingCenter.Unsubscribe<object, string>(this, "NoPressedToDeleteAttachment");
            //comment because main button remains enabled
            //  viewModel.IsSwichButtonEnable = false;
            viewModel.IsLoading = false;
        }
        public void SetDocType()
        {
            if (viewModel.IsComeFromForAttachment == IsComeFromForAttachment.Import)
            {
                if (viewModel.VATRegistrationDetailsForAttach.d.ImFg == "1" && viewModel.VATRegistrationDetailsForAttach.d.ExFg == "1")
                {
                    viewModel.IsImpoterAndExporter = true;
                    viewModel.IsSwitchToggled = true;
                    viewModel.DocTypeString = "ZVTC";
                }
                else
                {
                    viewModel.IsImpoterAndExporter = false;
                    if (viewModel.VATRegistrationDetailsForAttach.d.ImFg == "1")
                    {
                        viewModel.DocTypeString = "ZVTB";
                    }
                    else if (viewModel.VATRegistrationDetailsForAttach.d.ExFg == "1")
                    {
                        viewModel.DocTypeString = "ZVTC";
                    }
                }
            }
            else if (viewModel.IsComeFromForAttachment == IsComeFromForAttachment.Export)
            {
                viewModel.DocTypeString = "ZVTC";
            }
            else if (viewModel.IsComeFromForAttachment == IsComeFromForAttachment.FinancialReprsentative)
            {
                viewModel.DocTypeString = "";
            }
            else if (viewModel.IsComeFromForAttachment == IsComeFromForAttachment.General)
            {
                viewModel.DocTypeString = "ZVTA";
            }
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void Attachmentlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            try
            {
                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception)
            {


            }
        }

        private async void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            //Test check in
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
                        await PopupNavigation.Instance.PushAsync(new ConfirmationPopUpForVatRegistration(var, "FileAttachmentPopUpPageView"));
                    }

                }
                catch (Exception)

                {


                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = false;
                    });

                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    viewModel.IsLoading = false;
                    //await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                });
            }
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
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
            catch (Exception)
            {


            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            getYesForDeleteAttachment();
            getNoForDeleteAttachment();
        }



        public void getYesForDeleteAttachment()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesPressedToDeleteAttachment", (sender, arg) =>
                {
                    DeleteAttachmentForMessagingCenterCall();
                });
            }
            catch (Exception)
            {


            }
        }

        public void getNoForDeleteAttachment()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "NoPressedToDeleteAttachment", (sender, arg) =>
                {


                });
            }
            catch (Exception)
            {


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
                        string results = WebServiceManager.GAZTDeleteVATDeclarationAttachment(attachment.Filename, attachment.Doguid);
                        PopToRootPage();
                        if (results == "X")
                        {
                            Attachment listitem = (from itm in viewModel.VatAttachmentsList
                                                   where itm.Doguid == attachment.Doguid.ToString()
                                                   select itm)
                                            .FirstOrDefault();

                            VATAttachment listitemTwo = (from itm in viewModel.AttachmentList
                                                         where itm.Doguid == attachment.Doguid.ToString()
                                                         select itm)
                                            .FirstOrDefault();

                            if (listitem != null)
                                viewModel.VatAttachmentsList.Remove(listitem);




                            if (listitemTwo != null)
                                viewModel.AttachmentList.Remove(listitemTwo);

                            viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results.Remove(listitem);

                            viewModel.AttachmentCount--;
                            viewModel.filterList();
                            viewModel.CloneAttachmentList(viewModel.VatAttachmentsListtofilter);
                        }
                        viewModel.filterList();
                        viewModel.CloneAttachmentList(viewModel.VatAttachmentsListtofilter);
                    }
                });
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception)
            {


                viewModel.IsLoading = false;
            }
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
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
        private void OnDownloadAttachmentClicked(object sender, EventArgs e)
        {

        }

        private void btnSwitch_ClickedForNewVATChange(object sender, EventArgs e)
        {
            if (!viewModel.IsSwitchToggled)
            {
                viewModel.DocTypeString = "ZVTC";
                viewModel.filterList();
                viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
                viewModel.IsSwitchToggled = true;
                viewModel.isImporter = false;

            }
            else
            {
                viewModel.DocTypeString = "ZVTB";
                viewModel.filterList();
                viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
                viewModel.IsSwitchToggled = false;
                viewModel.isImporter = true;
            }
        }
    }


}
