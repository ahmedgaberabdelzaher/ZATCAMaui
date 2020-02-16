using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
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
            try
            {
                viewModel = App.Locator.AttachmentPageView;
                this.BindingContext = viewModel;

                if (vATDeclaration != null && vATDeclaration.d != null)
                {
                    viewModel.VATDeclarationData = vATDeclaration;


                    if (viewModel.VATDeclarationData.d.ATTACHSet.results.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATDeclarationData.d.ATTACHSet.results as List<Attachment>);

                        viewModel.VatAttachmentsList = myCollection;
                    }

                }
                viewModel.OnPageLoad();

            }
            catch (Exception e)
            {

            }
        }
        #endregion

        #region Method

        private void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            try
            {
                Image arrowImage = sender as Image;
           
            Attachment attachment = (Attachment)arrowImage.BindingContext;
            string results = WebServiceManager.GAZTDeleteVATDeclarationAttachment(attachment.Filename, attachment.Doguid);
            PopToRootPage();
            if (results == "X")
            {
                var item = (Xamarin.Forms.Image)sender;
                Attachment listitem = (from itm in viewModel.VatAttachmentsList
                                       where itm.Doguid == attachment.Doguid.ToString()
                                 select itm)
                                .FirstOrDefault<Attachment>();
                viewModel.VatAttachmentsList.Remove(listitem);

                viewModel.VATDeclarationData.d.ATTACHSet.results.Remove(listitem);

            }
            }
            catch (InternetException ex)
            {
                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
            }

        }

        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
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
            await Navigation.PushAsync(new PdfView(attachment.DocUrl));
        }
    }
}