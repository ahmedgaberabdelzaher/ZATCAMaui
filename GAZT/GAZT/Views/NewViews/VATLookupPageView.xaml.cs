using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATLookupPageView : ContentPage
    {
        VATLookupPageViewModel viewModel;
        int LanguageToolBarCount = 0;
        public VATLookupPageView()
        {
            viewModel = App.Locator.VATLookupPageView;
            InitializeComponent();
            this.BindingContext = viewModel;
            viewModel.OnPageLoad();
            SetLTR();
            //ToolbarItem toolbarItem1 = new ToolbarItem
            //{
              
            
            //};
            //if (LanguageToolBarCount == 0)
            //{
            //    LanguageToolBarCount = 1;
            //    this.ToolbarItems.Add(toolbarItem1);
            //}
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PPicker.Focus();
        }
    }
}