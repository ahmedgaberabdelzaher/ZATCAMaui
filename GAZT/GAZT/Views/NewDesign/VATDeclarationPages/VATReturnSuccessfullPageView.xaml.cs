using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATReturnSuccessfullPageView : ContentPage
    {
        #region Variable
        public VATReturnSuccessfullPageViewModel viewModel;
        #endregion
        public VATReturnSuccessfullPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATReturnSuccessfullPageView;
            this.BindingContext = viewModel;
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new RefundAccountPopupPageView());
        }
    }
}