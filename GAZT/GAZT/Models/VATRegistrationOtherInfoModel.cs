using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
    public class __metadataForVATREgistrationOtherInfo
    {

        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }


    [Preserve(AllMembers = true)]
    public class ResultsItemForButton
    {
        /// <summary>
        /// 
        /// </summary>
        public __metadataForVATREgistrationOtherInfo __metadata { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Fbtyp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Fbust { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Button { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class VR_UI_BTNSet
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ResultsItemForButton> results { get; set; }
    }


    [Preserve(AllMembers = true)]
    public class ResultsItemForDOCSet
    {
        /// <summary>
        /// 
        /// </summary>
        public __metadataForVATREgistrationOtherInfo __metadata { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Mandt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Spras { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Fbtyp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TxnTp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DmsTp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StartDt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EndDt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Txt50 { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ELGBL_DOCSet
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ResultsItemForElgblDocSet> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ELGBL_DOCSetforsubmit
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ResultsItemForDOCSetforsubmit> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class VATRegistrationWithOtherInformation
    {
        /// <summary>
        /// 
        /// </summary>
        public __metadataForVATREgistrationOtherInfo __metadata { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Fbtypz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Fbustz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EditFgz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Mandt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Fbnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PortalUsr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Lang { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Operation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StepNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ReturnId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Officer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Gpart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserTyp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TxnTp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Formproc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public VR_UI_BTNSet VR_UI_BTNSet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ELGBL_DOCSet ELGBL_DOCSet { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class VATRegistrationOtherDetails
    {
        /// <summary>
        /// 
        /// </summary>
        public VATRegistrationWithOtherInformation d { get; set; }
    }


    [Preserve(AllMembers = true)]


    public class DataToPassTofinancialDetailAttachmentPopup
        {
        public VATRegistrationOtherDetails vatRegOthrDetailtoPopup { get; set; }
        public VATRegistrationDetails VATRegistrationDetailsDatatoPopup { get; set; }
}

}
