using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ForgotPasswordPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignRecoverUsernamePageView : ContentPage
    {
        public GAZTNewDesignRecoverUsernamePageView()
        {
            InitializeComponent();
            BindingContext = App.Locator.GAZTNewDesignRecoverUsernameViewModel;
            NavigationPage.SetHasBackButton(this, false);

        }
    }
}