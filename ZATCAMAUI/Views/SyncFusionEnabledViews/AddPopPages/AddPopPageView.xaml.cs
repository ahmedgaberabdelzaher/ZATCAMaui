using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AddPopPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages
{

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
                    if (objPopUP.isFontSet)
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            Message_label.FontFamily = "Somar-Bold.otf#SomarBold";
                        }
                        else
                        {
                            Message_label.FontFamily = "Somar-Bold";
                        }
                    }
                    if (string.IsNullOrEmpty(objPopUP.HeaderText))
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
                        viewModel.IsRed = "{StaticResource ForgotPasswordGrayTextColor}";
                    }
                    viewModel.FlowDirections = objPopUP.FlowDirections;
                }
                catch (Exception)
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
                        viewModel.IsRed = "{StaticResource ForgotPasswordGrayTextColor}";
                    }
                    viewModel.FlowDirections = objPopUP.FlowDirections;
                }
            }
        }
       
        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
        private void CloseImage_Tapped(object sender, EventArgs e)
        {
            viewModel.IsBold = "Bold";
            viewModel.IsRed = "{StaticResource ForgotPasswordGrayTextColor}";
            PopupNavigation.Instance.PopAsync();
        }
    }
}
