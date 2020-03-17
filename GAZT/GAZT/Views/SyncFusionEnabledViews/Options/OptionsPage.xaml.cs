using GAZT;
using GAZTeServicesApp.ViewModels.LandingPage;
using GAZTeServicesApp.ViewModels.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace GAZTeServicesApp.Views.Options
{
    /// <summary>
    /// Page to show the setting.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPageView : ContentPage
    {
        OptionsPageViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsPageView" /> class.
        /// </summary>
        public OptionsPageView()
        {
            InitializeComponent();
            this.BindingContext = viewModel = App.Locator.OptionsPageView;
        }
    }
}