using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.GenericPickers;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Plugin.FilePicker;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    public class ActivityItemPageViewModel : BaseViewModel
    {
        #region variables

        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        private ActivitySetsList activityList = null;
        public OutletNumber newNumber { get; set; } = null;
        public ValidateCR validateCR { get; set; } = null;
        public List<Nreg_ActivityItem> NregActivityList = new List<Nreg_ActivityItem>();
        //public Nreg_ActivityItem cRActivityItem { get; set; } = null;
        public ActicityListDelegate goBackAction = null;
        private EstablishmentOutletActivitiesTabsEnum _currentTab = EstablishmentOutletActivitiesTabsEnum.CRDetails;
        public EstablishmentOutletActivitiesTabsEnum CurrentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(CurrentTab));
                switch (value)
                {
                    case EstablishmentOutletActivitiesTabsEnum.LicenseDetails:
                        ActivityTitle = AppResources.ESTAddLicense;
                        break;
                    case EstablishmentOutletActivitiesTabsEnum.ActivityList:
                        ActivityTitle = AppResources.ESTLicenseDetails; 
                        break;
                    case EstablishmentOutletActivitiesTabsEnum.CRDetails:
                    default:
                        ActivityTitle = AppResources.ESTCommercialRegistration; 
                        break;
                }
                fetchTabDataAndBind();
            }
        }
        private string _activityTitle = AppResources.ESTCommercialRegistration;
        public string ActivityTitle
        {
            get => _activityTitle;
            private set
            {
                _activityTitle = value;
                RaisePropertyChanged(nameof(ActivityTitle));
            }
        }

        private Dictionary<string, string> EnIssueBy = new Dictionary<string, string>()
        {
            {"90701", "STC" },
            {"90702", "Ministry of Commerce and Industry" },
            {"90703", "Ministry of Health" },
            {"90704", "Ministry of Culture and Information" },
            {"90705", "Ministry of Agriculture" },
            {"90706", "Ministry of Municipal and Rural Affairs" },
            {"90707", "Ministry of Education" },
            {"90708", "Technical and Vocational Training Corporation" },
            {"90709", "Ministry of Labor" },
            {"90710", "Ministry of Islamic Affairs, Endowments, Da`wah, and Guidance" },
            {"90711", "Ministry of Hajj" },
            {"90712", "Saudi Arabia General Investment Authority" },
            {"90713", "Ministry of Water and Electricity" },
            {"90714", "Saudi Arabian Monetary Agency" },
            {"90715", "General Authority of Civil Aviation" },
            {"90716", "Ministry of Interior" },
            {"90717", "Ministry of Transportation" },
            {"90719", "Same Government Agency" },
            {"90720", "Ministry of Social Affairs" },
            {"90722", "Saudi Organization for Certified public Accountants? SOCPA" },
            {"90723", "Saudi Organization Tourism & National Heritage" },
            {"90725", "Ministry Of Justice" },
            {"90729", "Saudi Council of Engineers" },
            {"90721", "Municipality" },
            {"90724", "Ministry of Petroleum and Mineral Resources" },
            {"90718", "Other" }
        };
        private Dictionary<string, string> ArIssueBy = new Dictionary<string, string>()
        {
            {"90701", "شركة الاتصالات السعوديه" },
            {"90702", "وزارة التجارة والصناعة" },
            {"90703", "وزارة الصحة" },
            {"90704", "وزارة الثقافه والاعلام" },
            {"90705", "وزارة الزراعة" },
            {"90706", "وزارة الشؤون البلدية والقروية" },
            {"90707", "وزارة التربية والتعليم" },
            {"90708", "التعليم الفني والتدريب المهني" },
            {"90709", "وزارة العمل" },
            {"90710", "وزارة الشؤون الإسلامية والأوقاف والدعوة والإرشاد" },
            {"90711", "وزارة الحـج" },
            {"90712", "الهيئة العامه للاستثمار" },
            {"90713", "وزارة المياه والكهرباء" },
            {"90714", "مؤسسة النقد العربي السعودي" },
            {"90715", "الهيئة العامة للطيران المدني" },
            {"90716", "وزارة الداخلية" },
            {"90717", "وزارة النقل" },
            {"90719", "نفس الجهة الحكومية" },
            {"90720", "وزارة الشؤون الإجتماعية" },
            {"90722", "الهيئة السعودية للمحاسبين القانونيين" },
            {"90723", "الهيئة العامة للسياحة والتراث الوطني" },
            {"90725", "لدية العمار" },
            {"90729", "وزارة العدل" },
            {"90721", "الهيئة السعودية للمهندسين" },
            {"90724", "وزارة البترول والثروة المعدنية" },
            {"90718", "غير معرف" }
        };
        private bool CanExecuteClickCommand(object args) => EnableInputFields;
        private bool CanIssueByExecuteClickCommand(object args) => EnableIssueByDropDown;

        private CountryDropdownItem _cRIssueCountry = null;
        public CountryDropdownItem CRIssueCountry
        {
            get => _cRIssueCountry;
            set
            {
                //if (value != null)
                //{
                _cRIssueCountry = value;
                RaisePropertyChanged(nameof(CRIssueCountry));
                //}
            }
        }
        private string _cRIssueBy = null;
        public string CRIssueBy
        {
            get => _cRIssueBy;
            set
            {
                //if (value != null)
                //{
                _cRIssueBy = value;
                RaisePropertyChanged(nameof(CRIssueBy));
                //}
            }
        }
        private CityDropdownItem _cRIssueCity = null;
        public CityDropdownItem CRIssueCity
        {
            get => _cRIssueCity;
            set
            {
                //if (value != null)
                //{
                _cRIssueCity = value;
                RaisePropertyChanged(nameof(CRIssueCity));
                //}
            }
        }
        private string _cRNumber = string.Empty;
        public string CRNumber
        {
            get => _cRNumber;
            set
            {
                _cRNumber = value;
                RaisePropertyChanged(nameof(CRNumber));
            }
        }
        private bool _enableInputFields = true;
        public bool EnableInputFields
        {
            get => _enableInputFields;
            set
            {
                _enableInputFields = value;
                OnIssueCountrySelectButtonClick.ChangeCanExecute();
                OnIssueCitySelectButtonClick.ChangeCanExecute();
                OnValidFromButtonClick.ChangeCanExecute();
                RaisePropertyChanged(nameof(EnableInputFields));
            }
        }
        private bool _enableIssueByDropDown = true;
        public bool EnableIssueByDropDown
        {
            get => _enableIssueByDropDown;
            set
            {
                _enableIssueByDropDown = value;
                OnIssueBySelectButtonClick.ChangeCanExecute();
                RaisePropertyChanged(nameof(EnableIssueByDropDown));
            }
        }
        private bool _enableCRInputField = true;
        public bool EnableCRInputField
        {
            get => _enableCRInputField;
            private set
            {
                _enableCRInputField = value;
                RaisePropertyChanged(nameof(EnableCRInputField));
            }
        }
        private OutletDropDowns _outletDropDowns = null;
        public OutletDropDowns OutletDropDowns
        {
            get => _outletDropDowns;
            set
            {
                //if (value != null)
                //{
                _outletDropDowns = value;
                RaisePropertyChanged(nameof(OutletDropDowns));
                //}
            }
        }
        private string _cRValidFrom = string.Empty;
        public string CRValidFrom
        {
            get => _cRValidFrom;
            set
            {
                _cRValidFrom = value;
                RaisePropertyChanged(nameof(CRValidFrom));
            }
        }
        public int _attachmentCount = 0;
        public int AttachmentCount
        {
            get
            {
                return _attachmentCount;
            }
            set
            {
                _attachmentCount = value;
                RaisePropertyChanged(nameof(AttachmentCount));
            }
        }
        private bool _mainActivity = false;
        public bool MainActivity
        {
            get => _mainActivity;
            set
            {
                _mainActivity = value;
                RaisePropertyChanged(nameof(MainActivity));
            }
        }
        private ActivityGroupSubGroup _cRMainGroup = null;
        public ActivityGroupSubGroup CRMainGroup
        {
            get => _cRMainGroup;
            set
            {
                if (value != null)
                {
                    _cRMainGroup = value;
                    RaisePropertyChanged(nameof(CRMainGroup));
                }
            }
        }
        private ActivityGroupSubGroup _cRSubGroup = null;
        public ActivityGroupSubGroup CRSubGroup
        {
            get => _cRSubGroup;
            set
            {
                //if (value != null)
                //{
                _cRSubGroup = value;
                RaisePropertyChanged(nameof(CRSubGroup));
                //}
            }
        }
        private ActivityGroupSubGroup _cRAcitivity;
        public ActivityGroupSubGroup CRAcitivity
        {
            get => _cRAcitivity;
            set
            {
                //if (value != null)
                //{
                _cRAcitivity = value;
                RaisePropertyChanged(nameof(CRAcitivity));
                //}
            }
        }
        private ObservableCollection<Attachment> _cRsCopies = new ObservableCollection<Attachment>();
        public ObservableCollection<Attachment> CRsCopies
        {
            get => _cRsCopies;
            set
            {
                if (value != null)
                {
                    _cRsCopies = value;
                    RaisePropertyChanged(nameof(CRsCopies));
                }
            }
        }
        private ObservableCollection<Attachment> _transferCRsCopies = new ObservableCollection<Attachment>();
        public ObservableCollection<Attachment> TransferCRsCopies
        {
            get => _transferCRsCopies;
            set
            {
                if (value != null)
                {
                    _transferCRsCopies = value;
                    RaisePropertyChanged(nameof(TransferCRsCopies));
                }
            }
        }

        private string _validFrom = string.Empty;
        public string ValidFrom
        {
            get => _validFrom;
            set
            {
                _validFrom = value;
                RaisePropertyChanged(nameof(ValidFrom));
            }
        }
        private CountryDropdownItem _licenseIssueCountry = null;
        public CountryDropdownItem LicenseIssueCountry
        {
            get => _licenseIssueCountry;
            set
            {
                //if (value != null)
                //{
                _licenseIssueCountry = value;
                RaisePropertyChanged(nameof(LicenseIssueCountry));
                //}
            }
        }
        private string _licenseIssueBy = null;
        public string LicenseIssueBy
        {
            get => _licenseIssueBy;
            set
            {
                //if (value != null)
                //{
                _licenseIssueBy = value;
                RaisePropertyChanged(nameof(LicenseIssueBy));
                //}
            }
        }
        private CityDropdownItem _licenseIssueCity = null;
        public CityDropdownItem LicenseIssueCity
        {
            get => _licenseIssueCity;
            set
            {
                //if (value != null)
                //{
                _licenseIssueCity = value;
                RaisePropertyChanged(nameof(LicenseIssueCity));
                //}
            }
        }
        private string _licenseNumber = string.Empty;
        public string LicenseNumber
        {
            get => _licenseNumber;
            set
            {
                _licenseNumber = value;
                RaisePropertyChanged(nameof(LicenseNumber));
            }
        }
        private ActivityGroupSubGroup _licenseMainGroup = null;
        public ActivityGroupSubGroup LicenseMainGroup
        {
            get => _licenseMainGroup;
            set
            {
                //if (value != null)
                //{
                _licenseMainGroup = value;
                RaisePropertyChanged(nameof(LicenseMainGroup));
                //}
            }
        }
        private ActivityGroupSubGroup _licenseSubGroup = null;
        public ActivityGroupSubGroup LicenseSubGroup
        {
            get => _licenseSubGroup;
            set
            {
                //if (value != null)
                //{
                _licenseSubGroup = value;
                RaisePropertyChanged(nameof(LicenseSubGroup));
                //}
            }
        }
        private ActivityGroupSubGroup _licenseAcitivity;
        public ActivityGroupSubGroup LicenseAcitivity
        {
            get => _licenseAcitivity;
            set
            {
                //if (value != null)
                //{
                _licenseAcitivity = value;
                RaisePropertyChanged(nameof(LicenseAcitivity));
                //}
            }
        }
        private ObservableCollection<Attachment> _licensesCopies = new ObservableCollection<Attachment>();
        public ObservableCollection<Attachment> LicensesCopies
        {
            get => _licensesCopies;
            set
            {
                //if (value != null)
                //{
                _licensesCopies = value;
                RaisePropertyChanged(nameof(LicensesCopies));
                //}
            }
        }
        private List<Nreg_ActivityItem> _licenseData = new List<Nreg_ActivityItem>();
        public List<Nreg_ActivityItem> LicenseData
        {
            get => _licenseData;
            private set
            {
                _licenseData = value;
                RaisePropertyChanged(nameof(LicenseData));
            }
        }
        #endregion

        #region commands
        public ICommand OnNextButtonClick { get; private set; }
        public ICommand OnPreButtonClick { get; private set; }
        public Command OnIssueCountrySelectButtonClick { get; set; }
        public Command OnIssueBySelectButtonClick { get; set; }
        public Command OnIssueCitySelectButtonClick { get; set; }
        public Command OnValidFromButtonClick { get; set; }
        public ICommand OnTransferCopyOfCRChoiceButtonClick { get; set; }
        public ICommand OnMainGroupSelectButtonClick { get; set; }
        public ICommand OnSubGroupSelectButtonClick { get; set; }
        public ICommand OnAcitivitySelectButtonClick { get; set; }
        public ICommand OnNewLicenseButtonClick { get; private set; }

        public ICommand OnTransferCopyOfLicenseChoiceButtonClick { get; set; }
        public ICommand OnDeleteCRsCopyButtonClick { get; set; }
        public ICommand OnDeleteTransferCRsCopyButtonClick { get; set; }
        public ICommand OnDeleteLicenseCopyButtonClick { get; set; }
        #endregion

        #region Constructor
        public ActivityItemPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnNextButtonClick = new Command(() => navigateToNext());
            OnPreButtonClick = new Command(() => navigationService.GoBack());
            OnNewLicenseButtonClick = new Command(() => CurrentTab = EstablishmentOutletActivitiesTabsEnum.LicenseDetails);
            OnIssueCountrySelectButtonClick = new Command((object o) =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(OutletDropDowns?.country_dropdownSet?.results);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                            CRIssueCountry = item as CountryDropdownItem;
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                            LicenseIssueCountry = item as CountryDropdownItem;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            }, CanExecuteClickCommand);
            OnIssueBySelectButtonClick = new Command((object o) =>
            {

                ListPopUpViewPage poupWindow = new ListPopUpViewPage(EnIssueBy.Values);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                            CRIssueBy = item as string;
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                            LicenseIssueBy = item as string;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            }, CanIssueByExecuteClickCommand);
            OnIssueCitySelectButtonClick = new Command((object o) =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(OutletDropDowns?.city_dropdownSet?.results.Where(i =>
                {
                    if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                    {
                        return i.Country == CRIssueCountry.Land1;
                    }
                    else
                    {
                        return i.Country == LicenseIssueCountry.Land1;
                    }
                }).ToList());

                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                            CRIssueCity = item as CityDropdownItem;
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                            LicenseIssueCity = item as CityDropdownItem;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            }, CanExecuteClickCommand);
            OnValidFromButtonClick = new Command((object o) =>
            {
                PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(new GenericDatePickerModel()
                {
                    DatePickerTitle = "Valid From",
                    PickerId = (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails) ? "LicenseValidFromId" : "CRValidFromId"
                }));
            }, CanExecuteClickCommand);
            OnTransferCopyOfCRChoiceButtonClick = new Command(async (type) =>
            {
                Console.WriteLine("OnTransferCopyOfCRChoiceButtonClick");
                await AddAttachment(type as string);
            });
            OnTransferCopyOfLicenseChoiceButtonClick = new Command(async (type) => await AddAttachment("RG02"));

            OnMainGroupSelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(activityList?.act_groupSet?.results);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                        {
                            CRMainGroup = item as ActivityGroupSubGroup;
                            CRSubGroup = null;
                            CRAcitivity = null;
                        }
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                        {
                            LicenseMainGroup = item as ActivityGroupSubGroup;
                            LicenseSubGroup = null;
                            LicenseAcitivity = null;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                    finally
                    {
                        updateActivityList((item as ActivityGroupSubGroup).IndSector);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            OnSubGroupSelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(activityList?.act_subgroupSet?.results);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                        {
                            CRSubGroup = item as ActivityGroupSubGroup;
                            CRAcitivity = null;
                        }
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                        {
                            LicenseSubGroup = item as ActivityGroupSubGroup;
                            LicenseAcitivity = null;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                    finally
                    {
                        updateActivityList((item as ActivityGroupSubGroup).IndSector);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            OnAcitivitySelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(activityList?.activitySet?.results);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                            CRAcitivity = item as ActivityGroupSubGroup;
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                            LicenseAcitivity = item as ActivityGroupSubGroup;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                    finally
                    {
                        updateActivityList((item as ActivityGroupSubGroup).IndSector);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });


            OnDeleteCRsCopyButtonClick = new Command((item) => OnDeleteAttachment(item as Attachment, "RG01"));
            OnDeleteTransferCRsCopyButtonClick = new Command((item) => OnDeleteAttachment(item as Attachment, "RG12"));
            OnDeleteLicenseCopyButtonClick =  new Command((item) => OnDeleteAttachment(item as Attachment, "RG02"));
        }

        #endregion

        #region Method
        private void navigateToNext()
        {
            try
            {
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails || CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                {
                    DateTime.TryParseExact(CRValidFrom, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime crIssueDate);
                    DateTime.TryParseExact(ValidFrom, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime issueDate);
                    DateTime.TryParseExact("9999/12/31", "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime maxDate);
                    Nreg_ActivityItem item = new Nreg_ActivityItem
                    {
                        Type = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? "BUP002" : "ZS0004",
                        ValidDateFrom = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? crIssueDate : issueDate,
                        ValidDateTo = maxDate,
                        Idnumber = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRNumber : LicenseNumber,
                        Country = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRIssueCountry.Land1 : LicenseIssueCountry.Land1,
                        Institute = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? EnIssueBy.FirstOrDefault(i => i.Value == CRIssueBy).Key : EnIssueBy.FirstOrDefault(i => i.Value == LicenseIssueBy).Key,
                        City = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRIssueCity.CityName : LicenseIssueCity.CityName,
                        CityCode = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRIssueCity.CityCode : LicenseIssueCity.CityCode,
                        Activity = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRAcitivity.IndSector : LicenseAcitivity.IndSector,
                        ActMgrp = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRMainGroup.IndSector : LicenseMainGroup.IndSector,
                        ActSgrp = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRSubGroup.IndSector : LicenseSubGroup.IndSector,
                        Actcat = MainActivity ? "M" : "S",
                        Actno = $"{Int16.Parse(newNumber?.Actno):000}",
                        Crattfg = CRsCopies.Count > 0 ? "X" : string.Empty
                    };

                    NregActivityList.Add(item);
                    CurrentTab = EstablishmentOutletActivitiesTabsEnum.ActivityList;
                }
                else if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.ActivityList)
                {
                    goBackAction?.Invoke(NregActivityList);
                    _navigationService.GoBack();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void OnAppearing()
        {
            //fetchTabDataAndBind();
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (sender, arg) =>
            {
                Console.WriteLine(string.Format("Subscribe {0}, {1}", arg.PickerId, arg.SelectedValue));
                if (arg.PickerId == "CRValidFromId")
                {
                    CRValidFrom = arg.SelectedValue;
                }
                else if (arg.PickerId == "LicenseValidFromId")
                {
                    ValidFrom = arg.SelectedValue;
                }
            });
        }
        public void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
        }
        private async Task AddAttachment(string docType)
        {
            try
            {
                decimal TotalAttachmentSize = 0;
                string[] filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForTaxEvasion();

                var fileData = await CrossFilePicker.Current.PickFile(filetypes);
                if (AttachmentCount < 5)
                {
                    if (fileData != null)
                    {
                        var attachmentByte = fileData.DataArray;

                        string base64String = Convert.ToBase64String(attachmentByte, 0, attachmentByte.Length);
                        var attachmentName = fileData.FileName;

                        float sizemb = (attachmentByte.Length / 1024f) / 1024f;
                        decimal attachmentSize = 0;
                        attachmentSize = attachmentSize + (Decimal)sizemb;

                        if (fileData.FileName.Contains("."))
                        {
                            string Extention = fileData.FileName.Split('.')[1];//pdf
                            if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "jpeg")
                            {
                                if (TotalAttachmentSize <= 30)
                                {
                                    attachmentSize = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachmentByte.Length) / 1048576.0)), 2);
                                    decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachmentByte.Length) / 1048576.0)), 4);
                                    if (Convert.ToDecimal(attachmentSize) <= 10)
                                    {
                                        if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                        {
                                            try
                                            {
                                                string attachmentType = UtilityManager.GetContentType(Extention);
                                                await SaveAttachment(attachmentByte, attachmentName, docType, attachmentType);
                                            }
                                            catch (Exception ex)
                                            {
                                            }
                                        }
                                        else
                                        {
                                            attachmentName = string.Empty;
                                            await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        private async void updateActivityList(string indSector)
        {
            IsLoading = true;
            activityList = await WebServiceManager.ESTOutletGetActivitySetsList(indSector);
            IsLoading = false;
        }
        private async void fetchTabDataAndBind()
        {
            try
            {
                IsLoading = true;
                EnableIssueByDropDown = true;
                EnableInputFields = true;
                resetForm();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                {
                    OutletDropDowns = await WebServiceManager.ESTOutletDropDowns();
                    activityList = await WebServiceManager.ESTOutletGetActivitySetsList();
                    EnableIssueByDropDown = false;
                    CRNumber = validateCR?.Crnum;
                    CRIssueCountry = OutletDropDowns.country_dropdownSet.results.Where(i => i.Land1 == "SA").FirstOrDefault();
                    CRIssueBy = CRIssueCountry.Land1 == "SA" ? EnIssueBy["90702"] : EnIssueBy["90718"];
                    CRIssueCity = new CityDropdownItem()
                    {
                        CityName = validateCR?.CityAry,
                        CityCode = string.Empty
                    };
                    CRValidFrom = validateCR?.Issuedt?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    EnableCRInputField = string.IsNullOrEmpty(validateCR?.Crname);
                }
                else if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                {
                    OutletDropDowns = await WebServiceManager.ESTOutletDropDowns();
                    activityList = await WebServiceManager.ESTOutletGetActivitySetsList();
                }
                else
                {
                    LicenseData = NregActivityList.Where(i => i.Type == "ZS0004").ToList();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }
        public async void validateCRNumber()
        {
            IsLoading = true;
            validateCR = await WebServiceManager.ESTValidateCRNum(CRNumber);
            IsLoading = false;

            if (validateCR?.NotFound == "X")
            {
                await _dialogService.ShowMessage("CR Number is invalid", AppResources.Information);
                return;
            }
            if (validateCR?.Excption == "X")
            {
                return;
            }
            CRIssueCity = new CityDropdownItem()
            {
                CityName = validateCR?.CityAry,
                CityCode = string.Empty
            };
            CRValidFrom = validateCR?.Issuedt?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
            EnableInputFields = string.IsNullOrEmpty(validateCR?.Crname);
        }
        private void resetForm()
        {
            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
            {
                CRIssueCountry = null;
                CRIssueBy = null;
                CRIssueCity = null;
                CRNumber = string.Empty;
                CRsCopies = new ObservableCollection<Attachment>();
                TransferCRsCopies = new ObservableCollection<Attachment>();
                MainActivity = false;
                CRValidFrom = null;
                CRMainGroup = null;
                CRSubGroup = null;
                CRAcitivity = null;
            }
            else if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
            {
                LicenseIssueCountry = null;
                LicenseIssueBy = null;
                LicenseIssueCity = null;
                LicenseNumber = string.Empty;
                LicensesCopies = new ObservableCollection<Attachment>();
                MainActivity = false;
                ValidFrom = null;
                LicenseMainGroup = null;
                LicenseSubGroup = null;
                LicenseAcitivity = null;
            }
        }
        private async Task SaveAttachment(byte[] attachmentByteData, string fileName, string docType, string contentType)
        {
            try
            {
                IsLoading = true;
                string CRLicenseNo = string.Empty;
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                {
                    CRLicenseNo = CRNumber;
                }
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                {
                    CRLicenseNo = LicenseNumber;
                }
                string outletref = $"{Int16.Parse(newNumber?.Actno):000-}" + CRLicenseNo;

                Attachment dd = await WebServiceManager.ESTAttachment(attachmentByteData, fileName, taxPayerDetails?.ReturnIdx, docType, contentType, outletref);

                if (docType == "RG01")
                {
                    CRsCopies.Add(dd);
                }
                else if (docType == "RG12")
                {
                    TransferCRsCopies.Add(dd);
                }
                else if (docType == "RG02")
                {
                    LicensesCopies.Add(dd);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                IsLoading = false;
            }
        }
        private async void OnDeleteAttachment(Attachment item, string docType)
        {
            IsLoading = true;
            var delete = WebServiceManager.ESTDeleteAttachment(item?.Filename, item?.RetGuid, docType, item?.Doguid);
            if(!string.IsNullOrEmpty(delete) && delete == "delete")
            {
                if (docType == "RG01")
                {
                    CRsCopies.Remove(item);
                }
                else if (docType == "RG12")
                {
                    TransferCRsCopies.Remove(item);
                }
                else if (docType == "RG02")
                {
                    LicensesCopies.Remove(item);
                }
                IsLoading = false;
            }
            else
            {
                IsLoading = false;
                await _dialogService.ShowError(AppResources.ZZSomethingwentwrong, AppResources.Information, "Ok", null);
            }
        }
        #endregion
    }
}
