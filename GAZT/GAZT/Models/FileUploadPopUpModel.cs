using System.Collections.Generic;

namespace EGAZT.Models.ZakatInstalationModels
{
    
        public class AttachmentModel
        {
            public Metadata5 __metadata { get; set; }
            public string RetGuid { get; set; }
            public string Seqno { get; set; }
            public string SchGuid { get; set; }
            public string Dotyp { get; set; }
            public int Srno { get; set; }
            public string Doguid { get; set; }
            public string AttBy { get; set; }
            public string Filename { get; set; }
            public string FileExtn { get; set; }
            public string Mimetype { get; set; }
            public string ByPusr { get; set; }
            public string Erfdt { get; set; }
            public string Erftm { get; set; }
            public string DataVersion { get; set; }
            public string DocUrl { get; set; }
            public string OutletRef { get; set; }
            public string Enbedit { get; set; }
            public string Enbdele { get; set; }
            public string Visedit { get; set; }
            public string Visdel { get; set; }
            
        }
        
        public class Attachments
        {
            public List <Attachment> results { get; set; }
        }
        
        public enum WhichAttachment
        {
           VATInstalment = 0,
            ContractReleaseCopy = 2,
            ContractReleaseInvoice = 3,
            ChnageFillingPeriod2Years = 4,
            ChnageFillingPeriod12Months=5,
            ChnageFillingPeriodOtherDoc=6,
    }
    
}