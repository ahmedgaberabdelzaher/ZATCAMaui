using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
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
           
            
            await PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView());
        }
    }
}