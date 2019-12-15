using System;
using System.Collections.Generic;

namespace GAZT.Models
{

    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class ZakatSet
    {
        public List<object> results { get; set; }
    }

    public class Metadata2
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result
    {
        public Metadata2 __metadata { get; set; }
        public string Ctime { get; set; }
        public string Coitm { get; set; }
        public string Gpartz { get; set; }
        public string Langz { get; set; }
        public string ObligFlagz { get; set; }
        public string TaxtpFgz { get; set; }
        public string UserTinz { get; set; }
        public string Formkey { get; set; }
        public string Descript { get; set; }
        public string CorrNum { get; set; }
        public string Cokey { get; set; }
        public string Cotyp { get; set; }
        public string Cotxt { get; set; }
        public string Gpart { get; set; }
        public string Vkont { get; set; }
        public string Vtref { get; set; }
        public string Fbnum { get; set; }
        public string CaseId { get; set; }
        public string LetterNum { get; set; }
        public string PrcBy { get; set; }
        public string Fbtyp { get; set; }
        public string Auditor { get; set; }
        public string TaxtpFg { get; set; }
        public string UserTin { get; set; }
        public string Pdfurl { get; set; }
        public DateTime Cdate { get; set; }
        public DateTime Copri { get; set; }
        public DateTime Coidt { get; set; }
    }

    public class VATSet
    {
        public List<Result> results { get; set; }
    }

    public class Metadata3
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result2
    {
        public Metadata3 __metadata { get; set; }
        public string Ctime { get; set; }
        public string Coitm { get; set; }
        public string Gpartz { get; set; }
        public string Langz { get; set; }
        public string ObligFlagz { get; set; }
        public string TaxtpFgz { get; set; }
        public string UserTinz { get; set; }
        public string Formkey { get; set; }
        public string Descript { get; set; }
        public string CorrNum { get; set; }
        public string Cokey { get; set; }
        public string Cotyp { get; set; }
        public string Cotxt { get; set; }
        public string Gpart { get; set; }
        public string Vkont { get; set; }
        public string Vtref { get; set; }
        public string Fbnum { get; set; }
        public string CaseId { get; set; }
        public string LetterNum { get; set; }
        public string PrcBy { get; set; }
        public string Fbtyp { get; set; }
        public string Auditor { get; set; }
        public string TaxtpFg { get; set; }
        public string UserTin { get; set; }
        public string Pdfurl { get; set; }
        public DateTime Cdate { get; set; }
        public DateTime Copri { get; set; }
        public DateTime Coidt { get; set; }
    }

    public class ExciseSet
    {
        public List<Result2> results { get; set; }
    }

    public class AllCertificate
    {
        public Metadata __metadata { get; set; }
        public string Gpartz { get; set; }
        public string Langz { get; set; }
        public DateTime Begdaz { get; set; }
        public DateTime Enddaz { get; set; }
        public ZakatSet ZakatSet { get; set; }
        public VATSet VATSet { get; set; }
        public ExciseSet ExciseSet { get; set; }
    }

    //public class AllCertificate
    //{
    //    public D d { get; set; }
    //}

    //public class Metadata
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }
    //}

    //public class Metadata2
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }
    //}

    //public class Result
    //{
    //    public Metadata2 __metadata { get; set; }
    //    public string Ctime { get; set; }
    //    public string Coitm { get; set; }
    //    public string Gpartz { get; set; }
    //    public string Langz { get; set; }
    //    public string ObligFlagz { get; set; }
    //    public string TaxtpFgz { get; set; }
    //    public string UserTinz { get; set; }
    //    public string Formkey { get; set; }
    //    public string Descript { get; set; }
    //    public string CorrNum { get; set; }
    //    public string Cokey { get; set; }
    //    public string Cotyp { get; set; }
    //    public string Cotxt { get; set; }
    //    public string Gpart { get; set; }
    //    public string Vkont { get; set; }
    //    public string Vtref { get; set; }
    //    public string Fbnum { get; set; }
    //    public string CaseId { get; set; }
    //    public string LetterNum { get; set; }
    //    public string PrcBy { get; set; }
    //    public string Fbtyp { get; set; }
    //    public string Auditor { get; set; }
    //    public string TaxtpFg { get; set; }
    //    public string UserTin { get; set; }
    //    public string Pdfurl { get; set; }
    //    public DateTime Cdate { get; set; }
    //    public DateTime Copri { get; set; }
    //    public DateTime Coidt { get; set; }
    //}

    //public class ZakatSet
    //{
    //    public List<Result> results { get; set; }
    //}

    //public class Metadata3
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }
    //}

    //public class Result2
    //{
    //    public Metadata3 __metadata { get; set; }
    //    public string Ctime { get; set; }
    //    public string Coitm { get; set; }
    //    public string Gpartz { get; set; }
    //    public string Langz { get; set; }
    //    public string ObligFlagz { get; set; }
    //    public string TaxtpFgz { get; set; }
    //    public string UserTinz { get; set; }
    //    public string Formkey { get; set; }
    //    public string Descript { get; set; }
    //    public string CorrNum { get; set; }
    //    public string Cokey { get; set; }
    //    public string Cotyp { get; set; }
    //    public string Cotxt { get; set; }
    //    public string Gpart { get; set; }
    //    public string Vkont { get; set; }
    //    public string Vtref { get; set; }
    //    public string Fbnum { get; set; }
    //    public string CaseId { get; set; }
    //    public string LetterNum { get; set; }
    //    public string PrcBy { get; set; }
    //    public string Fbtyp { get; set; }
    //    public string Auditor { get; set; }
    //    public string TaxtpFg { get; set; }
    //    public string UserTin { get; set; }
    //    public string Pdfurl { get; set; }
    //    public DateTime Cdate { get; set; }
    //    public DateTime Copri { get; set; }
    //    public DateTime Coidt { get; set; }
    //}

    //public class VATSet
    //{
    //    public List<Result2> results { get; set; }
    //}

    //public class ExciseSet
    //{
    //    public List<object> results { get; set; }
    //}

    //public class D
    //{
    //    public Metadata __metadata { get; set; }
    //    public string Gpartz { get; set; }
    //    public string Langz { get; set; }
    //    public DateTime Begdaz { get; set; }
    //    public DateTime Enddaz { get; set; }
    //    public ZakatSet ZakatSet { get; set; }
    //    public VATSet VATSet { get; set; }
    //    public ExciseSet ExciseSet { get; set; }
    //}


}
