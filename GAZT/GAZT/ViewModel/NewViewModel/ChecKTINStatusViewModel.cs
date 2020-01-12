using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class ChecKTINStatusViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnCloseClick { get; set; }

        public ICommand OnClickLessOrMore { get; set; }



        private string _TIN = string.Empty;

        public string TIN
        {
            get
            {
                return _TIN;
            }
            set
            {
                _TIN = value;
                RaisePropertyChanged("TIN");
            }
        }


        private string _TINStatus = string.Empty;

        public string TINStatus
        {
            get
            {
                return _TINStatus;
            }
            set
            {
                _TINStatus = value;
                RaisePropertyChanged("TINStatus");
            }
        }

        private string _LastUpdate = string.Empty;
        public string LastUpdate
        {
            get
            {
                return _LastUpdate;
            }
            set
            {
                _LastUpdate = value;
                RaisePropertyChanged("LastUpdate");
            }
        }

        private bool _isVisibleListItems = false;
        public bool IsVisibleListItems
        {
            get
            {
                return _isVisibleListItems;
            }
            set
            {
                _isVisibleListItems = value;
                RaisePropertyChanged("IsVisibleListItems");
            }

        }

        private List<TINStatus> _listTINStatus;

        public List<TINStatus> ListTINStatus
        {
            get
            {
                return _listTINStatus;
            }
            set
            {
                _listTINStatus = value;
                RaisePropertyChanged("ListTINStatus");
            }
        }

        private string _showLessOrMore= AppResources.ZShowmoredetails;

        public string ShowLessOrMore
        {
            get
            {
                return _showLessOrMore;
            }
            set
            {
                _showLessOrMore = value;
                RaisePropertyChanged("ShowLessOrMore");
            }
        }

        
        public ChecKTINStatusViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            OnCloseClick = new Command(async () =>
            {
                _navigationService.NavigateTo(App.DashboardPageView);
            });

            OnClickLessOrMore = new Command(async () =>
            {
                if (IsVisibleListItems == false)
                {
                    IsVisibleListItems = true;
                    ShowLessOrMore = AppResources.ZShowlessdetails;

                }
                else
                {
                    IsVisibleListItems = false;
                    ShowLessOrMore = AppResources.ZShowmoredetails;
                }
            });
        }

        public async Task OnPageLoad()
        {

            List<TINStatus> StatusList = new List<TINStatus>
            {
                new TINStatus{ CRNos="111111111111111",CRStatus="Deactive",LastUpdate="10-10-2019"},
                 new TINStatus{ CRNos="111111111111111",CRStatus="Deactive",LastUpdate="10-10-2019"},
                  new TINStatus{ CRNos="111111111111111",CRStatus="Deactive",LastUpdate="10-10-2019"},
                   new TINStatus{ CRNos="111111111111111",CRStatus="Deactive",LastUpdate="10-10-2019"},
                    new TINStatus{ CRNos="111111111111111",CRStatus="Deactive",LastUpdate="10-10-2019"},
                     new TINStatus{ CRNos="111111111111111",CRStatus="Deactive",LastUpdate="10-10-2019"},
                new TINStatus{ CRNos="111111122222222",CRStatus="Active",LastUpdate="11-10-2019"}
            };
            ListTINStatus = StatusList;

        }
    }
}
