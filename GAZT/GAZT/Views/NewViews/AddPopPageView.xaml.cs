using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPopPageView : PopupPage
    {

        AddPopPageViewModel viewModel;
        public AddPopPageView(PopUp objPopUP)
        {
            InitializeComponent();
            try
            {
                viewModel = App.Locator.AddPopPageView;
                this.BindingContext = viewModel;
                InitializeComponent();
                SetLTR();
                viewModel.PopMessage = objPopUP.Message;
                viewModel.IsVisibleLink = objPopUP.IsLinkAvailable;
                viewModel.Link = objPopUP.Link;
                viewModel.LinkMessage = objPopUP.LinkMessage;
            }
            catch (Exception e)
            {

            }
        }
        private void SetLTR()
        {


            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void CloseImage_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
