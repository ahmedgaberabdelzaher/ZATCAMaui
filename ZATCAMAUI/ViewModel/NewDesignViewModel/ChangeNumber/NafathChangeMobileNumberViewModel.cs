using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
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

        public int _maxLength = 10;
        public int MaxLength
        {
            get { return _maxLength; }
            set
            {
                _maxLength = value;
                OnPropertyChanged(nameof(MaxLength));
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

        private string _countryCode = "+966";
        public string CountryCode
        {
            get { return _countryCode; }
            set
            {
                if (_countryCode == value) return;
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

        private async Task Next()
        {
            try
            {
                IsLoading = true;
                if (!Validate())
                {
                    NafathChangeMobileNumberSendOTPModel model = new NafathChangeMobileNumberSendOTPModel();
                    model.Partner = SelectedTIN;
                    model.Guid = GUID;
                    model.MobExten = CountryCode.Replace("+", "00");
                    model.MobNum = MobileNumber;
                    model.ErrorMsg = string.Empty;
                    model.SendResendOtp = "1";
                    model.Lang = WebServiceManager.GetLangZParameterAREN();
                    model.Scrid = Device.RuntimePlatform == Device.iOS ? "C3" : "C4";
                    var response = await WebServiceManager.NafathChangeMobileNumberSendOTP(model);
                    IsLoading = false;
                    if (response != null && response.d != null)
                    {
                       await _navigationService.NavigateTo(App.NafathChangeMobileNumberOTPView, response);
                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
                    }
                }
            }
            catch (GAZTErrorException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
            }
            finally
            {
                IsLoading = false;
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
                MaxLength = 15 - CountryCode.Length;
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
                TinError = AppResources.PleaseSelectTIN;
            }

            if (string.IsNullOrEmpty(CountryCode))
            {
                IsError = true;
                MobileNumberError = AppResources.PleaseSelectCountryCode;
            }

            else if (string.IsNullOrEmpty(MobileNumber))
            {
                IsError = true;
                MobileNumberError += AppResources.EnterNewMobileNumber;
            }

            else if (MobileNumber.Length != 9)
            {
                IsError = true;
                MobileNumberError += AppResources.ZZMobilenumberlengthcannotbelessthan9digits;
            }
            return IsError;
        }

        public void OnAppearing()
        {
            IsError = false;
            TinError = string.Empty;
            MobileNumberError = string.Empty;
            SelectedTIN = string.Empty;
            CountryCode = "+966";
            MobileNumber = string.Empty;
            TinsPickerModel = null;
            CountryCodesList = null;
            BindEvents();
        }


    }
}

