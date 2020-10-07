using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.SyncFusionEnabledViews.LoginPages
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
