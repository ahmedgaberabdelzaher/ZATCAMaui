using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

namespace ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages
{

    public partial class ReturnPortalNavigationPopUp : PopupPage
    {
        public delegate void OnDoneDelegate();
        public OnDoneDelegate OnDone { get; set; } = null;

        public delegate void OnGotoPortalDelegate();
        public OnGotoPortalDelegate OnGotoPortal { get; set; } = null;

        public ReturnPortalNavigationPopUp(string infromationText)
        {
            InitializeComponent();
            InfromatationText.Text = infromationText;
        }

        private void OnCloseTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void GotoPortalButtonClicked(object sender, EventArgs e)
        {
            OnGotoPortal?.Invoke();
            PopupNavigation.Instance.PopAsync();
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            OnDone?.Invoke();
            PopupNavigation.Instance.PopAsync();
        }


    }
}
