using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.Models.TINOutletDeregister;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TINOutletDeregister;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.TINOutletDeregister;

public partial class TinOutletDeRegRequestPageView : ContentPage
{
	TinOutletDeRegRequestViewModel viewModel;

	public TinOutletDeRegRequestPageView(TinOutletPrevousRequestsModel.PreviousRequests requestCode)
	{
		InitializeComponent();
		try
		{
			viewModel = App.Locator.TinOutletDeRegRequestPageView;
			this.BindingContext = viewModel;
			this.FlowDirection = FlowDirection.LeftToRight;
			viewModel.SetDefaultDate();
			this.BindingContext = viewModel;
			InitializationPopups();
			SetViewsToDefault();

			viewModel.LoadReasonSet();

			int reqCode = 0;
			if (requestCode != null && requestCode.TxnTp != null && !string.IsNullOrEmpty(requestCode.TxnTp))
			{
				if (requestCode.TxnTp.Equals("DEREG_T"))
				{
					reqCode = 1;
				}
				else if (requestCode.TxnTp.Equals("DEREG_O"))
				{
					reqCode = 2;
				}
				else if (requestCode.TxnTp.Equals("DEREG_P"))
				{
					reqCode = 3;
				}
			}
			if (reqCode != 0)
			{
				viewModel.GetTinOutletDeregistrationDataBeforeNewRequest(reqCode, requestCode.Fbguid);
			}


		}
		catch (Exception)
		{

		}
	}




	private void SetViewsToDefault()
	{
		viewModel.OutlettUiList = new ObservableCollection<ContactInfo_NestedListView>();
		viewModel.SelectedOutletItem = new ContactInfo_NestedListView();
		viewModel.SelectedPermitItem = new ViewModel.SyncFusionEnabledViewModel.TINOutletDeregister.DetailsContactInfo();
		viewModel.SelectedDeRegType = string.Empty;
		viewModel.SelectedDeRegReason = string.Empty;
		viewModel.ShouldShowDeregReason = false;
		viewModel.EnableDeregReason = true;
		viewModel.EnableDeregType = true;
		viewModel.NewMainOutlet = string.Empty;
		viewModel.ShowMainOutletDropDown = false;
	}


	protected override void OnDisappearing()
	{
		base.OnDisappearing();

		MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelected");

	}

	private void InitializationPopups()
	{
		MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelected", (sender, arg) =>
		{
			// viewModel.PickerModelExcemptionYear = arg;

			var selectedType = string.Empty;
			string SelectedIDTypeValue = string.Empty;
			if (arg.PickerId == "DeregTypePicker")
			{
				viewModel.SelectedDeRegType = arg.SelectedValue;
				if (viewModel.SelectedDeRegType.Equals(AppResources.TinDeregistration))
				{
					viewModel.ShouldShowDeregReason = true;
					viewModel.ShowMainOutletDropDown = false;
					viewModel.DeregistrationType = 1;
					Task.Delay(500);
					Task.Run(() => this.viewModel.GetTinOutletDeregistrationDataBeforeNewRequest(1)).Wait();

				}
				else
				{
					viewModel.ContinueButtonEnabled = true;
					viewModel.DeregistrationType = 2;
					Task.Delay(500);
					Task.Run(() => this.viewModel.GetTinOutletDeregistrationDataBeforeNewRequest(2)).Wait();
				}



			}
			else if (arg.PickerId == "DeregReasonPicker")
			{
				viewModel.ContinueButtonEnabled = true;
				viewModel.SelectedDeRegReason = arg.SelectedValue;
			}
			else if (arg.PickerId == "OutletReasonPicker")
			{
				viewModel.UpdateOutletReason(arg.SelectedValue);
			}
			else if (arg.PickerId == "PermitReasonPicker")
			{
				viewModel.UpdatePermitReason(arg.SelectedValue);
			}
			else if (arg.PickerId == "OutletMainPicker")
			{
				viewModel.NewMainOutlet = arg.SelectedValue;
			}
		});

		MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
		{
			if (arg != null)
			{
				viewModel.PopulateAttachments(arg.results);
			}
		});


	}

	void LicenseCloseTransferDateCliccked(System.Object sender, System.EventArgs e)
	{

		TxDateNormalCalendar.IsOpen = true;
	}


	private void TxDateNormalCalendarOutlet_Closed(object sender, EventArgs e)
	{
		try
		{


			if (TxDateNormalCalendarOutlet.SelectedItem != null)
			{
				var selectedItem = TxDateNormalCalendarOutlet.SelectedItem as ObservableCollection<object>;
				string month = selectedItem[1].ToString();
				string day = selectedItem[0].ToString();
				string year = selectedItem[2].ToString();

				viewModel.TransferCloseDateOutlet = ConvertToDate(day + "/" + month + "/" + year); ;
				viewModel.updateOutletTransferCloseDate();
			}


			TxDateNormalCalendarOutlet.IsOpen = false;


        }
		catch (Exception)
		{
		}

	}

	private void TxDateNormalCalendar_Closed(object sender, EventArgs e)
	{
		try
		{


			if (TxDateNormalCalendar.SelectedItem != null)
			{
				var selectedItem = TxDateNormalCalendar.SelectedItem as ObservableCollection<object>;
				string month = selectedItem[1].ToString();
				string day = selectedItem[0].ToString();
				string year = selectedItem[2].ToString();

				viewModel.TransferCloseDate = ConvertToDate(day + "/" + month + "/" + year);
				viewModel.updatePermitTransferCloseDate();
			}


			TxDateNormalCalendar.IsOpen = false;


        }
		catch (Exception ex)
		{
			
			
		}

	}

	private DateTime ConvertToDate(string date)
	{
		// Output: 10/22/2015 12:00:00 AM
		DateTime dateTime16 = DateTime.ParseExact(date, new string[] { "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
		return dateTime16;
	}

	private void TxDateHijriCalendarOutlet_Closed(object sender, EventArgs e)
	{
		try
		{

			if (TxDateHijriCalendarOutlet.SelectedItem != null)
			{
				var selectedItem = TxDateHijriCalendarOutlet.SelectedItem as ObservableCollection<object>;
				string month = selectedItem[1].ToString();
				string day = selectedItem[0].ToString();
				string year = selectedItem[2].ToString();

				viewModel.TransferCloseDateOutlet = day + "/" + month + "/" + year;
				viewModel.updateOutletTransferCloseDate();
			}



		}
		catch (Exception)
		{
		}
	}

	private void TxDateHijriCalendar_Closed(object sender, EventArgs e)
	{
		try
		{


			if (TxDateNormalCalendar.SelectedItem != null)
			{
				var selectedItem = TxDateNormalCalendar.SelectedItem as ObservableCollection<object>;
				string month = selectedItem[1].ToString();
				string day = selectedItem[0].ToString();
				string year = selectedItem[2].ToString();

				viewModel.TransferCloseDate = day + "/" + month + "/" + year;
				viewModel.updatePermitTransferCloseDate();
			}

			if (TxDateHijriCalendar.SelectedItem != null)
			{
				var selectedItem = TxDateHijriCalendar.SelectedItem as ObservableCollection<object>;
				string month = selectedItem[1].ToString();
				string day = selectedItem[0].ToString();
				string year = selectedItem[2].ToString();

				viewModel.TransferCloseDate = day + "/" + month + "/" + year;
				viewModel.updatePermitTransferCloseDate();
			}



		}
		catch (Exception ex)
		{
			
			
		}

	}



	void Outlet_CheckChanged(System.Object sender, System.Boolean e)
	{
		var checkbox = (Image)sender;
		viewModel.SelectedOutletItem = checkbox.BindingContext as ContactInfo_NestedListView;
		if (viewModel.SelectedOutletItem.IsOutletChecked == true)
		{
			viewModel.CheckIsMainOutletSelectedAsync(viewModel.SelectedOutletItem.AOutletNoTb);
		}
		else
		{
			viewModel.ShowMainOutletDropDown = false;
		}
		viewModel.SelectAllPermitsOfOutlets(viewModel.SelectedOutletItem);
	}

	void Permit_CheckChanged(System.Object sender, System.Boolean e)
	{
		var checkbox = (Image)sender;
		viewModel.OnPermitChecked(checkbox.BindingContext as DetailsContactInfo);
	}
}