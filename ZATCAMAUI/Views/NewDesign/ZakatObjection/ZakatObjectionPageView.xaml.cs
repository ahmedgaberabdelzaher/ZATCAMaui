
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatObjection
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionPageView : ContentPage
    {
        private ZakatObjectionViewModel viewModel;
        public ZakatObjectionPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.ZakatObjectionView;
            BindingContext = viewModel;
            viewModel.ResetData();
            _ = GetZakatObjectionsData();

        }

       
        
        private async void SummaryAttachments_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            try
            {
                viewModel.IsLoading = true;
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



                viewModel.IsLoading = false;
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
               await viewModel.OnPageLoad();
            }
            catch (Exception)
            {


            }
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<object, AttachmentsList>(this, "AttachmentReceived");


        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();


                MessagingCenter.Subscribe<object, AttachmentsList>(this, "AttachmentReceived", (sender, arg) =>
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