using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AttachmentPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ICRListPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;
using Application = Microsoft.Maui.Controls.Application;
using ListView = Microsoft.Maui.Controls.ListView;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.AttachmentPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttachmentPageView : ContentPage
    {
        #region Variable
        AttachmentPageViewModel viewModel;
        VATDeclaration vatDec;

        string downloadFilePath;
        #endregion

        #region Constructor
        public AttachmentPageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();
            double ht = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceHeight();
            ht = ht * 45 / 100;
            AttachmentList.HeightRequest = ht;
            list.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is ListView lv) lv.SelectedItem = null;
            };
            try
            {

                viewModel = App.Locator.AttachmentPageView;
                BindingContext = viewModel;

                viewModel.VatAttachmentsList = null;
                viewModel.ClearData();
                if (vATDeclaration.data.ATTACHSet != null && vATDeclaration.data.ATTACHSet != null && vATDeclaration.data.ATTACHSet.Count > 0)
                    viewModel.NumberOfAttachmentComingFromServer = ICRListPageViewModel.numberOfAttachmentComingFromServer;// vATDeclaration.data.ATTACHSet.results.Count;
                viewModel.TotalAttachmentSize = AttachmentPageViewModel.AttachmentUploadedSize;
                viewModel.IsAmendClickedOnVAT = VATReturnsPageViewModelEX.IsAmend;
                if (vATDeclaration != null && vATDeclaration.data != null)
                {
                    viewModel.VATDeclarationDataForAttch = vATDeclaration;
                    if (viewModel.VATDeclarationDataForAttch.data.ATTACHSet.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATDeclarationDataForAttch.data.ATTACHSet as List<Attachment>);
                        viewModel.VatAttachmentsList = myCollection;
                        int AttachmentCount = 0;
                        foreach (var item in viewModel.VatAttachmentsList)
                        {
                            if (!App.ICRStatus.Equals("E0001"))
                            {
                                if (AttachmentCount < ICRListPageViewModel.numberOfAttachmentComingFromServer)
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
        #endregion
        #region Method
        private async void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            try
            {
                if (App.ICRStatus.Equals("E0045") && viewModel.IsAmendClickedOnVAT == false)
                {
                }
                else if (App.ICRStatus.Equals("E0045") && viewModel.IsAmendClickedOnVAT == true)
                {
                    try
                    {
                        Image arrowImage = sender as Image;
                        VATAttachment attachment = (VATAttachment)arrowImage.BindingContext;
                        if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
                        {
                            int indexToReduceTheSize = viewModel.GetDeletedAttachmentIndex(attachment);
                            if (indexToReduceTheSize > viewModel.NumberOfAttachmentComingFromServer - 1)
                            {
                                if (attachment != null)
                                {
                                    var result = await this.DisplayAlert(AppResources.ZZDELETEFILE, AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + "?", AppResources.ZZZOkayText, AppResources.ZZCancel);
                                    DeleteAttachment(result, attachment);
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
                        VATAttachment attachment = (VATAttachment)arrowImage.BindingContext;

                        if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
                        {
                            if (attachment != null)
                            {
                                var result = await this.DisplayAlert(AppResources.ZZDELETEFILE, AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + "?", AppResources.ZZZOkayText, AppResources.ZZCancel);
                                DeleteAttachment(result, attachment);
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
                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
        public async Task DeleteAttachment(bool result, VATAttachment attachment)
        {
            try
            {
                viewModel.IsLoading = true;
                await Task.Run(() =>
                {
                    if (result)
                    {
                        int indexToReduceTheSize = viewModel.GetDeletedAttachmentIndex(attachment);
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
                            viewModel.VATDeclarationDataForAttch.data.ATTACHSet.Remove(listitem);
                            if (indexToReduceTheSize != -1)
                                viewModel.ReduceTotalAttachmentSize(indexToReduceTheSize);
                        }
                    }
                });
                viewModel.IsLoading = false;
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
        #endregion
        private async void OnDownloadAttachmentClicked(object sender, EventArgs e)
        {
            try
            {
                Image arrowImage = sender as Image;
                VATAttachment attachment = (VATAttachment)arrowImage.BindingContext;

                viewModel.VATDeclarationDataForAttch = vatDec;

                string retGuid = attachment.RetGuid;
                string fbNum = viewModel.VATDeclarationDataForAttch.data.Fbnum;

                viewModel.IsLoading = true;
                Models.AttachmentDocumentModel attachmentDocumentModel = await WebServiceManager.GAZTGetAllAttachments(retGuid, fbNum);

                foreach (Models.AttachmentResult tempAttachmentDocumentModel in attachmentDocumentModel.D)
                {

                    if (attachment.Filename == tempAttachmentDocumentModel.Filename)
                    {
                        var platform = DeviceInfo.Platform;
                        if (DeviceInfo.Platform == DevicePlatform.iOS)
                        {
                            downloadFilePath = WriteFileToPath(tempAttachmentDocumentModel.Filename, tempAttachmentDocumentModel.Content);

                            viewModel.IsLoading = false;
                            var downloadDirectoryFilePath = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentToDownloadsPath(tempAttachmentDocumentModel.Filename, downloadFilePath);


                        }
                        else
                        {
                            //Activity Indicator while downloading
                            //Once download is completed you have to tell the user through an alert that download is completed and check in download folder.
                            if (tempAttachmentDocumentModel.Filename.Contains(""))
                            {
                                var downloadDirectoryFilePath = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentToDownloadsPath(tempAttachmentDocumentModel.Filename, tempAttachmentDocumentModel.Content);
                            }
                            else
                            {
                                throw new GAZTNetworkConnectivityIssueException();
                            }

                            viewModel.IsLoading = false;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await viewModel._dialogService.ShowMessageBox(AppResources.ZZDownloadAttachmentMessg, AppResources.Information);
                            });
                        }
                    }
                }

            }
            catch (Exception)
            {

            }
        }
        private async Task DownloadAndSaveFile(string pathToFile, string fileContents)
        {
            File.WriteAllBytes(pathToFile, Convert.FromBase64String(fileContents));
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
            string getFilePath = PathToFolder(fileName, "GAZTFiles");
            File.WriteAllBytes(getFilePath, Convert.FromBase64String(base64Data));

            return getFilePath;
        }
        private async void Attachmentlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                ListView Document = sender as ListView;
                VATAttachment attachment = (VATAttachment)Document.SelectedItem;
                //attachment.DocUrl;
                if (attachment.Filename.Contains(".")) ;
                string Extention = attachment.Filename.Split('.')[1];
                if (Extention.Equals("PDF") || Extention.Equals("pdf"))
                {
                    if (attachment.DocUrl != null)
                    {
                        viewModel._navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
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
        public async Task email(string doguid, VATAttachment attachment)
        {
            viewModel.IsLoading = true;
            await Task.Run( () =>
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
            viewModel.IsLoading = false;
        }
    }
}
