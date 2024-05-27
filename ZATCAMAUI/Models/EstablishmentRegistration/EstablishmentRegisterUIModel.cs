
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ZATCAMAUI.Models.EstablishmentRegistration
{
    public class EstablishmentRegisterUIModel : INotifyPropertyChanged
    {
        #region Properties

        public ObservableCollection<OuteltInfo_NestedListView> ContactInfo { get; set; }

        public Command<object> OuterListTapCommand { get; set; }
        #endregion

        #region Constructor
        public EstablishmentRegisterUIModel()
        {
            OuterListTapCommand = new Command<object>(OnOuterListTapped);
            GenerateDetails();
        }

        private void OnOuterListTapped(object obj)
        {
            var item = obj as OuteltInfo_NestedListView;
            item.IsInnerListVisible = !item.IsInnerListVisible;
        }
        #endregion

        #region Private Methods

        private void GenerateDetails()
        {
            ContactInfo = new ObservableCollection<OuteltInfo_NestedListView>
            {
               

            };
        }

        #endregion

        #region Interface Methods

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }

        #endregion

    }

    #region OuteltInfo_NestedListView
   
    public class OuteltInfo_NestedListView : INotifyPropertyChanged
    {
        #region Fields

        private bool isInnerListVisible;

        private Metadata __metadata { get; set; }
        private string mciEntry { get; set; }
        private string cityCode { get; set; }
        private string city1 { get; set; }
        private string crlicenceno { get; set; }
        private string oldmst { get; set; }
        private string outdocdreg { get; set; }
        private string caltp { get; set; }
        private string cmatt { get; set; }
        private string mandtx { get; set; }
        private string fbnumx { get; set; }
        private string rentatt { get; set; }
        private string conatt { get; set; }
        private string portalUsrx { get; set; }
        private string langx { get; set; }
        private string operationx { get; set; }
        private string stepNumberx { get; set; }
        private string returnIdx { get; set; }
        private string officerx { get; set; }
        private string gpartx { get; set; }
        private string mandt { get; set; }
        private string formGuid { get; set; }
        private string dataVersion { get; set; }
        private int lineNo { get; set; }
        private string rankingOrder { get; set; }
        private string actno { get; set; }
        private DateTime? startDate { get; set; }
        private DateTime? endDate { get; set; }
        private string actcat { get; set; }
        private string actnm { get; set; }
        private string actnm2 { get; set; }
        private string chInd { get; set; }
        //private bool showCRNoData { get; set; }
        //private bool showLicenceNoData { get; set; }
        private bool showDeleteIcon { get; set; }
        private bool showEditIcon { get; set; }
        private string actCatDesc { get; set; }
        private string crTypeDesc { get; set; }
        //private ObservableCollection<OutletItem> outletItems;
        private ObservableCollection<Nreg_ActivityItem> contactDetails;
        private ObservableCollection<Nreg_ActivityItem> contactDetails2;

   
        #endregion

        #region Properties
        public Command InnerListTapCommand { get; set; }

        //public ObservableCollection<OutletItem> OutletItems
        //{
        //    get { return outletItems; }
        //    set
        //    {
        //        outletItems = value;
        //        this.RaisedOnPropertyChanged("OutletItems");
        //    }
        //}

        public ObservableCollection<Nreg_ActivityItem> ContactDetails
        {
            get { return contactDetails; }
            set
            {
                contactDetails = value;
                this.RaisedOnPropertyChanged("ContactDetails");
            }
        }

        public ObservableCollection<Nreg_ActivityItem> ContactDetails2
        {
            get { return contactDetails2; }
            set
            {
                contactDetails2 = value;
                this.RaisedOnPropertyChanged("ContactDetails2");
            }
        }

        public bool IsInnerListVisible
        {
            get { return isInnerListVisible; }
            set
            {
                isInnerListVisible = value;
                this.RaisedOnPropertyChanged("IsInnerListVisible");
            }
        }

        public Metadata Metadata
        {
            get { return __metadata; }
            set { __metadata = value; this.RaisedOnPropertyChanged("Metadata"); }
        }


        
        public string MciEntry
        {
            get { return mciEntry; }
            set { mciEntry = value; this.RaisedOnPropertyChanged("MciEntry"); }
        }

        public string CityCode
        {
            get { return cityCode; }
            set { cityCode = value; this.RaisedOnPropertyChanged("CityCode"); }
        }
        public string City1
        {
            get { return city1; }
            set { city1 = value; this.RaisedOnPropertyChanged("City1"); }
        }

        public string Crlicenceno
        {
            get { return crlicenceno; }
            set { crlicenceno = value; this.RaisedOnPropertyChanged("Crlicenceno"); }
        }
        public string Oldmst
        {
            get { return oldmst; }
            set { oldmst = value; this.RaisedOnPropertyChanged("Oldmst"); }
        }

        public string Outdocdreg
        {
            get { return outdocdreg; }
            set { outdocdreg = value; this.RaisedOnPropertyChanged("Outdocdreg"); }
        }
        public string Caltp
        {
            get { return caltp; }
            set { caltp = value; this.RaisedOnPropertyChanged("Caltp"); }
        }

        public string Cmatt
        {
            get { return cmatt; }
            set { cmatt = value; this.RaisedOnPropertyChanged("Cmatt"); }
        }
        public string Mandtx
        {
            get { return mandtx; }
            set { mandtx = value; this.RaisedOnPropertyChanged("Mandtx"); }
        }


        public string Fbnumx
        {
            get { return fbnumx; }
            set { fbnumx = value; this.RaisedOnPropertyChanged("Fbnumx"); }
        }
        public string Rentatt
        {
            get { return rentatt; }
            set { rentatt = value; this.RaisedOnPropertyChanged("Rentatt"); }
        }

        public string Conatt
        {
            get { return conatt; }
            set { conatt = value; this.RaisedOnPropertyChanged("Conatt"); }
        }
        public string PortalUsrx
        {
            get { return portalUsrx; }
            set { portalUsrx = value; this.RaisedOnPropertyChanged("PortalUsrx"); }
        }

        public string Langx
        {
            get { return langx; }
            set { langx = value; this.RaisedOnPropertyChanged("Langx"); }
        }
        public string Operationx
        {
            get { return operationx; }
            set { operationx = value; this.RaisedOnPropertyChanged("Operationx"); }
        }

        public string StepNumberx
        {
            get { return stepNumberx; }
            set { stepNumberx = value; this.RaisedOnPropertyChanged("StepNumberx"); }
        }
        public string ReturnIdx
        {
            get { return returnIdx; }
            set { returnIdx = value; this.RaisedOnPropertyChanged("ReturnIdx"); }
        }

        public string Officerx
        {
            get { return officerx; }
            set { officerx = value; this.RaisedOnPropertyChanged("Officerx"); }
        }
        public string Gpartx
        {
            get { return gpartx; }
            set { gpartx = value; this.RaisedOnPropertyChanged("Gpartx"); }
        }
        public string Mandt
        {
            get { return mandt; }
            set { mandt = value; this.RaisedOnPropertyChanged("Mandt"); }
        }

        public string FormGuid
        {
            get { return formGuid; }
            set { formGuid = value; this.RaisedOnPropertyChanged("FormGuid"); }
        }
        public string DataVersion
        {
            get { return dataVersion; }
            set { dataVersion = value; this.RaisedOnPropertyChanged("DataVersion"); }
        }

        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; this.RaisedOnPropertyChanged("StartDate"); }
        }

        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; this.RaisedOnPropertyChanged("EndDate"); }
        }
        public string ActCat
        {
            get { return actcat; }
            set { actcat = value; this.RaisedOnPropertyChanged("ActCat"); }
        }
        public string ActNm
        {
            get { return actnm; }
            set { actnm = value; this.RaisedOnPropertyChanged("ActNm"); }
        }

        public string ActNm2
        {
            get { return actnm2; }
            set { actnm2 = value; this.RaisedOnPropertyChanged("ActNm2"); }
        }
        public string ChInd
        {
            get { return chInd; }
            set { chInd = value; this.RaisedOnPropertyChanged("ChInd"); }
        }


        //private int lineNo { get; set; }
        //private string rankingOrder { get; set; }
        //private string actno { get; set; }
        public int LineNo
        {
            get { return lineNo; }
            set { lineNo = value; this.RaisedOnPropertyChanged("LineNo"); }
        }

        public string RankingOrder
        {
            get { return rankingOrder; }
            set { rankingOrder = value; this.RaisedOnPropertyChanged("RankingOrder"); }
        }
        public string ActNo
        {
            get { return actno; }
            set { actno = value; this.RaisedOnPropertyChanged("ActNo"); }
        }

        public bool ShowDeleteIcon
        {
            get { return showDeleteIcon; }
            set { showDeleteIcon = value; this.RaisedOnPropertyChanged("ShowDeleteIcon"); }
        }

        public bool ShowEditIcon
        {
            get { return showEditIcon; }
            set { showEditIcon = value; this.RaisedOnPropertyChanged("ShowEditIcon"); }
        }
        public string ActCatDesc
        {
            get { return actCatDesc; }
            set { actCatDesc = value; this.RaisedOnPropertyChanged("ActCatDesc"); }
        }
        public string CrTypeDesc
        {
            get { return crTypeDesc; }
            set { crTypeDesc = value; this.RaisedOnPropertyChanged("CrTypeDesc"); }
        }
        //public bool ShowCRNoData
        //{
        //    get { return showCRNoData; }
        //    set { showCRNoData = value; this.RaisedOnPropertyChanged("ShowCRNoData"); }
        //}
        //public bool ShowLicenceNoData
        //{
        //    get { return showLicenceNoData; }
        //    set { showLicenceNoData = value; this.RaisedOnPropertyChanged("ShowLicenceNoData"); }
        //}

        #endregion

        #region Constructor
        public OuteltInfo_NestedListView()
        {
            InnerListTapCommand = new Command(OnInnerListTapped);
            //var contactsRepository = new ContactsInfoRepository();
            //ContactDetails = contactsRepository.GenerateContactDetails(4);
        }
        #endregion

        #region Private Methods

        private void OnInnerListTapped(object obj)
        {
            var item = obj as DetailsContactInfo;
            item.IsSubListVisible = !item.IsSubListVisible;
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }
        #endregion

    }
    #endregion

    #region DetailsContactInfo
    public class DetailsContactInfo : INotifyPropertyChanged
    {
        #region Fields

        private string contactName;
        private string contactNumber;
        public ImageSource image;
        private ObservableCollection<MembersInfo> members;
        private bool isSubListVisible;

        private string aPermitNoTb;
        private object aPermitValfrDtTb;
        private string aPermitIssueName;
        private object aLicenceCloseTransfer;
        private string aPermitReason;
        private bool isPermitChecked;
        private bool isPermitCheckEnable;
        private string aPermitOutletNoTb;
        private bool isPermitDateEnable;
        private bool isPermitActionTypeEnable;

        #endregion

        #region Constructor
        public DetailsContactInfo()
        {
            Members = new ObservableCollection<MembersInfo>();

        }
        #endregion

        #region Properties

        public string APermitNoTb
        {
            get { return aPermitNoTb; }
            set { aPermitNoTb = value; this.RaisedOnPropertyChanged("APermitNoTb"); }
        }

        public string APermitIssueName
        {
            get { return aPermitIssueName; }
            set { aPermitIssueName = value; this.RaisedOnPropertyChanged("AaPermitIssueName"); }
        }

        public object APermitValfrDtTb
        {
            get { return aPermitValfrDtTb; }
            set { aPermitValfrDtTb = value; this.RaisedOnPropertyChanged("APermitValfrDtTb"); }
        }

        public string APermitReason
        {
            get { return aPermitReason; }
            set { aPermitReason = value; this.RaisedOnPropertyChanged("APermitReason"); }
        }

        public object ALicenceCloseTransfer
        {
            get { return aLicenceCloseTransfer; }
            set { aLicenceCloseTransfer = value; this.RaisedOnPropertyChanged("ALicenceCloseTransfer"); }
        }
        public bool IsPermitChecked
        {
            get { return isPermitChecked; }
            set { isPermitChecked = value; this.RaisedOnPropertyChanged("IsPermitChecked"); }
        }

        public bool IsPermitCheckEnable
        {
            get { return isPermitCheckEnable; }
            set { isPermitCheckEnable = value; this.RaisedOnPropertyChanged("IsPermitCheckEnable"); }
        }

        public string APermitOutletNoTb
        {
            get { return aPermitOutletNoTb; }
            set { aPermitOutletNoTb = value; this.RaisedOnPropertyChanged("APermitOutletNoTb"); }
        }
        public bool IsPermitDateEnable
        {
            get { return isPermitDateEnable; }
            set { isPermitDateEnable = value; this.RaisedOnPropertyChanged("IsPermitDateEnable"); }
        }

        public bool IsPermitActionTypeEnable
        {
            get { return isPermitActionTypeEnable; }
            set { isPermitActionTypeEnable = value; this.RaisedOnPropertyChanged("IsPermitActionTypeEnable"); }
        }



        public string ContactName
        {
            get { return contactName; }
            set { contactName = value; this.RaisedOnPropertyChanged("ContactName"); }
        }
        public string ContactNumber
        {
            get { return contactNumber; }
            set { contactNumber = value; this.RaisedOnPropertyChanged("ContactNumber"); }
        }

        public ImageSource ContactImage
        {
            get { return this.image; }
            set { this.image = value; this.RaisedOnPropertyChanged("ContactImage"); }
        }

        public ObservableCollection<MembersInfo> Members
        {
            get { return members; }
            set
            {
                this.members = value;
                this.RaisedOnPropertyChanged("Members");
            }
        }

        public bool IsSubListVisible
        {
            get { return isSubListVisible; }
            set
            {
                isSubListVisible = value;
                this.RaisedOnPropertyChanged("IsSubListVisible");
            }
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }
        #endregion
    }
    #endregion

    #region Items
   
    public class MembersInfo : INotifyPropertyChanged
    {
        #region Fields

        private string name;
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { this.name = value; this.RaisedOnPropertyChanged("Name"); }
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }
        #endregion
    }
    #endregion
}

