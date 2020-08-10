using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttachmentPopUp : PopupPage
    {
        public AttachmentPopUp()
        {
            InitializeComponent();
        }

        private void OnCloseTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}