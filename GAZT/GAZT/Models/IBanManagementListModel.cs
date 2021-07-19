using EGAZT.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
    public class IBanManagementListModel
    {
        [Preserve(AllMembers = true)]
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
                            StatusText = "Deactivate";
                            isUpdateEnabled = true;
                            isUpdateDisabled = false;
                        }
                        else if (ActiveIban == "")
                        {
                            StatusText = "Activate";
                            isUpdateEnabled = true;
                            isUpdateDisabled = false;
                        }
                    }
                    else
                    {
                        //IsEnabled Update or disble update button
                        if (EnableUpdate == "X")
                        {
                            StatusText = "Update";
                            isUpdateEnabled = true;
                            isUpdateDisabled = false;
                        }
                        else if (EnableUpdate == "")
                        {
                            StatusText = "Update";
                            isUpdateEnabled = false;
                            isUpdateDisabled = true;
                        }
                    }

                }
            }




            public string Koinh { get; set; }

            public string Type { get; set; }

            public bool isUpdateEnabled {get;set;}
            public bool isUpdateDisabled {get;set;}

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
                            StatusText = "Deactivate";
                            isUpdateEnabled = true;
                            isUpdateDisabled = false;
                        }
                        else if (ActiveIban == "")
                        {
                            StatusText = "Activate";
                            isUpdateEnabled = true;
                            isUpdateDisabled = false;
                        }
                    }
                    else
                    {
                        //IsEnabled Update or disble update button
                        if (EnableUpdate == "X")
                        {
                            StatusText = "Update";
                            isUpdateEnabled = true;
                            isUpdateDisabled = false;
                        }
                        else if (EnableUpdate == "")
                        {
                            StatusText = "Update";
                            isUpdateEnabled = false;
                            isUpdateDisabled = true;
                        }
                    }

                }
            }

        }
        [Preserve(AllMembers = true)]
        public class IbanListSet
        {

           
            public List<IbanListSetResult> results { get; set; }
        }

        [Preserve(AllMembers = true)]
        public class Result
        {

         
        
        public string Bankid { get; set; }

        
        public string Bkext { get; set; }
    }

        [Preserve(AllMembers = true)]
        public class BankListSet
        {

            
            public List<Result> results { get; set; }
        }

        [Preserve(AllMembers = true)]
        public class IdNumberListSetResult
        {

        
        public string IdType { get; set; }

        
        public string IdNumber { get; set; }
    }

        [Preserve(AllMembers = true)]
        public class IdNumberListSet
        {

            
            public List<IdNumberListSetResult> results { get; set; }
        }

        [Preserve(AllMembers = true)]
        public class IdTypeListSetResult
        {
        
        public string IdType { get; set; }

        
        public string IdDesc { get; set; }
    }

        [Preserve(AllMembers = true)]
        public class IdTypeListSet
        {

            
            public List<IdTypeListSetResult> results { get; set; }
        }

        [Preserve(AllMembers = true)]
        public class IBanModelD
        {

            
            public Metadata __metadata { get; set; }

            
            public string Tin { get; set; }

            
            public string Fbguid { get; set; }

            
            public string Euser { get; set; }

            
            public string Name { get; set; }

            
            public IbanListSet IbanListSet { get; set; }

            
            public BankListSet BankListSet { get; set; }

            
            public IdNumberListSet IdNumberListSet { get; set; }

            
            public IdTypeListSet IdTypeListSet { get; set; }
        }

        [Preserve(AllMembers = true)]
        public class IBanAccountManagementResponseModel
        {

            
            public IBanModelD d { get; set; }
        }

    }


    [Preserve(AllMembers = true)]
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

    [Preserve(AllMembers = true)]
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


    [Preserve(AllMembers = true)]
    public class IBANPostResponse
    {
        public IBANNewData d { get; set; }
    }

    [Preserve(AllMembers = true)]
    public partial class IBanListResponseModel
    {
        public IBanListResponseModelD D { get; set; }
    }

    [Preserve(AllMembers = true)]
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
