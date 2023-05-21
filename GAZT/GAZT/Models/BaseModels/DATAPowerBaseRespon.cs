using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EGAZT.Models.BaseModels
{
  
    public class Header
    {
        public string requestID { get; set; }
        public Status status { get; set; }
        public MoreInformation moreInformation { get; set; }
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
    public class DATAPowerBaseResponseResult<T>
    {
        public Header header { get; set; }
        public T result { get; set; }
    }
    public class MoreInformation
    {
        public string backendErrors { get; set; }
        [JsonProperty("Error details:")]
        public List<string> Errordetails { get; set; }
    }

}

