using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
   public class RefundAccountPopupPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public RefundAccountPopupPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;
        }

        #region Properties
        private VATDeclaration _vATDeclarationDetails;
        public VATDeclaration VATDeclarationDetails
        {
            get
            {
                return _vATDeclarationDetails;
            }
            set
            {
                _vATDeclarationDetails = value;
                RaisePropertyChanged("VATDeclarationDetails");
            }
        }
        private IBANType _selectedIBANType;
        public IBANType SelectedIBANType
        {
            get
            {
                return _selectedIBANType;
            }
            set
            {
                _selectedIBANType = value;
                if (_selectedIBANType != null)
                {
                    SetIBANIdNumber();
                    TxtSelectedIBANType = _selectedIBANType.Text;
                }
                else
                {
                }
                RaisePropertyChanged("SelectedIBANType");
            }
        }
        private IBANType _selectedIBANTypePrev;
        public IBANType SelectedIBANTypePrev
        {
            get
            {
                return _selectedIBANTypePrev;
            }
            set
            {
                _selectedIBANTypePrev = value;
                //if (_selectedIBANType != null)
                //{
                //    SetIBANIdNumber();
                //    TxtSelectedIBANType = _selectedIBANType.Text;
                //}
                //else
                //{
                //}
                RaisePropertyChanged("SelectedIBANTypePrev");
            }
        }
        private List<IBANIDNumber> _iBANIDNumberList;
        public List<IBANIDNumber> IBANIDNumberList
        {
            get
            {
                return _iBANIDNumberList;
            }
            set
            {
                _iBANIDNumberList = value;
                //if (_iBANIDNumberList != null && _iBANIDNumberList.Count() != 0)
                //{
                //    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false)
                //    {
                //        IsEnableIBANIdNumber = false;
                //    }
                //    else
                //    {
                //        IsEnableIBANIdNumber = true;
                //    }
                //}
                //else
                //{
                //    IsEnableIBANIdNumber = false;
                //}
                RaisePropertyChanged("IBANIDNumberList");
            }
        }
        private IBANIDNumber _selectedIBANIDNumber;
        public IBANIDNumber SelectedIBANIDNumber
        {
            get
            {
                return _selectedIBANIDNumber;
            }
            set
            {
                _selectedIBANIDNumber = value;
                if (_selectedIBANIDNumber != null)
                {
                    TxtSelectedIBANIDNumber = _selectedIBANIDNumber.Idnumber;
                }
                RaisePropertyChanged("SelectedIBANIDNumber");
            }
        }
        private IBANIDNumber _selectedIBANIDNumberPrev;
        public IBANIDNumber SelectedIBANIDNumberPrev
        {
            get
            {
                return _selectedIBANIDNumberPrev;
            }
            set
            {
                _selectedIBANIDNumberPrev = value;
                //if (_selectedIBANIDNumber != null)
                //{
                //    TxtSelectedIBANIDNumber = _selectedIBANIDNumber.Idnumber;
                //}
                RaisePropertyChanged("SelectedIBANIDNumberPrev");
            }
        }
        private string _TxtSelectedIBANIDNumber;
        public string TxtSelectedIBANIDNumber
        {
            get
            {
                return _TxtSelectedIBANIDNumber;
            }
            set
            {
                _TxtSelectedIBANIDNumber = value;
                RaisePropertyChanged("TxtSelectedIBANIDNumber");
            }
        }
        private string _txtSelectedIBANType;
        public string TxtSelectedIBANType
        {
            get
            {
                return _txtSelectedIBANType;
            }
            set
            {
                _txtSelectedIBANType = value;
                RaisePropertyChanged("TxtSelectedIBANType");
            }
        }
        private string _selectedIban;
        public string SelectedIban
        {
            get
            {
                return _selectedIban;
            }
            set
            {
                _selectedIban = value;
                RaisePropertyChanged("SelectedIban");
            }
        }
        private bool _isRefundVisible = false;
        public bool IsRefundVisible
        {
            get
            {
                return _isRefundVisible;
            }
            set
            {
                _isRefundVisible = value;
                RaisePropertyChanged("IsRefundVisible");
            }
        }
        private bool _isVisibleDropdownForRefund;
        public bool IsVisibleDropdownForRefund
        {
            get
            {
                return _isVisibleDropdownForRefund;
            }
            set
            {
                _isVisibleDropdownForRefund = value;
                RaisePropertyChanged("IsVisibleDropdownForRefund");
            }
        }
        private bool _IsVisiblechkRefundDeclaration = false;
        public bool IsVisiblechkRefundDeclaration
        {
            get
            {
                return _IsVisiblechkRefundDeclaration;
            }
            set
            {
                _IsVisiblechkRefundDeclaration = value;
                RaisePropertyChanged("IsVisiblechkRefundDeclaration");
            }
        }
        private bool _isTextBoxVisibleForIban;
        public bool IsTextBoxVisibleForIban
        {
            get
            {
                return _isTextBoxVisibleForIban;
            }
            set
            {
                _isTextBoxVisibleForIban = value;
                RaisePropertyChanged("IsTextBoxVisibleForIban");
            }
        }
        private bool _isDropdownVisibleForIban;
        public bool IsDropdownVisibleForIban
        {
            get
            {
                return _isDropdownVisibleForIban;
            }
            set
            {
                _isDropdownVisibleForIban = value;
                RaisePropertyChanged("IsDropdownVisibleForIban");
            }
        }
        private bool _isMainButtonEnabled;
        public bool IsMainButtonEnabled
        {
            get
            {
                return _isMainButtonEnabled;
            }
            set
            {
                _isMainButtonEnabled = value;
                RaisePropertyChanged("IsMainButtonEnabled");
            }
        }


        

        private bool _isCheckedRefund;
        public bool IsCheckedRefund
        {
            get
            {
                return _isCheckedRefund;
            }
            set
            {
                _isCheckedRefund = value;
                //if (_isCheckedRefund == true)
                //{
                //    if (!((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false))
                //    {
                //        IsTextBoxVisibleForIban = true;
                //        IsTextBoxEnableForIban = true;
                //        IsDropdownVisibleForIban = false;
                //    }
                //}
                //else
                //{
                //    IsDropdownVisibleForIban = true;
                //    IsTextBoxEnableForIban = false;
                //    IsTextBoxVisibleForIban = false;
                //}
                RaisePropertyChanged("IsCheckedRefund");
            }
        }
        private string _ibanNumberText;
        public string IbanNumberText
        {
            get
            {
                return _ibanNumberText;
            }
            set
            {
                _ibanNumberText = value;
                RaisePropertyChanged("IbanNumberText");
            }
        }
        private bool _isIBANValid;
        public bool IsIBANValid
        {
            get
            {
                return _isIBANValid;
            }
            set
            {
                _isIBANValid = value;
                RaisePropertyChanged("IsIBANValid");
            }
        }

        private Result2 _selectedIBAN;
        public Result2 SelectedIBAN
        {
            get
            {
                return _selectedIBAN;
            }
            set
            {
                _selectedIBAN = value;
                if (_selectedIBAN != null)
                {
                    TxtSelectedIBAN = _selectedIBAN.Iban;
                }
                RaisePropertyChanged("SelectedIBAN");
            }
        }
        private string _txtSelectedIBAN;
        public string TxtSelectedIBAN
        {
            get
            {
                return _txtSelectedIBAN;
            }
            set
            {
                _txtSelectedIBAN = value;
                RaisePropertyChanged("TxtSelectedIBAN");
            }
        }
        private ObservableCollection<Result2> _iBANList;
        public ObservableCollection<Result2> IBANList
        {
            get
            {
                return _iBANList;
            }
            set
            {
                _iBANList = value;
                //if (_iBANList != null && _iBANList.Count != 0)
                //{
                //    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false)
                //    {
                //        IsEnableIBAN = false;
                //    }
                //    else
                //    {
                //        IsEnableIBAN = true;
                //    }
                //}
                //else
                //{
                //    IsEnableIBAN = false;
                //}
                RaisePropertyChanged("IBANList");
            }
        }
        private string _isNewAccountText;
        public string NewAccountText

        {
            get
            {
                return _isNewAccountText;
            }
            set
            {
                _isNewAccountText = value;
                RaisePropertyChanged("NewAccountText");
            }
        }
        private bool _isEnableIBAN;
        public bool IsEnableIBAN
        {
            get
            {
                return _isEnableIBAN;
            }
            set
            {
                _isEnableIBAN = value;
                RaisePropertyChanged("IsEnableIBAN");
            }
        }
        private bool _isVATRefunCheckedVisible;
        public bool IsVATRefunCheckedVisible
        {
            get
            {
                return _isVATRefunCheckedVisible;
            }
            set
            {
                _isVATRefunCheckedVisible = value;
                RaisePropertyChanged("IsVATRefunCheckedVisible");
            }
        }
        private bool _isEnableCheckedRefund = true;
        public bool IsEnableCheckedRefund
        {
            get
            {
                return _isEnableCheckedRefund;
            }
            set
            {
                _isEnableCheckedRefund = value;
                RaisePropertyChanged("IsEnableCheckedRefund");
            }
        }
        private bool _isNewAccountButtonVisible = false;
        public bool IsNewAccountButtonVisible
        {
            get
            {
                return _isNewAccountButtonVisible;
            }
            set
            {
                _isNewAccountButtonVisible = value;
                RaisePropertyChanged("IsNewAccountButtonVisible");
            }
        }
        private List<IBANType> _iBANTypesList;
        public List<IBANType> IBANTypesList
        {
            get
            {
                return _iBANTypesList;
            }
            set
            {
                _iBANTypesList = value;
                //if (_iBANTypesList != null && _iBANTypesList.Count != 0)
                //{
                //    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false)
                //    {
                //        IsEnableIBANType = false;
                //    }
                //    else
                //    {
                //        IsEnableIBANType = true;
                //    }
                //}
                //else
                //{
                //    IsEnableIBANType = false;
                //}
                RaisePropertyChanged("IBANTypesList");
            }
        }
        #endregion


        #region Method
        public void createIBANType()
        {
            IBANTypesList = new List<IBANType>();
            List<IBANType> IBANTypesDummyList = new List<IBANType>();
            IBANType iBANType = new IBANType();
            iBANType.key = "ZS0001";
            iBANType.Text = AppResources.ZIBANNationalID;
            IBANTypesDummyList.Add(iBANType);
            IBANType iBANType1 = new IBANType();
            iBANType1.key = "BUP002";
            iBANType1.Text = AppResources.ZIBANCommercialRegistrationID;
            IBANTypesDummyList.Add(iBANType1);
            IBANType iBANType2 = new IBANType();
            iBANType2.key = "ZS0005";
            iBANType2.Text = AppResources.ZIBANCompanyID;
            IBANTypesDummyList.Add(iBANType2);
            IBANTypesList = IBANTypesDummyList;
        }
        public async Task SetIBANIdNumber()
        {
            try
            {
                TxtSelectedIBANIDNumber = string.Empty;
                List<IBANIDNumber> iBANIDNumbersResponse = await WebServiceManager.GAZTGetIBANIdNumber(SelectedIBANType.key);
                PopToRootPage();
                if (iBANIDNumbersResponse != null || iBANIDNumbersResponse.Count() != 0)
                {
                    IBANIDNumberList = new List<IBANIDNumber>();
                    IBANIDNumberList = iBANIDNumbersResponse;
                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
        #endregion
    }
}
