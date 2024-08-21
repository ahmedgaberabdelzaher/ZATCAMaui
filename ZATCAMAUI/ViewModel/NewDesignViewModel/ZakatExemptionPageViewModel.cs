using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Mopups.Services;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Manager;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.NewDesign.ZakatExemptionRequest;
using static ZATCAMAUI.Models.ZakatExemptionModel;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatExemptionRequestViewModel
{
   
    public class ZakatExemptionPageViewModel : BaseViewModel
    {
        public ZakatExemptionModel zakatExemptionModel;
        public ZakatExemptionRequestResponse.Root zakatExemptionresponse;


        int selectedPage = (int)PagesEnum.ZakatExemptionYear;
        int SelectedAttachmentNumber = 0;

        public ICommand GoBackClick { get; set; }

        public ICommand FinancialYearTobeExemptedCommand { get; set; }
        public ICommand ZakatExemptionYearConBtnTapped { get; set; }
        public ICommand EntityInfoConBtnTapped { get; set; }

        public ICommand ZakatExcemptionTPDetailsBtnTapped { get; set; }

        public ICommand AttachmentsConBtnTapped { get; set; }
        public ICommand JustificationConBtnTapped { get; set; }
        public ICommand ShowExemptionYearPicker { get; set; }
        public ICommand ShowEntityCategoryPicker { get; set; }
        public ICommand ShowEntityTypePicker { get; set; }
        public ICommand ShowComEstablishOtherPicker { get; set; }
        public ICommand AddNewJustificationTapped { get; set; }


        public ICommand RemoveJustificiationOne { get; set; }
        public ICommand RemoveJustificiationTwo { get; set; }
        public ICommand RemoveJustificiationThree { get; set; }
        public ICommand RemoveJustificiationFour { get; set; }

        //Summary Commands
        public ICommand GoBackToZakatExemptionYearb { get; set; }
        public ICommand GoBackToZakatEntityInfo { get; set; }
        public ICommand GoBackToZakatExemptionAttachments { get; set; }
        public ICommand GoBackToZakatExemptionJustifications { get; set; }
        public ICommand SummaryConBtnTapped { get; set; }


        public ICommand NewCompanyArticalsAttachmentTapped { get; set; }
        public ICommand NewCertificateOfRegAttachmentTapped { get; set; }
        public ICommand NewCharitableTrustAttachmentTapped { get; set; }
        public ICommand NewCharityLicenseAttachmentTapped { get; set; }
        public ICommand NewMemorandumOfAssociationAttachmentTapped { get; set; }
        public ICommand NewOtherAttachmentTapped { get; set; }
        public ICommand AddtionalAtachmentTapped { get; set; }

        enum PagesEnum
        {
            ZakatExcemptionTPDetails,
            ZakatExemptionYear,
            EntityDetails,
            Attachments,
            Justification,
            Summary
        }

        public bool MarkComplete { get; private set; } = false;

        private int _MaxIndex = 5;

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
        private bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading == value) return;

                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public bool _zakatExcemptionTPDetailsVisible = true;

        public bool ZakatExcemptionTPDetailsVisible
        {
            get
            {
                return _zakatExcemptionTPDetailsVisible;
            }
            set
            {
                if (_zakatExcemptionTPDetailsVisible == value) return;
                _zakatExcemptionTPDetailsVisible = value;
                OnPropertyChanged("ZakatExcemptionTPDetailsVisible");
            }
        }

        private bool _zakatExemptionYearConButtonEnabled = false;

        public bool ZakatExemptionYearConButtonEnabled
        {
            get { return _zakatExemptionYearConButtonEnabled; }
            set
            {
                if (_zakatExemptionYearConButtonEnabled == value) return;

                _zakatExemptionYearConButtonEnabled = value;
                ZakatExemptionYearButtonBackGroundColor = (_zakatExemptionYearConButtonEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
                OnPropertyChanged("ZakatExemptionYearConButtonEnabled");
            }
        }

        private bool _zakatEntityInfoConButtonEnabled = false;

        public bool ZakatEntityInfoConButtonEnabled
        {
            get { return _zakatEntityInfoConButtonEnabled; }
            set
            {
                if (_zakatEntityInfoConButtonEnabled == value) return;

                _zakatEntityInfoConButtonEnabled = value;
                ZakatEntityInfoButtonBackGroundColor = (_zakatEntityInfoConButtonEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
                OnPropertyChanged("ZakatEntityInfoConButtonEnabled");
            }
        }

        private bool _zakatAttachmentConButtonEnabled = false;

        public bool ZakatAttachmentConButtonEnabled
        {
            get { return _zakatAttachmentConButtonEnabled; }
            set
            {
                if (_zakatAttachmentConButtonEnabled == value) return;

                _zakatAttachmentConButtonEnabled = value;
                ZakatAttachmentButtonBackGroundColor = (_zakatAttachmentConButtonEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
                OnPropertyChanged("ZakatAttachmentConButtonEnabled");
            }
        }

        private bool _zakatJustificationConButtonEnabled = false;

        public bool ZakatJustificationConButtonEnabled
        {
            get { return _zakatJustificationConButtonEnabled; }
            set
            {
                if (_zakatJustificationConButtonEnabled == value) return;

                _zakatJustificationConButtonEnabled = value;
                ZakatJustificationButtonBackGroundColor = (_zakatJustificationConButtonEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
                OnPropertyChanged("ZakatJustificationConButtonEnabled");
            }
        }

        private bool _isInstrunctionChecked = false;

        public bool IsInstrunctionChecked
        {
            get { return _isInstrunctionChecked; }
            set
            {
                if (_isInstrunctionChecked == value) return;

                _isInstrunctionChecked = value;
                ZakatSummarySubmitButtonBackGroundColor = (_isInstrunctionChecked ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);

                OnPropertyChanged("IsInstrunctionChecked");
            }
        }

        private Color _zakatExemptionYearButtonBackGroundColor = (Color)Application.Current.Resources["ButtonGray"];

        public Color ZakatExemptionYearButtonBackGroundColor
        {
            get { return _zakatExemptionYearButtonBackGroundColor; }
            set
            {
                if (_zakatExemptionYearButtonBackGroundColor == value)
                {
                    return;
                }

                _zakatExemptionYearButtonBackGroundColor = value;
                OnPropertyChanged("ZakatExemptionYearButtonBackGroundColor");
            }
        }

        private Color _zakatEntityInfoButtonBackGroundColor = (Color)Application.Current.Resources["ButtonGray"];

        public Color ZakatEntityInfoButtonBackGroundColor
        {
            get { return _zakatEntityInfoButtonBackGroundColor; }
            set
            {
                if (_zakatEntityInfoButtonBackGroundColor == value)
                {
                    return;
                }

                _zakatEntityInfoButtonBackGroundColor = value;
                OnPropertyChanged("ZakatEntityInfoButtonBackGroundColor");
            }
        }

        private Color _zakatAttachmentButtonBackGroundColor = (Color)Application.Current.Resources["ButtonGray"];

        public Color ZakatAttachmentButtonBackGroundColor
        {
            get { return _zakatAttachmentButtonBackGroundColor; }
            set
            {
                if (_zakatAttachmentButtonBackGroundColor == value)
                {
                    return;
                }

                _zakatAttachmentButtonBackGroundColor = value;
                OnPropertyChanged("ZakatAttachmentButtonBackGroundColor");
            }
        }

        private Color _zakatJustificationButtonBackGroundColor = (Color)Application.Current.Resources["ButtonGray"];

        public Color ZakatJustificationButtonBackGroundColor
        {
            get { return _zakatJustificationButtonBackGroundColor; }
            set
            {
                if (_zakatJustificationButtonBackGroundColor == value)
                {
                    return;
                }

                _zakatJustificationButtonBackGroundColor = value;
                OnPropertyChanged("ZakatJustificationButtonBackGroundColor");
            }
        }

        private Color _zakatSummarySubmitButtonBackgroundColor = (Color)Application.Current.Resources["ButtonGray"];

        public Color ZakatSummarySubmitButtonBackGroundColor
        {
            get { return _zakatSummarySubmitButtonBackgroundColor; }
            set
            {
                if (_zakatSummarySubmitButtonBackgroundColor == value)
                {
                    return;
                }

                _zakatSummarySubmitButtonBackgroundColor = value;
                OnPropertyChanged("ZakatSummarySubmitButtonBackGroundColor");
            }
        }

        private string _selectedYear = "";

        public string SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear == value) return;

                _selectedYear = value;
                OnPropertyChanged("SelectedYear");
            }
        }

        private string _selectedYearID = "";

        public string SelectedYearID
        {
            get { return _selectedYearID; }
            set
            {
                if (_selectedYearID == value) return;

                _selectedYearID = value;
                OnPropertyChanged("SelectedYearID");
            }
        }

        private string _selectedEntityType = "";

        public string SelectedEntityType
        {
            get { return _selectedEntityType; }
            set
            {
                if (_selectedEntityType == value) return;

                _selectedEntityType = value;
                OnPropertyChanged("SelectedEntityType");
            }
        }

        private string _selectedEntityID = "";

        public string SelectedEntityID
        {
            get { return _selectedEntityID; }
            set
            {
                if (_selectedEntityID == value) return;

                _selectedEntityID = value;
                OnPropertyChanged("SelectedEntityID");
                SelectedEntityIDCategory = "";
                if(SelectedEntityID=="06")
                {
                    IsOffspringVisible = true;
                }
                else
                {
                    IsOffspringVisible=false;
                }
            }
        }

        private string _selectedEntityCategory = "";

        public string SelectedEntityIDCategory
        {
            get { return _selectedEntityCategory; }
            set
            {
                if (_selectedEntityCategory == value) return;

                _selectedEntityCategory = value;
                OnPropertyChanged("SelectedEntityIDCategory");
                _selectedTechincalNumber = zakatExemptionModel.d.entityCategories.Find((e) => e.name.Equals(_selectedEntityCategory)).technicalNumber;
                setAttachments();
            }
        }

        private string _selectedTechincalNumber;

        public List<EntityAttachments> FilteredAttachments { get; set; }
        public Dictionary<string,bool> AttachmentsUploaded { get; set; }
        private List<ZakatExemptionModel.AttachmentModel> _selectedAttachments;
        public List<ZakatExemptionModel.AttachmentModel> SelectedAttachments
        {
            get
            {
                return _selectedAttachments;
            }
            set
            {
                _selectedAttachments = value;
                OnPropertyChanged(nameof(SelectedAttachments));
            }
        }

        public List<ZakatExemptionModel.AttachmentModel> Attachments { get; set; }

        private string _companyEstablishmentOther = "";

        public string CompanyEstablishmentOther
        {
            get { return _companyEstablishmentOther; }
            set
            {
                if (_companyEstablishmentOther == value) return;

                _companyEstablishmentOther = value;
                OnPropertyChanged("CompanyEstablishmentOther");
            }
        }

        private string _natureOfEntity = string.Empty;

        public string NatureOfEntity
        {
            get { return _natureOfEntity; }
            set
            {
                if (_natureOfEntity == value) return;

                _natureOfEntity = value;
                OnPropertyChanged("NatureOfEntity");
            }
        }

        private double _offSpringPercentage;

        public double OffSpringPercentage
        {
            get
            {
                return _offSpringPercentage;
            }

            set
            {
                if (value <= 100)
                {
                    value = Math.Round(value, 2);
                    _offSpringPercentage = value;
                }
                OnPropertyChanged("OffSpringPercentage");
            }
        }

        private double _charityPercentage;

        public double CharityPercentage
        {
            get
            {
                return _charityPercentage;
            }

            set
            {
                if (value <= 100)
                {
                    value = Math.Round(value, 2);
                    _charityPercentage = value;
                }
                OnPropertyChanged("CharityPercentage");
            }
        }

        private GenericPickerModel _pickerModelExcemptionYear { get; set; }

        public GenericPickerModel PickerModelExcemptionYear
        {
            get { return _pickerModelExcemptionYear; }
            set
            {
                if (_pickerModelExcemptionYear == value) return;

                _pickerModelExcemptionYear = value;
                OnPropertyChanged("PickerModelExcemptionYear");
            }
        }

        private GenericPickerModel _pickerModelEntityType { get; set; }

        public GenericPickerModel PickerModelEntityType
        {
            get { return _pickerModelEntityType; }
            set
            {
                if (_pickerModelEntityType == value) return;

                _pickerModelEntityType = value;
                OnPropertyChanged("PickerModelEntityType");
            }
        }

        private GenericPickerModel _pickerModelEntityCategory { get; set; }

        public GenericPickerModel PickerModelEntityCategory
        {
            get { return _pickerModelEntityCategory; }
            set
            {
                if (_pickerModelEntityCategory == value) return;

                _pickerModelEntityCategory = value;
                OnPropertyChanged("PickerModelEntityCategory");
            }
        }

        private GenericPickerModel _pickerModelEstOther { get; set; }

        public GenericPickerModel PickerModelEstOther
        {
            get { return _pickerModelEstOther; }
            set
            {
                if (_pickerModelEstOther == value) return;

                _pickerModelEstOther = value;
                OnPropertyChanged("PickerModelEstOther");
            }
        }

        private bool _zakatExemptionYearVisible = false;

        public bool ZakatExemptionYearVisible
        {
            get { return _zakatExemptionYearVisible; }
            set
            {
                if (_zakatExemptionYearVisible == value) return;

                _zakatExemptionYearVisible = value;
                OnPropertyChanged("ZakatExemptionYearVisible");
            }
        }
        
        private bool _entityInformationVisible = false;

        public bool EntityInformationVisible
        {
            get { return _entityInformationVisible; }
            set
            {
                if (_entityInformationVisible == value) return;

                _entityInformationVisible = value;
                OnPropertyChanged("EntityInformationVisible");
            }
        }
        
        private bool _summaryVisible = false;

        public bool SummaryVisible
        {
            get { return _summaryVisible; }
            set
            {
                if (_summaryVisible == value) return;

                _summaryVisible = value;
                OnPropertyChanged("SummaryVisible");
            }
        }

        private bool _pageOnEditMode = false;

        public bool PageOnEditMode
        {
            get { return _pageOnEditMode; }
            set
            {
                if (_pageOnEditMode == value) return;

                _pageOnEditMode = value;
                OnPropertyChanged("PageOnEditMode");
            }
        }

        private bool _isCharitabletrustsAndFoundations = false;

        public bool IsCharitabletrustsAndFoundations
        {
            get { return _isCharitabletrustsAndFoundations; }
            set
            {
                if (_isCharitabletrustsAndFoundations == value) return;

                _isCharitabletrustsAndFoundations = value;
                OnPropertyChanged("IsCharitabletrustsAndFoundations");
            }
        }

        private bool _attachmentInformationVisible = false;

        public bool AttachmentInformationVisible
        {
            get { return _attachmentInformationVisible; }
            set
            {
                if (_attachmentInformationVisible == value) return;

                _attachmentInformationVisible = value;
                OnPropertyChanged("AttachmentInformationVisible");
            }
        }

        private bool _companyArticlesAttachmentVisible = false;

        public bool CompanyArticlesAttachmentVisible
        {
            get { return _companyArticlesAttachmentVisible; }
            set
            {
                if (_companyArticlesAttachmentVisible == value) return;

                _companyArticlesAttachmentVisible = value;
                OnPropertyChanged("CompanyArticlesAttachmentVisible");
            }
        }

        private bool _certificateRegAttachmentVisible = false;

        public bool CertificateRegAttachmentVisible
        {
            get { return _certificateRegAttachmentVisible; }
            set
            {
                if (_certificateRegAttachmentVisible == value) return;

                _certificateRegAttachmentVisible = value;
                OnPropertyChanged("CertificateRegAttachmentVisible");
            }
        }

        private bool _charitableTrustAttachmentVisible = false;

        public bool CharitableTrustAttachmentVisible
        {
            get { return _charitableTrustAttachmentVisible; }
            set
            {
                if (_charitableTrustAttachmentVisible == value) return;

                _charitableTrustAttachmentVisible = value;
                OnPropertyChanged("CharitableTrustAttachmentVisible");
            }
        }

        private bool _charityAttachmentVisible = false;

        public bool CharityAttachmentVisible
        {
            get { return _charityAttachmentVisible; }
            set
            {
                if (_charityAttachmentVisible == value) return;

                _charityAttachmentVisible = value;
                OnPropertyChanged("CharityAttachmentVisible");
            }
        }

        private bool _memorandomAttachmentVisible = false;

        public bool MemorandomAttachmentVisible
        {
            get { return _memorandomAttachmentVisible; }
            set
            {
                if (_memorandomAttachmentVisible == value) return;

                _memorandomAttachmentVisible = value;
                OnPropertyChanged("MemorandomAttachmentVisible");
            }
        }

        private bool _otherAttachmentVisible = false;

        public bool OtherAttachmentVisible
        {
            get { return _otherAttachmentVisible; }
            set
            {
                if (_otherAttachmentVisible == value) return;

                _otherAttachmentVisible = value;
                OnPropertyChanged("OtharAttachmentVisible");
            }
        }

        private bool _justificationInformationVisible = false;

        private IEnumerable<Attachment> attachments;

        public bool JustificationInformationVisible
        {
            get { return _justificationInformationVisible; }
            set
            {
                if (_justificationInformationVisible == value) return;

                _justificationInformationVisible = value;
                OnPropertyChanged("JustificationInformationVisible");
            }
        }

        private bool _isJustificationOneVisible = false;

        public bool IsJustificationOneVisible
        {
            get { return _isJustificationOneVisible; }
            set
            {
                if (_isJustificationOneVisible == value) return;

                _isJustificationOneVisible = value;
                OnPropertyChanged("IsJustificationOneVisible");
            }
        }

        private bool _isJustificationTwoVisible = false;

        public bool IsJustificationTwoVisible
        {
            get { return _isJustificationTwoVisible; }
            set
            {
                if (_isJustificationTwoVisible == value) return;

                _isJustificationTwoVisible = value;
                OnPropertyChanged("IsJustificationTwoVisible");
            }
        }

        private bool _isJustificationThreeVisible = false;

        public bool IsJustificationThreeVisible
        {
            get { return _isJustificationThreeVisible; }
            set
            {
                if (_isJustificationThreeVisible == value) return;

                _isJustificationThreeVisible = value;
                OnPropertyChanged("IsJustificationThreeVisible");
            }
        }
        
        private bool _isJustificationFourVisible = false;

        public bool IsJustificationFourVisible
        {
            get { return _isJustificationFourVisible; }
            set
            {
                if (_isJustificationFourVisible == value) return;

                _isJustificationFourVisible = value;
                OnPropertyChanged("IsJustificationFourVisible");
            }
        }
        
        private bool _isJustificationFiveVisible = false;

        public bool IsJustificationFiveVisible
        {
            get { return _isJustificationFiveVisible; }
            set
            {
                if (_isJustificationFiveVisible == value) return;

                _isJustificationFiveVisible = value;
                OnPropertyChanged("IsJustificationFiveVisible");
            }
        }

        private bool _isEditRequired = false;

        public bool IsEditRequired
        {
            get { return _isEditRequired; }
            set
            {
                if (_isEditRequired == value) return;

                _isEditRequired = value;
                OnPropertyChanged("IsEditRequired");
            }
        }
        private bool _showTermsOnSubmit = true;

        public bool ShowTermsOnSubmit
        {
            get { return _showTermsOnSubmit; }
            set
            {
                if (_showTermsOnSubmit == value) return;

                _showTermsOnSubmit = value;
                OnPropertyChanged("ShowTermsOnSubmit");
            }
        }

        private string _justicationOne = string.Empty;

        public string JusticationOne
        {
            get { return _justicationOne; }
            set
            {
                if(value.Length > 0)
                {
                    ZakatJustificationConButtonEnabled = true; 
                }
                else
                {
                    ZakatJustificationConButtonEnabled = false;
                }
                if (_justicationOne == value) return;

                _justicationOne = value;
                OnPropertyChanged("JusticationOne");
            }
        }

        private string _justicationTwo = string.Empty;

        public string JusticationTwo
        {
            get { return _justicationTwo; }
            set
            {
                if (_justicationTwo == value) return;

                _justicationTwo = value;
                OnPropertyChanged("JusticationTwo");
            }
        }

        private string _justicationThree = string.Empty;

        public string JusticationThree
        {
            get { return _justicationThree; }
            set
            {
                if (_justicationThree == value) return;

                _justicationThree = value;
                OnPropertyChanged("JusticationThree");
            }
        }

        private string _justicationFour = string.Empty;

        public string JusticationFour
        {
            get { return _justicationFour; }
            set
            {
                if (_justicationFour == value) return;

                _justicationFour = value;
                OnPropertyChanged("JusticationFour");
            }
        }

        private string _justicationFive = string.Empty;

        public string JusticationFive
        {
            get { return _justicationFive; }
            set
            {
                if (_justicationFive == value) return;

                _justicationFive = value;
                OnPropertyChanged("JusticationFive");
            }
        }

        public ObservableCollection<Attachment> _companyArticalsAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> CompanyArticalsAttachmentsListViewData
        {
            get { return _companyArticalsAttachmentsListViewData; }

            set
            {
                if (_companyArticalsAttachmentsListViewData == value)
                {
                    return;
                }

                _companyArticalsAttachmentsListViewData = value;
                OnPropertyChanged("CompanyArticalsAttachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> _certificateOfRegAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> CertificateOfRegAttachmentsListViewData
        {
            get { return _certificateOfRegAttachmentsListViewData; }

            set
            {
                if (_certificateOfRegAttachmentsListViewData == value)
                {
                    return;
                }

                _certificateOfRegAttachmentsListViewData = value;
                OnPropertyChanged("CertificateOfRegAttachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> _charitableTrustAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> CharitableTrustAttachmentsListViewData
        {
            get { return _charitableTrustAttachmentsListViewData; }

            set
            {
                if (_charitableTrustAttachmentsListViewData == value)
                {
                    return;
                }

                _charitableTrustAttachmentsListViewData = value;
                OnPropertyChanged("CharitableTrustAttachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> _charityLicenseAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> CharityLicenseAttachmentsListViewData
        {
            get { return _charityLicenseAttachmentsListViewData; }

            set
            {
                if (_charityLicenseAttachmentsListViewData == value)
                {
                    return;
                }

                _charityLicenseAttachmentsListViewData = value;
                OnPropertyChanged("CharityLicenseAttachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> _memorandumOfAssociationAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> MemorandumOfAssociationAttachmentsListViewData
        {
            get { return _memorandumOfAssociationAttachmentsListViewData; }

            set
            {
                if (_memorandumOfAssociationAttachmentsListViewData == value)
                {
                    return;
                }

                _memorandumOfAssociationAttachmentsListViewData = value;
                OnPropertyChanged("MemorandumOfAssociationAttachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> _otherAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> OtherAttachmentsListViewData
        {
            get { return _otherAttachmentsListViewData; }

            set
            {
                if (_otherAttachmentsListViewData == value)
                {
                    return;
                }

                _otherAttachmentsListViewData = value;
                OnPropertyChanged("OtherAttachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> _additionalAttachmentsListViewData { get; set; }
        public ObservableCollection<Attachment> AdditionalAttachmentsListViewData
        {
            get { return _additionalAttachmentsListViewData; }

            set
            {
                if (_additionalAttachmentsListViewData == value)
                {
                    return;
                }

                _additionalAttachmentsListViewData = value;
                OnPropertyChanged("AdditionalAttachmentsListViewData");
            }
        }

        private bool _isNew;
        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                _isNew = value;
                OnPropertyChanged(nameof(IsNew));
            }
        }

        private bool _isOffspringVisible;
        public bool IsOffspringVisible
        {
            get { return _isOffspringVisible; }
            set
            {
                _isOffspringVisible = value;
                OnPropertyChanged(nameof(IsOffspringVisible));
            }
        }


        public ZakatExemptionPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        { 
            GoBackClick = new Command(() => { BackNavigations(); });
            ZakatExemptionYearConBtnTapped = new Command(() =>
            {
                FinancialYearToBeExemptedConBtnClicked();
            });

            ZakatExcemptionTPDetailsBtnTapped = new Command(() => {

                ExcemptionDetailsbtnTapped();
            });

            EntityInfoConBtnTapped = new Command(() =>
            {
                EntityInfoToBeExemptedConBtnClicked();
            });
            
            AttachmentsConBtnTapped = new Command(() =>
            {
                AttachmentsConBtnClicked();
            });

            JustificationConBtnTapped = new Command(() =>
            {
                JustificationConBtnClicked();
            });

            ShowExemptionYearPicker = new Command(() =>
            {
                
                showPickerDialog();
            });
            ShowEntityTypePicker = new Command(() =>
            {
                showEntityTypeDialog();
            });
            ShowEntityCategoryPicker = new Command(() =>
            {
                showEntityCategoryPicker();
            });
            ShowComEstablishOtherPicker = new Command(() => {
                ShowCompanyEstablishmentOtherPicker();
            });
            NewCompanyArticalsAttachmentTapped = new Command(() => {
                NewCompanyArticalsAttachmentsPopup();
            });

            NewCertificateOfRegAttachmentTapped = new Command(() => {
                NewCertifcateOfRegAttachmentsPopup();
            });
            NewCharitableTrustAttachmentTapped = new Command(() => {
                NewCharitableTrustAttachmentsPopup();
            });
            NewCharityLicenseAttachmentTapped = new Command(() => {
                NewCharityLicenseAttachmentsPopup();
            });
            NewMemorandumOfAssociationAttachmentTapped = new Command(() => {
                NewMemoRandomAttachmentsPopup();
            });
            NewOtherAttachmentTapped = new Command(() => {
                NewOtherAttachmentsPopup();
            });

            AddtionalAtachmentTapped = new Command(() => {
                NewAddtionalAtachmentPopUp();
            });
            AddNewJustificationTapped = new Command(() => {
                AddNewJustification();
            });

            RemoveJustificiationOne = new Command(() => {
                IsJustificationOneVisible = false;
            });
            RemoveJustificiationTwo = new Command(() => {
                IsJustificationTwoVisible = false;
            });
            RemoveJustificiationThree = new Command(() => {
                IsJustificationThreeVisible = false;
            });
            RemoveJustificiationFour = new Command(() => {
                IsJustificationFourVisible = false;
            });


            //BackNavigations In Summary screen
            GoBackToZakatExemptionYearb=new Command(() =>{ GoBackFromEntityDetailsView(); });
            GoBackToZakatEntityInfo=new Command(()=> { GoBackFromAttachmentsView(); });
            GoBackToZakatExemptionAttachments=new Command(()=> { GoBackFromJustification(); });
            GoBackToZakatExemptionJustifications = new Command(()=> { GoBackFromSummaryView(); });

            SummaryConBtnTapped = new Command(async () => {
                try
                {
                    if (PageOnEditMode && AdditionalAttachmentsListViewData.Count == 0)
                    {
                        return;//On edit mode with adding attachment we are proceding user to submit
                    }
                    ZakatExcemptionReqModel request = CreateRequestData();
                    string response = await ZakatExemptionWebServiceManager.SubmitExemptionRequest(request);
                    zakatExemptionresponse = JsonConvert.DeserializeObject<ZakatExemptionRequestResponse.Root>(response);

                    if (zakatExemptionresponse != null && zakatExemptionresponse.D != null && zakatExemptionresponse.D.Fbnumz != null)
                    {

                        GoBackFromEntityDetailsView();
                        IsLoading = false;
                        await Application.Current.MainPage.Navigation.PushAsync(new ZakatExemptionSuccessPage(this));
                    }
                    else
                    {

                        IsLoading = false;

                        SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(response);
                        StringBuilder Message = new StringBuilder();
                        foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                        {
                            if (itemerror.severity.Contains("error"))
                            {
                                if (Message.Length > 0)
                                {
                                    Message.Append(Environment.NewLine);
                                }
                                Message.Append(itemerror.message);
                            }
                        }

                        throw new GAZTErrorException(Message.ToString());
                    }
                }
                catch (GAZTUnlockAccountException ex)
                {
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (Exception ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
            });
        }
        public  async Task<ZakatExemptionModel> GetDetailsForZakatExeRwq()
        {
            ZakatExemptionModel zakatExemptionModel = new ZakatExemptionModel();
            try
            {
                zakatExemptionModel = await ZakatExemptionWebServiceManager.GetRequestToZakatExemtionRequest(null);
                
            }
            catch (Exception ex)
            {
                //IsLoading = false;
            }
            return zakatExemptionModel;
        }
        internal async Task GetDetailsForZakatExeRwqAsync()
        {
           // IsLoading = true;
            try
            {
                zakatExemptionModel  = await ZakatExemptionWebServiceManager.GetRequestToZakatExemtionRequest(null);
                zakatExemptionModel.d = zakatExemptionModel.data;
                if (zakatExemptionModel != null && zakatExemptionModel.data!=null)
                {
                   // IsLoading = false;
                   if(zakatExemptionModel.data.CR6774.Equals("X"))
                    {
                        IsNew = true;
                    }
                    else
                    {
                        IsNew = false;
                    }
                }
            }
            catch (Exception ex) {
               // IsLoading = false;
            }
           
        }

        private void ExcemptionDetailsbtnTapped()
        {
            try
            {
                if (ZakatExcemptionTPDetailsVisible)
                {
                    //EnableEntityDetailsView();
                    //EnableEntityDetailsConButton();

                    CurrentIndex = 1;
                    // IsBackVisible = true;
                    ZakatExcemptionTPDetailsVisible = false;
                    ZakatExemptionYearVisible = true;
                    EntityInformationVisible = false;
                    AttachmentInformationVisible = false;
                    JustificationInformationVisible = false;
                    SummaryVisible = false;
                    selectedPage = (int)PagesEnum.ZakatExemptionYear;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void FinancialYearToBeExemptedConBtnClicked()
        {
            try
            {
                if (ZakatExemptionYearConButtonEnabled)
                {
                    //EnableEntityDetailsView();
                    //EnableEntityDetailsConButton();

                    CurrentIndex = 2;
                    // IsBackVisible = true;
                    ZakatExcemptionTPDetailsVisible = false;
                    ZakatExemptionYearVisible = false;
                    EntityInformationVisible = true;
                    AttachmentInformationVisible = false;
                    JustificationInformationVisible = false;
                    SummaryVisible = false;
                    selectedPage = (int)PagesEnum.EntityDetails;
                }

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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

        public void EntityInfoToBeExemptedConBtnClicked()
        {
            try
            {
                if (ZakatEntityInfoConButtonEnabled)
                {
                    if (SelectedEntityType.Length > 0 && SelectedEntityID.Equals("03"))
                    {
                        if(CompanyEstablishmentOther.Length > 0 && NatureOfEntity.Length > 0)
                        {
                            PreviewAttachmentView();
                        }
                    }
                    else if(SelectedEntityType.Length > 0 && NatureOfEntity.Length > 0)
                    {
                        PreviewAttachmentView();
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void PreviewAttachmentView()
        {
          //  OtherAttachmentVisible = true; //Other is always visible irrespective of selections
            switch (SelectedEntityID)
            {
                case "01":
                    CompanyArticlesAttachmentVisible = true;

                    CharityAttachmentVisible = false;
                    MemorandomAttachmentVisible = false;
                    CertificateRegAttachmentVisible = false;
                    CharitableTrustAttachmentVisible = false;
                    break;
                case "02":
                    CharityAttachmentVisible = true;
                    MemorandomAttachmentVisible = true;

                    CompanyArticlesAttachmentVisible = false;
                    CertificateRegAttachmentVisible = false;
                    CharitableTrustAttachmentVisible = false;

                    break;
                case "03":
                    if (CompanyEstablishmentOther.ToLower().Equals("other"))
                    {
                        CompanyArticlesAttachmentVisible = true;

                        CharityAttachmentVisible = false;
                        MemorandomAttachmentVisible = false;
                        CertificateRegAttachmentVisible = false;
                        CharitableTrustAttachmentVisible = false;

                    }
                    else
                    {
                        CompanyArticlesAttachmentVisible = true;
                        CertificateRegAttachmentVisible = true;
                        CharitableTrustAttachmentVisible = true;

                        CharityAttachmentVisible = false;
                        MemorandomAttachmentVisible = false;
                    }
                    break;
            }
            CurrentIndex = 3;

            ZakatExemptionYearVisible = false;
            EntityInformationVisible = false;
            JustificationInformationVisible = false;
            AttachmentInformationVisible = true;
            ZakatExcemptionTPDetailsVisible = false;

            SummaryVisible = false;
            selectedPage = (int)PagesEnum.Attachments;
        }
        
        private void AddNewJustification()
        {
            try
            {
                if(!IsJustificationOneVisible)
                {
                    IsJustificationOneVisible = true;
                }
                else if(!IsJustificationTwoVisible)
                {
                    IsJustificationTwoVisible = true;
                }
                else if(!IsJustificationThreeVisible)
                {
                    IsJustificationThreeVisible = true;
                }
                else if (!IsJustificationFourVisible)
                {
                    IsJustificationFourVisible = true;
                }

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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

        private void AttachmentsConBtnClicked()
        {
            try
            {

                try
                {
                    if (ZakatAttachmentConButtonEnabled)
                    {
                        CurrentIndex = 4;

                        ZakatExemptionYearVisible = false;
                        EntityInformationVisible = false;
                        AttachmentInformationVisible = false;
                        JustificationInformationVisible = true;
                        ZakatExcemptionTPDetailsVisible = false;

                        SummaryVisible = false;
                        selectedPage = (int)PagesEnum.Justification;
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(AppResources.VatDeregAttachmentInputPlaceholder, AppResources.Information);
                        });
                    }
                }
                catch (GAZTUnlockAccountException ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                /*if (ZakatExemptionYearConButtonEnabled)
                {*/
                //EnableEntityDetailsView();
                //EnableEntityDetailsConButton();

                
                //}

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
        
        private void JustificationConBtnClicked()
        {
            try
            {
                if (ZakatJustificationConButtonEnabled)
                {
                    CurrentIndex = 5;
                    ZakatExemptionYearVisible = false;
                    EntityInformationVisible = false;
                    AttachmentInformationVisible = false;
                    JustificationInformationVisible = false;
                    ZakatExcemptionTPDetailsVisible = false;

                    SummaryVisible = true;
                    selectedPage = (int)PagesEnum.Summary;
                }
              
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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

        private void GoBackFromEntityDetailsView()
        {
            CurrentIndex = 1;
            // IsBackVisible = true;
            ZakatExemptionYearVisible = true;
            EntityInformationVisible = false;
            AttachmentInformationVisible = false;
            JustificationInformationVisible = false;
            SummaryVisible = false;
            ZakatExcemptionTPDetailsVisible = false;

            selectedPage = (int)PagesEnum.ZakatExemptionYear;
        }

        private void GoBackFromAttachmentsView()
        {
            CurrentIndex = 2;
            // IsBackVisible = true;
            ZakatExemptionYearVisible = false;
            EntityInformationVisible = true;
            AttachmentInformationVisible = false;
            JustificationInformationVisible = false;
            SummaryVisible = false;
            ZakatExcemptionTPDetailsVisible = false;

            selectedPage = (int)PagesEnum.EntityDetails;
        }
        
        private void GoBackFromJustification()
        {
            CurrentIndex = 3;
            // IsBackVisible = true;
            ZakatExemptionYearVisible = false;
            EntityInformationVisible = false;
            AttachmentInformationVisible = true;
            SummaryVisible = false;
            JustificationInformationVisible = false;
            ZakatExcemptionTPDetailsVisible = false;

            selectedPage = (int)PagesEnum.Attachments;
        }
        
        private void GoBackFromSummaryView()
        {
            if (IsEditRequired || PageOnEditMode)
            {
                GoBackFromEntityDetailsView();
                _navigationService.GoBack();
            }
            else
            {
                CurrentIndex = 4;
                // IsBackVisible = true;
                ZakatExemptionYearVisible = false;
                EntityInformationVisible = false;
                AttachmentInformationVisible = false;
                SummaryVisible = false;
                JustificationInformationVisible = true;
                ZakatExcemptionTPDetailsVisible = false;
                selectedPage = (int)PagesEnum.Justification;
            }
        }
         
        private void GoBackFromExcepmtionYearPAge()
        {
            CurrentIndex = 0;
            // IsBackVisible = true;
            ZakatExcemptionTPDetailsVisible = true;
            ZakatExemptionYearVisible = false;
            EntityInformationVisible = false;
            AttachmentInformationVisible = false;
            SummaryVisible = false;
            JustificationInformationVisible = false;
            selectedPage = (int)PagesEnum.ZakatExcemptionTPDetails;
        }


        private void BackNavigations()
        {
            switch (selectedPage)
            {
                case (int)PagesEnum.ZakatExcemptionTPDetails:
                    _navigationService.GoBack();
                    break;
                case (int)PagesEnum.ZakatExemptionYear:
                    // _navigationService.GoBack();
                    GoBackFromExcepmtionYearPAge();
                    break;
                case (int)PagesEnum.EntityDetails:
                    GoBackFromEntityDetailsView();
                    break;
                case (int)PagesEnum.Attachments:
                    GoBackFromAttachmentsView();
                    break;
                case (int)PagesEnum.Justification:
                    GoBackFromJustification();
                    break;
                case (int)PagesEnum.Summary:
                    GoBackFromSummaryView();
                    break;
            }
        }

        private async void showPickerDialog()
        {
            try
            {
                setExemptionYearPickerModel();
                await MopupService.Instance.PushAsync(new PickerPageView(PickerModelExcemptionYear));

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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

        private void setExemptionYearPickerModel()
        {

            if (PickerModelExcemptionYear != null)
            {

                PickerModelExcemptionYear = null;
            }


            var list = new List<string>();

            foreach (PeriodKeySetResult dropdown in zakatExemptionModel.d.PeriodKeySet)
            {
                try
                {
                    list.Add(dropdown.Persl);
                }
                catch (Exception ex)
                {

                }

            }


            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "ExemptionYearPicker";
            genericPickerModel.PageCode = 1;
            PickerModelExcemptionYear = genericPickerModel;
            SelectedYear = "";
            

        }

        internal string GetYearIDByType(string selectedValue)
        {
            var index =  zakatExemptionModel.d.PeriodKeySet.FindIndex(x => x.Persl == selectedValue);
            return zakatExemptionModel.d.PeriodKeySet[index].Persl;
        }

        private async void showEntityTypeDialog()
        {
            try
            {
                setEntityTypePickerModel();
                await MopupService.Instance.PushAsync(new PickerPageView(PickerModelEntityType));

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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

        private async void showEntityCategoryPicker()
        {
            try
            {
                setEntityCategoryPicker();
                await MopupService.Instance.PushAsync(new PickerPageView(PickerModelEntityCategory));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void setEntityTypePickerModel()
        {

            if (PickerModelEntityType != null)
            {

                PickerModelEntityType = null;
            }


            var list = new List<string>();

            foreach (EntityListSetResult dropdown in zakatExemptionModel.d.EntityListSet)
            {
                try
                {
                    list.Add(dropdown.EntText);
                }
                catch (Exception ex)
                {

                }

            }


            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "EntityTypePicker";
            genericPickerModel.PageCode = 1;
            PickerModelEntityType = genericPickerModel;
            SelectedEntityID = "";

        }

        private void setEntityCategoryPicker()
        {
            if (PickerModelEntityCategory != null)
            {
                PickerModelEntityCategory = null;
            }
            var list = new List<string>();
            var categories = zakatExemptionModel.d.entityCategories.Where((e) => e.entityType.Equals(SelectedEntityID));
            foreach(EntityCategories option in categories)
            {
                try
                {
                    list.Add(option.name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            GenericPickerModel pickerModel = new GenericPickerModel();
            pickerModel.PickerData = list;
            pickerModel.PickerTitle = "";
            pickerModel.PickerId = "EntityCategory";
            pickerModel.PageCode = 1;
            PickerModelEntityCategory = pickerModel;
            SelectedEntityIDCategory = "";
        }

        internal string GetEntityIDByType(string selectedValue)
        {
            var index = zakatExemptionModel.d.EntityListSet.FindIndex(x => x.EntText == selectedValue);
            return zakatExemptionModel.d.EntityListSet[index].EntType;
        }

        internal string GetEntityTypeByID(string selectedValue)
        {
            var index = zakatExemptionModel.d.EntityListSet.FindIndex(x => x.EntType == selectedValue);
            return zakatExemptionModel.d.EntityListSet[index].EntText;
        }

        internal string GetTrustTypeByID(string selectedValue)
        {

            string trustType = "";

            if (App.IsArabic)
            {
                switch (selectedValue)
                {

                    case "1":
                        trustType = "شركة";
                        break;

                    case "2":
                        trustType = "مؤسسة";
                        break;
                    case "3":
                        trustType = "أخرى";
                        break;

                    default:
                        trustType = "";
                        break;
                }
            }
            else
            {
                switch (selectedValue)
                {
                    case "1":
                        trustType = "Company";
                        break;

                    case "2":
                        trustType = "Establishment";
                        break;
                    case "3":
                        trustType = "Other";
                        break;

                    default:
                        trustType = "";
                        break;
                }
            }
            return trustType;
        }

        private async void ShowCompanyEstablishmentOtherPicker()
        {
            var list = new List<string>();
            if (PickerModelEstOther != null)
            {
                PickerModelEstOther = null;
            }

            if(App.IsArabic)
            {
                list.Add("شركة");
                list.Add("مؤسسة");
                list.Add("أخرى");
            }
            else
            {
                list.Add("Company"); 
                list.Add("Establishment");
                list.Add("Other");
            }
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "CompanyEstablishmentOther";
            genericPickerModel.PageCode = 1;
            PickerModelEstOther = genericPickerModel;
            CompanyEstablishmentOther = "";

            try
            {
                await MopupService.Instance.PushAsync(new PickerPageView(PickerModelEstOther));
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
            var attachmentsListViewData = new ObservableCollection<Attachment>();

            foreach (Attachment attachemnt in attachments)
            {
                attachmentsListViewData.Add(attachemnt);
            }

            if (attachmentsListViewData.Count > 0)
            {
                if (attachmentsListViewData[0].Dotyp.Equals("ZEX6"))
                {
                    CompanyArticalsAttachmentsListViewData = attachmentsListViewData;
                    OnPropertyChanged("CompanyArticalsAttachmentsListViewData");
                }
                else if (attachmentsListViewData[0].Dotyp.Equals("ZEX7"))
                {
                    CertificateOfRegAttachmentsListViewData = attachmentsListViewData;
                }
                else if (attachmentsListViewData[0].Dotyp.Equals("ZEX8"))
                {
                    CharitableTrustAttachmentsListViewData = attachmentsListViewData;
                }
                else if (attachmentsListViewData[0].Dotyp.Equals("ZEX9"))
                {
                    CharityLicenseAttachmentsListViewData = attachmentsListViewData;
                }
                else if (attachmentsListViewData[0].Dotyp.Equals("ZEX0"))
                {
                    MemorandumOfAssociationAttachmentsListViewData = attachmentsListViewData;
                }
                else if (attachmentsListViewData[0].Dotyp.Equals("ZEXA"))
                {
                    OtherAttachmentsListViewData = attachmentsListViewData;
                }
                else if (attachmentsListViewData[0].Dotyp.Equals("ZEXD"))
                {
                    AdditionalAttachmentsListViewData = attachmentsListViewData;
                }

            }
            else
            {
                switch(SelectedAttachmentNumber){

                    case (int)WhichAttachment.ZakatExemtionAttachmentOne:
                            CompanyArticalsAttachmentsListViewData.Clear();
                        break;
                    case (int)WhichAttachment.ZakatExemtionAttachmentTwo:
                            CertificateOfRegAttachmentsListViewData.Clear();
                        break;
                    case (int)WhichAttachment.ZakatExemtionAttachmentThree:
                            CharitableTrustAttachmentsListViewData.Clear();
                        break;
                    case (int)WhichAttachment.ZakatExemtionAttachmentFour:
                            CharityLicenseAttachmentsListViewData.Clear();
                        break;
                    case (int)WhichAttachment.ZakatExemtionAttachmentFive:
                            MemorandumOfAssociationAttachmentsListViewData.Clear();
                        break;
                    case (int)WhichAttachment.ZakatExemtionAttachmentSix:
                            OtherAttachmentsListViewData.Clear();
                        break;
                    case (int)WhichAttachment.ZakatExemtionAttachmentSeven:
                            AdditionalAttachmentsListViewData.Clear();
                        break;

                } 
            }

            ZakatAttachmentConButtonEnabled = ActiveAttachmentContinueButton();
        }

        public  bool ActiveAttachmentContinueButton()
        {
            bool ActivateContinueButton = false;

            if (CompanyArticlesAttachmentVisible)
            {
                if (CompanyArticalsAttachmentsListViewData != null && CompanyArticalsAttachmentsListViewData.Count > 0)
                {
                    ActivateContinueButton = true;
                }
                else
                {
                    return false;
                }

            }
            
            if (CertificateRegAttachmentVisible)
            {
                if (CertificateOfRegAttachmentsListViewData != null && CertificateOfRegAttachmentsListViewData.Count > 0)
                {
                    ActivateContinueButton = true;
                }
                else
                {
                    return false;
                }
            }
            
            if (CharitableTrustAttachmentVisible)
            {
                if (CharitableTrustAttachmentsListViewData != null && CharitableTrustAttachmentsListViewData.Count > 0)
                {
                    ActivateContinueButton = true;
                }
                else
                {
                    return false;
                }
            }
            
            if (CharityAttachmentVisible)
            {
                if (CharityLicenseAttachmentsListViewData != null && CharityLicenseAttachmentsListViewData.Count > 0)
                {
                    ActivateContinueButton = true;
                }
                else
                {
                    return false;
                }
                
            }

            if (MemorandomAttachmentVisible)
            {
                if (MemorandumOfAssociationAttachmentsListViewData != null && MemorandumOfAssociationAttachmentsListViewData.Count > 0)
                {
                    ActivateContinueButton = true;
                }
                else
                {
                    return false;
                }
            }
           

            return ActivateContinueButton;
        }

        public async void NewCompanyArticalsAttachmentsPopup()
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            if (CompanyArticalsAttachmentsListViewData == null)
            {
                CompanyArticalsAttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {
                SelectedAttachmentNumber = (int)WhichAttachment.ZakatExemtionAttachmentOne;
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                    CompanyArticalsAttachmentsListViewData.ToList(),WhichAttachment.ZakatExemtionAttachmentOne,
                    zakatExemptionModel.d.ReturnIdz));

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
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

        public async void NewCertifcateOfRegAttachmentsPopup()
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            if (CertificateOfRegAttachmentsListViewData == null)
            {
                CertificateOfRegAttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {
                SelectedAttachmentNumber = (int)WhichAttachment.ZakatExemtionAttachmentTwo;
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                    CertificateOfRegAttachmentsListViewData.ToList(),
                    WhichAttachment.ZakatExemtionAttachmentTwo,
                    zakatExemptionModel.d.ReturnIdz));

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
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

        public async void NewCharitableTrustAttachmentsPopup()
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            if (CharitableTrustAttachmentsListViewData == null)
            {
                CharitableTrustAttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {
                SelectedAttachmentNumber = (int)WhichAttachment.ZakatExemtionAttachmentThree;
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                    CharitableTrustAttachmentsListViewData.ToList(),
                    WhichAttachment.ZakatExemtionAttachmentThree,
                    zakatExemptionModel.d.ReturnIdz));

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
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

        public async void NewCharityLicenseAttachmentsPopup()
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            if (CharityLicenseAttachmentsListViewData == null)
            {
                CharityLicenseAttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {
                SelectedAttachmentNumber = (int)WhichAttachment.ZakatExemtionAttachmentFour;
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                    CharityLicenseAttachmentsListViewData.ToList(),
                    WhichAttachment.ZakatExemtionAttachmentFour,
                    zakatExemptionModel.d.ReturnIdz));

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
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

        public async void NewMemoRandomAttachmentsPopup()
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            if (MemorandumOfAssociationAttachmentsListViewData == null)
            {
                MemorandumOfAssociationAttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {
                SelectedAttachmentNumber = (int)WhichAttachment.ZakatExemtionAttachmentFive;
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                    MemorandumOfAssociationAttachmentsListViewData.ToList(),
                    WhichAttachment.ZakatExemtionAttachmentFive,
                    zakatExemptionModel.d.ReturnIdz));

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
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
        
        public async void NewOtherAttachmentsPopup()
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            if (OtherAttachmentsListViewData == null)
            {
                OtherAttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {
                SelectedAttachmentNumber = (int)WhichAttachment.ZakatExemtionAttachmentSix;
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                    OtherAttachmentsListViewData.ToList(),
                    WhichAttachment.ZakatExemtionAttachmentSix,
                    zakatExemptionModel.d.ReturnIdz));

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
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

        public async void NewAddtionalAtachmentPopUp()
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            if (AdditionalAttachmentsListViewData == null)
            {
                AdditionalAttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {
                SelectedAttachmentNumber = (int)WhichAttachment.ZakatExemtionAttachmentSeven;
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                    AdditionalAttachmentsListViewData.ToList(),
                    WhichAttachment.ZakatExemtionAttachmentSeven,
                    zakatExemptionModel.d.ReturnIdz));

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
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

        private ZakatExcemptionReqModel CreateRequestData()
        {
            ZakatExcemptionReqModel zakatExcemptionReq = new ZakatExcemptionReqModel();

            zakatExcemptionReq.__metadata = new List<MyMetaData>();
            zakatExcemptionReq.Decfg = "X";
            zakatExcemptionReq.Erftime = zakatExemptionModel.d.Erftime;
            zakatExcemptionReq.Recom01 = zakatExemptionModel.d.Recom01;
            zakatExcemptionReq.SuDecfg = zakatExemptionModel.d.SuDecfg;
            zakatExcemptionReq.Fbtypz = zakatExemptionModel.d.Fbtypz;
            zakatExcemptionReq.Recom02 = zakatExemptionModel.d.Recom02;
            zakatExcemptionReq.Fbustz = zakatExemptionModel.d.Fbustz;
            zakatExcemptionReq.Mandtz = zakatExemptionModel.d.Mandtz;
            zakatExcemptionReq.Decision = zakatExemptionModel.d.Decision;
            zakatExcemptionReq.TransactionTypez = "CRE_ZERQ";
            zakatExcemptionReq.UserTyp = zakatExemptionModel.d.UserTyp;
            zakatExcemptionReq.Aentime = zakatExemptionModel.d.Aentime;
            zakatExcemptionReq.EditFgz = zakatExemptionModel.d.EditFgz;
            zakatExcemptionReq.Fbnumz = zakatExemptionModel.d.Fbnumz;
            zakatExcemptionReq.Mandt = zakatExemptionModel.d.Mandt;
            zakatExcemptionReq.PortalUsrz = zakatExemptionModel.d.PortalUsrz;
            zakatExcemptionReq.FormGuid = zakatExemptionModel.d.FormGuid;
            zakatExcemptionReq.Langz = zakatExemptionModel.d.Langz;
            zakatExcemptionReq.DataVersion = zakatExemptionModel.d.DataVersion;
            zakatExcemptionReq.Operationz = "01"; //*
            zakatExcemptionReq.StepNumberz = "04";//*
            zakatExcemptionReq.Tin = zakatExemptionModel.d.Tin;
            zakatExcemptionReq.ReturnIdz = zakatExemptionModel.d.ReturnIdz;
            zakatExcemptionReq.TinName = zakatExemptionModel.d.TinName;
            zakatExcemptionReq.CrNumber = zakatExemptionModel.d.CrNumber;
            zakatExcemptionReq.Officerz = zakatExemptionModel.d.Officerz;
            zakatExcemptionReq.Fbnum = zakatExemptionModel.d.Fbnum;
            zakatExcemptionReq.Gpartz = zakatExemptionModel.d.Tin;
            zakatExcemptionReq.Persl = SelectedYear;//*
            zakatExcemptionReq.Statusz = zakatExemptionModel.d.Statusz;
            zakatExcemptionReq.Actnm = zakatExemptionModel.d.Actnm;
            zakatExcemptionReq.UserTypz = zakatExemptionModel.d.UserTypz;
            zakatExcemptionReq.Mobile = zakatExemptionModel.d.Mobile;
            zakatExcemptionReq.TxnTpz = "CRE_ZERQ";
            zakatExcemptionReq.Email = zakatExemptionModel.d.Email;
            zakatExcemptionReq.Formprocz = zakatExemptionModel.d.Formprocz;
            zakatExcemptionReq.Inpch = zakatExemptionModel.d.Inpch;
            zakatExcemptionReq.OfficerTz = zakatExemptionModel.d.OfficerTz;
            zakatExcemptionReq.Erfuser = zakatExemptionModel.d.Erfuser;
            zakatExcemptionReq.SrcAppz = zakatExemptionModel.d.SrcAppz;
            zakatExcemptionReq.Aenuser = zakatExemptionModel.d.Aenuser;
            zakatExcemptionReq.InChannelz = zakatExemptionModel.d.InChannelz;
            zakatExcemptionReq.Fbguid = zakatExemptionModel.d.Fbguid;
            zakatExcemptionReq.Euser = zakatExemptionModel.d.Euser;
            zakatExcemptionReq.EnType = SelectedEntityID;
            zakatExcemptionReq.entityCategory = _selectedTechincalNumber;

            
           //*
            if (CompanyEstablishmentOther.ToLower().Equals("company") || CompanyEstablishmentOther.Equals("شركة"))
            {
                zakatExcemptionReq.TypTrust = "1";
            }
            else if (CompanyEstablishmentOther.ToLower().Equals("establishment") || CompanyEstablishmentOther.Equals("مؤسسة"))
            {
                zakatExcemptionReq.TypTrust = "2";
            }
            else if(CompanyEstablishmentOther.ToLower().Equals("other") || CompanyEstablishmentOther.Equals("أخرى"))
            {
                zakatExcemptionReq.TypTrust = "3";
            }
            else
            {
                zakatExcemptionReq.TypTrust = "";
            }

            zakatExcemptionReq.EnNature = NatureOfEntity;
            zakatExcemptionReq.OffSprP = OffSpringPercentage.ToString();//*

            zakatExcemptionReq.CharityP = CharityPercentage.ToString(); //*

            zakatExcemptionReq.LastAFbnum = zakatExemptionModel.d.LastAFbnum;
            zakatExcemptionReq.ReturnId = zakatExemptionModel.d.ReturnId;
            zakatExcemptionReq.LastADate = zakatExemptionModel.d.LastADate;
            zakatExcemptionReq.LastADueAmt = zakatExemptionModel.d.LastADueAmt;
            zakatExcemptionReq.LastAPaidAmt = zakatExemptionModel.d.LastAPaidAmt;
            zakatExcemptionReq.CrDate = zakatExemptionModel.d.CrDate;
            //zakatExcemptionReq.CommDate = zakatExemptionModel.d.CommDate.ToString();
            zakatExcemptionReq.Erfdate = zakatExemptionModel.d.Erfdate;
            zakatExcemptionReq.Aendate = zakatExemptionModel.d.Aendate;
            zakatExcemptionReq.LItemSet = new List<object>();

            List<Models.Off_notesSet> OffNotesList = new List<Models.Off_notesSet>();

            {
                Models.Off_notesSet offNotesSet = new Models.Off_notesSet();
                offNotesSet.DataVersionz = zakatExemptionModel.d.DataVersion;
                offNotesSet.ByGpartz = zakatExemptionModel.d.Tin;
                offNotesSet.AttByz = "TP";

                offNotesSet.Notenoz = "1";
                offNotesSet.Refnamez = string.Empty;
                offNotesSet.XInvoicez = string.Empty;
                offNotesSet.XObsoletez = string.Empty;
                offNotesSet.Rcodez = "ZKT_EXMP1";
                offNotesSet.Erfusrz = string.Empty;
                offNotesSet.Erfdtz = null;
                offNotesSet.Erftmz = null;
                offNotesSet.Noteno = "1";
                offNotesSet.Lineno = 1;
                offNotesSet.ElemNo = 0;
                offNotesSet.Tdformat = string.Empty;
                offNotesSet.Tdline = JusticationOne;
                OffNotesList.Add(offNotesSet);
            }
          
            if (IsJustificationOneVisible && JusticationTwo.Trim().Length > 0)
            {
                Models.Off_notesSet offNotesSet = new Models.Off_notesSet();
                offNotesSet.DataVersionz = zakatExemptionModel.d.DataVersion;
                offNotesSet.ByGpartz = zakatExemptionModel.d.Tin;
                offNotesSet.AttByz = "TP";

                offNotesSet.Notenoz = "2";
                offNotesSet.Refnamez = string.Empty;
                offNotesSet.XInvoicez = string.Empty;
                offNotesSet.XObsoletez = string.Empty;
                offNotesSet.Rcodez = "ZKT_EXMP2";
                offNotesSet.Erfusrz = string.Empty;
                offNotesSet.Erfdtz = null;
                offNotesSet.Erftmz = null;
                offNotesSet.Noteno = "2";
                offNotesSet.Lineno = 1;
                offNotesSet.ElemNo = 0;
                offNotesSet.Tdformat = string.Empty;
                offNotesSet.Tdline = JusticationTwo;
                OffNotesList.Add(offNotesSet);
            }

            if (IsJustificationTwoVisible && JusticationThree.Trim().Length > 0)
            {
                Models.Off_notesSet offNotesSet = new Models.Off_notesSet();
                offNotesSet.DataVersionz = zakatExemptionModel.d.DataVersion;
                offNotesSet.ByGpartz = zakatExemptionModel.d.Tin;
                offNotesSet.AttByz = "TP";

                offNotesSet.Notenoz = "3";
                offNotesSet.Refnamez = string.Empty;
                offNotesSet.XInvoicez = string.Empty;
                offNotesSet.XObsoletez = string.Empty;
                offNotesSet.Rcodez = "ZKT_EXMP3";
                offNotesSet.Erfusrz = string.Empty;
                offNotesSet.Erfdtz = null;
                offNotesSet.Erftmz = null;
                offNotesSet.Noteno = "3";
                offNotesSet.Lineno = 1;
                offNotesSet.ElemNo = 0;
                offNotesSet.Tdformat = string.Empty;
                offNotesSet.Tdline = JusticationThree;
                OffNotesList.Add(offNotesSet);
            }

            if (IsJustificationThreeVisible && JusticationFour.Trim().Length > 0)
            {
                Models.Off_notesSet offNotesSet = new Models.Off_notesSet();
                offNotesSet.DataVersionz = zakatExemptionModel.d.DataVersion;
                offNotesSet.ByGpartz = zakatExemptionModel.d.Tin;
                offNotesSet.AttByz = "TP";

                offNotesSet.Notenoz = "4";
                offNotesSet.Refnamez = string.Empty;
                offNotesSet.XInvoicez = string.Empty;
                offNotesSet.XObsoletez = string.Empty;
                offNotesSet.Rcodez = "ZKT_EXMP4";
                offNotesSet.Erfusrz = string.Empty;
                offNotesSet.Erfdtz = null;
                offNotesSet.Erftmz = null;
                offNotesSet.Noteno = "4";
                offNotesSet.Lineno = 1;
                offNotesSet.ElemNo = 0;
                offNotesSet.Tdformat = string.Empty;
                offNotesSet.Tdline = JusticationFour;
                OffNotesList.Add(offNotesSet);
            }

            if (IsJustificationFourVisible && JusticationFive.Trim().Length > 0)
            {
                Models.Off_notesSet offNotesSet = new Models.Off_notesSet();
                offNotesSet.DataVersionz = zakatExemptionModel.d.DataVersion;
                offNotesSet.ByGpartz = zakatExemptionModel.d.Tin;
                offNotesSet.AttByz = "TP";

                offNotesSet.Notenoz = "5";
                offNotesSet.Refnamez = string.Empty;
                offNotesSet.XInvoicez = string.Empty;
                offNotesSet.XObsoletez = string.Empty;
                offNotesSet.Rcodez = "ZKT_EXMP5";
                offNotesSet.Erfusrz = string.Empty;
                offNotesSet.Erfdtz = null;
                offNotesSet.Erftmz = null;
                offNotesSet.Noteno = "5";
                offNotesSet.Lineno = 1;
                offNotesSet.ElemNo = 0;
                offNotesSet.Tdformat = string.Empty;
                offNotesSet.Tdline = JusticationFive;
                OffNotesList.Add(offNotesSet);
            }


            zakatExcemptionReq.Off_notesSet = OffNotesList;

            zakatExcemptionReq.AttDetSet = new List<Object>();
            zakatExcemptionReq.PeriodKeySet = new List<Object>();
            zakatExcemptionReq.EntityListSet = new List<Object>();
            zakatExcemptionReq.Recom_02Set = new List<Object>();
            zakatExcemptionReq.Recom_01Set = new List<Object>();
            zakatExcemptionReq.DecisionSet = new List<Object>();
            zakatExcemptionReq.ZerqBtnSet = new List<Object>();

            var serilized = JsonConvert.SerializeObject(zakatExcemptionReq);
            
            return zakatExcemptionReq;
        }

        internal void SetSummaryDetails(ZakatExemptionModel zakatExemptionModels)
        {
            zakatExemptionModel = zakatExemptionModels;
            PageOnEditMode = zakatExemptionModel.d.OpenPageOnEdit;
            CurrentIndex = 5;
            ZakatExemptionYearVisible = false;
            EntityInformationVisible = false;
            AttachmentInformationVisible = false;
            JustificationInformationVisible = false;
            SummaryVisible = true;
            selectedPage = (int)PagesEnum.Summary;

            SetupSummaryDetailsOnItemTap(zakatExemptionModels);
        }

        private void SetupSummaryDetailsOnItemTap(ZakatExemptionModel zakatExemptionModel)
        {
            if (PageOnEditMode)
            {
                if (zakatExemptionModel.d.Statusz.Equals("E0019") || zakatExemptionModel.d.Statusz.Equals("E0031"))
                {//edit enable
                    IsEditRequired = true;
                    ShowTermsOnSubmit = true;
                }
                else
                {
                    IsEditRequired = false;
                    ShowTermsOnSubmit = false;
                }

                SelectedYear = zakatExemptionModel.d.Persl;
                SelectedEntityType = GetEntityTypeByID(zakatExemptionModel.d.EnType);
                SelectedEntityID = zakatExemptionModel.d.EnType;
                //
                if (string.Equals(SelectedEntityID, "03") || string.Equals(SelectedEntityID, "3"))
                {
                    IsCharitabletrustsAndFoundations = true;
                    CompanyEstablishmentOther = GetTrustTypeByID(zakatExemptionModel.d.TypTrust);
                }
                else
                {
                    IsCharitabletrustsAndFoundations = false;
                    CompanyEstablishmentOther = string.Empty;
                }


                NatureOfEntity = zakatExemptionModel.d.EnNature;

                if (zakatExemptionModel.d.OffSprP.Length > 0)
                {
                    CharityPercentage = Convert.ToDouble(zakatExemptionModel.d.OffSprP);
                }
                if (zakatExemptionModel.d.CharityP.Length > 0)
                {
                    CharityPercentage = Convert.ToDouble(zakatExemptionModel.d.CharityP);
                }

                if (zakatExemptionModel.d.AttDetSet.Count > 0)
                {
                    CompanyArticalsAttachmentsListViewData = new ObservableCollection<Attachment>();
                    CertificateOfRegAttachmentsListViewData = new ObservableCollection<Attachment>();
                    CharitableTrustAttachmentsListViewData = new ObservableCollection<Attachment>();
                    CharityLicenseAttachmentsListViewData = new ObservableCollection<Attachment>();
                    MemorandumOfAssociationAttachmentsListViewData = new ObservableCollection<Attachment>();
                    OtherAttachmentsListViewData = new ObservableCollection<Attachment>();

                    foreach (var item in zakatExemptionModel.d.AttDetSet)
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
                            ByPusr = item.ByPusr,
                            Erfdt = item.Erfdt,
                            Erftm = item.Erftm,
                            DataVersion = item.DataVersion,
                            DocUrl = item.DocUrl,
                            OutletRef = item.OutletRef,
                            Enbedit = item.Enbedit,
                            Enbdele = item.Enbdele,
                            Visedit = item.Visedit,
                            Visdel = item.Visdel,
                        };

                        if (item.Dotyp.Equals("ZEX6"))
                        {
                            CompanyArticalsAttachmentsListViewData.Add(attachment);
                            CompanyArticlesAttachmentVisible = true;
                        }
                        else if (item.Dotyp.Equals("ZEX7"))
                        {
                            CertificateOfRegAttachmentsListViewData.Add(attachment);
                            CertificateRegAttachmentVisible = true;
                        }
                        else if (item.Dotyp.Equals("ZEX8"))
                        {
                            CharitableTrustAttachmentsListViewData.Add(attachment);
                            CharitableTrustAttachmentVisible = true;
                        }
                        else if (item.Dotyp.Equals("ZEX9"))
                        {
                            CharityLicenseAttachmentsListViewData.Add(attachment);
                            CharitableTrustAttachmentVisible = true;
                        }
                        else if (item.Dotyp.Equals("ZEX0"))
                        {
                            MemorandumOfAssociationAttachmentsListViewData.Add(attachment);
                            MemorandomAttachmentVisible = true;
                        }
                        else if (item.Dotyp.Equals("ZEXA"))
                        {
                            OtherAttachmentsListViewData.Add(attachment);
                            OtherAttachmentVisible = true;
                        }
                    }
                }

                if (zakatExemptionModel.d.OffNotesSet.Count > 0)
                {
                    foreach (var notesItem in zakatExemptionModel.d.OffNotesSet)
                    {
                        if (notesItem.Rcodez.Equals("ZKT_EXMP1"))
                        {
                            JusticationOne = notesItem.Strline;
                        }
                        else if (notesItem.Rcodez.Equals("ZKT_EXMP2"))
                        {
                            JusticationTwo = notesItem.Strline;
                            IsJustificationOneVisible = true;
                        }
                        else if (notesItem.Rcodez.Equals("ZKT_EXMP3"))
                        {
                            JusticationThree = notesItem.Strline;
                            IsJustificationTwoVisible = true;
                        }
                        else if (notesItem.Rcodez.Equals("ZKT_EXMP4"))
                        {
                            JusticationFour = notesItem.Strline;
                            IsJustificationThreeVisible = true;
                        }
                        else if (notesItem.Rcodez.Equals("ZKT_EXMP5"))
                        {
                            JusticationFive = notesItem.Strline;
                            IsJustificationFourVisible = true;
                        }
                    }
                }
            }
            else
            {
                IsEditRequired = false;
                ShowTermsOnSubmit = true;
                PageOnEditMode = false;
            }

            
        }

        private void setAttachments()
        {
            List<ZakatExemptionModel.AttachmentModel> attachmentModels = new List<ZakatExemptionModel.AttachmentModel> ();
            FilteredAttachments = zakatExemptionModel.d.entityAttachments.Where((e)=>e.entityType==SelectedEntityID && e.technicalNumber==_selectedTechincalNumber).ToList();
            foreach (var attachment in FilteredAttachments)
            {
                ZakatExemptionModel.AttachmentModel attachmentModel = new ZakatExemptionModel.AttachmentModel();
                attachmentModel.Title = attachment.name;
                attachmentModels.Add(attachmentModel);
            }
            SelectedAttachments = attachmentModels;
        }
    }
}


