
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentSignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeMobNafathLoginPage : ContentPage
    {
        ChangeMobNafathPageViewMode viewModel;
        HybridWebView Hybridview;
        public ChangeMobNafathLoginPage()
        {
            InitializeComponent();
            viewModel = App.Locator.ChangeMobNafathLoginPage;
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var url = "";
            try
            {
                MainThread.BeginInvokeOnMainThread( () =>
                {
                    url = ZATCAConstants.ChangeMobNafath;
                    Hybridview = new HybridWebView();
                    Hybridview.Url = url;
                    RegnagaftGrid.Add(Hybridview, 0, 0);
                    Hybridview.RegisterAction( (obj) =>
                    {
                        if (obj == ZATCAConstants.AppChangeMobCompanayNafath)
                        {
                            if (App.GUIDFrChangeMob.Contains(ZATCAConstants.WebKeyChangeMobCompanayNafath))
                            {
                                viewModel.Goback();
                                var guid = App.GUIDFrChangeMob.Split(new string[] { "guid=" }, StringSplitOptions.None)[1];
                                viewModel._navigationService.GoBack();
                                viewModel._navigationService.NavigateTo(App.ChangeMobileRequestPageView, guid);

                            }

                        }
                        if (obj == "displayLoginLoadingIndicator")
                        {
                            Hybridview.Opacity = 0;
                            viewModel.IsLoading = true;
                        }

                        if (obj == "displayLoadingIndicator")
                        {
                            viewModel.IsLoading = true;
                        }

                        if (obj == "hideLoadingIndicator")
                        {
                            Hybridview.Opacity = 1;
                            viewModel.IsLoading = false;

                        }

                        if (obj == "hideLoginLoadingIndicator")
                        {
                            viewModel.IsLoading = false;
                        }
                        if (obj == "displayLoadingIndicator")
                        {
                            viewModel.IsLoading = true;
                        }
                        if (obj == "navigateBackToLoginPage")
                        {
                            viewModel.Goback();
                        }

                    });
                });
            }
            catch (Exception)
            {

            }


        }


    }
}

