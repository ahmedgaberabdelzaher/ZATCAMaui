using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using static EGAZT.Models.LoginSSOModel;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.NewDesign.EstablishmentSignUP
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeMobNafathLoginPage : ContentPage
	{
        ChangeMobNafathPageViewMode viewModel;
        HybridWebView Hybridview;
        public ChangeMobNafathLoginPage ()
		{
			InitializeComponent ();
            viewModel = App.Locator.ChangeMobNafathLoginPage;
            BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var url = "";
            Task.Run(async () =>
            {
                try
                {
                   // LoginSSOModelClass modelSSOID = await WebServiceManager.LoginDataSSO();
                    //viewModel.IdNumber = viewModel.modelSSOID.results[0].Idnumber;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        url = Constants.ChangeMobNafath;
                        Hybridview = new HybridWebView();
                        Hybridview.Url = url;
                        RegnagaftGrid.Children.Add(Hybridview, 0, 0);
                        Hybridview.RegisterAction(async (obj) => {
                            if (obj == Constants.AppChangeMobCompanayNafath)
                            {
                                if (App.GUIDFrChangeMob.Contains(Constants.WebKeyChangeMobCompanayNafath))
                                {
                                    viewModel.Goback();
                                    var guid = App.GUIDFrChangeMob.Split("guid=")[1];
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
                            
                        });
                    });
                }
                catch (Exception)
                {

                }

            });



        }

        void webviewNavigated(object sender, WebNavigatedEventArgs e)
        {

        }

    }
}

