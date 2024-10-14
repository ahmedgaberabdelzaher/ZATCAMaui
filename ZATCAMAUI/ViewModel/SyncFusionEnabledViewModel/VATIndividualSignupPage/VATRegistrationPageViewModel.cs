

using Newtonsoft.Json;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{

    public class VATRegistrationPageViewModel : BaseViewModel
    {
        string idnumber { get; set; }
        public int DefaultMonth;
        public static IsComeFromForAttachment IsComeFromForAttachment;

        #region Variable


        private int _currenrIndex = 0;
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
            }
        }

        public ICommand onMoreOptionClicked { get; set; }

        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 5;
        #endregion


        #region Properties

        private string _iqamaTypeDesc = string.Empty;
        public string IqamaTypeDesc
        {
            get
            {
                return _iqamaTypeDesc;
            }
            set
            {
                _iqamaTypeDesc = value;
                OnPropertyChanged("IqamaTypeDesc");
            }
        }

        private bool _showIqamaTypeDesc = false;
        public bool ShowIqamaTypeDesc
        {
            get
            {
                return _showIqamaTypeDesc;
            }
            set
            {
                _showIqamaTypeDesc = value;
                OnPropertyChanged("ShowIqamaTypeDesc");
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
                OnPropertyChanged("ContinueButtonnBackroundColor");
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
                OnPropertyChanged("answer1selectedcount");
            }
        }
        private List<Attachment> _ATTDETSetObject;
        public List<Attachment> ATTDETSetObject
        {
            get
            {
                return _ATTDETSetObject;
            }
            set
            {
                if (_ATTDETSetObject == value) return;

                _ATTDETSetObject = value;
                OnPropertyChanged("ATTDETSetObject");
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
                OnPropertyChanged("answer2selectedcount");
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
                OnPropertyChanged("answer3selectedcount");
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
                OnPropertyChanged("answer4selectedcount");
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

                OnPropertyChanged("IsFDNameMobEmailEnable");
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

                OnPropertyChanged("IsBackStepButtonVisible");
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

                OnPropertyChanged("IDNumberNonMandatoryVisibility");
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

                OnPropertyChanged("IDNumberMandatoryVisibility");
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

                OnPropertyChanged("IDNumberVisibility");
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

                OnPropertyChanged("DOBNonMandatoryVisibility");
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

                OnPropertyChanged("DOBMandatoryVisibility");
            }
        }

        private bool _dOBMandatoryVisibilitySM = false;
        public bool DOBMandatoryVisibilitySM
        {
            get
            {
                return _dOBMandatoryVisibilitySM;
            }
            set
            {
                if (_dOBMandatoryVisibilitySM == value) return;

                _dOBMandatoryVisibilitySM = value;

                OnPropertyChanged("DOBMandatoryVisibilitySM");
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

                OnPropertyChanged("isAttachmentImporterExporterVisible");
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
                OnPropertyChanged("IsContinueButtonEnable");
            }
        }

        private DateTime _vatRegDate;
        public DateTime VatRegDate
        {
            get
            {
                return _vatRegDate;
            }
            set
            {
                if (_vatRegDate == value) return;

                _vatRegDate = value;
                OnPropertyChanged("VatRegDate");
            }
        }

        public async Task getVatEligibleDate(string vatEligibleStartDate)
        {
            await Task.Run(async () =>
            {
                VatCommencementDateFormat vATcommencementData = await VatRegistrationWebServiceManager.GAZTGetVATEligibilityDate(vatEligibleStartDate + "T00:00:00", "");
                PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                if (vATcommencementData != null && vATcommencementData.d != null && vATcommencementData.d.VatTaxDt != null)
                {
                    try
                    {
                        String DateTimeToBeParsed = DateTime.Parse(vATcommencementData.d.VatTaxDt).ToString("yyyy-MM-ddTHH:mm:ss");
                        String dateSource = UtilityManager.DDMMFormatDateToYYYYFromDateTypeString(UtilityManager.ConvertDateStringtoDateTime(DateTimeToBeParsed, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));
                        DateTime ChangedDate = new DateTime(2018, 1, 1, 0, 0, 0);

                        int Result = DateTime.Compare((DateTime)UtilityManager.ConvertDateStringtoDateTime(DateTimeToBeParsed, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture), (DateTime)VatRegDate);
                        Result = 1;
                        if (Result < 0)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATEligibleDateError1));
                            VatEligibleStartDate = "";
                        }
                        else
                        {
                            DateTime convertedDate = UtilityManager.ConvertDateStringtoDateTime(DateTimeToBeParsed, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

                            int CompareDate = DateTime.Compare((DateTime)convertedDate, ChangedDate);

                            if (CompareDate < 0)
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATEligibleDateError1));
                                VatEligibleStartDate = dateSource;
                            }
                            else
                            {
                                if (vATcommencementData.d.ErrorFg == "X")
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATEligibleDateError1));
                                }
                                VatEligibleStartDate = dateSource;
                            }
                        }
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }
                }
                else
                {
                }
                IsLoading = false;

            });
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

                OnPropertyChanged("StartdateToshow");
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

                OnPropertyChanged("quesTion3answerSelected");
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

                OnPropertyChanged("quesTion4answerSelected");
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
                if (_todayDate == value) return;

                _todayDate = value;
                OnPropertyChanged("TodayDate");
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
                OnPropertyChanged("DOB");
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
                OnPropertyChanged("ContactDOB");
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
                OnPropertyChanged("MaxLengthID");
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
                OnPropertyChanged("MaxLengthIDSR");
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
                OnPropertyChanged("FrameContactIDError");
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
                OnPropertyChanged("FrameIDError");
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
                OnPropertyChanged("FrameContactDOBError");
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
                OnPropertyChanged("FrameDOBError");
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
                    CurrentIndex = 0;
                }
                else
                {
                    IsBackStepButtonVisible = true;
                }
                OnPropertyChanged("IsInstrunctionVisible");
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
                if (_isTaxPayersVisible)
                {
                    CurrentIndex = 1;
                }
                OnPropertyChanged("IsTaxPayersVisible");
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
                if (_isSalesVisible)
                {
                    CurrentIndex = 2;
                }
                OnPropertyChanged("IsSalesVisible");
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
                OnPropertyChanged("IsExpensesVisible");
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
                    CurrentIndex = 3;
                }
                OnPropertyChanged("IsFinancialVisible");
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
                if (_isSummaryVisible)
                {
                    CurrentIndex = 4;
                }
                OnPropertyChanged("IsSummaryVisible");
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
                if (_currentStep == value) return;

                _currentStep = value;
                OnPropertyChanged("CurrentStep");
            }
        }

        private bool _shouldLoad = true;
        public bool ShouldLoad
        {
            get
            {
                return _shouldLoad;
            }
            set
            {
                if (_shouldLoad == value) return;

                _shouldLoad = value;
                OnPropertyChanged("ShouldLoad");
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
                OnPropertyChanged("ListOfActionButtonsApplicableForRegistration");
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
                OnPropertyChanged("RegTypeCode");
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

                //if( _vATRegistrationDetailsData != null || _vATRegistrationDetailsData.d != null)
                //{
                //    if (_vATRegistrationDetailsData.d.PendingIbanMsg != "")
                //    {
                //        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANIncomplete));
                //        PopToRootPage();
                //    }
                //}

                OnPropertyChanged("VATRegistrationDetailsData");
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
                if (_minimumDisplayValueOfSlider1 == value) return;

                _minimumDisplayValueOfSlider1 = value;
                OnPropertyChanged("MinimumDisplayValueOfSlider1");
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
                if (_maximumDisplayValueOfSlider1 == value) return;

                _maximumDisplayValueOfSlider1 = value;
                OnPropertyChanged("MaximumDisplayValueOfSlider1");
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
                if (_maximumDisplayValueOfSlider2 == value) return;

                _maximumDisplayValueOfSlider2 = value;
                OnPropertyChanged("MaximumDisplayValueOfSlider2");
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
                if (_minimumDisplayValueOfSlider2 == value) return;

                _minimumDisplayValueOfSlider2 = value;
                OnPropertyChanged("MinimumDisplayValueOfSlider2");
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
                if (_maximumValueOfSlider2 == value) return;

                _maximumValueOfSlider2 = value;
                OnPropertyChanged("MaximumValueOfSlider2");
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
                if (_maximumValueOfSlider1 == value) return;

                _maximumValueOfSlider1 = value;
                OnPropertyChanged("MaximumValueOfSlider1");
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

                OnPropertyChanged("SliderLable1EligibilityText");
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
                OnPropertyChanged("SliderLable1");
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
                OnPropertyChanged("SliderLable2");
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
                if (_sliderCurrentValue1 == value) return;

                _sliderCurrentValue1 = value;
                OnPropertyChanged("SliderCurrentValue1");
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
                if (_sliderCurrentValue2 == value) return;

                _sliderCurrentValue2 = value;
                OnPropertyChanged("SliderCurrentValue2");
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
                OnPropertyChanged("MinMaxRanges");
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
                OnPropertyChanged("VATRegistrationOtherDetails");
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
                MessagingCenter.Send<VATRegistrationPageViewModel, bool>(this, "IsInstrunctionChecked", value);

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

                OnPropertyChanged("IsInstrunctionChecked");
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

                OnPropertyChanged("IsDeclarationChecked");
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
                OnPropertyChanged("IsResident");
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
                OnPropertyChanged("Attachments");
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
                OnPropertyChanged("ADDRESSSetData");
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
                if (_addressLineOne == value) return;

                _addressLineOne = value;
                OnPropertyChanged("AddressLineOne");
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
                if (_addressLineTwo == value) return;

                _addressLineTwo = value;
                OnPropertyChanged("AddressLineTwo");
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
                if (_vatEligibleStartDate == value) return;

                _vatEligibleStartDate = value;
                OnPropertyChanged("VatEligibleStartDate");
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
                if (_gpartFR == value) return;

                _gpartFR = value;
                OnPropertyChanged("GpartFR");
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
                if (_typeFR == value) return;

                _typeFR = value;
                OnPropertyChanged("TypeFR");
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
                if (_idnumberFR == value) return;

                _idnumberFR = value;
                OnPropertyChanged("IdnumberFR");
            }
        }
        //private String _title = string.Empty;
        //public String Title
        //{
        //    get
        //    {
        //        return _title;
        //    }
        //    set
        //    {
        //        if (_title == value) return;

        //        _title = value;
        //        OnPropertyChanged("Title");
        //    }
        //}
        //private bool _titleVisibility = false;
        //public bool TitleVisibility
        //{
        //    get
        //    {
        //        return _titleVisibility;
        //    }
        //    set
        //    {
        //        if (_titleVisibility == value) return;

        //        _titleVisibility = value;
        //        OnPropertyChanged("TitleVisibility");

        //    }
        //}
        private String _firstnmFR = string.Empty;
        public String FirstnmFR
        {
            get
            {
                return _firstnmFR;
            }
            set
            {
                if (_firstnmFR == value) return;

                _firstnmFR = value;
                OnPropertyChanged("FirstnmFR");
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
                if (_lastnmFR == value) return;

                _lastnmFR = value;
                OnPropertyChanged("LastnmFR");
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
                if (_mobNumberFR == value) return;

                _mobNumberFR = value;
                OnPropertyChanged("MobNumberFR");
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
                if (_idNumberSR == value) return;

                _idNumberSR = value;
                OnPropertyChanged("IdNumberSR");
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
                if (_firstNameSR == value) return;

                _firstNameSR = value;
                OnPropertyChanged("FirstNameSR");
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
                if (_smtpAddrFR == value) return;

                _smtpAddrFR = value;
                OnPropertyChanged("SmtpAddrFR");
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
                OnPropertyChanged("IdTypeListFR");
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
                OnPropertyChanged("IdTypeListSR");
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
                OnPropertyChanged("IDTypeIndexFR");
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
                OnPropertyChanged("IDTypeIndexSR");
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
                OnPropertyChanged("TxtIDType");
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
                OnPropertyChanged("TxtIDTypeFR");
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
                OnPropertyChanged("TxtIDTypeSR");
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
                    catch (Exception ex)
                    {
                    }

                }
                OnPropertyChanged("SelectedIdTypeFR");
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

                    { }
                }
                OnPropertyChanged("SelectedIdTypeSR");
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
                OnPropertyChanged("ImporterImageSource");
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
                OnPropertyChanged("ImporterTextColor");
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
                OnPropertyChanged("ExporterTextColor");
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
                OnPropertyChanged("ExporterImageSource");
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
                OnPropertyChanged("SelectedIban");
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
                if (_ibanList == value) return;

                _ibanList = value;
                OnPropertyChanged("IbanList");
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
                OnPropertyChanged("IsNewAccountClicked");
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
                OnPropertyChanged("NewAccountText");
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
                if (_textQuestion3First == value) return;

                _textQuestion3First = value;
                OnPropertyChanged("TextQuestion3First");
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
                OnPropertyChanged("TextQuestion3FirstTextColor");
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
                if (_textQuestion4First == value) return;

                _textQuestion4First = value;
                OnPropertyChanged("TextQuestion4First");
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
                OnPropertyChanged("TextQuestion4FirstTextColor");
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
                if (_textQuestion3Second == value) return;

                _textQuestion3Second = value;
                OnPropertyChanged("TextQuestion3Second");
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
                OnPropertyChanged("TextQuestion3SecondTextColor");
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
                if (_textQuestion4Second == value) return;

                _textQuestion4Second = value;
                OnPropertyChanged("TextQuestion4Second");
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
                OnPropertyChanged("TextQuestion4SecondTextColor");
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
                if (_imageforTextQuestion3First == value) return;

                _imageforTextQuestion3First = value;
                OnPropertyChanged("ImageforTextQuestion3First");
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
                if (_imageforTextQuestion3Second == value) return;

                _imageforTextQuestion3Second = value;
                OnPropertyChanged("ImageforTextQuestion3Second");
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
                if (_imageforTextQuestion4First == value) return;

                _imageforTextQuestion4First = value;
                OnPropertyChanged("ImageforTextQuestion4First");
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
                if (_imageforTextQuestion4Second == value) return;

                _imageforTextQuestion4Second = value;
                OnPropertyChanged("ImageforTextQuestion4Second");
            }
        }

        public void setMoreOptioButtons()
        {
            var listOfActionButtonsApplicable = new List<string>();

            ListOfActionButtonsApplicable = ListOfActionButtonsApplicableForRegistration;
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
                OnPropertyChanged("ListOfActionButtonsApplicable");
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
                OnPropertyChanged("IsDeclarationViewEnabled");
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
                OnPropertyChanged("IsDeclarationViewEnabledNew");
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

        private TextAlignment _termsAlignment;
        public TextAlignment TermsAlignment
        {
            get { return _termsAlignment; }
            set
            {
                if (_termsAlignment == value) return;
                _termsAlignment = value;
                OnPropertyChanged("TermsAlignment");
            }
        }
        #endregion

        public VATRegistrationPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            onMoreOptionClicked = new Command(async () =>
            {
                await MopupService.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(ListOfActionButtonsApplicable));
            });

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
                var Bdt = string.Empty;
                if (!string.IsNullOrEmpty(VatEligibleStartDate))
                {
                    Bdt = DateTime.Parse(VatEligibleStartDate).ToString("yyyy-MM-ddTHH:mm:ss");
                }
                VATRegistrationDetailsData.d.VatTaxDt = Bdt;
                VATRegistrationDetailsData.d.StepNumberz = "2";
                VATRegistrationDetailsData.d.DecidTy = string.Empty;
                //Step 4

                VATRegistrationDetailsData.d.CONTACT_PERSONSet[0].Gpart = GpartFR;
                if (SelectedIdTypeFR != null)

                    VATRegistrationDetailsData.d.CONTACT_PERSONSet[0].Type = SelectedIdTypeFR.ID;
                VATRegistrationDetailsData.d.CONTACT_PERSONSet[0].Idnumber = IdnumberFR;
                VATRegistrationDetailsData.d.CONTACT_PERSONSet[0].Firstnm = FirstnmFR;
                VATRegistrationDetailsData.d.CONTACT_PERSONSet[0].Lastnm = LastnmFR;
                VATRegistrationDetailsData.d.CONTACTDTSet[0].MobNumber = MobNumberFR;
                VATRegistrationDetailsData.d.CONTACTDTSet[0].SmtpAddr = SmtpAddrFR;
                //VATRegistrationDetailsData.d.CONTACT_PERSONSet[0].Title = Title;


                //Step 5
                /* if (IsDeclarationChecked)
                 {*/
                VATRegistrationDetailsData.d.Decfg = "1";
                /*}
                else
                {
                    VATRegistrationDetailsData.d.Decfg = "0";
                }*/
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

            await MopupService.Instance.PushAsync(new ShowVatInformationConfirmationPageView(newDesignPopUp));
        }




        public async Task<vATRegistration> SubmitClicked()
        {
            vATRegistration response = new vATRegistration();
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                setDATA();
                List<Attachment> ATTDETSetnew = new List<Attachment>();
                ATTDETSetnew = VATRegistrationDetailsData.d.ATTDETSet;
                VATRegistrationDetailsData.d.NresFg = string.Empty;
                //VATRegistrationDetails vATRegistrationDetails = new VATRegistrationDetails();
                response = await VatRegistrationWebServiceManager.SaveVATRegistrationData(VATRegistrationDetailsData.d);
                PopToRootPage();
                if (response != null)
                {
                    try
                    {
                        if (response != null)
                        {
                            if (response.Operationz.Equals("04"))
                            {
                                string number = response.Fbnumz;
                                string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                                //await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));
                                _navigationService.GoBack();
                            }
                            if (response.Operationz.Equals("05"))
                            {
                                //  string number = response.d.Fbnumz;
                                string displayMessage = AppResources.VATRSaveasdraftMessage;
                                //await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));
                            }
                            if (response.Operationz.Equals("25"))
                            {
                                //  string number = response.d.Fbnumz;
                                string displayMessage = AppResources.VATRegistrationSuccessMessage + " " + response.Fbnumz + " " + AppResources.VatApproved;
                                //await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));
                                //_navigationService.GoBack();
                            }

                            VATRegistrationDetailsData = new VATRegistrationDetails();
                            VATRegistrationDetailsData.d = response;
                            VATRegistrationDetailsData.d.ATTDETSet = ATTDETSetnew;
                            VATRegistrationDetailsData.d.ATTDETSet = ATTDETSetObject;
                            //Set data after api call 
                            setDataAfterSubmitAPIAsync(response);

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
                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    //_navigationService.GoBack();

                });
                return response;
            }

            catch (Exception)
            {
                return response;
            }
        }

        public async Task setDataAfterSubmitAPIAsync(vATRegistration vatRegistration)
        {
            //Set applicable buttons
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                VATRegistrationOtherDetails vATRegistrationOther = await VatRegistrationWebServiceManager.GAZTGetVATRegistrationDataWithButtons(vatRegistration.Fbnumz, vatRegistration.Officerz, vatRegistration.Statusz, vatRegistration.TxnTpz, "ZTAX_VT_REG");

                PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                if (vATRegistrationOther != null && vATRegistrationOther.d != null)
                {
                    VATRegistrationOtherDetails = vATRegistrationOther;

                    SetApplicableButtons();
                    setMoreOptioButtons();
                }
                IsLoading = false;
            });
        }

        public void setQuestionImage()
        {
            if (VATRegistrationDetailsData.d.QUESTIONSSet != null && VATRegistrationDetailsData.d.QUESTIONSSet.Count > 0)
            {

                string value1forimage3first = VATRegistrationDetailsData.d.QUESTIONSSet.Where(x => x.QueNo == "003" && x.QoptNo == "031").Select(x => x.QoptAns).FirstOrDefault();
                string value2forimage3second = VATRegistrationDetailsData.d.QUESTIONSSet.Where(x => x.QueNo == "003" && x.QoptNo == "032").Select(x => x.QoptAns).FirstOrDefault();

                string value1forimage4first = VATRegistrationDetailsData.d.QUESTIONSSet.Where(x => x.QueNo == "004" && x.QoptNo == "041").Select(x => x.QoptAns).FirstOrDefault();
                string value2forimage4second = VATRegistrationDetailsData.d.QUESTIONSSet.Where(x => x.QueNo == "004" && x.QoptNo == "042").Select(x => x.QoptAns).FirstOrDefault();

                if (value1forimage3first == "1")
                {
                    ImageforTextQuestion3First = "vat_tile_IbanCard_background.png";
                    TextQuestion3FirstTextColor = Colors.White;
                }
                else
                {
                    ImageforTextQuestion3First = "vat_tile_IbanCard_background_white.png";
                    TextQuestion3FirstTextColor = (Color)App.Current.Resources["Primary"];
                }

                if (value2forimage3second == "1")
                {
                    ImageforTextQuestion3Second = "vat_tile_IbanCard_background.png";
                    TextQuestion3SecondTextColor = Colors.White;
                }
                else
                {
                    ImageforTextQuestion3Second = "vat_tile_IbanCard_background_white.png";
                    TextQuestion3SecondTextColor = (Color)App.Current.Resources["Primary"];
                }

                if (value1forimage4first == "1")
                {
                    ImageforTextQuestion4First = "vat_tile_IbanCard_background.png";
                    TextQuestion4FirstTextColor = Colors.White;
                }
                else
                {
                    ImageforTextQuestion4First = "vat_tile_IbanCard_background_white.png";
                    TextQuestion4FirstTextColor = (Color)App.Current.Resources["Primary"]; ;
                }

                if (value2forimage4second == "1")
                {
                    ImageforTextQuestion4Second = "vat_tile_IbanCard_background.png";
                    TextQuestion4SecondTextColor = Colors.White;
                }
                else
                {
                    ImageforTextQuestion4Second = "vat_tile_IbanCard_background_white.png";
                    TextQuestion4SecondTextColor = (Color)App.Current.Resources["Primary"]; ;
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
                IsLoading = true;
                GetSignUpIdType();
                IsLoading = true;
                // VATRegistrationDetailsData = null;
                VATRegistrationOtherDetails = null;
                ADDRESSSetData = null;
                VATRegistrationDetails vATRegistration = null;
                VATRegistrationOtherDetails vATRegistrationOther = null;
                try
                {
                    vATRegistration = await VatRegistrationWebServiceManager.GAZTGetVATRegistrationData();
                    //TODO 7062 Changes
                    VatDeregDeclaration = await VatRegistrationWebServiceManager.GAZTGetVATDeRegistrationDeclaration(vATRegistration.d.Fbnumz);
                    PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                    if (vATRegistration != null && vATRegistration.d != null)
                    {

                        //step 4 and 5 data set
                        if (vATRegistration.d.CONTACT_PERSONSet != null)
                        {
                            GpartFR = vATRegistration.d.CONTACT_PERSONSet[0].Gpart;
                            //  VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Type = SelectedIdTypeFR.ID;
                            idnumber = string.Empty;
                            idnumber = vATRegistration.d.CONTACT_PERSONSet[0].Idnumber;
                            FirstnmFR = vATRegistration.d.CONTACT_PERSONSet[0].Firstnm;
                            LastnmFR = vATRegistration.d.CONTACT_PERSONSet[0].Lastnm;
                            MobNumberFR = vATRegistration.d.CONTACTDTSet[0].MobNumber;
                            SmtpAddrFR = vATRegistration.d.CONTACTDTSet[0].SmtpAddr;
                            //Title = vATRegistration.d.CONTACT_PERSONSet[0].Title;

                            SelectedIdTypeFR = IdTypeListFR.Where(x => x.ID == vATRegistration.d.CONTACT_PERSONSet[0].Type).FirstOrDefault();
                            ATTDETSetObject = new List<Attachment>();
                            ATTDETSetObject = vATRegistration.d.ATTDETSet;

                        }

                        //Step 5

                        /* if (vATRegistration.d.Decfg == "1")
                         {*/
                        IsDeclarationChecked = true;
                        //VATRegistrationDetailsData.d.Decfg = "1";
                        /*}
                        else if (vATRegistration.d.Decfg == "0")
                        {
                            IsDeclarationChecked = false;
                        }*/
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

                            //StartdateToshow = UtilityManager.CovertDateTimeToDDMMYYYY(vATRegistration.d.CrStdt);
                            VatRegDate = JsonConvert.DeserializeObject<DateTime>(@"""" + vATRegistration.d.CrStdt + @"""");
                            StartdateToshow = JsonConvert.DeserializeObject<DateTime>(@"""" + vATRegistration.d.CrStdt + @"""").ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                        }
                        if (VATRegistrationDetailsData != null && VATRegistrationDetailsData.d != null)
                        {
                            vATRegistration.d.ImFg = VATRegistrationDetailsData.d.ImFg;
                        }
                        VATRegistrationDetailsData = vATRegistration;
                        // setIban();
                        if (VATRegistrationDetailsData.d.AgrFg != null)
                        { }


                        if (VATRegistrationDetailsData.d.ADDRESSSet != null && VATRegistrationDetailsData.d.ADDRESSSet.Count != 0)
                        {
                            ADDRESSSetData = VATRegistrationDetailsData.d.ADDRESSSet[0];
                            AddressLineOne = ADDRESSSetData.BuildingNo + " " + ADDRESSSetData.Street + " " + ADDRESSSetData.Quarter;
                            if (ADDRESSSetData.PostalCd.Equals("00000"))
                            {
                                ADDRESSSetData.PostalCd = string.Empty;
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
                            ImporterTextColor = (Color)App.Current.Resources["Primary"]; ;
                        }
                        if (VATRegistrationDetailsData.d.ExFg == "1")
                        {
                            ExporterImageSource = "vat_tile_IbanCard_background.png";
                            ExporterTextColor = Colors.White;
                        }
                        else
                        {
                            ExporterImageSource = "vat_tile_IbanCard_background_white.png";
                            ExporterTextColor = (Color)App.Current.Resources["Primary"]; ;
                        }


                        if (VATRegistrationDetailsData.d.QUESCONFIG_MSet != null && VATRegistrationDetailsData.d.QUESCONFIG_MSet.Count != 0)
                        {
                            MinMaxRanges = new List<QuestionNumberWithMinMaxRange>();
                            MinMaxRanges = UtilityManager.GetLowAndHighRangeForEachQuestionSet(VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                            MaximumDisplayValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.MaxRangeValue).FirstOrDefault();
                            MinimumDisplayValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.MinRangeValue).FirstOrDefault();

                            MaximumDisplayValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.MaxRangeValue).FirstOrDefault();
                            MinimumDisplayValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.MinRangeValue).FirstOrDefault();


                            MaximumValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.CountOfProbableAnswersForThisQuestions).FirstOrDefault() - 1;
                            MaximumValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.CountOfProbableAnswersForThisQuestions).FirstOrDefault() - 1;

                            TextQuestion3First = VATRegistrationDetailsData.d.QUESCONFIG_MSet.Where(x => x.QueNo == "003" && x.QoptNo == "031").Select(x => x.QoptTxt).FirstOrDefault();
                            TextQuestion3Second = VATRegistrationDetailsData.d.QUESCONFIG_MSet.Where(x => x.QueNo == "003" && x.QoptNo == "032").Select(x => x.QoptTxt).FirstOrDefault();

                            TextQuestion4First = VATRegistrationDetailsData.d.QUESCONFIG_MSet.Where(x => x.QueNo == "004" && x.QoptNo == "041").Select(x => x.QoptTxt).FirstOrDefault();
                            TextQuestion4Second = VATRegistrationDetailsData.d.QUESCONFIG_MSet.Where(x => x.QueNo == "004" && x.QoptNo == "042").Select(x => x.QoptTxt).FirstOrDefault();

                            setQuestionImage();

                        }
                        if (VATRegistrationDetailsData.d.QUESTIONSSet != null)
                        {
                            answer1selectedcount = VATRegistrationDetailsData.d.QUESTIONSSet.Where(s => s.QueNo == "001" && s.QoptAns == "1").Count();
                            answer2selectedcount = VATRegistrationDetailsData.d.QUESTIONSSet.Where(s => s.QueNo == "002" && s.QoptAns == "1").Count();
                            answer3selectedcount = VATRegistrationDetailsData.d.QUESTIONSSet.Where(s => s.QueNo == "003" && s.QoptAns == "1").Count();
                            answer4selectedcount = VATRegistrationDetailsData.d.QUESTIONSSet.Where(s => s.QueNo == "004" && s.QoptAns == "1").Count();
                        }

                        vATRegistrationOther = await VatRegistrationWebServiceManager.GAZTGetVATRegistrationDataWithButtons(vATRegistration.d.Fbnumz, vATRegistration.d.Officerz, vATRegistration.d.Statusz, vATRegistration.d.TxnTpz, "ZTAX_VT_REG");
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        if (vATRegistrationOther != null && vATRegistrationOther.d != null)
                        {
                            VATRegistrationOtherDetails = vATRegistrationOther;

                            SetApplicableButtons();
                            setMoreOptioButtons();
                        }


                        IdnumberFR = idnumber;

                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
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
                        //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                        IsLoading = false;
                        _navigationService.GoBack();
                    });
                    //   await Task.Run(() =>
                    //   {
                    //  });
                }
                IsLoading = false;

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {


                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }

        private void SetApplicableButtons()
        {
            //Parag
            if ((VATRegistrationOtherDetails.d.VR_UI_BTNSet != null) && (VATRegistrationOtherDetails.d.VR_UI_BTNSet != null))
            {
                if (ListOfActionButtonsApplicableForRegistration != null && ListOfActionButtonsApplicableForRegistration.Count > 0)
                    ListOfActionButtonsApplicableForRegistration.Clear();
                else
                    ListOfActionButtonsApplicableForRegistration = new List<string>();

                //beforoe returning buttons we need to set the value based on Buttons emumeration
                foreach (ResultsItemForButton button in VATRegistrationOtherDetails.d.VR_UI_BTNSet)
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

        //public void setIban()
        //{
        //    IbanList = new ObservableCollection<Result2>();

        //    if(VATRegistrationDetailsData.d.IBANSet!=null)
        //    {
        //        IbanList = new ObservableCollection<Result2>(VATRegistrationDetailsData.d.IBANSet.results);
        //    }

        //}
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
            catch (Exception)
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
            TodayDate = todaycollection;
            DefaultMonth = DateTime.Now.Date.Month;
        }
        #endregion

    }
}
