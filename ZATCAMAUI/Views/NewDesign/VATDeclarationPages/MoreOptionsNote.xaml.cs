using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using Mopups.Pages;
using Mopups.Services;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATDeclarationPagesVM;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AttachmentPage;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MoreOptionsNote : PopupPage
{
	#region Variable
	public MoreOptionsVIewModel viewModel;
	#endregion
	VATDeclaration vatDec;
	VATAttachment attachment;
	public static string AttachmentName = string.Empty;
	string downloadFilePath;
	private VATDeclaration vATDeclarationResponse;

	public MoreOptionsNote(VATDeclaration vATDeclaration)
	{

		InitializeComponent();
		viewModel = App.Locator.vIewModelOptions;
		BindingContext = viewModel;
		vatDec = vATDeclaration;
		vATDeclarationResponse = vATDeclaration;
		MoreOptionsVIewModel.IsComingFromNotePage = true;
		viewModel.NoteText = string.Empty;
		if (vATDeclarationResponse.data.ATTACHSet != null)
		{

			viewModel.AttachmentCount = vATDeclarationResponse.data.ATTACHSet.Count;
		}
		if (vATDeclaration.data.PendingIbanMsg != "")
		{
			MopupService.Instance.PushAsync(new AttachmentInformationPopUp(vATDeclaration.data.PendingIbanMsg));
		}

		if (vATDeclaration.data.Cr2215 != null && vATDeclaration.data.Cr2215.Equals("X"))
		{
			viewModel.CR2215flag = vATDeclaration.data.Cr2215;
			// PopUpPageView.CloseWhenBackgroundIsClicked = false;
		}

		if (viewModel.CR2215flag != null && viewModel.CR2215flag.Equals("X"))
		{
			viewModel.IsAttachEnabled = true;
		}
		else
		{
			viewModel.IsAttachEnabled = true;
		}

		if (vATDeclaration != null && vATDeclaration != null)
		{
			viewModel.VATDeclarationData = vATDeclaration;
			if (App.ICRStatus == "E0001" && NotesPopUpPageViewModel.NoteCount == 0)
			{
				viewModel.NoteText = string.Empty;
				NotesPopUpPageViewModel.NoteCount++;
			}
			else
			{
				if (App.ICRStatus == "E0001")
				{
					if (viewModel.VATDeclarationData.data.NOTESSet.Count != 0)
					{
						viewModel.NoteText = viewModel.VATDeclarationData.data.NOTESSet.Where(x => x.DataVersionz == "00000").Select(x => x.Strline).FirstOrDefault();
						viewModel.PreviousNoteText = viewModel.NoteText;
					}
				}
			}
			if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057" || App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
			{
				if (viewModel.VATDeclarationData.data.NOTESSet.Count != 0)
				{
					if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
					{
						if (GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsFirstTimeForNote == true)
						{
							viewModel.NoteText = string.Empty;
							viewModel.PreviousNoteText = viewModel.NoteText;

						}
						else
						{
							viewModel.NoteText = viewModel.VATDeclarationData.data.NOTESSet.Where(x => x.DataVersionz == "00000").Select(x => x.Strline).FirstOrDefault();
							viewModel.PreviousNoteText = viewModel.NoteText;
						}

					}
					else
					{
						viewModel.NoteText = viewModel.VATDeclarationData.data.NOTESSet.Where(x => x.DataVersionz == "00000").Select(x => x.Strline).FirstOrDefault();
						viewModel.PreviousNoteText = viewModel.NoteText;
					}
				}
			}
		}




		List2.ItemTapped += (object sender, ItemTappedEventArgs e) =>
		{
			// don't do anything if we just de-selected the row.
			if (e.Item == null) return;
			if (sender is ListView lv) lv.SelectedItem = null;
		};
		try
		{
			viewModel.VatAttachmentsList = null;
			viewModel.ClearData();
			if (vATDeclaration.data.ATTACHSet != null && vATDeclaration.data.ATTACHSet != null && vATDeclaration.data.ATTACHSet.Count > 0)
				viewModel.NumberOfAttachmentComingFromServer = GAZTNewDesignMyReturnsNewPageViewModel.numberOfAttachmentComingFromServer;// vATDeclaration.d.ATTACHSet.results.Count;
			viewModel.TotalAttachmentSize = AttachmentPageViewModel.AttachmentUploadedSize;
			viewModel.IsAmendClickedOnVAT = GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsAmend;
			if (vATDeclaration != null && vATDeclaration.data != null)
			{
				viewModel.VATDeclarationDataForAttch = vATDeclaration;
				if (viewModel.VATDeclarationDataForAttch.data.ATTACHSet.Count != 0)
				{
					ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATDeclarationDataForAttch.data.ATTACHSet as List<Attachment>);
					viewModel.VatAttachmentsList = myCollection;
					int AttachmentCount = 0;
					foreach (var item in viewModel.VatAttachmentsList)
					{
						// item.ColorOf = (Color)App.Current.Resources["SecondaryNew"];


						if (!App.ICRStatus.Equals("E0001"))
						{
							if (AttachmentCount < GAZTNewDesignMyReturnsNewPageViewModel.numberOfAttachmentComingFromServer)
							{
								AttachmentCount++;
								if (item.Erfdt != null)
								{
									item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
									item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
								}
							}

						}

					}
					if (App.ICRStatus.Equals("E0045"))
					{
						viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
					}
					else
					{
						viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
					}
				}
			}
			viewModel.OnPageLoad();


		}
		catch (Exception ex)
		{

		}

	}

	private async void OnDeleteAttachmentClicked(object sender, EventArgs e)
	{
		try
		{
			if ((App.ICRStatus.Equals("E0045") && viewModel.IsAmendClickedOnVAT == false))
			{

			}
			else if ((App.ICRStatus.Equals("E0045") && viewModel.IsAmendClickedOnVAT == true))
			{
				try
				{
					Image arrowImage = sender as Image;
					attachment = (VATAttachment)arrowImage.BindingContext;
					if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
					{
						int indexToReduceTheSize = viewModel.GetDeletedAttachmentIndex(attachment);
						if (indexToReduceTheSize > viewModel.NumberOfAttachmentComingFromServer - 1)
						{
							if (attachment != null)
							{

								AttachmentName = attachment.Filename;
								await MopupService.Instance.PushAsync(new ZAKATOkCancelPopUpView("DeleteVATAttachment"));

							}
						}
					}


				}
				catch (Exception)
				{

				}


			}
			else
			{
				try
				{
					Image arrowImage = sender as Image;
					attachment = (VATAttachment)arrowImage.BindingContext;

					if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
					{
						if (attachment != null)
						{

							await MopupService.Instance.PushAsync(new ZAKATOkCancelPopUpView("DeleteVATAttachment"));
						}
					}
					else
					{
						await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZVATReturnsReadOnly));
					}

				}
				catch (Exception)
				{

				}

			}

		}
		catch (InternetException ex)
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));


			});
		}
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		getYesCommandToDeleteTheAttachment();

	}

	public void getYesCommandToDeleteTheAttachment()
	{
		try
		{
			MessagingCenter.Subscribe<object, string>(this, "YesCommandToDeleteVATAttachment", async (sender, arg) =>
			{
				if (attachment != null)
				{
					await DeleteAttachment(attachment);
				}
			});
		}
		catch (Exception ex)
		{

		}
	}

	protected override bool OnBackButtonPressed()
	{
		return true;
	}
	protected override bool OnBackgroundClicked()
	{
		return true;
	}
	private async void OnClosedTapped(object sender, EventArgs e)
	{
		try
		{
			MessagingCenter.Send<Object, string>(this, "ClearNoteForVATDeclaration", "ClearNoteForVATDeclaration");
			await MopupService.Instance.PopAsync();

			if (viewModel.isUploadHappened)
				if (vATDeclarationResponse.data.Cr2215 != null && vATDeclarationResponse.data.Cr2215.Equals("X"))
				{
					viewModel.IsLoading = true;
					vATDeclarationResponse.data.ATTACHSet.Clear();
					string NotifyResponse = await WebServiceManager.SendNotificationToAuditorafterUploadingAttachments(viewModel.VATDeclarationDataForAttch.data);
					viewModel.isUploadHappened = false;
					viewModel.IsLoading = false;
				}
		}
		catch (Exception)
		{
		}
	}


	public async Task DeleteAttachment(VATAttachment attachment)
	{
		try
		{
			await Task.Run(() =>
			{
				viewModel.IsLoading = true;
			});
			await Task.Run(() =>
			{

				int indexToReduceTheSize = viewModel.GetDeletedAttachmentIndex(attachment);
				string results = WebServiceManager.GAZTDeleteVATDeclarationAttachment(attachment.Filename, attachment.Doguid);
				PopToRootPage();
				if (results == "X")
				{

					viewModel.AttachmentCount--;
					Attachment listitem = (from itm in viewModel.VatAttachmentsList
										   where itm.Doguid == attachment.Doguid.ToString()
										   select itm)
									.FirstOrDefault<Attachment>();

					VATAttachment listitemTwo = (from itm in viewModel.AttachmentList
												 where itm.Doguid == attachment.Doguid.ToString()
												 select itm)
									.FirstOrDefault<VATAttachment>();

					viewModel.VatAttachmentsList.Remove(listitem);
					viewModel.AttachmentList.Remove(listitemTwo);
					viewModel.VATDeclarationDataForAttch.data.ATTACHSet.Remove(listitem);
					if (indexToReduceTheSize != -1)
						viewModel.ReduceTotalAttachmentSize(indexToReduceTheSize);
				}

			});
			await Task.Run(() =>
			{
				viewModel.IsLoading = false;
			});
		}
		catch (Exception ex)
		{
		}
	}
	public void PopToRootPage()
	{
		if (App.IsSessionExpired)
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				var _navigation = Application.Current.MainPage.Navigation;
				await _navigation.PopToRootAsync();
			});
		}
	}
	private async void OnDownloadAttachmentClicked(object sender, EventArgs e)
	{
		try
		{
			Image arrowImage = sender as Image;
			VATAttachment attachment = (VATAttachment)arrowImage.BindingContext;

			viewModel.VATDeclarationDataForAttch = this.vatDec;

			String retGuid = attachment.RetGuid;
			String fbNum = viewModel.VATDeclarationDataForAttch.data.Fbnum;

			viewModel.IsLoading = true;
			Models.AttachmentDocumentModel attachmentDocumentModel = await WebServiceManager.GAZTGetAllAttachments(retGuid, fbNum);

			foreach (Models.AttachmentResult tempAttachmentDocumentModel in attachmentDocumentModel.D)
			{

				if (attachment.Filename == tempAttachmentDocumentModel.Filename)
				{
					var platform = DeviceInfo.Platform;
					if (DeviceInfo.Platform == DevicePlatform.iOS)
					{
						downloadFilePath = WriteFileToPath(tempAttachmentDocumentModel.Filename, tempAttachmentDocumentModel.Content);

						viewModel.IsLoading = false;
						var downloadDirectoryFilePath = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentToDownloadsPath(tempAttachmentDocumentModel.Filename, downloadFilePath);


					}
					else
					{
						//Activity Indicator while downloading
						//Once download is completed you have to tell the user through an alert that download is completed and check in download folder.
						if (tempAttachmentDocumentModel.Filename.Contains(""))
						{
							try
							{

								var downloadDirectoryFilePath = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentToDownloadsPath(tempAttachmentDocumentModel.Filename, tempAttachmentDocumentModel.Content);

							}
							catch (Exception ex)
							{

							}
						}
						else
						{
							throw new GAZTNetworkConnectivityIssueException();
						}

						viewModel.IsLoading = false;
						MainThread.BeginInvokeOnMainThread(async () =>
						{
							await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZDownloadAttachmentMessg));
						});
					}
				}
			}
		}
		catch (Exception ex)
		{

		}
	}

	public string PathToFolder(string fileName, string folderName)
	{
		try
		{
			string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Images", "temp");

			Directory.CreateDirectory(pathToNewFolder);
			string pathToNewFile = Path.Combine(pathToNewFolder, fileName);


			return pathToNewFile;
		}
		catch
		{
			return null;
		}
	}

	public String WriteFileToPath(string fileName, string base64Data)
	{
		try
		{
			string getFilePath = PathToFolder(fileName, "GAZTFiles");
			File.WriteAllBytes(getFilePath, Convert.FromBase64String(base64Data));

			return getFilePath;

		}
		catch (Exception ex)
		{
			return null;
		}
	}
	private async void Attachmentlist_ItemTapped(object sender, ItemTappedEventArgs e)
	{
		try
		{
			ListView Document = sender as ListView;
			VATAttachment attachment = (VATAttachment)Document.SelectedItem;
			//attachment.DocUrl;
			//if (attachment.Filename.Contains(".")) ;
			string Extention = attachment.Filename.Split('.')[1];
			if (Extention.Equals("PDF") || Extention.Equals("pdf"))
			{
				if (attachment.DocUrl != null)
				{
					await viewModel._navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
					await MopupService.Instance.PopAsync();
				}
			}
			else
			{
				await email(attachment.Doguid, attachment);
				await MopupService.Instance.PopAsync();
			}
			if (sender is ListView lv) lv.SelectedItem = null;
		}
		catch (Exception)
		{
		}
	}
	public async Task email(string doguid, VATAttachment attachment)
	{
		await Task.Run(() =>
		{
			viewModel.IsLoading = true;
		});
		await Task.Run(() =>
		{
			try
			{
				string attachmentURL = attachment.DocUrl;// "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/corr_dataSet(Cokey='" + doguid + "',Cotyp='VTA0')/$value?saml2=disabled";
				byte[] PdfBytes;
				HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(attachmentURL);
				WebResponse myResp = myReq.GetResponse();
				using (Stream streams = myResp.GetResponseStream())
				using (MemoryStream Ms = new MemoryStream())
				{
					int count = 0;
					do
					{
						byte[] buf = new byte[1024];
						count = streams.Read(buf, 0, 1024);
						Ms.Write(buf, 0, count);
					} while (streams.CanRead && count > 0);
					PdfBytes = Ms.ToArray();
				}
				var message = new EmailMessage
				{
					Subject = "Attached Form :",
				};
				var fn = attachment.Filename;
				var file = Path.Combine(FileSystem.CacheDirectory, fn);
				File.WriteAllBytes(file, PdfBytes);
				MainThread.BeginInvokeOnMainThread(async () =>
				{
					await Share.RequestAsync(new ShareFileRequest
					{
						Title = Title,
						File = new ShareFile(file)
					});
				});

			}
			catch (Exception)
			{
				viewModel.IsLoading = false;
			}
		});
		await Task.Run(() =>
		{
			viewModel.IsLoading = false;
		});
	}






}