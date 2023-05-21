using System;
namespace EGAZT.Models.TahqaqModels
{
    public class EInvoiceQRModel
    {
      
           public string sellerName{ get; set; }
            public string vatNumber{ get; set; }
            public string timeStamp{ get; set; }
            public string invoiceAmount{ get; set; }
            public string vatAmount{ get; set; }
            public string invoiceHash{ get; set; }
            public string ecdsapublicKey{ get; set; }
            public string signature{ get; set; }
            public string caSignature{ get; set; }
            public string UserId{ get; set; }
            public string latitude{ get; set; }
            public string Tin{ get; set; }
            public string longitude{ get; set; }
            public bool Status{ get; set; }
            public string InvalidData{ get; set; }
        }
}
