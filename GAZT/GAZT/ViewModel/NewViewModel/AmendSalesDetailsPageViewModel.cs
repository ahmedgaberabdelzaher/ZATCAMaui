using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GAZT.ViewModel.NewViewModel
{
    public class AmendSalesDetailsPageViewModel: ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        //  public ICommand OnBillsButtonClicked { get; set; }
        public ICommand OnChangeEmailSubmitButtonClicked { get; set; }
        #endregion

        #region Property
        private List<SalesDetailsAttachments> _zakatReturnAttachmentsList;
        public List<SalesDetailsAttachments> ZakatReturnAttachmentsList
        {
            get
            {
                return _zakatReturnAttachmentsList;
            }
            set
            {
                _zakatReturnAttachmentsList = value;
                RaisePropertyChanged("ZakatReturnAttachmentsList");
            }
        }
        #endregion

        #region Constructor
        public AmendSalesDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;



            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }


            OnChangeEmailSubmitButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                _navigationService.NavigateTo(App.ZakatBillDetailsPageView);
            });


        }
        #endregion

        #region Method
        public void OnLoad()
        {
            int k = 5;
            List<SalesDetailsAttachments> ZakatAttachment = new List<SalesDetailsAttachments>();
            ZakatReturnAttachmentsList = new List<SalesDetailsAttachments>();
            for (k = 0; k < 6; k++)
            {
                SalesDetailsAttachments m = new SalesDetailsAttachments();
                m.Id = "0001";
                m.DocumentName = "Test-Document.pdf";
                m.Size = "20.00";

                ZakatAttachment.Add(m);
            }
            ZakatReturnAttachmentsList = ZakatAttachment;
        }
        #endregion
    }
}
