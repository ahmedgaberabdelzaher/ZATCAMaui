using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttachmentPopUp : PopupPage
    {
        AttachmentPopUpViewModel viewModel;
        ZakatAttachment estimateZakatAttachment;
        public AttachmentPopUp(ZakatReturnDetailsD ZakatReturnDetail)
        {
            try
            {
                viewModel = App.Locator.AttachmentPopUp;
                InitializeComponent();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                this.BindingContext = viewModel;
                viewModel.ClearData();
                viewModel.ZakatReturnDetail = ZakatReturnDetail;
                viewModel.OnPageLoad();
                SetLTR();
            }
            catch(Exception ex)
            {

            }
          
    }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            getYesCommandToDeleteTheAttachment();

        }

        public async void getYesCommandToDeleteTheAttachment()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesCommandToDeleteTheAttachment", async (sender, arg) =>
                {
                    if(estimateZakatAttachment != null)
                    {
                        await viewModel.DeleteSelectedAttachment(estimateZakatAttachment.Filename, estimateZakatAttachment.Doguid);
                    }
                    //  await viewModel.OnReleaseOrBillsClicked();
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private async void OnCloseTapped(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.ZakatReturnAttachmentsList != null && viewModel.ZakatReturnAttachmentsList.Count > 0)
                {
                    ObservableCollection<ZakatAttachment> LocalZakatReturnAttachmentsList = new ObservableCollection<ZakatAttachment>();
                    LocalZakatReturnAttachmentsList = viewModel.ZakatReturnAttachmentsList;
                    foreach (ZakatAttachment obj in LocalZakatReturnAttachmentsList)
                    {
                        await viewModel.ClearAllAttachment(obj.Filename, obj.Doguid);
                    }
                    viewModel.ZakatReturnAttachmentsList.Clear();
                    AttachmentPopUpViewModel.SalesDetailList[viewModel.SelectedSalesTypeIndex].ChangeReason = string.Empty;
                    AttachmentPopUpViewModel.SalesDetailList[viewModel.SelectedSalesTypeIndex].estimateZakatAttachment.Clear();
                }
                viewModel.ObjectionReason = string.Empty;
                await PopupNavigation.Instance.PopAsync();

            }
            catch (Exception ex)
            {

            }
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            viewModel.ObjectionReason = string.Empty;
            PopupNavigation.Instance.PopAsync();
        }
        
        private async void OnAttachmentClicked(object sender, EventArgs e)
        {
          await  viewModel.AddAttachment();
        }
        
        private async void OnDeleteAttachmentClickedTapped(object sender, EventArgs e)
        {
            Image deleteImage = sender as Image;
             estimateZakatAttachment = (ZakatAttachment)deleteImage.BindingContext;
            if (estimateZakatAttachment != null)
            {
                await PopupNavigation.Instance.PushAsync(new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText));

                //var result = await this.DisplayAlert(AppResources.ZZDELETEFILE, AppResources.ZZDeleteAttachmentConfirmationText + " " + estimateZakatAttachment.Filename + "?", AppResources.ZZZOkayText, AppResources.ZZCancel);
                //if (result)
                //{
                //    await viewModel.DeleteSelectedAttachment(estimateZakatAttachment.Filename, estimateZakatAttachment.Doguid);
                //}
                //else
                //{
                //}
            }
        }
        public async Task email(string doguid, ZakatAttachment attachment)
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
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Share.RequestAsync(new ShareFileRequest
                        {
                            Title = Title,
                            File = new ShareFile(file)
                        });
                    });

                }
                catch (Exception ex)
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
            if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.ClearData();

        }

        private void OnObjectionReasonFocused(object sender, FocusEventArgs e)
        {
            
        }
        private async void OnObjectionReasonUnFocused(object sender, FocusEventArgs e)
        {
            //await PopupNavigation.Instance.PushAsync(new AttachmentPopUp(viewModel.ZakatReturnDetail));
           //    await PopupNavigation.Instance.PopAsync();
        }

    //protected override bool OnBackgroundClicked()
    //{
    //        //ClearAttachments();
    //        return false;
    //}

        //public async void ClearAttachments()
        //{
        //    if (viewModel.ZakatReturnAttachmentsList != null && viewModel.ZakatReturnAttachmentsList.Count > 0)
        //    {
        //        ObservableCollection<ZakatAttachment> LocalZakatReturnAttachmentsList = new ObservableCollection<ZakatAttachment>();
        //        LocalZakatReturnAttachmentsList = viewModel.ZakatReturnAttachmentsList;
        //        foreach (ZakatAttachment obj in LocalZakatReturnAttachmentsList)
        //        {
        //            await viewModel.ClearAllAttachment(obj.Filename, obj.Doguid);
        //            //if (viewModel.ZakatReturnAttachmentsList.Count == 0)
        //            //{
        //            //    
        //            //    break;
        //            //}
        //        }
        //        viewModel.ZakatReturnAttachmentsList.Clear();
        //        AttachmentPopUpViewModel.SalesDetailList[viewModel.SelectedSalesTypeIndex].ChangeReason = string.Empty;
        //        AttachmentPopUpViewModel.SalesDetailList[viewModel.SelectedSalesTypeIndex].estimateZakatAttachment.Clear();
        //    }
        //    viewModel.ObjectionReason = string.Empty;

        //}

    }
}