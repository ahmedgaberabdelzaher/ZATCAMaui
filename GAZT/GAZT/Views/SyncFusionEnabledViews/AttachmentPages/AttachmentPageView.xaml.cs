using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttachmentPageView : ContentPage
    {
        #region Variable
        AttachmentPageViewModel viewModel;
        #endregion
        #region Property
        #endregion
        #region Constructor
        public AttachmentPageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();
            SetLTR();
            ChangeAeroIcon();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            try
            {
                viewModel = App.Locator.AttachmentPageView;
                this.BindingContext = viewModel;
                viewModel.VatAttachmentsList = null;
                if (vATDeclaration.d.ATTACHSet != null && vATDeclaration.d.ATTACHSet.results != null && vATDeclaration.d.ATTACHSet.results.Count > 0)
                    viewModel.NumberOfAttachmentComingFromServer = ICRListPageViewModel.numberOfAttachmentComingFromServer;// vATDeclaration.d.ATTACHSet.results.Count;
                viewModel.TotalAttachmentSize = AttachmentPageViewModel.AttachmentUploadedSize;
                if (vATDeclaration != null && vATDeclaration.d != null)
                {
                    viewModel.VATDeclarationDataForAttch = vATDeclaration;
                    if (viewModel.VATDeclarationDataForAttch.d.ATTACHSet.results.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATDeclarationDataForAttch.d.ATTACHSet.results as List<Attachment>);
                        viewModel.VatAttachmentsList = myCollection;
                        foreach (var item in viewModel.VatAttachmentsList)
                        {
                            if (item.Erfdt != null)
                            {
                                item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            }
                            //if (App.IsArabic)
                            //{
                            //    if (item.Erfdt != null)
                            //    {
                            //        item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            //        item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            //        item.Erfdt = UtilityManager.ToArabicDate(item.Erfdt);
                            //    }
                            //}
                            //else
                            //{
                            //    if (item.Erfdt != null)
                            //    {
                            //        item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            //        item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            //    }
                            //}
                        }
                    }
                }
                viewModel.OnPageLoad();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            }
            catch (Exception e)
            {
            }
        }
        #endregion
        #region Method
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private async void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            try
            {
                Image arrowImage = sender as Image;
                Attachment attachment = (Attachment)arrowImage.BindingContext;
                if (attachment != null)
                {
                    var result = await this.DisplayAlert(AppResources.ZZDELETEFILE, AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + "?", AppResources.OKText, AppResources.ZZCancel);
                    DeleteAttachment(result, attachment);
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
        public async Task DeleteAttachment(bool result, Attachment attachment)
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
                        int indexToReduceTheSize = viewModel.GetDeletedAttachmentIndex(attachment);
                        string results = WebServiceManager.GAZTDeleteVATDeclarationAttachment(attachment.Filename, attachment.Doguid);
                        PopToRootPage();
                        if (results == "X")
                        {
                            Attachment listitem = (from itm in viewModel.VatAttachmentsList
                                                   where itm.Doguid == attachment.Doguid.ToString()
                                                   select itm)
                                            .FirstOrDefault<Attachment>();
                            viewModel.VatAttachmentsList.Remove(listitem);
                            viewModel.VATDeclarationDataForAttch.d.ATTACHSet.results.Remove(listitem);
                            if (indexToReduceTheSize != -1)
                                viewModel.ReduceTotalAttachmentSize(indexToReduceTheSize);
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
        #endregion
        private async void OnDownloadAttachmentClicked(object sender, EventArgs e)
        {
            Image arrowImage = sender as Image;
            Attachment attachment = (Attachment)arrowImage.BindingContext;
            //attachment.DocUrl;
            string Extention = attachment.Filename.Split('.')[1];
            if (Extention == "PDF" || Extention == "pdf" || Extention.Contains("PDF") || Extention.Contains("pdf"))
            {
                //if (Device.RuntimePlatform == Device.iOS)
                //{
                //    if (attachment.DocUrl != null)
                //    {
                //        //Uri uri = new Uri(pdfUrl);
                //        //Device.OpenUri(uri);
                //        viewModel._navigationService.NavigateTo(App.PdfiOSView, "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/corr_dataSet(Cokey='" + attachment.Doguid + "',Cotyp='VTA0')/$value?saml2=disabled");
                //    }
                //    else
                //    {
                //        //pop that certificate is not available
                //        Device.BeginInvokeOnMainThread(async () =>
                //        {
                //            await viewModel._dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                //        });
                //    }
                //}
                //else
                //{
                    if (attachment.DocUrl != null)
                    {
                        viewModel._navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
                    }
                    else
                    {
                        //pop that certificate is not available
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await viewModel._dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                        });
                    }
                //}
            }
            else
            {
                await email(attachment.Doguid, attachment);
            }
            // await Navigation.PushAsync(new PdfView(attachment.DocUrl));
        }
        private async void Attachmentlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Xamarin.Forms.ListView Document = sender as Xamarin.Forms.ListView;
            Attachment attachment = (Attachment)Document.SelectedItem;
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
            if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
        }
        public async Task email(string doguid, Attachment attachment)
        {
            await Task.Run(async () =>
            {
                viewModel.IsLoading = true;
            });
            await Task.Run(async() =>
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
                    await Share.RequestAsync(new ShareFileRequest
                    {
                        Title = Title,
                        File = new ShareFile(file)
                    });
                }
                catch (Exception ex)
                {
                    viewModel.IsLoading = false;
                }
            });
            await Task.Run(async () =>
            {
                viewModel.IsLoading = false;
            });
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
    }
}
