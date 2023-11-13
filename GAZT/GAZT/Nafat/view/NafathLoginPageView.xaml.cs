using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EGAZT;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using static EGAZT.Models.LoginSSOModel;

namespace EGAZT.Views.NewDesign.EstablishmentSignUP
{
    public partial class NafathLoginPageView : ContentPage
    {
        NafathLoginPageViewModel viewModel;
        HybridWebView Hybridview;
        public NafathLoginPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.NafathLoginPageView;
            this.BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetLTR();
            ChangeAeroIcon();

        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var url = "";
            Task.Run(async () =>
            {
                try
                {
                    LoginSSOModelClass modelSSOID = await WebServiceManager.LoginDataSSO();
                    //viewModel.IdNumber = viewModel.modelSSOID.results[0].Idnumber;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        url = modelSSOID.results[0].Murl;
                        Hybridview = new HybridWebView();
                        Hybridview.Url = url;
                        RegnagaftGrid.Children.Add(Hybridview, 0, 0);
                        Hybridview.RegisterAction(async (obj) =>
                        {
                            if (obj == "navigateToVATIndividualSignupPageSSO")
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    try
                                    {
                                        viewModel._navigationService.NavigateTo(App.IndividualRegistrationPageView, "RegisterPageSSO");
                                        //API Call
                                    }
                                    catch (Exception)
                                    {

                                    }
                                });
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
                            if (obj == "success")
                            {
                                try
                                {
                                    string[] minMaxVersions = App.LoginDataRetrieved.AppVersion.Split('-');

                                    if (minMaxVersions.Count() > 1)
                                    {
                                        double minVer = Convert.ToDouble(minMaxVersions[0].Replace(".", string.Empty));
                                        double maxVer = Convert.ToDouble(minMaxVersions[1].Replace(".", string.Empty));
                                        double currVer = Convert.ToDouble(App.AppVersion.Replace(".", string.Empty));

                                        if (currVer >= minVer && currVer <= maxVer)
                                        {
                                            App.IsUserLoggedIn = true;
                                            Xamarin.Forms.Application.Current.Properties["timeOut"] = DateTime.Now;
                                            await viewModel.LoginCompletedInWebView();
                                        }
                                        else
                                        {
                                            Hybridview.Opacity = 0;
                                            viewModel.IsLoading = false;

                                            await viewModel._dialogService.ShowMessageBox(AppResources.VersonCheckErrorMsg, AppResources.VersonCheckErrorTitle);
                                            viewModel._navigationService.GoBack();
                                            // await LogoffUser();
                                        }
                                    }
                                    else
                                    {
                                        App.LoginDataRetrieved.AppVersion = string.Empty;

                                        if (App.LoginDataRetrieved.AppVersion == App.AppVersion)
                                        {
                                            App.IsUserLoggedIn = true;
                                            await viewModel.LoginCompletedInWebView();
                                        }
                                        else
                                        {
                                            Hybridview.Opacity = 0;
                                            viewModel.IsLoading = false;

                                            await viewModel._dialogService.ShowMessageBox(AppResources.VersonCheckErrorMsg, AppResources.VersonCheckErrorTitle);
                                            viewModel._navigationService.GoBack();
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    await viewModel._dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                                }
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

