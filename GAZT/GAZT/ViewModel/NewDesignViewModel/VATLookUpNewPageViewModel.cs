using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class VATLookUpNewPageViewModel : BaseViewModel
    {
        #region proprety
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
                RaisePropertyChanged("SelectedParameterType");
            }
        }
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
                RaisePropertyChanged("IsTooltipEnableVisible");
            }
        }
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
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
                RaisePropertyChanged("Parameter");
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
                RaisePropertyChanged("Name");
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
                RaisePropertyChanged("NameOrNoResultLabel");
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
                RaisePropertyChanged("VATACCOrCRNOOrVATCER");
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
                RaisePropertyChanged("MaxDigids");
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
                RaisePropertyChanged("TxtSearchParameter");
            }
        }
        #endregion
        public VATLookUpNewPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }

        #region Methods
        public void OnPageLoad()
        {
            try
            {
                //EnteredCaptchaValue = string.Empty;
                //LookupNumber = string.Empty;
                //Name = string.Empty;
                VATACCOrCRNOOrVATCER = string.Empty;
                List<VATParameterType> VATParameterList = new List<VATParameterType>
            {//ZZZTaxRegistrationNumber
               //new VATParameterType{ id = "3" , ParameterType = AppResources.ZVATLookupIDTaxpayerTinType1},
               new VATParameterType{ id = "3" , ParameterType = AppResources.ZZZTaxRegistrationNumber},
                new VATParameterType{ id = "2" , ParameterType = AppResources.ZVATLookupCRNumberType3},
                new VATParameterType{ id = "4" , ParameterType = AppResources.ZVATLookupIDVatCertificateNumberType2}
            };
                
                ParameterTypeList = new List<VATParameterType>();
                ParameterTypeList = VATParameterList;
                SelectedParameterType = ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
            }
            catch (Exception ex)
            {
            }
        }
        public void SetSelectedParameterTypeData()
        {
            //EnteredCaptchaValue = string.Empty;
            //LookupNumber = string.Empty;
            //Name = string.Empty;
            //StringBuilder captcha = GetCaptcha();
            //Captcha = captcha.ToString();
            if (SelectedParameterType != null)
            {
                TxtSearchParameter = SelectedParameterType.ParameterType;
                IsTooltipEnableVisible = true;
                SetPlaceholderText();
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
        #endregion
    }
}
