using GAZTeServicesApp.ViewModels.LoginPage;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace GAZTeServicesApp.Views.LoginPage
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        LoginPageViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = viewModel = App.Locator.LoginPageView;
            ParentContainer.RaiseChild(BusyIndicator);
        }
    }
}