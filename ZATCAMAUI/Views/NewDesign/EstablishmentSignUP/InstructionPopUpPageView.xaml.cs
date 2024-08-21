using Mopups.Pages;
using ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentSignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionPopUpPageView : PopupPage
    {
        public InstructionPopUpPageView()
        {
            InitializeComponent();
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                string baseUrl = DependencyService.Get<IBaseUrl>().Get();
                string path = DependencyService.Get<IBaseUrl>().Get();
                if (!App.IsArabic)
                {
                    string url = Path.Combine(path, "TermsAndConditionsEN.html");
                    TCWebView.Source = url;
                }
                else
                {
                    string url = Path.Combine(path, "TermsAndConditionsAR.html");
                    TCWebView.Source = url;
                }
            }
            else
            {
                if (!App.IsArabic)
                {
                    TCWebView.Source = "file:///android_asset/TermsAndConditionsEN.html";
                }
                else
                {
                    TCWebView.Source = "file:///android_asset/TermsAndConditionsAR.html";
                }
            }
        }
    }
}