using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Nafat;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentSignUP;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class NafathLoginPageView : ContentPage
{
	NafathLoginPageViewModel viewModel;
	HybridWebView Hybridview;
	public NafathLoginPageView()
	{
		InitializeComponent();
		viewModel = App.Locator.NafathLoginPageView;
		BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		var url = "";
		Task.Run(async () =>
		{
			try
			{
				LoginSSOModelClassERAD modelSSOID = await WebServiceManager.LoginDataSSO();
				//viewModel.IdNumber = viewModel.modelSSOID.results[0].Idnumber;
				MainThread.BeginInvokeOnMainThread(async () =>
				{
					url = modelSSOID.results[0].Murl;
					Hybridview = new HybridWebView();
					Hybridview.Url = url;
					//    RegnagaftGrid.Children.Add(Hybridview, 0, 0);
					Hybridview.RegisterAction(async (obj) =>
					{
						if (obj == "navigateToVATIndividualSignupPageSSO")
						{
							MainThread.BeginInvokeOnMainThread(() =>
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
										Preferences.Default.Set("timeOut", DateTime.Now);
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
}