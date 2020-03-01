using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
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
    public partial class CorrespondenceDetailsPageView : ContentPage
    {
        CorrespondenceDetailsPageViewModel viewModel;
        public CorrespondenceDetailsPageView(CorrespondanceModel CorrModel)
        {
            InitializeComponent();
            viewModel = App.Locator.CorrespondenceDetailsPageView;
            this.BindingContext = viewModel;
            CorrespondenceDetailsRootObject CorrespondenceD = new CorrespondenceDetailsRootObject();
            try
            {
                CorrespondenceD = WebServiceManager.GAZTGetCorrespondeceDetails(CorrModel);
                PopToRootPage();
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
            string HTMLContent = string.Empty;

            foreach(CorrespondenceDetailsResult ItemC in CorrespondenceD.d.results)
            {
                HTMLContent = HTMLContent + ItemC.Tdline;
            }
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html =HTMLContent;
            CorWebView.Source = htmlSource;

            viewModel.CorrespondenceTitle = CorrModel.Title;
            viewModel.CorrespondenceD = CorrModel;
            if(CorrModel.IsFav==true)
            {
                viewModel.FavIcon = "ic_star.png"; 
            }
            else
            {
                viewModel.FavIcon = "ic_star_border.png";
            }
            SetLTR();
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }

    }
}