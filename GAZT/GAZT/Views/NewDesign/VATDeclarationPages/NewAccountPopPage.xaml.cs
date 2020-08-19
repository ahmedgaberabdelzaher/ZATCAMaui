using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Pages;
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
    public partial class NewAccountPopPage : PopupPage
    {
        public NewAccountPopPageViewModel viewModel;
        public NewAccountPopPage()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.NewAccountPopPageView;
                this.BindingContext = viewModel;
                SetLTR();
            }
            catch(Exception ex)
            {

            }
        }
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
        private void Closed_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}