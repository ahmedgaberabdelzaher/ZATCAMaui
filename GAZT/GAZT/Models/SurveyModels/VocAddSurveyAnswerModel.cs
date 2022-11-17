using System;
using System.Collections.Generic;

namespace EGAZT.Models.SurveyModels
{
    public class An
    {
        public string rowId { get; set; }
        public string columnID { get; set; }
        public string text { get; set; }
    }

    public class Answer
    {
        public string questionID { get; set; }
        public List<An> answer { get; set; }
    }

    public class CustomData
    {
        public string CustomerSegment { get; set; }
        public string mobile { get; set; }
        public string firstName { get; set; }
        public string TIN { get; set; }
    }

    public class Date
    {
        public string start { get; set; }
        public string end { get; set; }
    }

    public class ResponseArr
    {
        public CustomData user { get; set; }
        public List<Answer> surveyAnswers { get; set; }
        public Date date { get; set; }
    }

    public class VocAddSurveyAnswerModel
    {
        public string surveyID { get; set; }
        public string collectID { get; set; }
        public List<ResponseArr> feedback { get; set; }
    }
}
