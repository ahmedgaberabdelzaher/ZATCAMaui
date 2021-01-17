using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ForgotPasswordPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignRecoverPasswordPageView : ContentPage
    {
        GAZTNewDesignRecoverPasswordPageViewModel viewModel;
        public GAZTNewDesignRecoverPasswordPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.GAZTNewDesignRecoverPasswordPageViewModel;
            this.BindingContext = viewModel;
            NavigationPage.SetHasBackButton(this, false);

        }

        private async  void OnLogInClicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
            //viewModel._navigationService.GoBack();
            //viewModel._navigationService.NavigateTo(App.SFLoginPageView);

            //for (int index = Navigation.NavigationStack.Count - 1 ; index > 1; index--)
            //{
            //    Page pg = Navigation.NavigationStack[index];
            //    Navigation.RemovePage(pg);
            //}
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
                //Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                //Navigation.RemovePage(pg);
        }

    }
}