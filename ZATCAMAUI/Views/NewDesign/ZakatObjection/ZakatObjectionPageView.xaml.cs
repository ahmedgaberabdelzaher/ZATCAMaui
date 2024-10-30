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



       


        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<object, AttachmentsList>(this, "AttachmentReceived");


        }
    }
}