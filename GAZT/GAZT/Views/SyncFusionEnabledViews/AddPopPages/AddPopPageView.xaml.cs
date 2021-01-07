using EGAZT.ViewModel.SyncFusionEnabledViewModel.AddPopPage_ViewModel;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.AddPop
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPopPageView : PopupPage
    {
        AddPopPageViewModel viewModel;
        public AddPopPageView(PopUp objPopUP)
        {
            if (objPopUP != null)
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
                    if(string.IsNullOrEmpty(objPopUP.HeaderText))
                    {
                        viewModel.HeaderText = AppResources.ZInstructions;
                    }
                    else
                    {
                        viewModel.HeaderText = objPopUP.HeaderText;
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
