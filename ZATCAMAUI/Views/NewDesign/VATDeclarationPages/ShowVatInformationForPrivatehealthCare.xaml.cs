using Mopups.Pages;
using Mopups.Services;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowVatInformationForPrivatehealthCare : PopupPage
    {
        public ShowVatInformationForPrivatehealthCare()
        {
            InitializeComponent();
            FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            MopupService.Instance.PopAsync();
        }
    }
}