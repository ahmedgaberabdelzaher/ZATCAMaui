using System;
namespace EGAZT.Models.SurveyModels
{
    public class AddSurveyBody
    {
        public long tin { get; set; }
        public int scheduleid { get; set; }
        public bool dismiss { get; set; }
    }
}
