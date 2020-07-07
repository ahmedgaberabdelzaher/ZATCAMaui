using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        public FileAttachmentPopUpPageView(VATRegistrationDetails vATRegistrationDetails)
        {
            InitializeComponent();
            viewModel = App.Locator.FileAttachmentPopUpPageView;
            this.BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetLTR();
            onPageLoad(vATRegistrationDetails);

        }

        public void onPageLoad(VATRegistrationDetails vATRegistrationDetails)
        {
            viewModel.IsComeFromForAttachment = VATRegistrationPageViewModel.IsComeFromForAttachment;
            SetDocType();
            if (vATRegistrationDetails!=null && vATRegistrationDetails.d!=null)
            {
                viewModel.VATRegistrationDetailsForAttach = vATRegistrationDetails;
                if (viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATRegistrationDetailsForAttach.d.ATTDETSet.results as List<Attachment>);
                        viewModel.VatAttachmentsList = myCollection;
                        int AttachmentCount = 0;
                        foreach (var item in viewModel.VatAttachmentsList)
                        {
                                    if (item.Erfdt != null)
                                    {
                                        item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                        item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                    }
                        }
                    viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
                }
            }
        }

        public void SetDocType()
        {
            if(viewModel.IsComeFromForAttachment==IsComeFromForAttachment.Import)
            {
                viewModel.DocTypeString = "ZVTB";
            }
            else if(viewModel.IsComeFromForAttachment == IsComeFromForAttachment.Export)
            {
                viewModel.DocTypeString = "ZVTC";
            }
            else if (viewModel.IsComeFromForAttachment == IsComeFromForAttachment.FinancialReprsentative)
            {
                viewModel.DocTypeString = "";
            }
            else if (viewModel.IsComeFromForAttachment == IsComeFromForAttachment.General)
            {
                viewModel.DocTypeString = "ZVTA";
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

        }

        //private async void OnDeleteAttachmentClicked(object sender, EventArgs e)
        //{
        //    Image arrowImage = sender as Image;
        //    string attachment = (string)arrowImage.BindingContext;
         
        //    System.Diagnostics.Debug.WriteLine("File Attachment ="+attachment);

        //    if (attachment != null)
        //    {
        //        var result = await this.DisplayAlert("Delete File", "Do You wanna Delete this file" + " " + attachment + "?", "Ok", "Cancel");
        //        await DeleteAttachment(result, attachment);
        //    }
        //}

        //public async Task DeleteAttachment(bool result, string attachment)
        //{
        //    await Task.Run(() =>
        //    {
        //        if (result)
        //        {
        //            viewModel.FileAttachments.Remove(attachment);
        //        }
        //    });
        //}

        private async void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            try
            {
                    try
                    {
                        Image arrowImage = sender as Image;
                        VATAttachment attachment = (VATAttachment)arrowImage.BindingContext;
                        //if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
                        //{
                           
                                if (attachment != null)
                                {
                                    var result = await this.DisplayAlert(AppResources.ZZDELETEFILE, AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + "?", AppResources.ZZZOkayText, AppResources.ZZCancel);
                                    DeleteAttachment(result, attachment);
                                }
                           
                        //}
                    }
                    catch (Exception ex)
                    {

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


        public async Task DeleteAttachment(bool result, VATAttachment attachment)
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
                       // int indexToReduceTheSize = viewModel.GetDeletedAttachmentIndex(attachment);
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
                            viewModel.VATDeclarationDataForAttch.d.ATTACHSet.results.Remove(listitem);
                            //if (indexToReduceTheSize != -1)
                               // viewModel.ReduceTotalAttachmentSize(indexToReduceTheSize);
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
        private void OnDownloadAttachmentClicked(object sender, EventArgs e)
        {

        }
    }
}