using System;
using System.Collections.Generic;

namespace EGAZT.Models.BaseModels
{
  
    public class Header
    {
        public string requestID { get; set; }
        public Status status { get; set; }
    }

    public class DATAPowerBaseResponse<T>
    {
        public Header header { get; set; }
        public T data { get; set; }
    }

    public class Status
    {
        public string code { get; set; }
        public string description { get; set; }
    }

}

