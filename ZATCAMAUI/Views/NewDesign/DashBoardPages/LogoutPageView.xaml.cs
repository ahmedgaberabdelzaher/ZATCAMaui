using Mopups.Pages;
using Mopups.Services;

namespace ZATCAMAUI.Views.NewDesign.DashBoardPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutPageView : PopupPage
    {
        string _confirmationText = string.Empty;
        public delegate void OnSelectDelegate(string item);
        public OnSelectDelegate OnSelect { get; set; } = null;
        public LogoutPageView(string ConfirmationText)
        {
            InitializeComponent();
            FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            _confirmationText = confirmationText.Text = ConfirmationText;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        private async void OnOkayButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<Object, string>(this, "YesPressedToLogout", "Yes");
            OnSelect?.Invoke("Yes");
            await MopupService.Instance.PopAsync();

        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<Object, string>(this, "NoPressedToLogout", "No");
            OnSelect?.Invoke("No");
            await MopupService.Instance.PopAsync();

        }
        
    }
}