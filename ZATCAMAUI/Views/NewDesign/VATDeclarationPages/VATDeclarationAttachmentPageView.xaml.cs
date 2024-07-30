using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Newtonsoft.Json;
using Mopups.Pages;
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATDeclarationPagesVM;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AttachmentPage;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using Application = Microsoft.Maui.Controls.Application;
using ListView = Microsoft.Maui.Controls.ListView;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{
    public partial class VATDeclarationAttachmentPageView : PopupPage
    {
        VATDeclarationAttachmentPageViewModel viewModel;
        VATDeclaration vatDec;
        VATAttachment attachment;
        public static string AttachmentName = string.Empty;
        string downloadFilePath;
        public VATDeclarationAttachmentPageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();
            viewModel = App.Locator.VATDeclarationAttachmentPageView;
            this.BindingContext = viewModel;
            list.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is ListView lv) lv.SelectedItem = null;
            };
            try
            {


                viewModel.VatAttachmentsList = null;
                viewModel.ClearData();
                if (vATDeclaration.d.ATTACHSet != null && vATDeclaration.d.ATTACHSet.results != null && vATDeclaration.d.ATTACHSet.results.Count > 0)
                    viewModel.NumberOfAttachmentComingFromServer = GAZTNewDesignMyReturnsNewPageViewModel.numberOfAttachmentComingFromServer;// vATDeclaration.d.ATTACHSet.results.Count;
                viewModel.TotalAttachmentSize = AttachmentPageViewModel.AttachmentUploadedSize;
                viewModel.IsAmendClickedOnVAT = GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsAmend;
                if (vATDeclaration != null && vATDeclaration.d != null)
                {
                    viewModel.VATDeclarationDataForAttch = vATDeclaration;
                    if (viewModel.VATDeclarationDataForAttch.d.ATTACHSet.results.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATDeclarationDataForAttch.d.ATTACHSet.results as List<Attachment>);
                        viewModel.VatAttachmentsList = myCollection;
                        int AttachmentCount = 0;
                        foreach (var item in viewModel.VatAttachmentsList)
                        {
                            if (!App.ICRStatus.Equals("E0001"))
                            {
                                if (AttachmentCount < GAZTNewDesignMyReturnsNewPageViewModel.numberOfAttachmentComingFromServer)
                                {
                                    AttachmentCount++;
                                    if (item.Erfdt != null)
                                    {
                                        item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                        item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                    }
                                }

                            }
                        }
                        if (App.ICRStatus.Equals("E0045"))
                        {
                            viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
                        }
                        else
                        {
                            viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
                        }
                    }
                }
                viewModel.OnPageLoad();
                NavigationPage.SetBackButtonTitle(this, "");
            }
            catch (Exception)
            {
            }


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            getYesCommandToDeleteTheAttachment();

        }

        public void getYesCommandToDeleteTheAttachment()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesCommandToDeleteVATAttachment", async (sender, arg) =>
                {
                    if (attachment != null)
                    {
                        await DeleteAttachment(attachment);
                    }
                });
            }
            catch (Exception)
            {

            }
        }
        private void OnCloseTapped(object sender, EventArgs e)
        {
            try
            {
                MopupService.Instance.PopAsync();
            }
            catch (Exception)
            {

            }
        }

        private async void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            try
            {
                if (App.ICRStatus.Equals("E0045") && viewModel.IsAmendClickedOnVAT == false)
                {
                    //Show some message

                    //Image arrowImage = sender as Image;
                    //Attachment attachment = (Attachment)arrowImage.BindingContext;
                    //if (attachment != null)
                    //{
                    //    var result = await this.DisplayAlert(AppResources.ZZDELETEFILE, AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + "?", AppResources.OKText, AppResources.ZZCancel);
                    //    DeleteAttachment(result, attachment);
                    //}
                }
                else if (App.ICRStatus.Equals("E0045") && viewModel.IsAmendClickedOnVAT == true)
                {
                    try
                    {
                        Image arrowImage = sender as Image;
                        attachment = (VATAttachment)arrowImage.BindingContext;
                        if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
                        {
                            int indexToReduceTheSize = viewModel.GetDeletedAttachmentIndex(attachment);
                            if (indexToReduceTheSize > viewModel.NumberOfAttachmentComingFromServer - 1)
                            {
                                if (attachment != null)
                                {
                                    //  var result = await this.DisplayAlert(AppResources.ZZDELETEFILE, AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + "?", AppResources.ZZZOkayText, AppResources.ZZCancel);
                                    AttachmentName = attachment.Filename;
                                    await MopupService.Instance.PushAsync(new ZAKATOkCancelPopUpView("DeleteVATAttachment"));
                                    // DeleteAttachment(result, attachment);
                                }
                            }
                        }


                    }
                    catch (Exception)
                    {

                    }


                }
                else
                {
                    try
                    {
                        Image arrowImage = sender as Image;
                        attachment = (VATAttachment)arrowImage.BindingContext;

                        if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
                        {
                            if (attachment != null)
                            {
                                await MopupService.Instance.PushAsync(new ZAKATOkCancelPopUpView("DeleteVATAttachment"));
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }

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
        public async Task DeleteAttachment(VATAttachment attachment)
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await Task.Run(() =>
                {

                    int indexToReduceTheSize = viewModel.GetDeletedAttachmentIndex(attachment);
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

                        viewModel.VatAttachmentsList.Remove(listitem);
                        viewModel.AttachmentList.Remove(listitemTwo);
                        viewModel.VATDeclarationDataForAttch.d.ATTACHSet.results.Remove(listitem);
                        if (indexToReduceTheSize != -1)
                            viewModel.ReduceTotalAttachmentSize(indexToReduceTheSize);
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
        private async void OnDownloadAttachmentClicked(object sender, EventArgs e)
        {
            try
            {
                Image arrowImage = sender as Image;
                VATAttachment attachment = (VATAttachment)arrowImage.BindingContext;

                viewModel.VATDeclarationDataForAttch = vatDec;

                string retGuid = attachment.RetGuid;
                string fbNum = viewModel.VATDeclarationDataForAttch.d.Fbnum;

                viewModel.IsLoading = true;
                Models.AttachmentDocumentModel attachmentDocumentModel = await WebServiceManager.GAZTGetAllAttachments(retGuid, fbNum);

                foreach (Models.AttachmentResult tempAttachmentDocumentModel in attachmentDocumentModel.D.Results)
                {

                    if (attachment.Filename == tempAttachmentDocumentModel.Filename)
                    {
                        var platform = DeviceInfo.Platform;
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            downloadFilePath = WriteFileToPath(tempAttachmentDocumentModel.Filename, tempAttachmentDocumentModel.Content);

                            viewModel.IsLoading = false;
                            var downloadDirectoryFilePath = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetAttachmentToDownloadsPath(tempAttachmentDocumentModel.Filename, downloadFilePath);


                        }
                        else
                        {
                            //Activity Indicator while downloading
                            //Once download is completed you have to tell the user through an alert that download is completed and check in download folder.
                            if (tempAttachmentDocumentModel.Filename.Contains(""))
                            {
                                try
                                {

                                    var downloadDirectoryFilePath = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetAttachmentToDownloadsPath(tempAttachmentDocumentModel.Filename, tempAttachmentDocumentModel.Content);

                                }
                                catch (Exception)
                                {

                                }
                            }
                            else
                            {
                                throw new GAZTNetworkConnectivityIssueException();
                            }

                            viewModel.IsLoading = false;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZDownloadAttachmentMessg));
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public string PathToFolder(string fileName, string folderName)
        {
            try
            {
                string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Images", "temp");

                Directory.CreateDirectory(pathToNewFolder);
                string pathToNewFile = Path.Combine(pathToNewFolder, fileName);


                return pathToNewFile;
            }
            catch
            {
                return null;
            }
        }

        public string WriteFileToPath(string fileName, string base64Data)
        {
            try
            {
                string getFilePath = PathToFolder(fileName, "GAZTFiles");
                File.WriteAllBytes(getFilePath, Convert.FromBase64String(base64Data));

                return getFilePath;

            }
            catch (Exception)
            {


                return null;
            }
        }
        private async void Attachmentlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                ListView Document = sender as ListView;
                VATAttachment attachment = (VATAttachment)Document.SelectedItem;
                string Extention = attachment.Filename.Split('.')[1];
                if (Extention.Equals("PDF") || Extention.Equals("pdf"))
                {
                    if (attachment.DocUrl != null)
                    {
                        viewModel._navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
                        await MopupService.Instance.PopAsync();
                    }
                }
                else
                {
                    await email(attachment.Doguid, attachment);
                    await MopupService.Instance.PopAsync();
                }
                if (sender is ListView lv) lv.SelectedItem = null;
            }
            catch (Exception)
            {


            }
        }
        public async Task email(string doguid, VATAttachment attachment)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            await Task.Run(() =>
            {
                try
                {
                    string attachmentURL = attachment.DocUrl;// "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/corr_dataSet(Cokey='" + doguid + "',Cotyp='VTA0')/$value?saml2=disabled";
                    byte[] PdfBytes;
                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(attachmentURL);
                    WebResponse myResp = myReq.GetResponse();
                    using (Stream streams = myResp.GetResponseStream())
                    using (MemoryStream Ms = new MemoryStream())
                    {
                        int count = 0;
                        do
                        {
                            byte[] buf = new byte[1024];
                            count = streams.Read(buf, 0, 1024);
                            Ms.Write(buf, 0, count);
                        } while (streams.CanRead && count > 0);
                        PdfBytes = Ms.ToArray();
                    }
                    var message = new EmailMessage
                    {
                        Subject = "Attached Form :",
                    };
                    var fn = attachment.Filename;
                    var file = Path.Combine(FileSystem.CacheDirectory, fn);
                    File.WriteAllBytes(file, PdfBytes);
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Share.RequestAsync(new ShareFileRequest
                        {
                            Title = Title,
                            File = new ShareFile(file)
                        });
                    });

                }
                catch (Exception)
                {
                    viewModel.IsLoading = false;
                }
            });
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

    }
}
