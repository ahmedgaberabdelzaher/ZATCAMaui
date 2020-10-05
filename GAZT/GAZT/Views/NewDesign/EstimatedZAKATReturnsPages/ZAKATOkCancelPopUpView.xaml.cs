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

            if(ConfirmationText.Equals("SelectAttachment"))
            {
                Yes.Text = AppResources.NDCamera;
                No.Text = AppResources.NDFiles;
                confirmationText.Text = AppResources.NDSelectFilesOrCameraToUploadTheAttachment;
            }
            else
            {
                Yes.Text = AppResources.ZYes;
                No.Text = AppResources.ZNo;
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
          
        }

        private async void OnOkayButtonClicked(object sender, EventArgs e)
        {
            if(_confirmationText.Equals(AppResources.ZZDoyouwanttoreleasethedeclaration))
            {
                MessagingCenter.Send<Object, string>(this, "YesPressedToReleaseTheReturn", "Yes");

            }
            else if(_confirmationText.Contains(AppResources.ZZDeleteAttachmentConfirmationText))
            {
                MessagingCenter.Send<Object, string>(this, "YesCommandToDeleteTheAttachment", "Yes");
            }
            else if(_confirmationText.Equals("DeleteVATAttachment"))
            {
                MessagingCenter.Send<Object, string>(this, "YesCommandToDeleteVATAttachment", "Yes");
            }
            else if (_confirmationText.Equals("SelectAttachment"))
            {
               await PopupNavigation.Instance.PopAsync();
                MessagingCenter.Send<Object, string>(this, "OnCameraClicked", "Yes");
                return;
            }
            else
            {
                MessagingCenter.Send<Object, string>(this, "YesPressedToAmendheReturn", "Yes");
            }
            OnSelect?.Invoke("Yes");
            PopupNavigation.Instance.PopAsync();

        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            if (_confirmationText.Equals("SelectAttachment"))
            {
               await PopupNavigation.Instance.PopAsync();
                MessagingCenter.Send<Object, string>(this, "OnGalleryClicked", "No");
                return;
                
            }
            //MessagingCenter.Send<Object, string>(this, "NoReceived", "No");
            //OnSelect?.Invoke("No");
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
