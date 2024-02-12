using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages
{
    public partial class SessionTimeoutPageView : PopupPage
    {
        public SessionTimeoutPageView()
        {
            InitializeComponent();
        }

        async void btnLoginClicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
