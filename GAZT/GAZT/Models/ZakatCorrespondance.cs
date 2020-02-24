using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    public class Correspondance
    {
    }

    public class CorrespondenceMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class CorrespondenceResult
    {
        public CorrespondenceMetadata __metadata { get; set; }
        public string Pdfurl { get; set; }
        public string Zzfav { get; set; }
        public string UserTin { get; set; }
        public string TaxtpFg { get; set; }
        public string Fbtyp { get; set; }
        public string Auditor { get; set; }
        public string Gpartz { get; set; }
        public string Langz { get; set; }
        public DateTime Begdaz { get; set; }
        public DateTime Enddaz { get; set; }
        public string ObligFlagz { get; set; }
        public DateTime Cdate { get; set; }
        public string Ctime { get; set; }
        public string Formkey { get; set; }
        public string Descript { get; set; }
        public string CorrNum { get; set; }
        public string Cokey { get; set; }
        public string Cotyp { get; set; }
        public string Cotxt { get; set; }
        public string Gpart { get; set; }
        public string Vkont { get; set; }
        public string Vtref { get; set; }
        public DateTime Copri { get; set; }
        public DateTime Coidt { get; set; }
        public string Coitm { get; set; }
        public string Fbnum { get; set; }
        public string CaseId { get; set; }
        public string LetterNum { get; set; }
    }

    public class CorrespondenceD
    {
        public List<CorrespondenceResult> results { get; set; }
    }

    public class CorrespondenceRootObject
    {
        public CorrespondenceD d { get; set; }
    }
}
