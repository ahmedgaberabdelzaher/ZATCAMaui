using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ForgotPasswordPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignRecoverPasswordPageView : ContentPage
    {
        GAZTNewDesignRecoverPasswordPageViewModel viewModel;
        public GAZTNewDesignRecoverPasswordPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.GAZTNewDesignRecoverPasswordPageViewModel;
            this.BindingContext = viewModel;
        }

        private void OnLogInClicked(object sender, EventArgs e)
        {

            //viewModel._navigationService.NavigateTo(App.SFLoginPageView);
            for (int index = Navigation.NavigationStack.Count - 1 ; index > 1; index--)
            {
                Page pg = Navigation.NavigationStack[index];
                Navigation.RemovePage(pg);
            }
        }

    }
}