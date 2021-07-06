using EGAZT.Manager;
using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
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
                RaisePropertyChanged("getSupName");
            }
        }


        public List<IbanListSetResult> _mainListData ;
        public List<IbanListSetResult> MainListData
        {
            get
            {
                return _mainListData;
            }
            set
            {
                if (_mainListData == value) return;

                _mainListData = value;
                RaisePropertyChanged("MainListData");
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

        public void AddNewIBANTappedClicked()
        {
            try
            {
                _navigationService.NavigateTo(App.GAZTBankAccountAddOrUpdatePageView, IBANAccountData);
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


        public  async Task LoadAllIBanAccounts()
        {
            MainListData = new List<IbanListSetResult>();
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

            if (IbanAccounts != null && IbanAccounts.d !=null&& IbanAccounts.d.IbanListSet!= null
            && IbanAccounts.d.IbanListSet.results!=null&& IbanAccounts.d.IbanListSet.results.Count>0)
            {
                try
                    {
                        IBANAccountData = IbanAccounts;
                        MainListData = IbanAccounts.d.IbanListSet.results;
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                        IsLoading = false;
                    }
                }
                IsLoading = false;
            });
        }
    }
}
