using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RefundAccountPopupPageView : PopupPage
    {
        public RefundAccountPopupPageView()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new NewAccountPopPage());
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            Picker1.IsOpen = true;
        }

        private void SfButton_Clicked1(object sender, EventArgs e)
        {
            Picker2.IsOpen = true;
        }
    }
}