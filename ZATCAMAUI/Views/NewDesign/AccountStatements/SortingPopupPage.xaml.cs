using Mopups.Pages;
using Mopups.Services;
namespace ZATCAMAUI.Views.NewDesign.AccountStatements;

public partial class SortingPopupPage : PopupPage
{
	public SortingPopupPage()
	{
		InitializeComponent();
		List<string> items = new List<string> { AppResources.SortAcending, AppResources.SortDecending, AppResources.SortDefault };
		sampleList.ItemsSource = items;
	}
	void sampleList_ItemTapped(System.Object sender, ItemTappedEventArgs e)
	{
		MessagingCenter.Send<Object, string>(this, "SortingTappedforAcc", e.Item as string);
		MopupService.Instance.PopAsync();
	}

}