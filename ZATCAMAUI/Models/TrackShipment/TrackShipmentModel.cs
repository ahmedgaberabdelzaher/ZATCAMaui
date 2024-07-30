using Prism.Mvvm;
using ZATCAMAUI.Core.Helper;

namespace ZATCAMAUI.Models.TrackShipment
{
    public class TrackShipmentModel : BindableBase
    {
        int _declarationNumber;
        public int declarationNumber { get { return _declarationNumber; } set { _declarationNumber = value; RaisePropertyChanged(); } }

        string _carrierName;
        public string carrierName { get { return _carrierName; } set { _carrierName = value; RaisePropertyChanged(); } }

        string _declarationDate;
        public string declarationDate { get { return _declarationDate; } set { _declarationDate = value; RaisePropertyChanged(); } }

        double _excisetax;
        public double excisetax { get { return _excisetax; } set { _excisetax = value; RaisePropertyChanged(); } }

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

