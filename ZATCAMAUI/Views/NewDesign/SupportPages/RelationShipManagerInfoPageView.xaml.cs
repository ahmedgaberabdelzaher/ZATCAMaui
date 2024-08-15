using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;

namespace ZATCAMAUI.Views.NewDesign.SupportPages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class RelationShipManagerInfoPageView : ContentPage
{
	RelationShipManagerInfoPageViewModel viewModel;
	public RelationShipManagerInfoPageView()
	{
		InitializeComponent();
		viewModel = App.Locator.RelationShipManagerInfoPageView;
		this.BindingContext = viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		await viewModel.GetRmContactDetailsOnPageLoad();
	}

	public async void submitComplaintToRelationShipManager(object sender, EventArgs e)
	{
		try
		{

			string lang = WebServiceManager.GetLangZParameterAREN();
			if (lang.Equals("AR"))
				await Browser.OpenAsync(new Uri(ZATCAConstants.ComplaintsARUrl));
			else
				await Browser.OpenAsync(new Uri(ZATCAConstants.ComplaintsEngUrl));
		}
		catch (Exception)
		{

		}
	}
}