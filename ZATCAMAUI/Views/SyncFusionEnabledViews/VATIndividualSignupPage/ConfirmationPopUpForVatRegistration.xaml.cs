using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

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
            SetLTR();
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
                    PopupNavigation.Instance.PopAsync();
                }
                else if (_FromWhere == "FinancialDetailAttachmentPopupPageView")
                {
                    MessagingCenter.Send<object, string>(this, "YesPressedToDeleteFinancialAttachment", "Yes");
                    OnSelect?.Invoke("Yes");
                    PopupNavigation.Instance.PopAsync();
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
                    PopupNavigation.Instance.PopAsync();
                }
                else if (_FromWhere == "FinancialDetailAttachmentPopupPageView")
                {
                    MessagingCenter.Send<object, string>(this, "NoPressedToDeleteFinancialAttachment", "No");
                    OnSelect?.Invoke("No");
                    PopupNavigation.Instance.PopAsync();
                }
            }



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