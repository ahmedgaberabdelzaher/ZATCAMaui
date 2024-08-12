using System.Collections.ObjectModel;
using Foundation;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
    [Preserve(AllMembers = true)]
    public class ZakatExemptionModel
    {
        [JsonProperty("result")]
        public ZakatExeInitRequestResponse d { get; set; }

        [JsonProperty("data")]
        public ZakatExeInitRequestResponse data { get; set; }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Metadata
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("uri")]
            public string Uri { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class LItemSet
        {
            [JsonProperty("results")]
            public List<object> Results { get; set; }
        }

        public class OffNotesSet
        {
            [JsonProperty("results")]
            public List<OffNotesSetResult> Results { get; set; }
        }

        public class AttDetSet
        {
            [JsonProperty("results")]
            public List<AttachmentSetResult> Results { get; set; }
        }

        public class OffNotesSetResult
        {

            public Metadata Metadata { get; set; }

            //[JsonProperty("noteNumber")]
            public string Notenoz { get; set; }

            [JsonProperty("referenceName")]
            public string Refnamez { get; set; }

            [JsonProperty("displayOnAssessment")]
            public string XInvoicez { get; set; }

            [JsonProperty("completed")]
            public string XObsoletez { get; set; }

            [JsonProperty("processingReason")]
            public string Rcodez { get; set; }

            [JsonProperty("userName")]
            public string Erfusrz { get; set; }

            [JsonProperty("entryDate")]
            public string Erfdtz { get; set; }

            [JsonProperty("createdAt")]
            public string Erftmz { get; set; }

            [JsonProperty("attachedByPerson")]
            public string AttByz { get; set; }

            [JsonProperty("portalUser")]
            public string ByPusrz { get; set; }

            [JsonProperty("TIN")]
            public string ByGpartz { get; set; }

            [JsonProperty("dataVersion")]
            public string DataVersionz { get; set; }

            [JsonProperty("name")]
            public string Namez { get; set; }

            [JsonProperty("noteNumber")]
            public string Noteno { get; set; }

            [JsonProperty("lineNumber")]
            public int Lineno { get; set; }

            [JsonProperty("elementNumber")]
            public int ElemNo { get; set; }

            [JsonProperty("notesFormat")]
            public string Tdformat { get; set; }

            [JsonProperty("notesLine")]
            public string Tdline { get; set; }

            [JsonProperty("section")]
            public string Sect { get; set; }

            [JsonProperty("startDate")]
            public string Strdt { get; set; }

            [JsonProperty("startTime")]
            public string Strtime { get; set; }

            [JsonProperty("notesDescription")]
            public string Strline { get; set; }
        }

        public class AttachmentSetResult
        {

            [JsonProperty("returnGUID")]
            public string RetGuid { get; set; }

            [JsonProperty("sequenceNumber")]
            public string Seqno { get; set; }

            [JsonProperty("formGUID")]
            public string SchGuid { get; set; }

            [JsonProperty("documentCategory")]
            public string Dotyp { get; set; }

            [JsonProperty("serialNumber")]
            public int Srno { get; set; }

            [JsonProperty("documentId")]
            public string Doguid { get; set; }

            [JsonProperty("attachedByPerson")]
            public string AttBy { get; set; }

            [JsonProperty("fileName")]
            public string Filename { get; set; }

            [JsonProperty("fileExtension")]
            public string FileExtn { get; set; }

            [JsonProperty("MIMEType")]
            public string Mimetype { get; set; }

            [JsonProperty("portalUser")]
            public string ByPusr { get; set; }

            [JsonProperty("entryDate")]
            public string Erfdt { get; set; }

            [JsonProperty("createdAt")]
            public string Erftm { get; set; }

            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }

            [JsonProperty("documentURL")]
            public string DocUrl { get; set; }

            [JsonProperty("outletReference")]
            public string OutletRef { get; set; }

            [JsonProperty("fileSize")]
            public string ZfileSize { get; set; }

            [JsonProperty("enableEdit")]
            public string Enbedit { get; set; }

            [JsonProperty("enableDelete")]
            public string Enbdele { get; set; }

            [JsonProperty("visibleEdit")]
            public string Visedit { get; set; }

            [JsonProperty("visibleDelete")]
            public string Visdel { get; set; }

            [JsonProperty("attachmentName")]
            public string AttachNm { get; set; }


        }

        public class Result
        {
            [JsonProperty("__metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("Gpart")]
            public string Gpart { get; set; }

            [JsonProperty("Persl")]
            public string Persl { get; set; }

            [JsonProperty("PerslTxt")]
            public string PerslTxt { get; set; }

            [JsonProperty("EntType")]
            public string EntType { get; set; }

            [JsonProperty("EntText")]
            public string EntText { get; set; }

            [JsonProperty("systemCode")]
            public string Mandt { get; set; }

            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }

            [JsonProperty("formBundleStatus")]
            public string Fbust { get; set; }

            [JsonProperty("button")]
            public string Button { get; set; }

            [JsonProperty("transactionType")]
            public string TransactionType { get; set; }

            [JsonProperty("userType")]
            public string UserTyp { get; set; }
        }

        public class PeriodKeySetResult
        {
            [JsonProperty("__metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("TIN")]
            public string Gpart { get; set; }

            [JsonProperty("periodkey")]
            public string Persl { get; set; }

            [JsonProperty("periodDescription")]
            public string PerslTxt { get; set; }


        }

        public class EntityListSetResult
        {
            [JsonProperty("__metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("entityType")]
            public string EntType { get; set; }

            [JsonProperty("entityDescription")]
            public string EntText { get; set; }


        }

        public class EntityCategories
        {
            public string entityType { get; set; }
            public string technicalNumber { get; set; }
            public string name { get; set; }
            public string flag { get; set; }

        }

        public class EntityAttachments
        {
            public string formBundleType { get; set; }
            public string documentCategory { get; set; }
            public string entityType { get; set; }
            public string technicalNumber { get; set; }
            public string name { get; set; }
            public string attachmentType { get; set; }
        }

            public class PeriodKeySet
            {
                [JsonProperty("results")]
                public List<PeriodKeySetResult> Results { get; set; }
            }

            public class EntityListSet
            {
                [JsonProperty("results")]
                public List<EntityListSetResult> Results { get; set; }
            }

            public class ZerqBtnSet
            {
                [JsonProperty("results")]
                public List<Result> Results { get; set; }
            }

            public class Deferred
            {
                [JsonProperty("uri")]
                public string Uri { get; set; }
            }

            public class Recom02Set
            {
                [JsonProperty("__deferred")]
                public Deferred Deferred { get; set; }
            }

            public class Recom01Set
            {
                [JsonProperty("__deferred")]
                public Deferred Deferred { get; set; }
            }

            public class DecisionSet
            {
                [JsonProperty("__deferred")]
                public Deferred Deferred { get; set; }
            }

            public class ZakatExeInitRequestResponse
            {
                public Metadata Metadata { get; set; }


                // [JsonProperty("declaration")]
                public string Decfg { get; set; }

                [JsonProperty("createdAt")]
                public string Erftime { get; set; }

                [JsonProperty("recommendation01")]
                public string Recom01 { get; set; }

                [JsonProperty("declaration")]
                public string SuDecfg { get; set; }

                [JsonProperty("formBundleType")]
                public string Fbtypz { get; set; }

                [JsonProperty("recommendation02")]
                public string Recom02 { get; set; }

                [JsonProperty("formBundleStatus")]
                public string Fbustz { get; set; }

                [JsonProperty("systemCode")]
                public string Mandtz { get; set; }

                [JsonProperty("decision")]
                public string Decision { get; set; }

                [JsonProperty("transactionType")]
                public string TransactionTypez { get; set; }

                [JsonProperty("userType")]
                public string UserTyp { get; set; }

                [JsonProperty("changedAt")]
                public string Aentime { get; set; }

                [JsonProperty("edit")]
                public string EditFgz { get; set; }

                [JsonProperty("Fbnumz")]
                public string Fbnumz { get; set; }

                [JsonProperty("Mandt")]
                public string Mandt { get; set; }

                [JsonProperty("portalUser")]
                public string PortalUsrz { get; set; }

                [JsonProperty("formGUID")]
                public string FormGuid { get; set; }

                [JsonProperty("language")]
                public string Langz { get; set; }

                [JsonProperty("dataVersion")]
                public string DataVersion { get; set; }

                [JsonProperty("operation")]
                public string Operationz { get; set; }

                [JsonProperty("stepNumber")]
                public string StepNumberz { get; set; }

                [JsonProperty("TIN")]
                public string Tin { get; set; }

                [JsonProperty("returnId")]
                public string ReturnIdz { get; set; }

                [JsonProperty("TINName")]
                public string TinName { get; set; }

                [JsonProperty("CRNumber")]
                public string CrNumber { get; set; }

                [JsonProperty("userName")]
                public string Officerz { get; set; }

                [JsonProperty("formBundleNumber")]
                public string Fbnum { get; set; }

                [JsonProperty("Gpartz")]
                public string Gpartz { get; set; }

                [JsonProperty("periodKey")]
                public string Persl { get; set; }

                [JsonProperty("statusCode")]
                public string Statusz { get; set; }

                [JsonProperty("activityName")]
                public string Actnm { get; set; }

                [JsonProperty("UserTypz")]
                public string UserTypz { get; set; }

                [JsonProperty("mobile")]
                public string Mobile { get; set; }

                //[JsonProperty("transactionType")]
                public string TxnTpz { get; set; }

                [JsonProperty("email")]
                public string Email { get; set; }

                [JsonProperty("formProcess")]
                public string Formprocz { get; set; }

                [JsonProperty("inputChannel")]
                public string Inpch { get; set; }

                [JsonProperty("OfficerTz")]
                public string OfficerTz { get; set; }

                [JsonProperty("createdBy")]
                public string Erfuser { get; set; }

                [JsonProperty("sourceApplication")]
                public string SrcAppz { get; set; }

                [JsonProperty("changedBy")]
                public string Aenuser { get; set; }

                [JsonProperty("channel")]
                public string InChannelz { get; set; }

                [JsonProperty("formBundleGUID")]
                public string Fbguid { get; set; }

                [JsonProperty("authenticationUser")]
                public string Euser { get; set; }

                [JsonProperty("entityType")]
                public string EnType { get; set; }

                [JsonProperty("typeOfTrust")]
                public string TypTrust { get; set; }

                [JsonProperty("entityNature")]
                public string EnNature { get; set; }

                [JsonProperty("offSpringPercentage")]
                public string OffSprP { get; set; }

                [JsonProperty("charityPercentage")]
                public string CharityP { get; set; }

                [JsonProperty("lastFormBundleNumber")]
                public string LastAFbnum { get; set; }

                // [JsonProperty("returnId")]
                public string ReturnId { get; set; }

                [JsonProperty("lastDate")]
                public string LastADate { get; set; }

                [JsonProperty("lastDueAmount")]
                public string LastADueAmt { get; set; }

                [JsonProperty("lastPaidAmount")]
                public string LastAPaidAmt { get; set; }

                public bool OpenPageOnEdit { get; set; } = true;

                [JsonProperty("CRDate")]
                public string CrDate { get; set; }

                [JsonProperty("commencementDate")]
                public string CommDate { get; set; }

                [JsonProperty("createdOn")]
                public string Erfdate { get; set; }

                [JsonProperty("changedOn")]
                public string Aendate { get; set; }

                [JsonProperty("items")]
                public List<object> LItemSet { get; set; }

                [JsonProperty("offNotes")]
                public List<OffNotesSetResult> OffNotesSet { get; set; }

                [JsonProperty("attachments")]
                public List<AttachmentSetResult> AttDetSet { get; set; }

                [JsonProperty("periodKeys")]
                public List<PeriodKeySetResult> PeriodKeySet { get; set; }

                [JsonProperty("entities")]
                public List<EntityListSetResult> EntityListSet { get; set; }

                [JsonProperty("requestButtons")]
                public List<Result> ZerqBtnSet { get; set; }

                [JsonProperty("recommendations02")]
                public List<Recom02Set> Recom02Set { get; set; }

                [JsonProperty("recommendations01")]
                public List<Recom01Set> Recom01Set { get; set; }

                [JsonProperty("decisions")]
                public List<DecisionSet> DecisionSet { get; set; }
                public string CR6774 { get; set; }
                public List<EntityCategories> entityCategories { get; set; }
                public List<EntityAttachments> entityAttachments { get; set; }
            }

            public class AttachmentModel
            {
                public string Title { get; set; }
                public ObservableCollection<Attachment> AttachmentsListViewData { get; set; }
                //public List<EntityAttachments> SelectedAttachments { get; set; }
            }

        }
    }
