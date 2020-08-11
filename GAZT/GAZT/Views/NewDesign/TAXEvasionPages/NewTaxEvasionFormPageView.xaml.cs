using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTaxEvasionFormPageView : ContentPage
    {
        public NewTaxEvasionFormPageView()
        {
            InitializeComponent();
            BindingContext = App.Locator.NewTaxEvasionFormPageView;
        }

        #region Method
        private void RegionBtnClicked(object sender, EventArgs e)
        {
            RegionPicker.IsOpen = true;
        }
        #endregion
    }
}