using System;
using System.Collections.Generic;

namespace EGAZT.Models.EinvoiceModels.DPModels
{
    public class QRLog
    {
        public string sellerName { get; set; }
        public string VATNumber { get; set; }
        public string timeStamp { get; set; }
        public string invoiceAmount { get; set; }
        public string VATAmount { get; set; }
        public string invoiceHash { get; set; }
        public string ECDSAPublicKey { get; set; }
        public string signature { get; set; }
        public string CASignature { get; set; }
        public int userId { get; set; }
        public string phone { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public bool status { get; set; }
        public string invalidData { get; set; }
    }

    public class QRLogModel
    {
        public string languageCode { get; set; }
        public List<QRLog> QRLogs { get; set; }
    }
}

