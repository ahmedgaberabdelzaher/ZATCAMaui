using Mopups.Pages;
using Mopups.Services;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmationPopUpForVatRegistration : PopupPage
    {
        string _FromWhere = string.Empty;
        public delegate void OnSelectDelegate(string item);
        public OnSelectDelegate OnSelect { get; set; } = null;
        public ConfirmationPopUpForVatRegistration(string ConfirmationText, string FromWhere)
        {
            InitializeComponent();
            _FromWhere = FromWhere;
            confirmationText.Text = ConfirmationText;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        private void OnOkayButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_FromWhere))
            {
                if (_FromWhere == "FileAttachmentPopUpPageView")
                {
                    MessagingCenter.Send<object, string>(this, "YesPressedToDeleteAttachment", "Yes");
                    OnSelect?.Invoke("Yes");
                    MopupService.Instance.PopAsync();
                }
                else if (_FromWhere == "FinancialDetailAttachmentPopupPageView")
                {
                    MessagingCenter.Send<object, string>(this, "YesPressedToDeleteFinancialAttachment", "Yes");
                    OnSelect?.Invoke("Yes");
                    MopupService.Instance.PopAsync();
                }
            }


        }

        private void OnCancelClicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(_FromWhere))
            {
                if (_FromWhere == "FileAttachmentPopUpPageView")
                {
                    MessagingCenter.Send<object, string>(this, "NoPressedToDeleteAttachment", "No");
                    OnSelect?.Invoke("No");
                    MopupService.Instance.PopAsync();
                }
                else if (_FromWhere == "FinancialDetailAttachmentPopupPageView")
                {
                    MessagingCenter.Send<object, string>(this, "NoPressedToDeleteFinancialAttachment", "No");
                    OnSelect?.Invoke("No");
                    MopupService.Instance.PopAsync();
                }
            }



        }
    }
}