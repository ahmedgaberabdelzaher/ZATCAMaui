using EGAZT.Models;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class RefundAccountPopupPageViewModel : BaseViewModel
    {

        public RefundAccountPopupPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
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
                if (_vATDeclarationDetails == value) return;
                _vATDeclarationDetails = value;
                RaisePropertyChanged("VATDeclarationDetails");
            }
        }
        private VATDeclarationD _responseVATDeclarationD;
        public VATDeclarationD ResponseVATDeclarationD
        {
            get
            {
                return _responseVATDeclarationD;
            }
            set
            {
                if (_responseVATDeclarationD == value) return;

                _responseVATDeclarationD = value;
                RaisePropertyChanged("ResponseVATDeclarationD");
            }
        }
        private Result6 _vATNewModelFor15Percent;
        public Result6 VATNewModelFor15Percent
        {
            get
            {
                return _vATNewModelFor15Percent;
            }
            set
            {
                if (_vATNewModelFor15Percent == value) return;

                _vATNewModelFor15Percent = value;
                RaisePropertyChanged("VATNewModelFor15Percent");
            }
        }
        private Result6 _vATNewModelFor5Percent;
        public Result6 VATNewModelFor5Percent
        {
            get
            {
                return _vATNewModelFor5Percent;
            }
            set
            {
                if (_vATNewModelFor5Percent == value) return;

                _vATNewModelFor5Percent = value;
                RaisePropertyChanged("VATNewModelFor5Percent");
            }
        }
        private List<Attachment> _dummyaTTACHSetsList;
        public List<Attachment> DummyATTACHSetsList
        {
            get
            {
                return _dummyaTTACHSetsList;
            }
            set
            {
                if (_dummyaTTACHSetsList == value) return;

                _dummyaTTACHSetsList = value;
                RaisePropertyChanged("DummyATTACHSetsList");
            }
        }
        private List<Attachment> _aTTACHSetsList;
        public List<Attachment> ATTACHSetsList
        {
            get
            {
                return _aTTACHSetsList;
            }
            set
            {
                if (_aTTACHSetsList == value) return;

                _aTTACHSetsList = value;
                RaisePropertyChanged("ATTACHSetsList");
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
                if (_selectedIBANType == value) return;

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
                if (_selectedIBANTypePrev == value) return;

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
                if (_iBANIDNumberList == value) return;

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
                if (_selectedIBANIDNumber == value) return;

                _selectedIBANIDNumber = value;
                if (_selectedIBANIDNumber != null)
                {
                    TxtSelectedIBANIDNumber = _selectedIBANIDNumber.Idnumber;
                }
                else
                {
                    TxtSelectedIBANIDNumber = string.Empty;
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
                if (_selectedIBANIDNumberPrev == value) return;

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
                if (_TxtSelectedIBANIDNumber == value) return;

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
                if (_txtSelectedIBANType == value) return;

                _txtSelectedIBANType = value;
                RaisePropertyChanged("TxtSelectedIBANType");
            }
        }
        //private string _selectedIban;
        //public string SelectedIban
        //{
        //    get
        //    {
        //        return _selectedIban;
        //    }
        //    set
        //    {
        //        _selectedIban = value;
        //        RaisePropertyChanged("SelectedIban");
        //    }
        //}
        private bool _isRefundVisible = false;
        public bool IsRefundVisible
        {
            get
            {
                return _isRefundVisible;
            }
            set
            {
                if (_isRefundVisible == value) return;

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
                if (_isVisibleDropdownForRefund == value) return;

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
                if (_IsVisiblechkRefundDeclaration == value) return;

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
                if (_isTextBoxVisibleForIban == value) return;

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
                if (_isDropdownVisibleForIban == value) return;

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
                if (_isDropdownVisibleForIban == value) return;

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

        private void FilterIBANIDTypeList(string idType)
        {
            IBANTypesList = new List<IBANType>();
            if (idType == "ZS0005")
            {
                IBANTypesList.Add(new IBANType
                {
                    key = "ZS0005",
                    Text = AppResources.ZIBANCompanyID
                });
            }
            else if (idType == "ZS0001")
            {
                IBANTypesList.Add(new IBANType
                {
                    key = "ZS0001",
                    Text = AppResources.ZIBANNationalID
                });
            }
            else if (idType == "BUP002")
            {
                IBANTypesList.Add(new IBANType
                {
                    key = "BUP002",
                    Text = AppResources.ZIBANCommercialRegistrationID
                });
            }
        }

        private void FilterIBANIDNumberList(IBanResponseModelResults banResponseModelResults)
        {
            IBANIDNumberList = new List<IBANIDNumber>();
            try
            {
                IBANIDNumberList.Add(new IBANIDNumber
                {
                    Idnumber = banResponseModelResults.IdNumber,
                    Type = ""
                });
            }
            catch(Exception e)
            {

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

                    if (VATDeclarationDetails.d.Cr1645GoliveFg.Equals("X"))
                    {
                        for (int i = 0; i < CR1645IBanListModel.Count; i++)
                        {
                            if (CR1645IBanListModel[i].Iban.Equals(SelectedIBAN.Iban))
                            {
                                //ID Type list
                                FilterIBANIDTypeList(CR1645IBanListModel[i].IdType);
                                SelectedIBANType = IBANTypesList[0];
                                SelectedIBANTypePrev = IBANTypesList[0];
                                TxtSelectedIBANType = IBANTypesList[0].Text;

                                //ID Number List
                                FilterIBANIDNumberList(CR1645IBanListModel[i]);
                                SelectedIBANIDNumber = IBANIDNumberList[0];
                                SelectedIBANIDNumberPrev = IBANIDNumberList[0];
                                TxtSelectedIBANIDNumber = IBANIDNumberList[0].Idnumber;
                                IbanNumberText = _selectedIBAN.Iban;
                            }
                        }

                    }
                    if (IsNewAccountButtonVisible == true)
                    {
                        IbanNumberText = _selectedIBAN.Iban;
                    }
                    else
                    {
                        IbanNumberText = _selectedIBAN.Iban;
                    }
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
        private bool _isNewLoading = false;
        public bool IsNewLoading
        {
            get
            {
                return _isNewLoading;
            }
            set
            {
                _isNewLoading = value;
                RaisePropertyChanged("IsNewLoading");
            }
        }

        public bool _IsCarriedForwandReviewMessageForRefund;
        public bool IsCarriedForwandReviewMessageForRefund
        {
            get
            {
                return _IsCarriedForwandReviewMessageForRefund;
            }
            set
            {
                _IsCarriedForwandReviewMessageForRefund = value;
                RaisePropertyChanged("IsCarriedForwandReviewMessageForRefund");
            }
        }

        private bool _isRefundYesMsgDisplayed = false;
        public bool IsRefundYesMsgDisplayed
        {
            get
            {
                return _isRefundYesMsgDisplayed;
            }
            set
            {
                _isRefundYesMsgDisplayed = value;
                RaisePropertyChanged("IsRefundYesMsgDisplayed");
            }
        }

        private bool _isDeclarationCheckedForRefund = false;
        public bool IsDeclarationCheckedForRefund
        {
            get
            {
                return _isDeclarationCheckedForRefund;
            }
            set
            {
                _isDeclarationCheckedForRefund = value;
                RaisePropertyChanged("IsDeclarationCheckedForRefund");
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
        private VATDeclaration _vATDeclarationDataDummy;
        public VATDeclaration VATDeclarationDataDummy
        {
            get
            {
                return _vATDeclarationDataDummy;
            }
            set
            {
                _vATDeclarationDataDummy = value;
                RaisePropertyChanged("VATDeclarationDataDummy");
            }
        }
        private bool _iSSwichButtonEnable = false;
        public bool IsSwichButtonEnable
        {
            get
            {
                return _iSSwichButtonEnable;
            }
            set
            {
                _iSSwichButtonEnable = value;
                RaisePropertyChanged("IsSwichButtonEnable");
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

        private List<IBanResponseModelResults> _cR1645IBanListModel;
        public List<IBanResponseModelResults> CR1645IBanListModel
        {
            get
            {
                return _cR1645IBanListModel;
            }
            set
            {
                _cR1645IBanListModel = value;

                RaisePropertyChanged("CR1645IBanListModel");
            }
        }

        private bool _isIdTypeEnabled;
        public bool IsIdTypeEnabled
        {
            get
            {
                return _isIdTypeEnabled;
            }
            set
            {
                _isIdTypeEnabled = value;
                RaisePropertyChanged("IsIdTypeEnabled");
            }
        }
        private bool _isIdNumberEnabled;
        public bool IsIdNumberEnabled
        {
            get
            {
                return _isIdNumberEnabled;
            }
            set
            {
                _isIdNumberEnabled = value;
                RaisePropertyChanged("IsIdNumberEnabled");
            }
        }
        private bool _isNewAccountEnabled;
        public bool IsNewAccountEnabled
        {
            get
            {
                return _isNewAccountEnabled;
            }
            set
            {
                _isNewAccountEnabled = value;
                RaisePropertyChanged("IsNewAccountEnabled");
            }
        }
        private bool _isIbansEnabled;
        public bool IsIbansEnabled
        {
            get
            {
                return _isIbansEnabled;
            }
            set
            {
                _isIbansEnabled = value;
                RaisePropertyChanged("IsIbansEnabled");
            }
        }
        private bool _isRefundCheckboxEnabled;
        public bool IsRefundCheckboxEnabled
        {
            get
            {
                return _isRefundCheckboxEnabled;
            }
            set
            {
                _isRefundCheckboxEnabled = value;
                RaisePropertyChanged("IsRefundCheckboxEnabled");
            }
        }
        private bool _isConfirmRefundButtonEnabled;
        public bool IsConfirmRefundButtonEnabled
        {
            get
            {
                return _isConfirmRefundButtonEnabled;
            }
            set
            {
                _isConfirmRefundButtonEnabled = value;
                RaisePropertyChanged("IsConfirmRefundButtonEnabled");
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

        public async Task SubmitClicked()
        {
            try
            {
                // MessagingCenter.Send<Object, string>(this, "RefundClicked", "Yes");
                bool Result = await FirstCall();
                if (Result)
                {

                    await Task.Delay(7000);

                    if (string.IsNullOrEmpty(VATDeclarationDetails.d.Fbnum))
                    {

                        VATDeclaration _vATDeclarationForGet = await WebServiceManager.GAZTGetVATReturns(App.Fbguid, VATDeclarationDetails.d.Fbnumz, App.EUser, "");
                        PopToRootPage();
                        if (_vATDeclarationForGet != null && _vATDeclarationForGet.d != null)
                        {
                            if (!string.IsNullOrEmpty(_vATDeclarationForGet.d.Fbnum))
                            {
                                //
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                                    headerAmountInfo.IsLinkAvailable = false;
                                    headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                                    headerWithInfos.Add(headerAmountInfo);


                                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                    await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                    //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);




                                    _navigationService.GoBack();
                                });
                            }
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {

                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                    }

                    // CreateDataForPost();
                    setIbanData();
                    string operation = "01";// Passed operation "01" to submit the VAT Declaration Data
                                            //   VATDeclarationData.d.StepNumberz = "04";
                                            //VATDeclarationData.d.StepNumber = "00";
                                            // VATDeclarationData.d.Fbguid = string.Empty;
                    VATDeclarationDetails.d.StepNumberz = "04";
                    VATDeclarationDetails.d.UserTypz = "TP";
                    VATDeclarationDetails.d.Operationz = operation;
                    var res = await SaveReturnAndGetReturnAndSetButtons();
                    if (res != null && res.d != null)
                    {
                        MessagingCenter.Send<Object, string>(this, "Refundsubmitted", "Refundsubmitted");
                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        //    ManageEnabledProperty(false);
                        //    IsGetSadadNumberEnabled = false;
                        //    IsMainButtonEnabled = false;
                        //    IsMainButtonVisible = false;
                        //    IsRefundButtonEnabled = false;
                        //    IsRefundButtonVisible = false;
                        //    isBtnVisible = false;
                        //    IsEnableSwitchToggledFor15PercentChange = false;
                        //    //IsMainButtonVisible = false;
                        //    //IsSwichButtonEnableToTap = false;
                        //    //IsEnableIBAN = false;
                        //    //IsEnableCheckedRefund = false;
                        //    //IsEnableIBANType = false;
                        //    //IsEnableIBANIdNumber = false;
                        //    //IsGetAcknowledgementClicked = true;
                        //    //IsMoreButtonEnabled = false;
                        //});
                        if (App.ICRStatus == "E0045" || App.ICRStatus == "E0056")
                        {
                            await Task.Delay(5000);
                        }
                        await PopupNavigation.Instance.PopAsync();
                        MessagingCenter.Send<Object, string>(this, "RefundClicked", "Yes");
                        _navigationService.NavigateTo(App.VATReturnSuccessfullPageView, VATDeclarationDetails);
                        // _navigationService.NavigateTo(App.AcknowledgementDetailsPageView, VATDeclarationData);
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {

                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = WebServiceManager.ErrorMessageForVAT;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                //  MessagingCenter.Send<Object, string>(this, "RefundClickedForStop", "Yes");
                                //await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                                WebServiceManager.ErrorMessageForVAT = string.Empty;
                            });
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception)
            {
                // MessagingCenter.Send<Object, string>(this, "RefundClickedForStop", "Yes");
            }
        }
        public async Task<bool> FirstCall()
        {
            bool result = false;
            try
            {
                VATDeclaration resNew = null;
                //if (String.IsNullOrEmpty(VATDeclarationData.d.Fbnum) || App.ICRStatus == "E0045")
                //{
                //CreateDataForPost();
                setIbanData();
                string operation = "01";
                VATDeclarationDetails.d.StepNumber = "04";
                VATDeclarationDetails.d.StepNumberz = "04";
                VATDeclarationDetails.d.UserTypz = "TP";
                VATDeclarationDetails.d.Operationz = operation;
                VATDeclaration response = new VATDeclaration();
                resNew = await SaveReturnAndGetReturnAndSetButtons();
                // }
                if (resNew != null && resNew.d != null)
                {
                    VATDeclarationDataDummy = resNew;
                    decimal FourteenA = 0;
                    if (!string.IsNullOrEmpty(VATDeclarationDetails.d.TotaldueVat) && !string.IsNullOrEmpty(VATDeclarationDetails.d.Preperiodcorr))
                    {
                        FourteenA = Convert.ToDecimal(VATDeclarationDetails.d.TotaldueVat) + Convert.ToDecimal(VATDeclarationDetails.d.Preperiodcorr);
                    }
                    if ((IsSwichButtonEnable == false && FourteenA < 5000 && Convert.ToDecimal(VATDeclarationDetails.d.NetdueVat) < 0) || (IsSwichButtonEnable == true && FourteenA < 100000 && Convert.ToDecimal(VATDeclarationDetails.d.CreditVat) > 0))
                    {


                        //List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                        //HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                        //NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                        //headerAmountInfo.IsLinkAvailable = false;

                        //StringBuilder Masseges = new StringBuilder();
                        //Masseges.Append(AppResources.Pleasereviewthecalculationandsubmitagain);
                        //Masseges.Append(Environment.NewLine);
                        //Masseges.Append(Environment.NewLine);
                        //Masseges.Append(Environment.NewLine);
                        //Masseges.Append(AppResources.CreditReturnMsg);
                        //headerAmountInfo.IsLinkAvailable = false;
                        //headerAmountInfo.IsRed = "#ff0000";
                        //headerAmountInfo.IsBold = "Bold";
                        //headerAmountInfo.Message = Masseges.ToString();

                        //headerWithInfos.Add(headerAmountInfo);


                        //newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                        //newDesignPopUp.HeaderWithInfos = headerWithInfos;
                        //newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                        //PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                        result = true;

                    }
                    else
                    {
                        if (resNew.d.SubmitFg == "" || resNew.d.SubmitFg == string.Empty)
                        {
                            MessagingCenter.Send<Object, string>(this, "Refundsubmitted", "Refundsubmitted");
                            //Device.BeginInvokeOnMainThread(async () =>
                            //{
                            //    ManageEnabledProperty(false);
                            //    IsGetSadadNumberEnabled = false;
                            //    IsMainButtonEnabled = false;
                            //    IsMainButtonVisible = false;
                            //    IsRefundButtonEnabled = false;
                            //    IsRefundButtonVisible = false;
                            //    isBtnVisible = false;
                            //    IsEnableSwitchToggledFor15PercentChange = false;
                            //    //IsMainButtonVisible = false;
                            //    //IsSwichButtonEnableToTap = false;
                            //    //IsEnableIBAN = false;
                            //    //IsEnableCheckedRefund = false;
                            //    //IsEnableIBANType = false;
                            //    //IsEnableIBANIdNumber = false;
                            //    //IsGetAcknowledgementClicked = true;
                            //    //IsMoreButtonEnabled = false;
                            //});
                            if (App.ICRStatus == "E0045" || App.ICRStatus == "E0056")
                            {
                                await Task.Delay(5000);
                            }
                            //ManageEnabledProperty(false);
                            await PopupNavigation.Instance.PopAsync();
                            MessagingCenter.Send<Object, string>(this, "RefundClicked", "Yes");
                            _navigationService.NavigateTo(App.VATReturnSuccessfullPageView, VATDeclarationDetails);
                            //_navigationService.NavigateTo(App.AcknowledgementDetailsPageView, VATDeclarationData);
                        }
                        else
                        {
                            result = true;
                            // await _dialogService.ShowMessage(AppResources.Pleasereviewthecalculationandsubmitagain, AppResources.Information);
                            //VATReturnFormClicked();
                            //SelectedIndex = 2;
                            //PageSelectedItem = VatTabbledPageList[2];
                        }
                    }
                    //await _dialogService.ShowMessage(AppResources.Pleasereviewthecalculationandsubmitagain, AppResources.Information);
                    //VATReturnFormClicked();
                    //PageSelectedItem = VatTabbledPageList[2];
                }
                else
                {
                    result = false;
                    IsLoading = false;
                    if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                            //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {

                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = WebServiceManager.ErrorMessageForVAT;

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                            //MessagingCenter.Send<Object, string>(this, "RefundClickedForStop", "Yes");

                            //await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                            //_navigationService.GoBack();
                            WebServiceManager.ErrorMessageForVAT = string.Empty;
                        });
                    }
                    //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                }
                return result;
            }
            catch (Exception)
            {
                //  MessagingCenter.Send<Object, string>(this, "RefundClickedForStop", "Yes");
                result = false;
                return result;
            }
        }

        private async Task<VATDeclaration> SaveReturnAndGetReturnAndSetButtons()
        {
            try
            {
                //New code for VAT 15% Change

                //if (IsYesChecked == true)
                //{
                //    VATDeclarationData.d.Yesno = "X";
                //}
                //else
                //{
                //    VATDeclarationData.d.Yesno = string.Empty;
                //}




                //if (IsDeclarationCheckedForSummary == true)
                //{
                //    VATDeclarationData.d.DecFg = "1";
                //}
                //else
                //{
                //    VATDeclarationData.d.DecFg = "0";
                //}
                //if (IschkRefundDeclaration)
                //{
                //    VATDeclarationData.d.TcFlg = "1";
                //}
                //else
                //{
                //    VATDeclarationData.d.TcFlg = "0";
                //}
                //if (IsCheckedRefund)
                //{
                //    VATDeclarationData.d.IbanCb = "1";
                //}
                //else
                //{
                //    VATDeclarationData.d.IbanCb = "0";
                //}
                if (VATDeclarationDetails != null && VATDeclarationDetails.d != null && VATDeclarationDetails.d.ATTACHSet.results.Count() != 0)
                {
                    DummyATTACHSetsList = new List<Attachment>();
                    DummyATTACHSetsList = VATDeclarationDetails.d.ATTACHSet.results;
                }
                if (ATTACHSetsList != null && ATTACHSetsList.Count() != 0)
                {
                    VATDeclarationDetails.d.ATTACHSet.results = ATTACHSetsList;
                }
                VATDeclaration response = await WebServiceManager.SaveVATDeclarationData(VATDeclarationDetails);
                PopToRootPage();
                if (response != null && response.d != null && !string.IsNullOrEmpty(response.d.Fbnum))
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {
                            VATDeclarationDetails = response;
                            ResponseVATDeclarationD = VATDeclarationDetails.d;
                            if (VATDeclarationDetails.d.VATPERITEMSet.results != null)
                            {
                                if (VATDeclarationDetails.d.GoliveFg == "X")
                                {
                                    if (VATDeclarationDetails.d.Yesno == "X")
                                    {
                                        //VATNewModelFor15Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "002").FirstOrDefault();
                                        //VATNewModelFor5Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "003").FirstOrDefault();
                                    }
                                    else
                                    {
                                        // VATNewModelFor15Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "002").FirstOrDefault();
                                    }
                                }
                            }
                            //  SetCommasforAll();
                            if (DummyATTACHSetsList != null && DummyATTACHSetsList.Count() != 0)
                            {
                                VATDeclarationDetails.d.ATTACHSet.results = DummyATTACHSetsList;
                            }
                            if (VATDeclarationDetails.d.Operationz == "01" && App.ICRStatus == "E0001")
                            {
                                App.ICRStatus = "E0013";
                            }
                            //VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(VATDeclarationData.d.ReturnIdz, VATDeclarationData.d.Fbnumz, ICRListPageViewModel.EUser,"");
                            //if (_vATDeclaration != null && _vATDeclaration.d != null)
                            //{
                            //    VATDeclarationData = _vATDeclaration;
                            //    ResponseVATDeclarationD = VATDeclarationData.d;
                            //    SetData();
                            //}
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                //ManageEnabledProperty(true);
                            });
                        }
                        // await SetButtons(VATDeclarationData);
                        return response;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                return response;
            }
            catch (InternetException)
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public void SetCommasforAll()
        {
            ResponseVATDeclarationD.StdsalesAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.StdsalesAmt);
            ResponseVATDeclarationD.StdsalesAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.StdsalesAdj);
            ResponseVATDeclarationD.SalesGccAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.SalesGccAmt);
            ResponseVATDeclarationD.SalesGccAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.SalesGccAdj);
            ResponseVATDeclarationD.ZerosalesAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ZerosalesAmt);
            ResponseVATDeclarationD.ZerosalesAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ZerosalesAdj);
            ResponseVATDeclarationD.ExportsAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExportsAmt);
            ResponseVATDeclarationD.ExportsAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExportsAdj);
            ResponseVATDeclarationD.ExemptsalesAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExemptsalesAmt);
            ResponseVATDeclarationD.ExemptsalesAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExemptsalesAdj);
            ResponseVATDeclarationD.StdpurchaseAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.StdpurchaseAmt);
            ResponseVATDeclarationD.StdpurchaseAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.StdpurchaseAdj);
            ResponseVATDeclarationD.ImportspaidAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ImportspaidAmt);
            ResponseVATDeclarationD.ImportspaidAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ImportspaidAdj);
            ResponseVATDeclarationD.ImportsaccAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ImportsaccAmt);
            ResponseVATDeclarationD.ImportsaccAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ImportsaccAdj);
            ResponseVATDeclarationD.ZeropurchaseAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ZeropurchaseAmt);
            ResponseVATDeclarationD.ZeropurchaseAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ZeropurchaseAdj);
            ResponseVATDeclarationD.ExemptpurchaseAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExemptpurchaseAmt);
            ResponseVATDeclarationD.ExemptpurchaseAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExemptpurchaseAdj);
            ResponseVATDeclarationD.Preperiodcorr = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.Preperiodcorr);

            //For New 15% Change
            SetCommasforNew15percentchange();
        }
        public void SetCommasforNew15percentchange()
        {
            if (VATDeclarationDetails.d.GoliveFg == "X")
            {
                VATNewModelFor15Percent = VATDeclarationDetails.d.VATPERITEMSet.results.Where(x => x.Type == "002").FirstOrDefault();
                VATNewModelFor5Percent = VATDeclarationDetails.d.VATPERITEMSet.results.Where(x => x.Type == "003").FirstOrDefault();


                VATNewModelFor15Percent.ImportsaccAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportsaccAdj);
                VATNewModelFor15Percent.ImportsaccAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportsaccAmt);
                VATNewModelFor15Percent.ImportsaccVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportsaccVat);
                VATNewModelFor15Percent.ImportspaidAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportspaidAdj);
                VATNewModelFor15Percent.ImportspaidAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportspaidAmt);
                VATNewModelFor15Percent.ImportspaidVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportspaidVat);
                VATNewModelFor15Percent.StdpurchaseAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdpurchaseAdj);
                VATNewModelFor15Percent.StdpurchaseAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdpurchaseAmt);
                VATNewModelFor15Percent.StdpurchasesVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdpurchasesVat);
                VATNewModelFor15Percent.StdsalesAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdsalesAdj);
                VATNewModelFor15Percent.StdsalesAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdsalesAmt);
                VATNewModelFor15Percent.StdsalesVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdsalesVat);


                VATNewModelFor5Percent.ImportsaccAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportsaccAdj);
                VATNewModelFor5Percent.ImportsaccAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportsaccAmt);
                VATNewModelFor5Percent.ImportsaccVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportsaccVat);
                VATNewModelFor5Percent.ImportspaidAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportspaidAdj);
                VATNewModelFor5Percent.ImportspaidAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportspaidAmt);
                VATNewModelFor5Percent.ImportspaidVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportspaidVat);
                VATNewModelFor5Percent.StdpurchaseAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdpurchaseAdj);
                VATNewModelFor5Percent.StdpurchaseAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdpurchaseAmt);
                VATNewModelFor5Percent.StdpurchasesVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdpurchasesVat);
                VATNewModelFor5Percent.StdsalesAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdsalesAdj);
                VATNewModelFor5Percent.StdsalesAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdsalesAmt);
                VATNewModelFor5Percent.StdsalesVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdsalesVat);
            }
        }
        public void setIbanData()
        {
            // IsNewAccountButtonVisible
            //if (IsVisibleDropdownForRefund == true)
            //{
            VATDeclarationDetails.d.RefundFg = "1";
            if (IsNewAccountButtonVisible == true)
            {
                VATDeclarationDetails.d.Iban = IbanNumberText;
                VATDeclarationDetails.d.IbanCb = "1";
            }
            else
            {
                if (SelectedIBAN != null)
                {
                    VATDeclarationDetails.d.Iban = SelectedIBAN.Iban;
                    VATDeclarationDetails.d.IbanCb = "0";
                }
            }
            if (SelectedIBANType != null)
            {
                VATDeclarationDetails.d.Idtype = SelectedIBANType.key;
            }
            if (SelectedIBANIDNumber != null)
            {
                VATDeclarationDetails.d.Idnum = SelectedIBANIDNumber.Idnumber;
            }
            if (IsDeclarationCheckedForRefund)
            {
                VATDeclarationDetails.d.TcFlg = "1";
            }
            else
            {
                VATDeclarationDetails.d.TcFlg = "0";
            }
        }
        #endregion
    }
}
