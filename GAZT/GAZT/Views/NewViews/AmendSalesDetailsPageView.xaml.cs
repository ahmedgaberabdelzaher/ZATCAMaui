using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
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
    public partial class AmendSalesDetailsPageView : ContentPage
    {
        #region Variable
        AmendSalesDetailsPageViewModel viewModel;
        #endregion

        #region Property
        #endregion
     
        #region Constructor
        public AmendSalesDetailsPageView(SalesDetails SelectedSalesDetails)
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            try
            {
                viewModel = App.Locator.AmendSalesDetailsPageView;
                SelectedSalesDetails.ComingFromAmendEditMode = true;
                this.BindingContext = viewModel;
                AmendSalesDetailsPageViewModel.SelectedSalesDetails = SelectedSalesDetails;
                viewModel.ClearData();
                viewModel.isOnLoad = true;
                viewModel.OnLoad();
                SetLTR();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                SetDynamicBehaviour();
                ChangeAeroIcon();

            }
            catch (Exception ex)
            {
           
            }
        }
        #endregion

        #region Method

        private void SetDynamicBehaviour()
        {

            if (SalesType.Text.Equals(AppResources.ZZAveragenumberoflabour))
            {
                NewValue.Behaviors.Add(new ElevenDotTwoDecimalPlacesAndNoNegativeValue() { isNegativeEnable = false,Max = 14, numberOfDigitBeforDecimal = 11, numberOfDigitAfterDecimal = 2 });
            }
            else
            {
                NewValue.Behaviors.Add(new ElevenDotTwoDecimalPlacesAndNoNegativeValue() { isNegativeEnable = false,Max = 18, numberOfDigitBeforDecimal = 11, numberOfDigitAfterDecimal = 2 });
            }

        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        #endregion

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

        private async void OnDeleteAttachmentClickedTapped(object sender, EventArgs e)
        {
            Image deleteImage = sender as Image;
            ZakatAttachment estimateZakatAttachment = (ZakatAttachment)deleteImage.BindingContext;
            if(estimateZakatAttachment != null) 
            {
                var result = await this.DisplayAlert(AppResources.ZZDELETEFILE,AppResources.ZZDeleteAttachmentConfirmationText + " " + estimateZakatAttachment.Filename + "?", AppResources.OKText, AppResources.ZZCancel);
                if (result)
                {
                 await   viewModel.DeleteSelectedAttachment(estimateZakatAttachment.Filename, estimateZakatAttachment.Doguid);
                }
                else
                {

                }
            }
        }
        public async Task email(string doguid, ZakatAttachment attachment)
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

            }
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
                NewValue.TextColor = Color.Black;
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
                NewValue.TextColor = Color.Black;
            }
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
