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
           
            try
            {
                viewModel = App.Locator.AddPopPageView;
                this.BindingContext = viewModel;
                InitializeComponent();
                SetLTR();

                if (objPopUP.isFontSet)
                {
                    if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
                    {
                        Message_label.FontFamily = "SSTArabic-Bold.ttf#SSTArabic";
                    }
                    else
                    {
                        Message_label.FontFamily = "SSTArabic-Bold";
                    }
                    
                    //Msglabel.FontFamily = "SSTArBold";
                }
                viewModel.PopMessage = objPopUP.Message;
                viewModel.IsVisibleLink = objPopUP.IsLinkAvailable;
                viewModel.Link = objPopUP.Link;
                viewModel.LinkMessage = objPopUP.LinkMessage;
                if (!string.IsNullOrEmpty(objPopUP.IsBold))
                {
                    viewModel.IsBold = objPopUP.IsBold;
                }
                else
                {
                    viewModel.IsBold = "Bold";
                }
                if (!string.IsNullOrEmpty(objPopUP.IsRed))
                {
                    viewModel.IsRed = objPopUP.IsRed;
                }
                else
                {
                    viewModel.IsRed = "#7D858D";
                }
                viewModel.FlowDirections = objPopUP.FlowDirections;

               
            }
            catch (Exception e)
            {
                viewModel.PopMessage = objPopUP.Message;
                viewModel.IsVisibleLink = objPopUP.IsLinkAvailable;
                viewModel.Link = objPopUP.Link;
                viewModel.LinkMessage = objPopUP.LinkMessage;
                if (!string.IsNullOrEmpty(objPopUP.IsBold))
                {
                    viewModel.IsBold = objPopUP.IsBold;
                }
                else
                {
                    viewModel.IsBold = "Bold";
                }
                if (!string.IsNullOrEmpty(objPopUP.IsRed))
                {
                    viewModel.IsRed = objPopUP.IsRed;
                }
                else
                {
                    viewModel.IsRed = "#7D858D";
                }
                viewModel.FlowDirections = objPopUP.FlowDirections;
                SetLTR();
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
            viewModel.IsBold = "Bold";
            viewModel.IsRed = "#7D858D";
            PopupNavigation.Instance.PopAsync();
        }
    }
}
