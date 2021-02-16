using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using GAZT.Helper;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilesUploadPopUpPageView : PopupPage
    {
        private FilesUploadPopUpViewModel viewModel;
        private string dmsTypeString = string.Empty;

        public FilesUploadPopUpPageView(List<Attachment> attachments, WhichAttachment whichAttachment, string returnIdz)
        {
            InitializeComponent();
            //App.IsArabic = false;
            viewModel = App.Locator.FilesUploadPopUpView;
            this.BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetLTR();
            var attachement = new Attachments();
            attachement.results = attachments;
            onPageLoadAsync(attachement, whichAttachment, returnIdz);
        }

        public FilesUploadPopUpPageView(List<Attachment> attachments, WhichAttachment whichAttachment, string returnIdz, string dmsType)
       {
            InitializeComponent();
            //App.IsArabic = false;
            viewModel = App.Locator.FilesUploadPopUpView;
            this.BindingContext = viewModel;
            dmsTypeString = dmsType;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetLTR();
            var attachement = new Attachments();
            attachement.results = attachments;
            onPageLoadAsync(attachement, whichAttachment, returnIdz);

        }

        public void onPageLoadAsync(Attachments attachments, WhichAttachment whichAttachment, string returnIdz)
        {

            if (whichAttachment == WhichAttachment.VATInstalment)
            {
                viewModel.VatAttachmentCount = attachments.results.Count;
            }
            else if (whichAttachment == WhichAttachment.ZakatInstalmentBankStatements || whichAttachment == WhichAttachment.ZakatInstalmentFinance)
            {
                viewModel.VatAttachmentCount = attachments.results.Count;
            }
            else if (whichAttachment == WhichAttachment.OldZakatInstalmentBankStatements || whichAttachment == WhichAttachment.OldZakatInstalmentFinance)
            {
                viewModel.VatAttachmentCount = attachments.results.Count;
            }

            if (whichAttachment == WhichAttachment.ZakatInstalmentBankStatements || whichAttachment == WhichAttachment.ZakatInstalmentFinance)

            {
                viewModel.TitleOne = "";
                viewModel.TitleTwo = AppResources.ZakatAttachmentTitle;
            }
            else if (whichAttachment == WhichAttachment.OldZakatInstalmentBankStatements || whichAttachment == WhichAttachment.OldZakatInstalmentFinance)

            {
                viewModel.TitleOne = "";
                viewModel.TitleTwo = AppResources.OldZakatAttachmentTitle;
            }
            else if (whichAttachment == WhichAttachment.ContractReleaseCopy || whichAttachment == WhichAttachment.ContractReleaseInvoice)
            {
                viewModel.TitleOne = AppResources.ZContractReleaseAttachmentTitle;
                viewModel.TitleTwo = AppResources.ZContractReleaseChooseonlyfilewithextension;
            }
            else if (whichAttachment == WhichAttachment.VATInstalment)
            {
                viewModel.TitleOne = "";
                viewModel.TitleTwo = AppResources.VATInstalmentsAttachmentTitle;
            }
            else if (whichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachment || whichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachmentTwo)
            {
                viewModel.TitleOne = "";
                viewModel.TitleTwo = AppResources.ZOAttachmentsTitleTwo;
            }
            else if (whichAttachment == WhichAttachment.TINDeregistration)
            {
                viewModel.VatAttachmentCount = attachments.results.Count;

                viewModel.TitleOne = "";
                viewModel.TitleTwo = AppResources.TINDeregAttachmentsTitleTwo;
            }
            else
            {
                viewModel.TitleOne = AppResources.ZFilesizeshouldnotbemorethan5MB;
                viewModel.TitleTwo = AppResources.ZChooseonlyfilewithextension;
            }

            viewModel.VatAttachmentsList = null;
            viewModel.IsComeForWhichAttachment = whichAttachment;
            viewModel.returnIdz = returnIdz;
            if (attachments != null)
            {
                viewModel.AttachmentsList = attachments;
                SetDocType();
                if (viewModel.AttachmentsList != null && viewModel.AttachmentsList.results != null)
                {
                    if (viewModel.AttachmentsList.results.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.AttachmentsList.results);
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
            MessagingCenter.Send<Object, Attachments>(this, "AttachmentReceived", viewModel.AttachmentsList);
            viewModel.AttachmentList = new ObservableCollection<VATAttachment>();
            //comment because main button remains enabled
            //  viewModel.IsSwichButtonEnable = false;
            viewModel.IsLoading = false;
            viewModel.AttachmentsList = null;
        }
        public void SetDocType()
        {
            if (viewModel.IsComeForWhichAttachment == WhichAttachment.VATInstalment)
            {
                viewModel.DocTypeString = "ZVTA";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ContractReleaseCopy)
            {
                viewModel.DocTypeString = "N11A";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ContractReleaseInvoice)
            {
                viewModel.DocTypeString = "N11B";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ChangeFillingPeriod2Years)
            {
                viewModel.DocTypeString = "ZTPA";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ChangeFillingPeriod12Months)
            {
                viewModel.DocTypeString = "ZTPB";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ChangeFillingPeriodOtherDoc)
            {
                viewModel.DocTypeString = "ZTPC";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.VATDeregistration)
            {
                viewModel.DocTypeString = dmsTypeString;
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatInstalmentBankStatements)
            {
                viewModel.DocTypeString = "ZIP2";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatInstalmentFinance)
            {
                viewModel.DocTypeString = "ZIP3";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.OldZakatInstalmentBankStatements)
            {
                viewModel.DocTypeString = "IPR1";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.OldZakatInstalmentFinance)
            {
                viewModel.DocTypeString = "IPR2";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.TINDeregistration)
            {
                viewModel.DocTypeString = dmsTypeString;
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.VatReviewAttachments)
            {
                viewModel.DocTypeString = "RAGA";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.VatReviewBankGuranteeAttach)
            {
                viewModel.DocTypeString = "RVBT";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachment)
            {
                viewModel.DocTypeString = "N03A";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachmentTwo)
            {
                viewModel.DocTypeString = "N03B";
            }

        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        //private void Attachmentlist_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        //}



       
        public async Task email(string doguid, VATAttachment attachment)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {


                    string attachmentURL = attachment.DocUrl;// "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/corr_dataSet(Cokey='" + doguid + "',Cotyp='VTA0')/$value?saml2=disabled";

                    System.IO.MemoryStream pdfStream = new MemoryStream();


                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var uri = new Uri(attachmentURL);
                    HttpResponseMessage _fileDownloadResponse = await client.GetAsync(uri);

                    var fileName = Guid.NewGuid().ToString();

                    _fileDownloadResponse.EnsureSuccessStatusCode();
                    await _fileDownloadResponse.Content.CopyToAsync(pdfStream);



                    //byte[] PdfBytes;
                    //HttpWebRequest myReq = (System.Net.HttpWebRequest)WebRequest.Create(attachmentURL);
                    //WebResponse myResp = myReq.GetResponse();

                    //using (Stream streams = myResp.GetResponseStream())
                    //using (MemoryStream Ms = new MemoryStream())
                    //{
                    //    int count = 0;
                    //    do
                    //    {
                    //        byte[] buf = new byte[1024];
                    //        count = streams.Read(buf, 0, 1024);
                    //        Ms.Write(buf, 0, count);
                    //    } while (streams.CanRead && count > 0);
                    //    PdfBytes = Ms.ToArray();
                    //}
                    var message = new EmailMessage
                    {
                        Subject = "Attached Form :",
                    };
                    var fn = attachment.Filename;
                    var file = Path.Combine(FileSystem.CacheDirectory, fn);
                    File.WriteAllBytes(file, pdfStream.ToArray());

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Share.RequestAsync(new ShareFileRequest
                        {
                            Title = "",
                            File = new ShareFile(file)
                        });
                    });



                }
                catch (Exception)
                {
                }
            });
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }

        private async void Attachmentlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            Xamarin.Forms.ListView Document = sender as Xamarin.Forms.ListView;
            VATAttachment attachment = (VATAttachment)Document.SelectedItem;
            //attachment.DocUrl;
            if (attachment.Filename.Contains(".")) {

                string Extention = attachment.Filename.Split('.')[1];
                if (Extention.Equals("PDF") || Extention.Equals("pdf"))
                {
                    if (attachment.DocUrl != null)
                    {
                        await PopupNavigation.Instance.PopAsync();
                        viewModel._navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
                    }
                }
                else
                {
                    await email(attachment.Doguid, attachment);
                }
            }
            else {
                await email(attachment.Doguid, attachment);
            }

           
            if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
        }

        private async void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = true;
                    });
                    Image arrowImage = sender as Image;
                    VATAttachment attachment = (VATAttachment)arrowImage.BindingContext;


                    //if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
                    //{

                   

                    if (attachment != null)
                    {//ZZNotification
                        var confirmation = AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + AppResources.questionmark;

                        var result = await this.DisplayAlert(AppResources.ZZNotification, confirmation, AppResources.ZZZOkayText, AppResources.ZZCancel);

                        await viewModel.DeleteAttachment(result, attachment);
                    }
                    //}
                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = false;
                    });
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    viewModel.IsLoading = false;
                    await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Xamarin.Forms.Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
        private void OnDownloadAttachmentClicked(object sender, EventArgs e)
        {

        }
    }
}