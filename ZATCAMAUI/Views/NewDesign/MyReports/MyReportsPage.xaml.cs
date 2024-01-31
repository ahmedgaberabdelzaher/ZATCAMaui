using ZATCAMAUI.ViewModel.NewDesignViewModel.MyReportsVM;

namespace ZATCAMAUI.Views.NewDesign.MyReports
{
    public partial class MyReportsPage : ContentPage
    {
        MyReportsViewModel viewModel;
        public MyReportsPage(string phone)
        {
            try
            {
                viewModel = App.Locator.MyReportsViewModel;
                viewModel.PhoneNumber = phone;
                BindingContext = viewModel;
                InitializeComponent();

            }
            catch (Exception)
            {

            }

        }
    }
}

