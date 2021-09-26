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
    public partial class VatReturnNewYesCancelPopUp : PopupPage
    {
        string _confirmationText = string.Empty;
        public delegate void OnSelectDelegate(string item);
        public OnSelectDelegate OnSelect { get; set; } = null;
        public VatReturnNewYesCancelPopUp(string ConfirmationText)
        {
            InitializeComponent();
            SetLTR();
            _confirmationText = confirmationText.Text = ConfirmationText;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
        private async void OnOkayButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<Object, string>(this, "OkayToSubmit", "Yes");
            OnSelect?.Invoke("Yes");
            await PopupNavigation.Instance.PopAsync();

        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<Object, string>(this, "NoToCancel", "No");
            OnSelect?.Invoke("No");
            await PopupNavigation.Instance.PopAsync();

        }

    }
}