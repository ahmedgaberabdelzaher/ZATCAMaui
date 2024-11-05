using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.VATRefunds;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;
using ZATCAMAUI.Views.NewDesign.VATRefunds;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds
{

    public class VATRefundsNewRequestViewModel : BaseViewModel
    {
        #region Commands

        public ICommand IbanIdTypeTapped { get; set; }
        public ICommand IbanIdNumberTapped { get; set; }
        public ICommand OnMoreClicked { get; set; }
        public ICommand IBANAccManagementTapped { get; set; }
        public ICommand NewAccountCommand { get; set; }
        public ICommand ContinueCommand { get; set; }
        public ICommand VoidCommand { get; set; }


        #endregion

        #region CR4914
        private bool IsSadadBillCheckBox1 = false;
        public bool isSadadBillCheckBox1
        {
            get { return IsSadadBillCheckBox1; }
            set
            {
                if (IsSadadBillCheckBox1 == value) return;

                IsSadadBillCheckBox1 = value;
                OnPropertyChanged("isSadadBillCheckBox1");
            }
        }
        #endregion

        private Color _itemTappedcolorChange = (Color)Application.Current.Resources["Primary"];
        public Color SelectedBackgroundColor
        {
            get
            {
                return _itemTappedcolorChange;
            }
            set
            {
                if (_itemTappedcolorChange == value) return;
                _itemTappedcolorChange = value;
                OnPropertyChanged("SelectedBackgroundColor");
            }
        }
        public ObservableCollection<VatReffundAmtDetails> _vatfrmRefundsModel { get; set; }
        public ObservableCollection<VatReffundAmtDetails> VatfrmRefundsModel
        {
            get
            {
                return _vatfrmRefundsModel;
            }

            set
            {
                if (_vatfrmRefundsModel == value) return;
                _vatfrmRefundsModel = value;
                OnPropertyChanged("VatfrmRefundsModel");
            }
        }

        public ObservableCollection<VatReffundAmtDetails> _vatfrmRefundsCopyModel { get; set; }
        public ObservableCollection<VatReffundAmtDetails> VatfrmRefundsCopyModel
        {
            get
            {
                return _vatfrmRefundsCopyModel;
            }

            set
            {
                if (_vatfrmRefundsCopyModel == value) return;
                _vatfrmRefundsCopyModel = value;
                OnPropertyChanged("VatfrmRefundsCopyModel");
            }
        }

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
                OnPropertyChanged("VATRefundsModel");
            }
        }

        private decimal _Amount = 0;
        public decimal amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if (_Amount == value) return;
                _Amount = value;
                OnPropertyChanged("amount");
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
                OnPropertyChanged("VatRefundsDisplayDataModel");
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
                OnPropertyChanged("VatNewReqSummaryData");
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
                OnPropertyChanged("VatRefundsIbanDataModel");
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
                OnPropertyChanged("SelectedIdtype");
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
                OnPropertyChanged("SelectedIdNumber");
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
                OnPropertyChanged("SelectedIDTypeCode");
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


                OnPropertyChanged("PickerModel");
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
                OnPropertyChanged("IbanData");
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
                //if (_selectedIbanData == value) return;

                _selectedIbanData = value;


                if (IBanListResponse != null && IBanListResponse.D != null && IBanListResponse.D.Results != null && IBanListResponse.D.Results.Count > 0)
                {
                    try
                    {
                        var SlectedIban = IBanListResponse.D.Results.Where(m => m.Iban == SelectedIbanData.Iban).FirstOrDefault();

                        if (SlectedIban != null)
                        {

                            SelectedIdtype = SlectedIban.IdtypeDesc;
                            SelectedIdNumber = SlectedIban.IdNumber;

                            IBANType idType = IBANTypesList.Where(m => m.key == SlectedIban.IdType).FirstOrDefault();
                            SelectedIDTypeCode = idType.key;

                            _ = SetIBANIdNumber(idType.key);
                        }




                    }
                    catch (Exception ex)
                    {

                    }


                }


                OnPropertyChanged("SelectedIbanData");
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
                OnPropertyChanged("IsAddAccountVisisble");
            }
        }



        private bool _isTypeEditable = true;
        public bool IsTypeEditable
        {
            get
            {
                return _isTypeEditable;
            }

            set
            {
                if (_isTypeEditable == value) return;

                _isTypeEditable = value;
                OnPropertyChanged("IsTypeEditable");
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
                OnPropertyChanged("IsVoidBtnVisible");
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
                OnPropertyChanged("IBANTypesList");
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
                OnPropertyChanged("IBANIDNumberList");
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
                OnPropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    OnPropertyChanged(nameof(MarkComplete));
                }
                else
                {
                    MarkComplete = false;
                    OnPropertyChanged(nameof(MarkComplete));
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
                OnPropertyChanged("MaxIndex");
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
                OnPropertyChanged("IsNavigatedToSubmitted");
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

                OnPropertyChanged("ListOfActionButtonsApplicable");
            }
        }

        public bool IsSelected { get; private set; }



        public IBanListResponseModel IBanListResponse;



        public VATRefundsNewRequestViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            IbanIdTypeTapped = new Command(async () => await OnIbanIdTypeClicked());
            IbanIdNumberTapped = new Command(async () => await OnIbanNumberClicked());

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
            IBANAccManagementTapped = new Command(async () =>
            {
                await _navigationService.NavigateTo(App.GAZTBankAccountManagementPageView,true);
            });

            NewAccountCommand = new Command(async () =>
            {
                await MopupService.Instance.PushAsync(new NewAccountPopUpPageView(string.Empty));
            });


            ContinueCommand = new Command(async () =>
            {
                await ContinueBtnClicked();
            });
            VoidCommand = new Command(async () =>
            {
                if (App.IsArabic)
                {
                    var result = await Application.Current.MainPage.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZNo, AppResources.ZYes);

                    if (!result)
                    {
                        await OnVoidBtnClicked();
                    }
                }
                else
                {
                    var result = await Application.Current.MainPage.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZYes, AppResources.ZNo);

                    if (result)
                    {
                        await OnVoidBtnClicked();
                    }
                }
            });
        }

        internal void Selected_update(IReadOnlyList<object> currentSelection)
        {
            throw new NotImplementedException();
        }

        private async Task ShowMoreOptionsPopUp()
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
                              await  OnVoidBtnClicked();
                                break;
                            case ArButtons.حفظكمسودة:

                              await  OnSaveDraftClicked();



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
                              await  OnVoidBtnClicked();
                                break;

                            case Buttons.SaveasDraft:

                             await   OnSaveDraftClicked();

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
        public async Task VoidMsg()
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

        public async Task OnSaveDraftClicked()
        {




            try
            {

                VatRefundsDisplayDataModel.Operationx = "05";
                VatRefundsDisplayDataModel.Gpartx = App.LoginDataRetrieved.TIN;
                VatRefundsDisplayDataModel.Langx = UtilityManager.GetLanguageParameter();
                if (SelectedIbanData != null)
                {
                    VatRefundsDisplayDataModel.Iban = SelectedIbanData.Iban;
                    VatRefundsDisplayDataModel.IbanC = SelectedIbanData.Iban;
                }
                //  VatRefundsDisplayDataModel.Idnumber = SelectedIdNumber;
                VatRefundsDisplayDataModel.Idnum = SelectedIdNumber;
                if (SelectedIDTypeCode != null)
                {
                    VatRefundsDisplayDataModel.IdType = SelectedIDTypeCode;
                    VatRefundsDisplayDataModel.Idtype = SelectedIDTypeCode;
                }
                VatRefundsDisplayDataModel.RefundTp = AppResources.VATRefundTpParameter;
                VatNewReqSummaryData.Confirmfg = "";
                VatNewReqSummaryData.TcFg = "X";

                for (int i = 0; i < VatfrmRefundsModel.Count; i++)
                {
                    if (VatRefundsDisplayDataModel.VAtRefundSET[i].Fbnum == VatfrmRefundsModel[i].Fbnum)
                    {
                        VatRefundsDisplayDataModel.VAtRefundSET[i].CheckFg = VatfrmRefundsModel[i].CheckFg;
                    }
                    else
                    {

                    }
                }

                VatRefundsDisplayDataModel.TxnTpx = "CRE_VTRF";

                IsLoading = true;

                VatNewReqSummaryData = await VATDeregistrationWebServiceManager.GAZTVATRefundSubmitRequest(VatRefundsDisplayDataModel);

                if (VatNewReqSummaryData.Operationx.Equals("05"))
                {
                    App.selectedVATItem = VatNewReqSummaryData.Fbnumx;
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


                    IsLoading = false;

                }


            }
            catch (InternetException)
            {
                IsLoading = false;

                await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
            }
            catch (GAZTErrorException ex)
            {
                IsLoading = false;

                string message = ex.Message;

                await _dialogService.ShowMessage(message, AppResources.Information);
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

                IsLoading = true;
                amount = 0;
                VatRefundsDisplayDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData("");
                VatfrmRefundsModel = new ObservableCollection<VatReffundAmtDetails>(VatRefundsDisplayDataModel.VAtRefundSET);//

                VatRefundsIbanDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundGetIbanData("");
                IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>(VatRefundsIbanDataModel.IbanSet);
                VatRefundsDisplayDataModel.Rfamt = VatRefundsDisplayDataModel.Rfamt.Replace("-", string.Empty);

                if (VatRefundsDisplayDataModel.Cr1645GoliveFg == "X")
                {

                    if (VatRefundsDisplayDataModel.PendingIbanMsg != "")
                    {
                        IsLoading = false;
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


                        GetAllIbanList();
                    }
                    else
                    {
                        GetAllIbanList();
                    }
                }



                if (VatRefundsDisplayDataModel.Rfamt == "0")
                {
                    amount = 0;
                }
                else
                {
                    amount = decimal.Parse(VatRefundsDisplayDataModel.Rfamt);
                }

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



                IsLoading = false;
            }
            catch (InternetException)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                _navigationService.GoBack();
            }
            catch (GAZTErrorException ex)
            {
                IsLoading = false;

                string message = ex.Message;

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
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }



        public async Task GetAllIbanList()
        {

            try
            {
                IBanListResponse = await WebServiceManager.GetIBanDataForCR1645();

                IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>();

                if (IBanListResponse != null && IBanListResponse.D != null && IBanListResponse.D.Results != null && IBanListResponse.D.Results.Count > 0)
                {
                    for (int i = 0; i < IBanListResponse.D.Results.Count; i++)
                    {
                        var IbanListsResults = new VarRefundIbanDataModelMetadataResult()
                        {
                            Iban = IBanListResponse.D.Results[i].Iban
                        };
                        IbanData.Add(IbanListsResults);
                    }

                }
                if (IbanData.Count > 0)
                {
                    SelectedIbanData = IbanData.FirstOrDefault();
                    IsTypeEditable = false;
                }
            }
            catch (InternetException ex)
            {
                _ = _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
        }

        public async Task LoadDraftsData(VatRefundsListResultModel draftsData)
        {
            try
            {
                IsLoading = true;
                amount = 0;
                
                VatRefundsDisplayDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData(draftsData.WiDtlSet[0].Fbguid);

                if (VatRefundsDisplayDataModel.Idnumber != null && VatRefundsDisplayDataModel.Idnumber != string.Empty)
                {
                    SelectedIdNumber = VatRefundsDisplayDataModel.Idnum;
                }

                if (VatRefundsDisplayDataModel.Idtype != null && VatRefundsDisplayDataModel.Idtype != string.Empty)
                {
                    try
                    {
                        SelectedIDTypeCode = VatRefundsDisplayDataModel.Idtype;
                        IBANType idType = IBANTypesList.Where(m => m.key == SelectedIDTypeCode).FirstOrDefault();//"ZS0003" not suporrted
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
                VatfrmRefundsModel = new ObservableCollection<VatReffundAmtDetails>(VatRefundsDisplayDataModel.VAtRefundSET);//


                foreach (var i in VatfrmRefundsModel)
                {
                    decimal A = decimal.Parse(i.Betrw);
                    if (i.CheckFg == "X")
                    {
                        isSadadBillCheckBox1 = true;
                    }
                    else
                    {
                        isSadadBillCheckBox1 = false;
                    }
                }

                VatRefundsIbanDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundGetIbanData("");
                IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>(VatRefundsIbanDataModel.IbanSet);

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

                await GetAllIbanList();

                IsLoading = false;
            }
            catch (InternetException)
            {
                IsLoading = false;

                await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                _navigationService.GoBack();
            }
            catch (GAZTErrorException ex)
            {
                IsLoading = false;
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
                    IsLoading = false;

                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();

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
                    VatRefundsIbanDataModel.IbanSet = new VarRefundIbanDataModelMetadataResult[1000];
                }

                if (VatRefundsIbanDataModel.IbanSet == null)
                {
                    VatRefundsIbanDataModel.IbanSet = new VarRefundIbanDataModelMetadataResult[1000];
                }

                List<VarRefundIbanDataModelMetadataResult> tempNewIbanList = new List<VarRefundIbanDataModelMetadataResult>();
                tempNewIbanList.Add(newIbanModel);

                VatRefundsIbanDataModel.IbanSet = tempNewIbanList.ToArray();

                if (VatRefundsIbanDataModel.IbanSet.Count() > 0)
                {
                    IsAddAccountVisisble = false;
                }
            }
            catch (Exception)
            {
            }

        }

        public async Task OnIbanIdTypeClicked()
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

        public async Task OnIbanNumberClicked()
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
                IsLoading = true;
                List<IBANIDNumber> iBANIDNumbersResponse = await WebServiceManager.GAZTGetIBANIdNumber(selectedIbanIdType);

                PopToRootPage();
                if (iBANIDNumbersResponse != null || iBANIDNumbersResponse.Count() != 0)
                {
                    IBANIDNumberList = new ObservableCollection<IBANIDNumber>(iBANIDNumbersResponse);
                }

                IsLoading = false;
            }
            catch (InternetException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
            catch (Exception)
            {
            }
        }

        public async Task ContinueBtnClicked()
        {
            VatRefundsDisplayDataModel.Gpartx = App.LoginDataRetrieved.TIN;
            VatRefundsDisplayDataModel.Langx = UtilityManager.GetLanguageParameter();
            if (SelectedIbanData != null)
            {
                VatRefundsDisplayDataModel.Iban = SelectedIbanData.Iban;
                VatRefundsDisplayDataModel.IbanC = SelectedIbanData.Iban;
            }

            VatRefundsDisplayDataModel.Idnum = SelectedIdNumber;
            if (SelectedIDTypeCode != null)
            {
                VatRefundsDisplayDataModel.IdType = SelectedIDTypeCode;
                VatRefundsDisplayDataModel.Idtype = SelectedIDTypeCode;
            }
            VatRefundsDisplayDataModel.RefundTp = AppResources.VATRefundTpParameter;

            for (int i = 0; i < VatfrmRefundsModel.Count; i++)
            {
                if (VatRefundsDisplayDataModel.VAtRefundSET[i].Fbnum == VatfrmRefundsModel[i].Fbnum)
                {
                    VatRefundsDisplayDataModel.VAtRefundSET[i].CheckFg = VatfrmRefundsModel[i].CheckFg;
                }
            }

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

        public async Task OnVoidBtnClicked()
        {

            VatRefundsDisplayDataModel.Operationx = "04";
            VatRefundsDisplayDataModel.Gpartx = App.LoginDataRetrieved.TIN;
            VatRefundsDisplayDataModel.Langx = UtilityManager.GetLanguageParameter();
            VatRefundsDisplayDataModel.RefundTp = AppResources.VATRefundTpParameter;
            VatRefundsDisplayDataModel.TxnTpx = "CRE_VTRF";

            try
            {
                IsLoading = true;

                VatNewReqSummaryData = await VATDeregistrationWebServiceManager.GAZTVATRefundSubmitRequest(VatRefundsDisplayDataModel);
                if (VatNewReqSummaryData != null)
                {

                    ListOfActionButtonsApplicable = null;

                }
                IsLoading = false;
            }
            catch (InternetException)
            {
                IsLoading = false;

                await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
            }
            catch (GAZTErrorException ex)
            {
                IsLoading = false;
                string message = ex.Message;

                await _dialogService.ShowMessage(message, AppResources.Information);
            }

        }

        //CR4914

        public void CheckBoxSelected_update(VatReffundAmtDetails vatDetails, bool? isChecked)
        {

            VatfrmRefundsCopyModel = VatfrmRefundsModel;

            for (int i = 0; i < VatfrmRefundsCopyModel.Count; i++)
            {
                if (VatfrmRefundsCopyModel[i].Fbnum == vatDetails.Fbnum)
                {
                    decimal A = decimal.Parse(VatfrmRefundsCopyModel[i].Betrw);
                    if (isChecked == true)
                    {
                        VatfrmRefundsCopyModel[i].CheckFg = "X";
                        VatfrmRefundsCopyModel[i].IsItemSelected = true;
                        amount = A + amount;
                    }
                    else
                    {
                        VatfrmRefundsCopyModel[i].CheckFg = "";
                        VatfrmRefundsCopyModel[i].IsItemSelected = false;
                        amount -= A;
                    }
                }
            }
            VatRefundsDisplayDataModel.Rfamt = amount.ToString();
            VatfrmRefundsModel = VatfrmRefundsCopyModel;
            OnPropertyChanged("VatfrmRefundsModel");
        }
    }

}


