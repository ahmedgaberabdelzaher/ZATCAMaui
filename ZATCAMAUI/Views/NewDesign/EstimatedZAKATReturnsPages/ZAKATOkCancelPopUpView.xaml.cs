using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;

namespace ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages
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
                string QuestionMark = string.Empty;
                if (App.IsArabic)
                {
                    QuestionMark = "؟";
                }
                else
                {
                    QuestionMark = "?";
                }

                confirmationText.Text = AppResources.ZZDeleteAttachmentConfirmationText + " " + VATDeclarationAttachmentPageView.AttachmentName + QuestionMark;
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
            await PopupNavigation.Instance.PopAsync();

        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            if (_confirmationText.Equals("SelectAttachment"))
            {
               await PopupNavigation.Instance.PopAsync();
                MessagingCenter.Send<Object, string>(this, "OnGalleryClicked", "No");
                return;
                
            }
            OnSelect?.Invoke("No");
            await PopupNavigation.Instance.PopAsync();
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
