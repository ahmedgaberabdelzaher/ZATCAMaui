
using System.Collections.ObjectModel;
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class RefundAccountPopupPageViewModel : BaseViewModel
    {

        public RefundAccountPopupPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

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
                OnPropertyChanged("VATDeclarationDetails");
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
                OnPropertyChanged("ResponseVATDeclarationD");
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
                OnPropertyChanged("VATNewModelFor15Percent");
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
                OnPropertyChanged("VATNewModelFor5Percent");
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
                OnPropertyChanged("DummyATTACHSetsList");
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
                OnPropertyChanged("ATTACHSetsList");
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
                    if (!VATDeclarationDetails.data.Cr1645GoliveFg.Equals("X"))
                    {
                        if (VATDeclarationDetails.data.PendingIbanMsg != "")
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = AppResources.NDIBANIncomplete;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                            });
                            return;
                        }
                        else
                        {
                            SetIBANIdNumber();
                            TxtSelectedIBANType = _selectedIBANType.Text;
                        }
                    }
                }
                else
                {
                }
                OnPropertyChanged("SelectedIBANType");
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
                OnPropertyChanged("SelectedIBANTypePrev");
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
                OnPropertyChanged("IBANIDNumberList");
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
                OnPropertyChanged("SelectedIBANIDNumber");
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
                OnPropertyChanged("SelectedIBANIDNumberPrev");
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
                OnPropertyChanged("TxtSelectedIBANIDNumber");
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
                OnPropertyChanged("TxtSelectedIBANType");
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
                if (_isRefundVisible == value) return;

                _isRefundVisible = value;
                OnPropertyChanged("IsRefundVisible");
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
                OnPropertyChanged("IsVisibleDropdownForRefund");
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
                OnPropertyChanged("IsVisiblechkRefundDeclaration");
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
                OnPropertyChanged("IsTextBoxVisibleForIban");
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
                OnPropertyChanged("IsDropdownVisibleForIban");
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
                OnPropertyChanged("IsMainButtonEnabled");
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
                OnPropertyChanged("IsCheckedRefund");
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
                OnPropertyChanged("IbanNumberText");
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
                OnPropertyChanged("IsIBANValid");
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
            else if (idType == "ZS0003")
            {
                IBANTypesList.Add(new IBANType
                {
                    key = "ZS0003",
                    Text = AppResources.TinDeregistrationGCCID
                });
            }

            else if (idType == "ZS0018")
            {
                IBANTypesList.Add(new IBANType
                {
                    key = "ZS0018",
                    Text = AppResources.TinDeregistrationGCCID
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
            catch (Exception e)
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

                    if (VATDeclarationDetails.data.Cr1645GoliveFg.Equals("X"))
                    {
                        if (VATDeclarationDetails.data.PendingIbanMsg != "")
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = AppResources.NDIBANIncomplete;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);




                            });
                            return;
                        }
                        else
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
                                    SelectedIBANIDNumber = IBANIDNumberList.FirstOrDefault();
                                    SelectedIBANIDNumberPrev = IBANIDNumberList.FirstOrDefault();
                                    TxtSelectedIBANIDNumber = IBANIDNumberList[0].Idnumber;
                                    IbanNumberText = _selectedIBAN.Iban;
                                    break;
                                }
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
                OnPropertyChanged("SelectedIBAN");
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
                OnPropertyChanged("TxtSelectedIBAN");
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
                OnPropertyChanged("IBANList");
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
                OnPropertyChanged("IsCarriedForwandReviewMessageForRefund");
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
                OnPropertyChanged("IsRefundYesMsgDisplayed");
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
                OnPropertyChanged("IsDeclarationCheckedForRefund");
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
                OnPropertyChanged("NewAccountText");
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
                OnPropertyChanged("IsEnableIBAN");
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
                OnPropertyChanged("IsVATRefunCheckedVisible");
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
                OnPropertyChanged("IsEnableCheckedRefund");
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
                OnPropertyChanged("IsNewAccountButtonVisible");
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
                OnPropertyChanged("VATDeclarationDataDummy");
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
                OnPropertyChanged("IsSwichButtonEnable");
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
                OnPropertyChanged("IBANTypesList");
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

                OnPropertyChanged("CR1645IBanListModel");
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
                OnPropertyChanged("IsIdTypeEnabled");
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
                OnPropertyChanged("IsIdNumberEnabled");
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
                OnPropertyChanged("IsNewAccountEnabled");
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
                OnPropertyChanged("IsIbansEnabled");
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
                OnPropertyChanged("IsRefundCheckboxEnabled");
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
                OnPropertyChanged("IsConfirmRefundButtonEnabled");
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
            IBANType iBANType3 = new IBANType();
            iBANType3.key = "ZS0003";
            iBANType3.Text = AppResources.TinDeregistrationGCCID;
            IBANTypesDummyList.Add(iBANType3);

            IBANTypesList = IBANTypesDummyList;
        }
        public async Task SetIBANIdNumber()
        {
            try
            {
                TxtSelectedIBANIDNumber = string.Empty;
                List<IBANIDNumber> iBANIDNumbersResponse = await WebServiceManager.GAZTGetIBANIdNumber(SelectedIBANType.key);
                await PopToRootPage();
                if (iBANIDNumbersResponse != null || iBANIDNumbersResponse.Count() != 0)
                {
                    IBANIDNumberList = new List<IBANIDNumber>();
                    IBANIDNumberList = iBANIDNumbersResponse;
                }
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
        }

        public async Task<bool> SubmitClicked()
        {
            try
            {
                bool Result = await FirstCall();
                if (Result)
                {

                    await Task.Delay(7000);

                    if (string.IsNullOrEmpty(VATDeclarationDetails.data1.Fbnumz))
                    {

                        VATDeclaration _vATDeclarationForGet = await WebServiceManager.GAZTGetVATReturns(App.Fbguid, VATDeclarationDetails.data1.Fbnumz, App.EUser, "");
                        await PopToRootPage();
                        if (_vATDeclarationForGet != null && _vATDeclarationForGet.data != null)
                        {
                            if (!string.IsNullOrEmpty(_vATDeclarationForGet.data.Fbnum))
                            {
                                //
                            }
                            else
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

                                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                _navigationService.GoBack();
                            }
                        }
                        else
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

                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                            _navigationService.GoBack();
                        }
                    }

                    setIbanData();
                    string operation = "01";// Passed operation "01" to submit the VAT Declaration Data
                                            //   VATDeclarationData.d.StepNumberz = "04";
                                            //VATDeclarationData.d.StepNumber = "00";
                                            // VATDeclarationData.d.Fbguid = string.Empty;
                    VATDeclarationDetails.data.StepNumberz = "04";
                    VATDeclarationDetails.data.UserTypz = "TP";
                    VATDeclarationDetails.data.Operationz = operation;
                    var res = await SaveReturnAndGetReturnAndSetButtons();
                    if (res != null && res.data1 != null)
                    {
                        MessagingCenter.Send<Object, string>(this, "Refundsubmitted", "Refundsubmitted");
                        if (App.ICRStatus == "E0045" || App.ICRStatus == "E0056")
                        {
                            await Task.Delay(5000);
                        }

                        MessagingCenter.Send<Object, string>(this, "RefundClicked", "Yes");
                        await MopupService.Instance.PopAsync();
                        await _navigationService.NavigateTo(App.VATReturnSuccessfullPageView, VATDeclarationDetails);
                        return true;

                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
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

                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                            _navigationService.GoBack();
                        }
                        else
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

                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                            WebServiceManager.ErrorMessageForVAT = string.Empty;
                        }
                    }
                }
                else
                {

                }

            }
            catch (Exception)
            {
            }
            return false;
        }
        public async Task<bool> FirstCall()
        {
            bool result = false;
            try
            {
                VATDeclaration resNew = null;
                setIbanData();
                string operation = "01";
                VATDeclarationDetails.data.StepNumber = "04";
                VATDeclarationDetails.data.StepNumberz = "04";
                VATDeclarationDetails.data.UserTypz = "TP";
                VATDeclarationDetails.data.Operationz = operation;
                VATDeclaration response = new VATDeclaration();
                resNew = await SaveReturnAndGetReturnAndSetButtons();
                if (resNew.data == null)
                {
                    resNew.data = resNew.data1;
                }
                if (resNew != null && resNew.data1 != null)
                {
                    VATDeclarationDataDummy = resNew;
                    decimal FourteenA = 0;
                    if (!string.IsNullOrEmpty(VATDeclarationDetails.data1.TotaldueVat) && !string.IsNullOrEmpty(VATDeclarationDetails.data1.Preperiodcorr))
                    {
                        FourteenA = Convert.ToDecimal(VATDeclarationDetails.data1.TotaldueVat) + Convert.ToDecimal(VATDeclarationDetails.data1.Preperiodcorr);
                    }
                    if ((IsSwichButtonEnable == false && FourteenA < 5000 && Convert.ToDecimal(VATDeclarationDetails.data1.NetdueVat) < 0) || (IsSwichButtonEnable == true && FourteenA < 100000 && Convert.ToDecimal(VATDeclarationDetails.data1.CreditVat) > 0))
                    {
                        result = true;
                    }
                    else
                    {
                        if (resNew.data1.SubmitFg == "" || resNew.data1.SubmitFg == string.Empty)
                        {
                            MessagingCenter.Send<Object, string>(this, "Refundsubmitted", "Refundsubmitted");

                            if (App.ICRStatus == "E0045" || App.ICRStatus == "E0056")
                            {
                                await Task.Delay(5000);
                            }
                            //MopupService.Instance.PopAsync();
                            MessagingCenter.Send<Object, string>(this, "RefundClicked", "Yes");
                            await _navigationService.NavigateTo(App.VATReturnSuccessfullPageView, VATDeclarationDetails);
                            return true;
                        }
                        else
                        {
                            result = true;

                        }
                    }
                }
                else
                {
                    result = false;
                    IsLoading = false;
                    if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
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

                        await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                        _navigationService.GoBack();
                    }
                    else
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

                        await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                        WebServiceManager.ErrorMessageForVAT = string.Empty;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        private async Task<VATDeclaration> SaveReturnAndGetReturnAndSetButtons()
        {
            try
            {

                if (VATDeclarationDetails != null && VATDeclarationDetails.data1 != null && VATDeclarationDetails.data1.ATTACHSet.Count() != 0)
                {
                    DummyATTACHSetsList = new List<Attachment>();
                    DummyATTACHSetsList = VATDeclarationDetails.data1.ATTACHSet;
                }
                if (ATTACHSetsList != null && ATTACHSetsList.Count() != 0)
                {
                    VATDeclarationDetails.data1.ATTACHSet = ATTACHSetsList;
                }
                VATDeclaration response = await WebServiceManager.SaveVATDeclarationData(VATDeclarationDetails);
                await PopToRootPage();
                if (response != null && response.data1 != null && !string.IsNullOrEmpty(response.data1.Fbnumz))
                {
                    try
                    {
                        if (response != null && response.data1 != null)
                        {
                            VATDeclarationDetails = response;
                            if (VATDeclarationDetails.data == null)
                            {
                                VATDeclarationDetails.data = VATDeclarationDetails.data1;
                            }
                            ResponseVATDeclarationD = VATDeclarationDetails.data1;

                            if (DummyATTACHSetsList != null && DummyATTACHSetsList.Count() != 0)
                            {
                                VATDeclarationDetails.data1.ATTACHSet = DummyATTACHSetsList;
                            }
                            if (VATDeclarationDetails.data1.Operationz == "01" && App.ICRStatus == "E0001")
                            {
                                App.ICRStatus = "E0013";
                            }

                        }
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
            if (VATDeclarationDetails.data.GoliveFg == "X")
            {
                VATNewModelFor15Percent = VATDeclarationDetails.data.VATPERITEMSet.Where(x => x.Type == "002").FirstOrDefault();
                VATNewModelFor5Percent = VATDeclarationDetails.data.VATPERITEMSet.Where(x => x.Type == "003").FirstOrDefault();


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
            VATDeclarationDetails.data.RefundFg = "1";
            if (IsNewAccountButtonVisible == true)
            {
                VATDeclarationDetails.data.Iban = IbanNumberText;
                VATDeclarationDetails.data.IbanCb = "1";
            }
            else
            {
                if (SelectedIBAN != null)
                {
                    VATDeclarationDetails.data.Iban = SelectedIBAN.Iban;
                    VATDeclarationDetails.data.IbanCb = "0";
                }
            }
            if (SelectedIBANType != null)
            {
                VATDeclarationDetails.data.Idtype = SelectedIBANType.key;
            }
            if (SelectedIBANIDNumber != null)
            {
                VATDeclarationDetails.data.Idnum = SelectedIBANIDNumber.Idnumber;
            }
            if (IsDeclarationCheckedForRefund)
            {
                VATDeclarationDetails.data.TcFlg = "1";
            }
            else
            {
                VATDeclarationDetails.data.TcFlg = "0";
            }
        }
        #endregion
    }
}
