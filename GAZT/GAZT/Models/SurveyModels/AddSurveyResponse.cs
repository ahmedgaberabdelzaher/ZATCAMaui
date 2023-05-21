using System;
namespace EGAZT.Models.SurveyModels
{
    public class AddSurveyResponse
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public bool data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }
}
