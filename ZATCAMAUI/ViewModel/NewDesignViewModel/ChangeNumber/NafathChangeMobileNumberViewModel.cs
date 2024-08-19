using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeNumber
{
	public class NafathChangeMobileNumberViewModel : BaseViewModel
    {
        public ICommand SnipperTappedCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand CountryCodesCommand { get; set; }

        private string _selectedTIN = "";
        public Dictionary<string, string> TINs { get; set; }

        public string GUID { get; set; }

        public string SelectedTIN
        {
            get { return _selectedTIN; }
            set
            {
                _selectedTIN = value;
                OnPropertyChanged(nameof(SelectedTIN));
            }
        }

        private GenericPickerModel _tinsPickerModel;
        public GenericPickerModel TinsPickerModel
        {
            get { return _tinsPickerModel; }
            set
            {
                _tinsPickerModel = value;
                OnPropertyChanged(nameof(TinsPickerModel));
            }
        }

        private ObservableCollection<InternationalMobileData> _countryCodesList = new ObservableCollection<InternationalMobileData>();
        public ObservableCollection<InternationalMobileData> CountryCodesList
        {
            get
            {
                return _countryCodesList;
            }
            set
            {
                if (_countryCodesList == value) return;

                _countryCodesList = value;
                OnPropertyChanged("CountryCodesList");
            }
        }

        private string _countryCode;
        public string CountryCode
        {
            get { return _countryCode; }
            set
            {
                _countryCode = value;
                OnPropertyChanged(nameof(CountryCode));
            }
        }

        private string _mobileNumber;
        public string MobileNumber
        {
            get { return _mobileNumber; }
            set
            {
                _mobileNumber = value;
                OnPropertyChanged(nameof(MobileNumber));
            }
        }

        private bool isError;
        public bool IsError
        {
            get { return isError; }
            set
            {
                isError = value;
                OnPropertyChanged(nameof(IsError));
            }
        }

        private string tinError;
        public string TinError
        {
            get { return tinError; }
            set
            {
                tinError = value;
                OnPropertyChanged(nameof(TinError));
            }
        }

        private string mobileNumberError = "";
        public string MobileNumberError
        {
            get { return mobileNumberError; }
            set
            {
                mobileNumberError = value;
                OnPropertyChanged(nameof(MobileNumberError));
            }
        }

        public NafathChangeMobileNumberViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            BindCommands();
            //BindEvents();
        }

        private void BindCommands()
        {
            SnipperTappedCommand = new Command(async () => await SnipperTapped());
            NextCommand = new Command(() => Next());
            CountryCodesCommand = new Command(() => { CountryCodesTapped(); });
        }

        async Task SnipperTapped()
        {
            await showTINsPickerDialog();
        }

        async Task showTINsPickerDialog()
        {
            if (TINs != null && TINs.Count > 0)
            {
                setTINsPickerModel();
                await MopupService.Instance.PushAsync(new PickerPageView(TinsPickerModel));
            }
        }

        void setTINsPickerModel()
        {
            List<string> tins = TINs.Keys.ToList();
            _tinsPickerModel = new GenericPickerModel
            {
                PickerData = tins,
                PickerTitle = AppResources.PleaseChooseTinNumber,
                PickerId = AppResources.PleaseChooseTinNumber,
            };
        }

        public void UpdatePickerSelection()
        {
            SelectedTIN = TinsPickerModel.SelectedValue;
        }

        private async void Next()
        {
            if (!Validate())
            {
                NafathChangeMobileNumberSendOTPModel model = new NafathChangeMobileNumberSendOTPModel();
                model.Partner = SelectedTIN;
                model.Guid = GUID;
                model.MobExten = CountryCode;
                model.MobNum = MobileNumber;
                model.ErrorMsg = string.Empty;
                model.SendResendOtp = "1";
                model.Lang = WebServiceManager.GetLangZParameterAREN();
                model.Scrid = string.Empty;
                var response = await WebServiceManager.NafathChangeMobileNumberSendOTP(model);
                if (response != null && response.d != null)
                {
                    _navigationService.NavigateTo(App.NafathChangeMobileNumberOTPView, response);
                }
            }
        }

        private void CountryCodesTapped()
        {
            MopupService.Instance.PushAsync(new InternationalCodeSearchPage(CountryCodesList));
        }


        private void BindEvents()
        {
            MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedItem", (sender, arg) =>
            {
                CountryCode = arg;
            });

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                SelectedTIN = arg.SelectedValue;
                GUID = TINs[SelectedTIN];
            });
        }

        internal void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<InternationalCodeSearchPage, string>(this, "SelectedCountryCode");
            MessagingCenter.Unsubscribe<InternationalCodeSearchPage, string>(this, "SelectedItem");
            MessagingCenter.Unsubscribe<InternationalCodeSearchPage, string>(this, "PickerSelectedItem");
        }

        private bool Validate()
        {
            IsError = false;
            TinError = string.Empty;
            MobileNumberError = string.Empty;

            if (string.IsNullOrEmpty(SelectedTIN))
            {
                IsError = true;
                TinError = "Please select TIN";
            }

            if (string.IsNullOrEmpty(CountryCode))
            {
                IsError = true;
                MobileNumberError = "Please select country code.";
            }

            else if (string.IsNullOrEmpty(MobileNumber))
            {
                IsError = true;
                MobileNumberError += "Please enter your mobile number.";
            }

            else if (MobileNumber.Length != 10)
            {
                IsError = true;
                MobileNumberError += "Mobile number should be 10 digit length.";
            }
            return IsError;
        }

        public void OnAppearing()
        {
            IsError = false;
            TinError = string.Empty;
            MobileNumberError = string.Empty;
            SelectedTIN = string.Empty;
            CountryCode = string.Empty;
            MobileNumber = string.Empty;
            TinsPickerModel = null;
            CountryCodesList = null;
            BindEvents();
        }


    }
}

