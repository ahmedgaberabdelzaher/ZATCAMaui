using ZATCAMAUI.Core.CustomControls;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.ChangeNumber;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class NewChaneMobileView : ContentPage
{

	HybridWebView Hybridview;
	public NewChaneMobileView()
	{
		InitializeComponent();
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
}