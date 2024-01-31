using GalaSoft.MvvmLight.Views;
using System.Windows.Input;
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
                RaisePropertyChanged("AknowledgementDataList");
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
                RaisePropertyChanged(() => SadadBillNumber);
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
                RaisePropertyChanged(() => TotalZakatPayble);
            }
        }

        #endregion

        #region Commands
        public ICommand OnFinishClick { get; private set; }
        public ICommand OnBackButtonClick { get; private set; }
        #endregion

        #region Constructor
        public ZakatAcknowledgmentPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            OnFinishClick = new Command(() => navigateToNext());
            OnBackButtonClick = new Command(() => navigateBack());
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

        private void navigateToNext()
        {

        }

        private void navigateBack()
        {

        }
    }
}