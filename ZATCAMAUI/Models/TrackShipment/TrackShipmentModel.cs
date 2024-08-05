using CommunityToolkit.Mvvm.ComponentModel;
using ZATCAMAUI.Core.Helper;

namespace ZATCAMAUI.Models.TrackShipment
{
    public class TrackShipmentModel : ObservableRecipient
    {
        int _declarationNumber;
        public int declarationNumber { get { return _declarationNumber; } set { _declarationNumber = value; OnPropertyChanged(); } }

        string _carrierName;
        public string carrierName { get { return _carrierName; } set { _carrierName = value; OnPropertyChanged(); } }

        string _declarationDate;
        public string declarationDate { get { return _declarationDate; } set { _declarationDate = value; OnPropertyChanged(); } }

        double _excisetax;
        public double excisetax { get { return _excisetax; } set { _excisetax = value; OnPropertyChanged(); } }

        double _totalFees;
        public double totalFees { get { return _totalFees; } set { _totalFees = value; OnPropertyChanged(); } }

        double _CIF;
        public double CIF { get { return _CIF; } set { _CIF = value; OnPropertyChanged(); } }

        double _VAT;
        public double VAT { get { return _VAT; } set { _VAT = value; OnPropertyChanged(); } }

        double _customs;
        public double customs { get { return _customs; } set { _customs = value; OnPropertyChanged(); } }

        double _others;
        public double others { get { return _others; } set { _others = value; OnPropertyChanged(); } }

        bool _isCombined;
        public bool isCombined { get { return _isCombined; } set { _isCombined = value; OnPropertyChanged(); } }

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

