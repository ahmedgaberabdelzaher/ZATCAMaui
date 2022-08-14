using System.Collections.Generic;

namespace EGAZT.Models.SubmitReportModel
{
    public class BaseVatReport<T>
    {
        public string Status { get; set; }
        public bool Success { get; set; }
        public DataModel<T> Result { get; set; }
        public string MessageCode { get; set; }
    } 

    public class DataModel<T>
    {
        public T Data { get; set; }
    }
}
