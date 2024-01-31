using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.SupportPageVM;

namespace ZATCAMAUI.Views.NewDesign.DashBoardPages
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

                ChatWebView.Source = ZATCAConstants.GAZTChatPartialUrlar;
            }
            else
            {
                ChatWebView.Source = ZATCAConstants.GAZTChatPartialUrlen;
            }
        }
    }
}

