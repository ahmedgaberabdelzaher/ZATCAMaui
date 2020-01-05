using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class VATLookupPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ICommand OnCaptchaRegenerateClicked { get; set; }

        public ICommand OnSubmitClicked { get; set; }

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
                RaisePropertyChanged("Parameter");
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
                RaisePropertyChanged("Captcha");
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
                RaisePropertyChanged("EnteredCaptchaValue");
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
                RaisePropertyChanged("ParameterTypeList");


            }
        }

        private VATParameterType _selectedParameterType;
        public VATParameterType SelectedParameterType
        {
            get
            {
                return _selectedParameterType;
            }
            set
            {
                _selectedParameterType = value;
                RaisePropertyChanged("_selectedParameterType");
                if (SelectedParameterType != null)
                {

                    SetLayoutVisibilityForSelectedParameterType();
                }
                else
                {
                }


            }
        }
        private string _VATACCOrCRNOOrVATCER = AppResources.ZPleaseentertheVATAccountNocomposedof15digits;
        public string VATACCOrCRNOOrVATCER
        {
            get
            {
                return _VATACCOrCRNOOrVATCER;
            }
            set
            {
                _VATACCOrCRNOOrVATCER = value;
                RaisePropertyChanged("VATACCOrCRNOOrVATCER");
            }
        }

        private string _finalResult = "";
        public string FinalResult
        {
            get
            {
                return _finalResult;
            }
            set
            {
                _finalResult = value;
                RaisePropertyChanged("FinalResult");
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
                RaisePropertyChanged("LookupNumber");
            }
        }

        private void SetLayoutVisibilityForSelectedParameterType()
        {
            if (SelectedParameterType.id.Equals("1"))
            {
                VATACCOrCRNOOrVATCER = AppResources.ZPleaseentertheVATAccountNocomposedof15digits;
               

            }
            else if(SelectedParameterType.id.Equals("2"))
            {
                VATACCOrCRNOOrVATCER = AppResources.ZPleaseentertheCRcomposedof10digits;
                //IsTaxPayerTypeEnable = true;
                //IsForgotPassword = false;
                //IsForgotUserNameWithIndividual = true;
                //IsForgotUserNameWithCorporate = false;
            }
            else
            {
                VATACCOrCRNOOrVATCER = AppResources.ZPleaseentertheVATAccountNocomposedof15digits;
            }

        }

        public VATLookupPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            OnCaptchaRegenerateClicked = new Command(async () =>
            {
                StringBuilder captcha = GetCaptcha();
                Captcha = captcha.ToString();
            });
            OnSubmitClicked = new Command(async () =>
            {

                if (SelectedParameterType != null)
                {
                    if (LookupNumber != null && LookupNumber != "")
                    {
                        if (SelectedParameterType.id.Equals("1"))
                        {
                            if (LookupNumber.Length != 15)
                            {
                                await _dialogService.ShowMessageBox(AppResources.ZVATNumberisnotequalto15, AppResources.Information);
                                return;
                            }

                        }
                        else if (SelectedParameterType.id.Equals("2"))
                        {
                            if (LookupNumber.Length != 10)
                            {
                                await _dialogService.ShowMessageBox(AppResources.ZCRNumberisnotequalto10, AppResources.Information);
                                return;
                            }

                        }
                        else if (SelectedParameterType.id.Equals("3"))
                        {
                            if (LookupNumber.Length != 15)
                            {
                                await _dialogService.ShowMessageBox(AppResources.ZVATCerNumberisnotequalto15, AppResources.Information);
                                return;
                            }

                        }
                        bool isValiedCaptcha = ValidateCaptcha();
                        if (!isValiedCaptcha)
                        {
                            await _dialogService.ShowMessageBox(AppResources.enteredcaptchacodeisincorrect, AppResources.Information);
                            return;
                        }
                    }
                    else
                    {
                        _dialogService.ShowMessageBox(AppResources.ZPleaseselectparametertype, AppResources.Information);
                        return;
                    }
                }

                else
                {
                    _dialogService.ShowMessageBox(AppResources.ZPleaseenterlookupnumber, AppResources.Information);
                    return;
                }
                
                _dialogService.ShowMessageBox(AppResources.ZVATLookupDialogue, AppResources.Information);
                FinalResult = AppResources.ZVATLookupDialogue;

            });
        }

        public bool ValidateCaptcha()
        {
            bool isValidCaptcha = false;
            isValidCaptcha = EnteredCaptchaValue.Equals(Captcha);
            if (EnteredCaptchaValue.Equals(Captcha))
            {
                isValidCaptcha = true;
            }
            else
            {
                // _dialogService.ShowMessageBox(AppResources.InvaliedCaptcha, AppResources.Information);

                StringBuilder captcha = GetCaptcha();
                Captcha = captcha.ToString();
                isValidCaptcha = false;
            }
            return isValidCaptcha;
        }





        public async Task OnPageLoad()
        {
            StringBuilder captcha = GetCaptcha();
            Captcha = captcha.ToString();
            List<VATParameterType> VATParameterList = new List<VATParameterType>
            {
                new VATParameterType{ id = "1" , ParameterType = AppResources.ZVATAccountNumber},
                new VATParameterType{ id = "2" , ParameterType = AppResources.ZCRNumber},
                new VATParameterType{ id = "3" , ParameterType = AppResources.ZVATCertificateNumber}
            };
            ParameterTypeList = VATParameterList;
            FinalResult = "";
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
    }
}
