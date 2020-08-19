using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Views.NewDesign.EstablishmentRegistrationPages;
using GalaSoft.MvvmLight.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class EstablishmentRegistrationPageViewModel : BaseViewModel
    {
        #region Variable
        private EstablishmentRegistrationTabsEnum _currentTab = EstablishmentRegistrationTabsEnum.FinancialDetail;
        public EstablishmentRegistrationTabsEnum currentTab
        {
            get => _currentTab;
            private set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
            }
        }

        public bool MarkComplete { get; private set; } = false;
        private int _maxIndex = 5;
        public int MaxIndex
        {
            get => _maxIndex; private set
            {
                _maxIndex = value;
                RaisePropertyChanged(nameof(MaxIndex));
            }
        }



        private int _currenrIndex = 4;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                _currenrIndex = value;
                RaisePropertyChanged(nameof(CurrentIndex));
                MarkComplete = _currenrIndex == MaxIndex;
                RaisePropertyChanged(nameof(MarkComplete));
            }
        }

        private string _selectedTabText = "Financial Details";
        public string SelectedTabText
        {
            get => _selectedTabText;
            private set
            {
                _selectedTabText = value;
                RaisePropertyChanged(nameof(SelectedTabText));
            }
        }

        private bool _isClickedOwnRentOption = false;
        public bool IsClickedOwnRentOption
        {
            get => _isClickedOwnRentOption;
            private set
            {
                _isClickedOwnRentOption = value;
                RaisePropertyChanged("IsClickedOwnRentOption");
            }
        }

        private bool _isClickedStayMoreThanKSAOption = false;
        public bool IsClickedStayMoreThanKSAOption
        {
            get => _isClickedStayMoreThanKSAOption;
            private set
            {
                _isClickedStayMoreThanKSAOption = value;
                RaisePropertyChanged("IsClickedStayMoreThanKSAOption");
            }
        }


        private bool _isClickedNoneOfTheAboveOption = false;
        public bool IsClickedNoneOfTheAboveOption
        {
            get => _isClickedNoneOfTheAboveOption;
            private set
            {
                _isClickedNoneOfTheAboveOption = value;
                RaisePropertyChanged("IsClickedNoneOfTheAboveOption");
            }
        }


        private bool _isClickedPermanentLegalEntity = false;
        public bool IsClickedPermanentLegalEntity
        {
            get => _isClickedPermanentLegalEntity;
            private set
            {
                _isClickedPermanentLegalEntity = value;
                RaisePropertyChanged("IsClickedPermanentLegalEntity");
            }
        }


        private bool _isClickedOtherTaxableIncomeLegalEntity = false;
        public bool IsClickedOtherTaxableIncomeLegalEntity
        {
            get => _isClickedOtherTaxableIncomeLegalEntity;
            private set
            {
                _isClickedOtherTaxableIncomeLegalEntity = value;
                RaisePropertyChanged("IsClickedOtherTaxableIncomeLegalEntity");
            }
        }




        private bool _isClickedABranchOfNonResidentCompanyPE = false;
        public bool IsClickedABranchOfNonResidentCompanyPE
        {
            get => _isClickedABranchOfNonResidentCompanyPE;
            private set
            {
                _isClickedABranchOfNonResidentCompanyPE = value;
                RaisePropertyChanged("IsClickedABranchOfNonResidentCompanyPE");
            }
        }



        private bool _isClickedConstructionSitePE = false;
        public bool IsClickedConstructionSitePE
        {
            get => _isClickedConstructionSitePE;
            private set
            {
                _isClickedConstructionSitePE = value;
                RaisePropertyChanged("IsClickedConstructionSitePE");
            }
        }


        private bool _isClickedInstallationPE = false;
        public bool IsClickedInstallationPE
        {
            get => _isClickedInstallationPE;
            private set
            {
                _isClickedInstallationPE = value;
                RaisePropertyChanged("IsClickedInstallationPE");
            }
        }


        private bool _isClickedAFixedBasePE = false;
        public bool IsClickedAFixedBasePE
        {
            get => _isClickedAFixedBasePE;
            private set
            {
                _isClickedAFixedBasePE = value;
                RaisePropertyChanged("IsClickedAFixedBasePE");
            }
        }

        private bool _isClickedNonResidentPartnerPE = false;
        public bool IsClickedNonResidentPartnerPE
        {
            get => _isClickedNonResidentPartnerPE;
            private set
            {
                _isClickedNonResidentPartnerPE = value;
                RaisePropertyChanged("IsClickedNonResidentPartnerPE");
            }
        }




        private List<ETaxableIncomeSourceTypeListModel> _taxableIncomeSourceTypeList;
        public List<ETaxableIncomeSourceTypeListModel> TaxableIncomeSourceTypeList
        {
            get
            {
                return _taxableIncomeSourceTypeList;
            }
            set
            {
                _taxableIncomeSourceTypeList = value;
                RaisePropertyChanged("TaxableIncomeSourceTypeList");
            }
        }


        private bool _isAttachmentEnable = false;
        public bool IsAttachmentEnabled
        {
            get
            {
                return _isAttachmentEnable;
            }
            set
            {
                _isAttachmentEnable = value;
                RaisePropertyChanged("IsAttachmentEnable");
            }
        }

        #region Financial Details Tabs variables
        private ObservableCollection<string> _methodList = new ObservableCollection<string>();
        public ObservableCollection<string> MethodList
        {
            get => _methodList;
            private set
            {
                _methodList = value;
                RaisePropertyChanged(nameof(MethodList));
            }
        }
        private string _selectedMethod = null;
        public string SelectedMethod
        {
            get => _selectedMethod;
            private set
            {
                _selectedMethod = value;
                RaisePropertyChanged(nameof(SelectedMethod));
            }
        }
        public ObservableCollection<string> _calendarTypeList = new ObservableCollection<string>();
        public ObservableCollection<string> CalendarTypeList
        {
            get => _calendarTypeList;
            private set
            {
                _calendarTypeList = value;
                RaisePropertyChanged(nameof(CalendarTypeList));
            }
        }
        public string _calendarType = null;
        public string CalendarType
        {
            get => _calendarType;
            private set
            {
                _calendarType = value;
                RaisePropertyChanged(nameof(CalendarType));
            }
        }
        #endregion

        #endregion

        #region Commands


        public ICommand OnNextButtonClick { get; private set; }
        public ICommand OnPreButtonClick { get; private set; }


        #region NationalityStatus option commands
        public ICommand NationalityStatusRentOwnHouseClick { get; set; }
        public ICommand NationalityStatusStayMoreThanKSAClick { get; set; }
        public ICommand NationalityStatusNoneOfTheAboveClick { get; set; }

        #endregion

        #region Legal Entity commands
        public ICommand PermanentEstablishmentLegalEntityClick { get; set; }

        public ICommand OtherTaxableIncomeLegalEntityClick { get; set; }

        #endregion

        #region Permanent Establishment options Commmands
        public ICommand ABranchOfNonResidentCompanyPEClick { get; set; }
        public ICommand ConstructionSitePEClick { get; set; }
        public ICommand InstallationPEClick { get; set; }
        public ICommand AFixedBasePEClick { get; set; }
        public ICommand NonResidentPartnerPEClick { get; set; }
        #endregion



        public ICommand OnAttachmentClick { get; set; }


        #region Financial Details Tabs commands
        public ICommand OnMonthSelectButtonClick { get; set; }
        #endregion

        #endregion

        #region Constructor
        public EstablishmentRegistrationPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnNextButtonClick = new Command(() => navigateToNext());
            OnPreButtonClick = new Command(() => navigateToPre());
            NationalityStatusStayMoreThanKSAClick = new Command(() => nationalityStatusSelection(EstablishmentRegistrationNationalityEnum.StayMoreThanKSA));
            NationalityStatusRentOwnHouseClick = new Command(() => nationalityStatusSelection(EstablishmentRegistrationNationalityEnum.RentOwnhouseMoreThanThirtyDays));
            NationalityStatusNoneOfTheAboveClick = new Command(() => nationalityStatusSelection(EstablishmentRegistrationNationalityEnum.NoneOfTheAbove));
            PermanentEstablishmentLegalEntityClick = new Command(() => legalEntitySelection(EstablishmentRegistrationLegalEntityEnum.PermanentEstablishment));
            OtherTaxableIncomeLegalEntityClick = new Command(() => legalEntitySelection(EstablishmentRegistrationLegalEntityEnum.OtherTaxIncomeFromSourceWithInTheSKA));

            #region Permanent Establishment options

            ABranchOfNonResidentCompanyPEClick = new Command(() => permanentEstablishmentsOptionSelection(EstablishmentRegistrationParmanentEstablishmentEnum.ABranchOfNonResidentCompanyPE));
            ConstructionSitePEClick = new Command(() => permanentEstablishmentsOptionSelection(EstablishmentRegistrationParmanentEstablishmentEnum.ConstructionSitePE));
            InstallationPEClick = new Command(() => permanentEstablishmentsOptionSelection(EstablishmentRegistrationParmanentEstablishmentEnum.InstallationPE));
            AFixedBasePEClick = new Command(() => permanentEstablishmentsOptionSelection(EstablishmentRegistrationParmanentEstablishmentEnum.AFixedBasePE));
            NonResidentPartnerPEClick = new Command(() => permanentEstablishmentsOptionSelection(EstablishmentRegistrationParmanentEstablishmentEnum.NonResidentPartnerPE));

            #endregion

            #region Financial Details Tabs variable initialization
            MethodList.Clear();
            MethodList.Add("Accounts");
            MethodList.Add("Estimate");
            
            CalendarTypeList.Clear();
            CalendarTypeList.Add("Hijri");
            CalendarTypeList.Add("Gregorian");
            
            Device.BeginInvokeOnMainThread(()=> {
                SelectedMethod = MethodList.FirstOrDefault();
                CalendarType = CalendarTypeList.LastOrDefault();
            });
            OnMonthSelectButtonClick = new Command(() => {
                PopupNavigation.Instance.PushAsync(new NumberListPopUpPageView());
            });
            #endregion

            testObjectPrepare();
        }

        #endregion

        #region Method
        private void navigateToNext()
        {
            if (currentTab == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
            {
                currentTab = EstablishmentRegistrationTabsEnum.Outlets;
                SelectedTabText = "Outlets";
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.Outlets)
            {
                currentTab = EstablishmentRegistrationTabsEnum.FinancialDetail;
                SelectedTabText = "Financial Details";
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.FinancialDetail)
            {
                currentTab = EstablishmentRegistrationTabsEnum.Declaration;
                SelectedTabText = "Summary";
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.RegistrationType)
            {
                currentTab = EstablishmentRegistrationTabsEnum.TaxpayerDetail;
                SelectedTabText = "Taxpayer Personal Details";
            }
        }
        private void navigateToPre()
        {
            if (currentTab == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
            {
                currentTab = EstablishmentRegistrationTabsEnum.RegistrationType;
                SelectedTabText = "Registration/Taxpayer type";
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.Outlets)
            {
                currentTab = EstablishmentRegistrationTabsEnum.TaxpayerDetail;
                SelectedTabText = "Taxpayer Personal Details";
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.FinancialDetail)
            {
                currentTab = EstablishmentRegistrationTabsEnum.Outlets;
                SelectedTabText = "Outlets";
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.Declaration)
            {
                currentTab = EstablishmentRegistrationTabsEnum.FinancialDetail;
                SelectedTabText = "Financial Details";
            }
        }

        private void nationalityStatusSelection(EstablishmentRegistrationNationalityEnum selectedOption)
        {
            switch (selectedOption)
            {
                case EstablishmentRegistrationNationalityEnum.StayMoreThanKSA:

                    IsClickedStayMoreThanKSAOption = true;
                    IsClickedOwnRentOption = false;
                    IsClickedNoneOfTheAboveOption = false;

                    //Options clear or done false
                    IsClickedPermanentLegalEntity = false;
                    IsClickedOtherTaxableIncomeLegalEntity = false;

                    //Sub - Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;

                    break;
                case EstablishmentRegistrationNationalityEnum.RentOwnhouseMoreThanThirtyDays:

                    IsClickedStayMoreThanKSAOption = false;
                    IsClickedOwnRentOption = true;
                    IsClickedNoneOfTheAboveOption = false;

                    //Options clear or done false
                    IsClickedPermanentLegalEntity = false;
                    IsClickedOtherTaxableIncomeLegalEntity = false;


                    //Sub - Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;

                    break;
                case EstablishmentRegistrationNationalityEnum.NoneOfTheAbove:
                    IsClickedStayMoreThanKSAOption = false;
                    IsClickedOwnRentOption = false;
                    IsClickedNoneOfTheAboveOption = true;

                    //Options clear or done false
                    IsClickedPermanentLegalEntity = false;
                    IsClickedOtherTaxableIncomeLegalEntity = false;


                    //Sub - Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;

                    break;
                default:
                    IsClickedStayMoreThanKSAOption = false;
                    IsClickedOwnRentOption = false;
                    IsClickedNoneOfTheAboveOption = false;

                    //Options clear or done false
                    IsClickedPermanentLegalEntity = false;
                    IsClickedOtherTaxableIncomeLegalEntity = false;


                    //Sub - Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
            }
        }

        private void legalEntitySelection(EstablishmentRegistrationLegalEntityEnum selectedOption)
        {
            switch (selectedOption)
            {
                case EstablishmentRegistrationLegalEntityEnum.PermanentEstablishment:

                    IsClickedPermanentLegalEntity = true;
                    IsClickedOtherTaxableIncomeLegalEntity = false;

                    //Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case EstablishmentRegistrationLegalEntityEnum.OtherTaxIncomeFromSourceWithInTheSKA:

                    IsClickedPermanentLegalEntity = false;
                    IsClickedOtherTaxableIncomeLegalEntity = true;


                    //Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                default:
                    //Options clear or done false
                    IsClickedPermanentLegalEntity = false;
                    IsClickedOtherTaxableIncomeLegalEntity = false;

                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;

            }
        }

        private void permanentEstablishmentsOptionSelection(EstablishmentRegistrationParmanentEstablishmentEnum selectedOption)
        {
            switch (selectedOption)
            {
                case EstablishmentRegistrationParmanentEstablishmentEnum.ABranchOfNonResidentCompanyPE:
                    IsClickedABranchOfNonResidentCompanyPE = true;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case EstablishmentRegistrationParmanentEstablishmentEnum.ConstructionSitePE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = true;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case EstablishmentRegistrationParmanentEstablishmentEnum.InstallationPE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = true;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case EstablishmentRegistrationParmanentEstablishmentEnum.AFixedBasePE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = true;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case EstablishmentRegistrationParmanentEstablishmentEnum.NonResidentPartnerPE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = true;
                    break;
                default:
                    //Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;

            }
        }


        private void testObjectPrepare()
        {
            TaxableIncomeSourceTypeList = new List<ETaxableIncomeSourceTypeListModel>()
                { new ETaxableIncomeSourceTypeListModel() { IncomeSourceType="First Record", Id=101},
                new ETaxableIncomeSourceTypeListModel() { IncomeSourceType = "First Secound record", Id = 102 } };

        }
        #endregion
    }


    public class ETaxableIncomeSourceTypeListModel
    {
        public string IncomeSourceType { get; set; }

        public int Id { get; set; }

        public bool IsSelectedType { get; set; }
    }
}