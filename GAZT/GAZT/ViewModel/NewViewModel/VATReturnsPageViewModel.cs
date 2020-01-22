using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.ViewModel.NewViewModel
{
    public class VATReturnsPageViewModel:ViewModelBase
    {
        #region Variable

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
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

            //OnCopySadadNumberButtonClicked = new Xamarin.Forms.Command(async () =>
            //{
            //    await _dialogService.ShowMessage("It has copied sadad payment number", AppResources.Information);
            //});


        }

        #endregion

        #region Method

        public void pageLoad()
        {
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
