using Mopups.Pages;
using Mopups.Services;

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
            await MopupService.Instance.PopAsync();
        }
    }
}
