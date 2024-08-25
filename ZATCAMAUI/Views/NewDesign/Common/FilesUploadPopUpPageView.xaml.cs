using System.Collections.ObjectModel;
using System.Globalization;
using Newtonsoft.Json;
using Mopups.Pages;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using Application = Microsoft.Maui.Controls.Application;
using ListView = Microsoft.Maui.Controls.ListView;

namespace ZATCAMAUI.Views.NewDesign.Common
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilesUploadPopUpPageView : PopupPage
    {
        private FilesUploadPopUpViewModel viewModel;
        private string dmsTypeString = string.Empty;
        private string _outletRef = string.Empty;

        public FilesUploadPopUpPageView(List<Attachment> attachments, WhichAttachment whichAttachment, string returnIdz)
        {
            InitializeComponent();
            //App.IsArabic = false;
            viewModel = App.Locator.FilesUploadPopUpView;
            this.BindingContext = viewModel;
            var attachement = new AttachmentsList();
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
            var attachement = new AttachmentsList();
            attachement.results = attachments;
            onPageLoadAsync(attachement, whichAttachment, returnIdz);

        }

        public FilesUploadPopUpPageView(List<Attachment> attachments, WhichAttachment whichAttachment, string returnIdz, string dmsType, string outletRef)
        {
            InitializeComponent();
            viewModel = App.Locator.FilesUploadPopUpView;
            this.BindingContext = viewModel;

            var attachement = new AttachmentsList();
            attachement.results = attachments;
            _outletRef = outletRef;
            onPageLoadAsync(attachement, whichAttachment, returnIdz);

        }

        public void onPageLoadAsync(AttachmentsList attachments, WhichAttachment whichAttachment, string returnIdz)
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
                viewModel.TitleOne = "";
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
            else if (whichAttachment == WhichAttachment.TINOutletDeregisterAttachment)
            {
                viewModel.TitleOne = AppResources.ZVatAttachmentSizeNotfication;
                viewModel.TitleTwo = AppResources.ZChooseonlyfilewithextension; ;
            }
            else if (whichAttachment == WhichAttachment.IBANBankAccountOne || whichAttachment == WhichAttachment.IBANBankAccountTwo)
            {
                viewModel.TitleOne = "";
                viewModel.TitleTwo = AppResources.IBANAttachmentTitle; ;
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
            if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatInstalmentBankStatements)
            {
                MessagingCenter.Send<Object, AttachmentsList>(this, "AttachmentRecvdinst", viewModel.AttachmentsList);
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatInstalmentFinance)
            {
                MessagingCenter.Send<Object, AttachmentsList>(this, "AttachmentRecvdinst", viewModel.AttachmentsList);
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatExemtionDynamicAttachment)
            {
                MessagingCenter.Send<Object, FilesUploadPopUpViewModel>(this, "ZakatAttachmentReceived", viewModel);
            }
            else
            {
                MessagingCenter.Send<Object, AttachmentsList>(this, "AttachmentReceived", viewModel.AttachmentsList);
            }

            viewModel.AttachmentList = new ObservableCollection<VATAttachment>();
            viewModel.IsLoading = false;
            viewModel.AttachmentsList = null;

            base.OnDisappearing();
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
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.VatReviewLateFiling)
            {
                viewModel.DocTypeString = "ZVRA";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachment)
            {
                viewModel.DocTypeString = "N03A";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatObjectionsWithdrawAttachmentTwo)
            {
                viewModel.DocTypeString = "N03B";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentOne)
            {
                viewModel.DocTypeString = "ZEX6";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentTwo)
            {
                viewModel.DocTypeString = "ZEX7";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentThree)
            {
                viewModel.DocTypeString = "ZEX8";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentFour)
            {
                viewModel.DocTypeString = "ZEX9";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentFive)
            {
                viewModel.DocTypeString = "ZEX0";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentSix)
            {
                viewModel.DocTypeString = "ZEXA";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatExemtionAttachmentSeven)
            {
                viewModel.DocTypeString = "ZEXD";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.TINOutletDeregisterAttachment)
            {
                viewModel.DocTypeString = "DR01";
                viewModel.OutletRef = _outletRef;
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.ZakatExemtionDynamicAttachment)
            {
                viewModel.DocTypeString = viewModel.returnIdz;
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.IBANBankAccountOne)
            {
                viewModel.DocTypeString = "ZIB1";
            }
            else if (viewModel.IsComeForWhichAttachment == WhichAttachment.IBANBankAccountTwo)
            {
                viewModel.DocTypeString = "ZIB2";
            }

        }

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

                    MemoryStream pdfStream = new MemoryStream();


                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var uri = new Uri(attachmentURL);
                    HttpResponseMessage _fileDownloadResponse = await client.GetAsync(uri);

                    var fileName = Guid.NewGuid().ToString();

                    _fileDownloadResponse.EnsureSuccessStatusCode();
                    await _fileDownloadResponse.Content.CopyToAsync(pdfStream);

                    var message = new EmailMessage
                    {
                        Subject = "Attached Form :",
                    };
                    var fn = attachment.Filename;
                    var file = Path.Combine(FileSystem.CacheDirectory, fn);
                    File.WriteAllBytes(file, pdfStream.ToArray());

                    MainThread.BeginInvokeOnMainThread(async () =>
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
            try
            {
                ListView Document = sender as ListView;
                VATAttachment attachment = (VATAttachment)Document.SelectedItem;
                //attachment.DocUrl;
                if (attachment.Filename.Contains("."))
                {
                    string Extention = attachment.Filename.Split('.')[1];
                    if (Extention.Equals("PDF") || Extention.Equals("pdf"))
                    {
                        if (attachment.DocUrl != null)
                        {
                            await MopupService.Instance.PopAsync();
                            viewModel._navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
                        }
                    }
                    else
                    {
                        await email(attachment.Doguid, attachment);
                    }
                }
                else
                {
                    await email(attachment.Doguid, attachment);
                }


                if (sender is ListView lv) lv.SelectedItem = null;
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
                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = true;
                    });
                    Image arrowImage = sender as Image;
                    VATAttachment attachment = (VATAttachment)arrowImage.BindingContext;


                    if (attachment != null)
                    {
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
                MainThread.BeginInvokeOnMainThread(async () =>
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
    }
}