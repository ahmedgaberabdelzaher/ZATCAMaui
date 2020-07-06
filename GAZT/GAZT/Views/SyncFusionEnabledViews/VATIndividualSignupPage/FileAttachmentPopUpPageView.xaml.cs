using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileAttachmentPopUpPageView : PopupPage
    {
        FileAttachmentPopUpPageViewModel viewModel;
        public FileAttachmentPopUpPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.FileAttachmentPopUpPageView;
            this.BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetLTR();

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

        }

        private async void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            Image arrowImage = sender as Image;
            string attachment = (string)arrowImage.BindingContext;
         
            System.Diagnostics.Debug.WriteLine("File Attachment ="+attachment);

            if (attachment != null)
            {
                var result = await this.DisplayAlert("Delete File", "Do You wanna Delete this file" + " " + attachment + "?", "Ok", "Cancel");
                await DeleteAttachment(result, attachment);
            }
        }

        public async Task DeleteAttachment(bool result, string attachment)
        {
            await Task.Run(() =>
            {
                if (result)
                {
                    viewModel.FileAttachments.Remove(attachment);
                }
            });
        }
    }
}