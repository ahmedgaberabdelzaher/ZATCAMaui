using Syncfusion.Maui.Buttons;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATgoodsOnprofit;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.VATgoodsOnprofit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class NewYesorNoPageView : ContentPage
    {
        NewYesorNoPageViewModel viewModel;

        public NewYesorNoPageView()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.NewYesorNoView;
                BindingContext = viewModel;
               
            }
            catch (Exception)
            {

            }
        }
        void YesQ1_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
        {
            try
            {
                SfRadioButton button = sender as SfRadioButton;
                if (button.Text == AppResources.ZProfitOnGoodsQOptionsYes && YesQ1.IsChecked == true)
                {
                    viewModel.QA1 = "X";
                    viewModel.basedonQ1 = true;
                    viewModel.rq1 = (Color)Application.Current.Resources["DarkGrayTextColor"];

                }
                else if (button.Text == AppResources.ZProfitOnGoodsQOptionsNo && NoQ1.IsChecked == true)
                {
                    viewModel.QA1 = "R";
                    viewModel.basedonQ1 = false;
                    viewModel.rq1 = (Color)Application.Current.Resources["DarkGrayTextColor"];
                }

            }
            catch (Exception ex)
            {

            }
        }

        void Yes_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
        {
            try
            {
                SfRadioButton button = sender as SfRadioButton;
                if (button.Text == AppResources.ZProfitOnGoodsQOptionsYes && Yes.IsChecked == true)
                {
                    viewModel.QA2 = "X";
                    viewModel.rq2 = (Color)Application.Current.Resources["DarkGrayTextColor"];
                }
                else if (button.Text == AppResources.ZProfitOnGoodsQOptionsNo && No.IsChecked == true)
                {
                    viewModel.QA2 = "R";
                    viewModel.rq2 = (Color)Application.Current.Resources["DarkGrayTextColor"];
                }
            }

            catch (Exception)
            {

            }
        }


    }
}

