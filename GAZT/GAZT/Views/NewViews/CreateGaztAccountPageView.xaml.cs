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
    public partial class CreateGaztAccountPageView : ContentPage
    {
        CreateGaztAccountPageViewModel viewModel;
        public CreateGaztAccountPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.CreateGaztAccountPageView;
            this.BindingContext = viewModel;
        }
    }
}