using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using EGAZT.Models.BaseModels;

namespace EGAZT.Models.CustomServices.Tawreed
{

    public class Result
    {
        public long referenceNumber { get; set; }
    }
    public class SubmitFormResponse
    {
        public Header header { get; set; }
        public Result result { get; set; }

    }
  
}

