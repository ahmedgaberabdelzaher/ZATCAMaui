using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TINOutletDeregister;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.TINOutletDeregister;

public partial class DeregistrationSuccessPageView : ContentPage
{
	public TinOutletDeRegRequestViewModel viewModel;
	public DeregistrationSuccessPageView(TinOutletDeRegRequestViewModel viewModel)
	{
		try
		{
			InitializeComponent();
			this.viewModel = viewModel;
			this.BindingContext = viewModel;

			TaxpayerName.Text = viewModel.tinOutletPrevousRequestsModel.D.ATaxpayerName;
			RequestReferenceNumber.Text = viewModel.tinOutletPrevousRequestsModel.D.Fbnumz;
			ReceiptDate.Text = Convert.ToDateTime(viewModel.tinOutletPrevousRequestsModel.D.ASubmissionDate.ToString()).ToShortDateString();
		}
		catch (Exception)
		{

		}
	}

	void Download_AcknowledgementForm(System.Object sender, System.EventArgs e)
	{
		if (viewModel.tinOutletPrevousRequestsModel.D.Fbnumz != null)
		{
			string downloadurl = ZATCAConstants.ZakatExemtionDownloadCert + viewModel.tinOutletPrevousRequestsModel.D.Fbnumz;
			viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);
		}
	}
	void Dashboard_Tapped(System.Object sender, System.EventArgs e)
	{
		var _navigation = Application.Current.MainPage.Navigation;
		foreach (var item in _navigation.NavigationStack)
		{
			if (item.GetType().Name == App.TinOutletDeRegRequestPageView)
			{
				_navigation.RemovePage(item);
				break;
			}
		}

		foreach (var item in _navigation.NavigationStack)
		{
			if (item.GetType().Name == App.TINOutletDeregistrationPageView)
			{
				_navigation.RemovePage(item);
				break;
			}
		}
		foreach (var item in _navigation.NavigationStack)
		{
			if (item.GetType().Name == App.DeregistrationSuccessPageView)
			{
				_navigation.RemovePage(item);
				break;
			}
		}
		viewModel._navigationService.NavigateTo(App.TINOutletDeregistrationPageView);
	}

	async void OnCopyReferenceNumberButtonClicked(System.Object sender, System.EventArgs e)
	{
		try
		{
			if (viewModel.tinOutletPrevousRequestsModel.D.Fbnumz != null)
			{
				await Clipboard.SetTextAsync(viewModel.tinOutletPrevousRequestsModel.D.Fbnumz);
				if (Clipboard.HasText)
				{
					var text = await Clipboard.GetTextAsync();
					await viewModel._dialogService.ShowMessageBox(AppResources.CRReferenceNumber + " " + text, AppResources.Copied);
				}
			}
		}
		catch (Exception)
		{
		}
	}

}