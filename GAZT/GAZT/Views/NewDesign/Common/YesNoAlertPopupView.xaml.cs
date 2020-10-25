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
    public partial class YesNoAlertPopupView : PopupPage
    {
        public YesNoAlertPopupView(string buttonOKName, string buttonNoName, string message)
        {
            InitializeComponent();
            MessageText.Text = message;
            btnOK.Text = buttonOKName;
            btnNO.Text = buttonNoName;
            SetLTR();
        }

        private void OnNoButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<YesNoAlertPopupView, bool>(this, "YesNoAlertPopupResponse", false);
        }
        private void OnOkButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<YesNoAlertPopupView, bool>(this, "YesNoAlertPopupResponse", true);
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}