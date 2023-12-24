namespace ZATCAMAUI.Models
{

    public class EsimatedZAKATReturnsButtonSets
    {
        public EsimatedZAKATReturnsButtonSetsD d { get; set; }
    }

    
    public class EsimatedZAKATReturnsButtonSetsUIButton
    {
        public Metadata2 __metadata { get; set; }
        public string Fbtyp { get; set; }
        public string Fbust { get; set; }
        public string Button { get; set; }
        public string TransactionType { get; set; }
        public string UserTyp { get; set; }
    }
    
    public class UIBtnSet
    {
        public List<EsimatedZAKATReturnsButtonSetsUIButton> results { get; set; }
    }
    
    public class EsimatedZAKATReturnsButtonSetsD
    {
        public Metadata __metadata { get; set; }
        public string Fbtypz { get; set; }
        public string Fbustz { get; set; }
        public string UserTypz { get; set; }
        public string TransactionTypez { get; set; }
        public string EditFgz { get; set; }
        public string Fbnum { get; set; }
        public string PortalUsr { get; set; }
        public string Lang { get; set; }
        public string Operation { get; set; }
        public string StepNumber { get; set; }
        public string ReturnId { get; set; }
        public string Officer { get; set; }
        public string Gpart { get; set; }
        public string Status { get; set; }
        public string TxnTp { get; set; }
        public string Formproc { get; set; }
        public string Periodkey { get; set; }
        public UIBtnSet UI_BtnSet { get; set; }
    }
}
