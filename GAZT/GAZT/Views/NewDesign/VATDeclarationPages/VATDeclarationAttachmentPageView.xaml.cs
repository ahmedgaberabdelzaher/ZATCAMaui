using System;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    public partial class VATDeclarationAttachmentPageView : PopupPage
    {
        VATDeclarationAttachmentPageViewModel viewModel;
        public VATDeclarationAttachmentPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATDeclarationAttachmentPageView;
            this.BindingContext = viewModel;
        }

        private async void OnAttachmentClicked(object sender, EventArgs e)
        {
          //  await viewModel.AddAttachment();
        }

        private async void OnDeleteAttachmentClickedTapped(object sender, EventArgs e)
        {
            //Image deleteImage = sender as Image;
            //estimateZakatAttachment = (ZakatAttachment)deleteImage.BindingContext;
            //if (estimateZakatAttachment != null)
            //{
            //    await PopupNavigation.Instance.PushAsync(new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText));

                //var result = await this.DisplayAlert(AppResources.ZZDELETEFILE, AppResources.ZZDeleteAttachmentConfirmationText + " " + estimateZakatAttachment.Filename + "?", AppResources.ZZZOkayText, AppResources.ZZCancel);
                //if (result)
                //{
                //    await viewModel.DeleteSelectedAttachment(estimateZakatAttachment.Filename, estimateZakatAttachment.Doguid);
                //}
                //else
                //{
                //}
            //}
        }
        private void OnCloseTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private async void Attachmentlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //Xamarin.Forms.ListView Document = sender as Xamarin.Forms.ListView;
            //ZakatAttachment attachment = (ZakatAttachment)Document.SelectedItem;
            ////attachment.DocUrl;
            //if (attachment.Filename.Contains(".")) ;
            //string Extention = attachment.Filename.Split('.')[1];
            //if (Extention.Equals("PDF") || Extention.Equals("pdf"))
            //{
            //    if (attachment.DocUrl != null)
            //    {
            //        viewModel._navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
            //    }
            //}
            //else
            //{
            //    await email(attachment.Doguid, attachment);
            //}
            //if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
        }
    }
}
