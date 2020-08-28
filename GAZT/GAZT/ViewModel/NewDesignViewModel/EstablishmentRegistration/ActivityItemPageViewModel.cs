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
        private ActivitySetsList filteredActivityList = null;
        public OutletNumber newNumber { get; set; } = null;
        public ValidateCR validateCR { get; set; } = null;
        public Nreg_ActivityItem cRActivityItem { get; set; } = null;
        public Action goBackAction =null;
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
                        ActivityTitle = "Add License";
                        break;
                    case EstablishmentOutletActivitiesTabsEnum.ActivityList:
                        ActivityTitle = "License Details";
                        break;
                    case EstablishmentOutletActivitiesTabsEnum.CRDetails:
                    default:
                        ActivityTitle = "Commercial Registration";
                        break;
                }
            }
        }
        private string _activityTitle = "Commercial Registration";
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
        private bool CanExecuteClickCommand(object args) => DisableInputFields;

        private CountryDropdownItem _cRIssueCountry = null;
        public CountryDropdownItem CRIssueCountry
        {
            get => _cRIssueCountry;
            set
            {
                if (value != null)
                {
                    _cRIssueCountry = value;
                    RaisePropertyChanged(nameof(CRIssueCountry));
                }
            }
        }
        private string _cRIssueBy = null;
        public string CRIssueBy
        {
            get => _cRIssueBy;
            set
            {
                if (value != null)
                {
                    _cRIssueBy = value;
                    RaisePropertyChanged(nameof(CRIssueBy));
                }
            }
        }
        private CityDropdownItem _cRIssueCity = null;
        public CityDropdownItem CRIssueCity
        {
            get => _cRIssueCity;
            set
            {
                if (value != null)
                {
                    _cRIssueCity = value;
                    RaisePropertyChanged(nameof(CRIssueCity));
                }
            }
        }
        private string _cRNumber = string.Empty;
        public string CRNumber {
            get => _cRNumber;
            set
            {
                _cRNumber = value;
                RaisePropertyChanged(nameof(CRNumber));
            }
        }
        private bool _disableInputFields = true;
        public bool DisableInputFields
        {
            get => _disableInputFields;
            set
            {
                _disableInputFields = value;
                OnIssueCountrySelectButtonClick.ChangeCanExecute();
                OnIssueBySelectButtonClick.ChangeCanExecute();
                OnIssueCitySelectButtonClick.ChangeCanExecute();
                OnValidFromButtonClick.ChangeCanExecute();
                RaisePropertyChanged(nameof(DisableInputFields));
            }
        }
        private OutletDropDowns _outletDropDowns = null;
        public OutletDropDowns OutletDropDowns
        {
            get => _outletDropDowns;
            set
            {
                if (value != null)
                {
                    _outletDropDowns = value;
                    RaisePropertyChanged(nameof(OutletDropDowns));
                }
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
                if (value != null)
                {
                    _cRSubGroup = value;
                    RaisePropertyChanged(nameof(CRSubGroup));
                }
            }
        }
        private ActivityGroupSubGroup _cRAcitivity;
        public ActivityGroupSubGroup CRAcitivity
        {
            get => _cRAcitivity;
            set
            {
                if (value != null)
                {
                    _cRAcitivity = value;
                    RaisePropertyChanged(nameof(CRAcitivity));
                }
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
                if (value != null)
                {
                    _licenseIssueCountry = value;
                    RaisePropertyChanged(nameof(LicenseIssueCountry));
                }
            }
        }
        private string _licenseIssueBy = null;
        public string LicenseIssueBy
        {
            get => _licenseIssueBy;
            set
            {
                if (value != null)
                {
                    _licenseIssueBy = value;
                    RaisePropertyChanged(nameof(LicenseIssueBy));
                }
            }
        }
        private CityDropdownItem _licenseIssueCity = null;
        public CityDropdownItem LicenseIssueCity
        {
            get => _licenseIssueCity;
            set
            {
                if (value != null)
                {
                    _licenseIssueCity = value;
                    RaisePropertyChanged(nameof(LicenseIssueCity));
                }
            }
        }
        private ActivityGroupSubGroup _licenseMainGroup = null;
        public ActivityGroupSubGroup LicenseMainGroup
        {
            get => _licenseMainGroup;
            set
            {
                if (value != null)
                {
                    _licenseMainGroup = value;
                    RaisePropertyChanged(nameof(LicenseMainGroup));
                }
            }
        }
        private ActivityGroupSubGroup _licenseSubGroup = null;
        public ActivityGroupSubGroup LicenseSubGroup
        {
            get => _licenseSubGroup;
            set
            {
                if (value != null)
                {
                    _licenseSubGroup = value;
                    RaisePropertyChanged(nameof(LicenseSubGroup));
                }
            }
        }
        private ActivityGroupSubGroup _licenseAcitivity;
        public ActivityGroupSubGroup LicenseAcitivity
        {
            get => _licenseAcitivity;
            set
            {
                if (value != null)
                {
                    _licenseAcitivity = value;
                    RaisePropertyChanged(nameof(LicenseAcitivity));
                }
            }
        }
        private ObservableCollection<Attachment> _licensesCopies = new ObservableCollection<Attachment>();
        public ObservableCollection<Attachment> LicensesCopies
        {
            get => _licensesCopies;
            set
            {
                if (value != null)
                {
                    _licensesCopies = value;
                    RaisePropertyChanged(nameof(LicensesCopies));
                }
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

        public ICommand OnTransferCopyOfLicenseChoiceButtonClick { get; set; }
        #endregion

        #region Constructor
        public ActivityItemPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnNextButtonClick = new Command(() => navigateToNext());
            OnPreButtonClick = new Command(() => navigationService.GoBack());
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
            }, CanExecuteClickCommand);
            OnIssueCitySelectButtonClick = new Command((object o) =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(OutletDropDowns?.city_dropdownSet?.results);
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
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(filteredActivityList?.act_groupSet?.results);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                            CRMainGroup = item as ActivityGroupSubGroup;
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                            LicenseMainGroup = item as ActivityGroupSubGroup;
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
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(filteredActivityList?.act_subgroupSet?.results);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                            CRSubGroup = item as ActivityGroupSubGroup;
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                            LicenseSubGroup = item as ActivityGroupSubGroup;
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
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(filteredActivityList?.activitySet?.results);
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
        }
        #endregion

        #region Method
        private void navigateToNext()
        {
            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
            {
                CurrentTab = EstablishmentOutletActivitiesTabsEnum.ActivityList;
            }
            else if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails || CurrentTab == EstablishmentOutletActivitiesTabsEnum.ActivityList)
            {
                goBackAction?.Invoke();
                _navigationService.GoBack();
            }
        }
        public void OnAppearing()
        {
            fetchTabDataAndBind();
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
                                                //if (docType == "RG01")
                                                //{
                                                //    CRsCopies.Add(new Attachment());
                                                //}
                                                //else if (docType == "RG12")
                                                //{
                                                //    TransferCRsCopies.Add(new Attachment());
                                                //}
                                                //else if (docType == "RG02")
                                                //{
                                                //    LicensesCopies.Add(new Attachment());
                                                //}
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
            filteredActivityList = await WebServiceManager.ESTOutletGetActivitySetsList(indSector);
            IsLoading = false;
        }
        private async void fetchTabDataAndBind()
        {
            try
            {
                IsLoading = true;
                OutletDropDowns = await WebServiceManager.ESTOutletDropDowns();
                activityList = await WebServiceManager.ESTOutletGetActivitySetsList();
                filteredActivityList = activityList;
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                {
                    CRIssueCountry = OutletDropDowns.country_dropdownSet.results.Where(i => i.Land1 == cRActivityItem?.Country).FirstOrDefault();
                    CRIssueBy = EnIssueBy[cRActivityItem?.Institute];
                    CRIssueCity = OutletDropDowns.city_dropdownSet.results.Where(i => i.CityName == validateCR?.CityAry).FirstOrDefault();
                    CRNumber = cRActivityItem?.Idnumber;
                    CRValidFrom = validateCR?.Issuedt?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    DisableInputFields = string.IsNullOrEmpty(validateCR?.Crname);
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
        private async Task SaveAttachment(byte[] attachmentByteData, string fileName, string docType, string contentType)
        {
            try
            {
                IsLoading = true;
                string outletref = $"{Int16.Parse(newNumber?.Actno):000-}"+cRActivityItem?.Idnumber;

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
        #endregion
    }
}
