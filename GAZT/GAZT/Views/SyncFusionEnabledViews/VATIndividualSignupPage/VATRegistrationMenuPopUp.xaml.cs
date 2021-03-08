using EGAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATRegistrationMenuPopUp : PopupPage
    {
        public VATRegistrationMenuPopUp()
        {
            InitializeComponent();
        }

        private async void OnAttachmentTapped(object sender, EventArgs e)
        {
            await attach.TranslateTo(0, 0, 0, Easing.BounceOut);
            VATRegistrationDetails vatReg = null;

             await PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView(vatReg));
        }

        private void CloseTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}