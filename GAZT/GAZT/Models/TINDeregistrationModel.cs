using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EGAZT.Models
{
    public class TINDeregistrationModel
    {
        public TINDeregistrationModel()
        {
        }

        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }

    }

    public class TinDeregestrationAttachmentsModel: INotifyPropertyChanged
    {
        public TinDeregestrationAttachmentsModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private string _attachmentName { get; set; }
        public string AttachmentName
        {
            get
            {
                return _attachmentName;
            }
            set
            {
                _attachmentName = value;
                OnPropertyRaised("AttachmentName");
            }
        }

        private bool _isAttachmentAttached { get; set; }
        public bool IsAttachmentAttached
        {
            get
            {
                return _isAttachmentAttached;
            }
            set
            {
                _isAttachmentAttached = value;
                OnPropertyRaised("IsAttachmentAttached");
            }
        }

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
    }

    public class TINDeregistrationSummaryModel
    {
        public TINDeregistrationSummaryModel()
        {
        }

        public string SummaryTitle { get; set; }
        public string SummaryData { get; set; }
        public bool IsEditVisible { get; set; }
    }

    public class ZakatDeregistrationDetailsListModel
    {
        public string ZDTitle { get; set; }
        public string ZDImageSource { get; set; }
    }
    
}
