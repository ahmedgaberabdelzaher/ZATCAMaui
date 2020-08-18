using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.VATRefunds;
using EGAZT.Views.NewDesign.GenericPickers;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VATRefunds
{
    public class VATRefundsNewRequestViewModel: BaseViewModel
    {
        #region Commands

        public ICommand GoBackBtnTapped { get; set; }
        public ICommand CloseBtnTapped { get; set; }
        public ICommand IbanIdTypeTapped { get; set; }

        #endregion

        public ObservableCollection<VATRefundsModel> _vatRefundsModel { get; set; }
        public ObservableCollection<VATRefundsModel> VATRefundsModel
        {
            get
            {
                return _vatRefundsModel;
            }

            set
            {

                _vatRefundsModel = value;
                RaisePropertyChanged("VATRefundsModel");
            }
        }

        private VatRefundDisplayDataModel _vatRefundsDisplayDataModel = null;
        public VatRefundDisplayDataModel VatRefundsDisplayDataModel
        {
            get
            {
                return _vatRefundsDisplayDataModel;
            }

            set
            {

                _vatRefundsDisplayDataModel = value;
                RaisePropertyChanged("VatRefundsDisplayDataModel");
            }
        }

        private VarRefundIbanDataModel _vatRefundsIbanDataModel = null;
        public VarRefundIbanDataModel VatRefundsIbanDataModel
        {
            get
            {
                return _vatRefundsIbanDataModel;
            }

            set
            {

                _vatRefundsIbanDataModel = value;
                RaisePropertyChanged("VatRefundsIbanDataModel");
            }
        }

        private GenericPickerModel _pickerModel { get; set; }
        public GenericPickerModel PickerModel
        {
            get 
            {
                return _pickerModel;
            }   
            set
            {   
                _pickerModel = value;
                RaisePropertyChanged("PickerModel");
            }
        }

        private ObservableCollection<VarRefundIbanDataModelMetadataResult> _ibanData = null;
        public ObservableCollection<VarRefundIbanDataModelMetadataResult> IbanData
        {
            get
            {
                return _ibanData;
            }

            set
            {

                _ibanData = value;
                RaisePropertyChanged("IbanData");
            }
        }

        private bool _isAddAccountVisisble = false;
        public bool IsAddAccountVisisble
        {
            get
            {
                return _isAddAccountVisisble;
            }

            set
            {

                _isAddAccountVisisble = value;
                RaisePropertyChanged("IsAddAccountVisisble");
            }
        }

        private ObservableCollection<IBANType> _iBANTypesList { get; set; }
        public ObservableCollection<IBANType> IBANTypesList
        {
            get
            {
                return _iBANTypesList;
            }
            set
            {
                _iBANTypesList = value; 
                //if (_iBANTypesList != null && _iBANTypesList.Count != 0)
                //{
                //    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false)
                //    {
                //        IsEnableIBANType = false;
                //    }
                //    else
                //    {
                //        IsEnableIBANType = true;
                //    }
                //}
                //else
                //{
                //    IsEnableIBANType = false;
                //}
                RaisePropertyChanged("IBANTypesList");
            }
        }

        public VATRefundsNewRequestViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            GoBackBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            IbanIdTypeTapped = new Command(OnIbanIdTypeClicked);

            //ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);
            //OutletContinueBtnTapped = new Command(this.OutletContinueBtnClicked);
            //AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            //DeclarationContinueBtnTapped = new Command(this.DeclarationContinueBtnClicked);
            //SummaryContinueBtnTapped = new Command(this.SummaryContinueBtnClicked);
            //OnTinRegisrtationReasonDateTapped = new Command(this.OnTinRegisrtationReasonDateClicked);
            //OnTinRegistrationReasonTapped = new Command(this.OnTinRegisrtationReasonClicked);
            //TinDeregistrationModel = new TINDeregistrationModel();
            //SelectedOutletOption = new TINDeregistrationModel();

            //AddOutletDecisionOptions();
            //PopulateAttachmentsListViewTemplate();
            //PopulateSummaryReasonData();
            //PopulateSummaryDeclarationData();
            //EnableReasonView();
        }

        public async void ReloadData()
        {
            try
            {
                await Task.Run(() =>
                {
                    App.DisplayProgressView();
                });

                VatRefundsDisplayDataModel = await WebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData("");

                VatRefundsIbanDataModel = await WebServiceManager.GAZTGetVATRefundGetIbanData("");
                IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>(VatRefundsIbanDataModel.IbanSet.Results);

                if(IbanData == null || IbanData.Count == 0)
                {
                    IsAddAccountVisisble = true;
                }
                else
                {
                    IsAddAccountVisisble = false;
                }

                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }
            catch (GAZTErrorException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    App.HideProgressView();
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void OnIbanIdTypeClicked()
        {
            ObservableCollection<string> idTypeData = new ObservableCollection<string>();
            idTypeData.Add(AppResources.ZIBANNationalID);
            idTypeData.Add(AppResources.ZIBANCommercialRegistrationID);
            idTypeData.Add(AppResources.ZIBANCompanyID);

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = idTypeData;
            genericPickerModel.PickerTitle = "ID Type";
            genericPickerModel.PickerId = "idTypePicker";

            await PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
        }

        public void createIBANType()
        {
            IBANTypesList = new ObservableCollection<IBANType>();
            ObservableCollection<IBANType> IBANTypesDummyList = new ObservableCollection<IBANType>();
            IBANType iBANType = new IBANType();
            iBANType.key = "ZS0001";
            iBANType.Text = AppResources.ZIBANNationalID;
            IBANTypesDummyList.Add(iBANType);   
            IBANType iBANType1 = new IBANType();
            iBANType1.key = "BUP002";
            iBANType1.Text = AppResources.ZIBANCommercialRegistrationID;
            IBANTypesDummyList.Add(iBANType1);
            IBANType iBANType2 = new IBANType();
            iBANType2.key = "ZS0005";
            iBANType2.Text = AppResources.ZIBANCompanyID;
            IBANTypesDummyList.Add(iBANType2);
            IBANTypesList = IBANTypesDummyList;
        }
    }
}
