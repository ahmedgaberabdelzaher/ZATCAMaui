using Mopups.Pages;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionPopUp : PopupPage
    {
        public InstructionPopUp()
        {
            InitializeComponent();
            FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }



        private void OnClickedFAQ(object sender, EventArgs e)
        {
            string FaqUrl = string.Empty;
            if (App.IsArabic)
            {
                FaqUrl = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/default.aspx";
            }
            else
            {
                FaqUrl = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/default.aspx";
            }
            Launcher.OpenAsync(new Uri(FaqUrl));
        }
    }
}