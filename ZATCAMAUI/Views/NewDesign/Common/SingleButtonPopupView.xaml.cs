using Mopups.Pages;

namespace ZATCAMAUI.Views.NewDesign.Common
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleButtonPopupView : PopupPage
    {
        public SingleButtonPopupView(string buttonName, string message)
        {
            InitializeComponent();
            MessageText.Text = message;
            btnOK.Text = buttonName;
        }
        public SingleButtonPopupView(string buttonName, string message, string header)
        {
            InitializeComponent();
            MessageText.Text = message;
            btnOK.Text = buttonName;
            lblPopupHeader.Text = header;

            if (string.IsNullOrEmpty(header))
                lblPopupHeader.IsVisible = false;
            else
                lblPopupHeader.IsVisible = true;
        }

        private void OnOkayButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse", true);
        }
       

        private void OnBackGroundClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<SingleButtonPopupView, bool>(this, "SingleButtonPopupBackgroundClickedResponse", true);
        }
    }
}