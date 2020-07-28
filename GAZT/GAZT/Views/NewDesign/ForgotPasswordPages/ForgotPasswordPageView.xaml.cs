using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ForgotPasswordPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPageView : ContentPage
    {
        ForgotPasswordPageViewModel viewModel;
        public ForgotPasswordPageView()
        {

            InitializeComponent();
            viewModel = App.Locator.ForgotPasswordPageView;
            this.BindingContext = viewModel;
            viewModel.StartPage = 1;
        }
    }
}