using System.IO;

namespace EGAZT.Models.SubmitReportModel
{
    public class ReportFileModel
    {
        public string filename { get; set; }
        public Stream filecontentStream { get; set; }
        public double FileSize { get; set; }
        public string Id { get; set; }
        public byte[] paramFileStream { get; set; }
    }

}
