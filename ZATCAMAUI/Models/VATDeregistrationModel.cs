using System.ComponentModel;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class VATDeregistrationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private string _activeOutletDecisionOptions { get; set; }
        public string ActiveOutletDecisionOptions
        {
            get
            {
                return _activeOutletDecisionOptions;
            }
            set
            {
                _activeOutletDecisionOptions = value;
                OnPropertyRaised("ActiveOutletDecisionOptions");
            }
        }

        private bool _activeOutletDecisionOptionsIsSelected { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected
        {
            get
            {
                return _activeOutletDecisionOptionsIsSelected;
            }
            set
            {
                _activeOutletDecisionOptionsIsSelected = value;
                OnPropertyRaised("ActiveOutletDecisionOptionsIsSelected");
            }
        }

        public string ActiveOutletDocumentOptions { get; set; }
        public bool ActiveOutletDocumentOptionsIsSelected { get; set; }
        private Color textCol = (Color)Application.Current.Resources["Primary"];
        public Color TextCol
        {
            get { return textCol; }
            set
            {
                if (value != textCol)
                    textCol = value; OnPropertyRaised("TextCol");

            }
        }

        private string imgSource = "vat_tile_listofsignup_W";
        public string ImgSource
        {
            get { return imgSource; }
            set
            {
                if (value != imgSource)
                    imgSource = value; OnPropertyRaised("ImgSource");


            }
        }

    }
    
    public class VATDeregistrationModelRootObject
    {
        public VATDeregistrationModelDetailD d { get; set; }
    }
    
    public class VATDeregistrationModelDetailD
    {
        public List<VATDeregistrationModelDetailsResult> results { get; set; }
    }
    
    public class VATDeregistrationLastICRDateRootObject
    {
        public VATDeregistrationLastICRDateDetailD d { get; set; }
    }
    
    public class VATDeregistrationLastICRDateDetailD
    {
        [JsonProperty("results")]

        public List<VATDeregistrationLastICRDateDetailsResult> results { get; set; }
    }
    
    public class VATDeregistrationLastICRDateDetailsResult
    {
        public __metadata __metadata { get; set; }

        public DateTime Lasticrdt { get; set; }

        public string Reqtp { get; set; }

        public string TxnTpx { get; set; }

        public string UserTypx { get; set; }
        public string Gpartx { get; set; }


    }
    
    public class VATDeRegistrationAttachmentDropdownDetails
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("ELGBL_DOCSet")]
        public VatDeregSubItemsSet VatDeregSubItemsSet { get; set; }
        //public VATDeregistrationAttachmentDetailD d { get; set; }
    }
    
    public partial class VatDeregSubItemsSet
    {
        [JsonProperty("results")]
        public ResultsAttachmentItemForElgblDocSet[] Results { get; set; }
    }
    
    public class VATDeregistrationSuspendedDateRootObject
    {
        public VATDeregSuspendedDateRootObjectDetailD d { get; set; }
    }
    
    public class VATDeregSuspendedDateRootObjectDetailD
    {
        [JsonProperty("results")]
        public List<VATDeregSuspendedDateRootObjectDetailsResult> dateResults { get; set; }
    }
    
    public class VATDeregSuspendedDateRootObjectDetailsResult
    {
        public __metadata __metadata { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? SuspDtfrom
        {
            get; set;
        }
        public DateTime? SuspDtto { get; set; }
        public DateTime? NextDtfrom { get; set; }
        public DateTime? NextDtto { get; set; }
        public DateTime? Duedate { get; set; }

    }




    
    public class VATDeregistrationModelDetailsResult
    {
        public __metadata __metadata { get; set; }

        public string Reason { get; set; }
        public string Lang { get; set; }
        public string TxnTp { get; set; }
        public string Rdesc { get; set; }

    }
    
    public class ResultsAttachmentItemForElgblDocSet : INotifyPropertyChanged
    {
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        public __metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string Spras { get; set; }
        public string Fbtyp { get; set; }
        public string TxnTp { get; set; }
        public string DmsTp { get; set; }
        public string StartDt { get; set; }
        public string EndDt { get; set; }
        public string Txt50 { get; set; }
        private Color textCol = (Color)Application.Current.Resources["Primary"];
        public Color TextCol
        {
            get { return textCol; }
            set
            {
                if (value != textCol)
                    textCol = value; OnPropertyRaised("TextCol");

            }
        }

        private string imgSource = "vat_tile_listofsignup_W";
        public event PropertyChangedEventHandler PropertyChanged;

        public string ImgSource
        {
            get { return imgSource; }
            set
            {
                if (value != imgSource)
                    imgSource = value; OnPropertyRaised("ImgSource");


            }
        }


    }
    
    public class VATDeRegistrationDetails
    {
        public vATDeRegistration d { get; set; }
    }
    
    public class vATDeRegistration
    {
        public __metadata __metadata { get; set; }
        public bool CaseFg { get; set; }
        public string CaseReason { get; set; }
        public bool Agreeflg { get; set; }
        public bool OpenCaseFg { get; set; }
        public bool ReviewFg { get; set; }
        public bool Auditatt { get; set; }
        public bool Certatt { get; set; }
        public bool Chkdt { get; set; }
        public bool Contractatt { get; set; }
        public bool Declareflg { get; set; }
        public bool Declareflg2 { get; set; }
        public bool Incomatt { get; set; }
        public bool Incstmtatt { get; set; }
        public bool Insatt { get; set; }
        public bool Legatt { get; set; }
        public bool Otheratt { get; set; }
        public string Operationx { get; set; }
        public string TxnTpx { get; set; }
        public string Type { get; set; }
        public string UserTypx { get; set; }
        public string Dregdt { get; set; }
        public string Duedate { get; set; }
        public string SuspDtfrom { get; set; }
        public string SuspDtto { get; set; }
        public string NextDtfrom { get; set; }
        public string NextDtto { get; set; }
        public string Declaredt { get; set; }
        public string EndDate { get; set; }
        public string Lasticrdt { get; set; }
        public string StartDate { get; set; }
        public string Taxdt { get; set; }
        public string Actnm { get; set; }
        public string Langx { get; set; }
        public string CaseId { get; set; }
        public string SuspPeriod { get; set; }
        public string Atype { get; set; }
        public string NextPeriod { get; set; }
        public string Branchx { get; set; }
        public string Caltp { get; set; }
        public string Contactnm { get; set; }
        public string Crno { get; set; }
        public string DataVersion { get; set; }
        public string Designation { get; set; }
        public string Euser { get; set; }
        public string Fbnumx { get; set; }
        public string Fbstax { get; set; }
        public string Fbustx { get; set; }
        public string FormGuid { get; set; }
        public string Formprocx { get; set; }
        public string Forwardx { get; set; }
        public string FullName { get; set; }
        public string Gpart { get; set; }
        public string Gpartx { get; set; }
        public string Idnumbr { get; set; }
        public string Mandt { get; set; }
        public string Mandtx { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string NameOrg1 { get; set; }
        public string NameOrg2 { get; set; }
        public string Officerx { get; set; }
        public string Other { get; set; }
        public string PortalUsrx { get; set; }
        public string Reason { get; set; }
        public string Reqtp { get; set; }
        public string ReturnIdx { get; set; }
        public string Srcidentifyx { get; set; }
        public string Statusx { get; set; }

        public string StepNumber { get; set; }
        public string StepNumberx { get; set; }

        public AddressSet AddressSet { get; set; }
        public NotesSet NotesSet { get; set; }
        public AttdetSet AttdetSet { get; set; }
        public QuesListSet QuesListSet { get; set; }
    }
    
    public class VATDeregAttachmentRootOject
    {
        public VATDeregAttachment d { get; set; }
    }
    
    public class QuesListSet
    {
        public List<string> results { get; set; }
    }
    
    public class ResultsItemSet
    {
        public __metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public long LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string AddrType { get; set; }
        public string Srcidentify { get; set; }
        public string Begda { get; set; }
        public string Endda { get; set; }
        public string Gpart { get; set; }
        public string HouseNum1 { get; set; }
        public string HouseNum2 { get; set; }
        public string Floor { get; set; }
        public string City1 { get; set; }
        public string City2 { get; set; }
        public string Country { get; set; }
        public string PostCode1 { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Region { get; set; }
        public string RegionDesc { get; set; }
    }
    

    public class AddressSet
    {
        public List<ResultsItemSet> results { get; set; }
    }
    

    public class NotesSet
    {
        public List<VATDeregNote> results { get; set; }
    }
    
    public class VATDeregNote
    {
        public Metadata2 __metadata { get; set; }
        public string Notenoz { get; set; }
        public string Refnamez { get; set; }
        public string XInvoicez { get; set; }
        public string XObsoletez { get; set; }
        public string Rcodez { get; set; }
        public string Erfusrz { get; set; }
        public string Erfdtz { get; set; }
        public string Erftmz { get; set; }
        public string AttByz { get; set; }
        public string ByPusrz { get; set; }
        public string ByGpartz { get; set; }
        public string DataVersionz { get; set; }
        public string Namez { get; set; } //Name
        public string Noteno { get; set; }
        public int Lineno { get; set; }
        public int ElemNo { get; set; }
        public string Tdformat { get; set; }
        public string Tdline { get; set; }
        public string Sect { get; set; } //Section
        public string Strdt { get; set; } //date
        public string Strtime { get; set; } //time
        public string Strline { get; set; }   //note
    }
    
    public class VATDeregAttachment
    {
        public Metadata5 __metadata { get; set; }
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
    
    public class VATDeregistrationAttachmentsModel
    {
        public VATDeregistrationAttachmentsModel()
        {
        }

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
        public string AttachmentName { get; set; }

        public bool IsAttachmentAttached { get; set; }
    }
    

    public class AttdetSet
    {
        public List<Attachment> results { get; set; }
    }
    

    public class VATDeregistrationSummaryModel
    {
        public VATDeregistrationSummaryModel()
        {
        }

        public string SummaryTitle { get; set; }
        public string SummaryData { get; set; }
        public bool IsEditVisible { get; set; }
    }
    public class VATDeregDeclaration
    {
        [JsonProperty("d")]
        public VATDeregDeclarationData D;
    }
    public class VATDeregDeclarationData
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata;

        [JsonProperty("Fbnum")]
        public string Fbnum;

        [JsonProperty("Spras")]
        public string Spras;

        [JsonProperty("Zterms")]
        public string Zterms;
    }


}
