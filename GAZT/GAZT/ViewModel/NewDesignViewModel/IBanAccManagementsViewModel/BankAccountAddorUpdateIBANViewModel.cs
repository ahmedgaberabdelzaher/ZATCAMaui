using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using static EGAZT.Models.IBanManagementListModel;

namespace EGAZT.ViewModel.NewDesignViewModel.IBanAccManagementsViewModel
{
    [Preserve(AllMembers = true)]
    public class BankAccountAddorUpdateIBANViewModel : BaseViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand ContinueButtonTapped { get; set; }
        public ICommand ShowIDTypePicker { get; set; }
        public ICommand ShowIDNumberPicker { get; set; }
        public ICommand ShowBankNamePicker { get; set; }
        public ICommand GoBackToNewForm { get; set; }
        public ICommand SummaryConBtnTapped { get; set; }




        #region Enums

        enum PagesEnum
        {
            IBANNewForm,
            IBANSummary,

        }

        #endregion


        int selectedPage = (int)PagesEnum.IBANNewForm;

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
                else
                {
                    MarkComplete = false;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
            }
        }

        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 2;

        private string _selectedIDType = "";

        public string SelectedIDType
        {
            get { return _selectedIDType; }
            set
            {
                if (_selectedIDType == value) return;

                _selectedIDType = value;
                RaisePropertyChanged("SelectedIDType");
            }
        }

        private string _selectedIDNumber = "";

        public string SelectedIDNumber
        {
            get { return _selectedIDNumber; }
            set
            {
                if (_selectedIDNumber == value) return;

                _selectedIDNumber = value;
                RaisePropertyChanged("SelectedIDNumber");
            }
        }

        private string _selectedBankName = "";

        public string SelectedBankName
        {
            get { return _selectedBankName; }
            set
            {
                if (_selectedBankName == value) return;

                _selectedBankName = value;
                RaisePropertyChanged("SelectedBankName");
            }
        }

        private string _selectedIDTypeValue = "";

        public string SelectedIDTypeValue
        {
            get { return _selectedIDTypeValue; }
            set
            {
                if (_selectedIDTypeValue == value) return;

                _selectedIDTypeValue = value;
                RaisePropertyChanged("SelectedIDTypeValue");
            }
        }

        private string _selectedIDNumberValue = "";

        public string SelectedIDNumberValue
        {
            get { return _selectedIDNumberValue; }
            set
            {
                if (_selectedIDNumberValue == value) return;

                _selectedIDNumberValue = value;
                RaisePropertyChanged("SelectedIDNumberValue");
            }
        }

        private string _selectedBankNameValue = "";

        public string SelectedBankNameValue
        {
            get { return _selectedBankNameValue; }
            set
            {
                if (_selectedBankNameValue == value) return;

                _selectedBankNameValue = value;
                RaisePropertyChanged("SelectedBankNameValue");
            }
        }

        private string _accountOwnerName = "";

        public string AccountOwnerName
        {
            get { return _accountOwnerName; }
            set
            {
                if (_accountOwnerName == value) return;

                _accountOwnerName = value;
                RaisePropertyChanged("AccountOwnerName");
            }
        }

        private string _IBANValue = "";

        public string IBANValue
        {
            get { return _IBANValue; }
            set
            {
                if (_IBANValue == value) return;

                _IBANValue = value;
                RaisePropertyChanged("IBANValue");
            }
        }
        private IBanAccountManagementResponseModel _iBANAccountData;

        public IBanAccountManagementResponseModel IBANAccountData
        {
            get { return _iBANAccountData; }
            set
            {
                if (_iBANAccountData == value) return;

                _iBANAccountData = value;
                RaisePropertyChanged("IBANAccountData");
            }
        }

        private GenericPickerModel _pickerModel { get; set; }
        public GenericPickerModel PickerModel
        {
            get { return _pickerModel; }
            set
            {
                if (_pickerModel == value) return;

                _pickerModel = value;
                RaisePropertyChanged("PickerModel");
            }
        }


        private bool _newFormVisible = false;

        public bool NewFormVisible
        {
            get { return _newFormVisible; }
            set
            {
                if (_newFormVisible == value) return;

                _newFormVisible = value;
                RaisePropertyChanged("NewFormVisible");
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
                RaisePropertyChanged("SummaryVisible");
            }
        }

        public BankAccountAddorUpdateIBANViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
           


            GoBackBtnTapped = new Command(async () => { BackNavigations(); });

            ContinueButtonTapped = new Command(this.ContinueButtonClicked);

            ShowIDTypePicker = new Command(() => { showPickerDialog(1); });
            ShowIDNumberPicker = new Command(() => { showPickerDialog(2); });
            ShowBankNamePicker = new Command(() => { showPickerDialog(3); });
            GoBackToNewForm = new Command(() => { SummaryEditClicked(); });
            SummaryConBtnTapped = new Command(() => { SummaryConButtonClicked(); });

            NewFormVisible = true;


        }

        private void SummaryEditClicked()
        {
            EnableNewFormView();
        }

          private void BackNavigations()
        {
            switch (selectedPage)
            {
                case (int)PagesEnum.IBANNewForm:
                    _navigationService.GoBack();
                    break;
                case (int)PagesEnum.IBANSummary:
                    EnableNewFormView();
                    break;
            }
        }

        private void EnableNewFormView()
        {
            CurrentIndex = 1;
            NewFormVisible = true;
            SummaryVisible = false;
            selectedPage = (int)PagesEnum.IBANNewForm;
        }

        private void EnableSummaryView()
        {
            CurrentIndex = 2;
            NewFormVisible = false;
            SummaryVisible = true;
            selectedPage = (int)PagesEnum.IBANSummary;
        }

        private void setIDTypePickerModel()
        {

            if (PickerModel != null) {

                PickerModel = null;
            }


            var list = new List<string>();

            foreach (IdTypeListSetResult dropdown in IBANAccountData.d.IdTypeListSet.results)
            {
                try
                {
                    list.Add(dropdown.IdDesc);
                }
                catch (Exception ex)
                {

                }


            }


            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "IBANIdTypePicker";
            PickerModel = genericPickerModel;
        }

        private void setIDNumberPickerModel()
        {

            if (PickerModel != null)
            {

                PickerModel = null;
            }
            var list = new List<string>();

            foreach (IdNumberListSetResult dropdown in IBANAccountData.d.IdNumberListSet.results)
            {
                try
                {
                    list.Add(dropdown.IdNumber);
                }
                catch (Exception ex)
                {

                }

            }
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "IBANIdNumberPicker";
            PickerModel = genericPickerModel;
        }

        private void setBankNamePickerModel()
        {
            if (PickerModel != null)
            {

                PickerModel = null;
            }

            var list = new List<string>();

            foreach (Result dropdown in IBANAccountData.d.BankListSet.results)
            {
                try
                {
                    list.Add(dropdown.Bkext);
                }
                catch (Exception ex)
                {

                }

            }
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "IBANBankNamePicker";
            PickerModel = genericPickerModel;
        }

        private async void showPickerDialog(int pickerID)
        {
            try
            {

                if(pickerID == 1) {

                    setIDTypePickerModel();
                }
                else if (pickerID == 2)
                {
                    setIDNumberPickerModel();
                }
                else if (pickerID == 3)
                {
                    setBankNamePickerModel();
                }

                await PopupNavigation.Instance.PushAsync(new PickerPageView(PickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private async void SummaryConButtonClicked()
        {
            try
            {

     
                IBANPostRequest requestObj = new IBANPostRequest();

                if (string.IsNullOrEmpty(App.SelectedIBAN)) {

                    requestObj.Action = "N";

                }
                else {
                    requestObj.Action = "U";

                }

                requestObj.AgreeFg = "X";
                requestObj.Fbnum = "";
                requestObj.Tin = App.LoginDataRetrieved.TIN;
                requestObj.Iban = IBANValue;
                requestObj.Bkext = SelectedBankName;
                requestObj.Idnumber = SelectedIDNumber;
                requestObj.IdtypeDesc = SelectedIDType;
                requestObj.Koinh = AccountOwnerName;
                requestObj.Bankid = SelectedBankNameValue;
                requestObj.Type = SelectedIDTypeValue;

               var IBANPostResponse = await IBanManagmentWebserviceManager.GAZTSubmitBankAccountIBAN(requestObj);

                if(IBANPostResponse.d.Action.Equals("N"))
                {
                    var somewarningpopup = new AttachmentInformationPopUp(AppResources.AmendRegistrationSubmitWarning)
                    {
                        CloseWhenBackgroundIsClicked = false
                    };
                    somewarningpopup.OnDone = async () =>
                    {
                        _navigationService.GoBack();
                    };
                }

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
               

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public void ContinueButtonClicked()
        {
            try
            {
                if(string.IsNullOrEmpty(SelectedBankName) || string.IsNullOrEmpty(SelectedIDNumber) || string.IsNullOrEmpty(SelectedIDType) || string.IsNullOrEmpty(AccountOwnerName) || string.IsNullOrEmpty(IBANValue)) {

                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                }
                else {

                    if(IBANValue.Length < 24) {

                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforLenght));

                        return;

                    }


                    var selectedType = IBANAccountData.d.IdTypeListSet.results.Find(selectedValue => (selectedValue.IdDesc == SelectedIDType));

                    if(selectedType != null) {
                        SelectedIDTypeValue = selectedType.IdType;
                    }

                    var BankID = IBANAccountData.d.BankListSet.results.Find(selectedValue => (selectedValue.Bkext == SelectedBankName));

                    if (selectedType != null)
                    {
                        SelectedBankNameValue = BankID.Bankid;
                    }



                    EnableSummaryView();
                }


                
            }
            catch (GAZTUnlockAccountException ex)
            {

                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }



    }
}
