using Foundation;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class IBanManagementListModel
    {
        
        public class IbanListSetResult
        {
            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }
            [JsonProperty("status")]
            public string Status { get; set; }
            [JsonProperty("statusDescription")]
            public string StatusDesc { get; set; }
            [JsonProperty("TIN")]
            public string Tin { get; set; }
            [JsonProperty("agree")]
            public string AgreeFg { get; set; }
            [JsonProperty("idTypeDescription")]
            public string IdtypeDesc { get; set; }
            [JsonProperty("activeIBAN")]
            public string ActiveIban { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            // public string VisibleUpdate { get; set; }
            public string StatusText { get; set; }

            [JsonProperty("visibleUpdate")]
            public string _visibleUpdate = String.Empty;
            public string VisibleUpdate
            {
                get
                {
                    return _visibleUpdate;
                }
                set
                {
                    _visibleUpdate = value;

                    if (VisibleUpdate == "")
                    {
                        // Display ActionSheet Radio buttons 
                        if (ActiveIban == "X")
                        {
                            //StatusText = "Deactivate";
                            StatusText =AppResources.IBanDeactivate;

                            isUpdateEnabled = true;
                            isUpdateDisabled = false;
                        }
                        else if (ActiveIban == "")
                        {
                            // StatusText = "Activate";
                            StatusText = AppResources.IBanActivate;
                            isUpdateEnabled = true;
                            isUpdateDisabled = false;
                        }
                    }
                    else
                    {
                        //IsEnabled Update or disble update button
                        if (EnableUpdate == "X")
                        {
                            //StatusText = "Update";
                            StatusText = AppResources.IBANUpdate;
                            isUpdateEnabled = true;
                            isUpdateDisabled = false;
                        }
                        else if (EnableUpdate == "")
                        {
                            //StatusText = "Update";
                            StatusText = AppResources.IBANUpdate;
                            isUpdateEnabled = false;
                            isUpdateDisabled = true;
                        }
                    }

                }
            }



            [JsonProperty("accountHolder")]
            public string Koinh { get; set; }
            [JsonProperty("idType")]
            public string Type { get; set; }
         
            public bool isUpdateEnabled {get;set;}

            public bool isUpdateDisabled {get;set;}
            [JsonProperty("idNumber")]
            public string Idnumber { get; set; }
            [JsonProperty("IBAN")]
            public string Iban { get; set; }
            [JsonProperty("bankId")]
            public string Bankid { get; set; }
            [JsonProperty("bankDetails")]
            public string Bkext { get; set; }

            //public string EnableUpdate { get; set; }
            [JsonProperty("action")]
            public string Action { get; set; }

            [JsonProperty("enableUpdate")]
            public string _enableUpdate = String.Empty;
            public string EnableUpdate
            {
                get
                {

                    return _enableUpdate;
                }
                set
                {
                    _enableUpdate = value;


                    if (VisibleUpdate == "")
                    {
                        // Display ActionSheet Radio buttons 
                        if (ActiveIban == "X")
                        {
                           // StatusText = "Deactivate";
                            StatusText = AppResources.IBanDeactivate;
                            isUpdateEnabled = true;
                            isUpdateDisabled = false;
                        }
                        else if (ActiveIban == "")
                        {
                            //StatusText = "Activate";
                            StatusText = AppResources.IBanActivate;
                            isUpdateEnabled = true;
                            isUpdateDisabled = false;
                        }
                    }
                    else
                    {
                        //IsEnabled Update or disble update button
                        if (EnableUpdate == "X")
                        {
                            // StatusText = "Update";
                            StatusText = AppResources.IBANUpdate;
                            isUpdateEnabled = true;
                            isUpdateDisabled = false;
                        }
                        else if (EnableUpdate == "")
                        {
                            // StatusText = "Update";
                            StatusText = AppResources.IBANUpdate;
                            isUpdateEnabled = false;
                            isUpdateDisabled = true;
                        }
                    }

                }
            }

        }
        
        public class IbanListSet
        {
            public List<IbanListSetResult> results { get; set; }
        }

        
        public class Result
        {


            [JsonProperty("bankId")]
            public string Bankid { get; set; }

            [JsonProperty("bankDetails")]
            public string Bkext { get; set; }
    }

        
        public class BankListSet
        {

            
            public List<Result> results { get; set; }
        }

        
        public class IdNumberListSetResult
        {

            [JsonProperty("idType")]
            public string IdType { get; set; }
            [JsonProperty("idNumber")]
            public string IdNumber { get; set; }
            [JsonProperty("activityName")] 
            public string Actnm { get; set; }
        }

        [Preserve(AllMembers = true)]
        public class IdNumberListSet
        {
            
            public List<IdNumberListSetResult> results { get; set; }
        }

        
        public class IdTypeListSetResult
        {
            [JsonProperty("idType")]
            public string IdType { get; set; }

            [JsonProperty("idDescription")]
            public string IdDesc { get; set; }
    }

        
        public class IdTypeListSet
        {

            
            public List<IdTypeListSetResult> results { get; set; }
        }

        
        public class IBanModelD
        {

            
            public Metadata __metadata { get; set; }

            [JsonProperty("TIN")]
            public string Tin { get; set; }

            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
            [JsonProperty("isAutoPopulation")]
            public bool AutoPopFg { get; set; }
            [JsonProperty("authenticationUser")]
            public string Euser { get; set; }
            public bool isUpdateFlag { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("IBANs")]
            public List<IbanListSetResult> IbanListSet { get; set; }

            [JsonProperty("banks")]
            public List<Result> BankListSet { get; set; }

            [JsonProperty("idNumbers")]
            public List<IdNumberListSetResult> IdNumberListSet { get; set; }

            [JsonProperty("idTypes")]
            public List<IdTypeListSetResult> IdTypeListSet { get; set; }
        }

        
        public class IBanAccountManagementResponseModel
        {

            [JsonProperty("data")]
            public IBanModelD d { get; set; }
        }

    }
    public class IBanFormGUIDModel
    {
        [JsonProperty("visibleUpdate")]
        public string VisibleUpdate { get; set; }
        [JsonProperty("idType")]
        public string Type { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("statusDescription")]
        public string StatusDesc { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("accountHolder")]
        public string Koinh { get; set; }
        [JsonProperty("idTypeDescription")]
        public string IdtypeDesc { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        [JsonProperty("IBAN")]
        public string Iban { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("enableUpdate")]
        public string EnableUpdate { get; set; }
        [JsonProperty("bankDetails")]
        public string Bkext { get; set; }
        [JsonProperty("bankId")]
        public string Bankid { get; set; }
        [JsonProperty("agree")]
        public string AgreeFg { get; set; }
        [JsonProperty("activeIBAN")]
        public string ActiveIban { get; set; }
        [JsonProperty("action")]
        public string Action { get; set; }
    }

    public class IbanAccountFormGuidResponse
    {
        [JsonProperty("data")]
        public IBanFormGUIDModel d { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class IBANPostRequest
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("accountHolder")]
        public string Koinh { get; set; }
        [JsonProperty("idType")]
        public string Type { get; set; }
        [JsonProperty("idTypeDescription")]
        public string IdtypeDesc { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        [JsonProperty("IBAN")]
        public string Iban { get; set; }
        [JsonProperty("bankId")]
        public string Bankid { get; set; }
        [JsonProperty("bankDetails")]
        public string Bkext { get; set; }
        [JsonProperty("agree")]
        public string AgreeFg { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        public string activeIBAN { get; set; }
        public string enableUpdate { get; set; }
        public string formGUID { get; set; }
        public string statusDescription { get; set; }
        public string visibleUpdate { get; set; }
        public string status { get; set; }
    }

    
    public class IBANClickRequest
    {
        public string Action { get; set; }
        public string Fbnum { get; set; }
        public string FormGuid { get; set; }
        public string Iban { get; set; }

    }

    
    public class IBANNewData
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("statusDescription")]
        public string StatusDesc { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("agree")]
        public string AgreeFg { get; set; }
        [JsonProperty("idTypeDescription")]
        public string IdtypeDesc { get; set; }
        [JsonProperty("activeIBAN")]
        public string ActiveIban { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("visibleUpdate")]
        public string VisibleUpdate { get; set; }
        [JsonProperty("accountHolder")]
        public string Koinh { get; set; }
        [JsonProperty("idType")]
        public string Type { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        [JsonProperty("IBAN")]
        public string Iban { get; set; }
        [JsonProperty("bankId")]
        public string Bankid { get; set; }
        [JsonProperty("bankDetails")]
        public string Bkext { get; set; }
        [JsonProperty("enableUpdate")]
        public string EnableUpdate { get; set; }
        [JsonProperty("action")]
        public string Action { get; set; }
    }


    
    public class IBANPostResponse
    {
        [JsonProperty("result")]
        public IBANNewData d { get; set; }
    }

    
    public partial class IBanListResponseModel
    {
        [JsonProperty("data")]
        public IBanListResponseModelD D { get; set; }
    }

    
    public partial class IBanListResponseModelD
    {
        [JsonProperty("IBANDetails")]
        public List<IBanResponseModelResults> Results { get; set; }
    }

    public partial class IBanResponseModelResults
    {
        [JsonProperty("IBAN")]
        public string Iban { get; set; }
        [JsonProperty("accountHolder")]
        public string Koinh { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("idType")]
        public string IdType { get; set; }
        [JsonProperty("authenticationUser")]
        public string Euser { get; set; }
        [JsonProperty("idTypeDescription")]
        public string IdtypeDesc { get; set; }
        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        public string bankDetails { get; set; }
    }
}
