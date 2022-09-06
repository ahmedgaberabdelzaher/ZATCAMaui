using System;
using System.Collections.Generic;

namespace EGAZT.Models.SurveyModels
{
  
    public class An
    {
        public string rowId { get; set; }
        public string colId { get; set; }
        public string text { get; set; }
    }

    public class Answer
    {
        public string qId { get; set; }
        public List<An> ans { get; set; }
    }

    public class CustomData
    {
        public string CustomerSegment { get; set; }
        public string mobile { get; set; }
        public string Name { get; set; }
        public string TIN { get; set; }
    }

    public class Date
    {
        public string start { get; set; }
        public string end { get; set; }
    }

    public class ResponseArr
    {
        public CustomData customData { get; set; }
        public List<Answer> answers { get; set; }
        public Date date { get; set; }
    }

    public class VocAddSurveyAnswerModel
    {
        public string surveyId { get; set; }
        public string collectId { get; set; }
        public List<ResponseArr> responseArr { get; set; }
    }
}
