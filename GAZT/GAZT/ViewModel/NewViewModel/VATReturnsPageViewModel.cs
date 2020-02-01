using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using GAZT.Helper;

namespace GAZT.ViewModel.NewViewModel
{
    public class VATReturnsPageViewModel : ViewModelBase
    {
        #region Variable

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnStepButtonClicked { get; set; }
        public ICommand onInstructionsClicked { get; set; }
        public ICommand onTaxPayerDetailsClicked { get; set; }
        public ICommand onVATReturnFormClicked { get; set; }
        public ICommand onSummaryClicked { get; set; }
        public ICommand OnSaveAsDraftClicked { get; set; }
        public ICommand onOptionClicked { get; set; }
        public ICommand OnAttachmentClick { get; set; }
        public ICommand OnVATRefreshButtonClicked { get; set; }
        public ICommand OnDownloadAcknowlwdgementClicked { get; set; }
        public ICommand OnAcknowlwdgementClicked { get; set; }

        bool IsFirstSubmission = true;
        
        byte[] attachment;
        public ICommand onCreditCarriedForwardClicked { get; set; }
        


        public ICommand onStandardRatedSalesVatAmountTapped { get; set; }




        #endregion

        #region Property

        private int _firstSubmissionCount=0;
        public int FirstSubmissionCount
        {
            get
            {
                return _firstSubmissionCount;
            }
            set
            {
                _firstSubmissionCount = value;
                RaisePropertyChanged("FirstSubmissionCount");
            }
        }


        private VATDeclaration _responseVatDeclaration;
        public VATDeclaration ResponseVatDeclaration
        {
            get
            {
                return _responseVatDeclaration;
            }
            set
            {
                _responseVatDeclaration = value;
                RaisePropertyChanged("ResponseVatDeclaration");
            }
        }


        private String _stepNumber;
        public String StepNumber
        {
            get
            {
                return _stepNumber;
            }
            set
            {
                _stepNumber = value;
                RaisePropertyChanged("StepNumber");
            }
        }

        private bool _isLoading = false;
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

        private bool _isDeclarationChecked = false;
        public bool IsDeclarationChecked
        {
            get
            {
                return _isDeclarationChecked;
            }
            set
            {
                _isDeclarationChecked = value;
                RaisePropertyChanged("IsDeclarationChecked");
            }
        }


        private VATDeclarationD _vATDeclarationD;
        public VATDeclarationD VATDeclarationD
        {
            get
            {
                return _vATDeclarationD;
            }
            set
            {
                _vATDeclarationD = value;
                RaisePropertyChanged("VATDeclarationD");
            }
        }

        private VATDeclaration _vATDeclarationData;
        public VATDeclaration VATDeclarationData
        {
            get
            {
                return _vATDeclarationData;
            }
            set
            {
                _vATDeclarationData = value;
                RaisePropertyChanged("VATDeclarationData");
            }
        }


        private List<CreditCarried> _creditCarriedsList;
        public List<CreditCarried> CreditCarriedsList
        {
            get
            {
                return _creditCarriedsList;
            }
            set
            {
                _creditCarriedsList = value;
                RaisePropertyChanged("CreditCarriedsList");
            }
        }

        private List<VATDeclarationTabbedPageName> _vatTabbledPageList;
        public List<VATDeclarationTabbedPageName> VatTabbledPageList
        {
            get
            {
                return _vatTabbledPageList;
            }
            set
            {
                _vatTabbledPageList = value;
                RaisePropertyChanged("VatTabbledPageList");
            }
        }

        private ObservableCollection<Attachment> _vatAttachmentsList;
        public ObservableCollection<Attachment> VatAttachmentsList
        {
            get
            {
                return _vatAttachmentsList;
            }
            set
            {
                _vatAttachmentsList = value;
                RaisePropertyChanged("VatAttachmentsList");
            }
        }



        private bool _isVisibleOptionMenu = false;
        public bool IsVisibleOptionMenu
        {
            get
            {
                return _isVisibleOptionMenu;
            }
            set
            {
                _isVisibleOptionMenu = value;
                RaisePropertyChanged("IsVisibleOptionMenu");
            }
        }


        private bool _isVisibleVatReturnForm = false;
        public bool IsVisibleVatReturnForm
        {
            get
            {
                return _isVisibleVatReturnForm;
            }
            set
            {
                _isVisibleVatReturnForm = value;
                RaisePropertyChanged("IsVisibleVatReturnForm");
            }
        }

        private bool _isVisibleTaxPayerDetails = false;
        public bool IsVisibleTaxPayerDetails
        {
            get
            {
                return _isVisibleTaxPayerDetails;
            }
            set
            {
                _isVisibleTaxPayerDetails = value;
                RaisePropertyChanged("IsVisibleTaxPayerDetails");
            }
        }


        private bool _isVisibleCreditCarriedForward = false;
        public bool IsVisibleCreditCarriedForward
        {
            get
            {
                return _isVisibleCreditCarriedForward;
            }
            set
            {
                _isVisibleCreditCarriedForward = value;
                RaisePropertyChanged("IsVisibleCreditCarriedForward");
            }
        }

        private bool _isVisibleSummary = false;
        public bool IsVisibleSummary
        {
            get
            {
                return _isVisibleSummary;
            }
            set
            {
                _isVisibleSummary = value;
                RaisePropertyChanged("IsVisibleSummary");
            }
        }

        private bool _isVisibleAttachments = false;
        public bool IsVisibleAttachments
        {
            get
            {
                return _isVisibleAttachments;
            }
            set
            {
                _isVisibleAttachments = value;
                RaisePropertyChanged("IsVisibleAttachments");
            }
        }


        private bool _isVisibleAcknowledgment = false;
        public bool IsVisibleAcknowledgment
        {
            get
            {
                return _isVisibleAcknowledgment;
            }
            set
            {
                _isVisibleAcknowledgment = value;
                RaisePropertyChanged("IsVisibleAcknowledgment");
            }
        }



        private bool _isCheckedTaxPayerDetailsInfo = false;
        public bool IsCheckedTaxPayerDetailsInfo
        {
            get
            {
                return _isCheckedTaxPayerDetailsInfo;
            }
            set
            {
                _isCheckedTaxPayerDetailsInfo = value;
                if (_isCheckedTaxPayerDetailsInfo == true)
                {
                    IsMainButtonEnabled = true;
                }
                else
                {
                    IsMainButtonEnabled = false;
                }
                RaisePropertyChanged("IsCheckedTaxPayerDetailsInfo");
            }
        }


        private bool _isDeclarationCheckedForSummary = false;
        public bool IsDeclarationCheckedForSummary
        {
            get
            {
                return _isDeclarationCheckedForSummary;
            }
            set
            {
                _isDeclarationCheckedForSummary = value;
                if (_isDeclarationCheckedForSummary == true)
                {
                    IsMainButtonEnabled = true;
                }
                else
                {
                    IsMainButtonEnabled = false;
                }
                RaisePropertyChanged("IsDeclarationCheckedForSummary");
            }
        }



        private bool _isDeclarationCheckedForInstruction = false;
        public bool IsDeclarationCheckedForInstruction
        {
            get
            {
                return _isDeclarationCheckedForInstruction;
            }
            set
            {
                _isDeclarationCheckedForInstruction = value;
                if (_isDeclarationCheckedForInstruction == true)
                {
                    IsMainButtonEnabled = true;
                }
                else
                {
                    IsMainButtonEnabled = false;
                }
                RaisePropertyChanged("IsDeclarationCheckedForInstruction");
            }
        }


        private bool _isMainButtonEnabled = false;
        public bool IsMainButtonEnabled
        {
            get
            {
                return _isMainButtonEnabled;
            }
            set
            {
                _isMainButtonEnabled = value;
                RaisePropertyChanged("IsMainButtonEnabled");
            }
        }

        private bool _isTaxPayerControlEnabled = false;
        public bool IsTaxPayerControlEnabled
        {
            get
            {
                return _isTaxPayerControlEnabled;
            }
            set
            {
                _isTaxPayerControlEnabled = value;
                RaisePropertyChanged("IsTaxPayerControlEnabled");
            }
        }

        private bool _isVisibleNotes = false;
        public bool IsVisibleNotes
        {
            get
            {
                return _isVisibleNotes;
            }
            set
            {
                _isVisibleNotes = value;
                RaisePropertyChanged("IsVisibleNotes");
            }
        }


        private bool _isVisibleInstrunction = false;
        public bool IsVisibleInstrunction
        {
            get
            {
                return _isVisibleInstrunction;
            }
            set
            {
                _isVisibleInstrunction = value;
                RaisePropertyChanged("IsVisibleInstrunction");
            }
        }



        private bool _isTabbedMenuAvailable = true;
        public bool IsTabbedMenuAvailable
        {
            get
            {
                return _isTabbedMenuAvailable;
            }
            set
            {
                _isTabbedMenuAvailable = value;
                RaisePropertyChanged("IsTabbedMenuAvailable");
            }
        }

        private bool _isVisibleCreditCarriedLabel = false;
        public bool IsVisibleCreditCarriedLabel
        {
            get
            {
                return _isVisibleCreditCarriedLabel;
            }
            set
            {
                _isVisibleCreditCarriedLabel = value;
                RaisePropertyChanged("IsVisibleCreditCarriedLabel");
            }
        }



        private string _pageFontSize = "10";
        public string PageFontSize
        {
            get
            {
                return _pageFontSize;
            }
            set
            {
                _pageFontSize = value;

                RaisePropertyChanged("PageFontSize");
            }
        }

        private string _attachmentName = "Attachments";
        public string AttachmentName
        {
            get
            {
                return _attachmentName;
            }
            set
            {
                _attachmentName = value;

                RaisePropertyChanged("AttachmentName");
            }
        }



        private string _buttonName = AppResources.ZVatStepTwo;
        public string ButtonName
        {
            get
            {
                return _buttonName;
            }
            set
            {
                _buttonName = value;

                RaisePropertyChanged("ButtonName");
            }
        }

        private string _noteText = "";
        public string NoteText
        {
            get
            {
                return _noteText;
            }
            set
            {
                _noteText = value;

                RaisePropertyChanged("NoteText");
            }
        }

        private List<Note> _responseNote;
        public List<Note> ResponseNote
        {
            get
            {
                return _responseNote;
            }
            set
            {
                _responseNote = value;

                RaisePropertyChanged("ResponseNote");
            }
        }

        private List<Result2> _responseIBANSET;
        public List<Result2> ResponseIBANSET
        {
            get
            {
                return _responseIBANSET;
            }
            set
            {
                _responseIBANSET = value;

                RaisePropertyChanged("ResponseIBANSET");
            }
        }


        private List<Result3> _responseCFSET;
        public List<Result3> ResponseCFSET
        {
            get
            {
                return _responseCFSET;
            }
            set
            {
                _responseCFSET = value;

                RaisePropertyChanged("ResponseCFSET");
            }
        }

        private List<Attachment> _responseAttachSet;
        public List<Attachment> ResponseAttachSet
        {
            get
            {
                return _responseAttachSet;
            }
            set
            {
                _responseAttachSet = value;

                RaisePropertyChanged("ResponseAttachSet");
            }
        }

        private List<Result5> _responseAddressSET;
        public List<Result5> ResponseAddressSET
        {
            get
            {
                return _responseAddressSET;
            }
            set
            {
                _responseAddressSET = value;

                RaisePropertyChanged("ResponseResult5");
            }
        }


        private List<object> _responseobject;
        public List<object> Responseobject
        {
            get
            {
                return _responseobject;
            }
            set
            {
                _responseobject = value;

                RaisePropertyChanged("Responseobject");
            }
        }

        private VATDeclarationD _responseVATDeclarationD;
        public VATDeclarationD ResponseVATDeclarationD
        {
            get
            {
                return _responseVATDeclarationD;
            }
            set
            {
                _responseVATDeclarationD = value;

                RaisePropertyChanged("ResponseVATDeclarationD");
            }
        }



        private VATDeclarationTabbedPageName _pageSelectedItems;
        public VATDeclarationTabbedPageName PageSelectedItems
        {
            get
            {
                return _pageSelectedItems;
            }
            set
            {
                _pageSelectedItems = value;

                RaisePropertyChanged("PageSelectedItems");
            }
        }

        private VATDeclarationTabbedPageName _pageSelectedItem;
        public VATDeclarationTabbedPageName PageSelectedItem
        {
            get
            {
                return _pageSelectedItem;
            }
            set
            {
                _pageSelectedItem = value;

                RaisePropertyChanged("PageSelectedItem");
            }
        }

        private string _vATRate001;
        public string VATRate001
        {
            get
            {
                return _vATRate001;
            }
            set
            {
                _vATRate001 = value;

                RaisePropertyChanged("VATRate001");
            }
        }

        private string _vATRate002;
        public string VATRate002
        {
            get
            {
                return _vATRate002;
            }
            set
            {
                _vATRate002 = value;

                RaisePropertyChanged("VATRate002");
            }
        }


        private string _tPName ="";
        public string TPName
        {
            get
            {
                return _tPName;
            }
            set
            {
                _tPName = value;

                RaisePropertyChanged("TPName");
            }
        }

        private string _returnReferenceNumber = "";
        public string ReturnReferenceNumber
        {
            get
            {
                return _returnReferenceNumber;
            }
            set
            {
                _returnReferenceNumber = value;

                RaisePropertyChanged("ReturnReferenceNumber");
            }
        }

        private string _taxablePeriod = "";
        public string TaxablePeriod
        {
            get
            {
                return _taxablePeriod;
            }
            set
            {
                _taxablePeriod = value;

                RaisePropertyChanged("TaxablePeriod");
            }
        }


        private string _receiptDate = "";
        public string ReceiptDate
        {
            get
            {
                return _receiptDate;
            }
            set
            {
                _receiptDate = value;
                RaisePropertyChanged("ReceiptDate");
            }
        }


        private string _sadadNumber = "";
        public string SadadNumber
        {
            get
            {
                return _sadadNumber;
            }
            set
            {
                _sadadNumber = value;
                RaisePropertyChanged("SadadNumber");
            }
        }


        private List<VATCalculationDataVATRSet> _calculationRateSet;
        public List<VATCalculationDataVATRSet> CalculationRateSet
        {
            get
            {
                return _calculationRateSet;
            }
            set
            {
                _calculationRateSet = value;

                RaisePropertyChanged("CalculationRateSet");
            }
        }

        private string _correctionPeriodAmount;
        public string CorrectionPeriodAmount
        {
            get
            {
                return _correctionPeriodAmount;
            }
            set
            {
                _correctionPeriodAmount = value;

                RaisePropertyChanged("CorrectionPeriodAmount");
            }
        }




        #region NewProperty

        public string _totalsalesAmt;
        public string TotalsalesAmt
        {
            get
            {
                return _totalsalesAmt;
            }
            set
            {
                _totalsalesAmt = value;
                RaisePropertyChanged("TotalsalesAmt");
            }
        }

        public string _totalsalesAdj;
        public string TotalsalesAdj
        {
            get
            {
                return _totalsalesAdj;
            }
            set
            {
                _totalsalesAdj = value;
                RaisePropertyChanged("TotalsalesAdj");
            }
        }

        public string _totalpurchaseAmt;
        public string TotalpurchaseAmt
        {
            get
            {
                return _totalpurchaseAmt;
            }
            set
            {
                _totalpurchaseAmt = value;
                RaisePropertyChanged("TotalpurchaseAmt");
            }
        }

        public string _totalpurchaseAdj;
        public string TotalpurchaseAdj
        {
            get
            {
                return _totalpurchaseAdj;
            }
            set
            {
                _totalpurchaseAdj = value;
                RaisePropertyChanged("TotalpurchaseAdj");
            }
        }

        public string _stdsalesVat;
        public string StdsalesVat
        {
            get
            {
                return _stdsalesVat;
            }
            set
            {
                _stdsalesVat = value;
                RaisePropertyChanged("StdsalesVat");
            }
        }

        public string _totaldueVat;
        public string TotaldueVat
        {
            get
            {
                return _totaldueVat;
            }
            set
            {
                _totaldueVat = value;
                if(_totaldueVat!=null)
                {
                    NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                }
                RaisePropertyChanged("TotaldueVat");
            }
        }

        public string _preperiodcorr;
        public string Preperiodcorr
        {
            get
            {
                return _preperiodcorr;
            }
            set
            {
                _preperiodcorr = value;
                if (_preperiodcorr != null)
                {
                    NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                }
                RaisePropertyChanged("Preperiodcorr");
            }
        }

        public string _netdueVat;
        public string NetdueVat
        {
            get
            {
                return _netdueVat;
            }
            set
            {
                _netdueVat = value;
               
                RaisePropertyChanged("NetdueVat");
            }
        }


        

        public string _creditVat;
        public string CreditVat
        {
            get
            {
                return _creditVat;
            }
            set
            {
                _creditVat = value;
                if(_creditVat!=null)
                {
                    NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                }
                RaisePropertyChanged("CreditVat");
            }
        }


        public string _totalsalesVat;
        public string TotalsalesVat
        {
            get
            {
                return _totalsalesVat;
            }
            set
            {
                _totalsalesVat = value;
                if (!string.IsNullOrEmpty(_totalsalesVat))
                {
                    TotaldueVat = (Convert.ToDouble(TotalsalesVat) - Convert.ToDouble(TotalpurchaseVat)).ToString();
                }
                RaisePropertyChanged("TotalsalesVat");
            }
        }

        public string _stdpurchasesVat;
        public string StdpurchasesVat
        {
            get
            {
                return _stdpurchasesVat;
            }
            set
            {
                _stdpurchasesVat = value;
                RaisePropertyChanged("StdpurchasesVat");
            }
        }

        public string _importspaidVat;
        public string ImportspaidVat
        {
            get
            {
                return _importspaidVat;
            }
            set
            {
                _importspaidVat = value;
                RaisePropertyChanged("ImportspaidVat");
            }
        }

        public string _totalpurchaseVat;
        public string TotalpurchaseVat
        {
            get
            {
                return _totalpurchaseVat;
            }
            set
            {
                _totalpurchaseVat = value;
                if (!string.IsNullOrEmpty(_totalpurchaseVat))
                {
                    TotaldueVat = (Convert.ToDouble(TotalsalesVat) - Convert.ToDouble(TotalpurchaseVat)).ToString();
                 
                }
                RaisePropertyChanged("TotalpurchaseVat");
            }
        }


        private string _amountPayable = "2470";
        public string AmountPayable
        {
            get
            {
                return _amountPayable;
            }
            set
            {
                _amountPayable = value;
                RaisePropertyChanged("AmountPayable");
            }
        }
        public string _importsaccVat;
        public string ImportsaccVat
        {
            get
            {
                return _importsaccVat;
            }
            set
            {
                _importsaccVat = value;
                RaisePropertyChanged("ImportsaccVat");
            }
        }
        #endregion

        private bool _isSadadNumberVisible = false;
        public bool IsSadadNumberVisible
        {
            get
            {
                return _isSadadNumberVisible;
            }
            set
            {
                _isSadadNumberVisible = value;
                RaisePropertyChanged("IsSadadNumberVisible");
            }
        }

       



        #endregion

        #region Constructor

        public VATReturnsPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }


            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;

            IsMainButtonEnabled = false;

            OnStepButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                if (!string.IsNullOrEmpty(ButtonName))
                {
                    if (ButtonName == AppResources.ZVatStepTwo)
                    {
                        TaxpayerDetailsClicked();
                        PageSelectedItem = VatTabbledPageList[1];
                        //  VATTabbedPageReturnCollectionView.SelectedItems.Add((this.VATTabbedPageReturnCollectionView.ItemsSource as List<VATDeclarationTabbedPageName>)[0]);

                    }
                    else if (ButtonName == AppResources.ZVatStepThree)
                    {
                        VATReturnFormClicked();
                        PageSelectedItem = VatTabbledPageList[2];
                    }
                    else if (ButtonName == AppResources.ZVatStepFour)
                    {
                        SummaryClicked();
                        PageSelectedItem = VatTabbledPageList[3];
                    }
                    else if (ButtonName == AppResources.Submit)
                    {
                        SubmitClicked();
                    }
                    else if (ButtonName == AppResources.ZNote)
                    {
                        SetNoteData();
                    }
                    else if(ButtonName== "Go to ICR List")
                    {
                        _navigationService.GoBack();
                        _navigationService.NavigateTo(App.ICRListPageView);
                    }
                }
            });

            onStandardRatedSalesVatAmountTapped = new Xamarin.Forms.Command(() =>
            {
                // InstrunctionClicked();
                ResponseVATDeclarationD.StdsalesVat = StandardRatedSalesVatAmount(ResponseVATDeclarationD.StdsalesAmt, ResponseVATDeclarationD.StdsalesAdj);

            });

            onInstructionsClicked = new Xamarin.Forms.Command(async () =>
            {
                InstrunctionClicked();

            });

            onTaxPayerDetailsClicked = new Xamarin.Forms.Command(async () =>
            {
                TaxpayerDetailsClicked();

            });

            onVATReturnFormClicked = new Xamarin.Forms.Command(async () =>
            {
                VATReturnFormClicked();

            });

            OnAcknowlwdgementClicked = new Xamarin.Forms.Command(async () =>
            {
                string url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + VATDeclarationData + "')/$value?saml2=disabled";
                _navigationService.NavigateTo(App.AAcknowledgementView, url);
            });

            OnDownloadAcknowlwdgementClicked = new Xamarin.Forms.Command(async () =>
            {
                string url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_SRV/cover_formSet(Fbnum='" + VATDeclarationData + "',Utype='')/$value?saml2=disabled";
                _navigationService.NavigateTo(App.AAcknowledgementView, url);
            });

            OnVATRefreshButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    var response = await WebServiceManager.GAZTGetVATDeclarationSADADNumber(VATDeclarationData.d.Fbnum);
                    SadadNumber = response.d.results[0].Vtref;
                    AmountPayable = response.d.results[0].Betrh;
                    if (!string.IsNullOrEmpty(SadadNumber))
                    {
                        IsSadadNumberVisible = true;
                    }
                }
                catch(Exception e)
                {

                }
                // Call Sadad number API
            });

            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    var fileData = await CrossFilePicker.Current.PickFile();
                    attachment = fileData.DataArray;
                    AttachmentName = fileData.FileName;

                    AttachmentRootOject _attachment = await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachment, AttachmentName, VATDeclarationData.d.ReturnIdz);
                    if (_attachment != null && _attachment.d != null)
                    {
                        VATDeclarationData.d.ATTACHSet.results.Add(_attachment.d);
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationData.d.ATTACHSet.results as List<Attachment>);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            VatAttachmentsList = myCollection;
                        });
                        VatAttachmentsList = myCollection;
                    }

                }
                catch (Exception ex)
                {


                }

            });

            onSummaryClicked = new Xamarin.Forms.Command(async () =>
            {
                SummaryClicked();

            });

            onCreditCarriedForwardClicked = new Xamarin.Forms.Command(async () =>
            {
                CreditCarriedClicked();
            });

            onOptionClicked = new Xamarin.Forms.Command(async () =>
            {
                if (IsVisibleOptionMenu == true)
                {
                    IsVisibleOptionMenu = false;
                }
                else
                {
                    IsVisibleOptionMenu = true;
                }

            });

            //OnCopySadadNumberButtonClicked = new Xamarin.Forms.Command(async () =>
            //{
            //    await _dialogService.ShowMessage("It has copied sadad payment number", AppResources.Information);
            //});

            OnSaveAsDraftClicked = new Command(() =>
            {
                CreateDataForPost();
                
                string operation = "05";// Passed 05 to save the data as a draft
                VATDeclarationData.d.Operationz = operation;
                StepNumber = "01";
                if (IsDeclarationCheckedForInstruction==true)
                {
                    StepNumber = "02";
                }
                if (IsCheckedTaxPayerDetailsInfo == true)
                {
                    StepNumber = "03";
                }
                if(IsDeclarationCheckedForSummary==true)
                {
                    StepNumber = "04";
                }

                VATDeclarationData.d.StepNumber = StepNumber;
                //VATDeclarationData.d.StepNumberz = StepNumber;
                VATDeclarationData.d.UserTypz = "TP";   
                // VATDeclarationData.d.ADRSet.results[0].RegionDesc = "Mumbai";
                var response =  WebServiceManager.SaveVATDeclarationData(VATDeclarationData);
                if(response != null && response.d != null)
                {
                    VATDeclarationData = response;
                    ResponseVATDeclarationD = VATDeclarationData.d;
                    SetData();

                    _dialogService.ShowMessage(AppResources.DraftSaved, AppResources.Information);

                  //  _navigationService.GoBack();
                }

            });
        }

        #endregion

        #region Method

        public void InstrunctionClicked()
        {
            ClearPage();
            IsVisibleInstrunction = true;
            ButtonName = AppResources.ZVatStepTwo;
        }
        public void TaxpayerDetailsClicked()
        {
            ClearPage();
            IsVisibleTaxPayerDetails = true;
            IsMainButtonEnabled = false;
            ButtonName = AppResources.ZVatStepThree;
        }
        public void VATReturnFormClicked()
        {
            ClearPage();
            IsVisibleVatReturnForm = true;
            ButtonName = AppResources.ZVatStepFour;
        }
        public void SummaryClicked()
        {
            ClearPage();
            IsVisibleSummary = true;
            ButtonName = AppResources.Submit;
        }
        public void CreditCarriedClicked()
        {
            ClearPage();
            IsTabbedMenuAvailable = false;
            IsVisibleCreditCarriedLabel = true;
            IsVisibleCreditCarriedForward = true;
            ButtonName = AppResources.Submit;
        }
        public void SubmitClicked()
        {
            

            if (FirstSubmissionCount != 1)
            {
                CreateDataForPost();
            }
            
            //IsVisibleAcknowledgment = true;
            ButtonName = AppResources.Submit;
            if(!IsFirstSubmission)
            {
                FirstSubmissionCount = 0;
                ClearPage();
                IsVisibleAcknowledgment = true;
                //if(ResponseVatDeclaration.d==null)
                //{
                //    ResponseVatDeclaration = VATDeclarationData;
                //}
                string operation = "01";// Passed operation "01" to submit the VAT Declaration Data
                                        //   VATDeclarationData.d.StepNumberz = "04";
              //  VATDeclarationData.d.StepNumberz = "03";
                VATDeclarationData.d.StepNumber = "00";
                VATDeclarationData.d.UserTypz = "TP";
                VATDeclarationData.d.Operationz = operation;

                VATDeclaration response = WebServiceManager.SaveVATDeclarationData(VATDeclarationData);
                if (response != null && response.d != null)
                {
                    TPName = App.TP.Name;
                    ReturnReferenceNumber = response.d.Fbnum;
                    TaxablePeriod = response.d.Perslt;
                    ReceiptDate = response.d.ReceiptDt;
                }
                ResponseVatDeclaration = null;
                IsTabbedMenuAvailable = false;
                ButtonName = "Go to ICR List";
               
            }
            else
            {
                IsFirstSubmission = false;
              
                if (String.IsNullOrEmpty(VATDeclarationData.d.Fbnum))
                {
                    CreateDataForPost();
                    FirstSubmissionCount = 1;
                    string operation = "05";// Passed operation "01" to submit the VAT Declaration Data
                                            //   VATDeclarationData.d.StepNumberz = "04";
                                            //  VATDeclarationData.d.StepNumberz = "03";
                    VATDeclarationData.d.StepNumber = "04";
                    VATDeclarationData.d.UserTypz = "TP";
                    VATDeclarationData.d.Operationz = operation;
                    VATDeclaration response = new VATDeclaration();
                    response = WebServiceManager.SaveVATDeclarationData(VATDeclarationData);
                    if (response != null && response.d != null && !string.IsNullOrEmpty(response.d.Fbnum))
                    {
                        VATDeclarationData = response;
                    }
                }


                _dialogService.ShowMessage(AppResources.Pleasereviewthecalculationandsubmitagain, AppResources.Information);
            }
           


        }

        public void VATReturnAddNote()
        {
            WebServiceManager.GAZTSetVATReturnAddNote(String.Empty);
            _navigationService.NavigateTo("ICRListPageView");

        }

        public void VATReturnGetNotes()
        {
            ClearPage();
            IsVisibleNotes = true;
            ButtonName = AppResources.ZNote;

            WebServiceManager.GAZTSetVATReturnGetNotes(String.Empty);
            _navigationService.NavigateTo("ICRListPageView");

        }

        public  void VATViewAttachments()
        {
            ClearPage();
            IsVisibleAttachments = true;
            ButtonName = AppResources.Submit;

        }
        public async Task SetVATReturnVoidAsync()
        {
            string operation = "04";// Passed 04 to set void
            VATDeclarationData.d.Operationz = operation;
            StepNumber = "01";
            
            if (IsDeclarationCheckedForInstruction == true)
            {
                StepNumber = "02";
            }
            if (IsCheckedTaxPayerDetailsInfo == true)
            {
                StepNumber = "03";
            }
            if (IsDeclarationCheckedForSummary == true)
            {
                StepNumber = "04";
            }

            VATDeclarationData.d.StepNumber = StepNumber;
            VATDeclarationData.d.UserTypz = "TP";
            
            var response = WebServiceManager.GAZTSetVATReturnVoid(VATDeclarationData);
            if (response != null && response.d != null)
            {

                await _dialogService.ShowMessage("Return marked as VOID", "VOID Title");

                VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(VATDeclarationData.d.Fbguid);

                if (_vATDeclaration != null && _vATDeclaration.d != null)
                {
                    VATDeclarationData = _vATDeclaration;
                    ResponseVATDeclarationD = VATDeclarationData.d;

                    SetData();
                }
            }
        }

    public void VATReturnReset()
        {
            WebServiceManager.GAZTSetVATReturnReset(String.Empty);
            _navigationService.NavigateTo("ICRListPageView");

        }

        public void VATReturnDeleteAttachment()
        {
            WebServiceManager.GAZTSetVATReturnDeleteAttachment(String.Empty);
            _navigationService.NavigateTo("ICRListPageView");
        }

      

        public void ClearPage()
        {
            //for Header
            IsTabbedMenuAvailable = true;
            IsVisibleCreditCarriedLabel = false;
            ///
            IsVisibleAcknowledgment = false;
            IsVisibleAttachments = false;
            IsVisibleInstrunction = false;
            IsVisibleNotes = false;
            IsVisibleSummary = false;
            IsVisibleTaxPayerDetails = false;
            IsVisibleVatReturnForm = false;
            IsVisibleCreditCarriedForward = false;
        }
        public void RateSetAsPerDate()
        {
            if (VATDeclarationData.d != null)
            {
                if (VATDeclarationData.d.Abrzu != null && VATDeclarationData.d.Abrzo != null)
                {



                    DateTime startDate = new DateTime(2017, 1, 18);

                    DateTime endDate = new DateTime(2018, 1, 18);

                    //DateTime startDate = DateTime.Parse(VATDeclarationData.d.Abrzu);
                    //DateTime endDate = DateTime.Parse(VATDeclarationData.d.Abrzo);

                    List<VATCalculationDataVATRSet> vATCalculationsforBegin = new List<VATCalculationDataVATRSet>();
                    List<VATCalculationDataVATRSet> vATCalculationsforEnd = new List<VATCalculationDataVATRSet>();
                    VATCalculationDataVATRSet vATCalculationDataDummy;
                    foreach (var item in CalculationRateSet)
                    {
                        if (startDate >= item.Begda)
                        {
                            vATCalculationDataDummy = new VATCalculationDataVATRSet();
                            vATCalculationDataDummy = item;
                            vATCalculationsforBegin.Add(vATCalculationDataDummy);
                        }
                    }

                    foreach (var item1 in vATCalculationsforBegin)
                    {
                        if (endDate <= item1.Endda)
                        {
                            vATCalculationDataDummy = new VATCalculationDataVATRSet();
                            vATCalculationDataDummy = item1;
                            vATCalculationsforEnd.Add(vATCalculationDataDummy);
                        }
                    }

                    VATCalculationDataVATRSet Rate002 = vATCalculationsforEnd.Where(x => x.Type == "002").FirstOrDefault();
                    if (Rate002 != null)
                    {
                        VATRate002 = Rate002.Penalty;
                    }

                    VATCalculationDataVATRSet Rate001 = vATCalculationsforEnd.Where(x => x.Type == "001").FirstOrDefault();
                    if (Rate001 != null)
                    {
                        VATRate001 = Rate001.Penalty;
                    }


                    //VATCalculationDataVATRSet Rate00T1 = CalculationRateSet.Where(x => x.Begda <= startDate && x.Endda >= endDate).FirstOrDefault();



                    //VATCalculationDataVATRSet Rate002 = CalculationRateSet.Where(x => (x.Begda.Date >= startDate.Date) && (x.Endda.Date <= endDate.Date) && (x.Type== "002")).FirstOrDefault();
                    //if (Rate002 != null)
                    //{
                    //    VATRate002 = Rate002.Penalty;
                    //}

                    //VATCalculationDataVATRSet Rate001 = CalculationRateSet.Where(x => (x.Begda.Date >= startDate.Date) && (x.Endda.Date <= endDate.Date) && (x.Type == "001")).FirstOrDefault();
                    //if (Rate001 != null)
                    //{
                    //    VATRate001 = Rate001.Penalty;
                    //}
                }
            }
        }
        public async Task pageLoad()
        {
            IsFirstSubmission = true;
            IsSadadNumberVisible = false;

            //await Task.Run(() =>
            //{
            //    IsLoading = true;
            //});

            //await Task.Run(async () =>
            //{

            VATCalculationData vATCalculationData;
            VTTHSetResult vTTHSetResult;
            string periodKey = VATDeclarationData.d.Periodkeyz;
            string TxnTp = VATDeclarationData.d.TxnTpz;
            string status = VATDeclarationData.d.Statusz;
            string FormBundleNumber = VATDeclarationData.d.Fbnum;
            string Gpart = VATDeclarationData.d.Gpart;
            vATCalculationData = await WebServiceManager.GAZTGetVATDeclaratinCalculationData(periodKey, TxnTp, status, FormBundleNumber, Gpart);
            if (vATCalculationData.d != null)
            {

              //      if(vATCalculationData.d.)
                if (vATCalculationData.d.VATRSet.results.Count != 0)
                {
                    CalculationRateSet = new List<VATCalculationDataVATRSet>();
                    CalculationRateSet = vATCalculationData.d.VATRSet.results;



                    RateSetAsPerDate();



                }

                if (vATCalculationData.d.VTTHSet.results.Count != 0)
                {
                    CorrectionPeriodAmount = vATCalculationData.d.VTTHSet.results.Where(x => x.Type == "001").Select(x => x.MaxVal).FirstOrDefault();
                }
                

            }

            List<VATDeclarationTabbedPageName> vatTabbedList = new List<VATDeclarationTabbedPageName>();
            VatTabbledPageList = new List<VATDeclarationTabbedPageName>();

            VATDeclarationTabbedPageName s = new VATDeclarationTabbedPageName();
            s.pageName = "Instrunction";
            vatTabbedList.Add(s);
            VATDeclarationTabbedPageName s1 = new VATDeclarationTabbedPageName();
            s1.pageName = "TaxPayer Details";
            vatTabbedList.Add(s1);
            VATDeclarationTabbedPageName s2 = new VATDeclarationTabbedPageName();
            s2.pageName = "VAT Return Form";
            vatTabbedList.Add(s2);
            VATDeclarationTabbedPageName s3 = new VATDeclarationTabbedPageName();
            s3.pageName = "Summary";
            vatTabbedList.Add(s3);
           
            VatTabbledPageList = vatTabbedList;
            PageSelectedItem = VatTabbledPageList[0];
          

            ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationData.d.ATTACHSet.results as List<Attachment>);

            VatAttachmentsList = myCollection;

            if (VATDeclarationData != null)
            {
                SetPageForDraft();
                if (VATDeclarationData.d != null)
                {
                    ResponseVATDeclarationD = VATDeclarationData.d;
                    SetData();
                }
                if (VATDeclarationData.d.NOTESSet.results != null && VATDeclarationData.d.NOTESSet.results.Count() != 0)
                {
                    ResponseNote = VATDeclarationData.d.NOTESSet.results;
                }
                if (VATDeclarationData.d.VATR_MSGSet.results != null && VATDeclarationData.d.VATR_MSGSet.results.Count() != 0)
                {
                    Responseobject = VATDeclarationData.d.VATR_MSGSet.results;
                }
                if (VATDeclarationData.d.IBANSet.results != null && VATDeclarationData.d.IBANSet.results.Count() != 0)
                {
                    ResponseIBANSET = VATDeclarationData.d.IBANSet.results;
                }
                if (VATDeclarationData.d.CFSet.results != null && VATDeclarationData.d.CFSet.results.Count() != 0)
                {
                    ResponseCFSET = VATDeclarationData.d.CFSet.results;
                }
                if (VATDeclarationData.d.ATTACHSet.results != null && VATDeclarationData.d.ATTACHSet.results.Count() != 0)
                {
                    ResponseAttachSet = VATDeclarationData.d.ATTACHSet.results;
                }
                if (VATDeclarationData.d.ADRSet.results != null && VATDeclarationData.d.ADRSet.results.Count() != 0)
                {
                    ResponseAddressSET = VATDeclarationData.d.ADRSet.results;
                }
            }

            //});

            //await Task.Run(() =>
            //{
            //    IsLoading = false;
            //});

            //int j = 5;
            //List<CreditCarried> creditsCrarriedDummy = new List<CreditCarried>();
            //CreditCarriedsList = new List<CreditCarried>();
            //for (j = 0; j < 6; j++)
            //{
            //    CreditCarried m = new CreditCarried();
            //    m.SerialNumber = "0001";
            //    m.ReturnReferenceNumber = "000000000001";
            //    m.DocumentNumber = "0102000010202";
            //    m.Amount = "100000000,00";

            //    creditsCrarriedDummy.Add(m);
            //}
            //CreditCarriedsList = creditsCrarriedDummy;


            //int k = 5;
            //List<VATAttachments> vatAttachment = new List<VATAttachments>();
            //VatAttachmentsList = new List<VATAttachments>();
            //for (k = 0; k < 6; k++)
            //{
            //    VATAttachments m = new VATAttachments();
            //    m.Id = "0001";
            //    m.DocumentName = "Test-Document.pdf";
            //    m.Size = "20.00";

            //    vatAttachment.Add(m);
            //}
            //VatAttachmentsList = vatAttachment;
        }

        #region CalculationPart

        public void CreateDataForPost()
        {
            //List<Note> noteList = new List<Note>();
            //Note Note = new Note();
            //Note.Strline = NoteText;
            //noteList.Add(Note);
            //VATDeclarationData.d.NOTESSet.results = noteList;
            VATDeclarationD vATDeclarationD = SetDataForPost(ResponseVATDeclarationD);
           
            VATDeclarationData.d.TotalsalesAmt = vATDeclarationD.TotalsalesAmt;
            VATDeclarationData.d.TotalsalesAdj = vATDeclarationD.TotalsalesAdj;
            VATDeclarationData.d.TotalpurchaseAmt = vATDeclarationD.TotalpurchaseAmt;
            VATDeclarationData.d.TotalpurchaseAdj = vATDeclarationD.TotalpurchaseAdj;
            VATDeclarationData.d.StdsalesAdj = vATDeclarationD.StdsalesAdj;
            VATDeclarationData.d.TotalsalesAdj = vATDeclarationD.TotalsalesAdj;
            VATDeclarationData.d.StdpurchasesVat = vATDeclarationD.StdpurchasesVat;
            VATDeclarationData.d.ImportspaidVat = vATDeclarationD.ImportspaidVat;
            VATDeclarationData.d.ImportsaccVat = vATDeclarationD.ImportsaccVat;
            VATDeclarationData.d.TotalpurchaseVat = vATDeclarationD.TotalpurchaseVat;
            VATDeclarationData.d.TotaldueVat = vATDeclarationD.TotaldueVat;
            VATDeclarationData.d.Preperiodcorr = vATDeclarationD.Preperiodcorr;
            VATDeclarationData.d.CreditVat = vATDeclarationD.CreditVat;
            VATDeclarationData.d.NetdueVat = vATDeclarationD.NetdueVat;

        }

        public VATDeclarationD SetDataForPost(VATDeclarationD vATDeclarationD)
        {
            vATDeclarationD.TotalsalesAmt=TotalsalesAmt;
             vATDeclarationD.TotalsalesAdj= TotalsalesAdj;
             vATDeclarationD.TotalpurchaseAmt= TotalpurchaseAmt;
             vATDeclarationD.TotalpurchaseAdj= TotalpurchaseAdj;
             vATDeclarationD.StdsalesAdj= StdsalesVat;
             vATDeclarationD.TotalsalesAdj= TotalsalesVat;
             vATDeclarationD.StdpurchasesVat = StdpurchasesVat;
             vATDeclarationD.ImportspaidVat = ImportspaidVat;
             vATDeclarationD.ImportsaccVat = ImportsaccVat;
             vATDeclarationD.TotalpurchaseVat= TotalpurchaseVat;
            vATDeclarationD.TotaldueVat= TotaldueVat;
            vATDeclarationD.Preperiodcorr = Preperiodcorr;
            vATDeclarationD.CreditVat = CreditVat;
            vATDeclarationD.NetdueVat = NetdueVat;
            return vATDeclarationD;
        }

        public void SetPageForDraft()
        {
            if (!string.IsNullOrEmpty(VATDeclarationData.d.StepNumber))
            {
                if (VATDeclarationData.d.StepNumber == "00")
                {
                    ClearPage();
                    IsDeclarationCheckedForInstruction = false;
                    IsVisibleInstrunction = true;
                    PageSelectedItem = VatTabbledPageList[0];
                }
                if (VATDeclarationData.d.StepNumber == "01")
                {
                    ClearPage();
                    IsDeclarationCheckedForInstruction = false;
                    IsVisibleInstrunction = true;
                    PageSelectedItem = VatTabbledPageList[0];
                }
                else if (VATDeclarationData.d.StepNumber == "02")
                {
                    ClearPage();
                    IsDeclarationCheckedForInstruction = true;
                    IsVisibleTaxPayerDetails = true;
                    PageSelectedItem = VatTabbledPageList[1];
                }
                else if (VATDeclarationData.d.StepNumber == "03")
                {
                    ClearPage();
                    IsDeclarationCheckedForInstruction = true;
                    IsCheckedTaxPayerDetailsInfo = true;
                    IsVisibleVatReturnForm = true;
                    PageSelectedItem = VatTabbledPageList[2];
                }
                else if (VATDeclarationData.d.StepNumber == "04")
                {
                    ClearPage();
                    IsDeclarationCheckedForInstruction = true;
                    IsCheckedTaxPayerDetailsInfo = true;
                    IsDeclarationCheckedForSummary = true;
                    IsVisibleVatReturnForm = true;
                    PageSelectedItem = VatTabbledPageList[3];
                }

            }
        }

        public void SetData()
        {
            TotalsalesAmt = ResponseVATDeclarationD.TotalsalesAmt;
            TotalsalesAdj = ResponseVATDeclarationD.TotalsalesAdj;
            TotalpurchaseAmt = ResponseVATDeclarationD.TotalpurchaseAmt;
            TotalpurchaseAdj = ResponseVATDeclarationD.TotalpurchaseAdj;
            StdsalesVat = ResponseVATDeclarationD.StdsalesAdj;
            TotalsalesVat = ResponseVATDeclarationD.TotalsalesAdj;
            StdpurchasesVat = ResponseVATDeclarationD.StdpurchasesVat;
            ImportspaidVat = ResponseVATDeclarationD.ImportspaidVat;
            ImportsaccVat = ResponseVATDeclarationD.ImportsaccVat;
            TotalpurchaseVat = ResponseVATDeclarationD.TotalpurchaseVat;
            TotaldueVat = ResponseVATDeclarationD.TotaldueVat;
            Preperiodcorr = ResponseVATDeclarationD.Preperiodcorr;
            CreditVat = ResponseVATDeclarationD.CreditVat;
            NetdueVat = ResponseVATDeclarationD.NetdueVat;
           
        }

        private void SetNoteData()
        {
            VATDeclarationData.d.NOTESSet.results[0].Strline = NoteText;
        }

        public string StandardRatedSalesVatAmount(string Amount, string Adjustment)
        {
            string VATAmount = string.Empty;
            if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment))
            {
                Double dAmount = Convert.ToDouble(Amount);
                Double dAdjustment = Convert.ToDouble(Adjustment);
                Double dVATRate = Convert.ToDouble(VATRate002);

                VATAmount = (((dAmount - dAdjustment) * dVATRate) / 100).ToString();
            }
            return VATAmount;
        }

        public string TotalAmount(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5)
        {
            String TotalAmount = string.Empty;
            if (!String.IsNullOrEmpty(Amount1) && !String.IsNullOrEmpty(Amount2) && !String.IsNullOrEmpty(Amount3) && !String.IsNullOrEmpty(Amount4) && !String.IsNullOrEmpty(Amount5))
            {
                TotalAmount = (Convert.ToDouble(Amount1) + Convert.ToDouble(Amount2) + Convert.ToDouble(Amount3) + Convert.ToDouble(Amount4) + Convert.ToDouble(Amount5)).ToString();
            }
            return TotalAmount;
        }

        public string TotalAdjustment(string Adjustment1, string Adjustment2, string Adjustment3, string Adjustment4, string Adjustment5)
        {
            String TotalAmount = string.Empty;
            if (!String.IsNullOrEmpty(Adjustment1) && !String.IsNullOrEmpty(Adjustment2) && !String.IsNullOrEmpty(Adjustment3) && !String.IsNullOrEmpty(Adjustment4) && !String.IsNullOrEmpty(Adjustment5))
            {
                TotalAmount = (Convert.ToDouble(Adjustment1) + Convert.ToDouble(Adjustment2) + Convert.ToDouble(Adjustment3) + Convert.ToDouble(Adjustment4) + Convert.ToDouble(Adjustment5)).ToString();
            }
            return TotalAmount;
        }

        public string TotalVatAmount(string Amount1, string Amount2, string Amount3)
        {
            String TotalAmount = string.Empty;
            if (!String.IsNullOrEmpty(Amount1) && !String.IsNullOrEmpty(Amount2) && !String.IsNullOrEmpty(Amount3))
            {
                TotalAmount = (Convert.ToDouble(Amount1) + Convert.ToDouble(Amount2) + Convert.ToDouble(Amount3)).ToString();
            }
            return TotalAmount;
        }

        public string StandardRatedDomesticPurchaseVatAmount(string Amount, string Adjustment)
        {
            string VATAmount = string.Empty;
            if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment))
            {
                Double dAmount = Convert.ToDouble(Amount);
                Double dAdjustment = Convert.ToDouble(Adjustment);
                Double dVATRate = Convert.ToDouble(VATRate002);

                VATAmount = (((dAmount - dAdjustment) * dVATRate) / 100).ToString();
            }
            return VATAmount;
        }
        
        //This method is used to calculate  Imports subject to VAT accounted for through the reverse charge mechanism Vat Amount too.
        public string ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(string Amount, string Adjustment)
        {
            string VATAmount = string.Empty;
            if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment))
            {
                Double dAmount = Convert.ToDouble(Amount);
                Double dAdjustment = Convert.ToDouble(Adjustment);
                Double dVATRate001 = Convert.ToDouble(VATRate001);
                Double dVATRate002 = Convert.ToDouble(VATRate002);

                VATAmount = (((dAmount * dVATRate001) / 100) - ((dAdjustment * dVATRate002) / 100)).ToString();
            }
            return VATAmount;
        }

        public string ImportSubjectToVatPaidAtCustomsVatAmountForNonDesignated(string Amount, string Adjustment)
        {
            string VATAmount = string.Empty;
            if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment))
            {
                Double dAmount = Convert.ToDouble(Amount);
                Double dAdjustment = Convert.ToDouble(Adjustment);
                Double dVATRate = Convert.ToDouble(VATRate002);

                VATAmount = (((dAmount - dAdjustment) * dVATRate) / 100).ToString();
            }
            return VATAmount;
        }

        public string NetVatDue(string CurrentPeriod, string PreviousPeriod, string ForwardFromPreviousPeriod)
        {
            string NetVatDue = string.Empty;
            if (!string.IsNullOrEmpty(CurrentPeriod) && !string.IsNullOrEmpty(PreviousPeriod) && !string.IsNullOrEmpty(ForwardFromPreviousPeriod))
            {
                Double dCurrentPeriod = Convert.ToDouble(CurrentPeriod);
                Double dPreviousPeriod = Convert.ToDouble(PreviousPeriod);
                Double dForwardFromPreviousPeriod = Convert.ToDouble(ForwardFromPreviousPeriod);

                NetVatDue = (dCurrentPeriod + dPreviousPeriod + dForwardFromPreviousPeriod).ToString();
            }
            return NetVatDue;
        }

        #endregion

        #endregion
        
    }

}
