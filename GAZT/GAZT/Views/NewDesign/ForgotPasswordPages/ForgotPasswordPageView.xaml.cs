using EGAZT.ViewModel.NewDesignViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ForgotPasswordPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignForgotPasswordPageView : ContentPage
    {
        GAZTNewDesignForgotPasswordPageViewModel viewModel;
        public GAZTNewDesignForgotPasswordPageView()
        {

            InitializeComponent();
            viewModel = App.Locator.GAZTNewDesignForgotPasswordPageView;
            this.BindingContext = viewModel;
            viewModel.ClearData();
            viewModel.StartPage = 1;
        }
    }
}