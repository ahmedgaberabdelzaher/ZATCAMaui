using EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
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
    public partial class SignUpForEstablishmentPageView : ContentPage
    {
        SignUpForEstablishmentPageViewModel viewModel= App.Locator.SignUpForEstablishmentPageView;
        public SignUpForEstablishmentPageView()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}