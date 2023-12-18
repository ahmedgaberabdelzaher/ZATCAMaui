using Prism.Mvvm;
using ZATCAMAUI.Core.Helper;

namespace ZATCAMAUI.Models.TrackShipment
{
    public class TrackShipmentModel : BindableBase
    {
        //long _ISN;
        //public long ISN { get { return _ISN; } set { _ISN = value; RaisePropertyChanged(); } }

        double _totalFees;
        public double totalFees { get { return _totalFees; } set { _totalFees = value; RaisePropertyChanged(); } }

        double _CIF;
        public double CIF { get { return _CIF; } set { _CIF = value; RaisePropertyChanged(); } }

        double _VAT;
        public double VAT { get { return _VAT; } set { _VAT = value; RaisePropertyChanged(); } }

        double _customs;
        public double customs { get { return _customs; } set { _customs = value; RaisePropertyChanged(); } }

        double _others;
        public double others { get { return _others; } set { _others = value; RaisePropertyChanged(); } }

        bool _isCombined;
        public bool isCombined { get { return _isCombined; } set { _isCombined = value; RaisePropertyChanged(); } }

        public List<ActivitiesModel> activities { get; set; }
    }

    public class ActivitiesModel
    {
        public string status_Arabic { get; set; }
        public string status_English { get; set; }
        public string activityDate { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(status_Arabic, status_English);
            }
        }
    }
}

