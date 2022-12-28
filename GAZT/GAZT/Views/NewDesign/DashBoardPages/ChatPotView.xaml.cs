using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.SupportPageVM;
using GAZT.Helper;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.DashBoardPages
{
    public partial class ChatPotView : BaseContentPage
    {
        ChatViewModel viewModel;
        public ChatPotView()
        {
            viewModel = App.Locator.ChatViewModel;
            BindingContext = viewModel;
            InitializeComponent();
            if (App.IsArabic)
            {

                ChatWebView.Source = Constants.GAZTChatPartialUrlar;
            }
            else
            {
                ChatWebView.Source = Constants.GAZTChatPartialUrlen;
            }
        }
    }
}

