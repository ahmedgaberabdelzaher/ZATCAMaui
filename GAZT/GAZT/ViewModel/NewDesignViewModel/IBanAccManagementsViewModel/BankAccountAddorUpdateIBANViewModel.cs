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
    public class BankAccountAddorUpdateIBANViewModel : BaseViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand ContinueButtonTapped { get; set; }



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

        public BankAccountAddorUpdateIBANViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });

            ContinueButtonTapped = new Command(this.ContinueButtonClicked);


        }

        public void ContinueButtonClicked()
        {
            try
            {
                
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
