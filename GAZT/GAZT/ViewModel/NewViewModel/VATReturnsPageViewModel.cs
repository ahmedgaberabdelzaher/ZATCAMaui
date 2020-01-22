using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

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

        public ICommand onOptionClicked { get; set; }


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

        private List<VATAttachments> _vatAttachmentsList;
        public List<VATAttachments> VatAttachmentsList
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



            OnStepButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                if (!string.IsNullOrEmpty(ButtonName))
                {
                    if (ButtonName == AppResources.ZVatStepTwo)
                    {
                        TaxpayerDetailsClicked();
                    }
                    else if (ButtonName == AppResources.ZVatStepThree)
                    {
                        VATReturnFormClicked();
                    }
                    else if (ButtonName == AppResources.ZVatStepFour)
                    {
                        SummaryClicked();
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

            onSummaryClicked = new Xamarin.Forms.Command(async () =>
            {
                SummaryClicked();
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

        public void ClearPage()
        {
            IsVisibleAcknowledgment = false;
            IsVisibleAttachments = false;
            IsVisibleInstrunction = false;
            IsVisibleNotes = false;
            IsVisibleSummary = false;
            IsVisibleTaxPayerDetails = false;
            IsVisibleVatReturnForm = false;
        }

        public void pageLoad()
        {
           
            

            
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


            int k = 5;
            List<VATAttachments> vatAttachment = new List<VATAttachments>();
            VatAttachmentsList = new List<VATAttachments>();
            for (k = 0; k < 6; k++)
            {
                VATAttachments m = new VATAttachments();
                m.Id = "0001";
                m.DocumentName = "Test-Document.pdf";
                m.Size = "20.00";

                vatAttachment.Add(m);
            }
            VatAttachmentsList = vatAttachment;
        }

        #endregion
    }
}
