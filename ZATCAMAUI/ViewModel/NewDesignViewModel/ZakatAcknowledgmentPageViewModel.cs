
using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models.Form5Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{
   
    public class ZakatAcknowledgmentPageViewModel : BaseViewModel
    {

        #region Acknowledgement 

        private List<Result_9> _AknowledgementDataList;
        public List<Result_9> AknowledgementDataList
        {
            get
            {
                return _AknowledgementDataList;
            }
            set
            {
                if (_AknowledgementDataList == value) return;
                _AknowledgementDataList = value;
                OnPropertyChanged("AknowledgementDataList");
            }
        }

        public string _sadadBillNumber;
        public string SadadBillNumber
        {
            get => _sadadBillNumber;

            set
            {
                if (_sadadBillNumber == value) return;

                _sadadBillNumber = value;
                OnPropertyChanged(nameof( SadadBillNumber));
            }
        }


        public string _totalZakatPayble;
        public string TotalZakatPayble
        {
            get => _totalZakatPayble;

            set
            {
                if (_totalZakatPayble == value) return;

                _totalZakatPayble = value;
                OnPropertyChanged(nameof( TotalZakatPayble));
            }
        }

        #endregion

        #region Commands

        public ICommand OnAppearingZakatAcknowledgmentPageCommand
        {
            get
            {
                return new Command( _ =>
                {
                    LoadZakatForm5_ACK_Data();

                });
            }
        }

        public ICommand OnFinishedCommand
        {
            get
            {
                return new Command( _ =>
                {
                    try
                    {
                        var _navigation = Application.Current.MainPage.Navigation;
                        if (_navigation.NavigationStack.Count > 0)
                        {
                            Page pg = _navigation.NavigationStack[_navigation.NavigationStack.Count - 2];
                            _navigation.RemovePage(pg);
                        }
                        _navigationService.GoBack();
                    }
                    catch (Exception)
                    {

                    }
                   

                });
            }
        }

        #endregion

        #region Constructor
        public ZakatAcknowledgmentPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
        #endregion

        public void LoadZakatForm5_ACK_Data()
        {
            if (AknowledgementDataList[0].Sopbel != "")
            {
                SadadBillNumber = AknowledgementDataList[0].Sopbel.ToString();
            }
            else
            {
                SadadBillNumber = " - ";

            }

            if (AknowledgementDataList[0].Betrh != "")
            {

                TotalZakatPayble = UtilityManager.GetCommaSeparatedAmount(AknowledgementDataList[0].Betrh.ToString());
            }
            else
            {
                TotalZakatPayble = " - ";

            }


        }
    }
}