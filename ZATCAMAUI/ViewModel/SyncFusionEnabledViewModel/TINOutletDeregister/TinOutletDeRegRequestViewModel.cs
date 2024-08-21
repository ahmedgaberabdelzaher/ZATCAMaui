using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Mopups.Services;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.Models.TINOutletDeregister;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.ContractReleasePages;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.SyncFusionEnabledViews.TINOutletDeregister;
using static ZATCAMAUI.Models.ErrorMessage;
using static ZATCAMAUI.Models.TINOutletDeregister.TinOutletDeregisterListModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TINOutletDeregister;
public class TinOutletDeRegRequestViewModel : BaseViewModel
{

    public TinOutletDeregisterListModel tinOutletPrevousRequestsModel;

    public ICommand GoBackClick { get; set; }
    public ICommand ShowDeregTypePicker { get; set; }
    public ICommand ShowDeregReasonPicker { get; set; }
    public ICommand DeregisterAttachmentTapped { get; set; }
    public ICommand ContinueBtnTapped { get; set; }
    public ICommand SubmitBtnTapped { get; set; }
    public ICommand AttachmentInfoCommand { get; set; }

    public ICommand ShowOutletReasonPicker { get; set; }
    public ICommand ShowPermitReasonPicker { get; set; }
    public ICommand MainOutletPicker { get; set; }
    public ICommand LicenceCloseTransferDatePickerClicked { get; set; }
    public ICommand OutletClosetransferDatePickerClicker { get; set; }
    public Command<object> OuterListTapCommand { get; set; }
    public ICommand OutletCheckBoxClicked { get; set; }
    public ICommand OnVoidOrSaveDraftClick { get; set; }

    enum PagesEnum
    {
        TinOutLetDeregReason,
        Attachments
    }
    public int DefaultMonth;
    List<string> list = new List<string>();

    List<string> list1 = new List<string>();

    List<TinDeregReasonSetResult> ReasonsList = new List<TinDeregReasonSetResult>();
    List<string> ReasonsListUI = new List<string>();


    public bool MarkComplete { get; private set; } = false;
    int selectedPage = (int)PagesEnum.TinOutLetDeregReason;

    private int _MaxIndex = 2;

    public int MaxIndex
    {
        get { return _MaxIndex; }
        set
        {
            if (_MaxIndex == value) return;

            _MaxIndex = value;
            OnPropertyChanged("MaxIndex");
        }
    }


    public int DeregistrationType = 0;


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

    private bool _isLoading = false;
    public bool IsLoading
    {
        get { return _isLoading; }
        set
        {
            if (_isLoading == value) return;

            _isLoading = value;
            OnPropertyChanged("IsLoading");
        }
    }

    private bool _shouldShowList = false;
    public bool ShouldShowList
    {
        get { return _shouldShowList; }
        set
        {
            if (_shouldShowList == value) return;

            _shouldShowList = value;
            OnPropertyChanged("ShouldShowList");
        }
    }

    private bool _shouldShowAttachments = false;
    public bool ShouldShowAttachments
    {
        get { return _shouldShowAttachments; }
        set
        {
            if (_shouldShowAttachments == value) return;

            _shouldShowAttachments = value;
            OnPropertyChanged("ShouldShowAttachments");
        }
    }



    private string _selectedDeRegType = "";

    public string SelectedDeRegType
    {
        get { return _selectedDeRegType; }
        set
        {
            if (_selectedDeRegType == value) return;

            _selectedDeRegType = value;
            OnPropertyChanged("SelectedDeRegType");
        }
    }
    private string _newMainOutlet = "";

    public string NewMainOutlet
    {
        get { return _newMainOutlet; }
        set
        {
            if (_newMainOutlet == value) return;

            _newMainOutlet = value;
            OnPropertyChanged("NewMainOutlet");
        }
    }


    private string _selectedDeRegReason = "";

    public string SelectedDeRegReason
    {
        get { return _selectedDeRegReason; }
        set
        {
            if (_selectedDeRegReason == value) return;

            _selectedDeRegReason = value;
            OnPropertyChanged("SelectedDeRegReason");
        }
    }

    private bool _shouldShowDeregReason = false;

    public bool ShouldShowDeregReason
    {
        get { return _shouldShowDeregReason; }
        set
        {
            if (_shouldShowDeregReason == value) return;

            _shouldShowDeregReason = value;
            OnPropertyChanged("ShouldShowDeregReason");
        }
    }

    private bool _enableDeregReason = true;
    public bool EnableDeregReason
    {
        get { return _enableDeregReason; }
        set
        {
            if (_enableDeregReason == value) return;

            _enableDeregReason = value;
            OnPropertyChanged("EnableDeregReason");
        }
    }

    private bool _enableDeregType = true;
    public bool EnableDeregType
    {
        get { return _enableDeregType; }
        set
        {
            if (_enableDeregType == value) return;

            _enableDeregType = value;
            OnPropertyChanged("EnableDeregType");
        }
    }

    private bool _showMainOutletDropDown = false;

    public bool ShowMainOutletDropDown
    {
        get { return _showMainOutletDropDown; }
        set
        {
            if (_showMainOutletDropDown == value) return;

            _showMainOutletDropDown = value;
            OnPropertyChanged("ShowMainOutletDropDown");
        }
    }

    private bool _shouldShowDatePickerOutlet = false;
    public bool ShouldShowDatePickerOutlet
    {
        get { return _shouldShowDatePickerOutlet; }
        set
        {
            if (_shouldShowDatePickerOutlet == value) return;

            _shouldShowDatePickerOutlet = value;
            OnPropertyChanged("ShouldShowDatePickerOutlet");
        }
    }

    private bool _shouldShowDatePickerHijiriOutlet = false;
    public bool ShouldShowDatePickerHijiriOutlet
    {
        get { return _shouldShowDatePickerHijiriOutlet; }
        set
        {
            if (_shouldShowDatePickerHijiriOutlet == value) return;

            _shouldShowDatePickerHijiriOutlet = value;
            OnPropertyChanged("ShouldShowDatePickerHijiriOutlet");
        }
    }

    //

    private bool _shouldShowDatePicker = false;
    public bool ShouldShowDatePicker
    {
        get { return _shouldShowDatePicker; }
        set
        {
            if (_shouldShowDatePicker == value) return;

            _shouldShowDatePicker = value;
            OnPropertyChanged("ShouldShowDatePicker");
        }
    }

    private bool _shouldShowDatePickerHijiri = false;
    public bool ShouldShowDatePickerHijiri
    {
        get { return _shouldShowDatePickerHijiri; }
        set
        {
            if (_shouldShowDatePickerHijiri == value) return;

            _shouldShowDatePickerHijiri = value;
            OnPropertyChanged("ShouldShowDatePickerHijiri");
        }
    }


    private bool _showSaveSubmit = false;
    public bool ShowSaveSubmit
    {
        get { return _showSaveSubmit; }
        set
        {
            if (_showSaveSubmit == value) return;

            _showSaveSubmit = value;
            OnPropertyChanged("ShowSaveSubmit");
        }
    }
    private object _transferCloseDateOutlet = "";
    public object TransferCloseDateOutlet
    {
        get { return _transferCloseDateOutlet; }
        set
        {
            if (_transferCloseDateOutlet == value) return;

            _transferCloseDateOutlet = value;
            OnPropertyChanged("TransferCloseDateOutlet");
        }
    }


    private object _transferCloseDate = "";
    public object TransferCloseDate
    {
        get { return _transferCloseDate; }
        set
        {
            if (_transferCloseDate == value) return;

            _transferCloseDate = value;
            OnPropertyChanged("TransferCloseDate");
        }
    }

    private ContactInfo_NestedListView _selectedOutletItem;
    public ContactInfo_NestedListView SelectedOutletItem
    {
        get { return _selectedOutletItem; }
        set
        {
            if (_selectedOutletItem == value) return;
            _selectedOutletItem = value;
            OnPropertyChanged("SelectedOutletItem");
        }
    }


    private DetailsContactInfo _selectedPermitItem;
    public DetailsContactInfo SelectedPermitItem
    {
        get { return _selectedPermitItem; }
        set
        {
            if (_selectedPermitItem == value) return;

            _selectedPermitItem = value;
            OnPropertyChanged("SelectedPermitItem");
        }
    }

    private GenericPickerModel _pickerModelDeregType { get; set; }
    public GenericPickerModel PickerModelDeregType
    {
        get { return _pickerModelDeregType; }
        set
        {
            if (_pickerModelDeregType == value) return;

            _pickerModelDeregType = value;
            OnPropertyChanged("PickerModelDeregType");
        }
    }

    private GenericPickerModel _pickerModelOutlerReason { get; set; }
    public GenericPickerModel PickerModelOutletReason
    {
        get { return _pickerModelOutlerReason; }
        set
        {
            if (_pickerModelOutlerReason == value) return;

            _pickerModelOutlerReason = value;
            OnPropertyChanged("PickerModelOutletReason");
        }
    }

    private GenericPickerModel _pickerModelPermitReason { get; set; }
    public GenericPickerModel PickerModelPermitReason
    {
        get { return _pickerModelPermitReason; }
        set
        {
            if (_pickerModelPermitReason == value) return;

            _pickerModelPermitReason = value;
            OnPropertyChanged("PickerModelPermitReason");
        }
    }

    private GenericPickerModel _pickerModelDeregReason { get; set; }
    public GenericPickerModel PickerModelDeregReason
    {
        get { return _pickerModelDeregReason; }
        set
        {
            if (_pickerModelDeregReason == value) return;

            _pickerModelDeregReason = value;
            OnPropertyChanged("PickerModelDeregReason");
        }
    }

    private GenericPickerModel _pickerModelMainOutlet { get; set; }
    public GenericPickerModel PickerModelMainOutlet
    {
        get { return _pickerModelMainOutlet; }
        set
        {
            if (_pickerModelMainOutlet == value) return;

            _pickerModelMainOutlet = value;
            OnPropertyChanged("PickerModelMainOutlet");
        }
    }

    public List<TinDeregReasonSetResult> _tinDeregReasons { get; set; }
    public List<TinDeregReasonSetResult> TinDeregReasons
    {
        get
        {
            return _tinDeregReasons;
        }

        set
        {
            if (_tinDeregReasons == value) return;

            _tinDeregReasons = value;
        }
    }

    public ObservableCollection<ContactInfo_NestedListView> ContactInfo { get; set; }

    private TinOutletDeregisterListModel.OutletDeregisterListResponse _outlettListResponse;
    public TinOutletDeregisterListModel.OutletDeregisterListResponse OutlettListResponse
    {
        get
        {
            return _outlettListResponse;
        }
        set
        {
            if (_outlettListResponse == value) return;

            _outlettListResponse = value;

            OnPropertyChanged("OutlettListResponse");
        }
    }

    public ObservableCollection<Attachment> _deregisterAttachmentsListViewData = new ObservableCollection<Attachment>();
    public ObservableCollection<Attachment> DeregisterAttachmentsListViewData
    {
        get { return _deregisterAttachmentsListViewData; }

        set
        {
            if (_deregisterAttachmentsListViewData == value)
            {
                return;
            }

            _deregisterAttachmentsListViewData = value;
            OnPropertyChanged("DeregisterAttachmentsListViewData");
        }
    }

    private ObservableCollection<ContactInfo_NestedListView> _outlettUiList = new ObservableCollection<ContactInfo_NestedListView>();
    public ObservableCollection<ContactInfo_NestedListView> OutlettUiList
    {
        get
        {
            return _outlettUiList;
        }
        set
        {
            if (_outlettUiList == value) return;

            _outlettUiList = value;

            OnPropertyChanged("OutlettUiList");
        }
    }

    private TinDeregistrationReasonSetDataModel _tinDeregistrationReasonSetData { get; set; }
    public TinDeregistrationReasonSetDataModel TinDeregistrationReasonSetData
    {
        get
        {
            return _tinDeregistrationReasonSetData;
        }
        set
        {
            if (_tinDeregistrationReasonSetData == value) return;

            _tinDeregistrationReasonSetData = value;
            //SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(_selectedOutletOption as TINDeregistrationModel);
            OnPropertyChanged("TinDeregistrationReasonSetData");
        }
    }

    private Color _continueBtnBackgroundColor = (Color)Application.Current.Resources["ButtonGray"];

    public Color ContinueBtnBackgroundColor
    {
        get { return _continueBtnBackgroundColor; }
        set
        {
            if (_continueBtnBackgroundColor == value)
            {
                return;
            }

            _continueBtnBackgroundColor = value;
            OnPropertyChanged("ContinueBtnBackgroundColor");
        }
    }

    private bool _continueButtonEnabled = false;

    public bool ContinueButtonEnabled
    {
        get { return _continueButtonEnabled; }
        set
        {
            if (_continueButtonEnabled == value) return;

            _continueButtonEnabled = value;
            ContinueBtnBackgroundColor = (_continueButtonEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
            OnPropertyChanged("ContinueButtonEnabled");
        }
    }
    private ObservableCollection<object> _todayDateNormal;
    public ObservableCollection<object> TodayDateNormal
    {
        get
        {
            return _todayDateNormal;
        }
        set
        {
            if (_todayDateNormal == value) return;

            _todayDateNormal = value;
            OnPropertyChanged("TodayDateNormal");
        }
    }
    private ObservableCollection<object> _todayDateinHijri;
    public ObservableCollection<object> TodayDateinHijri
    {
        get
        {
            return _todayDateinHijri;
        }
        set
        {
            if (_todayDateinHijri == value) return;

            _todayDateinHijri = value;
            OnPropertyChanged("TodayDateinHijri");
        }
    }
    public TinOutletDeRegRequestViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService, dialogService)
    {
        GoBackClick = new Command(() =>
        {
            BackNavigations();

        });
        ShowDeregTypePicker = new Command(() =>
        {
            showDeregTypeDialog();
        });

        ShowDeregReasonPicker = new Command(() =>
        {
            showDeregReasonDialog();
        });
        DeregisterAttachmentTapped = new Command(() =>
        {
            DeregAttachmentsPopup();
        });

        AttachmentInfoCommand = new Command(async () =>
        {
            await MopupService.Instance.PushAsync(new ContractReleaseInfoPopup(Desc: AppResources.SupportingDocumentsMsg, Title: AppResources.Information));
        });

        SubmitBtnTapped = new Command(async () =>
        {
            var isvalid = await ValidateOutletsAndPermits();

            if (isvalid)
            {
                if (OutlettListResponse.Status == "IP011")
                {
                    var newAttachments = DeregisterAttachmentsListViewData.Where(i => i.OutletRef == "X").Count();
                    if (newAttachments > 10)
                    {
                        await _dialogService.ShowMessage(AppResources.ZVatAttachmentMaxSizeNotfication, AppResources.Information);
                    }
                    else if (newAttachments < 1)
                    {
                        await _dialogService.ShowMessage(AppResources.OneAttMandatory, AppResources.Information);
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(AppResources.AreYouSureToSubmit, AppResources.Information, AppResources.Submit, AppResources.CancelText, (async (bool isConfirmed) =>
                            {
                                if (isConfirmed == true)
                                {
                                    await Task.Delay(500);
                                    OnSubmitRequest(2);
                                }
                            }));
                        });
                    }
                }
                else
                {
                    if (DeregisterAttachmentsListViewData != null && DeregisterAttachmentsListViewData.Count() > 0 && DeregisterAttachmentsListViewData.Count() <= 10)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(AppResources.AreYouSureToSubmit, AppResources.Information, AppResources.Submit, AppResources.CancelText, (async (bool isConfirmed) =>
                            {
                                if (isConfirmed == true)
                                {
                                    await Task.Delay(500);
                                    OnSubmitRequest(2);
                                }
                            }));
                        });

                    }
                    else
                    {
                        if (DeregisterAttachmentsListViewData == null || DeregisterAttachmentsListViewData.Count() <= 0)
                        {
                            await _dialogService.ShowMessage(AppResources.OneAttMandatory, AppResources.Information);
                        }
                        else
                        {
                            await _dialogService.ShowMessage(AppResources.ZVatAttachmentMaxSizeNotfication, AppResources.Information);
                        }

                    }
                }

            }
        });

        OnVoidOrSaveDraftClick = new Command(async () =>
        {
            if (OutlettListResponse.Status == "IP011")
            {
                var newAttachments = DeregisterAttachmentsListViewData.Where(i => i.OutletRef == "X").Count();
                if (newAttachments > 10)
                {
                    await _dialogService.ShowMessage(AppResources.ZVatAttachmentMaxSizeNotfication, AppResources.Information);
                }
                else
                {
                    OnSubmitRequest(1);
                }

            }
            else
            {
                if (DeregisterAttachmentsListViewData.Count() <= 10)
                {
                    OnSubmitRequest(1);
                }
                else
                {
                    await _dialogService.ShowMessage(AppResources.ZVatAttachmentMaxSizeNotfication, AppResources.Information);

                }
            }

        });

        ContinueBtnTapped = new Command(async () =>
        {
          await  CheckvalidationsOnSubmitAsync();
        });

        ShouldShowList = true;
        ShouldShowAttachments = false;

        EnableDeregReason = true;
        EnableDeregType = true;
        NewMainOutlet = string.Empty;
        ShowMainOutletDropDown = false;

        ShowOutletReasonPicker = new Command(OnOutletReason);
        ShowPermitReasonPicker = new Command(OnPermitReason);
        MainOutletPicker = new Command(OnOutletChange);
        OutletCheckBoxClicked = new Command(OnOutlectCheckChanged);
        LicenceCloseTransferDatePickerClicked = new Command(OnPermitDateClicked);
        OutletClosetransferDatePickerClicker = new Command(OnOutletDateClicked);
        OuterListTapCommand = new Command<object>(OnOuterListTapped);
        TinDeregistrationReasonSetData = new TinDeregistrationReasonSetDataModel();

        LoadReasonSet();
    }

    private void OnOutletChange(object obj)
    {
        ShowMainOutletPopUpAsync();
    }

    private void BackNavigations()
    {
        switch (selectedPage)
        {
            case (int)PagesEnum.TinOutLetDeregReason:
                _navigationService.GoBack();
                break;
            case (int)PagesEnum.Attachments:
                GotoDeregReasonsPage();
                break;

        }
    }

    private void GotoDeregReasonsPage()
    {
        CurrentIndex = 1;
        ShouldShowList = true;
        ShouldShowAttachments = false;
        selectedPage = (int)PagesEnum.TinOutLetDeregReason;
    }

    private async Task CheckvalidationsOnSubmitAsync()
    {
        if (DeregistrationType == 1)
        {
            //Tin Dereg
            if (SelectedDeRegType.Length <= 0)
            {
                await _dialogService.ShowMessage(AppResources.DeregTypeMsg, AppResources.ZError);
                return;
            }
            if (SelectedDeRegReason.Length <= 0)
            {
                await _dialogService.ShowMessage(AppResources.DeregReasonMsg, AppResources.ZError);
                return;
            }

            bool isValid = await ValidateOutletsAndPermits();
            if (!isValid)
            {
                return;
            }
        }
        else
        {

            var totalOutletCount = OutlettUiList.Count();
            var totalOutletSelectedCount = OutlettUiList.Where(i => i.IsOutletChecked == true).Count();

            var totalPermitsSelectedCount = 0;
            var totalPermits = OutlettUiList.SelectMany(x => x.ContactDetails);
            var totalPertmitsCount = totalPermits.Count();

            if (totalPertmitsCount > 0)
            {
                totalPermitsSelectedCount = totalPermits.Where(y => y.IsPermitChecked == true).Count();
            }

            if (totalOutletCount == totalOutletSelectedCount && totalPertmitsCount == totalPermitsSelectedCount)
            {
                await _dialogService.ShowMessage("All outlets are selected, Please go to TIN Deregistration.", AppResources.ZError);
                return;
            }

            bool isValid = await ValidateOutletsAndPermits();
            if (!isValid)
            {
                return;
            }
        }

        if (ContinueButtonEnabled)
        {
            CurrentIndex = 2;
            ShouldShowAttachments = true;
            ShouldShowList = !ShouldShowAttachments;
            selectedPage = (int)PagesEnum.Attachments;
        }
    }

    private async Task<bool> ValidateOutletsAndPermits()
    {
        //checking dates and status selected or not for all selected permits and outlets

        var SelectedOutletsList = OutlettUiList.Where(x => x.IsOutletChecked == true);
        var SelectedPermitsList = OutlettUiList.SelectMany(x => x.ContactDetails.Where(x => x.IsPermitChecked == true));

        bool isvalid = true;
        foreach (var outlet in SelectedOutletsList)
        {
            if (string.IsNullOrEmpty(outlet.AoutletReason))
            {
                isvalid = false;
                await PleaseFillAllMandatory();
                break;
            }
            //outlet.AOutletCloseTransferDtTb
            DateTime outletDate = Convert.ToDateTime(outlet.AOutletCloseTransferDtTb);
            if (outletDate > DateTime.Now.Date)
            {
                isvalid = false;
                await OutletTransferDateError();
                break;
            }
        }

        if (isvalid)
        {
            foreach (var permit in SelectedPermitsList)
            {
                if (string.IsNullOrEmpty(permit.APermitReason) || string.IsNullOrEmpty(permit.ALicenceCloseTransfer.ToString()))
                {
                    isvalid = false;
                    await PleaseFillAllMandatory();
                    break;
                }
                var outletDate = OutlettUiList.Where(x => x.AOutletNoTb == permit.APermitOutletNoTb).Select(x => x.AOutletCloseTransferDtTb).FirstOrDefault();

                DateTime permitDate = Convert.ToDateTime(permit.ALicenceCloseTransfer);
                DateTime permitvalidFromDate = Convert.ToDateTime(permit.APermitValfrDtTb);

                DateTime outletDate2 = Convert.ToDateTime(outletDate);

                if (permitDate > outletDate2)
                {
                    isvalid = false;
                    await PermitTransferDateError();
                    break;
                }

                if (permitvalidFromDate > permitDate)
                {
                    isvalid = false;
                    await PermitTransferDateError();
                    break;
                }
            }
        }
        if (isvalid)
        {
            if (ShowMainOutletDropDown && string.IsNullOrEmpty(NewMainOutlet))
            {
                isvalid = false;
                await PleaseFillAllMandatory();
                return isvalid;
            }
        }


        return isvalid;
    }



    private async Task PermitTransferDateError()
    {
        await _dialogService.ShowMessage(AppResources.PermitTransferDateError, AppResources.ZError);
        return;
    }
    private async Task PermitEffectiveDateError()
    {
        await _dialogService.ShowMessage(AppResources.PermitEffectiveDateError, AppResources.ZError);
        return;
    }
    private async Task PleaseFillAllMandatory()
    {
        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.ZError);
        return;
    }

    private async Task OutletTransferDateError()
    {
        await _dialogService.ShowMessage(AppResources.FuterDateError, AppResources.ZError);
        return;
    }
    private void OnOutletReason(object obj)
    {
        SelectedOutletItem = obj as ContactInfo_NestedListView;
        ShowOutletItemReasonsDialog();
    }

    private void OnPermitReason(object obj)
    {
        SelectedPermitItem = obj as DetailsContactInfo;
        ShowPermitItemReasonsDialog();
    }

    public void OnPermitChecked(DetailsContactInfo Item)
    {
        SelectedPermitItem = Item;
        SelectedOutletItem = OutlettUiList.Where(i => i.AOutletNoTb == SelectedPermitItem.APermitOutletNoTb).FirstOrDefault();

        var permitItemCount = SelectedOutletItem.ContactDetails.Count();
        var selectedPermitsCount = SelectedOutletItem.ContactDetails.Where(i => i.IsPermitChecked == true).Count();
        if (permitItemCount == selectedPermitsCount && SelectedOutletItem.AOutletIdentificationNoTb.Length == 0)
        {
            SelectedOutletItem.IsOutletChecked = true;
        }
    }

    private void OnOutletDateClicked(object obj)
    {
        SelectedOutletItem = obj as ContactInfo_NestedListView;

        if (App.IsArabic)
        {
            ShouldShowDatePickerOutlet = true;
        }
        else
        {
            ShouldShowDatePickerOutlet = true;
        }
    }
    private void OnPermitDateClicked(object obj)
    {
        SelectedPermitItem = obj as DetailsContactInfo;

        if (App.IsArabic)
        {
            ShouldShowDatePicker = true;
        }
        else
        {
            ShouldShowDatePicker = true;
        }
    }

    private void OnOutlectCheckChanged(object obj)
    {
        SelectedOutletItem = obj as ContactInfo_NestedListView;
        var x = SelectedOutletItem.IsOutletChecked;

    }

    public async void SubmitButtonClicked() { }

    public async void DeregAttachmentsPopup()
    {
        if (MopupService.Instance.PopupStack.Count > 0) return;
        if (DeregisterAttachmentsListViewData == null)
        {
            DeregisterAttachmentsListViewData = new ObservableCollection<Attachment>();
        }

        try
        {
            // SelectedAttachmentNumber = (int)WhichAttachment.ZakatExemtionAttachmentOne;
            await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                     DeregisterAttachmentsListViewData.ToList(),
                     WhichAttachment.TINOutletDeregisterAttachment, tinOutletPrevousRequestsModel.D.CaseGuid, "", tinOutletPrevousRequestsModel.D.Status.ToUpper() == "IP011" ? "X" : string.Empty));

        }
        catch (InternetException ex)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            });
        }
    }

    public void PopulateAttachments(List<Attachment> attachments)
    {
        if (attachments.Count > 0)
        {

            DeregisterAttachmentsListViewData.Clear();


            foreach (Attachment item in attachments)
            {
                DeregisterAttachmentsListViewData.Add(item);
            }
        }
        else
        {
            DeregisterAttachmentsListViewData.Clear();
        }
    }

    private async void showDeregTypeDialog()
    {
        try
        {
            setDeregTypePickerModel();
            await MopupService.Instance.PushAsync(new PickerPageView(PickerModelDeregType));

        }
        catch (InternetException ex)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            });
        }
    }

    private async void showDeregReasonDialog()
    {
        try
        {
            setDeregReasonPickerModel();
            await MopupService.Instance.PushAsync(new PickerPageView(PickerModelDeregReason));

        }
       
        catch (InternetException ex)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            });
        }
    }

    private void setDeregTypePickerModel()
    {

        if (PickerModelDeregType != null)
        {

            PickerModelDeregType = null;
        }

        addDeRegTypes();
        GenericPickerModel genericPickerModel = new GenericPickerModel();
        genericPickerModel.PickerData = list;
        genericPickerModel.PickerTitle = "";
        genericPickerModel.PickerId = "DeregTypePicker";
        genericPickerModel.PageCode = 1;
        PickerModelDeregType = genericPickerModel;
        SelectedDeRegType = "";

    }
    private void addDeRegTypes()
    {
        list.Clear();
        list.Add(AppResources.TinDeregistration);


        list1.Clear();
        list1.Add(AppResources.TinDeregistration);
        list1.Add(AppResources.OutletDeReg);


    }

    private void setDeregReasonPickerModel()
    {

        if (PickerModelDeregReason != null)
        {

            PickerModelDeregReason = null;
        }


        ReasonsList.Clear();
        ReasonsListUI.Clear();
        foreach (TinDeregReasonSetResult item in TinDeregReasons)
        {
            try
            {
                ReasonsList.Add(item);
                ReasonsListUI.Add(item.ReasonDesc);
            }
            catch (Exception)
            {

            }

        }


        GenericPickerModel genericPickerModel = new GenericPickerModel();
        genericPickerModel.PickerData = ReasonsListUI;
        genericPickerModel.PickerTitle = "";
        genericPickerModel.PickerId = "DeregReasonPicker";
        genericPickerModel.PageCode = 1;
        PickerModelDeregReason = genericPickerModel;
        SelectedDeRegReason = "";

    }

    private void setOutletReasonPickerModel()
    {

        if (PickerModelOutletReason != null)
        {
            PickerModelOutletReason = null;
        }
        var list = new List<string>();

        if (!App.IsArabic)
        {
            list.Add("Close CR");
            list.Add("Transfer CR");
        }
        else
        {
            list.Add("ايقاف السجل");
            list.Add("نقل ملكية السجل");
        }

        GenericPickerModel genericPickerModel = new GenericPickerModel();
        genericPickerModel.PickerData = list;
        genericPickerModel.PickerTitle = "";
        genericPickerModel.PickerId = "OutletReasonPicker";
        genericPickerModel.PageCode = 1;
        PickerModelOutletReason = genericPickerModel;

    }

    private async void ShowOutletItemReasonsDialog()
    {
        try
        {
            setOutletReasonPickerModel();
            await MopupService.Instance.PushAsync(new PickerPageView(PickerModelOutletReason));

        }
        
        catch (InternetException ex)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            });
        }
    }

    private void setPermitReasonPickerModel()
    {

        if (PickerModelPermitReason != null)
        {
            PickerModelPermitReason = null;
        }
        var list = new List<string>();

        if (!App.IsArabic)
        {
            list.Add("Close");
            list.Add("Transfer");
        }
        else
        {
            list.Add("إيقاف");
            list.Add("نقل ملكية");
        }

        GenericPickerModel genericPickerModel = new GenericPickerModel();
        genericPickerModel.PickerData = list;
        genericPickerModel.PickerTitle = "";
        genericPickerModel.PickerId = "PermitReasonPicker";
        genericPickerModel.PageCode = 1;
        PickerModelPermitReason = genericPickerModel;

    }

    private async void ShowPermitItemReasonsDialog()
    {
        try
        {
            setPermitReasonPickerModel();
            await MopupService.Instance.PushAsync(new PickerPageView(PickerModelPermitReason));

        }
        
        catch (InternetException ex)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            });
        }
    }

    public void UpdateOutletReason(string selectedValue)
    {

        SelectedOutletItem.AoutletReason = selectedValue;
        SelectedOutletItem.AOutletActionTypeTb = GetOutletReasonCode(SelectedOutletItem.AoutletReason);

        var msg = onOutletStatusSelection(SelectedOutletItem);
        if (msg.Length > 0)
        {
            SelectedOutletItem.AoutletReason = "";
            SelectedOutletItem.AOutletActionTypeTb = GetOutletReasonCode(SelectedOutletItem.AoutletReason);
        }

        OnPropertyChanged("OutlettUiList");
    }

    public void UpdatePermitReason(string selectedValue)
    {
        SelectedPermitItem.APermitReason = selectedValue;

    }

    public void updateOutletTransferCloseDate()
    {
        SelectedOutletItem.AOutletCloseTransferDtTb = Convert.ToDateTime(TransferCloseDateOutlet.ToString()).ToShortDateString();
    }

    public void updatePermitTransferCloseDate()
    {
        SelectedPermitItem.ALicenceCloseTransfer = Convert.ToDateTime(TransferCloseDate.ToString()).ToShortDateString();
    }

    private void OnOuterListTapped(object obj)
    {
        var item = obj as ContactInfo_NestedListView;
        item.IsInnerListVisible = !item.IsInnerListVisible;
    }

    public async void GetTinOutletDeregistrationDataBeforeNewRequest(int DeregTypeCode, string fbGuid = "")
    {
        IsLoading = true;
        await Task.Delay(500);
        try
        { //

            if (DeregisterAttachmentsListViewData != null)
            {
                DeregisterAttachmentsListViewData.Clear();
            }
            string _responseData = await TINDeregistrationWebServiceManager.GetOutletDeRegisterNewRequest(DeregTypeCode, fbGuid);

            tinOutletPrevousRequestsModel = JsonConvert.DeserializeObject<TinOutletDeregisterListModel>(_responseData);
            if (tinOutletPrevousRequestsModel == null || tinOutletPrevousRequestsModel.D == null)
            {
                ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                {
                    string errorCode = errorMesg.error.innererror.errordetails[0].code;

                    WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;

                    if (errorCode.Contains("206"))
                    {
                        WebServiceManager.ErrorMessageForUnlockAccount = "206";
                    }
                    else if (errorCode.Contains("112"))
                    {
                        WebServiceManager.ErrorMessageForUnlockAccount = "112";
                    }
                    string line1 = "";

                    for (int i = 0; i < errorMesg.error.innererror.errordetails.Count; i++)
                    {
                        line1 = line1 + "\n" + "\n" + errorMesg.error.innererror.errordetails[i].message;
                    }
                    WebServiceManager.ErrorMessageForUnlockAccount = line1;

                    String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(WithReplacedString, AppResources.ZError);
                        GoBackAftersubmission();
                    });

                    throw new GAZTErrorException(WithReplacedString);
                }

            }

            if (tinOutletPrevousRequestsModel != null && tinOutletPrevousRequestsModel.D != null && tinOutletPrevousRequestsModel.D.OutletSet.Count > 0)
            {

                if (!string.IsNullOrEmpty(tinOutletPrevousRequestsModel.D.ADegister))
                {
                    int index = int.Parse(tinOutletPrevousRequestsModel.D.ADegister);
                    if (index == 1)
                    {
                        DeregistrationType = 1;
                    }
                    else
                    {
                        DeregistrationType = 2;
                        ContinueButtonEnabled = true;

                    }
                    if (index > 0)
                    {
                        if (list.Count == 0)
                        {
                            addDeRegTypes();
                        }
                        SelectedDeRegType = list1[index - 1].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(tinOutletPrevousRequestsModel.D.ADregReason))
                {
                    int index = int.Parse(tinOutletPrevousRequestsModel.D.ADregReason);
                    if (index > 0)
                    {
                        SelectedDeRegReason = ReasonsList.Where(i => i.ReasonCd == index.ToString()).Select(x => x.ReasonDesc).FirstOrDefault();
                        ShouldShowDeregReason = true;
                        ContinueButtonEnabled = true;
                    }
                }

                if (tinOutletPrevousRequestsModel.D.AttDetSet.Count() > 0)
                {
                    PrepareAttachmentsUi(tinOutletPrevousRequestsModel.D.AttDetSet);
                }

                OutlettListResponse = tinOutletPrevousRequestsModel.D;
                App.DeRegRequestStatus = tinOutletPrevousRequestsModel.D.Status;

                CopyResponseObjectToUiObject(OutlettListResponse);
                if (SelectedDeRegType.Equals(AppResources.OutletDeReg))
                {
                    ShowSaveSubmit = false;
                }
            }
            else
            {
                // IsListVisible = false;
            }
            IsLoading = false;
        }
        catch (Exception)
        {
            IsLoading = false;
        }
    }

    private void PrepareAttachmentsUi(List<AttachmentSetResult> attDetSet)
    {
        DeregisterAttachmentsListViewData = new ObservableCollection<Attachment>();
        App.DeregisterAttachments.Clear();
        foreach (var item in attDetSet)
        {
            Attachment attachment = new Attachment
            {
                RetGuid = item.RetGuid,
                Seqno = item.Seqno,
                SchGuid = item.SchGuid,
                Dotyp = item.Dotyp,
                Srno = item.Srno,
                Doguid = item.Doguid,
                AttBy = item.AttBy,
                Filename = item.Filename,
                FileExtn = item.FileExtn,
                Mimetype = item.Mimetype,
                Erfdt = item.Erfdt,
                Erftm = item.Erftm,
                DataVersion = item.DataVersion,
                DocUrl = item.DocUrl,
                Enbedit = item.Enbedit,
                Enbdele = item.Enbdele,
                Visedit = item.Visedit,
                Visdel = item.Visdel,
                OutletRef = item.OutletRef,
            };
            if (item.Dotyp.Equals("DR01"))
            {
                App.DeregisterAttachments.Add(attachment);
                DeregisterAttachmentsListViewData.Add(attachment);
            }
        }
    }

    private async void OnSubmitRequest(int SubmitType)
    {
        //SubmitType = 1;

        IsLoading = true;
        await Task.Delay(500);
        try
        {
            if (SubmitType == 1) // Draft
            {
                tinOutletPrevousRequestsModel.D.Savez = "X";
                tinOutletPrevousRequestsModel.D.Submitz = "";
            }
            else //Submit
            {
                tinOutletPrevousRequestsModel.D.Submitz = "X";
                tinOutletPrevousRequestsModel.D.Savez = "";

            }
            var currentDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

            //tinOutletPrevousRequestsModel.D.ASubmissionDate =UtilityManager.ConvertDateFormat(currentDate);
            tinOutletPrevousRequestsModel.D.ASubmissionDate = currentDate;
            //tinOutletPrevousRequestsModel.D.AEffectiveDt = UtilityManager.ConvertDateFormat(tinOutletPrevousRequestsModel?.D?.AEffectiveDt);
            //tinOutletPrevousRequestsModel.D.ADecDate = UtilityManager.ConvertDateFormat(tinOutletPrevousRequestsModel?.D?.ADecDate);

            tinOutletPrevousRequestsModel.D.AEffectiveDt = tinOutletPrevousRequestsModel?.D?.AEffectiveDt;
            tinOutletPrevousRequestsModel.D.ADecDate = tinOutletPrevousRequestsModel?.D?.ADecDate;

            string LangZ = WebServiceManager.GetLangZParameterAREN();
            tinOutletPrevousRequestsModel.D.Langz = LangZ;

            tinOutletPrevousRequestsModel.D.ADeclarationChkbox = "Checked";


            if (SelectedDeRegType.Equals(AppResources.TinDeregistration))
            {
                tinOutletPrevousRequestsModel.D.ADegister = "1";
                var ReasonCode = TinDeregReasons.Where(x => x.ReasonDesc == SelectedDeRegReason).Select(x => x.ReasonCd).FirstOrDefault();
                tinOutletPrevousRequestsModel.D.ADregReason = ReasonCode;
                tinOutletPrevousRequestsModel.D.TransactionTypez = "DEREG_T";

            }
            else
            {
                tinOutletPrevousRequestsModel.D.ADegister = "2";
                tinOutletPrevousRequestsModel.D.ADregReason = "";
            }

            foreach (var outlet in tinOutletPrevousRequestsModel.D.OutletSet.ToList())
            {
                try
                {
                    var index = tinOutletPrevousRequestsModel.D.OutletSet.IndexOf(outlet);
                    var uiOutlet = OutlettUiList.ElementAt<ContactInfo_NestedListView>(index);

                    outlet.AOutletActionTypeTb = GetOutletReasonCode(uiOutlet?.AoutletReason);

                    //outlet.AOutletEffDtTb =UtilityManager.convertToUniversalDate("MM/dd/yyyy", "yyyy-MM-ddTHH:mm:ss",uiOutlet.AOutletCloseTransferDtTb);
                    outlet.AOutletEffDtTb = UtilityManager.ConvertToDateFormat(uiOutlet.AOutletCloseTransferDtTb ?? null, "yyyy-MM-ddTHH:mm:ss");
                    outlet.AOutletEffDtTb = outlet.AOutletEffDtTb == "" ? null : outlet.AOutletEffDtTb;
                    //outlet.AOutletCloseTransferDtTb = UtilityManager.convertToUniversalDate("MM/dd/yyyy", "yyyy-MM-ddTHH:mm:ss", uiOutlet?.AOutletCloseTransferDtTb);
                    outlet.AOutletCloseTransferDtTb = UtilityManager.ConvertToDateFormat(uiOutlet?.AOutletCloseTransferDtTb ?? null, "yyyy-MM-ddTHH:mm:ss");
                    outlet.AOutletCloseTransferDtTb = outlet.AOutletCloseTransferDtTb == "" ? null : outlet.AOutletCloseTransferDtTb;
                    //outlet.AOutletIssueDtTb = uiOutlet.AOutletIssueDtTb;
                    //outlet.AOutletValidToTb = uiOutlet.AOutletValidToTb;

                    //outlet.AOutletIssueDtTb = UtilityManager.convertToUniversalDate("MM/dd/yyyy", "yyyy-MM-ddTHH:mm:ss", uiOutlet?.AOutletIssueDtTb);
                    outlet.AOutletIssueDtTb = UtilityManager.ConvertToDateFormat(uiOutlet?.AOutletIssueDtTb ?? null, "yyyy-MM-ddTHH:mm:ss");
                    outlet.AOutletIssueDtTb = outlet.AOutletIssueDtTb == "" ? null : outlet.AOutletIssueDtTb;

                    // outlet.AOutletValidToTb = UtilityManager.convertToUniversalDate("MM/dd/yyyy", "yyyy-MM-ddTHH:mm:ss", uiOutlet?.AOutletValidToTb);
                    outlet.AOutletValidToTb = UtilityManager.ConvertToDateFormat(uiOutlet?.AOutletValidToTb ?? null, "yyyy-MM-ddTHH:mm:ss");
                    outlet.AOutletValidToTb = outlet.AOutletValidToTb == "" ? null : outlet.AOutletValidToTb;
                    if (uiOutlet.IsOutletChecked)
                    {
                        outlet.AOutletToDeregTb = "1";
                    }
                    else
                    {
                        outlet.AOutletToDeregTb = "2";
                    }

                    if (outlet.AOutletTypeTb == "M" && uiOutlet.IsOutletChecked && DeregistrationType == 2)
                    {
                        outlet.AOutletNewMainOutnumTb = NewMainOutlet;
                    }

                    tinOutletPrevousRequestsModel.D.OutletSet[index] = outlet;
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                    IsLoading = false;
                }
            }


            foreach (var permit in tinOutletPrevousRequestsModel.D.PermitSet.ToList())
            {
                try
                {
                    var index = tinOutletPrevousRequestsModel.D.PermitSet.IndexOf(permit);

                    SelectedOutletItem = OutlettUiList.Where(i => i.AOutletNoTb == permit.APermitOutletnoTb).FirstOrDefault();

                    if (SelectedOutletItem != null && SelectedOutletItem.ContactDetails != null & SelectedOutletItem.ContactDetails.Count() > 0)
                    {

                        var uiPermits = SelectedOutletItem.ContactDetails.ToList();
                        foreach (var itemPermit in uiPermits)
                        {
                            if (itemPermit.APermitOutletNoTb == permit.APermitOutletnoTb)
                            {
                                permit.APermitDregRsnTb = GetPermitReasonCode(itemPermit.APermitReason);
                                permit.APermitCloseDate = UtilityManager.ConevrtSplittedDate(itemPermit.ALicenceCloseTransfer);
                                permit.APermitEffDtTb = UtilityManager.ConevrtSplittedDate(itemPermit.ALicenceCloseTransfer);
                                // permit.APermitValfrDtTb = itemPermit.APermitValfrDtTb;
                                permit.APermitValfrDtTb = UtilityManager.ConvertToDateFormat(itemPermit.APermitValfrDtTb ?? "", "yyyy-MM-ddTHH:mm:ss");
                                if (itemPermit.IsPermitChecked)
                                {
                                    permit.APermitCbFlag = "1";
                                }
                                else
                                {
                                    permit.APermitCbFlag = "2";
                                }

                                tinOutletPrevousRequestsModel.D.PermitSet[index] = permit;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    IsLoading = false;
                }
            }
            tinOutletPrevousRequestsModel.D.ErrMsgSet = new List<ErrMesgs>();
            tinOutletPrevousRequestsModel.D.OffNotesSet = new List<object>();
            tinOutletPrevousRequestsModel.D.OffNotesSet = new List<object>();
            if (IsLoading)
            {
                tinOutletPrevousRequestsModel = await TINDeregistrationWebServiceManager.PostSubmitOrSaveDraft(tinOutletPrevousRequestsModel.D);

                IsLoading = false;

                if (tinOutletPrevousRequestsModel != null && tinOutletPrevousRequestsModel.D != null)
                {
                    if (tinOutletPrevousRequestsModel.D.Cr2021popup.Length > 0 && tinOutletPrevousRequestsModel.D.Savez == "X")
                    {
                        await _dialogService.ShowMessage(tinOutletPrevousRequestsModel.D.Cr2021popup, AppResources.Information);
                        GoBackAftersubmission();
                    }
                    else if (tinOutletPrevousRequestsModel.D.Submitz == "X")
                    {
                        GotoDeregReasonsPage();
                        await Microsoft.Maui.Controls.Application.Current.MainPage.Navigation.PushAsync(new DeregistrationSuccessPageView(this));
                    }
                    else
                    {
                        GoBackAftersubmission();
                    }

                }
                else
                {
                    await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.ZError);
                }
            }
            else
            {
                await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.ZError);
            }


        }
        catch (Exception ex)
        {
            IsLoading = false;
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
        }


        IsLoading = false;

        return;

    }

    private void GoBackAftersubmission()
    {
        GotoDeregReasonsPage();

        if (DeregisterAttachmentsListViewData != null)
        {
            DeregisterAttachmentsListViewData.Clear();
        }


        var _navigation = Application.Current.MainPage.Navigation;
        foreach (var item in _navigation.NavigationStack)
        {
            if (item.GetType().Name == App.TinOutletDeRegRequestPageView)
            {
                _navigation.RemovePage(item);
                break;
            }
        }

        foreach (var item in _navigation.NavigationStack)
        {
            if (item.GetType().Name == App.TINOutletDeregistrationPageView)
            {
                _navigation.RemovePage(item);
                break;
            }
        }

        _navigationService.NavigateTo(App.TINOutletDeregistrationPageView);
    }

    private string GetOutletReasonCode(string aoutletReason)
    {
        if (string.IsNullOrEmpty(aoutletReason))
        {
            return "";
        }
        var list = new List<string>();

        if (!App.IsArabic)
        {
            list.Add("Close CR");
            list.Add("Transfer CR");
        }
        else
        {
            list.Add("ايقاف السجل");
            list.Add("نقل ملكية السجل");
        }

        var index = list.IndexOf(aoutletReason) + 1;
        return index.ToString();
    }

    private string GetPermitReasonCode(string aoutletReason)
    {
        if (string.IsNullOrEmpty(aoutletReason))
        {
            return "";
        }
        var list = new List<string>();

        if (!App.IsArabic)
        {
            list.Add("Close");
            list.Add("Transfer");
        }
        else
        {
            list.Add("إيقاف");
            list.Add("نقل ملكية");
        }
        var index = list.IndexOf(aoutletReason) + 1;
        return index.ToString();
    }

    private async void CopyResponseObjectToUiObject(TinOutletDeregisterListModel.OutletDeregisterListResponse outlettListResponse)
    {

        OutlettUiList.Clear();
        try
        {
            foreach (var item in OutlettListResponse.OutletSet)
            {
                ContactInfo_NestedListView newItem = new ContactInfo_NestedListView();

                newItem.AOutletStatusTb = item.AOutletStatusTb;
                newItem.AOutletTypeTb = item.AOutletTypeTb;
                newItem.AOutletNoTb = item.AOutletNoTb;
                newItem.AOutletNameTb = item.AOutletNameTb;
                newItem.AOutletIdentificationNoTb = item.AOutletCrNoTb;
                if (item.AOutletValidToTb != null)
                {
                    newItem.AOutletValidToTb = Convert.ToDateTime(item.AOutletValidToTb.ToString()).ToShortDateString();
                }

                if (item.AOutletCloseTransferDtTb != null)
                {
                    newItem.AOutletCloseTransferDtTb = Convert.ToDateTime(item.AOutletCloseTransferDtTb.ToString()).ToShortDateString();
                }

                if (item.AOutletIssueDtTb != null)
                {
                    newItem.AOutletIssueDtTb = Convert.ToDateTime(item.AOutletIssueDtTb.ToString()).ToShortDateString();
                }




                newItem.AOwner = item.AOwner;
                newItem.AActFlag = item.AActFlag;
                newItem.AOutletActionTypeTb = item.AOutletActionTypeTb;
                if (item.AOutletToDeregTb == "1")
                {
                    newItem.IsOutletChecked = true;
                }

                if (!string.IsNullOrEmpty(item.AOutletActionTypeTb))
                {
                    newItem.AoutletReason = GetOutletStatusById(item.AOutletActionTypeTb);
                }
                if (!string.IsNullOrEmpty(item.AOutletNewMainOutnumTb) && OutlettListResponse.OutletSet.Count > 1)
                {
                    ShowMainOutletDropDown = true;
                    NewMainOutlet = item.AOutletNewMainOutnumTb;
                }

                if (OutlettListResponse.Status == "E0001" || OutlettListResponse.Status == "IP011" || OutlettListResponse.Status == "IP017")
                {
                    ShowSaveSubmit = true;
                    if (item.AOutletHide == "X")
                    {
                        newItem.IsOutletChecked = true;
                        newItem.IsOutletCheckEnable = false;
                        newItem.IsDateEnable = false;
                        newItem.IsActionTypeEnabled = true;
                    }
                    else
                    {
                        if (DeregistrationType == 2)
                        {//Outlet Reg

                            //newItem.IsOutletChecked = true;
                            newItem.IsOutletCheckEnable = true;
                            newItem.IsDateEnable = true;
                            newItem.IsActionTypeEnabled = true;

                        }
                        else
                        {//Tin De Reg
                            newItem.IsOutletChecked = true;
                            newItem.IsOutletCheckEnable = false;
                            newItem.IsDateEnable = true;
                            newItem.IsActionTypeEnabled = true;
                        }
                    }

                    if (OutlettListResponse.Status == "IP011")
                    {
                        newItem.IsOutletCheckEnable = false;
                        newItem.IsDateEnable = false;
                        newItem.IsActionTypeEnabled = false;
                        EnableDeregType = false;
                        EnableDeregReason = false;
                    }
                }
                else
                {
                    //Show Application in only Display mode
                    newItem.IsOutletCheckEnable = false;
                    newItem.IsDateEnable = false;
                    newItem.IsActionTypeEnabled = false;
                    EnableDeregType = false;
                    EnableDeregReason = false;
                    ShowSaveSubmit = false;
                }

                newItem.ContactDetails = GetPermitSetsOfOutlets(item);

                OutlettUiList.Add(newItem);
            }

            AllOutletsSelected();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }


    }

    private void AllOutletsSelected()
    {
        if (OutlettUiList.Where(x => x.IsOutletChecked == true).ToList().Count == OutlettUiList.Count && DeregistrationType == 2)
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.OutletToTinDeRegistration, AppResources.Information, AppResources.Submit, AppResources.CancelText, (async (bool isConfirmed) =>
                    {
                        if (isConfirmed == true)
                        {
                            SelectedDeRegType = AppResources.TinDeregistration;
                            ShouldShowDeregReason = true;
                            DeregistrationType = 1;
                            Task.Run(() => GetTinOutletDeregistrationDataBeforeNewRequest(1)).Wait();
                        }
                    }));
                });
            }
            catch (Exception)
            {

            }
        }
    }

    private ObservableCollection<DetailsContactInfo> GetPermitSetsOfOutlets(TinOutletDeregisterListModel.OutletResult itemOutlet)
    {
        ObservableCollection<DetailsContactInfo> newList = new ObservableCollection<DetailsContactInfo>();
        try
        {
            foreach (var item in OutlettListResponse.PermitSet)
            {
                if (item.APermitOutletnoTb == itemOutlet.AOutletNoTb)
                {
                    DetailsContactInfo newItem = new DetailsContactInfo();
                    newItem.APermitNoTb = item.APermitNoTb;

                    if (item.APermitValfrDtTb != null)
                    {
                        newItem.APermitValfrDtTb = Convert.ToDateTime(item.APermitValfrDtTb.ToString()).ToShortDateString();
                    }
                    //    newItem.APermitValfrDtTb = item.APermitValfrDtTb;

                    newItem.APermitIssueName = item.APermitIssueName;
                    newItem.APermitOutletNoTb = item.APermitOutletnoTb;
                    if (!string.IsNullOrEmpty(item.APermitDregRsnTb))
                    {
                        newItem.APermitReason = GetPermitStatusById(item.APermitDregRsnTb);
                    }
                    if (item.APermitCloseDate != null)
                    {
                        newItem.ALicenceCloseTransfer = Convert.ToDateTime(item.APermitCloseDate.ToString()).ToShortDateString();
                    }
                    //   newItem.ALicenceCloseTransfer = item.APermitCloseDate;
                    if (item.APermitCbFlag == "1")
                    {
                        newItem.IsPermitChecked = true;
                    }
                    if (OutlettListResponse.Status == "E0001" || OutlettListResponse.Status == "IP011" || OutlettListResponse.Status == "IP017")
                    {
                        if (itemOutlet.AOutletHide == "X")
                        {
                            newItem.IsPermitChecked = true;
                            newItem.IsPermitCheckEnable = false;
                            newItem.IsPermitDateEnable = true;
                            newItem.IsPermitActionTypeEnable = true;
                        }
                        else
                        {
                            if (DeregistrationType == 2)
                            {//Outlet Reg

                                //newItem.IsPermitChecked = true;
                                newItem.IsPermitCheckEnable = true;
                                newItem.IsPermitDateEnable = true;
                                newItem.IsPermitActionTypeEnable = true;
                            }
                            else
                            {//Tin De Reg
                                newItem.IsPermitChecked = true;
                                newItem.IsPermitCheckEnable = false;
                                newItem.IsPermitDateEnable = true;
                                newItem.IsPermitActionTypeEnable = true;
                            }
                        }

                        if (OutlettListResponse.Status == "IP011")
                        {
                            newItem.IsPermitCheckEnable = false;
                            newItem.IsPermitDateEnable = false;
                            newItem.IsPermitActionTypeEnable = false;
                            EnableDeregType = false;
                            EnableDeregReason = false;
                        }
                    }
                    else
                    {
                        //Show Application in only Display mode
                        newItem.IsPermitCheckEnable = false;
                        newItem.IsPermitDateEnable = false;
                        newItem.IsPermitActionTypeEnable = false;

                    }

                    newList.Add(newItem);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }

        return newList;
    }


    private string GetOutletStatusById(string aOutletActionTypeTb)
    {
        if (string.IsNullOrEmpty(aOutletActionTypeTb))
        {
            return "";
        }
        var index = int.Parse(aOutletActionTypeTb);
        if (index == 0)
        {
            return "";
        }

        var list = new List<string>();

        if (!App.IsArabic)
        {
            list.Add("Close CR");
            list.Add("Transfer CR");
        }
        else
        {
            list.Add("ايقاف السجل");
            list.Add("نقل ملكية السجل");
        }
        var reason = list.ElementAt(index - 1);
        return reason.ToString();
    }

    private string GetPermitStatusById(string actionTypeDb)
    {
        if (string.IsNullOrEmpty(actionTypeDb))
        {
            return "";
        }
        var index = int.Parse(actionTypeDb);
        if (index == 0)
        {
            return "";
        }
        var list = new List<string>();

        if (!App.IsArabic)
        {
            list.Add("Close");
            list.Add("Transfer");
        }
        else
        {
            list.Add("إيقاف");
            list.Add("نقل ملكية");
        }
        var reason = list.ElementAt(index - 1);
        return reason.ToString();
    }


    public async void LoadReasonSet()
    {
        try
        {
            TinDeregistrationReasonSetData = await TINDeregistrationWebServiceManager.GaztTinDeregistrationReasonData();
            if (TinDeregistrationReasonSetData != null)
            {
                TinDeregReasons = TinDeregistrationReasonSetData.ReasonSet.ToList();
                setDeregReasonPickerModel();
            }
        }
        catch (InternetException ex)
        {
            
            
            await Task.Run(() =>
            {
                App.HideProgressView();
            });

            try
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    // await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));

                });
            }
            catch (Exception mex)
            {
                Console.WriteLine(mex.Message);
            }
        }
        catch (GAZTErrorException ex)
        {
            
            
        }

    }

    public void SetDefaultDate()
    {
        ObservableCollection<object> todaycollection = new ObservableCollection<object>();
        //Select today dates

        if (DateTime.Now.Date.Day < 10)
            todaycollection.Add("0" + DateTime.Now.Date.Day);
        else
            todaycollection.Add(DateTime.Now.Date.Day.ToString());
        if (DateTime.Now.Date.Month < 10)
            todaycollection.Add("0" + DateTime.Now.Date.Month);
        else
            todaycollection.Add(DateTime.Now.Date.Month.ToString());
        todaycollection.Add(DateTime.Now.Date.Year.ToString());
        TodayDateNormal = todaycollection;
        DefaultMonth = DateTime.Now.Date.Month;

        //TodayDateinHijri
        ObservableCollection<object> todaycollectionHijri = new ObservableCollection<object>();
        var calendar = new UmAlQuraCalendar();
        if (calendar.GetDayOfMonth(DateTime.Now.Date) < 10)
            todaycollectionHijri.Add("0" + calendar.GetDayOfMonth(DateTime.Now.Date).ToString());
        else
            todaycollectionHijri.Add(calendar.GetDayOfMonth(DateTime.Now.Date).ToString());
        if (calendar.GetMonth(DateTime.Now.Date) < 10)
            todaycollectionHijri.Add("0" + calendar.GetMonth(DateTime.Now.Date));
        else
            todaycollectionHijri.Add(calendar.GetMonth(DateTime.Now.Date).ToString());
        todaycollectionHijri.Add(calendar.GetYear(DateTime.Now.Date).ToString());

        TodayDateinHijri = todaycollectionHijri;
    }

    public string onOutletStatusSelection(ContactInfo_NestedListView outlet)
    {
        var AOwner = outlet.AOwner;
        var aactFlag = outlet.AActFlag;
        var msg = "";
        if (DeregistrationType == 1)//TIN
        {
            if (outlet.AOutletActionTypeTb == "1")
            {
                switch (aactFlag)
                {
                    case "1":
                        if (AOwner == "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "203").Select(x => x.MsgText).FirstOrDefault();
                        }
                        else
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "209").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                    case "2":
                        if (AOwner == "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "206").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                    case "3":
                        if (AOwner == "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "204").Select(x => x.MsgText).FirstOrDefault();
                        }
                        else
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "207").Select(x => x.MsgText).FirstOrDefault();
                        }

                        break;
                    case "4":

                        if (AOwner == "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "205").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                    case "5":
                        if (AOwner == "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "214").Select(x => x.MsgText).FirstOrDefault();
                        }
                        else
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "213").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;

                    case "6":

                        if (AOwner == "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "212").Select(x => x.MsgText).FirstOrDefault();
                        }
                        else
                        {
                        }
                        break;
                }
            }
            else
            {
                switch (aactFlag)
                {
                    case "1":
                        if (AOwner != "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "210").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                    case "2":
                        if (AOwner != "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "202").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                    case "3":
                        if (AOwner != "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "208").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;

                    case "4":
                        if (AOwner != "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "201").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;

                    case "5":
                        if (AOwner == "")
                        {
                        }
                        else
                        {
                        }
                        break;

                    case "6":
                        if (AOwner != "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "211").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;

                }
            }
        }
        else
        {
            if (outlet.AOutletActionTypeTb == "1")
            {
                switch (aactFlag)
                {

                    case "1":
                        if (AOwner == "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "008").Select(x => x.MsgText).FirstOrDefault();
                        }
                        else
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "001").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                    case "2":

                        if (AOwner == "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "011").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                    case "3":
                        if (AOwner == "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "009").Select(x => x.MsgText).FirstOrDefault();
                        }
                        else
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "003").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                    case "4":
                        if (AOwner == "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "010").Select(x => x.MsgText).FirstOrDefault();
                        }
                        else
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "005").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                    case "5":
                        if (AOwner == "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "015").Select(x => x.MsgText).FirstOrDefault();
                        }
                        else
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "014").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                    case "6":
                        if (AOwner == "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "013").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                }
            }
            else
            {
                switch (aactFlag)
                {

                    case "1":

                        if (AOwner != "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "002").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                    case "2":
                        if (AOwner != "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "007").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;

                    case "3":
                        if (AOwner != "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "004").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;

                    case "4":
                        if (AOwner != "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "006").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;

                    case "5":

                        break;

                    case "6":
                        if (AOwner != "")
                        {
                            msg = tinOutletPrevousRequestsModel.D.ErrMsgSet.Where(x => x.MsgId == "012").Select(x => x.MsgText).FirstOrDefault();
                        }
                        break;
                }
            }
        }
        if (!string.IsNullOrEmpty(msg))
        {
            _dialogService.ShowMessage(msg, AppResources.Information);
        }
        return msg;
    }

    public async Task ShowMainOutletPopUpAsync()
    {
        if (PickerModelMainOutlet != null)
        {
            PickerModelMainOutlet = null;
        }
        var list = new List<string>();

        list = tinOutletPrevousRequestsModel.D.OutletSet.Where(i => i.AOutletTypeTb != "M").Select(i => i.AOutletNoTb).ToList();

        GenericPickerModel genericPickerModel = new GenericPickerModel();
        genericPickerModel.PickerData = list;
        genericPickerModel.PickerTitle = "";
        genericPickerModel.PickerId = "OutletMainPicker";
        genericPickerModel.PageCode = 1;
        PickerModelMainOutlet = genericPickerModel;

        await MopupService.Instance.PushAsync(new PickerPageView(PickerModelMainOutlet));


    }

    public void CheckIsMainOutletSelectedAsync(string AOutletNoTb)
    {
        if (SelectedOutletItem.AOutletTypeTb == "M" && OutlettUiList.Count > 1)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                ShowMainOutletDropDown = true;
                await _dialogService.ShowMessage(string.Format(AppResources.MainOutletDeRegsMsg, AOutletNoTb), AppResources.OKText);
                ShowMainOutletPopUpAsync();
            });
        }
        else
        {
            AllOutletsSelected();
        }
    }

    internal void SelectAllPermitsOfOutlets(ContactInfo_NestedListView selectedOutletItem)
    {
        if (SelectedOutletItem.ContactDetails.Count > 0)
        {
            SelectedOutletItem.ContactDetails.Where(x => x.APermitOutletNoTb == SelectedOutletItem.AOutletNoTb).ToList().ForEach(x => x.IsPermitChecked = true);
        }
    }
}


