using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace GAZT.ViewModel.NewViewModel
{
    public class AmendSalesDetailsPageViewModel: ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public static SalesDetails SelectedSalesDetails = new SalesDetails();
        //  public ICommand OnBillsButtonClicked { get; set; }
        public ICommand OnChangeEmailSubmitButtonClicked { get; set; }
        public ICommand OnZakatReturnDataUpdateClicked { get; set; }
        public ICommand OnAttachmentClick { get; set; }
        public RootObject rootObject { get; set; }
        
        byte[] attachment;

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

        private string _newValue = "";
        public string NewValue
        {
            get
            {
                return _newValue;
            }
            set
            {
                _newValue = value;
                if(NewValue != null)
                {
                    SelectedSalesDetails.NewValue = NewValue;
                }
                RaisePropertyChanged("NewValue");
            }
        }

        private string _oldValue = "";
        public string OldValue
        {
            get
            {
                return _oldValue;
            }
            set
            {
                _oldValue = value;
                if (OldValue != null)
                {
                    SelectedSalesDetails.OldValue = _oldValue;
                }
                RaisePropertyChanged("OldValue");
            }
        }

        
        private string _changeReason = "";
        public string ChangeReason
        {
            get
            {
                return _changeReason;
            }
            set
            {
                _changeReason = value;
                if (ChangeReason != null)
                {
                    SelectedSalesDetails.ChangeReason = ChangeReason;
                    if (SelectedSalesDetails.IsReasonRequird)
                    {
                        SelectedSalesDetails.IsAttachmentRequired = false;
                    }                   
                }
                RaisePropertyChanged("ChangeReason");
            }
        }

        

        private string _attachmentName = "";
        public string AttachmentName
        {
            get
            {
                return _attachmentName;
            }
            set
            {
                _attachmentName = value;
                if (AttachmentName != null)
                {
                    SelectedSalesDetails.AttchamentName = AttachmentName;
                    if(SelectedSalesDetails.IsAttachmentRequired)
                    {
                        SelectedSalesDetails.IsAttachmentRequired = false;
                    }
                }
                RaisePropertyChanged("AttachmentName");
            }
        }

        private string _attachmentNumber = "";
        public string AttachmentNumber
        {
            get
            {
                return _attachmentNumber;
            }
            set
            {
                _attachmentNumber = value;
                if (_attachmentNumber != null)
                {
                    SelectedSalesDetails.AttchamentNumber = AttachmentNumber;
                }
                RaisePropertyChanged("AttachmentNumber");
            }
        }

        private string _salesType = "";
        public string SalesType
        {
            get
            {
                return _salesType;
            }
            set
            {
                _salesType = value;
                if (_attachmentNumber != null)
                RaisePropertyChanged("SalesType");
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


            OnZakatReturnDataUpdateClicked = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    _navigationService.GoBack();
                    // _navigationService.NavigateTo(App.ZakatBillDetailsPageView);
                 
                }
                catch(Exception ex)
                {

                }
            });

            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    var fileData = await CrossFilePicker.Current.PickFile();
                    attachment = fileData.DataArray;
                    AttachmentName = fileData.FileName;

                    AttachmentRootOject _attachment = await WebServiceManager.GAZTSaveEstimatedZAKATAttachment(attachment, AttachmentName, SalesDetailsPageViewModel.RetGuid, "Z12L");
                   // PopToRootPage();
                    if (_attachment != null && _attachment.d != null)
                    {
                        EstimateZakatAttachment _estimateZakatAttachment = new EstimateZakatAttachment();
                        _estimateZakatAttachment.Doguid = _attachment.d.Doguid;
                        _estimateZakatAttachment.Filename = _attachment.d.Filename;
                        _estimateZakatAttachment.RetGuid = _attachment.d.RetGuid;
                        _estimateZakatAttachment.Dotyp = "FZ01";
                        _estimateZakatAttachment.Mimetype = "multipart/form-data";
                        _estimateZakatAttachment.DocUrl = _attachment.d.DocUrl;
                        SelectedSalesDetails.estimateZakatAttachment = _estimateZakatAttachment;
                    }

                }
                catch (Exception ex)
                {


                }

            });
        }
        #endregion

        #region Method

        public void OnLoad()
        {
            SalesType = SelectedSalesDetails.SalesType;
            OldValue = SelectedSalesDetails.InformationFromPartie;
            NewValue =SelectedSalesDetails.NewValue;
            ChangeReason = SelectedSalesDetails.ChangeReason;

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
