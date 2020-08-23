using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentSignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentSignUPPageView : ContentPage
    {
        EstablishmentSignUPPageViewModel viewModel;
        public EstablishmentSignUPPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.EstablishmentSignUPPageView;
            BindingContext = viewModel;
        }

        private void OnEstablishmentTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SignUpForEstablishmentPageView);
        }
    }
}