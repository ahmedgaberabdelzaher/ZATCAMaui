using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

namespace ZATCAMAUI.Views.NewDesign.ChangeMobile;

public partial class NafathChangeMobileNumberView : ContentPage
{
	NafathChangeMobileNumberViewModel viewModel;
	public NafathChangeMobileNumberView(Dictionary<string, string> guid)
	{
		InitializeComponent();
		viewModel = App.Locator.NafathChangeMobileNumberViewModel;
		viewModel.OnAppearing();
		BindingContext = viewModel;
		updatePickerSelection();
		getData(guid);
	}


	void updatePickerSelection()
	{
		MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
		{
			viewModel.TinsPickerModel = arg;
			viewModel.UpdatePickerSelection();
		});
	}

	async Task getData(Dictionary<string, string> guid)
	{
		if (guid != null && guid.Count > 0)
		{
			var e = guid.First();
			viewModel.GUID = e.Value;
			await NafathChangeMobileNumber(e.Key, e.Value);
		}

	}

	async Task NafathChangeMobileNumber(string idNumber, string guid)
	{
		NafathChangeMobileNumberModel model = new NafathChangeMobileNumberModel()
		{
			Guid = string.Empty,
			Idnumber = idNumber,
			Lang = "EN",
			Scrid = string.Empty,
			Chmb = string.Empty,
			Str = string.Empty,
			GuidNf = guid,
			mobileExtensions = new List<MOB_EXTENSet>(),
			taxpayerRegistrationTypes = new List<TP_REGTYPSet>()
		};
		var response = await WebServiceManager.NafathChangeMobileNumber(model);
		BindTelephoneCodes(response);
	}

	void BindTelephoneCodes(NafathChangeMobileNumberModelResponse response)
	{
		if (response != null && response.d != null)
		{
			if (string.IsNullOrEmpty(response.d.messageDescription))
			{
				if (response.d.mobileExtensions!.Count() > 0)
				{
					var codes = new System.Collections.ObjectModel.ObservableCollection<InternationalMobileData>();
					foreach (var code in response.d.mobileExtensions)
					{
						codes.Add(new InternationalMobileData()
						{
							Telefto = code.Telefto,
							Landx = code.Land1,
							Land1 = code.Land1
						});
					}
					viewModel.CountryCodesList = codes;
				}

				if (response.d.taxpayerRegistrationTypes!.Count() > 0)
				{
					var TINs = new Dictionary<string, string>();
					foreach (var tin in response.d.taxpayerRegistrationTypes)
					{
						TINs[tin.Partner] = tin.Guid;
					}
					viewModel.TINs = TINs;
				}
			}
			else
			{
				viewModel._dialogService.ShowMessage(response.d.messageDescription, AppResources.Information, AppResources.OKText, delegate ()
				{
					viewModel._navigationService.GoBack();
					viewModel._navigationService.GoBack();
				});
			}
		}
		else
		{
			viewModel._dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information, AppResources.OKText, delegate ()
			{
				viewModel._navigationService.GoBack();
				viewModel._navigationService.GoBack();
			});
		}
	}


}