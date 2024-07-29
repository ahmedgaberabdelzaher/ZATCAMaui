using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.VATRefunds;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;
using ZATCAMAUI.Views.NewDesign.VATRefunds;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds
{

    public class VATRefundsNewRequestViewModel : BaseViewModel
    {
        #region Commands

        public ICommand GoBackBtnTapped { get; set; }
        public ICommand CloseBtnTapped { get; set; }
        public ICommand IbanIdTypeTapped { get; set; }
        public ICommand IbanIdNumberTapped { get; set; }
        public ICommand OnMoreClicked { get; set; }

        #endregion

        public ObservableCollection<VATRefundsModel> _vatRefundsModel { get; set; }
        public ObservableCollection<VATRefundsModel> VATRefundsModel
        {
            get
            {
                return _vatRefundsModel;
            }

            set
            {
                if (_vatRefundsModel == value) return;
                _vatRefundsModel = value;
                RaisePropertyChanged("VATRefundsModel");
            }
        }

        private VatRefundDisplayDataModel _vatRefundsDisplayDataModel = null;
        public VatRefundDisplayDataModel VatRefundsDisplayDataModel
        {
            get
            {
                return _vatRefundsDisplayDataModel;
            }

            set
            {
                if (_vatRefundsDisplayDataModel == value) return;

                _vatRefundsDisplayDataModel = value;
                RaisePropertyChanged("VatRefundsDisplayDataModel");
            }
        }

        private VatRefundDisplayDataModel _vatNewReqSummaryData = null;
        public VatRefundDisplayDataModel VatNewReqSummaryData
        {
            get
            {
                return _vatNewReqSummaryData;
            }

            set
            {
                if (_vatNewReqSummaryData == value) return;

                _vatNewReqSummaryData = value;
                RaisePropertyChanged("VatNewReqSummaryData");
            }
        }


        private VarRefundIbanDataModel _vatRefundsIbanDataModel = null;
        public VarRefundIbanDataModel VatRefundsIbanDataModel
        {
            get
            {
                return _vatRefundsIbanDataModel;
            }

            set
            {
                if (_vatRefundsIbanDataModel == value) return;

                _vatRefundsIbanDataModel = value;
                RaisePropertyChanged("VatRefundsIbanDataModel");
            }
        }



        private string _selectedIdtype { get; set; }
        public string SelectedIdtype
        {
            get
            {
                return _selectedIdtype;
            }

            set
            {
                if (_selectedIdtype == value) return;

                _selectedIdtype = value;
                RaisePropertyChanged("SelectedIdtype");
            }
        }

        private string _selectedIdNumber { get; set; }
        public string SelectedIdNumber
        {
            get
            {
                return _selectedIdNumber;
            }

            set
            {
                if (_selectedIdNumber == value) return;

                _selectedIdNumber = value;
                RaisePropertyChanged("SelectedIdNumber");
            }
        }

        private string _selectedIDTypeCode { get; set; }
        public string SelectedIDTypeCode
        {
            get
            {
                return _selectedIDTypeCode;
            }

            set
            {
                if (_selectedIDTypeCode == value) return;

                _selectedIDTypeCode = value;
                RaisePropertyChanged("SelectedIDTypeCode");
            }
        }

        private GenericPickerModel _pickerModel { get; set; }
        public GenericPickerModel PickerModel
        {
            get
            {
                return _pickerModel;
            }
            set
            {
                if (_pickerModel == value) return;

                _pickerModel = value;

                try
                {
                    if (PickerModel != null && PickerModel.SelectedValue != null && IBANTypesList != null)
                    {
                        if (PickerModel.PickerId == "idTypePicker")
                        {
                            SelectedIdtype = PickerModel.SelectedValue;
                            IBANType idType = IBANTypesList.Where(m => m.Text == PickerModel.SelectedValue).FirstOrDefault();
                            SelectedIDTypeCode = idType.key;
                            SelectedIdNumber = AppResources.IDNumber;
                            _ = SetIBANIdNumber(idType.key);
                        }
                        else
                        {
                            SelectedIdNumber = PickerModel.SelectedValue;
                        }
                    }
                }
                catch (Exception)
                {

                }


                RaisePropertyChanged("PickerModel");
            }
        }

        private ObservableCollection<VarRefundIbanDataModelMetadataResult> _ibanData = null;
        public ObservableCollection<VarRefundIbanDataModelMetadataResult> IbanData
        {
            get
            {
                return _ibanData;
            }

            set
            {
                if (_ibanData == value) return;

                _ibanData = value;
                RaisePropertyChanged("IbanData");
            }
        }

        private VarRefundIbanDataModelMetadataResult _selectedIbanData = null;
        public VarRefundIbanDataModelMetadataResult SelectedIbanData
        {
            get
            {
                return _selectedIbanData;
            }

            set
            {
                if (_selectedIbanData == value) return;

                _selectedIbanData = value;
                RaisePropertyChanged("SelectedIbanData");
            }
        }

        private bool _isAddAccountVisisble = false;
        public bool IsAddAccountVisisble
        {
            get
            {
                return _isAddAccountVisisble;
            }

            set
            {
                if (_isAddAccountVisisble == value) return;

                _isAddAccountVisisble = value;
                RaisePropertyChanged("IsAddAccountVisisble");
            }
        }

        private bool _isVoidBtnVisible = false;
        public bool IsVoidBtnVisible
        {
            get
            {
                return _isVoidBtnVisible;
            }

            set
            {
                if (_isVoidBtnVisible == value) return;

                _isVoidBtnVisible = value;
                RaisePropertyChanged("IsVoidBtnVisible");
            }
        }

        private ObservableCollection<IBANType> _iBANTypesList { get; set; }
        public ObservableCollection<IBANType> IBANTypesList
        {
            get
            {
                return _iBANTypesList;
            }
            set
            {
                if (_iBANTypesList == value) return;

                _iBANTypesList = value;
                RaisePropertyChanged("IBANTypesList");
            }
        }

        private ObservableCollection<IBANIDNumber> _iBANIDNumberList;
        public ObservableCollection<IBANIDNumber> IBANIDNumberList
        {
            get
            {
                return _iBANIDNumberList;
            }
            set
            {
                if (_iBANIDNumberList == value) return;

                _iBANIDNumberList = value;
                RaisePropertyChanged("IBANIDNumberList");
            }
        }
        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                if (_currenrIndex == value) return;

                _currenrIndex = value;
                RaisePropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
                else
                {
                    MarkComplete = false;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
            }
        }
        public bool MarkComplete { get; private set; } = false;
        private int _maxIndex = 3;
        public int MaxIndex
        {
            get
            {
                return _maxIndex;
            }
            set
            {
                if (_maxIndex == value) return;

                _maxIndex = value;
                RaisePropertyChanged("MaxIndex");
            }
        }
        public bool _isNavigatedToSubmitted;
        public bool IsNavigatedToSubmitted
        {
            get
            {
                return _isNavigatedToSubmitted;
            }
            set
            {
                if (_isNavigatedToSubmitted == value) return;

                _isNavigatedToSubmitted = value;
                RaisePropertyChanged("IsNavigatedToSubmitted");
            }
        }

        private List<string> _ListOfActionButtonsApplicable;
        public List<string> ListOfActionButtonsApplicable
        {
            get
            {
                return _ListOfActionButtonsApplicable;
            }
            set
            {
                if (_ListOfActionButtonsApplicable == value) return;

                _ListOfActionButtonsApplicable = value;

                RaisePropertyChanged("ListOfActionButtonsApplicable");
            }
        }


        public VATRefundsNewRequestViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });

            IbanIdTypeTapped = new Command(OnIbanIdTypeClicked);
            IbanIdNumberTapped = new Command(OnIbanNumberClicked);

            SelectedIdtype = AppResources.ZZIDType;
            SelectedIdNumber = AppResources.IDNumber;
            VatNewReqSummaryData = new VatRefundDisplayDataModel();

            PickerModel = new GenericPickerModel();
            CreateIBANType();
            OnMoreClicked = new Command(async () =>
            {

                if (ListOfActionButtonsApplicable == null)
                {
                    setMoreOptioButtons();
                    await MopupService.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(ListOfActionButtonsApplicable));

                }
                else
                {
                    await MopupService.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(ListOfActionButtonsApplicable));
                }
            });
        }

        private async void ShowMoreOptionsPopUp()
        {
            try
            {
                if (ListOfActionButtonsApplicable != null && ListOfActionButtonsApplicable.Count() != 0)
                {
                    string action = await Application.Current.MainPage.DisplayActionSheet("", AppResources.ZZCancel, null, ListOfActionButtonsApplicable.ToArray());
                    if (App.IsArabic)
                    {
                        ArButtons buttonId = ArButtons.None;
                        if (!string.IsNullOrEmpty(action))
                        {
                            action = action.Replace(" ", "");
                        }
                        Enum.TryParse(action, out buttonId);
                        switch (buttonId)
                        {

                            case ArButtons.إلغاء:
                                OnVoidBtnClicked();
                                break;
                            case ArButtons.حفظكمسودة:

                                OnSaveDraftClicked();



                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Buttons buttonId = Buttons.None;
                        if (!string.IsNullOrEmpty(action))
                        {
                            action = action.Replace(" ", "");
                        }
                        Enum.TryParse(action, out buttonId);
                        switch (buttonId)
                        {

                            case Buttons.Void:
                                OnVoidBtnClicked();
                                break;

                            case Buttons.SaveasDraft:

                                OnSaveDraftClicked();

                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {


            }
        }
        public async void VoidMsg()
        {

            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
            headerAmountInfo.IsLinkAvailable = false;
            headerAmountInfo.Message = AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost;

            headerWithInfos.Add(headerAmountInfo);


            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
            newDesignPopUp.HeaderWithInfos = headerWithInfos;
            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

            await MopupService.Instance.PushAsync(new ShowVatInformationConfirmationPageView(newDesignPopUp));

        }
        public bool isDraftClicked = false;

        public async void OnSaveDraftClicked()
        {

            VatRefundsDisplayDataModel.Operationx = "05";
            VatRefundsDisplayDataModel.Gpartx = App.LoginDataRetrieved.TIN;
            VatRefundsDisplayDataModel.Langx = UtilityManager.GetLanguageParameter();
            if (SelectedIbanData != null)
            {
                VatRefundsDisplayDataModel.Iban = SelectedIbanData.Iban;
                VatRefundsDisplayDataModel.IbanC = SelectedIbanData.Iban;
            }
            VatRefundsDisplayDataModel.Idnumber = SelectedIdNumber;
            VatRefundsDisplayDataModel.Idnum = SelectedIdNumber;
            if (SelectedIDTypeCode != null)
            {
                VatRefundsDisplayDataModel.IdType = SelectedIDTypeCode;
                VatRefundsDisplayDataModel.Idtype = SelectedIDTypeCode;
            }
            VatRefundsDisplayDataModel.RefundTp = AppResources.VATRefundTpParameter;
            VatNewReqSummaryData.Confirmfg = "";
            VatNewReqSummaryData.TcFg = "X";

            VatRefundsDisplayDataModel.TxnTpx = "CRE_VTRF";

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VatNewReqSummaryData = await VATDeregistrationWebServiceManager.GAZTVATRefundSubmitRequest(VatRefundsDisplayDataModel);

                if (VatNewReqSummaryData.Operationx.Equals("05"))
                {
                    App.selectedVATItem = VatNewReqSummaryData.Fbnumx;
                   MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        App.selectedVATItem = VatNewReqSummaryData.Fbnumx;
                        setMoreOptioButtons();

                        List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                        HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                        NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                        headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                        headerAmountInfo.IsLinkAvailable = false;
                        headerAmountInfo.Message = string.Format(AppResources.ZVatRefundRequestSavedAsDraft);

                        headerWithInfos.Add(headerAmountInfo);

                        newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                        newDesignPopUp.HeaderWithInfos = headerWithInfos;
                        newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                        await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                    });

                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                }


            }
            catch (InternetException)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

               MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                string message = ex.Message;

               MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(message, AppResources.Information);
                });
            }
            catch (Exception)
            {


            }


        }


        public void setMoreOptioButtons()
        {
            var listOfActionButtonsApplicable = new List<string>();
            if (App.selectedVATItem != "")
            {

                listOfActionButtonsApplicable.Add(AppResources.ZZVoid);
            }


            listOfActionButtonsApplicable.Add(AppResources.ZZSaveAsDraft);
            ListOfActionButtonsApplicable = listOfActionButtonsApplicable;
        }
        public async Task ReloadData()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VatRefundsDisplayDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData("");

                VatRefundsIbanDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundGetIbanData("");
                IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>(VatRefundsIbanDataModel.IbanSet.Results);
                VatRefundsDisplayDataModel.Rfamt = VatRefundsDisplayDataModel.Rfamt.Replace("-", string.Empty);

                if (VatRefundsDisplayDataModel.Fbnumx == string.Empty)
                {
                    IsVoidBtnVisible = false;
                }
                else
                {
                    IsVoidBtnVisible = true;
                }

                if (IbanData == null || IbanData.Count == 0)
                {
                    IsAddAccountVisisble = true;
                }
                else
                {
                    VarRefundIbanDataModelMetadataResult varRefundIbanDataModelMetadataResult = IbanData.FirstOrDefault();
                    IsAddAccountVisisble = false;

                    if (varRefundIbanDataModelMetadataResult.Iban == string.Empty)
                    {
                        IsAddAccountVisisble = true;
                    }
                }

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException )
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                try
                {
                   MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                        _navigationService.GoBack();
                    });

                }
                catch (Exception)
                {
                }
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                string message = ex.Message;

                try
                {
                    PopUp popUp = new PopUp();
                    StringBuilder PopMsg = new StringBuilder();

                    popUp.Message = message;
                    popUp.HeaderText = AppResources.Information;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }

                    await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                    _navigationService.GoBack();
                }
                catch (Exception)
                {
                }
            }
            catch (Exception)
            {


                try
                {
                   MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    });

                }
                catch (Exception)
                {
                }
            }
        }

        public async Task LoadDraftsData(VatRefundsListResultModel draftsData)
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VatRefundsDisplayDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData(draftsData.WiDtlSet.Results[0].Fbguid);
                VatRefundsDisplayDataModel.Rfamt = VatRefundsDisplayDataModel.Rfamt.Replace("-", string.Empty);

                if (VatRefundsDisplayDataModel.Idnumber != null && VatRefundsDisplayDataModel.Idnumber != string.Empty)
                {
                    SelectedIdNumber = VatRefundsDisplayDataModel.Idnumber;
                }

                if (VatRefundsDisplayDataModel.Idtype != null && VatRefundsDisplayDataModel.Idtype != string.Empty)
                {
                    try
                    {
                        SelectedIDTypeCode = VatRefundsDisplayDataModel.Idtype;
                        IBANType idType = IBANTypesList.Where(m => m.key == SelectedIDTypeCode).FirstOrDefault();
                        SelectedIdtype = idType.Text;
                    }
                    catch (Exception)
                    {

                    }
                }

                if (string.IsNullOrEmpty(VatRefundsDisplayDataModel.RefundTp))
                {
                    if (App.IsArabic)
                    {
                        VatNewReqSummaryData.RefundTp = "طلب إسترداد";

                    }
                    else
                    {
                        VatNewReqSummaryData.RefundTp = "Refund Request";
                    }
                }

                if (VatRefundsDisplayDataModel.Fbnumx == string.Empty)
                {
                    IsVoidBtnVisible = false;
                }
                else
                {
                    IsVoidBtnVisible = true;
                }

                VatRefundsIbanDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundGetIbanData("");
                IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>(VatRefundsIbanDataModel.IbanSet.Results);

                if (IbanData == null || IbanData.Count == 0)
                {
                    IsAddAccountVisisble = true;
                }
                else
                {
                    VarRefundIbanDataModelMetadataResult varRefundIbanDataModelMetadataResult = IbanData.FirstOrDefault();
                    IsAddAccountVisisble = false;
                    if (varRefundIbanDataModelMetadataResult.Iban == string.Empty)
                    {
                        IsAddAccountVisisble = true;
                    }
                }

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException )
            {


                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                try
                {
                   MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                        _navigationService.GoBack();
                    });

                }
                catch (Exception)
                {
                }
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                string message = ex.Message;

                try
                {
                    PopUp popUp = new PopUp();
                    StringBuilder PopMsg = new StringBuilder();

                    popUp.Message = message;
                    popUp.HeaderText = AppResources.Information;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }

                    await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                    _navigationService.GoBack();
                }
                catch (Exception)
                {
                }
            }
            catch (Exception)
            {


                try
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                   MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    });

                }
                catch (Exception)
                {
                }
            }
        }

        public void AddNewIban(string newIban)
        {
            try
            {
                if (IbanData == null)
                {
                    IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>();
                }

                VarRefundIbanDataModelMetadataResult newIbanModel = new VarRefundIbanDataModelMetadataResult();
                newIbanModel.Iban = newIban;
                IbanData.Add(newIbanModel);

                if (VatRefundsIbanDataModel.IbanSet == null)
                {
                    VatRefundsIbanDataModel.IbanSet = new NSet();
                }

                if (VatRefundsIbanDataModel.IbanSet.Results == null)
                {
                    VatRefundsIbanDataModel.IbanSet.Results = new VarRefundIbanDataModelMetadataResult[1000];
                }

                List<VarRefundIbanDataModelMetadataResult> tempNewIbanList = new List<VarRefundIbanDataModelMetadataResult>();
                tempNewIbanList.Add(newIbanModel);

                VatRefundsIbanDataModel.IbanSet.Results = tempNewIbanList.ToArray();

                if (VatRefundsIbanDataModel.IbanSet.Results.Count() > 0)
                {
                    IsAddAccountVisisble = false;
                }
            }
            catch (Exception)
            {

            }

            //VatRefundsIbanDataModel.IbanSet.Results
        }

        public async void OnIbanIdTypeClicked()
        {
            CreateIBANType();
            List<string> idTypeData = new List<string>();

            foreach (IBANType iBANType in IBANTypesList)
            {
                idTypeData.Add(iBANType.Text);
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = idTypeData;
            genericPickerModel.PickerTitle = AppResources.ZZIDType;
            genericPickerModel.PickerId = "idTypePicker";

            await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
        }

        public async void OnIbanNumberClicked()
        {
            if (IBANIDNumberList != null && IBANIDNumberList.Count > 0)
            {
                List<string> idNumberData = new List<string>();

                foreach (IBANIDNumber iBANId in IBANIDNumberList)
                {
                    idNumberData.Add(iBANId.Idnumber);
                }

                GenericPickerModel genericPickerModel = new GenericPickerModel();
                genericPickerModel.PickerData = idNumberData;
                genericPickerModel.PickerTitle = AppResources.IDNumber;
                genericPickerModel.PickerId = "idNumberPicker";

                await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
            }
            else
            {
                PopUp popUp = new PopUp();
                StringBuilder PopMsg = new StringBuilder();

                popUp.Message = AppResources.VATRefundsNoIdNumber;

                if (App.IsArabic)
                {
                    popUp.FlowDirections = "RightToLeft";
                    popUp.isFontSet = true;
                }
                else
                {
                    popUp.FlowDirections = "LeftToRight";
                }

                await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
            }
        }

        //OnVoidBtnClicked

        public void CreateIBANType()
        {
            IBANTypesList = new ObservableCollection<IBANType>();
            ObservableCollection<IBANType> IBANTypesDummyList = new ObservableCollection<IBANType>();
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

        public async Task SetIBANIdNumber(string selectedIbanIdType)
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                List<IBANIDNumber> iBANIDNumbersResponse = await WebServiceManager.GAZTGetIBANIdNumber(selectedIbanIdType);

                PopToRootPage();
                if (iBANIDNumbersResponse != null || iBANIDNumbersResponse.Count() != 0)
                {
                    IBANIDNumberList = new ObservableCollection<IBANIDNumber>(iBANIDNumbersResponse);
                }

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
               MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
            catch (Exception)
            {


            }
        }

        public async void ContinueBtnClicked()
        {
            VatRefundsDisplayDataModel.Gpartx = App.LoginDataRetrieved.TIN;
            VatRefundsDisplayDataModel.Langx = UtilityManager.GetLanguageParameter();
            if (SelectedIbanData != null)
            {
                VatRefundsDisplayDataModel.Iban = SelectedIbanData.Iban;
                VatRefundsDisplayDataModel.IbanC = SelectedIbanData.Iban;
            }
            VatRefundsDisplayDataModel.Idnumber = SelectedIdNumber;
            VatRefundsDisplayDataModel.Idnum = SelectedIdNumber;
            if (SelectedIDTypeCode != null)
            {
                VatRefundsDisplayDataModel.IdType = SelectedIDTypeCode;
                VatRefundsDisplayDataModel.Idtype = SelectedIDTypeCode;
            }
            VatRefundsDisplayDataModel.RefundTp = AppResources.VATRefundTpParameter;


            VatRefundsDisplayDataModel.TxnTpx = "CRE_VTRF";

            PopUp popUp = new PopUp();
            StringBuilder PopMsg = new StringBuilder();

            if (App.IsArabic)
            {
                popUp.FlowDirections = "RightToLeft";
                popUp.isFontSet = true;
            }
            else
            {
                popUp.FlowDirections = "LeftToRight";
            }

            if (SelectedIdtype == string.Empty || SelectedIdtype == AppResources.ZZIDType)
            {
                popUp.Message = AppResources.ZPleaseselectparametertype;
                await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                return;
            }

            if (SelectedIdNumber == string.Empty || SelectedIdNumber == AppResources.IDNumber)
            {
                popUp.Message = AppResources.ZVatRefundInformationSelectIBANIDNumber;
                await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                return;
            }

            if (SelectedIbanData == null)
            {
                popUp.Message = AppResources.ZVatRefundInformationSelectIBAN;
                await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                return;
            }

            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new VATRefundDetailsPageView(VatRefundsDisplayDataModel));
            }
            catch (Exception)
            {


            }
        }

        public async void OnVoidBtnClicked()
        {

            VatRefundsDisplayDataModel.Operationx = "04";
            VatRefundsDisplayDataModel.Gpartx = App.LoginDataRetrieved.TIN;
            VatRefundsDisplayDataModel.Langx = UtilityManager.GetLanguageParameter();
            VatRefundsDisplayDataModel.RefundTp = AppResources.VATRefundTpParameter;
            VatRefundsDisplayDataModel.TxnTpx = "CRE_VTRF";

            try
            {


                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VatNewReqSummaryData = await VATDeregistrationWebServiceManager.GAZTVATRefundSubmitRequest(VatRefundsDisplayDataModel);
                if (VatNewReqSummaryData != null)
                {

                    ListOfActionButtonsApplicable = null;

                }
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {


                await Task.Run(() =>
                {
                    IsLoading = false;
                });

               MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                string message = ex.Message;

               MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(message, AppResources.Information);
                });
            }
            catch (Exception)
            {

            }

        }
    }
}
