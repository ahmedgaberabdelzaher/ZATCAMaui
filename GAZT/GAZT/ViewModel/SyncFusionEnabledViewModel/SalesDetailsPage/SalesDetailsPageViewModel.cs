using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnDetailsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage_ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.SalesDetailsPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class SalesDetailsPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        //  public ICommand OnBillsButtonClicked { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand OnAcceptReturnButtonClicked { get; set; }
        public ICommand OnAmendReturnButtonClicked { get; set; }
        public ICommand OnSubmitButtonClicked { get; set; }
        public ICommand OnConfirmButtonClicked { get; set; }
        public ICommand OnRefreshButtonClicked { get; set; }
        public ICommand OnCloseButtonClicked { get; set; }
        List<EstimateZakatAttachment> EstimateZakatAttachmentList = new List<EstimateZakatAttachment>();
        public static bool IsComingFromSalesDetailsPage = false;
        public ObservableCollection<SalesDetails> SalesDetailsDataList { get; set; }// To Store the response data to compare the changed object
        public static string RetGuid;
        public ZakatReturnDetails zakatReturnDetailsD { get; set; }
        public ZakatReturnDetails zakatReturnDetailsDToCompare = new ZakatReturnDetails();
        ZakatReturnDetails _zakatReturnDetails = new ZakatReturnDetails();
        public bool IsCurrentZAKATTaxLess = false;
        public double existingZakatBase = 0.00;
        #endregion
        #region Property
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
        private SalesDetails _selectedSalesDetails;
        public SalesDetails SelectedSalesDetails
        {
            get
            {
                return _selectedSalesDetails;
            }
            set
            {
                _selectedSalesDetails = value;
                try
                {
                    if(!ConfirmButtonVisibility)
                    {
                        double d = Convert.ToDouble(zakatReturnDetailsD.d.TvtslI);
                        double d1 = Convert.ToDouble(zakatReturnDetailsD.d.ThresholdSet.results[0].Value);
                        bool IsThresholdGreaterLessVATAmount = d1 < d;
                        if (Convert.ToDouble(zakatReturnDetailsD.d.TvtslE) > Convert.ToDouble(zakatReturnDetailsD.d.ThresholdSet.results[0].Value))
                        {
                            if ((_selectedSalesDetails != null) && (SubmitButtonVisibility))
                            {
                                if (SelectedSalesDetails.SalesType.Equals(AppResources.ZZTotalVATSales) ||  SelectedSalesDetails.SalesType.Equals(AppResources.ZZCapitalamount))
                                {
                                    _navigationService.NavigateTo(App.AmendSalesDetailsPageView, SelectedSalesDetails);
                                }
                            }
                        }
                        else
                        {
                            if ((_selectedSalesDetails != null) && (SubmitButtonVisibility))
                            {
                                if (SelectedSalesDetails != null && !SelectedSalesDetails.SalesType.Equals(AppResources.ZZTotalVATSales)  || SelectedSalesDetails.SalesType.Equals(AppResources.ZZCapitalamount))
                                {
                                    _navigationService.NavigateTo(App.AmendSalesDetailsPageView, SelectedSalesDetails);
                                }
                            }
                        }
                    }
                    else
                    {
                        ShowOnlyInfoIcon();
                    }
                }
                catch(Exception ex)
                {
                }
                //if ((_selectedSalesDetails != null) && (SubmitButtonVisibility || ConfirmButtonVisibility))
                //{
                //    _navigationService.NavigateTo(App.AmendSalesDetailsPageView, SelectedSalesDetails);
                //}
                //RaisePropertyChanged("SelectedSalesDetails");
            }
        }
        private ZakatReturnDetails _zakatReturnDetail;
        public ZakatReturnDetails ZakatReturnDetail
        {
            get
            {
                return _zakatReturnDetail;
            }
            set
            {
                _zakatReturnDetail = value;
                RaisePropertyChanged("ZakatReturnDetail");
            }
        }
        private ObservableCollection<SalesDetails> _SalesDetailsList;
        public ObservableCollection<SalesDetails> SalesDetailsList
        {
            get
            {
                return _SalesDetailsList;
            }
            set
            {
                _SalesDetailsList = value;
                RaisePropertyChanged("SalesDetailsList");
            }
        }
        private string _persl;
        public string Persl
        {
            get
            {
                return _persl;
            }
            set
            {
                _persl = value;
                RaisePropertyChanged("Persl");
            }
        }
        private string _abrzu;
        public string Abrzu
        {
            get
            {
                return _abrzu;
            }
            set
            {
                _abrzu = value;
                RaisePropertyChanged("Abrzu");
            }
        }
        private string _abrzo;
        public string Abrzo
        {
            get
            {
                return _abrzo;
            }
            set
            {
                _abrzo = value;
                RaisePropertyChanged("Abrzo");
            }
        }
        private string _fbnum;
        public string Fbnum
        {
            get
            {
                return _fbnum;
            }
            set
            {
                _fbnum = value;
                RaisePropertyChanged("Fbnum");
            }
        }
        private string _estsl;
        public string Estsl
        {
            get
            {
                return _estsl;
            }
            set
            {
                _estsl = value;
                RaisePropertyChanged("Estsl");
            }
        }
        private bool _amedmentButtonVisibility = true;
        public bool AmedmentButtonVisibility
        {
            get
            {
                return _amedmentButtonVisibility;
            }
            set
            {
                _amedmentButtonVisibility = value;
                RaisePropertyChanged("AmedmentButtonVisibility");
            }
        }
        private bool _submitButtonVisibility = false;
        public bool SubmitButtonVisibility
        {
            get
            {
                return _submitButtonVisibility;
            }
            set
            {
                _submitButtonVisibility = value;
                RaisePropertyChanged("SubmitButtonVisibility");
            }
        }
        private bool _desClaimerVisibility = true;
        public bool DesClaimerVisibility
        {
            get
            {
                return _desClaimerVisibility;
            }
            set
            {
                _desClaimerVisibility = value;
                RaisePropertyChanged("DesClaimerVisibility");
            }
        }
        // For Invoice Pop up
        private EstimatedZAKATReturnsSADADNumberResult _estimatedZAKATSADADNumber;
        public EstimatedZAKATReturnsSADADNumberResult EstimatedZAKATSADADNumber
        {
            get
            {
                return _estimatedZAKATSADADNumber;
            }
            set
            {
                _estimatedZAKATSADADNumber = value;
                RaisePropertyChanged("EstimatedZAKATSADADNumber");
            }
        }
        private bool _invoicePopUpVisibility = false;
        public bool InvoicePopUpVisibility
        {
            get
            {
                return _invoicePopUpVisibility;
            }
            set
            {
                _invoicePopUpVisibility = value;
                RaisePropertyChanged("InvoicePopUpVisibility");
            }
        }
        private bool _checkBoxStatus = false;
        public bool CheckBoxStatus
        {
            get
            {
                return _checkBoxStatus;
            }
            set
            {
                _checkBoxStatus = value;
                RaisePropertyChanged("CheckBoxStatus");
            }
        }
        private bool _confirmButtonVisibility = false;
        public bool ConfirmButtonVisibility
        {
            get
            {
                return _confirmButtonVisibility;
            }
            set
            {
                _confirmButtonVisibility = value;
                RaisePropertyChanged("ConfirmButtonVisibility");
            }
        }
        private Color _refreshButtonDisableColor =  (Color)Application.Current.Resources["Primary"];
        public Color RefreshButtonDisableColor
        {
            get
            {
                return _refreshButtonDisableColor;
            }
            set
            {
                _refreshButtonDisableColor = value;
                RaisePropertyChanged("RefreshButtonDisableColor");
            }
        }
            private bool _objectionInvoicePopUpVisibility = false;
        public bool ObjectionInvoicePopUpVisibility
        {
            get
            {
                return _objectionInvoicePopUpVisibility;
            }
            set
            {
                _refreshiButtonDisability = value;
                RaisePropertyChanged("_objectionInvoicePopUpVisibility");
            }
        }
        private bool _refreshiButtonDisability = true;
        public bool RefreshiButtonDisability
        {
            get
            {
                return _refreshiButtonDisability;
            }
            set
            {
                _refreshiButtonDisability = value;
                RaisePropertyChanged("RefreshiButtonDisability");
            }
        }
        #endregion
        #region Constructor
        public SalesDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            OnAcceptReturnButtonClicked = new Command(async () =>
            {
                _navigationService.NavigateTo(App.BillDetailsPageView, zakatReturnDetailsD.d);
                    CheckBoxStatus = false;
            });
            GoBackClick = new Command(async () =>
            {
                if (!IsLoading)
                {
                    _navigationService.GoBack();
                }
            });
            OnAmendReturnButtonClicked = new Command(async () =>
            {
                try
                {
                    ShowSubmitButton();
                    SetSalesDetailsData(zakatReturnDetailsD);
                    ShowDisclaimer();
                    ShowEditIcon();
                    CheckBoxStatus = false;
                    //  _navigationService.NavigateTo(App.AmendSalesDetailsPageView, SelectedSalesDetails);
                }
                catch (Exception ex)
                {
                }
            });

            OnSubmitButtonClicked = new Command(async () =>
            {
                try
                {
                    bool IsValueChange = GetEstimatedZAKATValueChangeStatus();
                    if (IsValueChange)
                    {
                        if (CheckBoxStatus)
                        {
                           // SetUpdatedDataToZAKATEstimated();
                            // AssignAttachmentToPostDataObject();
                            string PostOperationID = "05";
                            await SubmitZakatReturn(PostOperationID,"");
                            CheckBoxStatus = false;
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () => {
                                await _dialogService.ShowMessageBox(AppResources.ZZPleaseselectthedisclaimercheckboxbeforesubmit, AppResources.Alerts);
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await _dialogService.ShowMessageBox(AppResources.ZZNochangesmadeFormcannotbesubmitted, AppResources.Alerts);
                        });
                    }
                }
                catch(Exception ex)
                {
                    IsLoading = false;
                }
            });
            OnConfirmButtonClicked = new Command(async () =>
            {
               // OnConfirmClicked(); 
            });
            //OnRefreshButtonClicked = new Command(async () =>
            //{
            //    await GetSADADNumber();
            //});
            //OnCloseButtonClicked = new Command(async () =>
            //{
            //    InvoicePopUpVisibility = false;
            //    _navigationService.GoBack();
            //});
        }
        #endregion
        #region Method
        public async Task OnConfirmClicked(string InvFlag)
        {
          //  ShowDisclaimer();
          //if(IsCurrentZAKATTaxLess)
          //  {
          //      if (CheckBoxStatus)
          //      {
          //          string PostOperationID = GetConfirmOperationId();
          //          //  string PostOperationID = "66";
          //          await SubmitZakatReturn(PostOperationID, InvFlag);
          //          CheckBoxStatus = false;
          //      }
          //      else
          //      {
          //          await _dialogService.ShowMessageBox(AppResources.ZZPleaseselectthedisclaimercheckboxbeforesubmit, AppResources.Alerts);
          //      }
          //  }
          //else
          //  {
                string PostOperationID = GetConfirmOperationId();
                //  string PostOperationID = "66";
                await SubmitZakatReturn(PostOperationID, InvFlag);
            //    CheckBoxStatus = false;
            //}
        }
        public void onPageLoad()
        {
            try
            {
                existingZakatBase = Convert.ToDouble(zakatReturnDetailsDToCompare.d.Zkamt);
                ShowDisclaimer();
                CheckBoxStatus = false;
                HideEditIcon();
                HideConfirmButton();
                ZakatReturnDetail = zakatReturnDetailsD;
                Persl = ZakatReturnDetail.d.Persl;
                Abrzu = ZakatReturnListPageViewModel.ReturnPeriod;
                //if (App.IsArabic)
                //{
                //    Abrzu  = JsonConvert.DeserializeObject<DateTime>(@"""" + ZakatReturnDetail.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //    Abrzu =  UtilityManager.ToArabicDate(Abrzu);
                //    Abrzo = JsonConvert.DeserializeObject<DateTime>(@"""" + ZakatReturnDetail.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //    Abrzo = UtilityManager.ToArabicDate(Abrzo);
                //    Abrzu = Abrzu + "  " + AppResources.To + "  " + Abrzo;
                //    // itemCR.Udate = UtilityManager.ToArabicDate(itemCR.Udate);
                //}
                //else
                //{
                //    Abrzu = JsonConvert.DeserializeObject<DateTime>(@"""" + ZakatReturnDetail.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //    Abrzo = JsonConvert.DeserializeObject<DateTime>(@"""" + ZakatReturnDetail.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //    Abrzu = Abrzu + "  " + AppResources.To + "  " + Abrzo;
                //}
                //Abrzu = UtilityManager.SingleDateConversion(ZakatReturnDetail.d.Abrzu);
                //Abrzo = UtilityManager.SingleDateConversion(ZakatReturnDetail.d.Abrzo);
                Fbnum = ZakatReturnDetail.d.Fbnum;
                Estsl = UtilityManager.GetCommaSeparatedAmount(ZakatReturnDetail.d.Estsl);
                RetGuid = zakatReturnDetailsD.d.ReturnId;
                SetSalesDetailsData(zakatReturnDetailsD);
                //if(ZakatReturnDetailsPageViewModel.IsAmendButtonPressed)
                //if (ZakatReturnDetail.d.Statusz.Equals("E0001") || ZakatReturnDetail.d.Statusz.Equals("IP011"))
                //{
                //        //HideAllButton();
                //}
                //else if (ZakatReturnDetail.d.Statusz.Equals("IP014") || ZakatReturnDetail.d.Statusz.Equals("E0002") || ZakatReturnDetail.d.Statusz.Equals("E0003"))
                //  //  _navigationService.NavigateTo(App.SalesDetailsPageView, ZakatReturnDetails);
                //}
                //else
                //{
                //   // _navigationService.NavigateTo(App.BillDetailsPageView, ZakatReturnDetails);
                //}
                if (ZakatReturnDetailsPageViewModel.IsAmendButtonPressed)
                {
                    ShowSubmitButton();
                   ShowEditIcon();
                }
                else if (ZakatReturnDetail.d.Statusz.Equals("E0001") || ZakatReturnDetail.d.Statusz.Equals("IP011"))// UnSubmitted
                {
                   HideAllButton();
                   HideDisclaimer();
                }
                else if (ZakatReturnDetail.d.Statusz.Equals("E0004"))// Amend without Objection
                {
                    HideAllButton();
                    HideDisclaimer();
                }
                else if (ZakatReturnDetail.d.Statusz.Equals("E0008"))
                {
                    HideAllButton();
                    HideDisclaimer();
                    // ShowAcceptAndAmendButton();
                }
                else if (ZakatReturnDetail.d.Statusz.Equals("E0005"))// In Processing
                {
                    HideAllButton();
                    HideDisclaimer();
                    // ShowAcceptAndAmendButton();
                }
                else if (ZakatReturnDetail.d.Statusz.Equals("E0003"))// In Paid state 
                {
                    ShowAcceptAndAmendButton();
                    HideDisclaimer();
                    // ShowAcceptAndAmendButton();
                }
                else if (ZakatReturnDetail.d.Statusz.Equals("E0011"))// In Paid state 
                {
                    ShowAcceptAndAmendButton();
                    HideDisclaimer();
                    // ShowAcceptAndAmendButton();
                }
                else if (ZakatReturnDetail.d.Statusz.Equals("E0002"))// Status when the return released by GAZT officer
                {
                    ShowAcceptAndAmendButton();
                    HideDisclaimer();
                    // ShowAcceptAndAmendButton();
                }
                else if (ZakatReturnDetail.d.Statusz.Equals(""))// Status when the return released by GAZT officer
                {
                    HideAllButton();
                    HideDisclaimer();
                    // ShowAcceptAndAmendButton();
                }
                else
                {
                    ShowAcceptAndAmendButton();
                   // HideEditIcon();Fwebser
                }
                if (ZakatReturnDetailsPageViewModel.IsAmendButtonClicked)
                {
                    OnAmendReturnClicked();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private async Task SubmitZakatReturn(String PostOperation, string InvFlag)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
            try
            {
                    WebServiceManager.ErrorMessage = string.Empty;
                    if (PostOperation.Equals("66") || PostOperation.Equals("65"))
                    {
                        _zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(zakatReturnDetailsD, PostOperation);
                        if (_zakatReturnDetails != null && _zakatReturnDetails.d != null)
                        {
                            HideDisclaimer();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () => {
                                if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                            {
                                await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                                WebServiceManager.ErrorMessage = string.Empty;
                            }
                            else
                            {
                                await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                                _navigationService.GoBack();
                                WebServiceManager.ErrorMessage = string.Empty;
                            }
                            });
                        }
                    }
                    else
                    {
                        ZakatReturnDetails UpdatedPostData =   GetPostDataAfterRemovingComma(zakatReturnDetailsD);
                      //  zakatReturnDetailsD.d.Cpamt = SalesDetailsList[7].InformationFromPartie.Replace(",", "");
                         _zakatReturnDetails = await WebServiceManager.GAZTSaveZakatReturnData(UpdatedPostData, PostOperation);
                        if(_zakatReturnDetails != null && _zakatReturnDetails.d != null)
                        {
                            HideDisclaimer();
                            Estsl = UtilityManager.GetCommaSeparatedAmount(_zakatReturnDetails.d.Estsl); 
                          //  IsCurrentZAKATTaxLess = existingZakatBase >= Convert.ToDouble(_zakatReturnDetails.d.Zkamt);
                            if(existingZakatBase > Convert.ToDouble(_zakatReturnDetails.d.Zkamt))
                            {
                                IsCurrentZAKATTaxLess = true;
                            }
                            else
                            {
                                IsCurrentZAKATTaxLess = false;
                            }
                            AssignCalculatedValueAfterSubmission();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () => {
                                if (string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                                {
                                    await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                    _navigationService.GoBack();
                                    WebServiceManager.ErrorMessage = string.Empty;
                                }
                                else
                                {
                                    await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                                    _navigationService.GoBack();
                                    WebServiceManager.ErrorMessage = string.Empty;
                                }
                            });
                        }
                    }
                    if (_zakatReturnDetails != null && _zakatReturnDetails.d != null)
                    {
                            if (PostOperation.Equals("66") || PostOperation.Equals("65"))
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    HideAllButton();
                                    await _dialogService.ShowMessageBox(AppResources.ZZReturnSubmittedSuccessfully, AppResources.Information);
                                    IsComingFromSalesDetailsPage = true;
                                    _navigationService.NavigateTo(App.BillDetailsPageView, zakatReturnDetailsD.d);
                        });
                    }
                            if (PostOperation.Equals("05"))
                            {
                                if (Convert.ToDouble(_zakatReturnDetails.d.Zkamt) >= existingZakatBase)//existingZakatBase
                                    {
                                        Estsl = UtilityManager.GetCommaSeparatedAmount(_zakatReturnDetails.d.Estsl);
                                        ShowOnlyInfoIcon();
                                        ShowConfirmButton();
                                        SetSalesDetailsData(_zakatReturnDetails);
                                        HideDisclaimer();
                            }
                            else
                            {
                                ShowDisclaimer();
                                SetChangedValueToUploadAttachment();
                                bool ISAllRequiredDocumentUploadedwithReason = IsAllRequiredAttachmentUploaded();
                                if (ISAllRequiredDocumentUploadedwithReason)
                                {
                                    Estsl = UtilityManager.GetCommaSeparatedAmount(_zakatReturnDetails.d.Estsl);
                                    ShowConfirmButton();
                                    SetSalesDetailsData(_zakatReturnDetails);
                                    // ShowEditIcon();// Commented 
                                    ShowOnlyInfoIcon();
                                    HideDisclaimer();
                                }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () => {
                                    await _dialogService.ShowMessageBox(AppResources.ZZPleaseuploadtheRequiredDocumentandChangereason, AppResources.Information);
                                });
                            }
                        }
                    }
                }
                else
                {
                }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                    });
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }

        public void SetUpdatedDataToZAKATEstimated(int selectedIndex)
        {
            try
            {
                //for (int i = 0; i < SalesDetailsList.Count; i++)
                //{
                    if (SalesDetailsList[selectedIndex].SelectedEditFieldId.Equals("1")) 
                    {
                        zakatReturnDetailsD.d.TvtslI = string.IsNullOrEmpty(SalesDetailsList[0].NewValue) ? "0.00" : SalesDetailsList[0].NewValue;
                        zakatReturnDetailsD.d.TvtslResn = SalesDetailsList[0].ChangeReason;
                        AddAttachmetToPostData(0, SalesDetailsList[0].estimateZakatAttachment);
                        //if (SalesDetailsList[0].estimateZakatAttachment.Doguid != null)
                        //{
                        //    EstimateZakatAttachmentList.Add(SalesDetailsList[0].estimateZakatAttachment); //  EstimateZakatAttachmentList.Add(SalesDetailsList[0].estimateZakatAttachment);
                        //}
                    }
                    else if (SalesDetailsList[selectedIndex].SelectedEditFieldId.Equals("2"))
                    {
                        zakatReturnDetailsD.d.LabnoI = string.IsNullOrEmpty(SalesDetailsList[1].NewValue) ? "0.00" : SalesDetailsList[1].NewValue;
                        zakatReturnDetailsD.d.LabnoResn = SalesDetailsList[1].ChangeReason;
                        AddAttachmetToPostData(1, SalesDetailsList[1].estimateZakatAttachment);
                        //if(SalesDetailsList[1].estimateZakatAttachment.Doguid != null)
                        //{
                        //    EstimateZakatAttachmentList.Add(SalesDetailsList[1].estimateZakatAttachment); //  EstimateZakatAttachmentList.Add(SalesDetailsList[0].estimateZakatAttachment);
                        //}
                    }
                    else if (SalesDetailsList[selectedIndex].SelectedEditFieldId.Equals("3"))
                    {
                        zakatReturnDetailsD.d.ImpvalI = string.IsNullOrEmpty(SalesDetailsList[2].NewValue) ? "0.00" : SalesDetailsList[2].NewValue;
                        zakatReturnDetailsD.d.ImpvalResn = SalesDetailsList[2].ChangeReason;
                        AddAttachmetToPostData(2, SalesDetailsList[2].estimateZakatAttachment);
                        //if (SalesDetailsList[2].estimateZakatAttachment.Doguid != null)
                        //{
                        //    EstimateZakatAttachmentList.Add(SalesDetailsList[2].estimateZakatAttachment); //  EstimateZakatAttachmentList.Add(SalesDetailsList[0].estimateZakatAttachment);
                        //}
                    }
                    else if (SalesDetailsList[selectedIndex].SelectedEditFieldId.Equals("4"))
                    {
                        zakatReturnDetailsD.d.PtoslI = string.IsNullOrEmpty(SalesDetailsList[3].NewValue) ? "0.00" : SalesDetailsList[3].NewValue;
                        zakatReturnDetailsD.d.PtoslResn = SalesDetailsList[3].ChangeReason;
                        AddAttachmetToPostData(3, SalesDetailsList[3].estimateZakatAttachment);
                        //if (SalesDetailsList[3].estimateZakatAttachment.Doguid != null)
                        //{
                        //    EstimateZakatAttachmentList.Add(SalesDetailsList[3].estimateZakatAttachment); //  EstimateZakatAttachmentList.Add(SalesDetailsList[0].estimateZakatAttachment);
                        //}
                    }
                    else if (SalesDetailsList[selectedIndex].SelectedEditFieldId.Equals("5"))
                    {
                        zakatReturnDetailsD.d.EtimadI = string.IsNullOrEmpty(SalesDetailsList[4].NewValue) ? "0.00" : SalesDetailsList[4].NewValue;
                        zakatReturnDetailsD.d.EtimadResn = SalesDetailsList[4].ChangeReason;
                        AddAttachmetToPostData(4, SalesDetailsList[4].estimateZakatAttachment);
                        //if (SalesDetailsList[4].estimateZakatAttachment.Doguid != null)
                        //{
                        //    EstimateZakatAttachmentList.Add(SalesDetailsList[4].estimateZakatAttachment); //  EstimateZakatAttachmentList.Add(SalesDetailsList[0].estimateZakatAttachment);
                        //}
                    }
                    else if (SalesDetailsList[selectedIndex].SelectedEditFieldId.Equals("6"))
                    {
                        zakatReturnDetailsD.d.ExamtI = string.IsNullOrEmpty(SalesDetailsList[5].NewValue) ? "0.00" : SalesDetailsList[5].NewValue;
                        zakatReturnDetailsD.d.ExamtResn = SalesDetailsList[5].ChangeReason;
                        AddAttachmetToPostData(5, SalesDetailsList[5].estimateZakatAttachment);
                        //if (SalesDetailsList[5].estimateZakatAttachment.Doguid != null)
                        //{
                        //    EstimateZakatAttachmentList.Add(SalesDetailsList[5].estimateZakatAttachment); //  EstimateZakatAttachmentList.Add(SalesDetailsList[0].estimateZakatAttachment);
                        //}
                    }
                    else if (SalesDetailsList[selectedIndex].SelectedEditFieldId.Equals("7"))
                    {
                        zakatReturnDetailsD.d.PramtI = string.IsNullOrEmpty(SalesDetailsList[6].NewValue) ? "0.00" : SalesDetailsList[6].NewValue;
                        zakatReturnDetailsD.d.PramtResn = SalesDetailsList[6].ChangeReason;
                        AddAttachmetToPostData(6, SalesDetailsList[6].estimateZakatAttachment);
                        //if (SalesDetailsList[6].estimateZakatAttachment.Doguid != null)
                        //{
                        //    EstimateZakatAttachmentList.Add(SalesDetailsList[6].estimateZakatAttachment); //  EstimateZakatAttachmentList.Add(SalesDetailsList[0].estimateZakatAttachment);
                        //}
                    }
                    else if (SalesDetailsList[selectedIndex].SelectedEditFieldId.Equals("8"))
                    {
                        zakatReturnDetailsD.d.Cpamt = string.IsNullOrEmpty(SalesDetailsList[7].NewValue) ? "0.00" : SalesDetailsList[7].NewValue;
                        zakatReturnDetailsD.d.CpamtResn = SalesDetailsList[7].ChangeReason;
                        AddAttachmetToPostData(7, SalesDetailsList[7].estimateZakatAttachment);
                        //if (SalesDetailsList[7].estimateZakatAttachment.Doguid != null)
                        //{
                        //    EstimateZakatAttachmentList.Add(SalesDetailsList[7].estimateZakatAttachment); //  EstimateZakatAttachmentList.Add(SalesDetailsList[0].estimateZakatAttachment);
                        //}
                    }
               // }
            }
            catch (Exception ex)
            {
            }
            AttachSet attachSet = new AttachSet();
            attachSet.results = EstimateZakatAttachmentList;
            zakatReturnDetailsD.d.AttachSet = attachSet;
        }
        private void ShowAcceptAndAmendButton()
        {
            AmedmentButtonVisibility = true;
            SubmitButtonVisibility = false;
        }
        public void ShowOnlyInfoIcon()
        {
            ObservableCollection<SalesDetails> _salesDetailsList = new ObservableCollection<SalesDetails>();
            if (SalesDetailsList != null)
            {
                foreach (SalesDetails salesDetails in SalesDetailsList)
                {
                    salesDetails.EditImageSource = "";
                    _salesDetailsList.Add(salesDetails);
                }
            }
            SalesDetailsList = _salesDetailsList;
        }
        private void ShowEditIcon()
        {
            ObservableCollection<SalesDetails> _salesDetailsList = new ObservableCollection<SalesDetails>();
            if (SalesDetailsList != null)
            {
                bool isVATAmountGreaterThanThreshold = IsVATAmountGreaterThanThreshold();
                foreach (SalesDetails salesDetails in SalesDetailsList)
                {
                    if(isVATAmountGreaterThanThreshold)
                    {
                        if (salesDetails.SelectedEditFieldId.Equals("1") || salesDetails.SelectedEditFieldId.Equals("8"))
                        {
                            salesDetails.EditImageSource = "ic_edit_gray.png";
                            _salesDetailsList.Add(salesDetails);
                        }
                        else
                        {
                            salesDetails.InformationIconVisibility = true;
                            salesDetails.SeparatorVisibility = true;
                            _salesDetailsList.Add(salesDetails);
                        }
                    }
                    else
                    {
                        if (!salesDetails.SelectedEditFieldId.Equals("1"))
                        {
                            salesDetails.InformationIconVisibility = true;
                            salesDetails.SeparatorVisibility = true;
                            salesDetails.EditImageSource = "ic_edit_gray.png";
                            _salesDetailsList.Add(salesDetails);
                        }
                        else
                        {
                            _salesDetailsList.Add(salesDetails);
                        }
                    }
                }
            }
            SalesDetailsList = _salesDetailsList;
        }
        public void HideEditIcon()
        {
            ObservableCollection<SalesDetails> _salesDetailsList = new ObservableCollection<SalesDetails>();
            if (SalesDetailsList != null)
            {
                foreach (SalesDetails salesDetails in SalesDetailsList)
                {
                    salesDetails.EditImageSource = "";
                    _salesDetailsList.Add(salesDetails);
                }
                SalesDetailsList = _salesDetailsList;
            }
        }
        private void ShowSubmitButton()
        {
            AmedmentButtonVisibility = false;
            SubmitButtonVisibility = true;
        }
        private void HideAllButton()
        {
            AmedmentButtonVisibility = false;
            SubmitButtonVisibility = false;
            ConfirmButtonVisibility = false;
        }
        private void ShowConfirmButton()
        {
            SubmitButtonVisibility = false;
            ConfirmButtonVisibility = true;
        }
        private void HideConfirmButton()
        {
            ConfirmButtonVisibility = false;
        }
        private bool GetEstimatedZAKATValueChangeStatus()
        {
            bool IsPreviousValueChanged = false;
            for (int i = 0; i < SalesDetailsList.Count; i++)
            {
                if (SalesDetailsList[i].IsOldValueChanged)
                {
                    IsPreviousValueChanged = true;
                    break;
                }
            }
            return IsPreviousValueChanged;
        }
        private bool IsZAKATBaseAMountGreaterAfterAmendment()
        {
            bool _isZAKATBaseAMountGreaterAfterAmendment = false;
            double PreviousZAKATAmount = 0.00;
            double ZAKATAmountAfterAmendment = 0.00;
            if (ZAKATAmountAfterAmendment >= PreviousZAKATAmount)
            {
                _isZAKATBaseAMountGreaterAfterAmendment = true;
            }
            else
            {
                _isZAKATBaseAMountGreaterAfterAmendment = false;
            }
            return _isZAKATBaseAMountGreaterAfterAmendment;
        }
        public void SetChangedValueToUploadAttachment()
        {
            try
            {
                ObservableCollection<SalesDetails> _salesDetailsList = new ObservableCollection<SalesDetails>();
                
                for (int i = 0; i < SalesDetailsList.Count; i++)
                {
                    if (SalesDetailsList[i].IsOldValueChanged)
                    {
                        bool IsNewValueLessThanExisting = (Convert.ToDouble(SalesDetailsList[i].InformationFromPartie) > Convert.ToDouble(SalesDetailsDataList[i].InformationFromPartieToCompare));
                        if (IsNewValueLessThanExisting)//
                        {
                            //if(!(SalesDetailsList[i].IsReasonRequird || SalesDetailsList[i].IsAttachmentRequired))
                            //{
                            if(SalesDetailsList[i].estimateZakatAttachment.Count == 0 || string.IsNullOrEmpty(SalesDetailsList[i].ChangeReason))
                            {
                                SalesDetailsList[i].EditImageSource = "ic_Edit_red.png";
                                SalesDetailsList[i].IsAttachmentRequired = true;
                                SalesDetailsList[i].IsReasonRequird = true;
                                _salesDetailsList.Add(SalesDetailsList[i]);
                            }
                            else
                            {
                                SalesDetailsList[i].EditImageSource = "ic_edit_gray.png";
                                SalesDetailsList[i].IsAttachmentRequired = true;
                                SalesDetailsList[i].IsReasonRequird = true;
                                _salesDetailsList.Add(SalesDetailsList[i]);
                            }
                            //}
                        }
                        else
                        {
                            _salesDetailsList.Add(SalesDetailsList[i]);
                        }
                    }
                    else
                    {
                        _salesDetailsList.Add(SalesDetailsList[i]);
                    }
                }
                SalesDetailsList = _salesDetailsList;
            }
            catch(Exception ex)
            {
            }
        }
        private bool IsAllRequiredAttachmentUploaded()
        {
            bool isAllDocumentUploaded = true;
            for (int i = 0; i < SalesDetailsList.Count; i++)
            {
                if (SalesDetailsList[i].IsReasonRequird || SalesDetailsList[i].IsAttachmentRequired)
                {
                    if (string.IsNullOrEmpty(SalesDetailsList[i].ChangeReason))
                    {
                        isAllDocumentUploaded = false;
                    }
                    if(SalesDetailsList[i].estimateZakatAttachment.Count == 0)
                    {
                        isAllDocumentUploaded = false;
                    }
                }
                if(!isAllDocumentUploaded)
                {
                    break;
                }
            }
            return isAllDocumentUploaded;
        }
        //private void AssignAttachmentToPostDataObject()
        //{
        //    try
        //    {
        //        List<EstimateZakatAttachment> listOfAttachments = new List<EstimateZakatAttachment>();
        //        for (int i = 0; i < SalesDetailsList.Count; i++)
        //        {
        //            if (SalesDetailsList !=null &&  SalesDetailsList[i].estimateZakatAttachment != null)
        //            {
        //                listOfAttachments.Add(SalesDetailsList[i].estimateZakatAttachment);
        //            }
        //        }
        //        zakatReturnDetailsD.d.AttachSet.results = listOfAttachments;
        //    }
        //    catch(Exception ex)
        //    {
        //    }
        //}
        //public async Task GetSADADNumber()
        //{
        //    await Task.Run(() =>
        //    {
        //        IsLoading = true;
        //    });
        //    await Task.Run(async() =>
        //    {
        //        try
        //        {
        //            EstimatedZAKATReturnsSADADNumber estimatedZAKATReturnsSADADNumber = await WebServiceManager.GAZTGetEstimatedZakatReturnSADADNumber(zakatReturnDetailsD.d.Fbnum, ZakatReturnDetailsPageViewModel.Fbguid); // Method to get the invoice
        //           // PopToRootPage();
        //            if (estimatedZAKATReturnsSADADNumber != null && estimatedZAKATReturnsSADADNumber.d != null)
        //            {
        //                // RefreshiButtonDisability = false;
        //                RefreshButtonDisableColor =  (Color)Application.Current.Resources["ButtonGray"];
        //                EstimatedZAKATSADADNumber = estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0];
        //                //if(Convert.ToDouble(EstimatedZAKATSADADNumber.Undisamt) > 0)
        //                //{
        //                //}
        //                //else
        //                //{
        //                //    ShowAmendInvoicePopUp();
        //                //}
        //                HideAllButton();
        //                UncheckDisclaimer();
        //            }
        //        }
        //        catch (InternetException ex)
        //        {
        //            Device.BeginInvokeOnMainThread(async () =>
        //            {
        //                 _dialogService.ShowMessage(ex.Message, AppResources.Information);
        //            });
        //        }
        //    });
        //    await Task.Run(() =>
        //    {
        //        IsLoading = false;
        //    });
        //}
        private void UncheckDisclaimer()
        {
            CheckBoxStatus = false;
        }
        private void HideDisclaimer()
        {
            DesClaimerVisibility = false;
        }
        public void ShowDisclaimer()
        {
            DesClaimerVisibility = true;
        }
        private void AddAttachmetToPostData(int index, ObservableCollection<EstimateZakatAttachment> attachmentList)
        {
            for(int i = 0; i< attachmentList.Count; i++)
            {
                EstimateZakatAttachmentList.Add(SalesDetailsList[index].estimateZakatAttachment[i]); //  EstimateZakatAttachmentList.Add(SalesDetailsList[0].estimateZakatAttachment);
            }
        }
        private string GetConfirmOperationId()
        {
            if(IsCurrentZAKATTaxLess)
            {
                return "66";// For Amendment
            }
            else
            {
                return "65";// For Objection
            }
        }
        private void ShowAmendInvoicePopUp()
        {
                        InvoicePopUpVisibility = true;
                        ObjectionInvoicePopUpVisibility = false;
        }
        private void ShowObjectionInvoicePopUp()
        {
                    InvoicePopUpVisibility = false;
                    ObjectionInvoicePopUpVisibility = true;
        }
        public void HideInvoicePopUp()
        {
            InvoicePopUpVisibility = false;
            ObjectionInvoicePopUpVisibility = false;
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFAnonymousLandingPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                    _navigation.NavigationStack.ToList().Clear();
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                });
            }
        }
        private void AssignCalculatedValueAfterSubmission()
        {
            zakatReturnDetailsD.d.Zbamt = _zakatReturnDetails.d.Zbamt;
            zakatReturnDetailsD.d.Zkamt = _zakatReturnDetails.d.Zkamt;
        }
        
        public void SetSalesDetailsData(ZakatReturnDetails zakatReturnResponse)
        {
            try
            {
                Color VATBackgroundColor;
                Color CapitalBackgroundColor;
                double d = Convert.ToDouble(zakatReturnDetailsD.d.TvtslI);
                double d1 = Convert.ToDouble(zakatReturnDetailsD.d.ThresholdSet.results[0].Value);
                bool IsThresholdGreaterLessVATAmount = d < d1;
                SalesDetailsList = new ObservableCollection<SalesDetails>();
                ObservableCollection<SalesDetails> SalesDetailsDummyList = new ObservableCollection<SalesDetails>();
                bool isVATAmountGreaterThanThreshold = IsVATAmountGreaterThanThreshold();
                SalesDetails salesDetails1 = new SalesDetails();
                salesDetails1.SalesType = AppResources.ZZTotalVATSales;
                salesDetails1.SeparatorVisibility = true;
                salesDetails1.InformationIconVisibility = true;
                salesDetails1.InformationFromPartieToCompare =  salesDetails1.InformationFromPartie = UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.TvtslI);// IsThresholdGreaterLessVATAmount ? AppResources.ZNA ://string.IsNullOrEmpty(zakatReturnDetails.d.TvtslI) ? "0.00" : zakatReturnDetailsD.d.TvtslI;
                salesDetails1.EstimateSales =  UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.TvtslE) + " " + AppResources.ZSAR;//IsThresholdGreaterLessVATAmount ? AppResources.ZNA ://string.IsNullOrEmpty(zakatReturnDetails.d.TvtslE) ? "0.00" : zakatReturnDetails.d.TvtslE;
                salesDetails1.SelectedEditFieldId = "1";
                salesDetails1.HelpIconVisibility = true;
                if (IsThresholdGreaterLessVATAmount)
                {
                    VATBackgroundColor = (Color)Application.Current.Resources["VATBgColor"];
                    CapitalBackgroundColor = Color.White;
                }
                else
                {
                    VATBackgroundColor = Color.White;
                    CapitalBackgroundColor = (Color)Application.Current.Resources["VATBgColor"];
                }
                salesDetails1.DisableItemBackgroundColor = VATBackgroundColor;
                SalesDetailsDummyList.Add(salesDetails1);
                SalesDetails salesDetails2 = new SalesDetails();
                salesDetails2.SalesType = AppResources.ZZAveragenumberoflabour;
                salesDetails2.InformationFromPartieToCompare = !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : salesDetails2.InformationFromPartie = UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.LabnoI);// string.IsNullOrEmpty(ZakatReturnDetail.d.LabnoI) ? "0.00" : ZakatReturnDetail.d.LabnoI; // ZakatReturnDetail.d.LabnoI;
                if(SubmitButtonVisibility && !IsThresholdGreaterLessVATAmount)
                {
                    salesDetails2.EstimateSales = "0.00";// !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.LabnoE) + " " + AppResources.ZSAR;// string.IsNullOrEmpty(zakatReturnDetails.d.LabnoE) ? "0.00" : zakatReturnDetails.d.LabnoE; //ZakatReturnDetail.d.LabnoE;
                }
                else
                {
                    salesDetails2.EstimateSales = !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.LabnoE) + " " + AppResources.ZSAR;// string.IsNullOrEmpty(zakatReturnDetails.d.LabnoE) ? "0.00" : zakatReturnDetails.d.LabnoE; //ZakatReturnDetail.d.LabnoE;
                }
                salesDetails2.SelectedEditFieldId = "2";
                salesDetails2.SeparatorVisibility = true;
                salesDetails2.InformationIconVisibility = true;
                salesDetails2.HelpIconVisibility = true;
                salesDetails2.DisableItemBackgroundColor = CapitalBackgroundColor;
                SalesDetailsDummyList.Add(salesDetails2);
                SalesDetails salesDetails3 = new SalesDetails();
                salesDetails3.SalesType = AppResources.ZZImportsvalue;
                salesDetails3.InformationFromPartieToCompare = !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : salesDetails3.InformationFromPartie = UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.ImpvalI);//string.IsNullOrEmpty(ZakatReturnDetail.d.ImpvalI) ? "0.00" : ZakatReturnDetail.d.ImpvalI; // ZakatReturnDetail.d.ImpvalI;
                salesDetails3.HelpIconVisibility = true;
                if (SubmitButtonVisibility && !IsThresholdGreaterLessVATAmount)
                {
                    salesDetails3.EstimateSales = "0.00";// !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.LabnoE) + " " + AppResources.ZSAR;// string.IsNullOrEmpty(zakatReturnDetails.d.LabnoE) ? "0.00" : zakatReturnDetails.d.LabnoE; //ZakatReturnDetail.d.LabnoE;
                }
                else
                {
                    salesDetails3.EstimateSales = !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.ImpvalE) + " " + AppResources.ZSAR;//string.IsNullOrEmpty(zakatReturnDetails.d.ImpvalE) ? "0.00" : zakatReturnDetails.d.ImpvalE; // ZakatReturnDetail.d.ImpvalE;
                }
                salesDetails3.SelectedEditFieldId = "3";
                salesDetails3.SeparatorVisibility = true;
                salesDetails3.InformationIconVisibility = true;
                salesDetails3.DisableItemBackgroundColor = CapitalBackgroundColor;
                SalesDetailsDummyList.Add(salesDetails3);
                SalesDetails salesDetails4 = new SalesDetails();
                salesDetails4.SalesType = AppResources.ZZSalesformpointofsales;
                salesDetails4.HelpIconVisibility = false;
                salesDetails4.InformationFromPartieToCompare = !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : salesDetails4.InformationFromPartie = UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.PtoslI);//string.IsNullOrEmpty(ZakatReturnDetail.d.PtoslI) ? "0.00" : ZakatReturnDetail.d.PtoslI; // ZakatReturnDetail.d.TvtslResn;
                if (SubmitButtonVisibility && !IsThresholdGreaterLessVATAmount)
                {
                    salesDetails4.EstimateSales = "0.00";// !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.LabnoE) + " " + AppResources.ZSAR;// string.IsNullOrEmpty(zakatReturnDetails.d.LabnoE) ? "0.00" : zakatReturnDetails.d.LabnoE; //ZakatReturnDetail.d.LabnoE;
                }
                else
                {
                    salesDetails4.EstimateSales = !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.Sumcnt) + " " + AppResources.ZSAR;//string.IsNullOrEmpty(zakatReturnDetails.d.Sumcnt) ? "0.00" : zakatReturnDetails.d.Sumcnt; // ZakatReturnDetail.d.TvtslResn;
                }
                salesDetails4.SeparatorVisibility = false;
                salesDetails4.InformationIconVisibility = false;
                salesDetails4.SelectedEditFieldId = "4";
                salesDetails4.DisableItemBackgroundColor = CapitalBackgroundColor;
                SalesDetailsDummyList.Add(salesDetails4);
                SalesDetails salesDetails5 = new SalesDetails();
                salesDetails5.SalesType = AppResources.ZZContractsformETIMADsystem;
                salesDetails5.InformationFromPartieToCompare = !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : salesDetails5.InformationFromPartie = UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.EtimadI);//string.IsNullOrEmpty(ZakatReturnDetail.d.EtimadI) ? "0.00" : ZakatReturnDetail.d.EtimadI; //ZakatReturnDetail.d.EtimadI;
                if (SubmitButtonVisibility && !IsThresholdGreaterLessVATAmount)
                {
                    salesDetails5.EstimateSales = "0.00";// !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.LabnoE) + " " + AppResources.ZSAR;// string.IsNullOrEmpty(zakatReturnDetails.d.LabnoE) ? "0.00" : zakatReturnDetails.d.LabnoE; //ZakatReturnDetail.d.LabnoE;
                }
                else
                {
                    salesDetails5.EstimateSales = !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.Sumcnt) + " " + AppResources.ZSAR;//string.IsNullOrEmpty(zakatReturnDetails.d.Sumcnt) ? "0.00" : zakatReturnDetails.d.Sumcnt; //ZakatReturnDetail.d.Estsl;
                }
                salesDetails5.SelectedEditFieldId = "5";
                salesDetails5.HelpIconVisibility = true;
                salesDetails5.SeparatorVisibility = false;
                salesDetails5.InformationIconVisibility = true;
                salesDetails5.DisableItemBackgroundColor = CapitalBackgroundColor;
                SalesDetailsDummyList.Add(salesDetails5);
                SalesDetails salesDetails6 = new SalesDetails();
                salesDetails6.SalesType = AppResources.ZZExportsvalue;
                salesDetails6.InformationFromPartieToCompare = !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : salesDetails6.InformationFromPartie = UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.ExamtI);//string.IsNullOrEmpty(ZakatReturnDetail.d.ExamtI) ? "0.00" : ZakatReturnDetail.d.ExamtI; //ZakatReturnDetail.d.ExamtResn;
                if (SubmitButtonVisibility && !IsThresholdGreaterLessVATAmount)
                {
                    salesDetails6.EstimateSales = "0.00";// !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.LabnoE) + " " + AppResources.ZSAR;// string.IsNullOrEmpty(zakatReturnDetails.d.LabnoE) ? "0.00" : zakatReturnDetails.d.LabnoE; //ZakatReturnDetail.d.LabnoE;
                }
                else
                {
                    salesDetails6.EstimateSales = !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.Sumcnt) + " " + AppResources.ZSAR;//string.IsNullOrEmpty(zakatReturnDetails.d.Sumcnt) ? "0.00" : zakatReturnDetails.d.Sumcnt; // ZakatReturnDetail.d.ExamtI;
                }
                salesDetails6.SelectedEditFieldId = "6";
                salesDetails6.SeparatorVisibility = true;
                salesDetails6.InformationIconVisibility = false;
                salesDetails6.HelpIconVisibility = false;
                salesDetails6.DisableItemBackgroundColor = CapitalBackgroundColor;
                SalesDetailsDummyList.Add(salesDetails6);
                SalesDetails salesDetails7 = new SalesDetails();
                salesDetails7.SalesType = AppResources.ZZPurchasevalue;
                salesDetails7.HelpIconVisibility = true;
                salesDetails7.InformationFromPartieToCompare = !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : salesDetails7.InformationFromPartie = UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.PramtI);//string.IsNullOrEmpty(ZakatReturnDetail.d.PramtI) ? "0.00" : ZakatReturnDetail.d.PramtI; // ZakatReturnDetail.d.PramtI;
                if (SubmitButtonVisibility && !IsThresholdGreaterLessVATAmount)
                {
                    salesDetails7.EstimateSales = "0.00";// !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.LabnoE) + " " + AppResources.ZSAR;// string.IsNullOrEmpty(zakatReturnDetails.d.LabnoE) ? "0.00" : zakatReturnDetails.d.LabnoE; //ZakatReturnDetail.d.LabnoE;
                }
                else
                {
                    salesDetails7.EstimateSales = !IsThresholdGreaterLessVATAmount ? AppResources.ZNA : UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.PramtE) + " " + AppResources.ZSAR;//string.IsNullOrEmpty(zakatReturnDetails.d.PramtE) ? "0.00" : zakatReturnDetails.d.PramtE; // ZakatReturnDetail.d.PramtE;
                }
                salesDetails7.SelectedEditFieldId = "7";
                salesDetails7.SeparatorVisibility = true;
                salesDetails7.InformationIconVisibility = true;
                salesDetails7.DisableItemBackgroundColor = CapitalBackgroundColor;
                SalesDetailsDummyList.Add(salesDetails7);
                SalesDetails salesDetails8 = new SalesDetails();
                salesDetails8.SalesType = AppResources.ZZCapitalamount;
                salesDetails8.InformationFromPartieToCompare =   salesDetails8.InformationFromPartie = UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.Cpamt);// string.IsNullOrEmpty(ZakatReturnDetail.d.Cpamt) ? "0.00" : ZakatReturnDetail.d.Cpamt;
                salesDetails8.EstimateSales = UtilityManager.GetCommaSeparatedAmount(zakatReturnResponse.d.Cpamt) + " " + AppResources.ZSAR;//string.IsNullOrEmpty(zakatReturnDetails.d.Cpamt) ? "0.00" : zakatReturnDetails.d.Cpamt;
                //SalesDetailsDummyList.Add(salesDetails8);
                salesDetails8.SelectedEditFieldId = "8";
                salesDetails8.SeparatorVisibility = true;
                salesDetails8.HelpIconVisibility = true;
                salesDetails8.InformationIconVisibility = true;
                salesDetails8.DisableItemBackgroundColor = Color.White;
                SalesDetailsDummyList.Add(salesDetails8);
                SalesDetailsList = SalesDetailsDummyList;
                SalesDetailsDataList = SalesDetailsDummyList;
            }
            catch(Exception ex)
            {
            }  
        }
        //public void SetChangedDataToTheList()
        //{
        //    ObservableCollection<SalesDetails> salesDetailsList = new ObservableCollection<SalesDetails>();
        //    for (int i = 0; i < SalesDetailsList.Count; i++)
        //    {
        //        salesDetailsList.Add(SalesDetailsList[i]);
        //    }
        //    SalesDetailsList = salesDetailsList;
        //}
        public void ClearData()
        {
        }
        private bool IsVATAmountGreaterThanThreshold()
        {
            bool IsThresholdGreaterLessVATAmount = false;
            try
            {
                double d = Convert.ToDouble(zakatReturnDetailsD.d.TvtslI);
                double d1 = Convert.ToDouble(zakatReturnDetailsD.d.ThresholdSet.results[0].Value);
                 IsThresholdGreaterLessVATAmount = d1 < d;
            }
            catch(Exception ex)
            {
                return false;
            }
            return IsThresholdGreaterLessVATAmount;
        }
        private ZakatReturnDetails GetPostDataAfterRemovingComma(ZakatReturnDetails zakatReturnDetails)
        {
            zakatReturnDetails.d.TvtslI = zakatReturnDetails.d.TvtslI.Replace(",", ""); 
                zakatReturnDetails.d.TvtslE = zakatReturnDetails.d.TvtslE.Replace(",", "");
                     zakatReturnDetails.d.LabnoI = zakatReturnDetails.d.LabnoI.Replace(",", "");
            zakatReturnDetails.d.LabnoE = zakatReturnDetails.d.LabnoE.Replace(",", "");
                     zakatReturnDetails.d.ImpvalI = zakatReturnDetails.d.ImpvalI.Replace(",", "");
            zakatReturnDetails.d.ImpvalE = zakatReturnDetails.d.ImpvalE.Replace(",", "");
                      zakatReturnDetails.d.PtoslI = zakatReturnDetails.d.PtoslI.Replace(",", "");
            zakatReturnDetails.d.Sumcnt = zakatReturnDetails.d.Sumcnt.Replace(",", "");
                       zakatReturnDetails.d.EtimadI = zakatReturnDetails.d.EtimadI.Replace(",", "");
            zakatReturnDetails.d.Sumcnt = zakatReturnDetails.d.Sumcnt.Replace(",", "");
                     zakatReturnDetails.d.ExamtI = zakatReturnDetails.d.ExamtI.Replace(",", "");
            zakatReturnDetails.d.Sumcnt = zakatReturnDetails.d.Sumcnt.Replace(",", "");
                      zakatReturnDetails.d.PramtI = zakatReturnDetails.d.PramtI.Replace(",", "");
            zakatReturnDetails.d.PramtE = zakatReturnDetails.d.PramtE.Replace(",", "");
                       zakatReturnDetails.d.Cpamt = zakatReturnDetails.d.Cpamt.Replace(",", "");
            zakatReturnDetails.d.Estsl = zakatReturnDetails.d.Estsl.Replace(",", "");
            zakatReturnDetails.d.Zbamt = zakatReturnDetails.d.Zbamt.Replace(",", "");
            zakatReturnDetails.d.Zkamt = zakatReturnDetails.d.Zkamt.Replace(",", "");
            return zakatReturnDetails;
        }
        public void OnAmendReturnClicked()
        {
            try
            {
                ShowSubmitButton();
                SetSalesDetailsData(zakatReturnDetailsD);
                ShowDisclaimer();
                ShowEditIcon();
                CheckBoxStatus = false;
                //  _navigationService.NavigateTo(App.AmendSalesDetailsPageView, SelectedSalesDetails);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}
