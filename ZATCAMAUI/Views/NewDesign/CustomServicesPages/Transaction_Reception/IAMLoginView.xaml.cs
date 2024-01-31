using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages.Transaction_Reception
{
    public partial class IAMLoginView : BaseContentPage
    {
        IAMLoginViewModel viewModel;
        public IAMLoginView(int commingFrom)
        {
            viewModel = App.Locator.IAMLoginViewModel;
            viewModel.CommingFrom = commingFrom;
            BindingContext = viewModel;
            InitializeComponent();
        }

        void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.ToLower().Contains("result?"))
            {
                viewModel.GetIAMToken(e.Url);
            }
        }
    }
}

