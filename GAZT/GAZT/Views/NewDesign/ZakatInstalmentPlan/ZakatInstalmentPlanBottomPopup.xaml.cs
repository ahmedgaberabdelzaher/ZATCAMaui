
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatInstalmentPlanBottomPopup : PopupPage
    {
        public ZakatInstalmentPlanBottomPopup()
        {
            InitializeComponent();
        }

        private void Close_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}