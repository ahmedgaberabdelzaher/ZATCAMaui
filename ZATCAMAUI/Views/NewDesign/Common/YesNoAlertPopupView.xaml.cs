using RGPopup.Maui.Pages;

namespace ZATCAMAUI.Views.NewDesign.Common
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YesNoAlertPopupView : PopupPage
    {
        public YesNoAlertPopupView(string buttonOKName, string buttonNoName, string message)
        {
            InitializeComponent();
            MessageText.Text = message;
            btnOK.Text = buttonOKName;
            btnNO.Text = buttonNoName;
        }

        private void OnNoButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<YesNoAlertPopupView, bool>(this, "YesNoAlertPopupResponse", false);
        }
        private void OnOkButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<YesNoAlertPopupView, bool>(this, "YesNoAlertPopupResponse", true);
        }
    }
}