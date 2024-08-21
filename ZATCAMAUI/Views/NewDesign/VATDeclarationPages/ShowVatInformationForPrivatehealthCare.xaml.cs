using Mopups.Pages;
using Mopups.Services;
using Syncfusion.Maui.Picker;
using System.Globalization;
using System.Resources;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowVatInformationForPrivatehealthCare : PopupPage
    {
        public ShowVatInformationForPrivatehealthCare()
        {
            InitializeComponent();
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            MopupService.Instance.PopAsync();
        }
    }
}