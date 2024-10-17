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
            FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }

        private void OnClosedTapped(object sender, EventArgs e)
        {
            MopupService.Instance.PopAsync();
        }
    }
}