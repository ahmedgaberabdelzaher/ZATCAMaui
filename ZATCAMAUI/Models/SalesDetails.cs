using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ZATCAMAUI.Models
{

    public class SalesDetails : INotifyPropertyChanged
    {
        public string SalesType { get; set; }
        public string InformationFromPartie { get; set; }
        // public string InformationFromPartieToCompare { get; set; }
        public string EstimateSales { get; set; }
        public string EditImageSource { get; set; } = "";
        public string NewValue { get; set; } = "";
        public string ChangeReason { get; set; } = "";
        public string AttchamentNumber { get; set; }
        public string AttchamentName { get; set; }
        public string SelectedEditFieldId { get; set; }
        public bool ComingFromAmendEditMode { get; set; } = false;
        public string OldValue { get; set; }
        public bool IsAttachmentRequired { get; set; } = false;
        public bool IsReasonRequird { get; set; } = false;
        public bool IsOldValueChanged { get; set; } = false;
        public bool SeparatorVisibility { get; set; } = true;
        public bool InformationIconVisibility { get; set; } = true;
        public bool HelpIconVisibility { get; set; }
        public ObservableCollection<EstimateZakatAttachment> estimateZakatAttachment = new ObservableCollection<EstimateZakatAttachment>();
        // public string SalesDetailsList { get; set; } = "ic_edit_gray.png";
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        private string _informationFromPartieToCompare;
        public string InformationFromPartieToCompare
        {
            get
            {
                return _informationFromPartieToCompare;
            }
            set
            {
                _informationFromPartieToCompare = value;
                OnPropertyRaised("InformationFromPartieToCompare");
            }
        }
        private Color _disableItemBackgroundColor = Color.Gray;
        public Color DisableItemBackgroundColor
        {
            get
            {
                return _disableItemBackgroundColor;
            }
            set
            {
                _disableItemBackgroundColor = value;
                OnPropertyRaised("DisableItemBackgroundColor");
            }
        }
    }
}
