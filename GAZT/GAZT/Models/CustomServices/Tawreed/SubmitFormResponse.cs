using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EGAZT.Models.CustomServices.Tawreed
{

    public class Header
    {
        public string requestID { get; set; }
        public Status status { get; set; }
        public MoreInformation moreInformation { get; set; }

    }

    public class Result
    {
        public long referenceNumber { get; set; }
    }
    public class SubmitFormResponse
    {
        public Header header { get; set; }
        public Result result { get; set; }

    }
    public class MoreInformation
    {
        [JsonProperty("Error details:")]
        public List<string> Errordetails { get; set; }
    }
    public class Status
    {
        public string code { get; set; }
        public string description { get; set; }
    }
}

