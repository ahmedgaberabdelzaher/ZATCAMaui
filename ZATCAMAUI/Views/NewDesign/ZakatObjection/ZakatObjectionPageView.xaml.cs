using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.ZakatObjection
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionPageView : ContentPage
    {
        private ZakatObjectionViewModel viewModel;
        public ZakatObjectionPageView()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "");

            ChangeAeroIcon();
            SetLTR();
            On<iOS>().SetUseSafeArea(true);
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
            viewModel = App.Locator.ZakatObjectionView;
            BindingContext = viewModel;
            //viewModel.showInstructionsDialog();
            viewModel.ResetData();
            _ = GetZakatObjectionsData();

        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
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
        private async void SummaryAttachments_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                var attachment = e.DataItem as Attachment;

                //if (attachment.Filename.Contains(".")) ;
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
                    await GetVATReviewWebServiceManager.email(attachment.Doguid, attachment);
                }



                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception)
            {


            }


        }

        private void SearchItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            try
            {
                if (viewModel.InputData.Length > 0)
                {
                    var itemsSource = viewModel.ReturnBills.Where(w => w.ReferenceNum.ToString().Contains(viewModel.InputData)).ToList();

                    ObjectionsList.ItemsSource = itemsSource;



                }
                else
                {
                    if (viewModel.ReturnBills != null)
                    {
                        ObjectionsList.ItemsSource = viewModel.ReturnBills;
                    }


                }

            }
            catch (Exception)
            {


            }

        }



        public async Task GetZakatObjectionsData()
        {
            try
            {
                await Task.Run(() =>
                {
                    //                    viewModel.IsLoading = true;

                });
                await Task.Run(() =>
                {
                    viewModel.OnPageLoad();

                });

            }
            catch (Exception)
            {


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

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                Padding = safeInsets;

                MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        viewModel.PopulateAttachments(arg.results);
                    }
                });

            }
            catch (Exception)
            {


            }
        }
    }
}