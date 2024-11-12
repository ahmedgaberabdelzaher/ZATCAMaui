using System;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.TINOutletDeregister
{
    public class TinOutletDeregisterListModel
    {
        [JsonProperty("data")]
        public OutletDeregisterListResponse D { get; set; }

        [JsonProperty("result")]
        public OutletDeregisterListResponse result { get; set; }

        public class AttDetSet
        {
            [JsonProperty("results")]
            public List<AttachmentSetResult> Results { get; set; }
        }
        public class AttachmentSetResult
        {
            public Metadata Metadata { get; set; }


            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("outletReference")]
            public string OutletRef { get; set; }

            [JsonProperty("documentURL")]
            public string DocUrl { get; set; }

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

            [JsonProperty("entryDate")]
            public string Erfdt { get; set; }

            [JsonProperty("createdAt")]
            public string Erftm { get; set; }
            [JsonProperty("enableEdit")]
            public string Enbedit { get; set; }

            [JsonProperty("enableDelete")]
            public string Enbdele { get; set; }
            [JsonProperty("visibleEdit")]
            public string Visedit { get; set; }

            [JsonProperty("visibleDelete")]
            public string Visdel { get; set; }



        }
        public class OutletDeregisterListResponse
        {
            public Metadata Metadata { get; set; }

            [JsonProperty("inProcess")]
            public string InProcessz { get; set; }

            [JsonProperty("transactionType")]
            public string TransactionTypez { get; set; }

            [JsonProperty("deregistartion")]
            public string BgDregFlg { get; set; }

            [JsonProperty("SEZTaxpayer")]
            public string SezTpFlag { get; set; }

            [JsonProperty("CR2021Popup")]
            public string Cr2021popup { get; set; }

            [JsonProperty("userStatus")]
            public string Fbust { get; set; }

            [JsonProperty("TIN")]
            public string Taxpayerz { get; set; }

            [JsonProperty("userType")]
            public string UserTypz { get; set; }

            [JsonProperty("submit")]
            public string Submitz { get; set; }

            [JsonProperty("save")]
            public string Savez { get; set; }

            [JsonProperty("reject")]
            public string Rejectz { get; set; }

            [JsonProperty("contractNumber")]
            public string RegIdz { get; set; }

            [JsonProperty("periodKey")]
            public string PeriodKeyz { get; set; }

            [JsonProperty("userName")]
            public string OfficerUidz { get; set; }

            [JsonProperty("language")]
            public string Langz { get; set; }

            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }

            [JsonProperty("formBundleNumber")]
            public string Fbnumz { get; set; }

            [JsonProperty("createTaxAssessment")]
            public string CreateTxAssesz { get; set; }

            [JsonProperty("auditor")]
            public string Auditorz { get; set; }

            [JsonProperty("approve")]
            public string Approvez { get; set; }

            [JsonProperty("deregister")]
            public string ADegister { get; set; }

            [JsonProperty("channel")]
            public string Inpchz { get; set; }

            [JsonProperty("assignToMe")]
            public string Assignme { get; set; }

            [JsonProperty("caseId")]
            public string Caseid { get; set; }

            [JsonProperty("void")]
            public string Xvoidz { get; set; }

            [JsonProperty("TINInProcessing")]
            public string TinInPrcFg { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("portalUser")]
            public string PortalUsrz { get; set; }

            [JsonProperty("operation")]
            public string Operation { get; set; }

            [JsonProperty("month")]
            public string Monthz { get; set; }

            [JsonProperty("legacyDocumentNumber")]
            public string LegacyDocNo { get; set; }

            public string Fbnum { get; set; }

            [JsonProperty("display")]
            public string Dflag { get; set; }

            [JsonProperty("change")]
            public string Cflag { get; set; }

            [JsonProperty("caseGUID")]
            public string CaseGuid { get; set; }

            [JsonProperty("transactionTIN")]
            public string ATransTin { get; set; }

            [JsonProperty("title")]
            public string ATitle { get; set; }

            [JsonProperty("TINType")]
            public string ATinType { get; set; }

            public string ATin { get; set; }

            [JsonProperty("taxpayerName")]
            public string ATaxpayerName { get; set; }

            [JsonProperty("submissionDateHijri")]
            public string ASubmissionDateH { get; set; }

            [JsonProperty("submissionDateCalendar")]
            public string ASubmissionDateC { get; set; }

            [JsonProperty("submissionDate")]
            public string ASubmissionDate { get; set; }

            [JsonProperty("step")]
            public string AStep { get; set; }

            [JsonProperty("officialUseOrigin")]
            public string AOffOrigin { get; set; }

            [JsonProperty("officialApplicationNumber")]
            public string AOffAppNo { get; set; }

            [JsonProperty("firstName7")]
            public string ANm7 { get; set; }

            [JsonProperty("firstName6")]
            public string ANm6 { get; set; }

            [JsonProperty("firstName5")]
            public string ANm5 { get; set; }

            [JsonProperty("firstName4")]
            public string ANm4 { get; set; }

            [JsonProperty("firstName3")]
            public string ANm3 { get; set; }

            [JsonProperty("firstName2")]
            public string ANm2 { get; set; }

            [JsonProperty("firstName1")]
            public string ANm1 { get; set; }

            [JsonProperty("amendmentReason")]
            public string AmdRsnz { get; set; }

            [JsonProperty("idType")]
            public string AIdType { get; set; }

            [JsonProperty("idNumber")]
            public string AIdNo { get; set; }

            [JsonProperty("formStatus")]
            public string AFormStatus { get; set; }
            [JsonProperty("expiryDateHijri")]
            public string AExpdtH { get; set; }

            [JsonProperty("expiryDateCalendar")]
            public string AExpdtC { get; set; }

            [JsonProperty("expiryDate")]
            public string AExpdt { get; set; }

            [JsonProperty("effectiveDateHijri")]
            public string AEffectiveDtH { get; set; }

            [JsonProperty("effectiveDateCalendar")]
            public string AEffectiveDtC { get; set; }

            [JsonProperty("effectiveDate")]
            public string AEffectiveDt { get; set; }

            [JsonProperty("deregistrationReason")]
            public string ADregReason { get; set; }

            [JsonProperty("deregistrationOption")]
            public string ADregOpt { get; set; }

            [JsonProperty("document9")]
            public string ADocumnt9 { get; set; }

            [JsonProperty("document8")]
            public string ADocumnt8 { get; set; }

            [JsonProperty("document7")]
            public string ADocumnt7 { get; set; }

            [JsonProperty("document6")]
            public string ADocumnt6 { get; set; }

            [JsonProperty("document5Description")]
            public string ADocumnt5Txt { get; set; }

            [JsonProperty("document5")]
            public string ADocumnt5 { get; set; }

            [JsonProperty("document4Description")]
            public string ADocumnt4Txt { get; set; }

            [JsonProperty("document4")]
            public string ADocumnt4 { get; set; }

            [JsonProperty("document3")]
            public string ADocumnt3 { get; set; }

            [JsonProperty("document2")]
            public string ADocumnt2 { get; set; }

            [JsonProperty("document14")]
            public string ADocumnt14 { get; set; }

            [JsonProperty("document13")]
            public string ADocumnt13 { get; set; }

            [JsonProperty("document12")]
            public string ADocumnt12 { get; set; }

            [JsonProperty("document11")]
            public string ADocumnt11 { get; set; }

            [JsonProperty("document10")]
            public string ADocumnt10 { get; set; }

            [JsonProperty("document1")]
            public string ADocumnt1 { get; set; }
            [JsonProperty("birthDateHijri")]
            public string ADobH { get; set; }

            [JsonProperty("birthDateCalendar")]
            public string ADobC { get; set; }

            [JsonProperty("birthDate")]
            public string ADob { get; set; }

            [JsonProperty("declarationTitle")]
            public string ADecTitle { get; set; }

            [JsonProperty("declarationTelephoneNumber")]
            public string ADecTelNo { get; set; }

            [JsonProperty("declarationName")]
            public string ADecName { get; set; }

            [JsonProperty("declarationCheckbox")]
            public string ADeclarationChkbox { get; set; }

            [JsonProperty("declarationDesignation")]
            public string ADecDesig { get; set; }
            [JsonProperty("declarationDateHijri")]
            public string ADecDateH { get; set; }

            [JsonProperty("declarationDateCalendar")]
            public string ADecDateC { get; set; }

            [JsonProperty("declarationDate")]
            public string ADecDate { get; set; }

            [JsonProperty("date")]
            public string ADateFormat { get; set; }

            [JsonProperty("branchDescription")]
            public string ABranchTxt { get; set; }

            [JsonProperty("partnerType")]
            public string ABpKind { get; set; }

            [JsonProperty("returns")]
            public List<object> ReturnSet { get; set; }

            [JsonProperty("attachments")]
            public List<AttachmentSetResult> AttDetSet { get; set; }

            [JsonProperty("outlets")]
            public List<OutletResult> OutletSet { get; set; }

            [JsonProperty("permits")]
            public List<PermitResult> PermitSet { get; set; }

            [JsonProperty("offNotes")]
            public List<object> OffNotesSet { get; set; }

            [JsonProperty("permitsTable")]
            public List<object> PermitTableSet { get; set; }

            [JsonProperty("errorMessages")]
            public List<ErrMesgs> ErrMsgSet { get; set; }
        }

        public class Deferred
        {
            [JsonProperty("uri")]
            public string Uri { get; set; }
        }

        public class ErrMsgSet
        {
            [JsonProperty("results")]
            public List<ErrMesgs> Results;
        }
        public class ErrMesgs
        {
            [JsonProperty("__metadata")]
            public Metadata Metadata;

            [JsonProperty("systemCode")]
            public string Mandt;

            [JsonProperty("messageId")]
            public string MsgId;

            [JsonProperty("language")]
            public string Spras;

            [JsonProperty("messageDescription")]
            public string MsgText;
        }

        public class Metadata
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("uri")]
            public string Uri { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class OffNotesSet
        {
            [JsonProperty("results")]
            public List<object> Results { get; set; }
        }

        public class OutletSet
        {
            [JsonProperty("results")]
            public List<OutletResult> Results { get; set; }
        }

        public class PermitSet
        {
            [JsonProperty("results")]
            public List<PermitResult> Results { get; set; }
        }

        public class PermitTableSet
        {
            [JsonProperty("results")]
            public List<object> Results { get; set; }
        }

        public class OutletResult
        {
            public Metadata Metadata { get; set; }
            [JsonProperty("status")]
            public string AOutletStatusTb { get; set; }
            [JsonProperty("issueDate")]
            public string AOutletIssueDtTb { get; set; }
            [JsonProperty("owner")]
            public string AOwner { get; set; }
            [JsonProperty("activity")]
            public string AActFlag { get; set; }

            [JsonProperty("inProcessing")]
            public string OutletInPrcFg { get; set; }
            [JsonProperty("type")]
            public string AOutletTypeTb { get; set; }
            [JsonProperty("validToDate")]
            public string AOutletValidToTb { get; set; }

            [JsonProperty("transferTIN")]
            public string AOutletTransTinTb { get; set; }

            [JsonProperty("toDeregister")]
            public string AOutletToDeregTb { get; set; }

            [JsonProperty("title")]
            public string AOutletTitleTb { get; set; }

            [JsonProperty("street2")]
            public string AOutletStreet2Tb { get; set; }

            [JsonProperty("street1")]
            public string AOutletStreet1Tb { get; set; }

            [JsonProperty("quarter")]
            public string AOutletQuarterTb { get; set; }

            [JsonProperty("province")]
            public string AOutletProvinceTb { get; set; }

            [JsonProperty("postalCode")]
            public string AOutletPostalCodeTb { get; set; }

            [JsonProperty("POBox")]
            public string AOutletPoBoxTb { get; set; }

            [JsonProperty("number")]
            public string AOutletNoTb { get; set; }

            [JsonProperty("name7")]
            public string AOutletNm7Tb { get; set; }

            [JsonProperty("name6")]
            public string AOutletNm6Tb { get; set; }

            [JsonProperty("name5")]
            public string AOutletNm5Tb { get; set; }

            [JsonProperty("name4")]
            public string AOutletNm4Tb { get; set; }

            [JsonProperty("name3")]
            public string AOutletNm3Tb { get; set; }

            [JsonProperty("name2")]
            public string AOutletNm2Tb { get; set; }

            [JsonProperty("name1")]
            public string AOutletNm1Tb { get; set; }

            [JsonProperty("newMainNumber")]
            public string AOutletNewMainOutnumTb { get; set; }

            [JsonProperty("name")]
            public string AOutletNameTb { get; set; }

            [JsonProperty("mobileNumber")]
            public string AOutletMobileNoTb { get; set; }

            [JsonProperty("mainOutlet")]
            public string AOutletMainFlagTb { get; set; }

            [JsonProperty("idType")]
            public string AOutletIdTypeTb { get; set; }

            [JsonProperty("idNumber")]
            public string AOutletIdNoTb { get; set; }
            [JsonProperty("identificationNumber")]
            public string AOutletIdentificationNoTb { get; set; }
            [JsonProperty("houseNumber")]
            public string AOutletHouseNoTb { get; set; }
            [JsonProperty("hide")]
            public string AOutletHide { get; set; }
            [JsonProperty("outlet")]
            public string AOutletFlag { get; set; }

            [JsonProperty("expiryDate")]
            public string AOutletExpdtTb { get; set; }
            [JsonProperty("expiryDateHijri")]
            public string AOutletExpdtHTb { get; set; }

            [JsonProperty("expiryDateCalendar")]
            public string AOutletExpdtCTb { get; set; }

            [JsonProperty("email")]
            public string AOutletEmailTb { get; set; }

            [JsonProperty("effectiveDate")]
            public string AOutletEffDtTb { get; set; }
            [JsonProperty("effectiveDateHijri")]
            public string AOutletEffDtHTb { get; set; }

            [JsonProperty("effectiveDateCalendar")]
            public string AOutletEffDtCTb { get; set; }

            [JsonProperty("deregisterOption")]
            public string AOutletDregOptTb { get; set; }

            [JsonProperty("birthDate")]
            public string AOutletDobTb { get; set; }
            [JsonProperty("birthDateHijri")]
            public string AOutletDobHTb { get; set; }

            [JsonProperty("birthDateCalendar")]
            public string AOutletDobCTb { get; set; }
            [JsonProperty("CRNumber")]
            public string AOutletCrNoTb { get; set; }

            [JsonProperty("country")]
            public string AOutletCountryTb { get; set; }

            [JsonProperty("address")]
            public string AOutletCompAddr { get; set; }

            [JsonProperty("outletCommencement")]
            public string AOutletComcdFlag { get; set; }
            [JsonProperty("closeTransferDate")]
            public string AOutletCloseTransferDtTb { get; set; }

            [JsonProperty("city")]
            public string AOutletCityTb { get; set; }

            [JsonProperty("buildingNumber")]
            public string AOutletBuildingNoTb { get; set; }
            [JsonProperty("actionType")]
            public string AOutletActionTypeTb { get; set; }
            [JsonProperty("MOI")]
            public string AOuletMoiFlag { get; set; }

            [JsonProperty("ACompFg")]
            public string ACompFg { get; set; }

            [JsonProperty("AOutletZ700NumberTb")]
            public string AOutletZ700NumberTb { get; set; }

        }

        //
        public class PermitResult
        {
            public Metadata Metadata { get; set; }
            [JsonProperty("issueName")]
            public string APermitIssueName { get; set; }

            [JsonProperty("address")]
            public string APermitAdrFlag { get; set; }
            [JsonProperty("MOI")]
            public string APermitMoiFlag { get; set; }

            [JsonProperty("comboBox")]
            public string APermitCbFlag { get; set; }

            [JsonProperty("TIN")]
            public string APermitTinFlag { get; set; }
            [JsonProperty("permitCommencementControl")]
            public string APermitComcdFlag { get; set; }

            [JsonProperty("newMainIdNumber")]
            public string APermitNewMainNoIdTb { get; set; }

            [JsonProperty("permitCommencement")]
            public string APermitCommFlag { get; set; }

            // [JsonProperty("permitCommencement")]
            public string APermitConFlag { get; set; }

            [JsonProperty("comboBoxControl")]
            public string APermitFrzCb { get; set; }

            [JsonProperty("APermitGovFlag")]
            public string APermitGovFlag { get; set; }
            [JsonProperty("TIN1")]
            public string APermitTinFlag1 { get; set; }

            [JsonProperty("mainActivity")]
            public string APermitMainActFlagTb { get; set; }

            [JsonProperty("mainNumber")]
            public string APermitMainnoTb { get; set; }

            [JsonProperty("name6")]
            public string APermitNm6Tb { get; set; }

            [JsonProperty("number")]
            public string APermitNoTb { get; set; }

            [JsonProperty("typeDescription")]
            public string APermitTypTxt { get; set; }

            [JsonProperty("inProcessing")]
            public string PermitInPrcFg { get; set; }
            [JsonProperty("expiryDateHijri")]
            public string APermitExpdtHTb { get; set; }

            [JsonProperty("mainActivityNumber")]
            public string APermitMainActNoTb { get; set; }

            [JsonProperty("outletNumber")]
            public string APermitOutletnoTb { get; set; }

            [JsonProperty("expiryDateCalendar")]
            public string APermitExpdtCTb { get; set; }

            [JsonProperty("title")]
            public string APermitTitleTb { get; set; }

            [JsonProperty("name5")]
            public string APermitNm5Tb { get; set; }

            [JsonProperty("name7")]
            public string APermitNm7Tb { get; set; }

            [JsonProperty("type")]
            public string APermitTypeTb { get; set; }
            [JsonProperty("validFromDateHijri")]
            public string APermitValfrDtHTb { get; set; }

            [JsonProperty("validFromDateCalendar")]
            public string APermitValfrDtCTb { get; set; }
            [JsonProperty("effectiveDateHijri")]
            public string APermitEffDtHTb { get; set; }

            [JsonProperty("effectiveDateCalendar")]
            public string APermitEffDtCTb { get; set; }

            [JsonProperty("deregistration")]
            public string APermitDregRsnTb { get; set; }

            [JsonProperty("transferTIN")]
            public string APermitTransTinTb { get; set; }

            [JsonProperty("idType")]
            public string APermitIdTypeTb { get; set; }

            [JsonProperty("idNumber")]
            public string APermitIdNoTb { get; set; }

            [JsonProperty("name1")]
            public string APermitNm1Tb { get; set; }

            [JsonProperty("name2")]
            public string APermitNm2Tb { get; set; }

            [JsonProperty("name3")]
            public string APermitNm3Tb { get; set; }

            [JsonProperty("name4")]
            public string APermitNm4Tb { get; set; }
            [JsonProperty("birthDatHijri")]
            public string APermitDobHTb { get; set; }

            [JsonProperty("birthDateCalendar")]
            public string APermitDobCTb { get; set; }
            [JsonProperty("closeDate")]
            public string APermitCloseDate { get; set; }

            [JsonProperty("expiryDate")]
            public string APermitExpdtTb { get; set; }

            [JsonProperty("validFromDate")]
            public string APermitValfrDtTb { get; set; }

            [JsonProperty("effectiveDate")]
            public string APermitEffDtTb { get; set; }

            [JsonProperty("birthDate")]
            public object APermitDobTb { get; set; }
        }

        public class ReturnSet
        {
            [JsonProperty("results")]
            public List<object> Results { get; set; }
        }

    }

    public class OutletResponseUiModel
    {

        public Metadata1 Metadata { get; set; }
        public string AOutletStatusTb { get; set; }
        public DateTime AOutletIssueDtTb { get; set; }
        public string AOwner { get; set; }
        public string AActFlag { get; set; }
        public string OutletInPrcFg { get; set; }
        public string AOutletTypeTb { get; set; }
        public DateTime AOutletValidToTb { get; set; }
        public string AOutletTransTinTb { get; set; }
        public string AOutletToDeregTb { get; set; }
        public string AOutletTitleTb { get; set; }
        public string AOutletStreet2Tb { get; set; }
        public string AOutletStreet1Tb { get; set; }
        public string AOutletQuarterTb { get; set; }
        public string AOutletProvinceTb { get; set; }
        public string AOutletPostalCodeTb { get; set; }
        public string AOutletPoBoxTb { get; set; }
        public string AOutletNoTb { get; set; }
        public string AOutletNm7Tb { get; set; }
        public string AOutletNm6Tb { get; set; }
        public string AOutletNm5Tb { get; set; }
        public string AOutletNm4Tb { get; set; }
        public string AOutletNm3Tb { get; set; }
        public string AOutletNm2Tb { get; set; }
        public string AOutletNm1Tb { get; set; }
        public string AOutletNewMainOutnumTb { get; set; }
        public string AOutletNameTb { get; set; }
        public string AOutletMobileNoTb { get; set; }
        public string AOutletMainFlagTb { get; set; }
        public string AOutletIdTypeTb { get; set; }
        public string AOutletIdNoTb { get; set; }
        public string AOutletIdentificationNoTb { get; set; }
        public string AOutletHouseNoTb { get; set; }
        public string AOutletHide { get; set; }
        public string AOutletFlag { get; set; }
        public object AOutletExpdtTb { get; set; }
        public string AOutletExpdtHTb { get; set; }
        public string AOutletExpdtCTb { get; set; }
        public string AOutletEmailTb { get; set; }
        public DateTime AOutletEffDtTb { get; set; }
        public string AOutletEffDtHTb { get; set; }
        public string AOutletEffDtCTb { get; set; }
        public string AOutletDregOptTb { get; set; }
        public object AOutletDobTb { get; set; }
        public string AOutletDobHTb { get; set; }
        public string AOutletDobCTb { get; set; }
        public string AOutletCrNoTb { get; set; }
        public string AOutletCountryTb { get; set; }
        public string AOutletCompAddr { get; set; }
        public string AOutletComcdFlag { get; set; }
        public DateTime AOutletCloseTransferDtTb { get; set; }
        public string AOutletCityTb { get; set; }
        public string AOutletBuildingNoTb { get; set; }
        public string AOutletActionTypeTb { get; set; }
        public string AOuletMoiFlag { get; set; }
        public string APermitIssueName { get; set; }
        public string APermitAdrFlag { get; set; }
        public string APermitMoiFlag { get; set; }
        public string APermitCbFlag { get; set; }
        public string APermitTinFlag { get; set; }
        public string APermitComcdFlag { get; set; }
        public string APermitNewMainNoIdTb { get; set; }
        public string APermitCommFlag { get; set; }
        public string APermitConFlag { get; set; }
        public string APermitFrzCb { get; set; }
        public string APermitGovFlag { get; set; }
        public string APermitTinFlag1 { get; set; }
        public string APermitMainActFlagTb { get; set; }
        public string APermitMainnoTb { get; set; }
        public string APermitNm6Tb { get; set; }
        public string APermitNoTb { get; set; }
        public string APermitTypTxt { get; set; }
        public string PermitInPrcFg { get; set; }
        public string APermitExpdtHTb { get; set; }
        public string APermitMainActNoTb { get; set; }
        public string APermitOutletnoTb { get; set; }
        public string APermitExpdtCTb { get; set; }
        public string APermitTitleTb { get; set; }
        public string APermitNm5Tb { get; set; }
        public string APermitNm7Tb { get; set; }
        public string APermitTypeTb { get; set; }
        public string APermitValfrDtHTb { get; set; }
        public string APermitValfrDtCTb { get; set; }
        public string APermitEffDtHTb { get; set; }
        public string APermitEffDtCTb { get; set; }
        public string APermitDregRsnTb { get; set; }
        public string APermitTransTinTb { get; set; }
        public string APermitIdTypeTb { get; set; }
        public string APermitIdNoTb { get; set; }
        public string APermitNm1Tb { get; set; }
        public string APermitNm2Tb { get; set; }
        public string APermitNm3Tb { get; set; }
        public string APermitNm4Tb { get; set; }
        public string APermitDobHTb { get; set; }
        public string APermitDobCTb { get; set; }
        public object APermitCloseDate { get; set; }
        public object APermitExpdtTb { get; set; }
        public DateTime APermitValfrDtTb { get; set; }
        public object APermitEffDtTb { get; set; }
        public object APermitDobTb { get; set; }
        //public PermitSetUi PermitSet { get; set; }
        public List<OutletResponseUiModel> PermitSetUi { get; set; }

    }

    public class Metadata1
    {
        public string Id { get; set; }

        public string Uri { get; set; }

        public string Type { get; set; }
    }

    public class PermitSetUi
    {
        public List<OutletResponseUiModel> Results { get; set; }
    }
}

