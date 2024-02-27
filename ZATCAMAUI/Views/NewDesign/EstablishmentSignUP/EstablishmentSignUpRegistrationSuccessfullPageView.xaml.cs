using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
namespace ZATCAMAUI.Views.NewDesign.EstablishmentSignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentSignUpRegistrationSuccessfullPageView : ContentPage
    {
        public EstablishmentSignUpRegistrationSuccessfullPageView()
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
        }
    }
}