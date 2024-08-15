using ZATCAMAUI.Core.Helper;

namespace ZATCAMAUI.Views.NewDesign.ZakatExemptionRequest;

public partial class ZakatExemptionSuccessPage : ContentPage
{
	public ZakatExemptionPageViewModel viewModel;
	public ZakatExemptionSuccessPage(ZakatExemptionPageViewModel viewModel)
	{
		try
		{
			InitializeComponent();
			this.viewModel = viewModel;
			this.BindingContext = viewModel;

			TaxpayerName.Text = viewModel.zakatExemptionresponse.D.TinName;
			RequestReferenceNumber.Text = viewModel.zakatExemptionresponse.D.Fbnumz;
			ReceiptDate.Text = viewModel.zakatExemptionresponse.D.CrDate.ToString();
		}
		catch (Exception)
		{

		}
	}


	void Download_AcknowledgementForm(System.Object sender, System.EventArgs e)
	{
		if (viewModel.zakatExemptionresponse.D.Fbnumz != null)
		{

			String downloadurl = ZATCAConstants.ZakatExemtionDownloadCert + viewModel.zakatExemptionresponse.D.Fbnumz;
			//await WebServiceManager.FileDownload(downloadurl, "pdf");
			viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

		}
	}
	void Dashboard_Tapped(System.Object sender, System.EventArgs e)
	{
		var _navigation = Application.Current.MainPage.Navigation;
		foreach (var item in _navigation.NavigationStack)
		{
			if (item.GetType().Name == App.ZakatExemptionPageView)
			{
				_navigation.RemovePage(item);
				break;
			}
		}

		foreach (var item in _navigation.NavigationStack)
		{
			if (item.GetType().Name == App.ZakatExemptionRequestListPageView)
			{
				_navigation.RemovePage(item);
				break;
			}
		}
		foreach (var item in _navigation.NavigationStack)
		{
			if (item.GetType().Name == App.ZakatExemptionSuccessPage)
			{
				_navigation.RemovePage(item);
				break;
			}
		}
		viewModel._navigationService.NavigateTo(App.ZakatExemptionRequestListPageView);
	}

	async void OnCopyReferenceNumberButtonClicked(System.Object sender, System.EventArgs e)
	{
		try
		{
			if (viewModel.zakatExemptionresponse.D.Fbnumz != null)
			{
				await Clipboard.SetTextAsync(viewModel.zakatExemptionresponse.D.Fbnumz);
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