using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class Correspondance
    {
    }

    [Preserve(AllMembers = true)]
    public class CorrespondenceMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    [Preserve(AllMembers = true)]
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
        public string Begdaz { get; set; }
        public string Enddaz { get; set; }
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
        public string Copri { get; set; }
        public string Coidt { get; set; }
        public string Coitm { get; set; }
        public string Fbnum { get; set; }
        public string CaseId { get; set; }
        public string LetterNum { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class CorrespondenceD
    {
        public List<CorrespondenceResult> results { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class CorrespondenceRootObject
    {
        public CorrespondenceD d { get; set; }
    }
}
