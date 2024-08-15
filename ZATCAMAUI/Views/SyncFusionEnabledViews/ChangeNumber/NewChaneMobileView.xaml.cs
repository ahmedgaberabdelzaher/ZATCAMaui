using ZATCAMAUI.Core.CustomControls;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.ChangeNumber;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class NewChaneMobileView : ContentPage
{

	NewChaneMobileViewModel viewModel;
	HybridWebView Hybridview;
	public NewChaneMobileView()
	{
		InitializeComponent();
		viewModel = App.Locator.newChaneMobileViewModel;
		this.BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Task.Run(() =>
		{
			try
			{
				var urlWeb = "https://www.iam.gov.sa/authservice/userauthservice?lang=en";
			}
			catch (Exception)
			{

			}

		});



	}

    void loadweb_Navigated(System.Object sender, Microsoft.Maui.Controls.WebNavigatedEventArgs e)
    {
        viewModel.IsLoading = false;
    }

    void loadweb_Navigating(System.Object sender, Microsoft.Maui.Controls.WebNavigatingEventArgs e)
    {
        viewModel.IsLoading = true;
    }
}