using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages
{
    public partial class ZAKATOkCancelPopUpView : PopupPage
    {
        string _confirmationText = string.Empty;
        public delegate void OnSelectDelegate(string item);
        public OnSelectDelegate OnSelect { get; set; } = null;
        public ZAKATOkCancelPopUpView(string ConfirmationText)
        {
            InitializeComponent();
            SetLTR();
            if(ConfirmationText.Equals("DeleteVATAttachment"))
            {
                _confirmationText = "DeleteVATAttachment";
                confirmationText.Text = AppResources.ZZDeleteAttachmentConfirmationText;
            }
            else
            {
                _confirmationText = confirmationText.Text = ConfirmationText;
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
          
        }

        private void OnOkayButtonClicked(object sender, EventArgs e)
        {
            if(_confirmationText.Equals(AppResources.ZZDoyouwanttoreleasethedeclaration))
            {
                MessagingCenter.Send<Object, string>(this, "YesPressedToReleaseTheReturn", "Yes");

            }
            else if(_confirmationText.Equals(AppResources.ZZDeleteAttachmentConfirmationText))
            {
                MessagingCenter.Send<Object, string>(this, "YesCommandToDeleteTheAttachment", "Yes");
            }
            else if(_confirmationText.Equals("DeleteVATAttachment"))
            {
                MessagingCenter.Send<Object, string>(this, "YesCommandToDeleteVATAttachment", "Yes");
            }
            else
            {
                MessagingCenter.Send<Object, string>(this, "YesPressedToAmendheReturn", "Yes");
            }
            OnSelect?.Invoke("Yes");
            PopupNavigation.Instance.PopAsync();

        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<Object, string>(this, "NoReceived", "No");
            OnSelect?.Invoke("No");
            PopupNavigation.Instance.PopAsync();

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
