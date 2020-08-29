using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.VATRefunds;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VATRefunds
{
    public class VATRefundsNewRequestViewModel: BaseViewModel
    {
        #region Commands

        public ICommand GoBackBtnTapped { get; set; }
        public ICommand CloseBtnTapped { get; set; }
        public ICommand IbanIdTypeTapped { get; set; }
        public ICommand IbanIdNumberTapped { get; set; }
        
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
                            SetIBANIdNumber(idType.key);
                        }
                        else
                        {
                            SelectedIdNumber = PickerModel.SelectedValue;
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
                _iBANIDNumberList = value;
                RaisePropertyChanged("IBANIDNumberList");
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

            GoBackBtnTapped = new Command(async () =>
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

            //ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);
            //OutletContinueBtnTapped = new Command(this.OutletContinueBtnClicked);
            //AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            //DeclarationContinueBtnTapped = new Command(this.DeclarationContinueBtnClicked);
            //SummaryContinueBtnTapped = new Command(this.SummaryContinueBtnClicked);
            //OnTinRegisrtationReasonDateTapped = new Command(this.OnTinRegisrtationReasonDateClicked);
            //OnTinRegistrationReasonTapped = new Command(this.OnTinRegisrtationReasonClicked);
            //TinDeregistrationModel = new TINDeregistrationModel();
            //SelectedOutletOption = new TINDeregistrationModel();

            //AddOutletDecisionOptions();
            //PopulateAttachmentsListViewTemplate();
            //PopulateSummaryReasonData();
            //PopulateSummaryDeclarationData();
            //EnableReasonView();
        }

        public async Task ReloadData()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VatRefundsDisplayDataModel = await WebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData("");
               
                VatRefundsIbanDataModel = await WebServiceManager.GAZTGetVATRefundGetIbanData("");
                IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>(VatRefundsIbanDataModel.IbanSet.Results);
                VatRefundsDisplayDataModel.Rfamt = VatRefundsDisplayDataModel.Rfamt.Replace("-", string.Empty);

                if(VatRefundsDisplayDataModel.Fbnumx == string.Empty)
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
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                try
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                        _navigationService.GoBack();
                    });
                   
                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
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

                    await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    _navigationService.GoBack();
                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
                }
            }
            catch(Exception ex)
            {
                try
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    });

                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
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

                VatRefundsDisplayDataModel = await WebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData(draftsData.WiDtlSet.Results[0].Fbguid);
                VatRefundsDisplayDataModel.Rfamt = VatRefundsDisplayDataModel.Rfamt.Replace("-",string.Empty);

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
                    catch(Exception ex)
                    {
                        Console.WriteLine("No ID type");
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

                VatRefundsIbanDataModel = await WebServiceManager.GAZTGetVATRefundGetIbanData("");
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
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                     IsLoading = false;
                });

                try
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                        _navigationService.GoBack();
                    });
                   
                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
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

                    await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    _navigationService.GoBack();
                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    });
                   
                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
                }
            }
        }

        public void AddNewIban(string newIban)
        {
            if(IbanData == null)
            {
                IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>();
            }

            VarRefundIbanDataModelMetadataResult newIbanModel = new VarRefundIbanDataModelMetadataResult();
            newIbanModel.Iban = newIban;
            IbanData.Add(newIbanModel);

            if(VatRefundsIbanDataModel.IbanSet == null)
            {
                VatRefundsIbanDataModel.IbanSet = new NSet();
            }

            if(VatRefundsIbanDataModel.IbanSet.Results == null)
            {
                VatRefundsIbanDataModel.IbanSet.Results = new VarRefundIbanDataModelMetadataResult[1000];
            }

            List<VarRefundIbanDataModelMetadataResult> tempNewIbanList = new List<VarRefundIbanDataModelMetadataResult>();
            tempNewIbanList.Add(newIbanModel);

            VatRefundsIbanDataModel.IbanSet.Results = tempNewIbanList.ToArray();

            //VatRefundsIbanDataModel.IbanSet.Results
        }

        public async void OnIbanIdTypeClicked()
        {
            ObservableCollection<string> idTypeData = new ObservableCollection<string>();

            foreach(IBANType iBANType in IBANTypesList)
            {
                idTypeData.Add(iBANType.Text);
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = idTypeData;
            genericPickerModel.PickerTitle = AppResources.ZZIDType;
            genericPickerModel.PickerId = "idTypePicker";

            await PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
        }

        public async void OnIbanNumberClicked()
        {
            if(IBANIDNumberList != null && IBANIDNumberList.Count > 0)
            {
                ObservableCollection<string> idNumberData = new ObservableCollection<string>();

                foreach (IBANIDNumber iBANId in IBANIDNumberList)
                {
                    idNumberData.Add(iBANId.Idnumber);
                }

                GenericPickerModel genericPickerModel = new GenericPickerModel();
                genericPickerModel.PickerData = idNumberData;
                genericPickerModel.PickerTitle = AppResources.IDNumber;
                genericPickerModel.PickerId = "idNumberPicker";

                await PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
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

                await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
            catch(Exception)
            {

            }
        }

        public async void ContinueBtnClicked()
        {
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
                await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                return;
            }

            if (SelectedIdNumber == string.Empty || SelectedIdNumber == AppResources.IDNumber)
            {
                popUp.Message = AppResources.ZVatRefundInformationSelectIBANIDNumber;
                await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                return;
            }

            if(SelectedIbanData == null)
            {
                popUp.Message = AppResources.ZVatRefundInformationSelectIBAN;
                await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                return;
            }

            VatRefundsDisplayDataModel.Operationx = "05";
            VatRefundsDisplayDataModel.Gpartx = App.LoginDataRetrieved.TIN;
            VatRefundsDisplayDataModel.Langx = UtilityManager.GetLanguageParameter();
            VatRefundsDisplayDataModel.Iban = SelectedIbanData.Iban;
            VatRefundsDisplayDataModel.IbanC = SelectedIbanData.Iban;
            VatRefundsDisplayDataModel.Idnumber = SelectedIdNumber;
            VatRefundsDisplayDataModel.Idnum = SelectedIdNumber;
            VatRefundsDisplayDataModel.IdType = SelectedIDTypeCode;
            VatRefundsDisplayDataModel.Idtype = SelectedIDTypeCode;
            VatRefundsDisplayDataModel.RefundTp = AppResources.VATRefundsRequest;
            VatNewReqSummaryData.Confirmfg = "X";
            VatNewReqSummaryData.TcFg = "X";

            //VatRefundsDisplayDataModel.Statusx = "E0013";
            VatRefundsDisplayDataModel.TxnTpx = "CRE_VTRF";

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VatNewReqSummaryData = await WebServiceManager.GAZTVATRefundSubmitRequest(VatRefundsDisplayDataModel);

                Device.BeginInvokeOnMainThread(async () =>
                {
                   _navigationService.NavigateTo(App.VATRefundDetailsPageView, VatNewReqSummaryData);
                });
            }
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                Device.BeginInvokeOnMainThread(async () =>
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

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(message, AppResources.Information);
                });
            }

        }

        public async void OnVoidBtnClicked()
        {
            VatRefundsDisplayDataModel.Operationx = "04";
            VatRefundsDisplayDataModel.Gpartx = App.LoginDataRetrieved.TIN;
            VatRefundsDisplayDataModel.Langx = UtilityManager.GetLanguageParameter();

            VatRefundsDisplayDataModel.RefundTp = AppResources.VATRefundsRequest;

            //VatRefundsDisplayDataModel.Statusx = "E0013";
            VatRefundsDisplayDataModel.TxnTpx = "CRE_VTRF";

            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = true;
                });

                VatNewReqSummaryData = await WebServiceManager.GAZTVATRefundSubmitRequest(VatRefundsDisplayDataModel);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    _navigationService.GoBack();
                });
            }
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                Device.BeginInvokeOnMainThread(async () =>
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

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(message, AppResources.Information);
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
