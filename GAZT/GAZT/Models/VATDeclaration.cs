using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    public class VATDeclaration
    {
    }

    public class VATAttachments
    {
        public string Id { get; set; }
        public string DocumentName { get; set; }
        public string Size { get; set; }
    }

    public class CreditCarried
    {
        public string SerialNumber { get; set; }
        public string ReturnReferenceNumber { get; set; }
        public string DocumentNumber { get; set; }
        
        public string Amount { get; set; }
    }
}
