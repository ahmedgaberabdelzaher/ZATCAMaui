using Mopups.Pages;
using Mopups.Services;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileAttachmentPopupPageView : PopupPage
    {
        public FileAttachmentPopupPageView()
        {
            InitializeComponent();
        }

        private void OnClosedTapped(object sender, EventArgs e)
        {
            MopupService.Instance.PopAsync();
        }
    }
}