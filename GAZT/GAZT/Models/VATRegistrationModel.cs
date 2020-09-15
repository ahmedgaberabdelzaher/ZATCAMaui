using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.Models
{
    public class __metadata
{
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
}



public class ResultsItem
{
        public __metadata __metadata { get; set; }
        public string Addrnumber { get; set; }
        public string City { get; set; }
        public string Quarter { get; set; }
        public string PostalCd { get; set; }
        public string Street { get; set; }
        public string AdditionalNo { get; set; }
        public string BuildingNo { get; set; }
        public string Region { get; set; }
        public string RegionDesc { get; set; }
}

public class ADDRESSSet
{
        public List <ResultsItem> results { get; set; }
}

//public class NOTESSet
//{
//        public List <string> results { get; set; }
//}



public class ResultsItemForContact
{
        public __metadata __metadata { get; set; }
        public string TransactionType { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Srcidentify { get; set; }
        public string Consnumber { get; set; }
        public string Begda { get; set; }
        public string Endda { get; set; }
        public string TelNumber { get; set; }
        public string R3User { get; set; }
        public string SmtpAddr { get; set; }
        public string MobNumber { get; set; }
}

public class CONTACTDTSet
{
        public List <ResultsItemForContact> results { get; set; }
}





public class ResultsItemForContactPerson
{
        public __metadata __metadata { get; set; }
        public string TransactionType { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Srcidentify { get; set; }
        public string Gpart { get; set; }
        public string Enddt { get; set; }
        public string Contacttp { get; set; }
        public bool Defaultfg { get; set; }
        public string Startdt { get; set; }
        public string Firstnm { get; set; }
        public string Lastnm { get; set; }
        public string Relationtp { get; set; }
        public string Fathernm { get; set; }
        public string Grandfathernm { get; set; }
        public string Familynm { get; set; }
        public string Dobdt { get; set; }
        public string StartdtC { get; set; }
        public string Type { get; set; }
        public string Idnumber { get; set; }
        public string Title { get; set; }
        public string Initials { get; set; }
}

public class CONTACT_PERSONSet
{
        public List <ResultsItemForContactPerson> results { get; set; }
}



public class ResultsItemForQuestion
{
        public __metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ResidencyTy { get; set; }
        public string QueNo { get; set; }
        public string QoptNo { get; set; }
        public string QoptTxt { get; set; }
        public string QoptAns { get; set; }
}

    public class ResultsItemForElgblDocSet
    {
        public __metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Fbtyp { get; set; }
        public string TxnTp { get; set; }
        public string DmsTp { get; set; }
        public string DmsTxt { get; set; }
        public string Txt50 { get; set; }
    }
    public class ResultsItemForDOCSetforsubmit
    {
   //     public __metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Fbtyp { get; set; }
        public string TxnTp { get; set; }
        public string DmsTp { get; set; }
        public string DmsTxt { get; set; }
        //public string Txt50 { get; set; }
    }

    public class QUESTIONSSet
{
        public List <ResultsItemForQuestion> results { get; set; }
}


    public class ResultsForATTDETSet
    {
        public __metadata __metadata { get; set; }
        public string RetGuid { get; set; }
        public string Seqno { get; set; }
        public string SchGuid { get; set; }
        public string Dotyp { get; set; }
        public int Srno { get; set; }
        public string Doguid { get; set; }
        public string AttBy { get; set; }
        public string Filename { get; set; }
        public string FileExtn { get; set; }
        public string Mimetype { get; set; }
        public string ByPusr { get; set; }
        public string Erfdt { get; set; }
        public string Erftm { get; set; }
        public string DataVersion { get; set; }
        public string DocUrl { get; set; }
        public string OutletRef { get; set; }
        public string Enbedit { get; set; }
        public string Enbdele { get; set; }
        public string Visedit { get; set; }
        public string Visdel { get; set; }

    }

    public class ATTDETSet
{
        public List <Attachment> results { get; set; }
}



    public class QUESLISTSet
    {
        public List<string> results { get; set; }
    }

    public class vATRegistration
{
        public __metadata __metadata { get; set; }
        public string SmartReg { get; set; }
        public string Source { get; set; }
        public string AgrFg { get; set; }
        public string Decname { get; set; }
        public string ConfTaxDt { get; set; }
        public string CrNm { get; set; }
        public string CrNo { get; set; }
        public string CrStdt { get; set; }
        public string DataVersion { get; set; }
        public string Decconno { get; set; }
        public string Decdate { get; set; }
        public string Decdesignation { get; set; }
        public string Decfg { get; set; }
        public string DecidNo { get; set; }
        public string DecidTy { get; set; }
        public string Euser { get; set; }
        public string ExAttch { get; set; }
        public string ExFg { get; set; }
        public string Fbguid { get; set; }
        public string Fbnumz { get; set; }
        public string FormGuid { get; set; }
        public string Formprocz { get; set; }
        public string FutureDt { get; set; }
        public string GlobalCalTy { get; set; }
        public string GoLiveDt { get; set; }
        public string Gpartz { get; set; }
        public string Iban { get; set; }
        public string ImAttch { get; set; }
        public string ImFg { get; set; }
        public string Langz { get; set; }
        public string Mandt { get; set; }
        public string NewRegTy { get; set; }
        public string NewRegTyFrDt { get; set; }
        public string Officerz { get; set; }
        public string Operationz { get; set; }
        public string OptIban { get; set; }
        public string PortalUsrz { get; set; }
        public string ReaFg { get; set; }
        public string Reason { get; set; }
        public string RegTy { get; set; }
        public string ResidencyTy { get; set; }
        public string ReturnIdz { get; set; }
        public string Statusz { get; set; }
        public string StepNumberz { get; set; }
        public string Stp2Cbbox { get; set; }
        public string Stp3Cbbox { get; set; }
        public string Stp4Cbbox1 { get; set; }
        public string Stp4Cbbox2 { get; set; }
        public string TinNm { get; set; }
        public string ToSflg { get; set; }
        public string TxnTpz { get; set; }
        public string UserTypz { get; set; }
        public string VatDt { get; set; }
        public string VatTaxDt { get; set; }
        public ADDRESSSet ADDRESSSet { get; set; }
        public NOTESSet NOTESSet { get; set; }
        public CONTACTDTSet CONTACTDTSet { get; set; }
        public ELGBL_DOCSetforsubmit ELGBL_DOCSet { get; set; }
        public CONTACT_PERSONSet CONTACT_PERSONSet { get; set; }
        public QUESTIONSSet QUESTIONSSet { get; set; }
        public QUESCONFIG_MSet QUESCONFIG_MSet { get; set; }
        public ATTDETSet ATTDETSet { get; set; }
        public IBANSet IBANSet { get; set; }
        public QUESLISTSet QUESLISTSet { get; set; }
}
    public class QUESCONFIG_MSet
    {
        public IList<QuestionsetWithMinMax> results { get; set; }

    }

    public class QuestionsetWithMinMax
    {
        public __metadata __metadata { get; set; }
        public string FormGuid { get; set; }
        public string Gpart { get; set; }
        public string DataVersion { get; set; }
        public string ResidencyTy { get; set; }
        public int LineNo { get; set; }
        public string QoptNo { get; set; }
        public string QoptTxt { get; set; }
        public string RankingOrder { get; set; }
        public string QoptAns { get; set; }
        public string QueNo { get; set; }
        public string Fbnum { get; set; }
        public string Minvalue { get; set; }
        public string Maxvalue { get; set; }

    }

    public class VATRegistrationDetails
{
        public vATRegistration d { get; set; }
}
    public enum IsComeFromForAttachment
    {
       Import=0,
       Export=1,
       General=3,
       FinancialReprsentative=4
    }

    public class QuestionNumberWithMinMaxRange
    {
        public string QueNo = String.Empty;
        public double MinRangeValue = -1;
        public double MaxRangeValue = -1;
        public int CountOfProbableAnswersForThisQuestions = -1;
    }
}
