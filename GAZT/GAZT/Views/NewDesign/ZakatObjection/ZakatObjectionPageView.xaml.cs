using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;
using GAZT.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatObjection
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionPageView : ContentPage
    {
        private ZakatObjectionViewModel viewModel;
        public ZakatObjectionPageView()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel = App.Locator.ZakatObjectionView;
            this.BindingContext = viewModel;
            //viewModel.showInstructionsDialog();
            viewModel.ResetData();
            GetZakatObjectionsData();

        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
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

        private void IdNumberTextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OnIDNumberFocusChanged(object sender, FocusEventArgs e)
        {

        }

        private void ContactPersonTextUnFocus(object sender, FocusEventArgs e)
        {

        }
        private async void SummaryAttachments_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            var attachment = e.ItemData as Attachment;

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
                await WebServiceManager.email(attachment.Doguid, attachment);
            }



            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });



        }


        public async Task GetZakatObjectionsData()
        {
            try
            {
                await Task.Run(() =>
                {
                    //                    viewModel.IsLoading = true;

                });
                await Task.Run(async () =>
                {
                    viewModel.OnPageLoad();

                });

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<object, Attachments>(this, "AttachmentReceived");


        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();


                MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        viewModel.PopulateAttachments(arg.results);
                    }
                });

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
    }
}