using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.ChageFillingPeriodModel;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using EGAZT.Views.NewDesign;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json;
using EGAZT.Views.NewDesign.ChangeFillingPeriodPages;
using System.Text.RegularExpressions;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using Xamarin.Forms.Internals;
using EGAZT.Manager;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel
{
    [Preserve(AllMembers = true)]
    public class ChangeFillingPeriodViewModel : ViewModelBase
    {

        #region Variable

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
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

        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

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
                RaisePropertyChanged("IsFrequencyDetailsChecked");
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
                RaisePropertyChanged("IsDecCheckBoxVisible");
            }
        }

        #region Commands

        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
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

        #endregion

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
        public int MaxIndex { get; private set; } = 4;

        private string _pickedDate = "";

        public string PickedDate
        {
            get { return _pickedDate; }
            set
            {
                if (_pickedDate == value) return;

                _pickedDate = value;
                RaisePropertyChanged("PickedDate");
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
                RaisePropertyChanged("ContactPersonName");
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
                RaisePropertyChanged("IDNumber");
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
                RaisePropertyChanged("IsIDVerified");
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
                RaisePropertyChanged("ContractPersonEditable");
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
                RaisePropertyChanged("IDType");
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
                RaisePropertyChanged("IsDOBVisible");
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
                RaisePropertyChanged("IsAtachmentsVisible");
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
                RaisePropertyChanged("EffectiveDatePicked");
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
                RaisePropertyChanged("IDTypePickerModel");
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
                RaisePropertyChanged("EffectiveDatePickerModel");
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
                RaisePropertyChanged("IsFrequencyDetailsEnabled");
            }
        }
        private Color _frequencyDetailsButtonBackGroundColor =  (Color)Application.Current.Resources["ButtonGray"];
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
                RaisePropertyChanged("FrequencyDetailsButtonBackGroundColor");
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
                RaisePropertyChanged("IsAttachmentsEnabled");
            }
        }
        private Color _attachButtonBackGroundColor =  (Color)Application.Current.Resources["ButtonGray"];
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
                RaisePropertyChanged("AttachButtonBackGroundColor");
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
                RaisePropertyChanged("IsDeclarationEnabled");
            }
        }
        private Color _declarationButtonBackGroundColor =  (Color)Application.Current.Resources["ButtonGray"];
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
                RaisePropertyChanged("DeclarationButtonBackGroundColor");
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
                RaisePropertyChanged("YearsattachmentsListViewData");
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
                RaisePropertyChanged("MonthsattachmentsListViewData");
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
                RaisePropertyChanged("OtherAttachmentsListViewData");
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
                RaisePropertyChanged("AttachmentsListViewData");
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
                RaisePropertyChanged("ChangeFillingResponse");
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
                RaisePropertyChanged("EffectiveDateResponse");
            }
        }

        private Dictionary<string, string> IDTypeDictionary = null;
        private Dictionary<string, string> IDValueDictionary = null;


        public ChangeFillingInterface cFInterface { get; set; }



        public ChangeFillingPeriodViewModel(INavigationService navigationService, IDialogService dialogService)
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

            ShowDatePicker = new Command(() =>
            {
                showDatePickerDialog();
            });

            EffectiveDateSpinnerClicked = new Command(() =>
            {
                showEffectiveDatePickerDialog();
            });

            IdTypeSpinnerTapped = new Command(() =>
            {
                showIdTypePickerDialog();
            });

            _dialogService = dialogService;
            GoBackClick = new Command(() =>
            {
                Backnavigations();
            });
            onMoreOptionClicked = new Command(async () =>
            {
                await PopupNavigation.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(ListOfActionButtonsApplicable));
            });

            CloseClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            FrequencyContinueBtnTapped = new Command(this.FrequencyContinueBtnClicked);
            AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            DeclarationContinueBtnTapped = new Command(this.DeclarationContinueBtnClicked);
            GoBackToFrequencyDetails = new Command(this.GoBackToFrequencyDetailsClicked);
            GoBackToAttachments = new Command(this.GoBackToAttachmentsClicked);
            GoBackToDeclaration = new Command(this.GoBackToDeclarationClicked);
            NewAttachmentTapped = new Command(this.NewAttachmentClicked);
            SummaryContinueBtnTapped = new Command(this.SummaryContinueBtnClicked);
            setIdPickerModel();
            SelectedOutletOption = new ChangeFillingPeriodModel();

            genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregDOBDatePickerTitle;
            genericDatePickerModel.PickerId = "DatePicker";
        }


        public void setMoreOptioButtons()
        {
            var listOfActionButtonsApplicable = new List<string>();
            if (App.selectedVatFillingItem != "")
            {

                if (ChangeFillingResponse != null && ChangeFillingResponse.d != null)
                {
                    if (ChangeFillingResponse.d.Statusz == "E0013")
                    {
                        listOfActionButtonsApplicable.Add(AppResources.ZZVoid);
                    }
                }
            }
            listOfActionButtonsApplicable.Add(AppResources.ZZSaveAsDraft);
            ListOfActionButtonsApplicable = listOfActionButtonsApplicable;
        }

        private List<String> _ListOfActionButtonsApplicable;
        public List<String> ListOfActionButtonsApplicable
        {
            get
            {
                return _ListOfActionButtonsApplicable;
            }
            set
            {
                _ListOfActionButtonsApplicable = value;
                RaisePropertyChanged("ListOfActionButtonsApplicable");
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
            setMoreOptioButtons();
        }


        public bool isDraftClicked = false;
        public async void OnSaveDraftClicked()
        {

            ChangeFillingResponse.d.Operationz = "05";

            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    if (!isDraftClicked)
                    {
                        isDraftClicked = true;
                        ChangeFillingResponse = await SubmitClicked();
                        if (ChangeFillingResponse != null && ChangeFillingResponse.d != null)
                        {

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                App.selectedVatFillingItem = ChangeFillingResponse.d.Fbnumz;
                                setMoreOptioButtons();

                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = string.Format(AppResources.VATFillingDraftSaved, "  " + ChangeFillingResponse.d.Fbnumz);

                                headerWithInfos.Add(headerAmountInfo);

                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                            });
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                            {
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    IsLoading = false;
                                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                });
                            }
                            else
                            {
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    IsLoading = false;
                                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                });
                            }
                        }
                    }
                });
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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

            await PopupNavigation.Instance.PushAsync(new ShowVatInformationConfirmationPageView(newDesignPopUp));
        }

        public async void VATSetReturnVoidAsync()
        {
            ChangeFillingResponse.d.Operationz = "04";

            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {

                    if (!isDraftClicked)
                    {
                        isDraftClicked = true;
                        ChangeFillingResponse = await SubmitClicked();
                        isDraftClicked = false;
                        if (ChangeFillingResponse != null && ChangeFillingResponse.d != null)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
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

                                await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                                _navigationService.GoBack();
                            });
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
                                    headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                    headerAmountInfo.IsLinkAvailable = false;
                                    headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                                    headerWithInfos.Add(headerAmountInfo);

                                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                    await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
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
                                    headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                    headerAmountInfo.IsLinkAvailable = false;
                                    headerAmountInfo.Message = WebServiceManager.ErrorMessageForVAT;
                                    headerWithInfos.Add(headerAmountInfo);
                                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                    await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                    WebServiceManager.ErrorMessageForVAT = string.Empty;
                                });
                            }
                        }
                    }
                });
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public void PopulateDraftData()
        {
            foreach (var att in EffectiveDateResponse.d.EffDateSet.results)
            {
                if (att.Persl == ChangeFillingResponse.d.Persl)
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
            foreach (var attach in ChangeFillingResponse.d.ATTACHSet.results)
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


        public async void ValidateIdNumber()
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
                            //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));

                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));

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
                                //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));

                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));

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

                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));

                            //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
                                //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));

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
                            //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));

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
                            //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));

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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public async void showInstructionDialog()
        {
            if (App.selectedVatFillingItem != "")
            {
                if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018")
                {
                    await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(
                instructionString: AppResources.VFCInstructions, checkBoxString: AppResources.VFCCheckBoxDesc,
                continueString: AppResources.CRContinue,
                isEditable: true,
                _dialogType: InstructionsBottomPopUpViewModel.DialogType
                    .Instructions));
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(
                                    instructionString: AppResources.VFCInstructions, checkBoxString: AppResources.VFCCheckBoxDesc,
                                    continueString: AppResources.CRContinue,
                                    _dialogType: InstructionsBottomPopUpViewModel.DialogType
                                        .Instructions));
                }
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(
                                   instructionString: AppResources.VFCInstructions, checkBoxString: AppResources.VFCCheckBoxDesc,
                                   continueString: AppResources.CRContinue,
                                   _dialogType: InstructionsBottomPopUpViewModel.DialogType
                                       .Instructions));
            }
        }
        private async void showDatePickerDialog()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
            }
            catch (GAZTUnlockAccountException )
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private async void showEffectiveDatePickerDialog()
        {
            try
            {
                selectedPicker = PickerEnum.EffectiveDate;
                await PopupNavigation.Instance.PushAsync(new PickerPageView(EffectiveDatePickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private async void showIdTypePickerDialog()
        {
            try
            {
                selectedPicker = PickerEnum.IdType;
                await PopupNavigation.Instance.PushAsync(new PickerPageView(IDTypePickerModel));
            }
            catch (GAZTUnlockAccountException)
            {
            }
            catch (InternetException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public void updateEffectiveDatePicker()
        {
            EffectiveDatePicked = EffectiveDatePickerModel.SelectedValue;
            EnableFrequencyDetails();
        }

        public void updateIdTypePicker()
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

            ValidateIdNumber();
        }
        private void setEffectiveDatePickerModel()
        {
            var list = new List<string>();

            foreach (var date in EffectiveDateResponse.d.EffDateSet.results)
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

        public async void NewAttachmentClicked()
        {

            try
            {
                if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0) return;
                if (SelectedOutletOptionIndex == 0)
                {
                    if (YearsattachmentsListViewData == null)
                    {
                        YearsattachmentsListViewData = new ObservableCollection<Attachment>();
                    }
                    await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                 YearsattachmentsListViewData.ToList(),
                 Models.ZakatInstalationModels.WhichAttachment.ChangeFillingPeriod2Years, ChangeFillingResponse.d.ReturnIdz));
                }
                else if (SelectedOutletOptionIndex == 1)
                {
                    if (MonthsattachmentsListViewData == null)
                    {
                        MonthsattachmentsListViewData = new ObservableCollection<Attachment>();
                    }
                    await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                 MonthsattachmentsListViewData.ToList(),
                 Models.ZakatInstalationModels.WhichAttachment.ChangeFillingPeriod12Months, ChangeFillingResponse.d.ReturnIdz));
                }
                else
                {
                    if (OtherAttachmentsListViewData == null)
                    {
                        OtherAttachmentsListViewData = new ObservableCollection<Attachment>();
                    }
                    await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                 OtherAttachmentsListViewData.ToList(),
                 Models.ZakatInstalationModels.WhichAttachment.ChangeFillingPeriodOtherDoc, ChangeFillingResponse.d.ReturnIdz));
                }
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EnableFrequencyDetailsView()
        {
            CurrentIndex = 1;
            IsFrequencyViewEnabled = true;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
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
                RaisePropertyChanged("IsBackVisible");
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
                RaisePropertyChanged("IsFrequencyViewEnabled");
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
                RaisePropertyChanged("IsAttachmentsViewEnabled");
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
                RaisePropertyChanged("IsDeclarationViewEnabled");
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
                RaisePropertyChanged("IsSummaryViewEnabled");
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
                RaisePropertyChanged("ShowAttachments");
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

                RaisePropertyChanged("SelectedOutletOptionIndex");
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
                RaisePropertyChanged("SelectedAttachmentText");
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
                RaisePropertyChanged("ReferenceNumber");
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
                RaisePropertyChanged("CurrentFrequency");
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
                RaisePropertyChanged("NewFrequency");
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
                RaisePropertyChanged("IsTwoYearsAtachmentsVisible");
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
                RaisePropertyChanged("IsMonthsAtachmentsVisible");
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
                RaisePropertyChanged("IsOthersAtachmentsVisible");
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
                RaisePropertyChanged("OutletDecisionOptions");
            }
        }

        public void AddAttachmentOptions()
        {
            OutletDecisionOptions = new ObservableCollection<ChangeFillingPeriodModel>();

            foreach (var attach in EffectiveDateResponse.d.ATT_TYPSet.results)
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
                RaisePropertyChanged("SelectedOutletOption");
            }
        }

        public void FrequencyContinueBtnClicked()
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
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public void EnableDeclaration()
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
                RaisePropertyChanged("IsCheckboxChecked");
            }
        }

        public void AttachmentsContinueBtnClicked()
        {
            try
            {

                if (!IsAttachmentsEnabled)
                {
                    return;
                }
                EnableDeclarationView();
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public void GoBackToDeclarationClicked()
        {
            try
            {
                EnableDeclarationView();
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public void GoBackToDashboardClicked()
        {
            try
            {
                _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public void GoBackToAttachmentsClicked()
        {
            try
            {
                EnableAttachmentsView();
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public void GoBackToFrequencyDetailsClicked()
        {
            try
            {
                EnableFrequencyDetailsView();
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
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
            
                if ((IsTwoYearsAtachmentsVisible&&YearsattachmentsListViewData != null&&YearsattachmentsListViewData.Count != 0 )
                || (IsMonthsAtachmentsVisible&&MonthsattachmentsListViewData != null&&MonthsattachmentsListViewData.Count != 0)
                || (IsOthersAtachmentsVisible && OtherAttachmentsListViewData != null&& OtherAttachmentsListViewData.Count != 0))
                {
                    IsAttachmentsEnabled = true;
                }
                else
                {
                    IsAttachmentsEnabled = false;
                }
            
            
        }



        public void DeclarationContinueBtnClicked()
        {

            try
            {
                if (!IsDeclarationEnabled)
                {
                    return;
                }
                EnableSummaryView();
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void SummaryContinueBtnClicked()
        {
            try
            {
                if (!isSubmitted)
                {

                    isSubmitted = true;

                    ChangeFillingResponse.d.Operationz = "01";

                    ChangeFillingResponse = await SubmitClicked();

                    if (ChangeFillingResponse.d != null)
                    {

                        ReferenceNumber = ChangeFillingResponse.d.Fbnumz;
                        await Application.Current.MainPage.Navigation.PushAsync(new ChangeFillingPeriodSuccessPage());

                    }
                }
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task<VATChangeFillingPeriodRequestModel> SubmitClicked()
        {
            VATChangeFillingPeriodRequestModel response = new VATChangeFillingPeriodRequestModel();
            VATchangeFillingPeriodPostModel request = new VATchangeFillingPeriodPostModel();

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                int index1 = EffectiveDateResponse.d.EffDateSet.results.IndexOf(EffectiveDateResponse.d.EffDateSet.results.Where(p => p.Txt50 == EffectiveDatePicked).FirstOrDefault());

                if (index1 != -1)
                {
                    ChangeFillingResponse.d.Persl = EffectiveDateResponse.d.EffDateSet.results[index1].Persl;

                }
                //foreach (var date in EffectiveDateResponse.d.EffDateSet.results)
                //{
                //    int index = EffectiveDateResponse.d.EffDateSet.results.ToList().FindIndex(item => date.Txt50 == EffectiveDatePicked);


                //    if (index != -1)
                //    {
                //        ChangeFillingResponse.d.Persl = EffectiveDateResponse.d.EffDateSet.results[index].Persl;

                //    }
                //}


                request = BuildRequestObject();

                response = await VATChangeFillingWebServiceManager.GAZTPostVATChangeFillingPeriodData(request);
                PopToRootPage();
                if (response != null && response.d != null)
                {
                    try
                    {
                        IsLoading = false;
                        return response;
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    isSubmitted = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
                return response;
            }

            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
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

        #region API Integration

        public async Task GetVATChangeFillingData()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    try
                    {
                        var resultData = await VATChangeFillingWebServiceManager.GAZTGetVATChangeFillingPeriodRequestData(App.selectedVatFillingItem);
                        if (resultData != null && resultData.d != null)
                        {
                            ChangeFillingResponse = resultData;
                            CurrentFrequency = resultData.d.CureentF;
                            NewFrequency = resultData.d.FilingF;

                            if (resultData.d.Attchk == "Q")
                            {
                                IsAtachmentsVisible = true;
                                IsDecCheckBoxVisible = true;
                            }
                            else
                            {
                                IsAtachmentsVisible = false;
                                IsDecCheckBoxVisible = false;
                            }


                            if (resultData.d.Attchk == "M")
                            {
                                IsAtachmentsVisible = false;
                                IsDecCheckBoxVisible = false;
                            }
                            else
                            {
                                IsAtachmentsVisible = true;
                                IsDecCheckBoxVisible = true;
                            }
                            await GetEffectiveDateList();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        IsLoading = false;
                    }
                    catch (GAZTVATChangeFillingPeriodException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTVATChangeFillingPeriodException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
        }

        public async Task GetEffectiveDateList()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    try
                    {
                        var resultData = await VATChangeFillingWebServiceManager.GAZTGetVATChangeFillingPeriodDropdownData(App.LoginDataRetrieved.TIN);
                        if (resultData != null && resultData.d != null)
                        {
                            EffectiveDateResponse = resultData;
                            showInstructionDialog();
                            setEffectiveDatePickerModel();
                            AddAttachmentOptions();
                            if (App.selectedVatFillingItem != "")
                            {
                                PopulateDraftData();
                                setMoreOptioButtons();
                            }
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        IsLoading = false;
                    }
                    catch (GAZTVATChangeFillingPeriodException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTVATChangeFillingPeriodException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
        }

        public async Task ValidateIdNumberFromApi(string idType)
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    try
                    {
                        var resultData = await VATChangeFillingWebServiceManager.GAZTVATChangeFillingPeriodValidateIDnumber(App.LoginDataRetrieved.TIN, idType, IDNumber, "", "", PickedDate.Replace("/", ""));
                        if (resultData != null && resultData.d != null)
                        {
                            IsIDVerified = true;
                            ContactPersonName = resultData.d.Name1 + " " + resultData.d.Name2;
                            EnableDeclaration();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                IsLoading = false;
                                await _dialogService.ShowMessage(resultData.errorMessage, AppResources.Information);
                            });
                        }
                        IsLoading = false;
                    }
                    catch (GAZTVATChangeFillingPeriodException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTVATChangeFillingPeriodException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
        }


        public VATchangeFillingPeriodPostModel BuildRequestObject()
        {

            VATchangeFillingPeriodPostModel request = new VATchangeFillingPeriodPostModel();
            request.d = new VATchangeFillingPeriodPostModel.RequestVATFillingPeriod();
            try
            {
                VATchangeFillingPeriodPostModel.Metadata _metadata = new VATchangeFillingPeriodPostModel.Metadata();
                _metadata.id = ChangeFillingResponse.d.__metadata.id;
                _metadata.uri = ChangeFillingResponse.d.__metadata.uri;
                _metadata.type = ChangeFillingResponse.d.__metadata.type;

                request.d.__metadata = _metadata;
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
                    request.d.StepNumberz = "01";
                }
                else
                {
                    request.d.StepNumber = "02";
                    request.d.StepNumberz = "02";
                }
                request.d.Fbust = ChangeFillingResponse.d.Fbust;
                request.d.ReturnIdz = ChangeFillingResponse.d.ReturnIdz;
                request.d.Officerz = ChangeFillingResponse.d.Officerz;
                request.d.UserTyp = ChangeFillingResponse.d.UserTyp;
                request.d.Gpartz = ChangeFillingResponse.d.Gpartz;
                request.d.TransactionType = ChangeFillingResponse.d.TransactionType;
                request.d.EditFg = ChangeFillingResponse.d.EditFg;
                request.d.Statusz = ChangeFillingResponse.d.Statusz;
                request.d.Euser = ChangeFillingResponse.d.Euser;

                request.d.Fbguid = ChangeFillingResponse.d.Fbguid;
                request.d.TxnTpz = ChangeFillingResponse.d.TxnTpz;
                request.d.DmodeFlg = ChangeFillingResponse.d.DmodeFlg;
                request.d.Formprocz = ChangeFillingResponse.d.Formprocz;
                request.d.EvStatus = ChangeFillingResponse.d.EvStatus;
                request.d.OfficerTz = ChangeFillingResponse.d.OfficerTz;
                request.d.SrcAppz = ChangeFillingResponse.d.SrcAppz;


                request.d.Mandt = ChangeFillingResponse.d.Mandt;
                request.d.FormGuid = ChangeFillingResponse.d.FormGuid;
                request.d.DataVersion = ChangeFillingResponse.d.DataVersion;
                request.d.ReturnId = ChangeFillingResponse.d.ReturnId;
                request.d.CureentF = ChangeFillingResponse.d.CureentF;
                request.d.FilingF = ChangeFillingResponse.d.FilingF;
                request.d.Persl = ChangeFillingResponse.d.Persl;

                request.d.EffDateSet = ChangeFillingResponse.d.EffDateSet.results;
                request.d.UI_BTNSet = ChangeFillingResponse.d.UI_BTNSet;
                request.d.NOTESSet = ChangeFillingResponse.d.NOTESSet.results;
                request.d.ATTACHSet = ChangeFillingResponse.d.ATTACHSet.results;
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

                if (ChangeFillingResponse.d.NOTESSet.results.Count != 0)
                {

                    string apiDate = ChangeFillingResponse.d.NOTESSet.results[0].Erfdtz;
                    if (!apiDate.Contains("Date"))
                    {

                        foreach (var item in ChangeFillingResponse.d.NOTESSet.results)
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

                request.d.NOTESSet = ChangeFillingResponse.d.NOTESSet.results.ToList();

                if (IsAttachmentsEnabled)
                {
                    var attTypeSet = new AttTypSetList();
                    attTypeSet.__metadata = new Models.ChageFillingPeriodModel.Metadata();
                    attTypeSet.__metadata.id = Constants.VATChangeFillingPostATTTYSetURL;
                    attTypeSet.__metadata.type = "ZDP_VAT_TPCV_SRV.ATT_TYP";
                    attTypeSet.__metadata.uri = Constants.VATChangeFillingPostATTTYSetURL;
                    attTypeSet.DmsTp = "ZTPA";
                    attTypeSet.Txt50 = AppResources.ChangeFillingPeriodAttachmentsTwoYears;

                    var attTypeSet1 = new AttTypSetList();
                    attTypeSet1.__metadata = new Models.ChageFillingPeriodModel.Metadata();
                    attTypeSet1.__metadata.id = Constants.VATChangeFillingPostATTTYSetURL;
                    attTypeSet1.__metadata.type = "ZDP_VAT_TPCV_SRV.ATT_TYP";
                    attTypeSet1.__metadata.uri = Constants.VATChangeFillingPostATTTYSetURL;
                    attTypeSet1.DmsTp = "ZTPB";
                    attTypeSet1.Txt50 = AppResources.ChangeFillingPeriodAttachmentsTwelveMonths;

                    var attSet = new List<AttTypSetList>();
                    attSet.Add(attTypeSet);
                    attSet.Add(attTypeSet1);

                    if (OtherAttachmentsListViewData != null)
                    {

                        var attTypeSet2 = new AttTypSetList();
                        attTypeSet2.__metadata = new Models.ChageFillingPeriodModel.Metadata();
                        attTypeSet2.__metadata.id = Constants.VATChangeFillingPostATTTYSetURL;
                        attTypeSet2.__metadata.type = "ZDP_VAT_TPCV_SRV.ATT_TYP";
                        attTypeSet2.__metadata.uri = Constants.VATChangeFillingPostATTTYSetURL;
                        attTypeSet2.DmsTp = "ZTPC";
                        attTypeSet2.Txt50 = AppResources.ChangeFillingPeriodAttachmentsOtherDocuments;
                        attSet.Add(attTypeSet2);
                    }
                    request.d.ATT_TYPSet = attSet;
                }
                else
                {
                    request.d.ATT_TYPSet = ChangeFillingResponse.d.ATT_TYPSet.results;
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

            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return request;
        }

        #endregion

    }
}
