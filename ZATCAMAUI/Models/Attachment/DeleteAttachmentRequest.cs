
namespace ZATCAMAUI.Models.Attachments
{
    public class DeleteAttachmentRequest
    {
        public string outletReference { get; set; }
        public string returnGUID { get; set; }
        public string attachment { get; set; }
        public string documentCategory { get; set; }
        public string formGUID { get; set; }
        public string serialNumber { get; set; }
        public object documentId { get; set; }
        public string attachedByPerson { get; set; }
        public string fileName { get; set; }
    }
}
