using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatReturnDetailsSuccessfullPageView : ContentPage
    {
        ZakatReturnDetailsSuccessfullPageViewModel viewModel;
        public ZakatReturnDetailsSuccessfullPageView(ZakatReturnDetailsD ZakatReturnDetail)
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatReturnDetailsSuccessfullPageView;
            this.BindingContext = viewModel;
            viewModel.OnPageLoad(ZakatReturnDetail);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
            Navigation.RemovePage(pg);
        }
        private void OnReturnClicked(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }
    }
}