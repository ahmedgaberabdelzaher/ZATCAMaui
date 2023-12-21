using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

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
            MessagingCenter.Send<Object, string>(this, "YesPressedToLogout", "Yes");
            OnSelect?.Invoke("Yes");
            await PopupNavigation.Instance.PopAsync();

        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<Object, string>(this, "NoPressedToLogout", "No");
            OnSelect?.Invoke("No");
            await PopupNavigation.Instance.PopAsync();

        }
        
    }
}