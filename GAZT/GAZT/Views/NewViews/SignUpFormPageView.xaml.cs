using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpFormPageView : ContentPage
    {
        SignUpFormPageViewModel viewModel;
        public SignUpFormPageView()
        {
            viewModel = App.Locator.SignUpFormPageView;
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}