using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilesUploadPopUpPageView : PopupPage
    {
        private FilesUploadPopUpViewModel viewModel;
        private string dmsTypeString = string.Empty;

        public FilesUploadPopUpPageView(List<Attachment> attachments,WhichAttachment whichAttachment,string returnIdz)
        {
            InitializeComponent();
            //App.IsArabic = false;
            viewModel = App.Locator.FilesUploadPopUpView;
            this.BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetLTR();
            var attachement = new Attachments();
            attachement.results = attachments;
            onPageLoad(attachement, whichAttachment,returnIdz);
        }

        public FilesUploadPopUpPageView(List<Attachment> attachments, WhichAttachment whichAttachment, string returnIdz, string dmsType)
        {
            InitializeComponent();
            //App.IsArabic = false;
            viewModel = App.Locator.FilesUploadPopUpView;
            this.BindingContext = viewModel;
            dmsTypeString = dmsType;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetLTR();
            var attachement = new Attachments();
            attachement.results = attachments;
            onPageLoad(attachement, whichAttachment, returnIdz);
        }

        public void onPageLoad(Attachments attachments,WhichAttachment whichAttachment,string returnIdz)
        {
            viewModel.IsComeForWhichAttachment = whichAttachment;
            viewModel.returnIdz = returnIdz;
            if (attachments!=null )
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
            base.OnDisappearing();
            MessagingCenter.Send<Object, Attachments>(this, "AttachmentReceived", viewModel.AttachmentsList);
            viewModel.AttachmentList = new ObservableCollection<VATAttachment>();
            //comment because main button remains enabled
            //  viewModel.IsSwichButtonEnable = false;
            viewModel.IsLoading = false;
            viewModel.AttachmentsList = null;
        }
        public void SetDocType()
        {
            if(viewModel.IsComeForWhichAttachment==WhichAttachment.VATInstalment)
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
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
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
                        //if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
                        //{
                           
                                if (attachment != null)
                    {//ZZNotification
                        var result = await this.DisplayAlert(AppResources.ZZNotification, AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + "?", AppResources.ZZZOkayText, AppResources.ZZCancel);
                                    
                                    await viewModel.DeleteAttachment(result, attachment);
                                }

                    //}
                    await Task.Run(() =>
                    {
                       viewModel.IsLoading = false;
                    });
                }
                    catch (Exception ex)
                {
                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = false;
                    });

                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Xamarin.Forms.Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
        private void OnDownloadAttachmentClicked(object sender, EventArgs e)
        {

        }
    }
}