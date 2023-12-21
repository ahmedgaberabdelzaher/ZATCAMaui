using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.ForgotPasswordPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignRecoverUsernamePageView : ContentPage
    {
        public GAZTNewDesignRecoverUsernamePageView()
        {
            InitializeComponent();
            BindingContext = App.Locator.GAZTNewDesignRecoverUsernameViewModel;
            NavigationPage.SetHasBackButton(this, false);
            On<iOS>().SetUseSafeArea(true);

        }



    }
}