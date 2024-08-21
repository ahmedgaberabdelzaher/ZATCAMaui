using System.Collections.ObjectModel;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.Common;

namespace ZATCAMAUI.Views.NewDesign.ZakatExemptionRequest;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AttachmentView : ContentView
{

	AttachmentViewModel viewModel;

	int SelectedAttachmentNumber = 0;
	public string ReturnId = "";
	public string Title = "";
	Action<string, bool> update;

	public AttachmentView(string title, string id, Action<string, bool> update)
	{
		viewModel = App.Locator.AttachmentViewModel;
		BindingContext = viewModel;
		InitializeComponent();
		Title = title;
		ReturnId = id;
		this.update = update;
		setData();
	}

	void setData()
	{
		label_title.Text = Title;
		viewModel.AttachmentsListViewData = new ObservableCollection<Attachment>();
		viewModel.AttachmentsListViewData.Clear();

		MessagingCenter.Subscribe<object, FilesUploadPopUpViewModel>(this, "ZakatAttachmentReceived", (sender, arg) =>
		{
			if (ReturnId == arg.returnIdz && arg != null && arg.AttachmentsList != null && arg.AttachmentsList.results != null)
			{
				viewModel.AttachmentsListViewData.Clear();
				foreach (Attachment attachment in arg.AttachmentsList.results)
				{
					viewModel.AttachmentsListViewData.Add(attachment);
				}
				if (viewModel.AttachmentsListViewData != null && viewModel.AttachmentsListViewData.Count > 0)
				{
					update(ReturnId, true);
				}
			}
		});
	}

	private void SfListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
	{
		NewCompanyArticalsAttachmentsPopup();
	}

	public async void NewCompanyArticalsAttachmentsPopup()
	{
		if (MopupService.Instance.PopupStack.Count > 0) return;
		if (viewModel.AttachmentsListViewData == null)
		{
			viewModel.AttachmentsListViewData = new ObservableCollection<Attachment>();
		}

		try
		{
			SelectedAttachmentNumber = (int)WhichAttachment.ZakatExemtionDynamicAttachment;
			await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
				viewModel.AttachmentsListViewData.ToList(),
				WhichAttachment.ZakatExemtionDynamicAttachment,
				ReturnId));

		}
		catch (Exception)
		{
		}
	}

	private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
		NewCompanyArticalsAttachmentsPopup();
	}


}