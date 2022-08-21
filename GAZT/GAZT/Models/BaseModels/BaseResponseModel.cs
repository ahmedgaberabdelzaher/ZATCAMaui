using System.Collections.Generic;

namespace EGAZT.Models.BaseModels
{
    public class BaseResponseModel<T>
    {
        public string Status { get; set; }
        public bool Success { get; set; }
        public DataModel<T> Result { get; set; }
        public string MessageCode { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
    } 

    public class DataModel<T>
    {
        public T Data { get; set; }
        public int TotalCount { get; set; }
        public int PagesCount { get; set; }
    }
}
