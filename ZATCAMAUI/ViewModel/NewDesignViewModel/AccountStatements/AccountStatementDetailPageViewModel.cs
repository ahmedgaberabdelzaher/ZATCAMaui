
using System.Collections.ObjectModel;

using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models.AccountDetails;
using static ZATCAMAUI.Models.AccountDetails.AccoungtDetails;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements
{
    
    public class AccountStatementDetailPageViewModel:BaseViewModel
    {
        public ICommand TapCommand { get; set; }
   //     public AccoungtDetails accoungtDetails { get; set; }

        private AccoungtDetails _AccDertails = null;
        public AccoungtDetails accoungtDetails
        {
            get
            {
                return _AccDertails;
            }
            set
            {
                if (_AccDertails == value) return;
                _AccDertails = value;
                OnPropertyChanged("accoungtDetails");
            }

        }

        private ObservableCollection<Result_Bill> _BillDetails;
        public ObservableCollection<Result_Bill> billDetails
        {
            get
            {
                return _BillDetails;
            }
            set
            {
                if (_BillDetails == value) return;
                if (_BillDetails == null || _BillDetails.Count == 0)
                {
                    isBIllDetialsVisble = false;
                }
                else
                {
                    isBIllDetialsVisble = true;
                }

                _BillDetails = value;
                OnPropertyChanged("billDetails");
            }

           
        }

        private ObservableCollection<Result_InST> _InstDTLSET;
        public ObservableCollection<Result_InST> instDTLSET
        {
            get
            {
                return _InstDTLSET;
            }
            set
            {
                if (_InstDTLSET == value) return;
                if (_InstDTLSET == null || _InstDTLSET.Count == 0)
                {
                    isInstalmentDetailsVisible = false;
                }
                else
                {
                    isInstalmentDetailsVisible = true;
                }

                _InstDTLSET = value;
                OnPropertyChanged("instDTLSET");
            }

        }

        private ObservableCollection<Result_Obj> _ObjDTLSETS;
        public ObservableCollection<Result_Obj> oBJDTLSets
        {
            get
            {
                return _ObjDTLSETS;
            }
            set
            {
                if (_ObjDTLSETS == value) return;
                if (_ObjDTLSETS == null || _ObjDTLSETS.Count == 0)
                {
                    isObjectionDetailsVisible = false;
                }
                else
                {
                    isObjectionDetailsVisible = true;
                }

                _ObjDTLSETS = value;
                OnPropertyChanged("oBJDTLSets");

            }

        }

        private ObservableCollection<Result_RET> _rETDTLSets;
        public ObservableCollection<Result_RET> RETDTLSets
        {
            get
            {
                return _rETDTLSets;
            }
            set
            {
                if (_rETDTLSets == value) return;
                if (_rETDTLSets == null || _rETDTLSets.Count == 0)
                {
                    isRetunVisible = false;
                }
                else
                {
                    isRetunVisible = true;
                }
                _rETDTLSets = value;
                OnPropertyChanged("RETDTLSets");
            }
        }

        private bool _IsBIllDetialsVisble = true;
        public bool isBIllDetialsVisble
        {
            get
            {
                return _IsBIllDetialsVisble;
               
            }
            set
            {
                if (_IsBIllDetialsVisble == value) return;
                _IsBIllDetialsVisble = value;
                OnPropertyChanged("isBIllDetialsVisble");
            }
        }

        private bool _IsInstalmentDetailsVisible = true;
        public bool isInstalmentDetailsVisible
        {
            get
            {
                return _IsInstalmentDetailsVisible;

            }
            set
            {
                if (_IsInstalmentDetailsVisible == value) return;
                _IsInstalmentDetailsVisible = value;
                OnPropertyChanged("isInstalmentDetailsVisible");
            }
        }

        private bool _IsObjectionDetailsVisible = true;
        public bool isObjectionDetailsVisible {
            get
            {
                return _IsObjectionDetailsVisible;
            }
            set
            {
                if (_IsObjectionDetailsVisible == value) return;
                _IsObjectionDetailsVisible = value;
                OnPropertyChanged("isObjectionDetailsVisible");
            }
        }

        private bool _IsRetunVisiblel = true;
        public bool isRetunVisible
        {
            get {
                return _IsRetunVisiblel;
            }
            set {
                if (_IsRetunVisiblel == value) return;
                _IsRetunVisiblel = value;
                OnPropertyChanged("isRetunVisible");
                }
        }


        public AccountStatementDetailPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            TapCommand = new Command((object s) => {
                     this.onObjTapped(s);
                });
        }

        public void onReload()
        {

        }


        public void onObjTapped(object s)
        {
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
            if (s == "ObjDetails")
            {
                Application.Current.MainPage.Navigation.PushAsync(new AccountsStatementObjectionDetailsPage("Objection Details",this.accoungtDetails));
            }
            else if (s == "InstDetails")
            {
                Application.Current.MainPage.Navigation.PushAsync(new AccountsStatementObjectionDetailsPage("Instalment Plan Details", this.accoungtDetails));
            }
            else if (s == "InstObjeORdetails")
            {
                Application.Current.MainPage.Navigation.PushAsync(new AccountsStatementObjectionDetailsPage("Instalment Plan Orginal Bill Details", this.accoungtDetails));
            }
            else if (s == "ReturnDetails")
            {
                Application.Current.MainPage.Navigation.PushAsync(new AccountsStatementObjectionDetailsPage("Return Details", this.accoungtDetails));
            }
            else
            {
                throw GAZTErrorException();
            }
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
        }

       

        private Exception GAZTErrorException()
        {
            throw new NotImplementedException();
        }
    }
}

