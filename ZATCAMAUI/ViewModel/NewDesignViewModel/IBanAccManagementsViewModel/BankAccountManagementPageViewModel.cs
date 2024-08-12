
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static ZATCAMAUI.Models.IBanManagementListModel;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Manager;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.IBanAccManagementsViewModel
{
    public class BankAccountManagementPageViewModel : BaseViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand AddNewIBANTapped { get; set; }

        public BankAccountManagementPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });

            AddNewIBANTapped = new Command(this.AddNewIBANTappedClicked);

        }

        private string _SupName = string.Empty;
        public string getSupName
        {
            get
            {
                return _SupName;
            }
            set
            {
                if (_SupName == value) return;

                _SupName = value;
                OnPropertyChanged("getSupName");
            }
        }


        public ObservableCollection<IbanListSetResult> _mainListData = new ObservableCollection<IbanListSetResult>();
        public ObservableCollection<IbanListSetResult> MainListData
        {
            get
            {
                return _mainListData;
            }
            set
            {
                if (_mainListData == value) return;

                _mainListData = value;
                OnPropertyChanged("MainListData");
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
                OnPropertyChanged("IBANAccountData");
            }
        }

        public void AddNewIBANTappedClicked()
        {
            try
            {
                IBANAccountData.d.isUpdateFlag = false;
                App.SelectedIBAN = "";
                _navigationService.NavigateTo(App.GAZTBankAccountAddOrUpdatePageView, IBANAccountData);
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

        public async Task SummaryConButtonClickedAsync(IbanListSetResult selectedItem, string actionFlag, int btnCode = 0)
        {
            IsLoading = true;
            try
            {
                IBANPostResponse IBANPostResponse = null;
                if (btnCode == 1)
                {
                    IBANClickRequest requestObj = new IBANClickRequest();
                    requestObj.Action = actionFlag;
                    requestObj.Fbnum = selectedItem.Fbnum;
                    requestObj.Iban = selectedItem.Iban;
                    requestObj.FormGuid = App.SelectedFbGuid;

                    IBANPostResponse = await IBanManagmentWebserviceManager.GAZTSubmitBankAccountIBAN(requestObj);
                    //public string Action { get; set; }
                    //public string Fbnum { get; set; }
                    //public string FormGuid { get; set; }
                    //public string Iban { get; set; }

                }
                else
                {
                    IBANPostRequest requestObj = new IBANPostRequest();
                    requestObj.Action = actionFlag;
                    requestObj.AgreeFg = "X";
                    requestObj.Fbnum = selectedItem.Fbnum;
                    requestObj.Tin = App.LoginDataRetrieved.TIN;
                    requestObj.Iban = selectedItem.Iban;
                    requestObj.Bkext = selectedItem.Bkext;
                    requestObj.Idnumber = selectedItem.Idnumber;
                    requestObj.IdtypeDesc = selectedItem.IdtypeDesc;
                    requestObj.Koinh = selectedItem.Koinh;
                    requestObj.Bankid = selectedItem.Bankid;
                    requestObj.Type = selectedItem.Type;

                    IBANPostResponse = await IBanManagmentWebserviceManager.GAZTSubmitBankAccountIBAN(requestObj);
                }


                if (IBANPostResponse != null)
                {
                    IsLoading = false;
                    if (actionFlag == "D")
                    {

                        var somewarningpopup = new AttachmentInformationPopUp(AppResources.NDIBANIsDeActivated)
                        {
                            CloseWhenBackgroundIsClicked = false
                        };
                        somewarningpopup.OnDone = async () =>
                        {
                            await LoadAllIBanAccounts();
                        };
                        await MopupService.Instance.PushAsync(somewarningpopup);
                    }
                    else if (actionFlag == "A")
                    {

                        var somewarningpopup = new AttachmentInformationPopUp(AppResources.NDIBANIsActivated)
                        {
                            CloseWhenBackgroundIsClicked = false
                        };
                        somewarningpopup.OnDone = async () =>
                        {
                            await LoadAllIBanAccounts();
                        };
                        await MopupService.Instance.PushAsync(somewarningpopup);
                    }



                }
                IsLoading = false;

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                    IsLoading = false;
                });
            }
        }


        public async Task LoadAllIBanAccounts()
        {
            // MainListData = new ObservableCollection<IbanListSetResult>();
            MainListData.Clear();
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                IBanAccountManagementResponseModel IbanAccounts = await IBanManagmentWebserviceManager.GAZTGetIBanAccounts();

                PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                IsLoading = false;

                foreach (var item in IbanAccounts.d.IbanListSet)
                {
                    IbanListSetResult newItem = new IbanListSetResult();

                    newItem.VisibleUpdate = item.VisibleUpdate;
                    newItem.Type = item.Type;
                    newItem.Tin = item.Tin;
                    newItem.StatusDesc = item.StatusDesc;
                    newItem.Status = item.Status;
                    newItem.Koinh = item.Koinh;
                    newItem.IdtypeDesc = item.IdtypeDesc;
                    newItem.Idnumber = item.Idnumber;
                    newItem.Iban = item.Iban;
                    newItem.FormGuid = item.FormGuid;
                    newItem.Fbnum = item.Fbnum;
                    newItem.EnableUpdate = item.EnableUpdate;
                    newItem.Bkext = item.Bkext;
                    newItem.Bankid = item.Bankid;
                    newItem.AgreeFg = item.AgreeFg;
                    newItem.ActiveIban = item.ActiveIban;
                    newItem.Action = item.Action;

                    if (item.VisibleUpdate.Equals(""))
                    {
                        if (item.ActiveIban.Equals("X"))
                        {
                            //EDIT icon
                            newItem.StatusText = AppResources.IBanDeactivate;
                            newItem.isUpdateDisabled = false;
                            newItem.isUpdateEnabled = true;
                        }
                        else
                        {
                            newItem.StatusText = AppResources.IBanActivate;
                            newItem.isUpdateDisabled = true;
                            newItem.isUpdateEnabled = false;
                        }

                    }
                    else
                    {
                        if (item.EnableUpdate.Equals("X"))
                        {
                            newItem.StatusText = AppResources.IBANUpdate;
                            newItem.isUpdateEnabled = true;
                            newItem.isUpdateDisabled = false;

                        }
                        else
                        {
                            newItem.StatusText = AppResources.IBANUpdate;
                            newItem.isUpdateEnabled = false;
                            newItem.isUpdateDisabled = true;
                        }
                        //newItem.isUpdateDisabled = true;
                        //newItem.isUpdateEnabled = false;
                    }
                    MainListData.Add(newItem);
                }



                if (IbanAccounts != null && IbanAccounts.d != null && IbanAccounts.d.IbanListSet != null
                && IbanAccounts.d.IbanListSet != null && IbanAccounts.d.IbanListSet.Count > 0)
                {
                    try
                    {
                        // MainListData = IbanAccounts.d.IbanListSet.results;

                        var RejectMatch = MainListData.Where(selectedValue => (selectedValue.StatusDesc == "Rejected") || (selectedValue.StatusDesc == "„—›Ê÷")).First();
                        var MissingIfoMatch = MainListData.Where(selectedValue => (selectedValue.StatusDesc == "Missing Informaiton") || (selectedValue.StatusDesc == "„⁄·Ê„«  «·Õ”«» €Ì— „ﬂ „·…")).First();


                        if (MissingIfoMatch != null || RejectMatch != null)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANIncomplete));

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                        IsLoading = false;
                    }
                }


                if (IbanAccounts != null && IbanAccounts.d != null)
                {

                    IBANAccountData = IbanAccounts;

                }

                IsLoading = false;
            });
        }
    }
}

