using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System.Net;
using ZATCAMAUI.Core.Behaviors;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AmendSalesDetailsPage;
using Application = Microsoft.Maui.Controls.Application;
using ListView = Microsoft.Maui.Controls.ListView;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.AmendSalesDetailsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AmendSalesDetailsPageView : ContentPage
    {
        #region Variable
        AmendSalesDetailsPageViewModel viewModel;
        string downloadFilePath;
        public string fbNum;
        #endregion

        #region Constructor
        public AmendSalesDetailsPageView(SalesDetails SelectedSalesDetails)
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            double ht = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceHeight();
            ht = ht * 35 / 100;
            Attachmentlist.HeightRequest = ht;
            try
            {
                viewModel = App.Locator.AmendSalesDetailsPageView;
                SelectedSalesDetails.ComingFromAmendEditMode = true;
                BindingContext = viewModel;
                AmendSalesDetailsPageViewModel.SelectedSalesDetails = SelectedSalesDetails;
                viewModel.ClearData();
                viewModel.isOnLoad = true;
                viewModel.OnLoad();
                NavigationPage.SetBackButtonTitle(this, "");
                SetDynamicBehaviour();
                ChangeAeroIcon();
            }
            catch (Exception)
            {


            }
        }
        #endregion
        #region Method
        private void SetDynamicBehaviour()
        {
            if (SalesType.Text.Equals(AppResources.ZZAveragenumberoflabour))
            {
                NewValue.Behaviors.Add(new ElevenDotTwoDecimalPlacesAndNoNegativeValue() { isNegativeEnable = false, Max = 14, numberOfDigitBeforDecimal = 11, numberOfDigitAfterDecimal = 2 });
            }
            else
            {
                NewValue.Behaviors.Add(new ElevenDotTwoDecimalPlacesAndNoNegativeValue() { isNegativeEnable = false, Max = 18, numberOfDigitBeforDecimal = 11, numberOfDigitAfterDecimal = 2 });
            }
        }
        #endregion
        private async void OnDownloadAttachmentClicked(object sender, EventArgs e)
        {
            try
            {
                Image DownloadImage = sender as Image;
                ZakatAttachment attachment = (ZakatAttachment)DownloadImage.BindingContext;
                //attachment.DocUrl;

                string retGuid = attachment.RetGuid;
                string fbNum = AmendSalesDetailsPageViewModel.fbNum;
                viewModel.IsLoading = true;

                AttachmentDocumentModel attachmentDocumentModel = await WebServiceManager.GAZTGetAllAttachments(retGuid, fbNum);
                foreach (AttachmentResult tempAttachmentDocumentModel in attachmentDocumentModel.D)
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

                            if (tempAttachmentDocumentModel.Filename.Contains(""))
                            {
                                try
                                {

                                    var downloadDirectoryFilePath = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentToDownloadsPath(tempAttachmentDocumentModel.Filename, tempAttachmentDocumentModel.Content);

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
            ListView Document = sender as ListView;
            ZakatAttachment attachment = (ZakatAttachment)Document.SelectedItem;
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
        private async void OnDeleteAttachmentClickedTapped(object sender, EventArgs e)
        {
            try
            {
                Image deleteImage = sender as Image;
                ZakatAttachment estimateZakatAttachment = (ZakatAttachment)deleteImage.BindingContext;
                if (estimateZakatAttachment != null)
                {
                    var result = await this.DisplayAlert(AppResources.ZZDELETEFILE, AppResources.ZZDeleteAttachmentConfirmationText + " " + estimateZakatAttachment.Filename + "?", AppResources.ZZZOkayText, AppResources.ZZCancel);
                    if (result)
                    {
                        await viewModel.DeleteSelectedAttachment(estimateZakatAttachment.Filename, estimateZakatAttachment.Doguid);
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception)
            {


            }
        }
        public async Task email(string doguid, ZakatAttachment attachment)
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
                {}
            });
            viewModel.IsLoading = false;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue = viewModel.NewValue;
            AmendSalesDetailsPageViewModel.SelectedSalesDetails.ChangeReason = viewModel.ChangeReason;
        }
        public void OnEntryUnFocussed(object sender, EventArgs args)
        {
            if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
            {
                NewValue.Text = UtilityManager.GetCommaSeparatedAmount(NewValue.Text);
                //NewValue.TextColor = Color.Black;
                //TextColor = Color.Black,
                NewValue.TextColor = (Color)Application.Current.Resources["Primary"];
            }
            else
            {
                // UserName.TextColor = Color.Black;
            }
        }
        public void OnEntryFocussed(object sender, EventArgs args)
        {
            if (NewValue.Text.Contains(","))
            {
                NewValue.Text = NewValue.Text.Replace(",", "");
                NewValue.TextColor = (Color)Application.Current.Resources["Primary"];
            }
        }

        public void OnNewValueTextChanged(object sender, EventArgs args)
        {
            if (NewValue.Text.Contains(","))
            {
                NewValue.Text = NewValue.Text.Replace(",", "");
                NewValue.TextColor = (Color)Application.Current.Resources["Primary"];
            }
        }


        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }
    }
}
