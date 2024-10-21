using Mopups.Pages;
using Mopups.Services;
using ZATCAMAUI.Core.Helper;

namespace ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages
{
    public partial class ErrorMessagePopup : PopupPage
    {
        public delegate void OnDoneDelegate();
        public delegate void OnLinkDelegate();

        public OnDoneDelegate OnDone { get; set; } = null;
        public OnLinkDelegate OnLink { get; set; } = null;

        public ErrorMessagePopup(string infromationText)
        {
            InitializeComponent();
            InfromatationText.Text = infromationText;
            FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }


        private void OnCloseTapped(object sender, EventArgs e)
        {
            MopupService.Instance.PopAsync();
        }

        private void OnOkClicked(object sender, EventArgs e)
        {
            MopupService.Instance.PopAsync();
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            OnDone?.Invoke();
            MopupService.Instance.PopAsync();
        }

        void Link_Clicked(System.Object sender, System.EventArgs e)
        {
            // OnLink?.Invoke();
            MopupService.Instance.PopAsync();
            if (App.IsArabic)
            {
                Uri uri = new Uri(ZATCAConstants.ZAtcaContactUsAR);
                OpenBrowser(uri);
            }
            else
            {
                Uri uri = new Uri(ZATCAConstants.ZAtcaContactUsEN);
                OpenBrowser(uri);
            }
            
        }
        public async void OpenBrowser(Uri uri)
        {
            await Launcher.OpenAsync(uri);
        }
    }
}