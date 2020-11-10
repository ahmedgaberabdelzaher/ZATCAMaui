using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleButtonPopupView : PopupPage
    {
        public SingleButtonPopupView(string buttonName, string message)
        {
            InitializeComponent();
            MessageText.Text = message;
            btnOK.Text = buttonName;
            SetLTR();
        }

        private void OnOkayButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse", true);
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void OnBackGroundClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<SingleButtonPopupView, bool>(this, "SingleButtonPopupBackgroundClickedResponse", true);
        }
    }
}