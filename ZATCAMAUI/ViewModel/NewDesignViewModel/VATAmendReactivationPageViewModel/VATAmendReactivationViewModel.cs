using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using RGPopup.Maui.Services;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.Template;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATAmendReactivationPageViewModel
{
  
    public class VATAmendReactivationPageViewModel : BaseViewModel
    {
        public List<ListViewCardTemplateModel> ImporterExporterItems { get; set; }
        public VATRegistrationDetails VATRegistrationData = new VATRegistrationDetails();
        public string OriginalData;
        public string ModifiedData;


        public int DefaultMonth;
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public static IsComeFromForAttachment IsComeFromForAttachment;
        public string _pageTitle;
        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                _pageTitle = value;
                RaisePropertyChanged("PageTitle");
            }
        }
        public int PageType { get; set; }

        #region Variable


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
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 5;
        #endregion


        #region Properties
        private string _newVatEligibleStartDate = string.Empty;
        public string NewVatEligibleStartDate
        {
            get
            {
                return _newVatEligibleStartDate;
            }
            set
            {
                if (_newVatEligibleStartDate == value) return;
                _newVatEligibleStartDate = value;
                RaisePropertyChanged("NewVatEligibleStartDate");
            }
        }

        private bool _isNewStartDateInfoChecked = false;
        public bool IsNewStartDateInfoChecked
        {
            get
            {
                return _isNewStartDateInfoChecked;
            }
            set
            {
                if (_isNewStartDateInfoChecked == value) return;
                _isNewStartDateInfoChecked = value;
                if (_isNewStartDateInfoChecked)
                {
                    //isNewEligibleStartDateIsValid = true;
                    CheckCR1450FieldsValid();
                }
                RaisePropertyChanged("IsNewStartDateInfoChecked");
            }
        }


        private Color _continueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
        public Color ContinueButtonnBackroundColor
        {
            get
            {
                return _continueButtonnBackroundColor;
            }
            set
            {
                if (_continueButtonnBackroundColor == value) return;

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
                if (_answer1selectedcount == value) return;

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
                if (_ATTDETSetObject == value) return;

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
                if (_answer2selectedcount == value) return;

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
                if (_answer3selectedcount == value) return;

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
                if (_answer4selectedcount == value) return;

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
                if (_isFDNameMobEmailEnable == value) return;

                _isFDNameMobEmailEnable = value;
                if (IsChangeEmailChecked)
                {
                    _isFDNameMobEmailEnable = true;

                }
                else
                {
                    _isFDNameMobEmailEnable = false;
                }
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
                if (_IsBackStepButtonVisible == value) return;

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
                if (_iDNumberNonMandatoryVisibility == value) return;

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
                if (_iDNumberMandatoryVisibility == value) return;

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
                if (_iDNumberVisibility == value) return;

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
                if (_dOBNonMandatoryVisibility == value) return;

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
                if (_dOBMandatoryVisibility == value) return;

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
                if (_isAttachmentImporterExporterVisible == value) return;

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
                if (_isContinueButtonEnable == value) return;

                _isContinueButtonEnable = value;
                if (_isContinueButtonEnable)
                {
                    ContinueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
                }
                else
                {
                    ContinueButtonnBackroundColor = (Color)Application.Current.Resources["ButtonGray"];
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
                if (_startdateToshow == value) return;

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
                if (_quesTion3answerSelected == value) return;

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
                if (_quesTion4answerSelected == value) return;

                _quesTion4answerSelected = value;

                RaisePropertyChanged("quesTion4answerSelected");
            }
        }
        private List<object> _todayDate;
        public List<object> TodayDate
        {
            get
            {
                return _todayDate;
            }
            set
            {
                if (_todayDate == value) return;

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
                if (_DOB == value) return;

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
                if (_contactDOB == value) return;

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
                if (_maxLengthID == value) return;

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
                if (_maxLengthIDSR == value) return;

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
                if (_frameContactIDError == value) return;

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
                if (_frameIDError == value) return;

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
                if (_frameContactDOBError == value) return;

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
                if (_frameDOBError == value) return;

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
                if (_isInstrunctionVisible == value) return;

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
        private bool _IsFDChangeSectionEnabled = false;
        public bool IsFDChangeSectionEnabled
        {
            get
            {
                return _IsFDChangeSectionEnabled;
            }
            set
            {
                if (_IsFDChangeSectionEnabled == value) return;

                _IsFDChangeSectionEnabled = value;
                RaisePropertyChanged("IsFDChangeSectionEnabled");
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
                if (_isTaxPayersVisible == value) return;

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
                if (_isSalesVisible == value) return;

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
                if (_isExpensesVisible == value) return;

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
                if (_isFinancialVisible == value) return;

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
                if (_isDeclarationDOBVisible == value) return;

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
                if (_isTaxPayerIBANEnabled == value) return;

                _isTaxPayerIBANEnabled = value;
                RaisePropertyChanged(nameof(IsTaxPayerIBANEnabled));
            }
        }
        private bool _isTaxPayerEligDateEnabled = false;
        public bool IsTaxPayerEligDateEnabled
        {
            get => _isTaxPayerEligDateEnabled;
            set
            {
                if (_isTaxPayerEligDateEnabled == value) return;

                _isTaxPayerEligDateEnabled = value;
                RaisePropertyChanged(nameof(IsTaxPayerEligDateEnabled));
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
                if (_isSummaryVisible == value) return;

                _isSummaryVisible = value;
                RaisePropertyChanged("IsSummaryVisible");
            }
        }
        private bool _IsFinancialDChangeSectionEnabled = true;
        public bool IsFinancialDChangeSectionEnabled
        {
            get
            {
                return _IsFinancialDChangeSectionEnabled;
            }
            set
            {
                if (_IsFinancialDChangeSectionEnabled == value) return;

                _IsFinancialDChangeSectionEnabled = value;
                RaisePropertyChanged("IsFinancialDChangeSectionEnabled");
            }
        }

        private string _currentStep;
        public string CurrentStep
        {
            get
            {
                return _currentStep;
            }
            set
            {
                if (_currentStep == value) return;

                _currentStep = value;
                RaisePropertyChanged("CurrentStep");
            }
        }


        private List<string> _ListOfActionButtonsApplicableForRegistration;
        public List<string> ListOfActionButtonsApplicableForRegistration
        {
            get
            {
                return _ListOfActionButtonsApplicableForRegistration;
            }
            set
            {
                if (_ListOfActionButtonsApplicableForRegistration == value) return;

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
                if (_regTypeCode == value) return;

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
                if (_vATRegistrationDetailsData == value) return;

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
                if (_originalVATRegistrationDetailsData == value) return;

                _originalVATRegistrationDetailsData = value;
                RaisePropertyChanged("OriginalVATRegistrationDetailsData");
            }
        }


        private double _minimumDisplayValueOfSlider1 = 0.0;
        public double MinimumDisplayValueOfSlider1
        {
            get
            {
                return _minimumDisplayValueOfSlider1;
            }
            set
            {
                if (_minimumDisplayValueOfSlider1 == value) return;

                _minimumDisplayValueOfSlider1 = value;
                RaisePropertyChanged("MinimumDisplayValueOfSlider1");
            }
        }

        private double _maximumDisplayValueOfSlider1 = 1.0;
        public double MaximumDisplayValueOfSlider1
        {
            get
            {
                return _maximumDisplayValueOfSlider1;
            }
            set
            {
                if (_maximumDisplayValueOfSlider1 == value) return;

                _maximumDisplayValueOfSlider1 = value;
                RaisePropertyChanged("MaximumDisplayValueOfSlider1");
            }
        }

        private double _maximumDisplayValueOfSlider2 = 1.0;
        public double MaximumDisplayValueOfSlider2
        {
            get
            {
                return _maximumDisplayValueOfSlider2;
            }
            set
            {
                if (_maximumDisplayValueOfSlider2 == value) return;

                _maximumDisplayValueOfSlider2 = value;
                RaisePropertyChanged("MaximumDisplayValueOfSlider2");
            }
        }

        private double _minimumDisplayValueOfSlider2 = 0.0;
        public double MinimumDisplayValueOfSlider2
        {
            get
            {
                return _minimumDisplayValueOfSlider2;
            }
            set
            {
                if (_minimumDisplayValueOfSlider2 == value) return;

                _minimumDisplayValueOfSlider2 = value;
                RaisePropertyChanged("MinimumDisplayValueOfSlider2");
            }
        }

        private double _maximumValueOfSlider2 = 1.0;
        public double MaximumValueOfSlider2
        {
            get
            {
                return _maximumValueOfSlider2;
            }
            set
            {
                if (_maximumValueOfSlider2 == value) return;

                _maximumValueOfSlider2 = value;
                RaisePropertyChanged("MaximumValueOfSlider2");
            }
        }

        private double _maximumValueOfSlider1 = 1.0;
        public double MaximumValueOfSlider1
        {
            get
            {
                return _maximumValueOfSlider1;
            }
            set
            {
                if (_maximumValueOfSlider1 == value) return;

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
                if (_sliderLable1EligibilityText == value) return;

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
                if (_sliderLable1 == value) return;

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
                if (_sliderLable2 == value) return;

                _sliderLable2 = value;
                RaisePropertyChanged("SliderLable2");
            }
        }
        private double _sliderCurrentValue1 = 0.0;
        public double SliderCurrentValue1
        {
            get
            {
                return _sliderCurrentValue1;
            }
            set
            {
                if (_sliderCurrentValue1 == value) return;

                _sliderCurrentValue1 = value;
                RaisePropertyChanged("SliderCurrentValue1");
            }
        }

        private double _sliderCurrentValue2 = 0.0;
        public double SliderCurrentValue2
        {
            get
            {
                return _sliderCurrentValue2;
            }
            set
            {
                if (_sliderCurrentValue2 == value) return;

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
                if (_minMaxRanges == value) return;

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
                if (_vATRegistrationOtherDetails == value) return;

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
                MessagingCenter.Send(this, "IsInstrunctionChecked", value);
                if (_isInstrunctionChecked == value) return;

                _isInstrunctionChecked = value;

                if (_isInstrunctionChecked)
                {
                    IsContinueButtonEnable = true;
                }
                else
                {
                    IsContinueButtonEnable = false;
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
                MessagingCenter.Send(this, "IsDeclarationChecked", value);
                if (_isDeclarationChecked == value) return;

                _isDeclarationChecked = value;

                if (_isDeclarationChecked)
                {
                    IsContinueButtonEnable = true;
                }
                else
                {
                    IsContinueButtonEnable = false;
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
                MessagingCenter.Send(this, "IsAddAdditionalInfoChecked", value);
                if (_isAddAdditionalInfoChecked == value) return;

                _isAddAdditionalInfoChecked = value;
                if (_isAddAdditionalInfoChecked)
                {
                    AddAdditionalInfoCheckBoxEnabled = false;
                    IsTaxPayerIBANEnabled = true;
                    IsTaxPayerEligDateEnabled = false;

                }
                else
                {
                    AddAdditionalInfoCheckBoxEnabled = true;
                    IsTaxPayerIBANEnabled = false;
                    IsTaxPayerEligDateEnabled = false;

                }

                RaisePropertyChanged("IsAddAdditionalInfoChecked");
            }
        }
        private bool _AddAdditionalInfoCheckBoxEnabled = true;
        public bool AddAdditionalInfoCheckBoxEnabled
        {
            get
            {
                return _AddAdditionalInfoCheckBoxEnabled;
            }
            set
            {
                if (_AddAdditionalInfoCheckBoxEnabled == value) return;

                _AddAdditionalInfoCheckBoxEnabled = value;

                RaisePropertyChanged("AddAdditionalInfoCheckBoxEnabled");
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
                MessagingCenter.Send(this, "IsFDChangeSectionChecked", value);
                if (_isFDChangeSectionChecked == value) return;

                _isFDChangeSectionChecked = value;
                if (_isFDChangeSectionChecked)
                {
                    IsFinancialDChangeSectionEnabled = false;
                }
                else
                {
                    IsFinancialDChangeSectionEnabled = true;
                }

                RaisePropertyChanged("IsFDChangeSectionChecked");
            }
        }
        private bool _isAddNewRepresentativeChecked = false;
        public bool IsAddNewRepresentativeChecked
        {
            get
            {
                return _isAddNewRepresentativeChecked;
            }
            set
            {
                _isAddNewRepresentativeChecked = value;
                IsNewFinancialRepVisible = value;
                IsChangeEmailCheckBoxEnabled = !value;
                RaisePropertyChanged("IsAddNewRepresentativeChecked");
            }
        }
        private bool _IsChangeEmailChecked = false;
        public bool IsChangeEmailChecked
        {
            get
            {
                return _IsChangeEmailChecked;
            }
            set
            {
                if (_IsChangeEmailChecked == value) return;


                _IsChangeEmailChecked = value;
                if (_IsChangeEmailChecked)
                {
                    IsFDNameMobEmailEnable = true;
                    IsAddFinancialRepresentativeCheckBoxEnabled = false;
                    IsAddFinancialRepButtonEnabled = false;
                }
                else
                {
                    IsFDNameMobEmailEnable = false;
                    IsAddFinancialRepresentativeCheckBoxEnabled = true;
                    IsAddFinancialRepButtonEnabled = true;

                    if (TempDataContacts != null)
                    {
                        var items = new List<FinancialRepresentativesModel>();
                        for (int i = 0; i < ListFinanceRepresenatives.Count(); i++)
                        {
                            FinancialRepresentativesModel financialRepresentativesModel = ListFinanceRepresenatives[i];
                            financialRepresentativesModel.MobNumberFR = TempDataContacts[i].tempmobile;
                            financialRepresentativesModel.SmtpAddrFR = TempDataContacts[i].tempEmail;
                            items.Add(financialRepresentativesModel);
                        }
                        ListFinanceRepresenatives = items;

                    }

                }

                IsAddFinancialRepresentativeCheckBoxEnabled = !value;

                RaisePropertyChanged("IsChangeEmailChecked");
            }
        }

        private bool _IsAddFinancialRepresentativeCheckBoxEnabled = false;
        public bool IsAddFinancialRepresentativeCheckBoxEnabled
        {
            get
            {
                return _IsAddFinancialRepresentativeCheckBoxEnabled;
            }
            set
            {
                if (_IsAddFinancialRepresentativeCheckBoxEnabled == value) return;

                _IsAddFinancialRepresentativeCheckBoxEnabled = value;


                RaisePropertyChanged("IsAddFinancialRepresentativeCheckBoxEnabled");
            }
        }

        private bool _IsAddFinancialRepButtonEnabled = false;
        public bool IsAddFinancialRepButtonEnabled
        {
            get
            {
                return _IsAddFinancialRepButtonEnabled;
            }
            set
            {
                if (_IsAddFinancialRepButtonEnabled == value) return;

                _IsAddFinancialRepButtonEnabled = value;


                RaisePropertyChanged("IsAddFinancialRepButtonEnabled");
            }
        }


        private bool _IsChangeEmailCheckBoxEnabled = false;
        public bool IsChangeEmailCheckBoxEnabled
        {
            get
            {
                return _IsChangeEmailCheckBoxEnabled;
            }
            set
            {
                if (_IsChangeEmailCheckBoxEnabled == value) return;

                _IsChangeEmailCheckBoxEnabled = value;




                RaisePropertyChanged("IsChangeEmailCheckBoxEnabled");
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
                if (_isResident == value) return;

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
                if (_attachments == value) return;

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
                if (_aDDRESSSetData == value) return;

                _aDDRESSSetData = value;
                RaisePropertyChanged("ADDRESSSetData");
            }
        }

        private string _addressLineOne;
        public string AddressLineOne
        {
            get
            {
                return _addressLineOne;
            }
            set
            {
                if (_addressLineOne == value) return;

                _addressLineOne = value;
                RaisePropertyChanged("AddressLineOne");
            }
        }

        private string _addressLineTwo;
        public string AddressLineTwo
        {
            get
            {
                return _addressLineTwo;
            }
            set
            {
                if (_addressLineTwo == value) return;

                _addressLineTwo = value;
                RaisePropertyChanged("AddressLineTwo");
            }
        }

        private string _vatEligibleStartDate = string.Empty;
        public string VatEligibleStartDate
        {
            get
            {
                return _vatEligibleStartDate;
            }
            set
            {
                if (_vatEligibleStartDate == value) return;

                _vatEligibleStartDate = value;
                RaisePropertyChanged("VatEligibleStartDate");
            }
        }

        private string _gpartFR = string.Empty;
        public string GpartFR
        {
            get
            {
                return _gpartFR;
            }
            set
            {
                if (_gpartFR == value) return;

                _gpartFR = value;
                GpartSum = value;
                RaisePropertyChanged("GpartFR");
            }
        }
        private string _gpartSum = string.Empty;
        public string GpartSum
        {
            get
            {
                return _gpartSum;
            }
            set
            {
                if (_gpartSum == value) return;

                _gpartSum = value;
                RaisePropertyChanged("GpartSum");
            }
        }
        private string _typeFR = string.Empty;
        public string TypeFR
        {
            get
            {
                return _typeFR;
            }
            set
            {
                if (_typeFR == value) return;

                _typeFR = value;
                RaisePropertyChanged("TypeFR");
            }
        }

        private string _idnumberFR = string.Empty;
        public string IdnumberFR
        {
            get
            {
                return _idnumberFR;
            }
            set
            {
                if (_idnumberFR == value) return;

                _idnumberFR = value;
                IdnumberSum = value;
                RaisePropertyChanged("IdnumberFR");
            }
        }
        private string _idnumberSum = string.Empty;
        public string IdnumberSum
        {
            get
            {
                return _idnumberSum;
            }
            set
            {
                if (_idnumberSum == value) return;

                _idnumberSum = value;
                RaisePropertyChanged("IdnumberSum");
            }
        }

        private string _firstnmFR = string.Empty;
        public string FirstnmFR
        {
            get
            {
                return _firstnmFR;
            }
            set
            {
                if (_firstnmFR == value) return;

                _firstnmFR = value;
                FirstnmSum = value;

                RaisePropertyChanged("FirstnmFR");
            }
        }
        private string _firstnmSum = string.Empty;
        public string FirstnmSum
        {
            get
            {
                return _firstnmSum;
            }
            set
            {
                if (_firstnmSum == value) return;

                _firstnmSum = value;
                RaisePropertyChanged("FirstnmSum");
            }
        }

        private string _lastnmFR = string.Empty;
        public string LastnmFR
        {
            get
            {
                return _lastnmFR;
            }
            set
            {
                if (_lastnmFR == value) return;

                _lastnmFR = value;
                LastnmSum = value;

                RaisePropertyChanged("LastnmFR");
            }
        }
        private string _lastnmSum = string.Empty;
        public string LastnmSum
        {
            get
            {
                return _lastnmSum;
            }
            set
            {
                if (_lastnmSum == value) return;

                _lastnmSum = value;
                RaisePropertyChanged("LastnmSum");
            }
        }

        private List<TempDataContact> TempDataContacts { get; set; } = new List<TempDataContact>();
        public class TempDataContact
        {
            public string tempEmail { get; set; }
            public string tempmobile { get; set; }
        }


        private string _primarymobNumberFR = string.Empty;
        public string PrimaryMobNumberFR
        {
            get
            {
                return _primarymobNumberFR;
            }
            set
            {
                if (_primarymobNumberFR == value) return;

                _primarymobNumberFR = value;
                RaisePropertyChanged("PrimaryMobNumberFR");
            }
        }

        private string _mobNumberFR = string.Empty;
        public string MobNumberFR
        {
            get
            {
                return _mobNumberFR;
            }
            set
            {
                if (_primarymobNumberFR == value) return;

                _primarymobNumberFR = value;
                MobNumberSum = value;
                _mobNumberFR = value;
                RaisePropertyChanged("MobNumberFR");
            }
        }
        private string _mobNumberSum = string.Empty;
        public string MobNumberSum
        {
            get
            {
                return _mobNumberSum;
            }
            set
            {
                if (_mobNumberSum == value) return;

                _mobNumberSum = value;
                RaisePropertyChanged("MobNumberSum");
            }
        }

        private string _idNumberSR = string.Empty;
        public string IdNumberSR
        {
            get
            {
                return _idNumberSR;
            }
            set
            {
                if (_idNumberSR == value) return;

                _idNumberSR = value;
                RaisePropertyChanged("IdNumberSR");
            }
        }

        private string _firstNameSR = string.Empty;
        public string FirstNameSR
        {
            get
            {
                return _firstNameSR;
            }
            set
            {
                if (_firstNameSR == value) return;

                _firstNameSR = value;
                RaisePropertyChanged("FirstNameSR");
            }
        }

        private string _primarysmtpAddrFR = string.Empty;
        public string PrimarySmtpAddrFR
        {
            get
            {
                return _primarysmtpAddrFR;
            }
            set
            {
                if (_primarysmtpAddrFR == value) return;

                _primarysmtpAddrFR = value;
                RaisePropertyChanged("PrimarySmtpAddrFR");
            }
        }

        private string _smtpAddrFR = string.Empty;
        public string SmtpAddrFR
        {
            get
            {
                return _smtpAddrFR;
            }
            set
            {
                if (_smtpAddrFR == value) return;

                _smtpAddrFR = value;
                SmtpAddrSum = value;

                RaisePropertyChanged("SmtpAddrFR");
            }
        }
        private string _smtpAddrSum = string.Empty;
        public string SmtpAddrSum
        {
            get
            {
                return _smtpAddrSum;
            }
            set
            {
                if (_smtpAddrSum == value) return;

                _smtpAddrSum = value;
                RaisePropertyChanged("SmtpAddrSum");
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
                if (_idTypeListFR == value) return;

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
                if (_idTypeListSR == value) return;

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
                if (_iDTypeIndexFR == value) return;

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
                if (_iDTypeIndexSR == value) return;

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
                if (_txtIDType == value) return;

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
                if (_txtIDTypeFR == value) return;

                _txtIDTypeFR = value;
                TxtIDTypeSum = value;

                TxtIDTypeSR = value;
                RaisePropertyChanged("TxtIDTypeFR");
            }
        }
        private string _txtIDTypeSum = string.Empty;
        public string TxtIDTypeSum
        {
            get
            {
                return _txtIDTypeSum;
            }
            set
            {
                if (_txtIDTypeSum == value) return;

                _txtIDTypeSum = value;
                RaisePropertyChanged("TxtIDTypeSum");
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
                if (_txtIDTypeSR == value) return;

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
                if (_selectedIdTypeFR == value) return;

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
                    catch (Exception)
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
                if (_selectedIdTypeSR == value) return;

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
                    {

                    }
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
                if (_importerImageSource == value) return;

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
                if (_importerTextColor == value) return;

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
                if (_exporterTextColor == value) return;

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
                if (_exporterImageSource == value) return;

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
                if (_selectedIban == value) return;

                _selectedIban = value;
                RaisePropertyChanged("SelectedIban");
            }
        }

        private ObservableCollection<Result2> _ibanList = new ObservableCollection<Result2>();
        public ObservableCollection<Result2> IbanList
        {
            get
            {
                return _ibanList;
            }
            set
            {
                if (_ibanList == value) return;

                _ibanList = value;
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
                if (_isNewAccountClicked == value) return;

                _isNewAccountClicked = value;
                RaisePropertyChanged("IsNewAccountClicked");
            }
        }
        private bool _isNewVatEligibleDateInsVisible = false;
        public bool IsNewVatEligibleDateInsVisible
        {
            get
            {
                return _isNewVatEligibleDateInsVisible;
            }
            set
            {
                if (_isNewVatEligibleDateInsVisible == value) return;

                _isNewVatEligibleDateInsVisible = value;
                RaisePropertyChanged("IsNewVatEligibleDateInsVisible");
            }
        }
        private bool _vatEligibleStartDateInsVisible = true;
        public bool VatEligibleStartDateInsVisible
        {
            get
            {
                return _vatEligibleStartDateInsVisible;
            }
            set
            {
                if (_vatEligibleStartDateInsVisible == value) return;

                _vatEligibleStartDateInsVisible = value;
                RaisePropertyChanged("VatEligibleStartDateInsVisible");
            }
        }

        private bool _isCheckEnabled = true;
        public bool IsCheckEnabled
        {
            get
            {
                return _isCheckEnabled;
            }
            set
            {
                if (_isCheckEnabled == value) return;

                _isCheckEnabled = value;
                RaisePropertyChanged("IsCheckEnabled");
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
                if (_isNewAccountText == value) return;

                _isNewAccountText = value;
                RaisePropertyChanged("NewAccountText");
            }
        }

        private string _textQuestion3First = string.Empty;
        public string TextQuestion3First
        {
            get
            {
                return _textQuestion3First;
            }
            set
            {
                if (_textQuestion3First == value) return;

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
                if (_textQuestion3FirstTextColor == value) return;

                _textQuestion3FirstTextColor = value;
                RaisePropertyChanged("TextQuestion3FirstTextColor");
            }
        }

        private string _textQuestion4First = string.Empty;
        public string TextQuestion4First
        {
            get
            {
                return _textQuestion4First;
            }
            set
            {
                if (_textQuestion4First == value) return;

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
                if (_textQuestion4FirstTextColor == value) return;

                _textQuestion4FirstTextColor = value;
                RaisePropertyChanged("TextQuestion4FirstTextColor");
            }
        }

        private string _textQuestion3Second = string.Empty;
        public string TextQuestion3Second
        {
            get
            {
                return _textQuestion3Second;
            }
            set
            {
                if (_textQuestion3Second == value) return;

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
                if (_textQuestion3SecondTextColor == value) return;

                _textQuestion3SecondTextColor = value;
                RaisePropertyChanged("TextQuestion3SecondTextColor");
            }
        }

        private string _textQuestion4Second = string.Empty;
        public string TextQuestion4Second
        {
            get
            {
                return _textQuestion4Second;
            }
            set
            {
                if (_textQuestion4Second == value) return;

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
                if (_textQuestion4SecondTextColor == value) return;

                _textQuestion4SecondTextColor = value;
                RaisePropertyChanged("TextQuestion4SecondTextColor");
            }
        }

        private string _imageforTextQuestion3First = string.Empty;
        public string ImageforTextQuestion3First
        {
            get
            {
                return _imageforTextQuestion3First;
            }
            set
            {
                if (_imageforTextQuestion3First == value) return;

                _imageforTextQuestion3First = value;
                RaisePropertyChanged("ImageforTextQuestion3First");
            }
        }
        private string _imageforTextQuestion3Second = string.Empty;
        public string ImageforTextQuestion3Second
        {
            get
            {
                return _imageforTextQuestion3Second;
            }
            set
            {
                if (_imageforTextQuestion3Second == value) return;

                _imageforTextQuestion3Second = value;
                RaisePropertyChanged("ImageforTextQuestion3Second");
            }
        }
        private string _imageforTextQuestion4First = string.Empty;
        public string ImageforTextQuestion4First
        {
            get
            {
                return _imageforTextQuestion4First;
            }
            set
            {
                if (_imageforTextQuestion4First == value) return;

                _imageforTextQuestion4First = value;
                RaisePropertyChanged("ImageforTextQuestion4First");
            }
        }

        private string _imageforTextQuestion4Second = string.Empty;
        public string ImageforTextQuestion4Second
        {
            get
            {
                return _imageforTextQuestion4Second;
            }
            set
            {
                if (_imageforTextQuestion4Second == value) return;

                _imageforTextQuestion4Second = value;
                RaisePropertyChanged("ImageforTextQuestion4Second");
            }
        }

        private List<FinancialRepresentativesModel> _listFinanceRepresenatives = new List<FinancialRepresentativesModel>();
        public List<FinancialRepresentativesModel> ListFinanceRepresenatives
        {
            get => _listFinanceRepresenatives;
            set
            {
                if (_listFinanceRepresenatives == value) return;

                if (value != null)
                {
                    _listFinanceRepresenatives = value;
                    if (value.Count > 0)
                    {
                        MobNumberSum = value[0].MobNumberFR;
                        SmtpAddrSum = value[0].SmtpAddrFR;
                    }
                    RaisePropertyChanged(nameof(ListFinanceRepresenatives));
                }
            }
        }
        private bool _isDeclarationViewEnabled;
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


        private bool _isDeclarationViewEnabledNew;
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
                RaisePropertyChanged("IsDeclarationViewEnabledNew");
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
                RaisePropertyChanged("VatDeregDeclaration");
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
                RaisePropertyChanged("Zterms");
            }
        }

        #region Item Availability Properties

        private InstAndConditionAvailability _instAndCondition = new InstAndConditionAvailability();
        public InstAndConditionAvailability InstAndCondition
        {
            get
            {
                _instAndCondition.CBAgreeCondition = App.VATType == PageExecutionType.Amend ? false : App.VATType == PageExecutionType.Reactivation ? false : true;
                return _instAndCondition;
            }
        }
        private TaxPayer_DetailsAvailability _taxPayerDetails = new TaxPayer_DetailsAvailability();
        public TaxPayer_DetailsAvailability TaxPayerDetails
        {
            get
            {
                return _taxPayerDetails;
            }
            set
            {
                if (_taxPayerDetails == value) return;

                _taxPayerDetails = value;
                RaisePropertyChanged("TaxPayerDetails");
            }
        }
        private FinancialDetailsAvailability _financialDetails = new FinancialDetailsAvailability();
        public FinancialDetailsAvailability FinancialDetails
        {
            get
            {
                _financialDetails.Parent = App.VATType == PageExecutionType.Amend ? false : App.VATType == PageExecutionType.Reactivation ? false : true;
                _financialDetails.VATEligibilityPoint1Parent = App.VATType == PageExecutionType.Amend ? false : App.VATType == PageExecutionType.Reactivation ? false : true;
                _financialDetails.VATEligibilityPoint2Parent = App.VATType == PageExecutionType.Amend ? false : App.VATType == PageExecutionType.Reactivation ? false : true;
                _financialDetails.VATEligibilityPoint3Parent = App.VATType == PageExecutionType.Amend ? false : App.VATType == PageExecutionType.Reactivation ? false : true;
                _financialDetails.VATEligibilityPoint4Parent = App.VATType == PageExecutionType.Amend ? false : App.VATType == PageExecutionType.Reactivation ? false : true;
                _financialDetails.AttachSectionCB = App.VATType == PageExecutionType.Amend ? true : App.VATType == PageExecutionType.Reactivation ? false : false;
                _financialDetails.AttachSectionCBVisible = App.VATType == PageExecutionType.Amend ? true : App.VATType == PageExecutionType.Reactivation ? false : false;
                IsFDChangeSectionEnabled = App.VATType == PageExecutionType.Amend ? false : App.VATType == PageExecutionType.Reactivation ? false : true;
                _financialDetails.AttachSectionAddNewType = App.VATType == PageExecutionType.Amend ? true : App.VATType == PageExecutionType.Reactivation ? false : true;
                return _financialDetails;
            }
        }
        private FinancialRepresentativeAvailability _financialRepresentative = new FinancialRepresentativeAvailability();
        public FinancialRepresentativeAvailability FinancialRepresentative
        {
            get
            {

                return _financialRepresentative;
            }
            set
            {
                if (_financialRepresentative == value) return;

                _financialRepresentative = value;
                RaisePropertyChanged("FinancialRepresentative");
            }
        }
        private DeclarationAvailability _declaration = new DeclarationAvailability();
        public DeclarationAvailability Declaration
        {
            get
            {
                return _declaration;
            }
            set
            {
                if (_declaration == value) return;

                _declaration = value;
                RaisePropertyChanged("Declaration");
            }
        }
        private bool shouldShowAR = false;
        public bool ShouldShowAR
        {
            get { return shouldShowAR; }
            set
            {
                if (shouldShowAR == value) return;

                shouldShowAR = value;
                RaisePropertyChanged("ShouldShowAR");
            }
        }
        private bool shouldShowEN = false;
        public bool ShouldShowEN
        {
            get { return shouldShowEN; }
            set
            {
                if (shouldShowEN == value) return;

                shouldShowEN = value;
                RaisePropertyChanged("ShouldShowEN");
            }
        }

        private TextAlignment _termsAlignment;
        public TextAlignment TermsAlignment
        {
            get { return _termsAlignment; }
            set
            {
                if (_termsAlignment == value) return;
                _termsAlignment = value;
                RaisePropertyChanged("TermsAlignment");
            }
        }

        #endregion


        #endregion

        public void SetUIAvailability()
        {
            switch (App.VATType)
            {
                case PageExecutionType.Amend:
                    InstAndCondition.CBAgreeCondition =

                    TaxPayerDetails.Parent =
                    TaxPayerDetails.TaxPayerDetailsParent =

                    TaxPayerDetails.TinEntry1 =
                    TaxPayerDetails.TinEntry2 =
                    TaxPayerDetails.MainOutletEntry1 =
                    TaxPayerDetails.MainOutletEntry2 =
                    TaxPayerDetails.StartDateEntry =
                    TaxPayerDetails.AddressEntry1 =
                    TaxPayerDetails.AddressEntry2 =
                    TaxPayerDetails.SourceEntry =
                    TaxPayerDetails.CommencementDate = false;

                    IsTaxPayerEligDateEnabled = false;
                    TaxPayerDetails.AddInformationCB =
                    TaxPayerDetails.AddInformationCBVisible =
                    TaxPayerDetails.AddInformationParent =
                    IsTaxPayerIBANEnabled =
                    TaxPayerDetails.ImporterYesRB =
                    TaxPayerDetails.ImporterNoRB =
                    TaxPayerDetails.ImporterAttachmentsBtn =
                    TaxPayerDetails.ExporterYesRB =
                    TaxPayerDetails.ExporterNoRB =
                    TaxPayerDetails.ExporterrAttachmentsBtn =
                    TaxPayerDetails.ExistingIBANPicker =
                    TaxPayerDetails.NewIBANPicker = true;

                    FinancialRepresentative.Parent = false;
                    FinancialRepresentative.ChangeMobileEmailCB = false;
                    FinancialRepresentative.AddNewFinRepresentativeCB = true;
                    FinancialRepresentative.AddNewFinRepCBVisible = true;
                    IsNewFinancialRepVisible = false;
                    FinancialRepresentative.SkipBtn = false;


                    FinancialRepresentative.TinEntry = true;
                    FinancialRepresentative.IDTypeEntry = true;
                    FinancialRepresentative.IDNoEntry = false;
                    FinancialRepresentative.FNameEntry = false;
                    FinancialRepresentative.SurnameEntry = false;
                    FinancialRepresentative.MobileNoEntry = false;
                    FinancialRepresentative.EmailIDEntry = false;

                    Declaration.Parent = false;
                    Declaration.AcknowledgementCB = false;
                    Declaration.IDTypeOrNoPicker = false;
                    Declaration.IDTypeOrNoEntry = false;
                    Declaration.DOBEntry = false;
                    IsDeclarationDOBVisible = false;
                    Declaration.ContactNameEntry = false;



                    break;
                case PageExecutionType.Reactivation:
                    InstAndCondition.CBAgreeCondition = true;

                    TaxPayerDetails.Parent = false;
                    TaxPayerDetails.TaxPayerDetailsParent = false;

                    TaxPayerDetails.TinEntry1 = false;
                    TaxPayerDetails.TinEntry2 = false;
                    TaxPayerDetails.MainOutletEntry1 = false;
                    TaxPayerDetails.MainOutletEntry2 = false;
                    TaxPayerDetails.StartDateEntry = false;
                    TaxPayerDetails.AddressEntry1 = false;
                    TaxPayerDetails.AddressEntry2 = false;
                    TaxPayerDetails.SourceEntry = false;

                    TaxPayerDetails.AddInformationCB = false;
                    TaxPayerDetails.AddInformationCBVisible = false;
                    TaxPayerDetails.AddInformationParent = false;
                    IsTaxPayerIBANEnabled = false;
                    IsTaxPayerEligDateEnabled = true;

                    TaxPayerDetails.ImporterYesRB = false;
                    TaxPayerDetails.ImporterNoRB = false;
                    TaxPayerDetails.ImporterAttachmentsBtn = false;
                    TaxPayerDetails.ExporterYesRB = false;
                    TaxPayerDetails.ExporterNoRB = false;
                    TaxPayerDetails.ExporterrAttachmentsBtn = false;
                    TaxPayerDetails.ExistingIBANPicker = false;
                    TaxPayerDetails.NewIBANPicker = false;
                    TaxPayerDetails.CommencementDate = true;

                    FinancialRepresentative.Parent = false;
                    FinancialRepresentative.ChangeMobileEmailCB = false;
                    FinancialRepresentative.AddNewFinRepresentativeCB = false;
                    FinancialRepresentative.AddNewFinRepCBVisible = false;
                    IsNewFinancialRepVisible = false;
                    FinancialRepresentative.SkipBtn = false;


                    FinancialRepresentative.TinEntry = false;
                    FinancialRepresentative.IDTypeEntry = false;
                    FinancialRepresentative.IDNoEntry = false;
                    FinancialRepresentative.FNameEntry = false;
                    FinancialRepresentative.SurnameEntry = false;
                    FinancialRepresentative.MobileNoEntry = false;
                    FinancialRepresentative.EmailIDEntry = false;

                    Declaration.Parent = true;
                    Declaration.AcknowledgementCB = true;
                    Declaration.IDTypeOrNoPicker = true;
                    Declaration.IDTypeOrNoEntry = true;
                    Declaration.DOBEntry = false;
                    IsDeclarationDOBVisible = false;
                    Declaration.ContactNameEntry = true;


                    break;
                default:
                    InstAndCondition.CBAgreeCondition = true;

                    break;
            }
        }


        public VATAmendReactivationPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            if (App.VATType == PageExecutionType.Amend)
            {
                PageTitle = AppResources.ZZZZVatRegistrationAmendmentTile;
                PageType = (int)PageExecutionType.Amend;
            }
            else if (App.VATType == PageExecutionType.Reactivation)
            {
                PageTitle = AppResources.ZZZZVatRegistrationReactivationTile;
                PageType = (int)PageExecutionType.Reactivation;
            }
            else if (App.VATType == PageExecutionType.Register)
            {
                PageTitle = AppResources.ZZZZVatRegistrationTile;
                PageType = (int)PageExecutionType.Register;
            }

        }

        #region Method


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
                    string[] date1 = VatEligibleStartDate.Split('/');
                    Bdt = date1[2] + "-" + date1[1] + "-" + date1[0] + "T00:00:00";

                }

                //Step 4
                if (IsAddNewRepresentativeChecked)
                {

                    ResultsItemForContactPerson contactSet = new ResultsItemForContactPerson();
                    contactSet.TransactionType = "CHG_RGVT";//VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].TransactionType;
                    contactSet.FormGuid = VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].FormGuid;
                    contactSet.DataVersion = VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].DataVersion;
                    contactSet.LineNo = VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].LineNo;
                    contactSet.RankingOrder = VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].RankingOrder;
                    contactSet.Srcidentify = VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Srcidentify;
                    contactSet.Contacttp = VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Contacttp;
                    contactSet.Relationtp = VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Relationtp;
                    contactSet.Defaultfg = VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Defaultfg;
                    contactSet.Startdt = VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Startdt;
                    contactSet.StartdtC = VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].StartdtC;
                    contactSet.Enddt = VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Enddt;
                    contactSet.Dobdt = VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Dobdt;
                    contactSet.Fathernm = string.Empty;
                    contactSet.Grandfathernm = string.Empty;
                    contactSet.Familynm = string.Empty;
                    contactSet.Title = string.Empty;
                    contactSet.Initials = string.Empty;

                    contactSet.Gpart = GpartFR;
                    if (SelectedIdTypeFR != null)
                    {
                        contactSet.Type = SelectedIdTypeFR.ID;
                    }
                    contactSet.Idnumber = IdnumberFR;
                    contactSet.Firstnm = FirstnmFR;
                    contactSet.Lastnm = LastnmFR;


                    VATRegistrationDetailsData.d.CONTACT_PERSONSet.results =
                        VATRegistrationDetailsData.d.CONTACT_PERSONSet.results.Select(x => { x.__metadata = null; return x; }).ToList();
                    VATRegistrationDetailsData.d.CONTACT_PERSONSet.results.Add(contactSet);

                    bool checkDuplicate = false;
                    foreach (var item in VATRegistrationDetailsData.d.CONTACT_PERSONSet.results)
                    {
                        checkDuplicate = JsonCompare(item, contactSet);
                        if (checkDuplicate)
                            break;
                    }
                    if (!checkDuplicate)
                        VATRegistrationDetailsData.d.CONTACT_PERSONSet.results.Add(contactSet);

                    checkDuplicate = false;
                    ResultsItemForContact contact = new ResultsItemForContact();

                    contact.TransactionType = "CHG_RGVT";
                    contact.FormGuid = VATRegistrationDetailsData.d.CONTACTDTSet.results[0].FormGuid;
                    contact.DataVersion = VATRegistrationDetailsData.d.CONTACTDTSet.results[0].DataVersion;
                    contact.LineNo = VATRegistrationDetailsData.d.CONTACTDTSet.results[0].LineNo;
                    contact.RankingOrder = VATRegistrationDetailsData.d.CONTACTDTSet.results[0].RankingOrder;
                    contact.Srcidentify = VATRegistrationDetailsData.d.CONTACTDTSet.results[0].Srcidentify;
                    contact.Consnumber = VATRegistrationDetailsData.d.CONTACTDTSet.results[0].Consnumber;
                    contact.Begda = null;
                    contact.Endda = null;
                    contact.TelNumber = MobNumberFR;
                    contact.R3User = string.Empty;
                    contact.MobNumber = MobNumberFR;
                    contact.SmtpAddr = SmtpAddrFR;
                    VATRegistrationDetailsData.d.CONTACTDTSet.results =
                     VATRegistrationDetailsData.d.CONTACTDTSet.results.Select(x => { x.__metadata = null; return x; }).ToList();


                    foreach (var item in VATRegistrationDetailsData.d.CONTACTDTSet.results)
                    {
                        checkDuplicate = JsonCompare(item, contact);
                        if (checkDuplicate)
                            break;
                    }
                    if (!checkDuplicate)
                        VATRegistrationDetailsData.d.CONTACTDTSet.results.Add(contact);
                }

                if (IsChangeEmailChecked)
                {


                    ResultsItemForContact contact = new ResultsItemForContact();

                    contact.TransactionType = "CHG_RGVT";
                    contact.FormGuid = VATRegistrationDetailsData.d.CONTACTDTSet.results[0].FormGuid;
                    contact.DataVersion = VATRegistrationDetailsData.d.CONTACTDTSet.results[0].DataVersion;
                    contact.LineNo = VATRegistrationDetailsData.d.CONTACTDTSet.results[0].LineNo;
                    contact.RankingOrder = VATRegistrationDetailsData.d.CONTACTDTSet.results[0].RankingOrder;
                    contact.Srcidentify = VATRegistrationDetailsData.d.CONTACTDTSet.results[0].Srcidentify;
                    contact.Consnumber = VATRegistrationDetailsData.d.CONTACTDTSet.results[0].Consnumber;
                    contact.Begda = null;
                    contact.Endda = null;
                    contact.TelNumber = PrimaryMobNumberFR;
                    contact.R3User = string.Empty;
                    contact.MobNumber = PrimaryMobNumberFR;
                    contact.SmtpAddr = PrimarySmtpAddrFR;

                    VATRegistrationDetailsData.d.CONTACTDTSet.results =
                     VATRegistrationDetailsData.d.CONTACTDTSet.results.Select(x => { x.__metadata = null; return x; }).ToList();

                    VATRegistrationDetailsData.d.CONTACTDTSet.results.Add(contact);

                    var updatedContactList = VATRegistrationDetailsData.d.CONTACTDTSet.results.GroupBy(x => x.MobNumber).Select(x => x.First()).ToList();
                    VATRegistrationDetailsData.d.CONTACTDTSet.results = updatedContactList;
                }

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
                if (IsChangeEmailChecked)
                {
                    VATRegistrationDetailsData.d.Stp4Cbbox1 = "1";
                }
                else
                {
                    VATRegistrationDetailsData.d.Stp4Cbbox1 = "0";

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
            catch (Exception)
            {


            }
        }

        private bool JsonCompare(object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if (obj == null || another == null) return false;
            if (obj.GetType() != another.GetType()) return false;

            var objJson = JsonConvert.SerializeObject(obj);
            var anotherJson = JsonConvert.SerializeObject(another);
            return objJson == anotherJson;
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
                for (int i = 0; i < ListFinanceRepresenatives.Count(); i++)
                {
                    VATRegistrationDetailsData.d.CONTACTDTSet.results[i].MobNumber = ListFinanceRepresenatives[i].MobNumberFR;
                    VATRegistrationDetailsData.d.CONTACTDTSet.results[i].SmtpAddr = ListFinanceRepresenatives[i].SmtpAddrFR;
                }
                var ATTDETSetnew = VATRegistrationDetailsData.d.ATTDETSet;
                response = await VatRegistrationWebServiceManager.SaveVATRegistrationData(VATRegistrationDetailsData);
                PopToRootPage();
                if (TempDataContacts != null)
                {
                    var items = new List<FinancialRepresentativesModel>();
                    for (int i = 0; i < ListFinanceRepresenatives.Count(); i++)
                    {
                        FinancialRepresentativesModel financialRepresentativesModel = ListFinanceRepresenatives[i];
                        financialRepresentativesModel.MobNumberFR = VATRegistrationDetailsData.d.CONTACTDTSet.results[i].MobNumber;
                        financialRepresentativesModel.SmtpAddrFR = VATRegistrationDetailsData.d.CONTACTDTSet.results[i].SmtpAddr;
                        items.Add(financialRepresentativesModel);
                    }
                    ListFinanceRepresenatives = items;
                }

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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));
                                _navigationService.GoBack();
                            }
                            if (response.d.Operationz.Equals("05"))
                            {
                                string displayMessage = AppResources.VATRSaveasdraftMessage;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));
                                if (IsAddAdditionalInfoChecked)
                                {
                                    IsAddAdditionalInfoChecked = true;
                                }
                            }
                            VATRegistrationDetailsData = response;
                            VATRegistrationDetailsData.d.ATTDETSet = ATTDETSetnew;
                            VATRegistrationDetailsData.d.ATTDETSet = ATTDETSetObject;
                            //Set data after api call 
                            await setDataAfterSubmitAPIAsync(response);
                        }
                        IsLoading = false;
                        return response;
                    }
                    catch (Exception)
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                });
                return response;
            }
            catch (Exception)
            {


                return response;
            }
        }

        public async Task setDataAfterSubmitAPIAsync(VATRegistrationDetails vATRegistration)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                VATRegistrationOtherDetails vATRegistrationOther = await VatRegistrationWebServiceManager.GAZTGetVATRegistrationDataWithButtons(vATRegistration.d.Fbnumz, vATRegistration.d.Officerz, vATRegistration.d.Statusz, vATRegistration.d.TxnTpz, "ZTAX_VT_REG");

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

                string value1forimage3first = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(x => x.QueNo == "003" && x.QoptNo == "031")?.Select(x => x.QoptAns)?.FirstOrDefault();
                string value2forimage3second = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(x => x.QueNo == "003" && x.QoptNo == "032")?.Select(x => x.QoptAns)?.FirstOrDefault();

                string value1forimage4first = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(x => x.QueNo == "004" && x.QoptNo == "041")?.Select(x => x.QoptAns).FirstOrDefault();
                string value2forimage4second = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(x => x.QueNo == "004" && x.QoptNo == "042")?.Select(x => x.QoptAns).FirstOrDefault();

                if (value1forimage3first == "1")
                {
                    quesTion3answerSelected = TextQuestion3First;
                    ImageforTextQuestion3First = "selected171x136.png";
                    TextQuestion3FirstTextColor = Colors.White;
                }
                else
                {
                    ImageforTextQuestion3First = "unselected171x136.png";
                    TextQuestion3FirstTextColor = (Color)Application.Current.Resources["Primary"];
                }

                if (value2forimage3second == "1")
                {
                    quesTion3answerSelected = TextQuestion3Second;
                    ImageforTextQuestion3Second = "selected171x136.png";
                    TextQuestion3SecondTextColor = Colors.White;
                }
                else
                {
                    ImageforTextQuestion3Second = "unselected171x136.png";
                    TextQuestion3SecondTextColor = (Color)Application.Current.Resources["Primary"];
                }

                if (value1forimage4first == "1")
                {
                    quesTion4answerSelected = TextQuestion4First;
                    ImageforTextQuestion4First = "selected171x136.png";
                    TextQuestion4FirstTextColor = Colors.White;
                }
                else
                {
                    ImageforTextQuestion4First = "unselected171x136.png";
                    TextQuestion4FirstTextColor = (Color)Application.Current.Resources["Primary"];
                }

                if (value2forimage4second == "1")
                {
                    quesTion4answerSelected = TextQuestion4Second;
                    ImageforTextQuestion4Second = "selected171x136.png";
                    TextQuestion4SecondTextColor = Colors.White;
                }
                else
                {
                    ImageforTextQuestion4Second = "unselected171x136.png";
                    TextQuestion4SecondTextColor = (Color)Application.Current.Resources["Primary"];
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }

        private string GetLocalisedButtonString(string ButtonName)
        {
            string LocalisedButtonString = string.Empty;
            if (0 == string.Compare(ButtonName, "SaveasDraft"))
            {
                LocalisedButtonString = AppResources.ZZSaveAsDraft;
            }
            else if (0 == string.Compare(ButtonName, "Attachments"))
            {
                LocalisedButtonString = AppResources.Attachments;
            }
            else if (0 == string.Compare(ButtonName, "Void"))
            {
                LocalisedButtonString = AppResources.ZZVoid;
            }
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
                        if (App.VATType == PageExecutionType.Amend)
                            pageType = "05";
                        else if (App.VATType == PageExecutionType.Reactivation)
                            pageType = "07";
                        else
                            pageType = "04";

                        vATRegistration = await VatRegistrationWebServiceManager.GAZTGetVATRegistrationData(pageType);
                        VatDeregDeclaration = await VatRegistrationWebServiceManager.GAZTGetVATDeRegistrationDeclaration(vATRegistration.d.Fbnumz);

                        if (vATRegistration != null && vATRegistration.d != null)
                        {
                            if (App.VATType == PageExecutionType.Amend)
                            {
                                if (App.isVatEffectDateNav)
                                {
                                    if (vATRegistration.d.TxnTpz == "VT_EFDT")
                                    {
                                        IsCheckEnabled = false;
                                        IsChangeEmailCheckBoxEnabled = false;
                                        IsAddFinancialRepresentativeCheckBoxEnabled = false;
                                        VatEligibleStartDateInsVisible = false;
                                        IsNewVatEligibleDateInsVisible = true;
                                    }

                                    else
                                        IsCheckEnabled = true;

                                }
                            }
                            //step 4 and 5 data set
                            if (vATRegistration.d.CONTACT_PERSONSet != null)
                            {
                                if (vATRegistration.d.CONTACT_PERSONSet.results != null)
                                {
                                    if (!string.IsNullOrEmpty(vATRegistration.d.CONTACT_PERSONSet.results[0].Idnumber))
                                    {
                                        MainThread.BeginInvokeOnMainThread(() =>
                                        {
                                            if (App.isVatEffectDateNav)
                                            {
                                                if (vATRegistration.d.TxnTpz == "VT_EFDT")
                                                {
                                                    IsCheckEnabled = false;
                                                    IsChangeEmailCheckBoxEnabled = false;
                                                    IsAddFinancialRepresentativeCheckBoxEnabled = false;
                                                    VatEligibleStartDateInsVisible = false;
                                                    IsNewVatEligibleDateInsVisible = true;
                                                }

                                            }
                                            else
                                            {
                                                IsChangeEmailCheckBoxEnabled = true;
                                            }

                                        });
                                    }

                                    else
                                    {
                                        MainThread.BeginInvokeOnMainThread(() =>
                                        {
                                            IsChangeEmailCheckBoxEnabled = false;
                                        });
                                    }
                                }
                                GpartFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Gpart;
                                IdnumberFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Idnumber;
                                FirstnmFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Firstnm;
                                LastnmFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Lastnm;
                                PrimaryMobNumberFR = vATRegistration.d.CONTACTDTSet.results[0].MobNumber;
                                PrimarySmtpAddrFR = vATRegistration.d.CONTACTDTSet.results[0].SmtpAddr;
                                try
                                {
                                    TxtIDTypeFR = IdTypeListFR.Where(x => x.ID == vATRegistration.d.CONTACT_PERSONSet.results[0].Type)?.FirstOrDefault()?.Name;
                                }
                                catch (Exception)
                                {


                                }

                                GpartSum = vATRegistration.d.CONTACT_PERSONSet.results[0].Gpart;
                                IdnumberSum = vATRegistration.d.CONTACT_PERSONSet.results[0].Idnumber;
                                FirstnmSum = vATRegistration.d.CONTACT_PERSONSet.results[0].Firstnm;
                                LastnmSum = vATRegistration.d.CONTACT_PERSONSet.results[0].Lastnm;
                                PrimaryMobNumberFR = vATRegistration.d.CONTACTDTSet.results[0].MobNumber;
                                PrimarySmtpAddrFR = vATRegistration.d.CONTACTDTSet.results[0].SmtpAddr;
                                try
                                {
                                    TxtIDTypeSum = IdTypeListFR.Where(x => x.ID == vATRegistration.d.CONTACT_PERSONSet.results[0].Type)?.FirstOrDefault()?.Name;
                                }
                                catch (Exception)
                                {


                                }

                                if (App.VATType == PageExecutionType.Amend)
                                {
                                    GpartFR = string.Empty;
                                    IdnumberFR = string.Empty;
                                    FirstnmFR = string.Empty;
                                    LastnmFR = string.Empty;
                                    PrimarySmtpAddrFR = string.Empty;
                                    PrimaryMobNumberFR = string.Empty;
                                    TxtIDTypeFR = string.Empty;
                                }
                                if (App.VATType == PageExecutionType.Amend || App.VATType == PageExecutionType.Reactivation)
                                {

                                    var listFRep = new List<FinancialRepresentativesModel>();
                                    int count = 0;
                                    foreach (var item in vATRegistration.d.CONTACT_PERSONSet.results)
                                    {
                                        try
                                        {
                                            SelectedIdTypeFR = IdTypeListFR.Where(x => x.ID == vATRegistration.d.CONTACT_PERSONSet.results[0].Type)?.FirstOrDefault();

                                            string selectedIdTypeName = "";

                                            if (SelectedIdTypeFR != null && string.IsNullOrEmpty(SelectedIdTypeFR.Name))
                                            {

                                                selectedIdTypeName = SelectedIdTypeFR.Name;
                                            }
                                            listFRep.Add(new FinancialRepresentativesModel()
                                            {
                                                GpartFR = item.Gpart,
                                                IdnumberFR = item.Idnumber,
                                                FirstnmFR = item.Firstnm,
                                                LastnmFR = item.Lastnm,
                                                MobNumberFR = vATRegistration.d.CONTACTDTSet.results[count].MobNumber,
                                                SmtpAddrFR = vATRegistration.d.CONTACTDTSet.results[count].SmtpAddr,
                                                TxtIDTypeFR = IdTypeListFR.Where(x => x.ID == vATRegistration.d.CONTACT_PERSONSet.results[count].Type)?.FirstOrDefault()?.Name

                                            });
                                            TempDataContacts.Add(new TempDataContact
                                            {
                                                tempEmail = vATRegistration.d.CONTACTDTSet.results[count].SmtpAddr,
                                                tempmobile = vATRegistration.d.CONTACTDTSet.results[count].MobNumber
                                            });
                                            count++;
                                        }
                                        catch (Exception)
                                        {


                                        }
                                    }
                                    ListFinanceRepresenatives = listFRep;
                                }
                                try
                                {
                                    SelectedIdTypeFR = IdTypeListFR.Where(x => x.ID == vATRegistration.d.CONTACT_PERSONSet.results[0].Type).FirstOrDefault();

                                    TxtIDTypeSum = IdTypeListFR.Where(x => x.ID == vATRegistration.d.CONTACT_PERSONSet.results[0].Type).FirstOrDefault()?.Name;
                                }
                                catch (Exception)
                                {


                                }
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
                                AddAdditionalInfoCheckBoxEnabled = false;
                            }
                            else if (vATRegistration.d.Stp2Cbbox == "0" || string.IsNullOrEmpty(vATRegistration.d.Stp2Cbbox))
                            {
                                IsAddAdditionalInfoChecked = false;
                                AddAdditionalInfoCheckBoxEnabled = true;
                            }
                            if (vATRegistration.d.Stp3Cbbox == "1")
                            {
                                IsFDChangeSectionChecked = true;
                                IsFinancialDChangeSectionEnabled = false;
                            }
                            else if (vATRegistration.d.Stp3Cbbox == "0" || string.IsNullOrEmpty(vATRegistration.d.Stp3Cbbox))
                            {
                                IsFDChangeSectionChecked = false;
                                IsFinancialDChangeSectionEnabled = true;
                            }
                            if (vATRegistration.d.Stp4Cbbox1 == "1")
                            {
                                IsChangeEmailChecked = true;
                            }
                            else
                            {
                                IsChangeEmailChecked = false;

                            }
                            if (vATRegistration.d.Stp4Cbbox2 == "1")
                            {
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    IsAddNewRepresentativeChecked = true;
                                    IsNewFinancialRepVisible = true;
                                });
                            }
                            else if (vATRegistration.d.Stp4Cbbox2 == "0")
                            {
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    IsAddNewRepresentativeChecked = false;
                                    IsNewFinancialRepVisible = false;
                                });
                            }
                            switch (App.VATType)
                            {
                                case PageExecutionType.Amend:
                                    if (App.isVatEffectDateNav)
                                    {
                                        if (vATRegistration.d.TxnTpz == "VT_EFDT")
                                        {
                                            IsCheckEnabled = false;
                                            IsChangeEmailCheckBoxEnabled = false;
                                            IsAddFinancialRepresentativeCheckBoxEnabled = false;
                                        }
                                        else
                                            IsCheckEnabled = true;

                                    }

                                    IsInstrunctionChecked = true;
                                    break;
                                case PageExecutionType.Reactivation:
                                    IsInstrunctionChecked = true;
                                    break;
                                case PageExecutionType.Register:
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
                                SelectedIdTypeSR = IdTypeListSR.Where(x => x.ID == vATRegistration.d.DecidTy)?.FirstOrDefault();
                                TxtIDTypeSR = IdTypeListSR.Where(x => x.ID == vATRegistration.d.DecidTy)?.FirstOrDefault()?.Name;
                            }
                            if (vATRegistration.d.Decconno != null)
                                IdNumberSR = vATRegistration.d.DecidNo;
                            FirstNameSR = vATRegistration.d.Decname;

                            if (vATRegistration.d.CrStdt != null)
                            {
                                StartdateToshow = JsonConvert.DeserializeObject<DateTime>(@"""" + vATRegistration.d.CrStdt + @"""").ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                            }
                            VATRegistrationDetailsData = vATRegistration;
                            setIban();
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
                                ImporterTextColor = Colors.White;
                            }
                            else
                            {
                                ImporterImageSource = "vat_tile_IbanCard_background_white.png";
                                ImporterTextColor = (Color)Application.Current.Resources["Primary"];
                            }
                            if (VATRegistrationDetailsData.d.ExFg == "1")
                            {
                                ExporterImageSource = "vat_tile_IbanCard_background.png";
                                ExporterTextColor = Colors.White;
                            }
                            else
                            {
                                ExporterImageSource = "vat_tile_IbanCard_background_white.png";
                                ExporterTextColor = (Color)Application.Current.Resources["Primary"];
                            }
                            if (VATRegistrationDetailsData.d.QUESCONFIG_MSet.results.Count != 0)
                            {
                                MinMaxRanges = new List<QuestionNumberWithMinMaxRange>();
                                MinMaxRanges = UtilityManager.GetLowAndHighRangeForEachQuestionSet(VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                                MaximumDisplayValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.MaxRangeValue)?.FirstOrDefault() == null ? 0 : MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.MaxRangeValue).FirstOrDefault();
                                MinimumDisplayValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.MinRangeValue)?.FirstOrDefault() == null ? 0 : MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.MinRangeValue).FirstOrDefault();

                                MaximumDisplayValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.MaxRangeValue)?.FirstOrDefault() == null ? 0 : MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.MaxRangeValue).FirstOrDefault();
                                MinimumDisplayValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.MinRangeValue)?.FirstOrDefault() == null ? 0 : MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.MinRangeValue).FirstOrDefault();

                                MaximumValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.CountOfProbableAnswersForThisQuestions).FirstOrDefault() - 1;
                                MaximumValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.CountOfProbableAnswersForThisQuestions).FirstOrDefault() - 1;

                                TextQuestion3First = VATRegistrationDetailsData.d.QUESCONFIG_MSet.results.Where(x => x.QueNo == "003" && x.QoptNo == "031")?.Select(x => x.QoptTxt)?.FirstOrDefault();
                                TextQuestion3Second = VATRegistrationDetailsData.d.QUESCONFIG_MSet.results.Where(x => x.QueNo == "003" && x.QoptNo == "032").Select(x => x.QoptTxt).FirstOrDefault();

                                TextQuestion4First = VATRegistrationDetailsData.d.QUESCONFIG_MSet.results.Where(x => x.QueNo == "004" && x.QoptNo == "041")?.Select(x => x.QoptTxt)?.FirstOrDefault();
                                TextQuestion4Second = VATRegistrationDetailsData.d.QUESCONFIG_MSet.results.Where(x => x.QueNo == "004" && x.QoptNo == "042")?.Select(x => x.QoptTxt)?.FirstOrDefault();

                                setQuestionImage();
                            }
                            if (VATRegistrationDetailsData.d.QUESTIONSSet != null)
                            {
                                answer1selectedcount = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(s => s.QueNo == "001" && s.QoptAns == "1").Count();
                                answer2selectedcount = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(s => s.QueNo == "002" && s.QoptAns == "1").Count();
                                answer3selectedcount = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(s => s.QueNo == "003" && s.QoptAns == "1").Count();
                                answer4selectedcount = VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(s => s.QueNo == "004" && s.QoptAns == "1").Count();
                            }

                            vATRegistrationOther = await VatRegistrationWebServiceManager.GAZTGetVATRegistrationDataWithButtons(vATRegistration.d.Fbnumz, vATRegistration.d.Officerz, vATRegistration.d.Statusz, vATRegistration.d.TxnTpz, "ZTAX_VT_REG");
                            PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                            if (vATRegistrationOther != null && vATRegistrationOther.d != null)
                            {
                                VATRegistrationOtherDetails = vATRegistrationOther;
                                SetApplicableButtons();
                            }
                            OriginalData = JsonConvert.SerializeObject(VATRegistrationDetailsData);
                            VATRegistrationData = JsonConvert.DeserializeObject<VATRegistrationDetails>(OriginalData);
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
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
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
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
            catch (GAZTVATRegistrationInProcessException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    _navigationService.GoBack();
                });
            }
            catch (Exception)
            {


                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }

        private void SetApplicableButtons()
        {
            if (VATRegistrationOtherDetails.d.VR_UI_BTNSet != null && VATRegistrationOtherDetails.d.VR_UI_BTNSet.results != null)
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
                    string localisedString = GetLocalisedButtonString(buttonEnumId.ToString());

                    if (false == string.IsNullOrEmpty(localisedString))
                        ListOfActionButtonsApplicableForRegistration.Add(localisedString);
                }
            }
        }

        public void setIban()
        {
            if (VATRegistrationDetailsData.d.IBANSet != null && VATRegistrationDetailsData.d.IBANSet.results != null)
            {
                foreach (var item in VATRegistrationDetailsData.d.IBANSet.results)
                {
                    if (!string.IsNullOrEmpty(item.Bkvid))
                    {
                        IbanList.Add(item);
                    }
                }
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

                TxtIDTypeFR = IdTypeListFR[IDTypeIndexFR].Name;
                TxtIDTypeSR = IdTypeListSR[IDTypeIndexSR].Name;
                SelectedIdTypeFR = IdTypeListFR[IDTypeIndexFR];
                SelectedIdTypeSR = IdTypeListSR[IDTypeIndexSR];
            }
            catch (Exception)
            {


            }
        }

        public void SetDefaultDate()
        {
            List<object> todaycollection = new List<object>();

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

        private bool IsFinancialRepresentativeDataAvailable(List<FinancialRepresentativesModel> financialRepresentativeList)
        {
            if (!string.IsNullOrEmpty(financialRepresentativeList[0].FirstnmFR) ||
                !string.IsNullOrEmpty(financialRepresentativeList[0].GpartFR) ||
                !string.IsNullOrEmpty(financialRepresentativeList[0].IdnumberFR) ||
                !string.IsNullOrEmpty(financialRepresentativeList[0].LastnmFR) ||
                !string.IsNullOrEmpty(financialRepresentativeList[0].MobNumberFR) ||
                !string.IsNullOrEmpty(financialRepresentativeList[0].SmtpAddrFR) ||
                !string.IsNullOrEmpty(financialRepresentativeList[0].GpartFR) ||
                financialRepresentativeList[0].TxtIDTypeFR != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task getVatEligibleDate(string vatEligibleStartDate)
        {
            //await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp("we can proceed now"));

            // throw new NotImplementedException();

            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                VatCommencementDateFormat vATcommencementData = await VatRegistrationWebServiceManager.GAZTGetVATEligibilityDate(vatEligibleStartDate + "T00:00:00", "");

                PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                if (vATcommencementData.d.ErrorFg == "X")
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATEligibleDateError1));
                    IsLoading = false;
                }
                else
                {
                    if (vATcommencementData != null && vATcommencementData.d != null
                    && vATcommencementData.d.__metadata != null && vATcommencementData.d.__metadata.uri != null)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(vATcommencementData.d.__metadata.uri))
                            {
                                string dateSource = UtilityManager.DDMMFormatDateToYYYYFromDateTypeString(vATcommencementData.d.VatTaxDt);
                                VatEligibleStartDate = dateSource;
                            }
                            IsLoading = false;
                        }
                        catch (Exception)
                        {


                            IsLoading = false;
                        }
                    }
                }
                IsLoading = false;
            });
        }

        public async Task GetNewVatEligibleDateAsync(string newVatDate)
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                await Task.Run(async () =>
                {
                    VatCommencementDateFormat vATcommencementData = await VatRegistrationWebServiceManager.GAZTGetVATEligibilityDate(newVatDate + "T00:00:00", "VT_EFDT");

                    PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                    if (vATcommencementData != null && vATcommencementData.d != null && vATcommencementData.d.VatTaxDt != null)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(vATcommencementData.d.__metadata.uri))
                            {
                                //String dateSource = await filerDateFromResponse(vATcommencementData.d.__metadata.uri);

                                // VatEligibleStartDate = String.Join("-", dateSource.Split('-').Reverse());
                                NewVatEligibleStartDate = UtilityManager.DDMMFormatDateToYYYYFromDateTypeString(vATcommencementData.d.VatTaxDt);

                                //isNewEligibleStartDateIsValid = true;
                                CheckCR1450FieldsValid();

                            }
                            IsLoading = false;
                        }
                        catch (Exception)
                        {


                            IsLoading = false;
                            NewVatEligibleStartDate = string.Empty;
                        }
                    }
                    IsLoading = false;
                });
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    NewVatEligibleStartDate = string.Empty;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    NewVatEligibleStartDate = string.Empty;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                    IsLoading = false;
                });
            }
        }

        public void CheckCR1450FieldsValid()
        {
            if (IsNewStartDateInfoChecked && !string.IsNullOrEmpty(NewVatEligibleStartDate))
            {
                IsContinueButtonEnable = true;
            }
            else
            {
                IsContinueButtonEnable = false;
            }
        }

        private async Task<string> filerDateFromResponse(string uri)
        {
            try
            {
                int startPos = uri.LastIndexOf("taxDateSet(datetime'") + "taxDateSet(datetime'".Length;
                int length = uri.IndexOf("T00%3A00%3A00')") - startPos;
                string sub = uri.Substring(startPos, length);
                return sub;
            }
            catch (Exception)
            {


                return "";
            }

        }
        #endregion
    }
}