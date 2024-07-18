using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

namespace ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages
{

    public partial class AttachmentInformationPopUp : PopupPage
    {
        public delegate void OnDoneDelegate();
        public OnDoneDelegate OnDone { get; set; } = null;

        public AttachmentInformationPopUp(string infromationText)
        {
            InitializeComponent();
            this.FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            InfromatationText.Text = infromationText;
        }

        private void OnCloseTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void OnOkClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            OnDone?.Invoke();
            PopupNavigation.Instance.PopAsync();
        }
    }
}
