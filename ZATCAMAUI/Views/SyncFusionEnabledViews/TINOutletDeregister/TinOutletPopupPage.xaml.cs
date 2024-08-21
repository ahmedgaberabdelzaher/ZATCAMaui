using Mopups.Pages;
using Mopups.Services;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.TINOutletDeregister;

public partial class TinOutletPopupPage : PopupPage
{
	public TinOutletPopupPage()
	{
		InitializeComponent();
		List<string> items = new List<string> { AppResources.TinDeReg, AppResources.OutletDeReg };
		sampleList.ItemsSource = items;
	}
	void sampleList_ItemTapped(System.Object sender, ItemTappedEventArgs e)
	{
		if (e.ItemIndex == 0)
		{
			// await TINDeregistrationWebServiceManager.GetOutletDeRegisterNewRequest(1);
		}
		else
		{
			MessagingCenter.Send<Object, string>(this, "OutletDeregTappedforAcc", e.Item as string);
		}
		MopupService.Instance.PopAsync();
	}

}