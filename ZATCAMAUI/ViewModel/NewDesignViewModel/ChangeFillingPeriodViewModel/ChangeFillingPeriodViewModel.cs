

using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ChageFillingPeriodModel;
using ZATCAMAUI.Core.Interfaces;
using Mopups.Services;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Instructions;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.NewDesign.ChangeFillingPeriodPages;
using ZATCAMAUI.Core.Enums;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel
{

    public class ChangeFillingPeriodViewModel : BaseViewModel
    {

        #region Variable

        public bool isSubmitted = false;
        int selectedPage = (int)PagesEnum.FrequencyDetailsView;

        #endregion

        #region Enums
        enum PagesEnum
        {
            FrequencyDetailsView,
            AttachmentsView,
            DeclarationView,
            SummaryView,
        }

        public enum PickerEnum
        {
            IdType,
            EffectiveDate
        }
        #endregion

        public PickerEnum selectedPicker = PickerEnum.EffectiveDate;



        private bool _isFrequencyDetailsChecked = false;
        public bool IsFrequencyDetailsChecked
        {
            get
            {
                return _isFrequencyDetailsChecked;
            }
            set
            {
                if (_isFrequencyDetailsChecked == value) return;

                _isFrequencyDetailsChecked = value;
                OnPropertyChanged("IsFrequencyDetailsChecked");
            }
        }

        private bool _isDecCheckBoxVisible = false;
        public bool IsDecCheckBoxVisible
        {
            get { return _isDecCheckBoxVisible; }
            set
            {
                if (_isDecCheckBoxVisible == value) return;

                _isDecCheckBoxVisible = value;
                OnPropertyChanged("IsDecCheckBoxVisible");
            }
        }

        #region Commands

        public ICommand GoBackClick { get; set; }
        public ICommand FrequencyContinueBtnTapped { get; set; }
        public ICommand AttachmentsContinueBtnTapped { get; set; }
        public ICommand DeclarationContinueBtnTapped { get; set; }
        public ICommand GoBackToFrequencyDetails { get; set; }
        public ICommand GoBackToAttachments { get; set; }
        public ICommand GoBackToDeclaration { get; set; }
        public ICommand ShowDatePicker { get; set; }
        public ICommand EffectiveDateSpinnerClicked { get; set; }
        public ICommand IdTypeSpinnerTapped { get; set; }
        public ICommand NewAttachmentTapped { get; set; }
        public ICommand SummaryContinueBtnTapped { get; set; }
        public ICommand onMoreOptionClicked { get; set; }
        public ICommand DashboardTapped { get; set; }
        public ICommand ReferenceNumberCopyTapped { get; set; }
        public ICommand DownloadAcknowledgement { get; set; }
        public ICommand OnAppearingChangeFillingPeriodPageCommand { get; set; }
        public ICommand OnIDNumberFocusChanged { get; set; }

        #endregion

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
        public int MaxIndex { get; private set; } = 4;

        private string _pickedDate = "";

        public string PickedDate
        {
            get { return _pickedDate; }
            set
            {
                if (_pickedDate == value) return;

                _pickedDate = value;
                OnPropertyChanged("PickedDate");
            }
        }

        private GenericDatePickerModel genericDatePickerModel;

        private string _contactPersonName = "";

        public string ContactPersonName
        {
            get { return _contactPersonName; }
            set
            {
                if (_contactPersonName == value) return;

                _contactPersonName = value;
                OnPropertyChanged("ContactPersonName");
            }
        }

        public string _idNumber = "";

        public string IDNumber
        {
            get { return _idNumber; }
            set
            {
                if (_idNumber == value) return;

                _idNumber = value;
                OnPropertyChanged("IDNumber");
            }
        }

        private bool _isIDVerified = false;
        public bool IsIDVerified
        {
            get { return _isIDVerified; }
            set
            {
                if (_isIDVerified == value) return;

                _isIDVerified = value;
                OnPropertyChanged("IsIDVerified");
            }
        }
        private bool _contractPersonEditable = false;
        public bool ContractPersonEditable
        {
            get { return _contractPersonEditable; }
            set
            {
                if (_contractPersonEditable == value) return;

                _contractPersonEditable = value;
                OnPropertyChanged("ContractPersonEditable");
            }
        }

        private string _idType = "";

        public string IDType
        {
            get { return _idType; }
            set
            {
                if (_idType == value) return;

                _idType = value;
                OnPropertyChanged("IDType");
            }
        }

        private bool _isDOBVisible = false;

        public bool IsDOBVisible
        {
            get { return _isDOBVisible; }
            set
            {
                if (_isDOBVisible == value) return;

                _isDOBVisible = value;
                OnPropertyChanged("IsDOBVisible");
            }
        }

        private bool _isAtachmentsVisible = false;

        public bool IsAtachmentsVisible
        {
            get { return _isAtachmentsVisible; }
            set
            {
                if (_isAtachmentsVisible == value) return;

                _isAtachmentsVisible = value;
                OnPropertyChanged("IsAtachmentsVisible");
            }
        }

        private string _effectiveDatePicked = "";

        public string EffectiveDatePicked
        {
            get { return _effectiveDatePicked; }
            set
            {
                if (_effectiveDatePicked == value) return;

                _effectiveDatePicked = value;
                OnPropertyChanged("EffectiveDatePicked");
            }
        }

        private GenericPickerModel _idTypePickerModel { get; set; }

        public GenericPickerModel IDTypePickerModel
        {
            get { return _idTypePickerModel; }
            set
            {
                if (_idTypePickerModel == value) return;

                _idTypePickerModel = value;
                OnPropertyChanged("IDTypePickerModel");
            }
        }

        private GenericPickerModel _effectiveDatePickerModel { get; set; }

        public GenericPickerModel EffectiveDatePickerModel
        {
            get { return _effectiveDatePickerModel; }
            set
            {
                if (_effectiveDatePickerModel == value) return;

                _effectiveDatePickerModel = value;
                OnPropertyChanged("EffectiveDatePickerModel");
            }
        }


        private bool _isFrequencyDetailsEnabled = false;
        public bool IsFrequencyDetailsEnabled
        {
            get { return _isFrequencyDetailsEnabled; }
            set
            {
                if (_isFrequencyDetailsEnabled == value) return;

                _isFrequencyDetailsEnabled = value;
                FrequencyDetailsButtonBackGroundColor = (_isFrequencyDetailsEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
                OnPropertyChanged("IsFrequencyDetailsEnabled");
            }
        }
        private Color _frequencyDetailsButtonBackGroundColor = (Color)Application.Current.Resources["ButtonGray"];
        public Color FrequencyDetailsButtonBackGroundColor
        {
            get
            {
                return _frequencyDetailsButtonBackGroundColor;
            }
            set
            {
                if (_frequencyDetailsButtonBackGroundColor == value)
                {
                    return;
                }
                _frequencyDetailsButtonBackGroundColor = value;
                OnPropertyChanged("FrequencyDetailsButtonBackGroundColor");
            }
        }
        private bool _isAttachmentsEnabled = false;
        public bool IsAttachmentsEnabled
        {
            get { return _isAttachmentsEnabled; }
            set
            {
                if (_isAttachmentsEnabled == value) return;

                _isAttachmentsEnabled = value;
                AttachButtonBackGroundColor = (_isAttachmentsEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
                OnPropertyChanged("IsAttachmentsEnabled");
            }
        }
        private Color _attachButtonBackGroundColor = (Color)Application.Current.Resources["ButtonGray"];
        public Color AttachButtonBackGroundColor
        {
            get
            {
                return _attachButtonBackGroundColor;
            }
            set
            {
                if (_attachButtonBackGroundColor == value)
                {
                    return;
                }
                _attachButtonBackGroundColor = value;
                OnPropertyChanged("AttachButtonBackGroundColor");
            }
        }
        private bool _isDeclarationEnabled = false;
        public bool IsDeclarationEnabled
        {
            get { return _isDeclarationEnabled; }
            set
            {
                if (_isDeclarationEnabled == value) return;

                _isDeclarationEnabled = value;
                DeclarationButtonBackGroundColor = (_isDeclarationEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
                OnPropertyChanged("IsDeclarationEnabled");
            }
        }
        private Color _declarationButtonBackGroundColor = (Color)Application.Current.Resources["ButtonGray"];
        public Color DeclarationButtonBackGroundColor
        {
            get
            {
                return _declarationButtonBackGroundColor;
            }
            set
            {
                if (_declarationButtonBackGroundColor == value)
                {
                    return;
                }
                _declarationButtonBackGroundColor = value;
                OnPropertyChanged("DeclarationButtonBackGroundColor");
            }
        }

        public ObservableCollection<Attachment> yearsattachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> YearsattachmentsListViewData
        {
            get { return yearsattachmentsListViewData; }

            set
            {
                if (yearsattachmentsListViewData == value)
                {
                    return;
                }

                yearsattachmentsListViewData = value;
                OnPropertyChanged("YearsattachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> monthsattachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> MonthsattachmentsListViewData
        {
            get { return monthsattachmentsListViewData; }

            set
            {
                if (monthsattachmentsListViewData == value)
                {
                    return;
                }

                monthsattachmentsListViewData = value;
                OnPropertyChanged("MonthsattachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> otherAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> OtherAttachmentsListViewData
        {
            get { return otherAttachmentsListViewData; }

            set
            {
                if (otherAttachmentsListViewData == value)
                {
                    return;
                }

                otherAttachmentsListViewData = value;
                OnPropertyChanged("OtherAttachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> attachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> AttachmentsListViewData
        {
            get { return attachmentsListViewData; }

            set
            {
                if (attachmentsListViewData == value)
                {
                    return;
                }

                attachmentsListViewData = value;
                OnPropertyChanged("AttachmentsListViewData");
            }
        }

        private void Backnavigations()
        {
            switch (selectedPage)
            {
                case (int)PagesEnum.FrequencyDetailsView:
                    _navigationService.GoBack();
                    break;
                case (int)PagesEnum.AttachmentsView:
                    EnableFrequencyDetailsView();
                    break;

                case (int)PagesEnum.DeclarationView:
                    if (IsAtachmentsVisible)
                    {
                        EnableAttachmentsView();
                    }
                    else
                    {
                        EnableFrequencyDetailsView();
                    }

                    break;
                case (int)PagesEnum.SummaryView:
                    EnableDeclarationView();
                    break;

                default:
                    break;
            }
        }

        private VATChangeFillingPeriodRequestModel _changeFillingResponse { get; set; }

        public VATChangeFillingPeriodRequestModel ChangeFillingResponse
        {
            get { return _changeFillingResponse; }
            set
            {
                if (_changeFillingResponse == value) return;

                _changeFillingResponse = value;
                OnPropertyChanged("ChangeFillingResponse");
            }
        }

        private VATRefillingDropdownModel _effectiveDateResponse { get; set; }

        public VATRefillingDropdownModel EffectiveDateResponse
        {
            get { return _effectiveDateResponse; }
            set
            {
                if (_effectiveDateResponse == value) return;

                _effectiveDateResponse = value;
                OnPropertyChanged("EffectiveDateResponse");
            }
        }

        private bool _isDeclarationViewEnabledNew = false;
        public bool IsDeclarationViewEnabledNew
        {
            get
            {
                return _isDeclarationViewEnabledNew;
            }
            set
            {
                if (_isDeclarationViewEnabledNew == value) return;

                _isDeclarationViewEnabledNew = value;
                OnPropertyChanged("IsDeclarationViewEnabledNew");
            }
        }

        private bool _isDeclarationViewEnabledOld = false;
        public bool IsDeclarationViewEnabledOld
        {
            get
            {
                return _isDeclarationViewEnabledOld;
            }
            set
            {
                if (_isDeclarationViewEnabledOld == value) return;

                _isDeclarationViewEnabledOld = value;
                OnPropertyChanged("IsDeclarationViewEnabledOld");
            }
        }

        public VATDeregDeclaration _vatDeregDeclaration;
        public VATDeregDeclaration VatDeregDeclaration
        {
            get
            {
                return _vatDeregDeclaration;
            }
            set
            {
                if (_vatDeregDeclaration == value) return;

                _vatDeregDeclaration = value;
                OnPropertyChanged("VatDeregDeclaration");
            }
        }

        public string _zterms;
        public string Zterms
        {
            get
            {
                return _zterms;
            }
            set
            {
                if (_zterms == value) return;

                _zterms = value;
                OnPropertyChanged("Zterms");
            }
        }


        private Dictionary<string, string> IDTypeDictionary = null;
        private Dictionary<string, string> IDValueDictionary = null;


        public ChangeFillingInterface cFInterface { get; set; }



        public ChangeFillingPeriodViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            ShowDatePicker = new Command(async () =>
            {
                await ShowDatePickerDialog();
            });

            EffectiveDateSpinnerClicked = new Command(async () =>
            {
                await ShowEffectiveDatePickerDialog();
            });

            IdTypeSpinnerTapped = new Command(async () =>
            {
                await ShowIdTypePickerDialog();
            });

            GoBackClick = new Command(() =>
            {
                Backnavigations();
            });
            onMoreOptionClicked = new Command(async () =>
            {
                await MopupService.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(ListOfActionButtonsApplicable));
            });

            DownloadAcknowledgement = new Command(async () =>
            {
                if (ChangeFillingResponse.d1.Fbnumz != null)
                {
                    String downloadurl = ZATCAConstants.downloadFile + ChangeFillingResponse.d1.Fbnumz;
                    await _navigationService.NavigateTo(App.PdfView, downloadurl);

                }
            });

            OnIDNumberFocusChanged = new Command(async () =>
            {
                await ValidateIdNumber();
            });

            OnAppearingChangeFillingPeriodPageCommand = new Command(async () =>
            {
                IsLoading = true;
                ResetData();
                await GetVATChangeFillingData();

                getYesCommand();
                getNoCommand();

                MessagingCenter.Subscribe<object, string>(this, "SaveCommandReceived", async (sender, arg) =>
                {
                    await MopupService.Instance.PopAsync();
                    if (arg != null)
                    {
                        string message = arg;
                        if (App.IsArabic)
                        {
                            ArButtons buttonId = ArButtons.None;
                            if (!string.IsNullOrEmpty(message))
                            {
                                message = message.Replace(" ", "");
                            }
                            Enum.TryParse(message, out buttonId);
                            switch (buttonId)
                            {
                                case ArButtons.إضافةملاحظات:
                                    break;
                                case ArButtons.عرضملاحظات:
                                    break;
                                case ArButtons.المرفقات:
                                    break;
                                case ArButtons.إلغاء:
                                    isDraftClicked = true;
                                    await VoidMsg();
                                    isDraftClicked = false;
                                    break;
                                case ArButtons.عادةتعيين:
                                    break;
                                case ArButtons.تعديل:
                                    break;
                                case ArButtons.حفظكمسودة:
                                    isDraftClicked = true;
                                    await OnSaveDraftClicked();
                                    isDraftClicked = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Buttons buttonId = Buttons.None;
                            if (!string.IsNullOrEmpty(message))
                            {
                                message = message.Replace(" ", "");
                            }
                            Enum.TryParse(message, out buttonId);
                            switch (buttonId)
                            {
                                case Buttons.CreateNotes:
                                    break;
                                case Buttons.DisplayNotes:
                                    break;
                                case Buttons.Attachments:
                                    break;
                                case Buttons.Void:
                                    isDraftClicked = true;
                                    await VoidMsg();
                                    isDraftClicked = false;
                                    break;
                                case Buttons.Reset:
                                    break;
                                case Buttons.Amend:
                                    break;
                                case Buttons.SaveasDraft:
                                    isDraftClicked = true;
                                    await OnSaveDraftClicked();
                                    isDraftClicked = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                });

                MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem",
                    async (sender, arg) =>
                    {

                        PickedDate = arg.SelectedValue;
                        await ValidateIdNumber();
                    });

                MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", async (sender, arg) =>
                {
                    if (selectedPicker == ChangeFillingPeriodViewModel.PickerEnum.EffectiveDate)
                    {
                        EffectiveDatePickerModel = arg;
                        updateEffectiveDatePicker();
                    }
                    else if (selectedPicker == ChangeFillingPeriodViewModel.PickerEnum.IdType)
                    {
                        IDTypePickerModel = arg;
                        await updateIdTypePicker();
                    }
                });

                MessagingCenter.Subscribe<object, AttachmentsList>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        PopulateAttachments(arg.results);
                    }
                });
                IsLoading = false;
            });

            ReferenceNumberCopyTapped = new Command(async () =>
            {
                try
                {
                    if (ChangeFillingResponse.d1.Fbnumz != null)
                    {
                        await Clipboard.SetTextAsync(ChangeFillingResponse.d1.Fbnumz);
                        if (Clipboard.HasText)
                        {
                            var text = await Clipboard.GetTextAsync();
                            await _dialogService.ShowMessageBox(
                                 AppResources.CRReferenceNumber + " " + text, AppResources.Copied);
                        }
                    }
                }
                catch (Exception)
                {
                }
            });

            DashboardTapped = new Command(async () =>
            {
                var _navigation = Application.Current.MainPage.Navigation;
                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.ChangeFillingPeriodPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }

                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.ChangeFillingPeriodListPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }

                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.ChangeFillingPeriodSuccessPage)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }
               await _navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);
            });

            FrequencyContinueBtnTapped = new Command(async () => await this.FrequencyContinueBtnClicked());
            AttachmentsContinueBtnTapped = new Command(async () => await this.AttachmentsContinueBtnClicked());
            DeclarationContinueBtnTapped = new Command(async () => await this.DeclarationContinueBtnClicked());
            GoBackToFrequencyDetails = new Command(async () => await this.GoBackToFrequencyDetailsClicked());
            GoBackToAttachments = new Command(async () => await this.GoBackToAttachmentsClicked());
            GoBackToDeclaration = new Command(async () => await this.GoBackToDeclarationClicked());
            NewAttachmentTapped = new Command(async () => await this.NewAttachmentClicked());
            SummaryContinueBtnTapped = new Command(async () => await this.SummaryContinueBtnClicked());
            setIdPickerModel();
            SelectedOutletOption = new ChangeFillingPeriodModel();

            genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregDOBDatePickerTitle;
            genericDatePickerModel.PickerId = "DatePicker";
        }


        public void SetMoreOptioButtons()
        {
            var listOfActionButtonsApplicable = new List<string>();
            if (App.selectedVatFillingItem != "")
            {

                if (ChangeFillingResponse != null && ChangeFillingResponse.d != null)
                {
                    if (ChangeFillingResponse.d.Fbust == "E0013")
                    {
                        listOfActionButtonsApplicable.Add(AppResources.ZZVoid);
                    }
                }
            }
            listOfActionButtonsApplicable.Add(AppResources.ZZSaveAsDraft);
            ListOfActionButtonsApplicable = listOfActionButtonsApplicable;
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
                _ListOfActionButtonsApplicable = value;
                OnPropertyChanged("ListOfActionButtonsApplicable");
            }
        }

        public void ResetData()
        {


            IDTypeDictionary = new Dictionary<string, string>
        {
            {AppResources.VFCNationalID,"ZS0001"},
            {AppResources.VFCIqamaID,"ZS0002"},
            {AppResources.VFCGCCID,"ZS0003"},
        };

            IDValueDictionary = new Dictionary<string, string>
        {
            {"ZS0001",AppResources.VFCNationalID},
            {"ZS0002",AppResources.VFCIqamaID},
            {"ZS0003",AppResources.VFCGCCID},
        };

            EnableFrequencyDetailsView();
            IsFrequencyDetailsChecked = false;
            ContactPersonName = "";
            IDNumber = "";
            IsAtachmentsVisible = false;
            IsIDVerified = false;
            ContractPersonEditable = false;
            IDType = "";
            IsDOBVisible = false;
            EffectiveDatePicked = "";
            AttachmentsListViewData = null;
            YearsattachmentsListViewData = null;
            MonthsattachmentsListViewData = null;
            OtherAttachmentsListViewData = null;
            IsFrequencyDetailsEnabled = false;
            IsAttachmentsEnabled = false;
            IsDeclarationEnabled = false;
            CurrentFrequency = "";
            NewFrequency = "";
            isSubmitted = false;
            IsTwoYearsAtachmentsVisible = false;
            IsMonthsAtachmentsVisible = false;
            IsOthersAtachmentsVisible = false;
            ShowAttachments = false;

            IsCheckboxChecked = false;
            setIdPickerModel();
            SetMoreOptioButtons();
        }


        public bool isDraftClicked = false;
        public async Task OnSaveDraftClicked()
        {



            try
            {
                IsLoading = true;
                ChangeFillingResponse.d.Operationz = "05";
                if (!isDraftClicked)
                {
                    isDraftClicked = true;
                    ChangeFillingResponse = await SubmitClicked();
                    if (ChangeFillingResponse != null && ChangeFillingResponse.d1 != null)
                    {

                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            App.selectedVatFillingItem = ChangeFillingResponse.d1.Fbnumz;
                            SetMoreOptioButtons();

                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = string.Format(AppResources.VATFillingDraftSaved, "  " + ChangeFillingResponse.d1.Fbnumz);

                            headerWithInfos.Add(headerAmountInfo);

                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                        });
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            IsLoading = false;
                            await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        }
                        else
                        {

                            IsLoading = false;
                            await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        }
                    }
                }
                IsLoading = false;
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

        public async Task VATSetReturnVoidAsync()
        {
            ChangeFillingResponse.d.Operationz = "04";

            try
            {
                IsLoading = true;
                if (!isDraftClicked)
                {
                    isDraftClicked = true;
                    ChangeFillingResponse = await SubmitClicked();
                    isDraftClicked = false;
                    if (ChangeFillingResponse != null && ChangeFillingResponse.d1 != null)
                    {
                        List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                        HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                        NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                        headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                        headerAmountInfo.IsLinkAvailable = false;
                        headerAmountInfo.Message = AppResources.ZZGeneralMessage_VATFillingCancelled;

                        headerWithInfos.Add(headerAmountInfo);

                        newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                        newDesignPopUp.HeaderWithInfos = headerWithInfos;
                        newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                        await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                        _navigationService.GoBack();
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
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
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
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
                IsLoading = false;
            }
            catch (Exception)
            {


            }
        }

        public void PopulateDraftData()
        {
            //foreach (var att in EffectiveDateResponse.d.EffDateSet.results)
            foreach (var att in EffectiveDateResponse.d.EffDateSet)
            {
                if (att.Persl == ChangeFillingResponse.d.CPersl)
                {
                    EffectiveDatePicked = att.Txt50;
                }
            }
            if (ChangeFillingResponse.d.Iagrfg == "0")
            {
                IsFrequencyDetailsChecked = false;
            }
            else
            {
                IsFrequencyDetailsChecked = true;
            }
            if (ChangeFillingResponse.d.DecidTy != "")
            {

                IDType = IDValueDictionary[ChangeFillingResponse.d.DecidTy];
                IsIDVerified = true;
                ContactPersonName = ChangeFillingResponse.d.Decname;
                IDNumber = ChangeFillingResponse.d.DecidNo;
                if (IDType == AppResources.VFCGCCID)
                {
                    IsDOBVisible = false;
                    ContractPersonEditable = true;
                }
                else
                {
                    IsDOBVisible = true;
                    ContractPersonEditable = false;
                }
            }
            if (ChangeFillingResponse.d.Decfg != "")
            {
                if (ChangeFillingResponse.d.Decfg == "0")
                {
                    IsCheckboxChecked = false;
                }
                else
                {
                    IsCheckboxChecked = true;
                }
            }
            var yearsAttachments = new ObservableCollection<Attachment>();
            var monthsAttachments = new ObservableCollection<Attachment>();
            var othersAttachments = new ObservableCollection<Attachment>();
            foreach (var attach in ChangeFillingResponse.d.ATTACHSet)
            {
                if (attach.Dotyp == "ZTPA")
                {
                    yearsAttachments.Add(attach);
                }
                else if (attach.Dotyp == "ZTPB")
                {
                    monthsAttachments.Add(attach);
                }
                else if (attach.Dotyp == "ZTPC")
                {
                    othersAttachments.Add(attach);
                }
            }
            YearsattachmentsListViewData = yearsAttachments;
            MonthsattachmentsListViewData = monthsAttachments;
            OtherAttachmentsListViewData = othersAttachments;
            EnableFrequencyDetails();
            EnableDeclaration();
            EnableAttachments();
        }


        public async Task ValidateIdNumber()
        {
            try
            {
                IsIDVerified = false;
                EnableDeclaration();
                PopUp popUp = new PopUp();
                StringBuilder Messages = new StringBuilder();
                if (!string.IsNullOrEmpty(IDNumber))
                {
                    if (IDType.Equals(AppResources.VFCNationalID))
                    {
                        if (IDNumber.Substring(0, 1) != "1")
                        {
                            popUp.Message = AppResources.ZZNationalIDstartswith1;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));

                            IDNumber = string.Empty;
                        }
                        else
                        {
                            if (IDNumber.Length != 10)
                            {
                                if (Messages.Length > 0)
                                {
                                    Messages.Append(Environment.NewLine);
                                }
                                Messages.Append(AppResources.ZZNationalIDlengthis10digit);
                            }
                            if (Messages.Length > 0)
                            {
                                popUp.Message = Messages.ToString();
                                popUp.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    popUp.FlowDirections = "RightToLeft";
                                    popUp.isFontSet = true;
                                }
                                else
                                {
                                    popUp.FlowDirections = "LeftToRight";
                                }

                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));

                                IDNumber = string.Empty;
                            }
                            else
                            {

                                if (!string.IsNullOrEmpty(PickedDate))
                                {
                                    await ValidateIdNumberFromApi("ZS0001");
                                }
                            }
                        }
                    }
                    if (IDType.Equals(AppResources.VFCIqamaID))
                    {
                        if (IDNumber.Substring(0, 1) != "2")
                        {
                            popUp.Message = AppResources.ZZIqamaIDstartswith2;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));

                            IDNumber = string.Empty;
                        }
                        else
                        {
                            if (IDNumber.Length != 10)
                            {
                                if (Messages.Length > 0)
                                {
                                    Messages.Append(Environment.NewLine);
                                }
                                Messages.Append(AppResources.ZZIqamaIDlengthis10digit);
                            }
                            if (Messages.Length > 0)
                            {
                                popUp.Message = Messages.ToString();
                                popUp.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    popUp.FlowDirections = "RightToLeft";
                                    popUp.isFontSet = true;
                                }
                                else
                                {
                                    popUp.FlowDirections = "LeftToRight";
                                }
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));

                                IDNumber = string.Empty;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(PickedDate))
                                {
                                    await ValidateIdNumberFromApi("ZS0002");
                                }
                            }
                        }
                    }
                    if (IDType.Equals(AppResources.VFCGCCID))
                    {

                        if (IDNumber.Substring(0, 1) == "0")
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.ZZGCCIDdonotstartwith0;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));

                            IDNumber = string.Empty;
                        }
                        else if (!(IDNumber.Length <= 15 && IDNumber.Length >= 7))
                        {
                            popUp.Message = AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));

                            IDNumber = string.Empty;
                        }
                        else
                        {
                            IsIDVerified = true;
                            EnableDeclaration();
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
        }

        public async Task ShowInstructionDialog()
        {
            if (App.selectedVatFillingItem != "")
            {
                if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018")
                {
                    await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(
                instructionString: AppResources.VFCInstructions, checkBoxString: AppResources.VFCCheckBoxDesc,
                continueString: AppResources.CRContinue,
                isEditable: true,
                _dialogType: InstructionsBottomPopUpViewModel.DialogType
                    .Instructions));
                }
                else
                {
                    await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(
                                    instructionString: AppResources.VFCInstructions, checkBoxString: AppResources.VFCCheckBoxDesc,
                                    continueString: AppResources.CRContinue,
                                    _dialogType: InstructionsBottomPopUpViewModel.DialogType
                                        .Instructions));
                }
            }
            else
            {
                await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(
                                   instructionString: AppResources.VFCInstructions, checkBoxString: AppResources.VFCCheckBoxDesc,
                                   continueString: AppResources.CRContinue,
                                   _dialogType: InstructionsBottomPopUpViewModel.DialogType
                                       .Instructions));
            }
        }
        private async Task ShowDatePickerDialog()
        {
            try
            {
                await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        private async Task ShowEffectiveDatePickerDialog()
        {
            try
            {
                selectedPicker = PickerEnum.EffectiveDate;
                await MopupService.Instance.PushAsync(new PickerPageView(EffectiveDatePickerModel));
            }

            catch (InternetException ex)
            {

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        private async Task ShowIdTypePickerDialog()
        {
            try
            {
                selectedPicker = PickerEnum.IdType;
                await MopupService.Instance.PushAsync(new PickerPageView(IDTypePickerModel));
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public void updateEffectiveDatePicker()
        {
            EffectiveDatePicked = EffectiveDatePickerModel.SelectedValue;
            EnableFrequencyDetails();
        }

        public async Task updateIdTypePicker()
        {
            IDType = IDTypePickerModel.SelectedValue;
            ContactPersonName = "";
            IDNumber = "";
            if (IDType == AppResources.VFCGCCID)
            {
                IsDOBVisible = false;
                ContractPersonEditable = true;
            }
            else
            {
                IsDOBVisible = true;
                ContractPersonEditable = false;
            }

            await ValidateIdNumber();
        }
        private void setEffectiveDatePickerModel()
        {
            var list = new List<string>();
            // foreach (var date in EffectiveDateResponse.d.EffDateSet.results)
            foreach (var date in EffectiveDateResponse.d.EffDateSet)
            {
                list.Add(date.Txt50);
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = AppResources.VFCEffectiveDate;
            genericPickerModel.PickerId = "Effective Date";

            EffectiveDatePickerModel = genericPickerModel;
        }

        private void setIdPickerModel()
        {
            List<string> iDTypes = new List<string>();
            iDTypes.Add(AppResources.VFCNationalID);
            iDTypes.Add(AppResources.VFCIqamaID);
            iDTypes.Add(AppResources.VFCGCCID);


            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = iDTypes;
            genericPickerModel.PickerTitle = AppResources.VFCIDType;
            genericPickerModel.PickerId = AppResources.ZZIDType;

            IDTypePickerModel = genericPickerModel;
        }

        public async Task NewAttachmentClicked()
        {

            try
            {
                if (MopupService.Instance.PopupStack.Count > 0) return;
                if (SelectedOutletOptionIndex == 0)
                {
                    if (YearsattachmentsListViewData == null)
                    {
                        YearsattachmentsListViewData = new ObservableCollection<Attachment>();
                    }
                    await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                 YearsattachmentsListViewData.ToList(),
                 WhichAttachment.ChangeFillingPeriod2Years, ChangeFillingResponse.d.ReturnId));
                }
                else if (SelectedOutletOptionIndex == 1)
                {
                    if (MonthsattachmentsListViewData == null)
                    {
                        MonthsattachmentsListViewData = new ObservableCollection<Attachment>();
                    }
                    await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                 MonthsattachmentsListViewData.ToList(),
                 WhichAttachment.ChangeFillingPeriod12Months, ChangeFillingResponse.d.ReturnId));
                }
                else
                {
                    if (OtherAttachmentsListViewData == null)
                    {
                        OtherAttachmentsListViewData = new ObservableCollection<Attachment>();
                    }
                    await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                 OtherAttachmentsListViewData.ToList(),
                 WhichAttachment.ChangeFillingPeriodOtherDoc, ChangeFillingResponse.d.ReturnId));
                }
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public void EnableFrequencyDetailsView()
        {
            CurrentIndex = 1;
            IsFrequencyViewEnabled = true;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsDeclarationViewEnabledNew = false;
            IsSummaryViewEnabled = false;
            IsBackVisible = true;
            selectedPage = (int)PagesEnum.FrequencyDetailsView;
        }

        public void EnableAttachmentsView()
        {
            CurrentIndex = 2;
            IsFrequencyViewEnabled = false;
            IsAttachmentsViewEnabled = true;
            IsDeclarationViewEnabled = false;
            IsDeclarationViewEnabledNew = false;
            IsSummaryViewEnabled = false;
            IsBackVisible = true;
            selectedPage = (int)PagesEnum.AttachmentsView;
        }

        public void EnableDeclarationView()
        {
            CurrentIndex = 3;
            IsFrequencyViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = true;
            if (VatDeregDeclaration != null && VatDeregDeclaration.D != null && string.IsNullOrEmpty(VatDeregDeclaration.D.Zterms))
            {
                IsDeclarationViewEnabledOld = true;
                IsDeclarationViewEnabledNew = false;
            }
            else
            {
                IsDeclarationViewEnabledOld = false;
                IsDeclarationViewEnabledNew = true;
                Zterms = VatDeregDeclaration.D.Zterms;
            }
            IsSummaryViewEnabled = false;
            IsBackVisible = true;
            selectedPage = (int)PagesEnum.DeclarationView;
        }

        public void EnableSummaryView()
        {
            CurrentIndex = 4;
            IsFrequencyViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = true;
            IsBackVisible = true;
            selectedPage = (int)PagesEnum.SummaryView;
        }

        private bool _isBackVisible = false;
        public bool IsBackVisible
        {
            get
            {
                return _isBackVisible;
            }
            set
            {
                if (_isBackVisible == value) return;
                _isBackVisible = value;
                OnPropertyChanged("IsBackVisible");
            }
        }

        private bool _isFrequencyViewEnabled = false;
        public bool IsFrequencyViewEnabled
        {
            get
            {
                return _isFrequencyViewEnabled;
            }
            set
            {
                if (_isFrequencyViewEnabled == value) return;

                _isFrequencyViewEnabled = value;
                OnPropertyChanged("IsFrequencyViewEnabled");
            }
        }


        private bool _isAttachmentsViewEnabled = false;
        public bool IsAttachmentsViewEnabled
        {
            get
            {
                return _isAttachmentsViewEnabled;
            }
            set
            {
                if (_isAttachmentsViewEnabled == value) return;

                _isAttachmentsViewEnabled = value;
                OnPropertyChanged("IsAttachmentsViewEnabled");
            }
        }

        private bool _isDeclarationViewEnabled = false;
        public bool IsDeclarationViewEnabled
        {
            get
            {
                return _isDeclarationViewEnabled;
            }
            set
            {
                if (_isDeclarationViewEnabled == value) return;

                _isDeclarationViewEnabled = value;
                OnPropertyChanged("IsDeclarationViewEnabled");
            }
        }

        private bool _isSummaryViewEnabled = false;
        public bool IsSummaryViewEnabled
        {
            get
            {
                return _isSummaryViewEnabled;
            }
            set
            {
                if (_isSummaryViewEnabled == value) return;

                _isSummaryViewEnabled = value;
                OnPropertyChanged("IsSummaryViewEnabled");
            }
        }

        private bool _showAttachments = false;
        public bool ShowAttachments
        {
            get
            {
                return _showAttachments;
            }
            set
            {
                if (_showAttachments == value) return;

                _showAttachments = value;
                OnPropertyChanged("ShowAttachments");
            }
        }


        private int _selectedOutletOptionIndex;
        public int SelectedOutletOptionIndex
        {
            get
            {
                return _selectedOutletOptionIndex;
            }
            set
            {
                if (_selectedOutletOptionIndex == value) return;

                _selectedOutletOptionIndex = value;
                EnableAttachments();

                OnPropertyChanged("SelectedOutletOptionIndex");
            }
        }

        private string _selectedAttachmentText = "";
        public string SelectedAttachmentText
        {
            get
            {
                return _selectedAttachmentText;
            }
            set
            {
                if (_selectedAttachmentText == value) return;

                _selectedAttachmentText = value;
                OnPropertyChanged("SelectedAttachmentText");
            }
        }


        private string _referenceNumber = "";
        public string ReferenceNumber
        {
            get
            {
                return _referenceNumber;
            }
            set
            {
                if (_referenceNumber == value) return;

                _referenceNumber = value;
                OnPropertyChanged("ReferenceNumber");
            }
        }

        private string _currentFrequency = "";
        public string CurrentFrequency
        {
            get
            {
                return _currentFrequency;
            }
            set
            {
                if (_currentFrequency == value) return;

                _currentFrequency = value;
                OnPropertyChanged("CurrentFrequency");
            }
        }

        private string _newFrequency = "";
        public string NewFrequency
        {
            get
            {
                return _newFrequency;
            }
            set
            {
                if (_newFrequency == value) return;

                _newFrequency = value;
                OnPropertyChanged("NewFrequency");
            }
        }

        private bool _isTwoYearsAtachmentsVisible = false;
        public bool IsTwoYearsAtachmentsVisible
        {
            get { return _isTwoYearsAtachmentsVisible; }
            set
            {
                if (_isTwoYearsAtachmentsVisible == value) return;

                _isTwoYearsAtachmentsVisible = value;
                OnPropertyChanged("IsTwoYearsAtachmentsVisible");
            }
        }
        private bool _isMonthsAtachmentsVisible = false;
        public bool IsMonthsAtachmentsVisible
        {
            get { return _isMonthsAtachmentsVisible; }
            set
            {
                if (_isMonthsAtachmentsVisible == value) return;

                _isMonthsAtachmentsVisible = value;
                OnPropertyChanged("IsMonthsAtachmentsVisible");
            }
        }
        private bool _isOthersAtachmentsVisible = false;
        public bool IsOthersAtachmentsVisible
        {
            get { return _isOthersAtachmentsVisible; }
            set
            {
                if (_isOthersAtachmentsVisible == value) return;

                _isOthersAtachmentsVisible = value;
                OnPropertyChanged("IsOthersAtachmentsVisible");
            }
        }

        public ObservableCollection<ChangeFillingPeriodModel> outletDecisionOptions { get; set; }
        public ObservableCollection<ChangeFillingPeriodModel> OutletDecisionOptions
        {
            get
            {
                return outletDecisionOptions;
            }

            set
            {
                if (outletDecisionOptions == value)
                {
                    return;
                }

                outletDecisionOptions = value;
                OnPropertyChanged("OutletDecisionOptions");
            }
        }

        public void AddAttachmentOptions()
        {
            OutletDecisionOptions = new ObservableCollection<ChangeFillingPeriodModel>();

            //foreach (var attach in EffectiveDateResponse.d.ATT_TYPSet.results)
            foreach (var attach in EffectiveDateResponse.d.ATT_TYPSet)
            {
                OutletDecisionOptions.Add(new ChangeFillingPeriodModel
                {
                    ActiveOutletDecisionOptions = attach.Txt50,
                    ActiveOutletDecisionOptionsIsSelected = false
                });
            }

        }

        private ChangeFillingPeriodModel _selectedOutletOption;
        public ChangeFillingPeriodModel SelectedOutletOption
        {
            get
            {
                return _selectedOutletOption;
            }
            set
            {
                if (_selectedOutletOption == value) return;

                _selectedOutletOption = value;
                OnPropertyChanged("SelectedOutletOption");
            }
        }

        public async Task FrequencyContinueBtnClicked()
        {
            try
            {
                if (!IsFrequencyDetailsEnabled)
                {
                    return;
                }

                if (!IsAtachmentsVisible)
                {
                    EnableDeclarationView();
                }
                else
                {
                    EnableAttachmentsView();
                }
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public void EnableDeclaration()
        {
            if (IsDeclarationViewEnabledNew && IsCheckboxChecked)
            {
                IsDeclarationEnabled = true;
            }
            else
            {
                if (ContactPersonName == "" || !IsIDVerified || !IsCheckboxChecked)
                {
                    IsDeclarationEnabled = false;
                }
                else
                {
                    IsDeclarationEnabled = true;
                }
            }
        }

        private bool _IsCheckboxChecked = false;
        public bool IsCheckboxChecked
        {
            get
            {
                return _IsCheckboxChecked;
            }
            set
            {
                _IsCheckboxChecked = value;
                OnPropertyChanged("IsCheckboxChecked");
            }
        }

        public async Task AttachmentsContinueBtnClicked()
        {
            try
            {

                if (!IsAttachmentsEnabled)
                {
                    return;
                }
                EnableDeclarationView();
            }
            catch (InternetException ex)
            {

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public async Task GoBackToDeclarationClicked()
        {
            try
            {
                EnableDeclarationView();
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }
        public async Task GoBackToDashboardClicked()
        {
            try
            {
              await  _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }
        public async Task GoBackToAttachmentsClicked()
        {
            try
            {
                EnableAttachmentsView();
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }
        public async Task GoBackToFrequencyDetailsClicked()
        {
            try
            {
                EnableFrequencyDetailsView();
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public void PopulateAttachments(List<Attachment> attachments)
        {

            var attachmentsListViewData = new ObservableCollection<Attachment>();

            foreach (Attachment attachemnt in attachments)
            {
                attachmentsListViewData.Add(attachemnt);
            }

            if (SelectedOutletOptionIndex == 0)
            {
                YearsattachmentsListViewData = attachmentsListViewData;
            }
            else if (SelectedOutletOptionIndex == 1)
            {
                MonthsattachmentsListViewData = attachmentsListViewData;
            }
            else
            {
                OtherAttachmentsListViewData = attachmentsListViewData;
            }

            AttachmentsListViewData = attachmentsListViewData;

            EnableAttachments();


        }

        public void EnableFrequencyDetails()
        {
            if ((IsDecCheckBoxVisible && !IsFrequencyDetailsChecked) || EffectiveDatePicked == "")
            {
                IsFrequencyDetailsEnabled = false;
            }
            else
            {
                IsFrequencyDetailsEnabled = true;
            }
        }
        public void EnableAttachments()
        {

            if ((IsTwoYearsAtachmentsVisible && YearsattachmentsListViewData != null && YearsattachmentsListViewData.Count != 0)
            || (IsMonthsAtachmentsVisible && MonthsattachmentsListViewData != null && MonthsattachmentsListViewData.Count != 0)
            || (IsOthersAtachmentsVisible && OtherAttachmentsListViewData != null && OtherAttachmentsListViewData.Count != 0))
            {
                IsAttachmentsEnabled = true;
            }
            else
            {
                IsAttachmentsEnabled = false;
            }


        }



        public async Task DeclarationContinueBtnClicked()
        {

            try
            {
                if (!IsDeclarationEnabled)
                {
                    return;
                }
                EnableSummaryView();
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public async Task SummaryContinueBtnClicked()
        {
            try
            {
                if (!isSubmitted)
                {

                    isSubmitted = true;

                    ChangeFillingResponse.d.Operationz = "01";

                    ChangeFillingResponse = await SubmitClicked();

                    if (ChangeFillingResponse.d1 != null)
                    {

                        ReferenceNumber = ChangeFillingResponse.d1.Fbnumz;
                        await Application.Current.MainPage.Navigation.PushAsync(new ChangeFillingPeriodSuccessPage());

                    }
                }
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public async Task<VATChangeFillingPeriodRequestModel> SubmitClicked()
        {
            VATChangeFillingPeriodRequestModel response = new VATChangeFillingPeriodRequestModel();
            VATchangeFillingPeriodPostModel request = new VATchangeFillingPeriodPostModel();

            try
            {
                IsLoading = true;

                int index1 = EffectiveDateResponse.d.EffDateSet.IndexOf(EffectiveDateResponse.d.EffDateSet.Where(p => p.Txt50 == EffectiveDatePicked).FirstOrDefault());



                request = BuildRequestObject();

                response = await VATChangeFillingWebServiceManager.GAZTPostVATChangeFillingPeriodData(request);
                await PopToRootPage();
                if (response != null && response.d1 != null)
                {
                    try
                    {
                        IsLoading = false;
                        return response;
                    }
                    catch (Exception)
                    {


                        isSubmitted = false;
                        IsLoading = false;
                        return null;
                    }
                }
                IsLoading = false;
                return response;
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                IsLoading = false;
                isSubmitted = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                return response;
            }

            catch (Exception)
            {

                isSubmitted = false;
                return response;
            }
        }


        public ObservableCollection<MyRequestsListModel> MyRequestsListViewData { get; private set; }

        public void PopulateMyRequestsListViewData()
        {
            MyRequestsListViewData = new ObservableCollection<MyRequestsListModel>();
            MyRequestsListViewData.Add(new MyRequestsListModel
            {
                Title = "VAT Filling Period",
                ReferenceNumber = "0009856456",
                Status = "In Process",
                CurrentFrequency = "Monthly",
                NewFrequency = "Quarterly",
                EffectiveDate = "Quarter 4 - 2020",
                ReleaseDate = "09th August 2020"
            });
        }
        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }
        }

        public ObservableCollection<InstalmentAgreementAttachmentsModel> FrequencyDetailsListViewData { get; private set; }

        public void PopulateFrequencyDetailsListViewData()
        {
            FrequencyDetailsListViewData = new ObservableCollection<InstalmentAgreementAttachmentsModel>();
            FrequencyDetailsListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Current Frequency",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "Monthly",
                IsAttachmentAttached = true
            });
            FrequencyDetailsListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "New Frequency",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "Quarterly",
                IsAttachmentAttached = true
            });
            FrequencyDetailsListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Effective Date",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "Quarter 4 - 2020",
                IsAttachmentAttached = true
            });

        }

        public ObservableCollection<InstalmentAgreementAttachmentsModel> ChangeFillingAttachmentsListViewData { get; private set; }

        public void PopulateChangeFillingAttachmentsListViewData()
        {
            ChangeFillingAttachmentsListViewData = new ObservableCollection<InstalmentAgreementAttachmentsModel>();
            ChangeFillingAttachmentsListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Type of Document",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "12 Months Taxable Revenue",
                IsAttachmentAttached = true
            });
            ChangeFillingAttachmentsListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Attachment",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "File1.pdf",
                IsAttachmentAttached = true
            });

        }

        public ObservableCollection<InstalmentAgreementAttachmentsModel> DeclarationListViewData { get; private set; }

        public void PopulateDeclarationListViewData()
        {
            DeclarationListViewData = new ObservableCollection<InstalmentAgreementAttachmentsModel>();
            DeclarationListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Item Type",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "National ID",
                IsAttachmentAttached = true
            });
            DeclarationListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "ID Number",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "Q17581231",
                IsAttachmentAttached = true
            });
            DeclarationListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Date of Birth",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "7 June 1995",
                IsAttachmentAttached = true
            });
            DeclarationListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Contact Person Name",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "Zaed Hardy",
                IsAttachmentAttached = true
            });
        }
        public void getYesCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesReceived", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        if (arg == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost)
                        {
                            await MopupService.Instance.PopAsync();
                            await VATSetReturnVoidAsync();
                        }
                        else if (arg == AppResources.ZZZRefundEnableMessage)
                        {
                            await MopupService.Instance.PopAsync();
                        }
                    }
                });
            }
            catch (Exception)
            {
            }
        }

        public void getNoCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "NoReceived", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        if (arg == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost)
                        {
                            await MopupService.Instance.PopAsync();
                        }
                        else if (arg == AppResources.ZZZRefundEnableMessage)
                        {
                            await MopupService.Instance.PopAsync();
                        }
                    }
                });
            }
            catch (Exception)
            {
            }
        }
        #region API Integration

        public async Task GetVATChangeFillingData()
        {
            try
            {
                IsLoading = true;
                var resultData = await VATChangeFillingWebServiceManager.GAZTGetVATChangeFillingPeriodRequestData(App.selectedVatFillingItem);
                if (resultData != null && resultData.d != null)
                {
                    ChangeFillingResponse = resultData;
                    CurrentFrequency = resultData.d.CureentF;
                    NewFrequency = resultData.d.FilingF;


                    if ((resultData.d.CureentF == "Monthly" || resultData.d.CureentF == "شهرية")
                        && (resultData.d.FilingF == "ربع سنوية" || resultData.d.FilingF == "Quarterly"))
                    {
                        IsAtachmentsVisible = true;
                        IsDecCheckBoxVisible = true;
                    }
                    else
                    {
                        IsAtachmentsVisible = false;
                        IsDecCheckBoxVisible = false;

                    }


                    await GetEffectiveDateList();
                    VatDeregDeclaration = await VatRegistrationWebServiceManager.GAZTGetVATDeRegistrationDeclaration(resultData.d.Fbnumz);
                }
                else
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                }
                IsLoading = false;
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                IsLoading = false;
                _navigationService.GoBack();
            }
            catch (GAZTVATChangeFillingPeriodException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
            catch (Exception)
            {

                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task GetEffectiveDateList()
        {
            try
            {
                IsLoading = true;
                try
                {
                    var resultData = await VATChangeFillingWebServiceManager.GAZTGetVATChangeFillingPeriodDropdownData(App.LoginDataRetrieved.TIN);
                    IsLoading = false;
                    if (resultData != null && resultData.d != null)
                    {
                        EffectiveDateResponse = resultData;
                        await ShowInstructionDialog();
                        setEffectiveDatePickerModel();
                        AddAttachmentOptions();
                        if (App.selectedVatFillingItem != "")
                        {
                            PopulateDraftData();
                            SetMoreOptioButtons();
                        }
                    }
                    else
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    }
                    
                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    IsLoading = false;
                    _navigationService.GoBack();
                }
                IsLoading = false;
            }
            catch (GAZTVATChangeFillingPeriodException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
            catch (Exception)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public async Task ValidateIdNumberFromApi(string idType)
        {
            try
            {
                IsLoading = true;
                var resultData = await VATChangeFillingWebServiceManager.GAZTVATChangeFillingPeriodValidateIDnumber(App.LoginDataRetrieved.TIN, idType, IDNumber, "", "", PickedDate.Replace("/", ""));
                if (resultData != null && resultData.d != null)
                {
                    IsIDVerified = true;
                    ContactPersonName = resultData.d.Name1 + " " + resultData.d.Name2;
                    //ContactPersonName = resultData.d.TpTitle + resultData.d.Name2 + " " + resultData.d.Name1;
                    EnableDeclaration();
                }
                else
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(resultData.errorMessage, AppResources.Information);
                }
                IsLoading = false;
            }
            catch (GAZTVATChangeFillingPeriodException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
            catch (Exception)
            {


                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }


        public VATchangeFillingPeriodPostModel BuildRequestObject()
        {

            VATchangeFillingPeriodPostModel request = new VATchangeFillingPeriodPostModel();
            request.d = new VATchangeFillingPeriodPostModel.RequestVATFillingPeriod();
            try
            {
                VATchangeFillingPeriodPostModel.Metadata _metadata = new VATchangeFillingPeriodPostModel.Metadata();

                request.d.Attchk = ChangeFillingResponse.d.Attchk;
                request.d.CPersl = ChangeFillingResponse.d.CPersl;
                request.d.Fbnumz = ChangeFillingResponse.d.Fbnumz;
                request.d.Iagrfg = "1";
                request.d.Reqfg = "1";
                request.d.Begda = ChangeFillingResponse.d.Begda;
                request.d.PortalUsrz = ChangeFillingResponse.d.PortalUsrz;
                request.d.Langz = ChangeFillingResponse.d.Langz;
                request.d.Gpart = ChangeFillingResponse.d.Gpart;

                request.d.Operationz = ChangeFillingResponse.d.Operationz;
                request.d.Fbtyp = ChangeFillingResponse.d.Fbtyp;
                if (CurrentIndex == 1 || CurrentIndex == 2)
                {
                    request.d.StepNumber = "01";
                    //request.d.StepNumberz = "01";
                }
                else
                {
                    request.d.StepNumber = "02";
                    //request.d.StepNumberz = "02";
                }
                request.d.Fbust = ChangeFillingResponse.d.Fbust;
                //request.d.ReturnIdz = ChangeFillingResponse.d.ReturnIdz;
                request.d.Officerz = ChangeFillingResponse.d.Officerz;
                request.d.UserTyp = ChangeFillingResponse.d.UserTyp;
                // request.d.Gpartz = ChangeFillingResponse.d.Gpart;
                // request.d.TransactionType = ChangeFillingResponse.d.TransactionType;
                request.d.EditFg = ChangeFillingResponse.d.EditFg;
                //request.d.Statusz = ChangeFillingResponse.d.Fbust;
                request.d.Euser = ChangeFillingResponse.d.Euser;

                request.d.Fbguid = ChangeFillingResponse.d.Fbguid;
                request.d.TxnTpz = ChangeFillingResponse.d.TransactionType;
                //request.d.DmodeFlg = ChangeFillingResponse.d.DmodeFlg;
                request.d.Formprocz = ChangeFillingResponse.d.Formprocz;
                //request.d.EvStatus = ChangeFillingResponse.d.Fbust;
                //request.d.OfficerTz = ChangeFillingResponse.d.OfficerTz;
                request.d.SrcAppz = ChangeFillingResponse.d.SrcAppz;


                request.d.Mandt = ChangeFillingResponse.d.Mandt;
                request.d.FormGuid = ChangeFillingResponse.d.FormGuid;
                request.d.DataVersion = ChangeFillingResponse.d.DataVersion;
                request.d.ReturnId = ChangeFillingResponse.d.ReturnId;
                request.d.CureentF = ChangeFillingResponse.d.CureentF;
                request.d.FilingF = ChangeFillingResponse.d.FilingF;
                request.d.Persl = ChangeFillingResponse.d.Persl;

                request.d.EffDateSet = ChangeFillingResponse.d.EffDateSet;
                request.d.UI_BTNSet = ChangeFillingResponse.d.UI_BTNSet;
                request.d.NOTESSet = ChangeFillingResponse.d.NOTESSet;
                request.d.ATTACHSet = ChangeFillingResponse.d.ATTACHSet;
                request.d.ATTACHSet.Clear();
                request.d.QuesListSet = ChangeFillingResponse.d.QuesListSet;

                if (IDType != "")
                {
                    request.d.DecidTy = IDTypeDictionary[IDType];
                    request.d.Decname = ContactPersonName;
                    request.d.DecidNo = IDNumber;
                }
                else
                {
                    request.d.DecidTy = "";
                    request.d.Decname = "";
                    request.d.DecidNo = "";
                }
                request.d.Decdesignation = "";
                if (IsCheckboxChecked)
                {
                    request.d.Decfg = "1";
                }
                else
                {
                    request.d.Decfg = "0";
                }
                request.d.TransType = "CRE_TPCV";
                request.d.UserTypz = "TP";

                if (ChangeFillingResponse.d.NOTESSet.Count != 0)
                {

                    string apiDate = ChangeFillingResponse.d.NOTESSet[0].Erfdtz;
                    if (!apiDate.Contains("Date"))
                    {

                        foreach (var item in ChangeFillingResponse.d.NOTESSet)
                        {

                            DateTime dt1 = Convert.ToDateTime(item.Erfdtz);
                            JsonSerializerSettings microsoftDateFormatSettings1 = new JsonSerializerSettings
                            {
                                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                            };
                            var jsonDateTime1 = JsonConvert.SerializeObject(dt1.Date, microsoftDateFormatSettings1);
                            string[] dateList1 = jsonDateTime1.Split('+');
                            jsonDateTime1 = Regex.Replace(dateList1[0], "[@,\\.\";'\\\\]", string.Empty);
                            jsonDateTime1 = jsonDateTime1 + ")/";

                            item.Erfdtz = jsonDateTime1;

                        }
                    }
                }

                request.d.NOTESSet = ChangeFillingResponse.d.NOTESSet.ToList();

                if (IsAttachmentsEnabled)
                {
                    var attTypeSet = new AttTypSetList();
                    attTypeSet.__metadata = new Models.ChageFillingPeriodModel.Metadata();
                    attTypeSet.__metadata.id = ZATCAConstants.VATChangeFillingPostATTTYSetURL;
                    attTypeSet.__metadata.type = "ZDP_VAT_TPCV_SRV.ATT_TYP";
                    attTypeSet.__metadata.uri = ZATCAConstants.VATChangeFillingPostATTTYSetURL;
                    attTypeSet.DmsTp = "ZTPA";
                    attTypeSet.Txt50 = AppResources.ChangeFillingPeriodAttachmentsTwoYears;

                    var attTypeSet1 = new AttTypSetList();
                    attTypeSet1.__metadata = new Models.ChageFillingPeriodModel.Metadata();
                    attTypeSet1.__metadata.id = ZATCAConstants.VATChangeFillingPostATTTYSetURL;
                    attTypeSet1.__metadata.type = "ZDP_VAT_TPCV_SRV.ATT_TYP";
                    attTypeSet1.__metadata.uri = ZATCAConstants.VATChangeFillingPostATTTYSetURL;
                    attTypeSet1.DmsTp = "ZTPB";
                    attTypeSet1.Txt50 = AppResources.ChangeFillingPeriodAttachmentsTwelveMonths;

                    var attSet = new List<AttTypSetList>();
                    attSet.Add(attTypeSet);
                    attSet.Add(attTypeSet1);

                    if (OtherAttachmentsListViewData != null)
                    {

                        var attTypeSet2 = new AttTypSetList();
                        attTypeSet2.__metadata = new Models.ChageFillingPeriodModel.Metadata();
                        attTypeSet2.__metadata.id = ZATCAConstants.VATChangeFillingPostATTTYSetURL;
                        attTypeSet2.__metadata.type = "ZDP_VAT_TPCV_SRV.ATT_TYP";
                        attTypeSet2.__metadata.uri = ZATCAConstants.VATChangeFillingPostATTTYSetURL;
                        attTypeSet2.DmsTp = "ZTPC";
                        attTypeSet2.Txt50 = AppResources.ChangeFillingPeriodAttachmentsOtherDocuments;
                        attSet.Add(attTypeSet2);
                    }
                    request.d.ATT_TYPSet = attSet;
                }
                else
                {
                    request.d.ATT_TYPSet = ChangeFillingResponse.d.ATT_TYPSet;
                }
                var todayDate = DateTime.Now.ToString();

                DateTime dt2 = Convert.ToDateTime(todayDate);
                JsonSerializerSettings microsoftDateFormatSettings2 = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var jsonDateTime2 = JsonConvert.SerializeObject(dt2, microsoftDateFormatSettings2);
                string[] dateList2 = jsonDateTime2.Split('+');
                jsonDateTime2 = dateList2[0].Replace("\"\\", "");
                jsonDateTime2 = jsonDateTime2 + ")/";
                var convretedTodayate = jsonDateTime2;
            }

            catch (Exception)
            {


            }
            return request;
        }

        #endregion

    }
}
