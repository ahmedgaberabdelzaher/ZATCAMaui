

using System.Text;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATLookupPage
{
    public class VATLookupPageViewModel : BaseViewModel
    {
        public ICommand OnCaptchaRegenerateClicked { get; set; }
        bool isMendatoryDataEntered = true;
        public ICommand OnSubmitClicked { get; set; }
        public ICommand OnHomeIconClicked { get; set; }
        public ICommand OnHomeButtonClicked { get; set; }
        public ICommand OnBackButtonClicked { get; set; }
        private bool _isTooltipEnableVisible = false;
        public bool IsTooltipEnableVisible
        {
            get
            {
                return _isTooltipEnableVisible;
            }
            set
            {
                _isTooltipEnableVisible = value;
                OnPropertyChanged("IsTooltipEnableVisible");
            }
        }
        
        private string _parameter;
        public string Parameter
        {
            get
            {
                return _parameter;
            }
            set
            {
                _parameter = value;
                OnPropertyChanged("Parameter");
            }
        }

        private Thickness _navigationCornerRadius;
        public Thickness NavigationCornerRadius
        {
            get
            {
                return _navigationCornerRadius;
            }
            set
            {
                _navigationCornerRadius = value;
                OnPropertyChanged("NavigationCornerRadius");
            }
        }


        private string _captcha;
        public string Captcha
        {
            get
            {
                return _captcha;
            }
            set
            {
                _captcha = value;
                OnPropertyChanged("Captcha");
            }
        }
        private string _enteredCaptchaValue;
        public string EnteredCaptchaValue
        {
            get
            {
                return _enteredCaptchaValue;
            }
            set
            {
                _enteredCaptchaValue = value;
                OnPropertyChanged("EnteredCaptchaValue");
            }
        }
        private string _name = "";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _nameOrNoResultLabel = "";
        public string NameOrNoResultLabel
        {
            get
            {
                return _nameOrNoResultLabel;
            }
            set
            {
                _nameOrNoResultLabel = value;
                OnPropertyChanged("NameOrNoResultLabel");
            }
        }
        private List<VATParameterType> _parameterTypeList;
        public List<VATParameterType> ParameterTypeList
        {
            get
            {
                return _parameterTypeList;
            }
            set
            {
                _parameterTypeList = value;
                OnPropertyChanged("ParameterTypeList");
            }
        }
        private VATParameterType _selectedParameterType = null;
        public VATParameterType SelectedParameterType
        {
            get
            {
                return _selectedParameterType;
            }
            set
            {
                _selectedParameterType = value;
                if (_selectedParameterType != null)
                {
                    SetSelectedParameterTypeData();
                    TxtSearchParameter = _selectedParameterType.ParameterType;
                    SetPlaceholderText();
                }
                OnPropertyChanged("SelectedParameterType");
            }
        }
        private VATParameterType _selectedParameterTypePrev = null;
        public VATParameterType SelectedParameterTypePrev
        {
            get
            {
                return _selectedParameterTypePrev;
            }
            set
            {
                _selectedParameterTypePrev = value;
                OnPropertyChanged("SelectedParameterTypePrev");
            }
        }
        private string _VATACCOrCRNOOrVATCER = "";// AppResources.ZPleaseentertheVATAccountNocomposedof15digits;
        public string VATACCOrCRNOOrVATCER
        {
            get
            {
                return _VATACCOrCRNOOrVATCER;
            }
            set
            {
                _VATACCOrCRNOOrVATCER = value;
                OnPropertyChanged("VATACCOrCRNOOrVATCER");
            }
        }
        private string _lookupNumber = "";
        public string LookupNumber
        {
            get
            {
                return _lookupNumber;
            }
            set
            {
                _lookupNumber = value;
                OnPropertyChanged("LookupNumber");
            }
        }
        private string _maxDigids = "15";
        public string MaxDigids
        {
            get
            {
                return _maxDigids;
            }
            set
            {
                _maxDigids = value;
                OnPropertyChanged("MaxDigids");
            }
        }
        private string _txtSearchParameter = string.Empty;
        public string TxtSearchParameter
        {
            get
            {
                return _txtSearchParameter;
            }
            set
            {
                _txtSearchParameter = value;
                OnPropertyChanged("TxtSearchParameter");
            }
        }
        private void SetPlaceholderText()
        {
            IsTooltipEnableVisible = true;
            if (SelectedParameterType.id.Equals("3"))
            {
                VATACCOrCRNOOrVATCER = AppResources.ZPleaseentertheVATAccountNocomposedof15digits;
                MaxDigids = "15";
            }
            else if (SelectedParameterType.id.Equals("2"))
            {
                VATACCOrCRNOOrVATCER = AppResources.ZPleaseentertheCRcomposedof10digits;
                MaxDigids = "10";
            }
            else
            {
                VATACCOrCRNOOrVATCER = AppResources.PleaseentertheVATCertificateNocomposedof15digits;
                MaxDigids = "15";
            }
        }
        public VATLookupPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnCaptchaRegenerateClicked = new Command( () =>
            {
                StringBuilder captcha = GetCaptcha();
                Captcha = captcha.ToString();
            });
            OnHomeIconClicked = new Command(() =>
            {
                if (App.TP != null)
                {
                    _navigationService.NavigateTo(App.SFLandingPageView);
                }
                else
                {
                    _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                }
            });
            OnHomeButtonClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });
            OnBackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });
            OnSubmitClicked = new Command(async () =>
            {
                await OnSubmitClick();
            });
        }
        public bool ValidateCaptcha()
        {
            bool isValidCaptcha = false;
            if (EnteredCaptchaValue != null)
            {
                isValidCaptcha = EnteredCaptchaValue.Equals(Captcha);
                if (EnteredCaptchaValue.Equals(Captcha))
                {
                    isValidCaptcha = true;
                }
                else
                {
                    StringBuilder captcha = GetCaptcha();
                    Captcha = captcha.ToString();
                    isValidCaptcha = false;
                }
            }
            return isValidCaptcha;
        }
        public void OnPageLoad()
        {
            try
            {
                EnteredCaptchaValue = string.Empty;
                LookupNumber = string.Empty;
                Name = string.Empty;
                VATACCOrCRNOOrVATCER = string.Empty;
                List<VATParameterType> VATParameterList = new List<VATParameterType>
            {
               new VATParameterType{ id = "3" , ParameterType = AppResources.ZZZTaxRegistrationNumber},
                new VATParameterType{ id = "2" , ParameterType = AppResources.ZVATLookupCRNumberType3},
                new VATParameterType{ id = "4" , ParameterType = AppResources.ZVATLookupIDVatCertificateNumberType2}
            };
                StringBuilder captcha = GetCaptcha();
                Captcha = captcha.ToString();
                ParameterTypeList = new List<VATParameterType>();
                ParameterTypeList = VATParameterList;
                SelectedParameterType = ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
                SelectedParameterTypePrev = ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
            }
            catch (Exception)
            {
            }
        }
        public StringBuilder GetCaptcha()
        {

            StringBuilder Captcha;
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                    captcha.Append(combination[random.Next(combination.Length)]);
                //Session["captcha"] = captcha.ToString();
                //imgCaptcha.ImageUrl = "~/Captcha/GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
                Captcha = captcha;
            }
            catch
            {
                throw;
            }
            return Captcha;
        }
        public async Task OnSubmitClick()
        {
            try
            {
                isMendatoryDataEntered = true;
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    ValidateFormData();//isMendatoryDataEntered
                    if (isMendatoryDataEntered)
                    {
                        isMendatoryDataEntered = true;
                        string _language = "A"; //UtilityManager.GetLanguageParameter();
                        VATLookUp vatLookUp = await WebServiceManager.GAZTGetVATLookUp(_language, SelectedParameterType.id, LookupNumber);
                        if (vatLookUp.d != null)
                        {
                            if (string.IsNullOrEmpty(vatLookUp.d.results[0].Description))// Provided condiotion as per Vinay, Description comes null when the there is no error while calling the API
                            {
                                NameOrNoResultLabel = AppResources.Name;
                                Name = vatLookUp.d.results[0].Name;
                            }
                            else
                            {
                                NameOrNoResultLabel = "";
                                Name = "";
                                MainThread.BeginInvokeOnMainThread(async () =>
                                 {
                                     await _dialogService.ShowMessageBox(vatLookUp.d.results[0].Description, AppResources.ZError);
                                 });
                            }
                        }
                        else
                        {
                            Name = vatLookUp.d.results[0].Name;
                            NameOrNoResultLabel = AppResources.Nodataavailable;
                        }
                    }
                    // IsLoading = true;
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            StringBuilder captcha = GetCaptcha();
            Captcha = captcha.ToString();
            EnteredCaptchaValue = string.Empty;
        }
        private void ValidateFormData()
        {
            try
            {
                if (SelectedParameterType != null)
                {
                    if (LookupNumber != null && LookupNumber != "")
                    {
                        if (SelectedParameterType.id.Equals("3"))
                        {
                            if (LookupNumber.Length != 15)
                            {
                                isMendatoryDataEntered = false;
                                MainThread.BeginInvokeOnMainThread(() =>
                                 {
                                     _dialogService.ShowMessageBox(AppResources.ZVATNumberisnotequalto15, AppResources.Information);
                                 });
                                return;
                            }
                        }
                        else if (SelectedParameterType.id.Equals("2"))
                        {
                            if (LookupNumber.Length != 10)
                            {
                                isMendatoryDataEntered = false;
                                MainThread.BeginInvokeOnMainThread(() =>
                                 {
                                     _dialogService.ShowMessageBox(AppResources.ZCRNumberisnotequalto10, AppResources.Information);
                                 });
                                return;
                            }
                        }
                        else if (SelectedParameterType.id.Equals("4"))
                        {
                            if (LookupNumber.Length != 15)
                            {
                                isMendatoryDataEntered = false;
                                MainThread.BeginInvokeOnMainThread(() =>
                                 {
                                     _dialogService.ShowMessageBox(AppResources.ZVATCerNumberisnotequalto15, AppResources.Information);
                                 });
                                return;
                            }
                        }
                        bool isValiedCaptcha = ValidateCaptcha();
                        if (!isValiedCaptcha)
                        {
                            isMendatoryDataEntered = false;
                            MainThread.BeginInvokeOnMainThread(() =>
                             {
                                 _dialogService.ShowMessageBox(AppResources.enteredcaptchacodeisincorrect, AppResources.Information);
                             });
                            return;
                        }
                    }
                    else
                    {
                        isMendatoryDataEntered = false;
                        MainThread.BeginInvokeOnMainThread(() =>
                         {
                             isMendatoryDataEntered = false;
                             _dialogService.ShowMessageBox(AppResources.ZPleaseenterthecorrespondingnumber, AppResources.Information);
                         });
                        return;
                    }
                }
                else
                {
                    isMendatoryDataEntered = false;
                    MainThread.BeginInvokeOnMainThread(() =>
                     {
                         _dialogService.ShowMessageBox(AppResources.ZPleaseselectparametertype, AppResources.Information);
                     });
                    return;
                }
            }
            catch (Exception)
            {
                isMendatoryDataEntered = false;
            }
        }
        public void SetSelectedParameterTypeData()
        {
            EnteredCaptchaValue = string.Empty;
            LookupNumber = string.Empty;
            Name = string.Empty;
            StringBuilder captcha = GetCaptcha();
            Captcha = captcha.ToString();
            if (SelectedParameterType != null)
            {
                TxtSearchParameter = SelectedParameterType.ParameterType;
                IsTooltipEnableVisible = true;
                SetPlaceholderText();
            }
        }
    }
}
