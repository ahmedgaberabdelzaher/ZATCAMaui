namespace ZATCAMAUI.Models.SurveyModels
{

    public class SurveyByDateData
    {
        public int id { get; set; }
        public bool isuservotedbefore { get; set; }
        public bool dismiss { get; set; }
    }

    public class SurveyByDateResponse
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public SurveyByDateData data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }
}
