
namespace ZATCAMAUI.Models
{

    public class IBanManagementListModel
    {
        
        public class IbanListSetResult
        {

            public string FormGuid { get; set; }

            public string Status { get; set; }

            public string StatusDesc { get; set; }

            public string Tin { get; set; }

            public string AgreeFg { get; set; }

            public string IdtypeDesc { get; set; }

            public string ActiveIban { get; set; }

            public string Fbnum { get; set; }

            // public string VisibleUpdate { get; set; }
            public string StatusText { get; set; }


            public string VisibleUpdate { get; set; }
            
            public string Koinh { get; set; }
            public string Type { get; set; }
            public bool isUpdateEnabled { get; set; }
            public bool isUpdateDisabled { get; set; }
            public string Idnumber { get; set; }
            public string Iban { get; set; }
            public string Bankid { get; set; }
            public string Bkext { get; set; }
            //public string EnableUpdate { get; set; }
            public string Action { get; set; }
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



            public string Bankid { get; set; }


            public string Bkext { get; set; }
        }

        
        public class BankListSet
        {


            public List<Result> results { get; set; }
        }

        
        public class IdNumberListSetResult
        {


            public string IdType { get; set; }


            public string IdNumber { get; set; }

            public string Actnm { get; set; }

        }

        
        public class IdNumberListSet
        {


            public List<IdNumberListSetResult> results { get; set; }
        }

        
        public class IdTypeListSetResult
        {

            public string IdType { get; set; }


            public string IdDesc { get; set; }
        }

        
        public class IdTypeListSet
        {


            public List<IdTypeListSetResult> results { get; set; }
        }

        
        public class IBanModelD
        {


            public Metadata __metadata { get; set; }


            public string Tin { get; set; }


            public string Fbguid { get; set; }

            public bool AutoPopFg { get; set; }

            public string Euser { get; set; }

            public bool isUpdateFlag { get; set; }

            public string Name { get; set; }


            public IbanListSet IbanListSet { get; set; }


            public BankListSet BankListSet { get; set; }


            public IdNumberListSet IdNumberListSet { get; set; }


            public IdTypeListSet IdTypeListSet { get; set; }
        }

        
        public class IBanAccountManagementResponseModel
        {


            public IBanModelD d { get; set; }
        }

    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class IBanFormGUIDModel
    {
        public Metadata __metadata { get; set; }
        public string VisibleUpdate { get; set; }
        public string Type { get; set; }
        public string Tin { get; set; }
        public string StatusDesc { get; set; }
        public string Status { get; set; }
        public string Koinh { get; set; }
        public string IdtypeDesc { get; set; }
        public string Idnumber { get; set; }
        public string Iban { get; set; }
        public string FormGuid { get; set; }
        public string Fbnum { get; set; }
        public string EnableUpdate { get; set; }
        public string Bkext { get; set; }
        public string Bankid { get; set; }
        public string AgreeFg { get; set; }
        public string ActiveIban { get; set; }
        public string Action { get; set; }
    }

    public class IbanAccountFormGuidResponse
    {
        public IBanFormGUIDModel d { get; set; }
    }




    
    public class IBANPostRequest
    {
        public string Action { get; set; }
        public string Fbnum { get; set; }
        public string Koinh { get; set; }
        public string Type { get; set; }
        public string IdtypeDesc { get; set; }
        public string Idnumber { get; set; }
        public string Iban { get; set; }
        public string Bankid { get; set; }
        public string Bkext { get; set; }
        public string AgreeFg { get; set; }
        public string Tin { get; set; }
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
        public string FormGuid { get; set; }
        public string Status { get; set; }
        public string StatusDesc { get; set; }
        public string Tin { get; set; }
        public string AgreeFg { get; set; }
        public string IdtypeDesc { get; set; }
        public string ActiveIban { get; set; }
        public string Fbnum { get; set; }
        public string VisibleUpdate { get; set; }
        public string Koinh { get; set; }
        public string Type { get; set; }
        public string Idnumber { get; set; }
        public string Iban { get; set; }
        public string Bankid { get; set; }
        public string Bkext { get; set; }
        public string EnableUpdate { get; set; }
        public string Action { get; set; }
    }


    
    public class IBANPostResponse
    {
        public IBANNewData d { get; set; }
    }

    
    public partial class IBanListResponseModel
    {
        public IBanListResponseModelD D { get; set; }
    }

    
    public partial class IBanListResponseModelD
    {
        public List<IBanResponseModelResults> Results { get; set; }
    }

    public partial class IBanResponseModelResults
    {
        public string Iban { get; set; }
        public string Koinh { get; set; }
        public string FormGuid { get; set; }
        public string IdType { get; set; }
        public string Euser { get; set; }
        public string IdtypeDesc { get; set; }
        public string IdNumber { get; set; }
        public string Tin { get; set; }
    }


}
