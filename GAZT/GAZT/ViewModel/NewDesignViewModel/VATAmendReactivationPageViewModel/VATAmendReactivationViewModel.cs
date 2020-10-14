using EGAZT.Models;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace EGAZT.ViewModel.NewDesignViewModel.VATAmendReactivationPageViewModel
{
    public class VATAmendReactivationPageViewModel : ViewModelBase
    {
        public string OriginalData;
        public string ModifiedData;
        string idnumber { get; set; }
        public int DefaultMonth;
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public static IsComeFromForAttachment IsComeFromForAttachment;
        public string PageTitle { get; set; }

        #region Variable

        //private ZakatForm5TabEnum _currentTab = ZakatForm5TabEnum.BasicInformation;
        //public ZakatForm5TabEnum currentTab
        //{
        //    get => _currentTab;
        //    private set
        //    {
        //        _currentTab = value;
        //        RaisePropertyChanged(nameof(currentTab));
        //        CurrentIndex = (int)_currentTab;
        //        RaisePropertyChanged(nameof(CurrentIndex));
        //    }
        //}
        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                _currenrIndex = value;
                RaisePropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 5;
        #endregion


        #region Properties
        private Color _continueButtonnBackroundColor = Color.FromHex("#d49504");
        public Color ContinueButtonnBackroundColor
        {
            get
            {
                return _continueButtonnBackroundColor;
            }
            set
            {
                _continueButtonnBackroundColor = value;
                RaisePropertyChanged("ContinueButtonnBackroundColor");
            }
        }
        private int _answer1selectedcount = 0;
        public int answer1selectedcount
        {
            get
            {
                return _answer1selectedcount;
            }
            set
            {
                _answer1selectedcount = value;
                RaisePropertyChanged("answer1selectedcount");
            }
        }
        private ATTDETSet _ATTDETSetObject;
        public ATTDETSet ATTDETSetObject
        {
            get
            {
                return _ATTDETSetObject;
            }
            set
            {
                _ATTDETSetObject = value;
                RaisePropertyChanged("ATTDETSetObject");
            }
        }
        private int _answer2selectedcount = 0;
        public int answer2selectedcount
        {
            get
            {
                return _answer2selectedcount;
            }
            set
            {
                _answer2selectedcount = value;
                RaisePropertyChanged("answer2selectedcount");
            }
        }

        private int _answer3selectedcount = 0;
        public int answer3selectedcount
        {
            get
            {
                return _answer3selectedcount;
            }
            set
            {
                _answer3selectedcount = value;
                RaisePropertyChanged("answer3selectedcount");
            }
        }
        private int _answer4selectedcount = 0;
        public int answer4selectedcount
        {
            get
            {
                return _answer4selectedcount;
            }
            set
            {
                _answer4selectedcount = value;
                RaisePropertyChanged("answer4selectedcount");
            }
        }

        private bool _isFDNameMobEmailEnable = false;
        public bool IsFDNameMobEmailEnable
        {
            get
            {
                return _isFDNameMobEmailEnable;
            }
            set
            {
                _isFDNameMobEmailEnable = value;

                RaisePropertyChanged("IsFDNameMobEmailEnable");
            }
        }

        private bool _IsBackStepButtonVisible = true;
        public bool IsBackStepButtonVisible
        {
            get
            {
                return _IsBackStepButtonVisible;
            }
            set
            {
                _IsBackStepButtonVisible = value;

                RaisePropertyChanged("IsBackStepButtonVisible");
            }
        }

        private bool _iDNumberNonMandatoryVisibility = true;
        public bool IDNumberNonMandatoryVisibility
        {
            get
            {
                return _iDNumberNonMandatoryVisibility;
            }
            set
            {
                _iDNumberNonMandatoryVisibility = value;

                RaisePropertyChanged("IDNumberNonMandatoryVisibility");
            }
        }

        private bool _iDNumberMandatoryVisibility = false;
        public bool IDNumberMandatoryVisibility
        {
            get
            {
                return _iDNumberMandatoryVisibility;
            }
            set
            {
                _iDNumberMandatoryVisibility = value;
                if (_iDNumberMandatoryVisibility)
                {
                    IDNumberVisibility = false;
                }
                else
                {
                    IDNumberVisibility = true;
                }

                RaisePropertyChanged("IDNumberMandatoryVisibility");
            }
        }
        private bool _iDNumberVisibility = true;
        public bool IDNumberVisibility
        {
            get
            {
                return _iDNumberVisibility;
            }
            set
            {
                _iDNumberVisibility = value;

                RaisePropertyChanged("IDNumberVisibility");
            }
        }
        private bool _dOBNonMandatoryVisibility = false;
        public bool DOBNonMandatoryVisibility
        {
            get
            {
                return _dOBNonMandatoryVisibility;
            }
            set
            {
                _dOBNonMandatoryVisibility = value;

                RaisePropertyChanged("DOBNonMandatoryVisibility");
            }
        }

        private bool _dOBMandatoryVisibility = false;
        public bool DOBMandatoryVisibility
        {
            get
            {
                return _dOBMandatoryVisibility;
            }
            set
            {
                _dOBMandatoryVisibility = value;

                RaisePropertyChanged("DOBMandatoryVisibility");
            }
        }


        private bool _isAttachmentImporterExporterVisible = false;
        public bool isAttachmentImporterExporterVisible
        {
            get
            {
                return _isAttachmentImporterExporterVisible;
            }
            set
            {
                _isAttachmentImporterExporterVisible = value;

                RaisePropertyChanged("isAttachmentImporterExporterVisible");
            }
        }

        private bool _isContinueButtonEnable = false;
        public bool IsContinueButtonEnable
        {
            get
            {
                return _isContinueButtonEnable;
            }
            set
            {
                _isContinueButtonEnable = value;
                if (_isContinueButtonEnable)
                {
                    ContinueButtonnBackroundColor = Color.FromHex("#d49504");
                }
                else
                {
                    ContinueButtonnBackroundColor = Color.FromHex("#9EA4A9");
                }
                RaisePropertyChanged("IsContinueButtonEnable");
            }
        }
        private string _startdateToshow = string.Empty;
        public string StartdateToshow
        {
            get
            {
                return _startdateToshow;
            }
            set
            {
                _startdateToshow = value;

                RaisePropertyChanged("StartdateToshow");
            }
        }
        private string _quesTion3answerSelected = string.Empty;
        public string quesTion3answerSelected
        {
            get
            {
                return _quesTion3answerSelected;
            }
            set
            {
                _quesTion3answerSelected = value;

                RaisePropertyChanged("quesTion3answerSelected");
            }
        }
        private string _quesTion4answerSelected = string.Empty;
        public string quesTion4answerSelected
        {
            get
            {
                return _quesTion4answerSelected;
            }
            set
            {
                _quesTion4answerSelected = value;

                RaisePropertyChanged("quesTion4answerSelected");
            }
        }
        private ObservableCollection<object> _todayDate;
        public ObservableCollection<object> TodayDate
        {
            get
            {
                return _todayDate;
            }
            set
            {
                _todayDate = value;
                RaisePropertyChanged("TodayDate");
            }
        }
        private string _DOB = string.Empty;
        public string DOB
        {
            get
            {
                return _DOB;
            }
            set
            {
                _DOB = value;
                RaisePropertyChanged("DOB");
            }
        }

        private string _contactDOB = string.Empty;
        public string ContactDOB
        {
            get
            {
                return _contactDOB;
            }
            set
            {
                _contactDOB = value;
                RaisePropertyChanged("ContactDOB");
            }
        }

        private int _maxLengthID = 10;
        public int MaxLengthID
        {
            get
            {
                return _maxLengthID;
            }
            set
            {
                _maxLengthID = value;
                RaisePropertyChanged("MaxLengthID");
            }
        }
        private int _maxLengthIDSR = 10;
        public int MaxLengthIDSR
        {
            get
            {
                return _maxLengthIDSR;
            }
            set
            {
                _maxLengthIDSR = value;
                RaisePropertyChanged("MaxLengthIDSR");
            }
        }
        private bool _frameContactIDError = false;
        public bool FrameContactIDError
        {
            get
            {
                return _frameContactIDError;
            }
            set
            {
                _frameContactIDError = value;
                RaisePropertyChanged("FrameContactIDError");
            }
        }

        private bool _frameIDError = false;
        public bool FrameIDError
        {
            get
            {
                return _frameIDError;
            }
            set
            {
                _frameIDError = value;
                RaisePropertyChanged("FrameIDError");
            }
        }
        private bool _frameContactDOBError = false;
        public bool FrameContactDOBError
        {
            get
            {
                return _frameContactDOBError;
            }
            set
            {
                _frameContactDOBError = value;
                RaisePropertyChanged("FrameContactDOBError");
            }
        }
        private bool _frameDOBError = false;
        public bool FrameDOBError
        {
            get
            {
                return _frameDOBError;
            }
            set
            {
                _frameDOBError = value;
                RaisePropertyChanged("FrameDOBError");
            }
        }
        private bool _isInstrunctionVisible = false;
        public bool IsInstrunctionVisible
        {
            get
            {
                return _isInstrunctionVisible;
            }
            set
            {
                _isInstrunctionVisible = value;
                if (_isInstrunctionVisible)
                {
                    IsBackStepButtonVisible = false;
                }
                else
                {
                    IsBackStepButtonVisible = true;
                }
                RaisePropertyChanged("IsInstrunctionVisible");
            }
        }
        private bool _isTaxPayersVisible = false;
        public bool IsTaxPayersVisible
        {
            get
            {
                return _isTaxPayersVisible;
            }
            set
            {
                _isTaxPayersVisible = value;
                RaisePropertyChanged("IsTaxPayersVisible");
            }
        }
        private bool _isSalesVisible = false;
        public bool IsSalesVisible
        {
            get
            {
                return _isSalesVisible;
            }
            set
            {
                _isSalesVisible = value;
                RaisePropertyChanged("IsSalesVisible");
            }
        }
        private bool _isExpensesVisible = false;
        public bool IsExpensesVisible
        {
            get
            {
                return _isExpensesVisible;
            }
            set
            {
                _isExpensesVisible = value;
                RaisePropertyChanged("IsExpensesVisible");
            }
        }
        private bool _isFinancialVisible = false;
        public bool IsFinancialVisible
        {
            get
            {
                return _isFinancialVisible;
            }
            set
            {
                _isFinancialVisible = value;
                if (_isFinancialVisible)
                {
                    IsContinueButtonEnable = true;
                }
                RaisePropertyChanged("IsFinancialVisible");
            }
        }

        private bool _isNewFinancialRepVisible;
        public bool IsNewFinancialRepVisible
        {
            get => _isNewFinancialRepVisible;
            set
            {
                _isNewFinancialRepVisible = value;
                RaisePropertyChanged(nameof(IsNewFinancialRepVisible));
            }
        }
        private bool _isDeclarationDOBVisible;
        public bool IsDeclarationDOBVisible
        {
            get => _isDeclarationDOBVisible;
            set
            {
                _isDeclarationDOBVisible = value;
                RaisePropertyChanged(nameof(IsDeclarationDOBVisible));
            }
        }
        private bool _isTaxPayerIBANEnabled;
        public bool IsTaxPayerIBANEnabled
        {
            get => _isTaxPayerIBANEnabled;
            set
            {
                _isTaxPayerIBANEnabled = value;
                RaisePropertyChanged(nameof(IsTaxPayerIBANEnabled));
            }
        }
        private bool _isTaxPayerEligDateEnabled;
        public bool IsTaxPayerEligDateEnabled
        {
            get => _isTaxPayerEligDateEnabled;
            set
            {
                _isTaxPayerEligDateEnabled = value;
                RaisePropertyChanged(nameof(IsTaxPayerEligDateEnabled));
            }
        }
        private bool _isFDChangeSectionEnabled;
        public bool IsFDChangeSectionEnabled
        {
            get => _isFDChangeSectionEnabled;
            set
            {
                _isFDChangeSectionEnabled = value;
                RaisePropertyChanged(nameof(IsFDChangeSectionEnabled));
            }
        }
        private bool _isSummaryVisible = false;
        public bool IsSummaryVisible
        {
            get
            {
                return _isSummaryVisible;
            }
            set
            {
                _isSummaryVisible = value;
                RaisePropertyChanged("IsSummaryVisible");
            }
        }

        private String _currentStep;
        public String CurrentStep
        {
            get
            {
                return _currentStep;
            }
            set
            {
                _currentStep = value;
                RaisePropertyChanged("CurrentStep");
            }
        }

        private bool _isLoading;
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

        private List<String> _ListOfActionButtonsApplicableForRegistration;
        public List<String> ListOfActionButtonsApplicableForRegistration
        {
            get
            {
                return _ListOfActionButtonsApplicableForRegistration;
            }
            set
            {
                _ListOfActionButtonsApplicableForRegistration = value;
                if (_ListOfActionButtonsApplicableForRegistration != null && _ListOfActionButtonsApplicableForRegistration.Count() != 0)
                {
                    // OnMoreOptionsEnabled = true;
                }
                else
                {
                    //OnMoreOptionsEnabled = false;
                }
                RaisePropertyChanged("ListOfActionButtonsApplicableForRegistration");
            }
        }

        private string _regTypeCode = string.Empty;
        public string RegTypeCode
        {
            get
            {
                return _regTypeCode;
            }
            set
            {
                _regTypeCode = value;
                RaisePropertyChanged("RegTypeCode");
            }
        }

        private VATRegistrationDetails _vATRegistrationDetailsData;
        public VATRegistrationDetails VATRegistrationDetailsData
        {
            get
            {
                return _vATRegistrationDetailsData;
            }
            set
            {
                _vATRegistrationDetailsData = value;
                RaisePropertyChanged("VATRegistrationDetailsData");
            }
        }


        private VATRegistrationDetails _originalVATRegistrationDetailsData;
        public VATRegistrationDetails OriginalVATRegistrationDetailsData
        {
            get
            {
                return _originalVATRegistrationDetailsData;
            }
            set
            {
                _originalVATRegistrationDetailsData = value;
                RaisePropertyChanged("OriginalVATRegistrationDetailsData");
            }
        }


        private Double _minimumDisplayValueOfSlider1 = 0.0;
        public Double MinimumDisplayValueOfSlider1
        {
            get
            {
                return _minimumDisplayValueOfSlider1;
            }
            set
            {
                _minimumDisplayValueOfSlider1 = value;
                RaisePropertyChanged("MinimumDisplayValueOfSlider1");
            }
        }

        private Double _maximumDisplayValueOfSlider1 = 1.0;
        public Double MaximumDisplayValueOfSlider1
        {
            get
            {
                return _maximumDisplayValueOfSlider1;
            }
            set
            {
                _maximumDisplayValueOfSlider1 = value;
                RaisePropertyChanged("MaximumDisplayValueOfSlider1");
            }
        }

        private Double _maximumDisplayValueOfSlider2 = 1.0;
        public Double MaximumDisplayValueOfSlider2
        {
            get
            {
                return _maximumDisplayValueOfSlider2;
            }
            set
            {
                _maximumDisplayValueOfSlider2 = value;
                RaisePropertyChanged("MaximumDisplayValueOfSlider2");
            }
        }

        private Double _minimumDisplayValueOfSlider2 = 0.0;
        public Double MinimumDisplayValueOfSlider2
        {
            get
            {
                return _minimumDisplayValueOfSlider2;
            }
            set
            {
                _minimumDisplayValueOfSlider2 = value;
                RaisePropertyChanged("MinimumDisplayValueOfSlider2");
            }
        }

        private Double _maximumValueOfSlider2 = 1.0;
        public Double MaximumValueOfSlider2
        {
            get
            {
                return _maximumValueOfSlider2;
            }
            set
            {
                _maximumValueOfSlider2 = value;
                RaisePropertyChanged("MaximumValueOfSlider2");
            }
        }

        private Double _maximumValueOfSlider1 = 1.0;
        public Double MaximumValueOfSlider1
        {
            get
            {
                return _maximumValueOfSlider1;
            }
            set
            {
                _maximumValueOfSlider1 = value;
                RaisePropertyChanged("MaximumValueOfSlider1");
            }
        }

        private string _sliderLable1EligibilityText;
        public string SliderLable1EligibilityText
        {
            get
            {
                return _sliderLable1EligibilityText;
            }
            set
            {
                _sliderLable1EligibilityText = value;

                RaisePropertyChanged("SliderLable1EligibilityText");
            }
        }
        private string _sliderLable1;
        public string SliderLable1
        {
            get
            {
                return _sliderLable1;
            }
            set
            {
                _sliderLable1 = value;
                RaisePropertyChanged("SliderLable1");
            }
        }
        private string _sliderLable2;
        public string SliderLable2
        {
            get
            {
                return _sliderLable2;
            }
            set
            {
                _sliderLable2 = value;
                RaisePropertyChanged("SliderLable2");
            }
        }
        private Double _sliderCurrentValue1 = 0.0;
        public Double SliderCurrentValue1
        {
            get
            {
                return _sliderCurrentValue1;
            }
            set
            {
                _sliderCurrentValue1 = value;
                RaisePropertyChanged("SliderCurrentValue1");
            }
        }

        private Double _sliderCurrentValue2 = 0.0;
        public Double SliderCurrentValue2
        {
            get
            {
                return _sliderCurrentValue2;
            }
            set
            {
                _sliderCurrentValue2 = value;
                RaisePropertyChanged("SliderCurrentValue2");
            }
        }

        private List<QuestionNumberWithMinMaxRange> _minMaxRanges;
        public List<QuestionNumberWithMinMaxRange> MinMaxRanges
        {
            get
            {
                return _minMaxRanges;
            }
            set
            {
                _minMaxRanges = value;
                RaisePropertyChanged("MinMaxRanges");
            }
        }



        private VATRegistrationOtherDetails _vATRegistrationOtherDetails;
        public VATRegistrationOtherDetails VATRegistrationOtherDetails
        {
            get
            {
                return _vATRegistrationOtherDetails;
            }
            set
            {
                _vATRegistrationOtherDetails = value;
                RaisePropertyChanged("VATRegistrationOtherDetails");
            }
        }

        private bool _isInstrunctionChecked;
        public bool IsInstrunctionChecked
        {
            get
            {
                return _isInstrunctionChecked;
            }
            set
            {
                _isInstrunctionChecked = value;
                if (_isInstrunctionChecked != null)
                {
                    if (_isInstrunctionChecked)
                    {
                        IsContinueButtonEnable = true;
                    }
                    else
                    {
                        IsContinueButtonEnable = false;
                    }
                }
                RaisePropertyChanged("IsInstrunctionChecked");
            }
        }

        private bool _isDeclarationChecked;
        public bool IsDeclarationChecked
        {
            get
            {
                return _isDeclarationChecked;
            }
            set
            {
                _isDeclarationChecked = value;
                if (_isDeclarationChecked != null)
                {
                    if (_isDeclarationChecked)
                    {
                        IsContinueButtonEnable = true;
                    }
                    else
                    {
                        IsContinueButtonEnable = false;
                    }
                }
                RaisePropertyChanged("IsDeclarationChecked");
            }
        }
        private bool _isAddAdditionalInfoChecked = false;
        public bool IsAddAdditionalInfoChecked
        {
            get
            {
                return _isAddAdditionalInfoChecked;
            }
            set
            {
                _isAddAdditionalInfoChecked = value;
         
                RaisePropertyChanged("IsAddAdditionalInfoChecked");
            }
        }
        private bool _isFDChangeSectionChecked = false;
        public bool IsFDChangeSectionChecked
        {
            get
            {
                return _isFDChangeSectionChecked;
            }
            set
            {
                _isFDChangeSectionChecked = value;

                RaisePropertyChanged("IsFDChangeSectionChecked");
            }
        }
        private bool _isAddNewRepresentativeChecked= false;
        public bool IsAddNewRepresentativeChecked
        {
            get
            {
                return _isAddNewRepresentativeChecked;
            }
            set
            {
                _isAddNewRepresentativeChecked = value;

                RaisePropertyChanged("IsAddNewRepresentativeChecked");
            }
        }

        private bool _isResident;
        public bool IsResident
        {
            get
            {
                return _isResident;
            }
            set
            {
                _isResident = value;
                RaisePropertyChanged("IsResident");
            }
        }

        private string _attachments;
        public string Attachments
        {
            get
            {
                return _attachments;
            }
            set
            {
                _attachments = value;
                RaisePropertyChanged("Attachments");
            }
        }

        private ResultsItem _aDDRESSSetData;
        public ResultsItem ADDRESSSetData
        {
            get
            {
                return _aDDRESSSetData;
            }
            set
            {
                _aDDRESSSetData = value;
                RaisePropertyChanged("ADDRESSSetData");
            }
        }

        private String _addressLineOne;
        public String AddressLineOne
        {
            get
            {
                return _addressLineOne;
            }
            set
            {
                _addressLineOne = value;
                RaisePropertyChanged("AddressLineOne");
            }
        }

        private String _addressLineTwo;
        public String AddressLineTwo
        {
            get
            {
                return _addressLineTwo;
            }
            set
            {
                _addressLineTwo = value;
                RaisePropertyChanged("AddressLineTwo");
            }
        }

        private String _vatEligibleStartDate = string.Empty;
        public String VatEligibleStartDate
        {
            get
            {
                return _vatEligibleStartDate;
            }
            set
            {
                _vatEligibleStartDate = value;
                RaisePropertyChanged("VatEligibleStartDate");
            }
        }

        private String _gpartFR = string.Empty;
        public String GpartFR
        {
            get
            {
                return _gpartFR;
            }
            set
            {
                _gpartFR = value;
                RaisePropertyChanged("GpartFR");
            }
        }
        private String _typeFR = string.Empty;
        public String TypeFR
        {
            get
            {
                return _typeFR;
            }
            set
            {
                _typeFR = value;
                RaisePropertyChanged("TypeFR");
            }
        }

        private String _idnumberFR = string.Empty;
        public String IdnumberFR
        {
            get
            {
                return _idnumberFR;
            }
            set
            {
                _idnumberFR = value;
                RaisePropertyChanged("IdnumberFR");
            }
        }

        private String _firstnmFR = string.Empty;
        public String FirstnmFR
        {
            get
            {
                return _firstnmFR;
            }
            set
            {
                _firstnmFR = value;
                RaisePropertyChanged("FirstnmFR");
            }
        }

        private String _lastnmFR = string.Empty;
        public String LastnmFR
        {
            get
            {
                return _lastnmFR;
            }
            set
            {
                _lastnmFR = value;
                RaisePropertyChanged("LastnmFR");
            }
        }

        private String _mobNumberFR = string.Empty;
        public String MobNumberFR
        {
            get
            {
                return _mobNumberFR;
            }
            set
            {
                _mobNumberFR = value;
                RaisePropertyChanged("MobNumberFR");
            }
        }

        private String _idNumberSR = string.Empty;
        public String IdNumberSR
        {
            get
            {
                return _idNumberSR;
            }
            set
            {
                _idNumberSR = value;
                RaisePropertyChanged("IdNumberSR");
            }
        }

        private String _firstNameSR = string.Empty;
        public String FirstNameSR
        {
            get
            {
                return _firstNameSR;
            }
            set
            {
                _firstNameSR = value;
                RaisePropertyChanged("FirstNameSR");
            }
        }

        private String _smtpAddrFR = string.Empty;
        public String SmtpAddrFR
        {
            get
            {
                return _smtpAddrFR;
            }
            set
            {
                _smtpAddrFR = value;
                RaisePropertyChanged("SmtpAddrFR");
            }
        }

        private List<SignUpIdType> _idTypeListFR;
        public List<SignUpIdType> IdTypeListFR
        {
            get
            {
                return _idTypeListFR;
            }
            set
            {
                _idTypeListFR = value;
                RaisePropertyChanged("IdTypeListFR");
            }
        }

        private List<SignUpIdType> _idTypeListSR;
        public List<SignUpIdType> IdTypeListSR
        {
            get
            {
                return _idTypeListSR;
            }
            set
            {
                _idTypeListSR = value;
                RaisePropertyChanged("IdTypeListSR");
            }
        }

        public int _iDTypeIndexFR = 0;
        public int IDTypeIndexFR
        {
            get
            {
                return _iDTypeIndexFR;
            }
            set
            {
                _iDTypeIndexFR = value;
                RaisePropertyChanged("IDTypeIndexFR");
            }
        }

        public int _iDTypeIndexSR = 0;
        public int IDTypeIndexSR
        {
            get
            {
                return _iDTypeIndexSR;
            }
            set
            {
                _iDTypeIndexSR = value;
                RaisePropertyChanged("IDTypeIndexSR");
            }
        }
        private string _txtIDType = string.Empty;
        public string TxtIDType
        {
            get
            {
                return _txtIDType;
            }
            set
            {
                _txtIDType = value;
                RaisePropertyChanged("TxtIDType");
            }
        }

        private string _txtIDTypeFR = string.Empty;
        public string TxtIDTypeFR
        {
            get
            {
                return _txtIDTypeFR;
            }
            set
            {
                _txtIDTypeFR = value;
                RaisePropertyChanged("TxtIDTypeFR");
            }
        }

        private string _txtIDTypeSR = string.Empty;
        public string TxtIDTypeSR
        {
            get
            {
                return _txtIDTypeSR;
            }
            set
            {
                _txtIDTypeSR = value;
                RaisePropertyChanged("TxtIDTypeSR");
            }
        }



        private SignUpIdType _selectedIdTypeFR = null;
        public SignUpIdType SelectedIdTypeFR
        {
            get
            {
                return _selectedIdTypeFR;
            }
            set
            {
                _selectedIdTypeFR = value;
                if (_selectedIdTypeFR != null)
                {
                    TxtIDTypeFR = _selectedIdTypeFR.Name;
                    try
                    {
                        if (_selectedIdTypeFR.ID.Equals("ZS0001"))
                        {
                            MaxLengthID = 10;
                            IsFDNameMobEmailEnable = false;

                        }
                        else if (_selectedIdTypeFR.ID.Equals("ZS0002"))
                        {
                            MaxLengthID = 10;
                            IsFDNameMobEmailEnable = false;
                        }

                        else if (_selectedIdTypeFR.ID.Equals("ZS0003"))
                        {
                            MaxLengthID = 15;
                            IsFDNameMobEmailEnable = true;

                        }
                        TxtIDTypeFR = _selectedIdTypeFR.Name;
                    }
                    catch (Exception Ex)
                    {
                    }

                }
                RaisePropertyChanged("SelectedIdTypeFR");
            }
        }

        private SignUpIdType _selectedIdTypeSR = null;
        public SignUpIdType SelectedIdTypeSR
        {
            get
            {
                return _selectedIdTypeSR;
            }
            set
            {
                _selectedIdTypeSR = value;
                if (_selectedIdTypeSR != null)
                {



                    try
                    {
                        TxtIDTypeSR = _selectedIdTypeSR.Name;
                        if (_selectedIdTypeSR.ID.Equals("ZS0001"))
                        {
                            MaxLengthIDSR = 10;

                        }
                        else if (_selectedIdTypeSR.ID.Equals("ZS0002"))
                        {
                            MaxLengthIDSR = 10;
                        }

                        else if (_selectedIdTypeSR.ID.Equals("ZS0003"))
                        {
                            MaxLengthIDSR = 15;

                        }

                    }
                    catch

                    { }
                }
                RaisePropertyChanged("SelectedIdTypeSR");
            }
        }

        private string _importerImageSource = null;
        public string ImporterImageSource
        {
            get
            {
                return _importerImageSource;
            }
            set
            {
                _importerImageSource = value;
                RaisePropertyChanged("ImporterImageSource");
            }
        }

        private Color _importerTextColor;
        public Color ImporterTextColor
        {
            get
            {
                return _importerTextColor;
            }
            set
            {
                _importerTextColor = value;
                RaisePropertyChanged("ImporterTextColor");
            }
        }

        private Color _exporterTextColor;
        public Color ExporterTextColor
        {
            get
            {
                return _exporterTextColor;
            }
            set
            {
                _exporterTextColor = value;
                RaisePropertyChanged("ExporterTextColor");
            }
        }



        private string _exporterImageSource = null;
        public string ExporterImageSource
        {
            get
            {
                return _exporterImageSource;
            }
            set
            {
                _exporterImageSource = value;
                RaisePropertyChanged("ExporterImageSource");
            }
        }

        private string _selectedIban;
        public string SelectedIban
        {
            get
            {
                return _selectedIban;
            }
            set
            {
                _selectedIban = value;
                RaisePropertyChanged("SelectedIban");
            }
        }

        private ObservableCollection<Result2> _ibanList;
        public ObservableCollection<Result2> IbanList
        {
            get
            {
                return _ibanList;
            }
            set
            {
                _ibanList = value;
                RaisePropertyChanged("IbanList");
            }
        }

        private bool _isNewAccountClicked;
        public bool IsNewAccountClicked
        {
            get
            {
                return _isNewAccountClicked;
            }
            set
            {
                _isNewAccountClicked = value;
                RaisePropertyChanged("IsNewAccountClicked");
            }
        }

        private string _isNewAccountText;
        public string NewAccountText

        {
            get
            {
                return _isNewAccountText;
            }
            set
            {
                _isNewAccountText = value;
                RaisePropertyChanged("NewAccountText");
            }
        }

        private String _textQuestion3First = string.Empty;
        public String TextQuestion3First
        {
            get
            {
                return _textQuestion3First;
            }
            set
            {
                _textQuestion3First = value;
                RaisePropertyChanged("TextQuestion3First");
            }
        }

        private Color _textQuestion3FirstTextColor;
        public Color TextQuestion3FirstTextColor
        {
            get
            {
                return _textQuestion3FirstTextColor;
            }
            set
            {
                _textQuestion3FirstTextColor = value;
                RaisePropertyChanged("TextQuestion3FirstTextColor");
            }
        }

        private String _textQuestion4First = string.Empty;
        public String TextQuestion4First
        {
            get
            {
                return _textQuestion4First;
            }
            set
            {
                _textQuestion4First = value;
                RaisePropertyChanged("TextQuestion4First");
            }
        }

        private Color _textQuestion4FirstTextColor;
        public Color TextQuestion4FirstTextColor
        {
            get
            {
                return _textQuestion4FirstTextColor;
            }
            set
            {
                _textQuestion4FirstTextColor = value;
                RaisePropertyChanged("TextQuestion4FirstTextColor");
            }
        }

        private String _textQuestion3Second = string.Empty;
        public String TextQuestion3Second
        {
            get
            {
                return _textQuestion3Second;
            }
            set
            {
                _textQuestion3Second = value;
                RaisePropertyChanged("TextQuestion3Second");
            }
        }

        private Color _textQuestion3SecondTextColor;
        public Color TextQuestion3SecondTextColor
        {
            get
            {
                return _textQuestion3SecondTextColor;
            }
            set
            {
                _textQuestion3SecondTextColor = value;
                RaisePropertyChanged("TextQuestion3SecondTextColor");
            }
        }

        private String _textQuestion4Second = string.Empty;
        public String TextQuestion4Second
        {
            get
            {
                return _textQuestion4Second;
            }
            set
            {
                _textQuestion4Second = value;
                RaisePropertyChanged("TextQuestion4Second");
            }
        }

        private Color _textQuestion4SecondTextColor;
        public Color TextQuestion4SecondTextColor
        {
            get
            {
                return _textQuestion4SecondTextColor;
            }
            set
            {
                _textQuestion4SecondTextColor = value;
                RaisePropertyChanged("TextQuestion4SecondTextColor");
            }
        }

        private String _imageforTextQuestion3First = string.Empty;
        public String ImageforTextQuestion3First
        {
            get
            {
                return _imageforTextQuestion3First;
            }
            set
            {
                _imageforTextQuestion3First = value;
                RaisePropertyChanged("ImageforTextQuestion3First");
            }
        }
        private String _imageforTextQuestion3Second = string.Empty;
        public String ImageforTextQuestion3Second
        {
            get
            {
                return _imageforTextQuestion3Second;
            }
            set
            {
                _imageforTextQuestion3Second = value;
                RaisePropertyChanged("ImageforTextQuestion3Second");
            }
        }
        private String _imageforTextQuestion4First = string.Empty;
        public String ImageforTextQuestion4First
        {
            get
            {
                return _imageforTextQuestion4First;
            }
            set
            {
                _imageforTextQuestion4First = value;
                RaisePropertyChanged("ImageforTextQuestion4First");
            }
        }

        private String _imageforTextQuestion4Second = string.Empty;
        public String ImageforTextQuestion4Second
        {
            get
            {
                return _imageforTextQuestion4Second;
            }
            set
            {
                _imageforTextQuestion4Second = value;
                RaisePropertyChanged("ImageforTextQuestion4Second");
            }
        }

        private List<FinancialRepresentativesModel> _listFinanceRepresenatives { get; set; }
        public List<FinancialRepresentativesModel> ListFinanceRepresenatives
        {
            get => _listFinanceRepresenatives;
            set
            {
                _listFinanceRepresenatives = value;
                RaisePropertyChanged(nameof(ListFinanceRepresenatives));
            }
        }

        #region Item Availability Properties

        private InstAndConditionAvailability _instAndCondition = new InstAndConditionAvailability();
        public InstAndConditionAvailability InstAndCondition
        {
            get
            {
                _instAndCondition.CBAgreeCondition = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                return _instAndCondition;
            }
        }
        private TaxPayer_DetailsAvailability _taxPayerDetails = new TaxPayer_DetailsAvailability();
        public TaxPayer_DetailsAvailability TaxPayerDetails
        {
            get
            {
                _taxPayerDetails.Parent = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.TaxPayerDetailsParent = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;

                _taxPayerDetails.TinEntry1 = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.TinEntry2 = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.MainOutletEntry1 = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.MainOutletEntry2 = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.StartDateEntry = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.AddressEntry1 = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.AddressEntry2 = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.SourceEntry = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;

                _taxPayerDetails.AddInformationCB = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : false;
                _taxPayerDetails.AddInformationCBVisible = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : false;
                _taxPayerDetails.AddInformationParent = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                IsTaxPayerIBANEnabled = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                IsTaxPayerEligDateEnabled = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? true : true;

                _taxPayerDetails.ImporterYesRB = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.ImporterNoRB = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.ImporterAttachmentsBtn = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.ExporterYesRB = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.ExporterNoRB = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.ExporterrAttachmentsBtn = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.ExistingIBANPicker = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.NewIBANPicker = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _taxPayerDetails.CommencementDate = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? true : true;
                return _taxPayerDetails;
            }
        }
        private FinancialDetailsAvailability _financialDetails = new FinancialDetailsAvailability();
        public FinancialDetailsAvailability FinancialDetails
        {
            get
            {
                _financialDetails.Parent = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialDetails.VATEligibilityPoint1Parent = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialDetails.VATEligibilityPoint2Parent = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialDetails.VATEligibilityPoint3Parent = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialDetails.VATEligibilityPoint4Parent = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialDetails.AttachSectionCB = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : false;
                _financialDetails.AttachSectionCBVisible = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : false;
                IsFDChangeSectionEnabled = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialDetails.AttachSectionAddNewType = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                return _financialDetails;
            }
        }
        private FinancialRepresentativeAvailability _financialRepresentative = new FinancialRepresentativeAvailability();
        public FinancialRepresentativeAvailability FinancialRepresentative
        {
            get
            {
                _financialRepresentative.Parent = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialRepresentative.ChangeMobileEmailCB = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialRepresentative.AddNewFinRepresentativeCB = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : false;
                _financialRepresentative.AddNewFinRepCBVisible = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : false;
                IsNewFinancialRepVisible = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialRepresentative.SkipBtn = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;


                _financialRepresentative.TinEntry = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialRepresentative.IDTypeEntry = App.VATType == Enums.PageExecutionType.Amend ? true : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialRepresentative.IDNoEntry = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialRepresentative.FNameEntry = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                _financialRepresentative.SurnameEntry = IsFDNameMobEmailEnable && (App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true);
                _financialRepresentative.MobileNoEntry = IsFDNameMobEmailEnable && (App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true);
                _financialRepresentative.EmailIDEntry = IsFDNameMobEmailEnable && (App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true);

                return _financialRepresentative;
            }
        }
        private DeclarationAvailability _declaration = new DeclarationAvailability();
        public DeclarationAvailability Declaration
        {
            get
            {
                _declaration.Parent = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? true : true;
                _declaration.AcknowledgementCB = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? true : true;
                _declaration.IDTypeOrNoPicker = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? true : true;
                _declaration.IDTypeOrNoEntry = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? true : true;
                _declaration.DOBEntry = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : true;
                IsDeclarationDOBVisible = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? false : false;
                _declaration.ContactNameEntry = App.VATType == Enums.PageExecutionType.Amend ? false : App.VATType == Enums.PageExecutionType.Reactivation ? true : true;
                return _declaration;
            }
        }

        #endregion


        #endregion

        public VATAmendReactivationPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

        }

        #region Method


        void SetUIAvailability()
        {
            // to be added, currently not using
            switch (App.VATType)
            {
                case Enums.PageExecutionType.Amend:
                    InstAndCondition.CBAgreeCondition = false;
                    break;
                case Enums.PageExecutionType.Reactivation:
                    InstAndCondition.CBAgreeCondition = true;
                    break;
                default:
                    InstAndCondition.CBAgreeCondition = true;
                    break;

            }
        }
        public void setDATA()
        {
            try
            {
                if (IsInstrunctionChecked)
                {
                    VATRegistrationDetailsData.d.AgrFg = "1";
                }
                else
                {
                    VATRegistrationDetailsData.d.AgrFg = "0";
                }
                var Bdt = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString() + "T00:00:00";
                if (!string.IsNullOrEmpty(VatEligibleStartDate))
                {
                    //var dateTime = new DateTime(year, month, day, 10, 2, 0, DateTimeKind.Local);
                    //var dateTimeOffset = new DateTimeOffset(dateTime);
                    //var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();
                    //var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();
                    // Int32 unixTimestamp = (Int32)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    string[] date1 = VatEligibleStartDate.Split('/');
                    Bdt = date1[2] + "-" + date1[1] + "-" + date1[0] + "T00:00:00";

                }
                VATRegistrationDetailsData.d.VatTaxDt = Bdt;
                VATRegistrationDetailsData.d.StepNumberz = "2";
              //  VATRegistrationDetailsData.d.DecidTy = string.Empty;
                //Step 4

                VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Gpart = GpartFR;
                if (SelectedIdTypeFR != null)
                    VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Type = SelectedIdTypeFR.ID;
                VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Idnumber = IdnumberFR;
                VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Firstnm = FirstnmFR;
                VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Lastnm = LastnmFR;
                VATRegistrationDetailsData.d.CONTACTDTSet.results[0].MobNumber = MobNumberFR;
                VATRegistrationDetailsData.d.CONTACTDTSet.results[0].SmtpAddr = SmtpAddrFR;

                //Step 5
                if (IsDeclarationChecked)
                {
                    VATRegistrationDetailsData.d.Decfg = "1";
                }
                else
                {
                    VATRegistrationDetailsData.d.Decfg = "0";
                }
                if (IsAddAdditionalInfoChecked)
                {
                    VATRegistrationDetailsData.d.Stp2Cbbox = "1";
                }
                else 
                {
                    VATRegistrationDetailsData.d.Stp2Cbbox = "0";
                }
                if (IsFDChangeSectionChecked)
                {
                    VATRegistrationDetailsData.d.Stp3Cbbox = "1";
                }
                else 
                {
                    VATRegistrationDetailsData.d.Stp3Cbbox = "0";
                }
                if (IsAddNewRepresentativeChecked)
                {
                    VATRegistrationDetailsData.d.Stp4Cbbox2 = "1";
                }
                else 
                {
                    VATRegistrationDetailsData.d.Stp4Cbbox2 = "0";
                }
                if (SelectedIdTypeSR != null)
                {
                    VATRegistrationDetailsData.d.DecidTy = SelectedIdTypeSR.ID;
                }

                VATRegistrationDetailsData.d.DecidNo = IdNumberSR;
                VATRegistrationDetailsData.d.Decname = FirstNameSR;


            }
            catch (Exception ex)
            {

            }
        }

        public async Task<VATRegistrationDetails> SubmitClicked()
        {
            VATRegistrationDetails response = new VATRegistrationDetails();
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                setDATA();
                ATTDETSet ATTDETSetnew = new ATTDETSet();
                ATTDETSetnew = VATRegistrationDetailsData.d.ATTDETSet;
                //VATRegistrationDetails vATRegistrationDetails = new VATRegistrationDetails();
                response = await WebServiceManager.SaveVATRegistrationData(VATRegistrationDetailsData);
                PopToRootPage();
                if (response != null && response.d != null)
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {
                            if (response.d.Operationz.Equals("04"))
                            {
                                string number = response.d.Fbnumz;
                                string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                                //await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));
                                _navigationService.GoBack();
                            }
                            if (response.d.Operationz.Equals("05"))
                            {
                                //  string number = response.d.Fbnumz;
                                string displayMessage = AppResources.VATRSaveasdraftMessage;
                                //await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));
                            }



                            VATRegistrationDetailsData = response;
                            VATRegistrationDetailsData.d.ATTDETSet = ATTDETSetnew;
                            VATRegistrationDetailsData.d.ATTDETSet = ATTDETSetObject;
                            //Set data after api call 
                            setDataAfterSubmitAPIAsync(response);

                        }
                        IsLoading = false;
                        return response;

                    }
                    catch (Exception ex)
                    {
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
                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    //_navigationService.GoBack();

                });
                return response;
            }

            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task setDataAfterSubmitAPIAsync(VATRegistrationDetails vATRegistration)
        {
            //Set applicable buttons
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                VATRegistrationOtherDetails vATRegistrationOther = await WebServiceManager.GAZTGetVATRegistrationDataWithButtons(vATRegistration.d.Fbnumz, vATRegistration.d.Officerz, vATRegistration.d.Statusz, vATRegistration.d.TxnTpz, "ZTAX_VT_REG");

                PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                if (vATRegistrationOther != null && vATRegistrationOther.d != null)
                {
                    VATRegistrationOtherDetails = vATRegistrationOther;

                    SetApplicableButtons();
                }
                IsLoading = false;
            });
        }

        public void setQuestionImage()
        {
            if (VATRegistrationDetailsData.d.QUESTIONSSet.results != null)
            {

                string value1forimage3first = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(x => x.QueNo == "003" && x.QoptNo == "031").Select(x => x.QoptAns).FirstOrDefault();
                string value2forimage3second = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(x => x.QueNo == "003" && x.QoptNo == "032").Select(x => x.QoptAns).FirstOrDefault();

                string value1forimage4first = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(x => x.QueNo == "004" && x.QoptNo == "041").Select(x => x.QoptAns).FirstOrDefault();
                string value2forimage4second = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(x => x.QueNo == "004" && x.QoptNo == "042").Select(x => x.QoptAns).FirstOrDefault();

                if (value1forimage3first == "1")
                {
                    quesTion3answerSelected = TextQuestion3First;
                    ImageforTextQuestion3First = "vat_tile_IbanCard_background.png";
                    TextQuestion3FirstTextColor = Color.White;
                }
                else
                {
                    ImageforTextQuestion3First = "vat_tile_IbanCard_background_white.png";
                    TextQuestion3FirstTextColor = Color.Black;
                }

                if (value2forimage3second == "1")
                {
                    quesTion3answerSelected = TextQuestion3Second;
                    ImageforTextQuestion3Second = "vat_tile_IbanCard_background.png";
                    TextQuestion3SecondTextColor = Color.White;
                }
                else
                {
                    ImageforTextQuestion3Second = "vat_tile_IbanCard_background_white.png";
                    TextQuestion3SecondTextColor = Color.Black;
                }

                if (value1forimage4first == "1")
                {
                    quesTion4answerSelected = TextQuestion4First;
                    ImageforTextQuestion4First = "vat_tile_IbanCard_background.png";
                    TextQuestion4FirstTextColor = Color.White;
                }
                else
                {
                    ImageforTextQuestion4First = "vat_tile_IbanCard_background_white.png";
                    TextQuestion4FirstTextColor = Color.Black;
                }

                if (value2forimage4second == "1")
                {
                    quesTion4answerSelected = TextQuestion4Second;
                    ImageforTextQuestion4Second = "vat_tile_IbanCard_background.png";
                    TextQuestion4SecondTextColor = Color.White;
                }
                else
                {
                    ImageforTextQuestion4Second = "vat_tile_IbanCard_background_white.png";
                    TextQuestion4SecondTextColor = Color.Black;
                }
            }

        }

        public void SetVisibility()
        {
            IsInstrunctionVisible = false;
            IsTaxPayersVisible = false;
            IsSalesVisible = false;
            IsExpensesVisible = false;
            IsFinancialVisible = false;
            IsSummaryVisible = false;
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

        private String GetLocalisedButtonString(String ButtonName)
        {
            String LocalisedButtonString = String.Empty;

            //if (0 == String.Compare(ButtonName, "Submit"))
            //{
            //    LocalisedButtonString = AppResources.Submit;
            //}

            if (0 == String.Compare(ButtonName, "SaveasDraft"))
            {
                LocalisedButtonString = AppResources.ZZSaveAsDraft;
            }
            else if (0 == String.Compare(ButtonName, "Attachments"))
            {
                LocalisedButtonString = AppResources.Attachments;
            }
            else if (0 == String.Compare(ButtonName, "Void"))
            {
                LocalisedButtonString = AppResources.ZZVoid;
            }
            //else if (0 == String.Compare(ButtonName, "Validate"))
            //{
            //    LocalisedButtonString = AppResources.ZZValidate;
            //}            

            return LocalisedButtonString;
        }

        public async Task onPageLoad()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    GetSignUpIdType();
                    IsLoading = true;
                    VATRegistrationDetailsData = null;
                    VATRegistrationOtherDetails = null;
                    ADDRESSSetData = null;
                    VATRegistrationDetails vATRegistration = null;
                    VATRegistrationOtherDetails vATRegistrationOther = null;
                    try
                    {
                        string pageType = string.Empty;
                        if (App.VATType == Enums.PageExecutionType.Amend)
                            pageType = "05";
                        else if (App.VATType == Enums.PageExecutionType.Reactivation)
                            pageType = "07";
                        else
                            pageType = "04";

                        vATRegistration = await WebServiceManager.GAZTGetVATRegistrationData(pageType);
                        //OriginalVATRegistrationDetailsData = await WebServiceManager.GAZTGetVATRegistrationData(pageType);



                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                        if (vATRegistration != null && vATRegistration.d != null)
                        {

                            //step 4 and 5 data set
                            if (vATRegistration.d.CONTACT_PERSONSet != null)
                            {
                                ListFinanceRepresenatives = new List<FinancialRepresentativesModel>();
                                GpartFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Gpart;
                                //  VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Type = SelectedIdTypeFR.ID;
                                idnumber = string.Empty;
                                idnumber = vATRegistration.d.CONTACT_PERSONSet.results[0].Idnumber;
                                FirstnmFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Firstnm;
                                LastnmFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Lastnm;
                                MobNumberFR = vATRegistration.d.CONTACTDTSet.results[0].MobNumber;
                                SmtpAddrFR = vATRegistration.d.CONTACTDTSet.results[0].SmtpAddr;
                                if (App.VATType == Enums.PageExecutionType.Amend)
                                {
                                    GpartFR = string.Empty;
                                    idnumber = string.Empty;
                                    FirstnmFR = string.Empty;
                                    LastnmFR = string.Empty;
                                    SmtpAddrFR = string.Empty;
                                    MobNumberFR = string.Empty;
                                    TxtIDTypeFR = string.Empty;
                                }
                                if (App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Reactivation)
                                {
                                    int count = 0;
                                    foreach (var item in vATRegistration.d.CONTACT_PERSONSet.results)
                                    {
                                        ListFinanceRepresenatives.Add(new FinancialRepresentativesModel()
                                        {
                                            GpartFR = item.Gpart,
                                            IdnumberFR = item.Idnumber,
                                            FirstnmFR = item.Firstnm,
                                            LastnmFR = item.Lastnm,
                                            MobNumberFR = vATRegistration.d.CONTACTDTSet.results[count].MobNumber,
                                            SmtpAddrFR = vATRegistration.d.CONTACTDTSet.results[0].SmtpAddr,
                                            TxtIDTypeFR = IdTypeListFR[IDTypeIndexFR].Name
                                        });
                                        count++;
                                    }
                                }

                                SelectedIdTypeFR = IdTypeListFR.Where(x => x.ID == vATRegistration.d.CONTACT_PERSONSet.results[0].Type).FirstOrDefault();
                                ATTDETSetObject = new ATTDETSet();
                                ATTDETSetObject = vATRegistration.d.ATTDETSet;

                            }

                            //Step 5

                            if (vATRegistration.d.Decfg == "1")
                            {
                                IsDeclarationChecked = true;
                                //VATRegistrationDetailsData.d.Decfg = "1";
                            }
                            else if (vATRegistration.d.Decfg == "0")
                            {
                                IsDeclarationChecked = false;
                            }
                            if (vATRegistration.d.Stp2Cbbox == "1")
                            {
                                IsAddAdditionalInfoChecked = true;
                                //VATRegistrationDetailsData.d.Decfg = "1";
                            }
                            else if (vATRegistration.d.Stp2Cbbox == "0")
                            {
                                IsAddAdditionalInfoChecked = false;
                            }
                            if (vATRegistration.d.Stp3Cbbox == "1")
                            {
                                IsFDChangeSectionChecked = true;
                                //VATRegistrationDetailsData.d.Decfg = "1";
                            }
                            else if (vATRegistration.d.Stp3Cbbox == "0")
                            {
                                IsFDChangeSectionChecked = false;
                            }
                            if (vATRegistration.d.Stp4Cbbox2 == "1")
                            {
                                IsAddNewRepresentativeChecked = true;
                                //VATRegistrationDetailsData.d.Decfg = "1";
                            }
                            else if (vATRegistration.d.Stp4Cbbox2 == "0")
                            {
                                IsAddNewRepresentativeChecked = false;
                            }
                            switch (App.VATType)
                            {
                                case Enums.PageExecutionType.Amend:
                                case Enums.PageExecutionType.Reactivation:
                                    IsInstrunctionChecked = true;
                                    break;
                                case Enums.PageExecutionType.Register:
                                    break;
                                default:
                                    break;
                            }
                            if (vATRegistration.d.AgrFg != null)
                            {
                                if (vATRegistration.d.AgrFg == "1")
                                {
                                    IsInstrunctionChecked = true;

                                }
                                if (vATRegistration.d.AgrFg == "0")
                                {
                                    IsInstrunctionChecked = false;
                                }

                            }
                            if (vATRegistration.d.DecidTy != null && vATRegistration.d.DecidTy != "")
                            {

                                SelectedIdTypeSR = IdTypeListSR.Where(x => x.ID == vATRegistration.d.DecidTy).FirstOrDefault();
                            }
                            if (vATRegistration.d.Decconno != null)


                                IdNumberSR = vATRegistration.d.DecidNo;
                            FirstNameSR = vATRegistration.d.Decname;

                            //Added By Divya to display Start Date in TaxPayer Details page 1303,1304
                            if (vATRegistration.d.CrStdt != null)
                            {
                                StartdateToshow = JsonConvert.DeserializeObject<DateTime>(@"""" + vATRegistration.d.CrStdt + @"""").ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                            }
                            VATRegistrationDetailsData = vATRegistration;

                            setIban();
                            if (VATRegistrationDetailsData.d.AgrFg != null)
                            { }


                            if (VATRegistrationDetailsData.d.ADDRESSSet != null && VATRegistrationDetailsData.d.ADDRESSSet.results.Count != 0)
                            {
                                ADDRESSSetData = VATRegistrationDetailsData.d.ADDRESSSet.results[0];
                                AddressLineOne = ADDRESSSetData.BuildingNo + " " + ADDRESSSetData.Street + " " + ADDRESSSetData.Quarter;
                                if (ADDRESSSetData.PostalCd.Equals("00000"))
                                {

                                //commenting the below line for save as draft data comparison
                                // ADDRESSSetData.PostalCd = string.Empty;

                                 }
                                AddressLineTwo = ADDRESSSetData.RegionDesc + " " + ADDRESSSetData.City + " " + ADDRESSSetData.PostalCd;

                            }
                            if (VATRegistrationDetailsData.d.VatTaxDt != null)
                            {
                                string convertedDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATRegistrationDetailsData.d.VatTaxDt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                VatEligibleStartDate = Convert.ToDateTime(convertedDate).ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                            }
                            if (VATRegistrationDetailsData.d.ImFg == "1")
                            {
                                ImporterImageSource = "vat_tile_IbanCard_background.png";
                                ImporterTextColor = Color.White;
                            }
                            else
                            {
                                ImporterImageSource = "vat_tile_IbanCard_background_white.png";
                                ImporterTextColor = Color.Black;
                            }
                            if (VATRegistrationDetailsData.d.ExFg == "1")
                            {
                                ExporterImageSource = "vat_tile_IbanCard_background.png";
                                ExporterTextColor = Color.White;
                            }
                            else
                            {
                                ExporterImageSource = "vat_tile_IbanCard_background_white.png";
                                ExporterTextColor = Color.Black;
                            }


                            if (VATRegistrationDetailsData.d.QUESCONFIG_MSet.results.Count != 0)
                            {
                                MinMaxRanges = new List<QuestionNumberWithMinMaxRange>();
                                MinMaxRanges = UtilityManager.GetLowAndHighRangeForEachQuestionSet(VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                                MaximumDisplayValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.MaxRangeValue).FirstOrDefault();
                                MinimumDisplayValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.MinRangeValue).FirstOrDefault();

                                MaximumDisplayValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.MaxRangeValue).FirstOrDefault();
                                MinimumDisplayValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.MinRangeValue).FirstOrDefault();


                                MaximumValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.CountOfProbableAnswersForThisQuestions).FirstOrDefault() - 1;
                                MaximumValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.CountOfProbableAnswersForThisQuestions).FirstOrDefault() - 1;

                                TextQuestion3First = VATRegistrationDetailsData.d.QUESCONFIG_MSet.results.Where(x => x.QueNo == "003" && x.QoptNo == "031").Select(x => x.QoptTxt).FirstOrDefault();
                                TextQuestion3Second = VATRegistrationDetailsData.d.QUESCONFIG_MSet.results.Where(x => x.QueNo == "003" && x.QoptNo == "032").Select(x => x.QoptTxt).FirstOrDefault();

                                TextQuestion4First = VATRegistrationDetailsData.d.QUESCONFIG_MSet.results.Where(x => x.QueNo == "004" && x.QoptNo == "041").Select(x => x.QoptTxt).FirstOrDefault();
                                TextQuestion4Second = VATRegistrationDetailsData.d.QUESCONFIG_MSet.results.Where(x => x.QueNo == "004" && x.QoptNo == "042").Select(x => x.QoptTxt).FirstOrDefault();

                                setQuestionImage();

                            }
                            if (VATRegistrationDetailsData.d.QUESTIONSSet != null)
                            {
                                answer1selectedcount = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(s => s.QueNo == "001" && s.QoptAns == "1").Count();
                                answer2selectedcount = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(s => s.QueNo == "002" && s.QoptAns == "1").Count();
                                answer3selectedcount = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(s => s.QueNo == "003" && s.QoptAns == "1").Count();
                                answer4selectedcount = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(s => s.QueNo == "004" && s.QoptAns == "1").Count();
                            }

                            vATRegistrationOther = await WebServiceManager.GAZTGetVATRegistrationDataWithButtons(vATRegistration.d.Fbnumz, vATRegistration.d.Officerz, vATRegistration.d.Statusz, vATRegistration.d.TxnTpz, "ZTAX_VT_REG");
                            PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                            if (vATRegistrationOther != null && vATRegistrationOther.d != null)
                            {
                                VATRegistrationOtherDetails = vATRegistrationOther;

                                SetApplicableButtons();
                            }

                            OriginalData = JsonConvert.SerializeObject(VATRegistrationDetailsData);

                            IdnumberFR = idnumber;

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                                _navigationService.GoBack();
                            });
                        }
                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                        //   await Task.Run(() =>
                        //   {
                        //  });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //_dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }

        private void SetApplicableButtons()
        {
            //Parag
            if ((VATRegistrationOtherDetails.d.VR_UI_BTNSet != null) && (VATRegistrationOtherDetails.d.VR_UI_BTNSet.results != null))
            {
                if (ListOfActionButtonsApplicableForRegistration != null && ListOfActionButtonsApplicableForRegistration.Count > 0)
                    ListOfActionButtonsApplicableForRegistration.Clear();
                else
                    ListOfActionButtonsApplicableForRegistration = new List<string>();

                //beforoe returning buttons we need to set the value based on Buttons emumeration
                foreach (ResultsItemForButton button in VATRegistrationOtherDetails.d.VR_UI_BTNSet.results)
                {
                    Buttons buttonEnumId = Buttons.None;

                    Enum.TryParse(button.Button, out buttonEnumId);
                    String localisedString = GetLocalisedButtonString(buttonEnumId.ToString());

                    if (false == String.IsNullOrEmpty(localisedString))
                        ListOfActionButtonsApplicableForRegistration.Add(localisedString);


                }
            }
            //Parag
        }

        public void setIban()
        {
            IbanList = new ObservableCollection<Result2>();

            if (VATRegistrationDetailsData.d.IBANSet != null)
            {

                IbanList = new ObservableCollection<Result2>(VATRegistrationDetailsData.d.IBANSet.results);

                ObservableCollection<Result2> resultList = new ObservableCollection<Result2>();

                foreach (var item in IbanList)
                {
                    if(!string.IsNullOrEmpty(item.Bkvid))
                    {
                        resultList.Add(item);
                    }
                }
                IbanList = resultList;
            }

        }
        public void GetSignUpIdType()
        {
            try
            {
                List<SignUpIdType> signUpIdTypeList = new List<SignUpIdType>{
                new SignUpIdType {ID = "00000",Name = "      "},
                new SignUpIdType {ID = "ZS0001",Name = AppResources.NationaID},
                new SignUpIdType {ID = "ZS0002",Name = AppResources.ZZIqamaID},
                new SignUpIdType {ID = "ZS0003",Name = AppResources.ZZGCCID},
            };
                List<SignUpIdType> lst = new List<SignUpIdType>();
                lst = signUpIdTypeList;
                IdTypeListFR = signUpIdTypeList;
                IdTypeListSR = signUpIdTypeList;
                IDTypeIndexFR = 0;
                IDTypeIndexSR = 0;
                TxtIDType = AppResources.ZZNationalID;
                //  SelectedIdTypeFR = IdTypeListFR.FirstOrDefault();

                TxtIDTypeFR = IdTypeListFR[IDTypeIndexFR].Name;
                TxtIDTypeSR = IdTypeListSR[IDTypeIndexSR].Name;
                SelectedIdTypeFR = IdTypeListFR[IDTypeIndexFR];
                SelectedIdTypeSR = IdTypeListSR[IDTypeIndexSR];
            }
            catch (Exception ex)
            {

            }
        }


        public async Task SetDefaultDate()
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
            TodayDate = todaycollection;
            DefaultMonth = DateTime.Now.Date.Month;
        }
        #endregion

    }
}