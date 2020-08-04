using System;
using System.Collections.ObjectModel;

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

    public class TinDeregestrationAttachmentsModel
    {
        public TinDeregestrationAttachmentsModel()
        {
        }

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
        public bool IsAttachmentAttached { get; set; }
    }
}
