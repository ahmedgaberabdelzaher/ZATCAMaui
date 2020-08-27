using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.GenericPickers;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    public class ActivityItemPageViewModel : BaseViewModel
    {
        #region variables
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        private ActivitySetsList activityList = null;
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

        private ActivityGroupSubGroup _cRMainGroup = null;
        public ActivityGroupSubGroup CRMainGroup {
            get => _cRMainGroup;
            set
            {
                if(value != null)
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
                if(value != null)
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
        #endregion

        #region commands
        public ICommand OnNextButtonClick { get; private set; }
        public ICommand OnPreButtonClick { get; private set; }
        public ICommand OnIssueCountrySelectButtonClick { get; set; }
        public ICommand OnIssueBySelectButtonClick { get; set; }
        public ICommand OnIssueCitySelectButtonClick { get; set; }
        public ICommand OnValidFromButtonClick { get; set; }
        //public ICommand OnAcitivitySelectButtonClick { get; set; }
        public ICommand OnMainGroupSelectButtonClick { get; set; }
        public ICommand OnSubGroupSelectButtonClick { get; set; }
        public ICommand OnAcitivitySelectButtonClick { get; set; }
        #endregion

        #region Constructor
        public ActivityItemPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnNextButtonClick = new Command(() => navigateToNext());
            OnPreButtonClick = new Command(() => navigationService.GoBack());
            OnIssueCountrySelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(OutletDropDowns?.country_dropdownSet?.results);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        CRIssueCountry = item as CountryDropdownItem;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            OnIssueBySelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(EnIssueBy.Values);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        CRIssueBy = item as string;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            OnIssueCitySelectButtonClick = new Command(()=> {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(OutletDropDowns?.city_dropdownSet?.results.Where(i => i.Country == CRIssueCountry.Land1));
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        CRIssueCity = item as CityDropdownItem;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            OnValidFromButtonClick = new Command(()=> {
                PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(new GenericDatePickerModel()
                {
                    DatePickerTitle = "Valid From",
                    PickerId = "ValidFromId"
                }));
            });
            OnMainGroupSelectButtonClick = new Command(() => {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(activityList?.act_groupSet?.results);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        CRMainGroup = item as ActivityGroupSubGroup;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            OnSubGroupSelectButtonClick = new Command(() => {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(activityList?.act_subgroupSet?.results);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        CRSubGroup = item as ActivityGroupSubGroup;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            OnAcitivitySelectButtonClick = new Command(() => {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(activityList?.activitySet?.results);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        CRAcitivity = item as ActivityGroupSubGroup;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
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
                _navigationService.GoBack();
            }
        }
        public void OnAppearing()
        {
            fetchTabDataAndBind();
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (sender, arg) => {
                Console.WriteLine(string.Format( "Subscribe {0}, {1}", arg.PickerId, arg.SelectedValue ));
            });
        }
        public void OnDisappearing()
        {

            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
        }

        private async void fetchTabDataAndBind()
        {
            try
            {
                IsLoading = true;
                //TaxpayerFullNationlityList = await WebServiceManager.ESTTaxPayerNationality(taxPayerDetails?.Tpnationality);
                OutletDropDowns = await WebServiceManager.ESTOutletDropDowns();
                activityList = await WebServiceManager.ESTOutletGetActivitySetsList();
            }
            catch (Exception e)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion
    }
}
