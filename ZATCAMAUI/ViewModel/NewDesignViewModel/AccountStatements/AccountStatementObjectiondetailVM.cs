using System.Collections.ObjectModel;
using System.Windows.Input;
using Foundation;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models.AccountDetails;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using static ZATCAMAUI.Models.AccountDetails.AccoungtDetails;

namespace EGAZT.ViewModel.NewDesignViewModel.AccountStatements
{
    [Preserve(AllMembers = true)]
    public class AccountStatementObjectiondetailVM : BaseViewModel
    {
        public ICommand GoToBackbutton{ get; set;}


        private AccoungtDetails _AccDertails = null;
        public AccoungtDetails accountDetails
        {
            get
            {
                return _AccDertails;
            }
            set
            {
                if (_AccDertails == value) return;
                _AccDertails = value;
                OnPropertyChanged("accountDetails");
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
                if(_rETDTLSets == null || _rETDTLSets.Count == 0)
                {
                    isReturnsVisible = false;
                }
                else
                {
                    isReturnsVisible = true;
                }
                _rETDTLSets = value;
                OnPropertyChanged("RETDTLSets");
            }
        }

        private bool _IsRetunsVisible = false;
        public bool isReturnsVisible
        {
            get
            {
                return _IsRetunsVisible;

            }
            set
            {
                if (_IsRetunsVisible == value) return;
                _IsRetunsVisible = value;
                OnPropertyChanged("isReturnsVisible");
            }
        }

        private bool _IsBIllDetialsVisble = false;
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

        private bool _IsInstalmentDetailsVisible = false;
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

        private bool _IsObjectionDetailsVisible = false;
        public bool isObjectionDetailsVisible
        {
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

        private bool _IsInstalmentplanOrginaldetailsAvlbl = false;
        public bool isInstalmentplanOrginaldetailsAvlbl
        {
            get
            {
                return _IsInstalmentplanOrginaldetailsAvlbl;
            }
            set
            {
                if (_IsInstalmentplanOrginaldetailsAvlbl == value) return;
                _IsInstalmentplanOrginaldetailsAvlbl = value;
                OnPropertyChanged("isInstalmentplanOrginaldetailsAvlbl");
            }
        }

        public string _nAmeof;
       // public string _filterLabelText;
        public string NAmeof
        {
            get
            {
                return _nAmeof;
            }
            set
            {
                if (_nAmeof == value) return;
                   _nAmeof = value;

                OnPropertyChanged("NAmeof");
            }
        }

        public AccountStatementObjectiondetailVM(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoToBackbutton = new Command(() => {
                _navigationService.GoBack();
            });
        }


    }
}

