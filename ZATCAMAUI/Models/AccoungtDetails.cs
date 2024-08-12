using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Collections.Generic;
using Foundation;

namespace ZATCAMAUI.Models.AccountDetails
{
    [Preserve(AllMembers = true)]
    public class AccoungtDetails
    {
        [JsonProperty("data")]
        public D d { get; set; }
        [Preserve(AllMembers = true)]
        public class D
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("documentNumber")]
            public string Opbel { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("returnDetails")]
            public List<Result_RET> RET_DTLSet { get; set; }
            [JsonProperty("billsDetails")]
            public List<Result_Bill> BILL_DTLSet { get; set; }
            [JsonProperty("installmentDetails")]
            public List<Result_InST> INSTL_DTLSet { get; set; }
            [JsonProperty("objectionDetails")]
            public List<Result_Obj> OBJ_DTLSet { get; set; }
    
        }
        [Preserve(AllMembers = true)]
        public class RETDTLSet
        {
            [JsonProperty("returnDetails")]
            public List<Result_RET> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class INSTLDTLSet
        {
            [JsonProperty("installmentDetails")]
            public List<Result_InST> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class OBJDTLSet
        {
            [JsonProperty("objectionDetails")]
            public List<Result_Obj> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class BILLDTLSet
        {
            [JsonProperty("billsDetails")]
            public List<Result_Bill> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Metadata
        {
            [JsonProperty("id")]
            public string id { get; set; }
            [JsonProperty("uri")]
            public string uri { get; set; }
            [JsonProperty("type")]
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Result_InST
        {
            //Instalment DTL SET

            public Metadata __metadata { get; set; }
            [JsonProperty("caseStatus")]
            public string Status { get; set; }
            [JsonProperty("dueAmount")]
            public string DueAmt { get; set; }
            [JsonProperty("installmentAmount")]
            public string InstAmt { get; set; }
            [JsonProperty("numberOfInstallments")]
            public string NoOfInst { get; set; }
            [JsonProperty("downPaymentAmount")]
            public string DpAmt { get; set; }

            [JsonIgnore]
            public string SubmissionDate { get; set; }

            [JsonIgnore]
            public string _SubmissionDt;
            [JsonProperty("submissionDate")]
            public string SubmissionDt {
                get {
                    return _SubmissionDt;
                }
                set {
                    _SubmissionDt = value;
                    if(_SubmissionDt != null)
                    {
                        SubmissionDate = _SubmissionDt; 
                    }
                }
            }
            [JsonProperty("requestNumber")]
            public string ReqNum { get; set; }

           
            
        }
        [Preserve(AllMembers = true)]
        public class Result_Obj
        {

            //Objection DTL SET
           
            public Metadata __metadata { get; set; }
            [JsonProperty("objectionFormBundleNumber")]
            public string ObjFbnum { get; set; }
            [JsonProperty("returnDocumentNumber")]
            public string RetOpbel { get; set; }
            [JsonProperty("disputedAmount")]
            public string ObjDispAmt { get; set; }
            [JsonProperty("returnFormBundleNumber")]
            public string RetFbnum { get; set; }
            [JsonProperty("returnFormBundleType")]
            public string RetFbtypTxt { get; set; }
        }

        [Preserve(AllMembers = true)]
        public class Result_Bill
        {
            // BillDTL SET
            
            public Metadata __metadata { get; set; }
            [JsonProperty("serialNumber")]
            public string SrNo { get; set; }
            [JsonProperty("billNumber")]
            public string BillNo { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }

            [JsonProperty("paymentStatus")]
            public string PymtStatus { get; set; }

            [JsonIgnore]
            public string OutTime { get; set; }
            [JsonIgnore]
            public string Intime { get; set; }

            [JsonIgnore]
            public string _Abrzu;
            [JsonProperty("billEndDate")]
            public string Abrzu { get
                {
                    return _Abrzu;
                } set {
                    _Abrzu = value;
                    if(_Abrzu != null)
                    {
                        OutTime = _Abrzu;
                    }
                }
            }

            [JsonIgnore]
            private string _Abrzo;
            [JsonProperty("billStartDate")]
            public string Abrzo { get
                {
                    return _Abrzo;
                }
                set
                {
                    _Abrzo = value;
                    if(_Abrzo != null)
                    {
                        Intime = OutTime+ " - " +_Abrzo;
                    }
                }
            }

           

            [JsonIgnore]
            public string PeriodTime { get; set; }

            [JsonProperty("periodKey")]
            public string Period { get; set; }

            [JsonProperty("amount")]
            public string Betrw { get; set; }
            [JsonProperty("taxType")]
            public string TaxType { get; set; }
            [JsonProperty("billDescription")]
            public string BillDesc { get; set; }

        }
        [Preserve(AllMembers = true)]
        public class Result_RET
        {
           // [JsonProperty("__metadata")]
            public Metadata __metadata { get; set; }
            [JsonProperty("formBundleNumber")]
            public string RetFbnum { get; set; }
            [JsonProperty("returnRevenueType")]
            public string RetRevType { get; set; }
            [JsonProperty("returnFormBundleType")]
            public string RetFbtypTxt { get; set; }

            [JsonIgnore]
            public string FormattedBldat { get; set; }

            [JsonIgnore]
            public string _RetSubDt;
            [JsonProperty("returnSubDate")]
            public string RetSubDt
            {
                get
                {
                    return _RetSubDt;
                }
                set
                {
                    _RetSubDt = value;
                    if (_RetSubDt != null)
                    {
                        FormattedBldat = _RetSubDt;

                        //FormattedBldat = _RetSubDt?.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                    }
                }
            }

        }
    }
}

