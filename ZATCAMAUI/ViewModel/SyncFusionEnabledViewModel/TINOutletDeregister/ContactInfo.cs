using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TINOutletDeregister;

#region ContactInfo_NestedListView
public class ContactInfo_NestedListView : INotifyPropertyChanged
{
    #region Fields

    private bool isInnerListVisible;
    private ObservableCollection<DetailsContactInfo> contactDetails;
    private string location;

    private string aOutletNoTb;
    private string aOutletTypeTb;
    private string aOutletNameTb;
    private string aOutletIdentificationNoTb;
    private string aOutletStatusTb;
    private string aoutletReason;
    private string aOutletValidToTb;
    private string aOutletCloseTransferDtTb;
    private string aOutletIssueDtTb;
    private bool isOutletChecked;
    private bool isOutletCheckEnable;
    private bool isDateEnable;
    private string aOwner;
    private string aActFlag;
    private string aOutletActionTypeTb;
    private bool ssActionTypeEnabled;




    #endregion

    #region Properties
    public Command InnerListTapCommand { get; set; }

    public ObservableCollection<DetailsContactInfo> ContactDetails
    {
        get { return contactDetails; }
        set
        {
            contactDetails = value;
            this.RaisedOnPropertyChanged("ContactDetails");
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

    public string Location
    {
        get { return location; }
        set { location = value; this.RaisedOnPropertyChanged("Location"); }
    }

    //private string ;
    //private string ;
    //private string aOutletNameTb;
    //private string ;
    //private string ;
    //private string ;

    public string AOutletNoTb
    {
        get { return aOutletNoTb; }
        set { aOutletNoTb = value; this.RaisedOnPropertyChanged("AOutletNoTb"); }
    }
    public string AOutletTypeTb
    {
        get { return aOutletTypeTb; }
        set { aOutletTypeTb = value; this.RaisedOnPropertyChanged("AOutletTypeTb"); }
    }
    public string AOutletNameTb
    {
        get { return aOutletNameTb; }
        set { aOutletNameTb = value; this.RaisedOnPropertyChanged("AOutletNameTb"); }
    }
    public string AOutletIdentificationNoTb
    {
        get { return aOutletIdentificationNoTb; }
        set { aOutletIdentificationNoTb = value; this.RaisedOnPropertyChanged("AOutletIdentificationNoTb"); }
    }
    public string AOutletStatusTb
    {
        get { return aOutletStatusTb; }
        set { aOutletStatusTb = value; this.RaisedOnPropertyChanged("AOutletStatusTb"); }
    }

    public string AoutletReason
    {
        get { return aoutletReason; }
        set { aoutletReason = value; this.RaisedOnPropertyChanged("AoutletReason"); }
    }
    public string AOutletValidToTb
    {
        get { return aOutletValidToTb; }
        set { aOutletValidToTb = value; this.RaisedOnPropertyChanged("AOutletValidToTb"); }
    }

    public string AOutletCloseTransferDtTb
    {
        get { return aOutletCloseTransferDtTb; }
        set { aOutletCloseTransferDtTb = value; this.RaisedOnPropertyChanged("AOutletCloseTransferDtTb"); }
    }

    public string AOutletIssueDtTb
    {
        get { return aOutletIssueDtTb; }
        set { aOutletIssueDtTb = value; this.RaisedOnPropertyChanged("AOutletIssueDtTb"); }
    }

    public bool IsOutletChecked
    {
        get { return isOutletChecked; }
        set { isOutletChecked = value; this.RaisedOnPropertyChanged("IsOutletChecked"); }
    }
    public bool IsOutletCheckEnable
    {
        get { return isOutletCheckEnable; }
        set { isOutletCheckEnable = value; this.RaisedOnPropertyChanged("IsOutletCheckEnable"); }
    }

    public bool IsDateEnable
    {
        get { return isDateEnable; }
        set { isDateEnable = value; this.RaisedOnPropertyChanged("IsDateEnable"); }
    }
    public string AOwner
    {
        get { return aOwner; }
        set { aOwner = value; this.RaisedOnPropertyChanged("AOwner"); }
    }
    public string AActFlag
    {
        get { return aActFlag; }
        set { aActFlag = value; this.RaisedOnPropertyChanged("AActFlag"); }
    }
    public string AOutletActionTypeTb
    {
        get { return aOutletActionTypeTb; }
        set { aOutletActionTypeTb = value; this.RaisedOnPropertyChanged("AOutletActionTypeTb"); }
    }
    public bool IsActionTypeEnabled
    {
        get { return ssActionTypeEnabled; }
        set { ssActionTypeEnabled = value; this.RaisedOnPropertyChanged("IsActionTypeEnabled"); }
    }





    #endregion

    #region Constructor
    public ContactInfo_NestedListView()
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
    private string aPermitValfrDtTb;
    private string aPermitIssueName;
    private string aLicenceCloseTransfer;
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

    public string APermitValfrDtTb
    {
        get { return aPermitValfrDtTb; }
        set { aPermitValfrDtTb = value; this.RaisedOnPropertyChanged("APermitValfrDtTb"); }
    }

    public string APermitReason
    {
        get { return aPermitReason; }
        set { aPermitReason = value; this.RaisedOnPropertyChanged("APermitReason"); }
    }

    public string ALicenceCloseTransfer
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

