using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
                viewModel.VatAttachmentsList = null;
                if (vATDeclaration != null && vATDeclaration.d != null)
                {
                    viewModel.VATDeclarationData = vATDeclaration;


                    if (viewModel.VATDeclarationData.d.ATTACHSet.results.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATDeclarationData.d.ATTACHSet.results as List<Attachment>);

                        viewModel.VatAttachmentsList = myCollection;

                        //foreach (var item in viewModel.VatAttachmentsList)
                        //{
                        //    if (App.IsArabic)
                        //    {
                        //        if (item.Erfdt != null)
                        //        {
                        //            item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                        //            item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                        //            item.Erfdt = UtilityManager.ToArabicDate(item.Erfdt);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (item.Erfdt != null)
                        //        {
                        //            item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                        //            item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        //        }
                        //    }
                        //}
                    }

                }
                viewModel.OnPageLoad();
                SetLTR();
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
                    var result = await this.DisplayAlert(AppResources.ZZDELETEFILE, AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + "?", AppResources.OkText, AppResources.ZZCancel);
                    if (result)
                    {
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

        private void Attachmentlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListView Document = sender as ListView;

            Attachment attachment = (Attachment)Document.SelectedItem;
            //attachment.DocUrl;
            if (attachment.FileExtn == "PDF" || attachment.FileExtn == "pdf")
            {
                viewModel.ShowPdf(attachment.DocUrl);
            }

            if (sender is ListView lv) lv.SelectedItem = null;
        }
    }
}