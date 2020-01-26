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
namespace GAZT.ViewModel.NewViewModel
{
    public class VATReturnsPageViewModel:ViewModelBase
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
        public ICommand OnAttachmentClick{ get; set; }

        byte[] attachment;
        public ICommand onCreditCarriedForwardClicked { get; set; }
        string StepNumber = "04";


        #endregion

        #region Property


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
                if(_isDeclarationCheckedForInstruction==true)
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


        private bool _isVisibleInstrunction=false;
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



        private string _pageFontSize="10";
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

        private string _attachmentName ="Attachments";
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

        

        private string _buttonName=AppResources.ZVatStepTwo;
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

        private string _noteText ="";
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
                }
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
            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    var fileData = await CrossFilePicker.Current.PickFile();
                    attachment = fileData.DataArray;
                    AttachmentName = fileData.FileName;
                    
                    AttachmentRootOject _attachment = await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachment, AttachmentName, VATDeclarationData.d.ReturnIdz);
                    if(_attachment != null && _attachment.d != null)
                    {
                        VATDeclarationData.d.ATTACHSet.results.Add(_attachment.d);
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationData.d.ATTACHSet.results as List<Attachment>);
                        Device.BeginInvokeOnMainThread(async () => {
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
                if(IsVisibleOptionMenu==true)
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

            OnSaveAsDraftClicked = new Command(async () =>
            {
                string operation = "05";// Passed 05 to save the data as a draft
                VATDeclarationData.d.Operationz = operation;
                VATDeclarationData.d.StepNumber = StepNumber;
                await WebServiceManager.SaveVATDeclarationData(VATDeclarationData);
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
        public async Task SubmitClicked()
        {
            ClearPage();
            IsVisibleAcknowledgment = true;
            ButtonName = AppResources.Submit;
            string operation = "01";
            VATDeclarationData.d.StepNumber = StepNumber;
            VATDeclarationData.d.Operationz = operation;
            await WebServiceManager.SaveVATDeclarationData(VATDeclarationData);


        }
        public void ClickNotes()
        {
            ClearPage();
            IsVisibleNotes = true;
            ButtonName = AppResources.ZNote;
        }
        public async void ClickAttachment()
        {
            ClearPage();
            IsVisibleAttachments = true;
            ButtonName = AppResources.Submit;

        }
        public void ClickVoid()
        {
            
        }

        private void AddNote()
        {

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

        public async Task  pageLoad()
        {

            string periodKey = VATDeclarationData.d.Periodkeyz;
            string TxnTp = VATDeclarationData.d.TxnTpz;
            string status = VATDeclarationData.d.Statusz;
            string FormBundleNumber = VATDeclarationData.d.Fbnum;
            string Gpart = VATDeclarationData.d.Gpart;
            await WebServiceManager.GAZTGetVATDeclaratinCalculationData(periodKey, TxnTp, status, FormBundleNumber, Gpart);


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
            InstrunctionClicked();

            ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationData.d.ATTACHSet.results as List<Attachment>);

            VatAttachmentsList = myCollection;

            if (VATDeclarationData != null)
            {
               if(VATDeclarationData.d!=null)
                {
                    ResponseVATDeclarationD = VATDeclarationData.d;
                }
                if (VATDeclarationData.d.NOTESSet.results != null && VATDeclarationData.d.NOTESSet.results.Count()!=0)
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




            int j = 5;
            List<CreditCarried> creditsCrarriedDummy = new List<CreditCarried>();
            CreditCarriedsList = new List<CreditCarried>();
            for (j = 0; j < 6; j++)
            {
                CreditCarried m = new CreditCarried();
                m.SerialNumber = "0001";
                m.ReturnReferenceNumber = "000000000001";
                m.DocumentNumber = "0102000010202";
                m.Amount = "100000000,00";

                creditsCrarriedDummy.Add(m);
            }
            CreditCarriedsList = creditsCrarriedDummy;


            //int k = 5;
            //List<Attachment> vatAttachment = new List<Attachment>();
            //VatAttachmentsList = new List<Attachment>();
            //for (k = 0; k < 6; k++)
            //{
            //    Attachment m = new Attachment();
            //    //m.Id = "0001";
            //    //m.DocumentName = "Test-Document.pdf";
            //    //m.Size = "20.00";

            //    vatAttachment.Add(m);
            //}
            //VatAttachmentsList = vatAttachment;
        }

        private void SetNoteData()
        {
            VATDeclarationData.d.NOTESSet.results[0].Strline = NoteText;
        }
        #endregion
    }
}
