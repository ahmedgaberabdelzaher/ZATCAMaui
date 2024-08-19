using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models.CountryInquiries
{
    public class CountryInquiresResponseModel
    {
        public Header header { get; set; }
        public Data data { get; set; }

        public class Country
        {
            public string countryCode { get; set; }
            public string countryName { get; set; }
            public string countryDescription { get; set; }
            public string language { get; set; }
            public string telephoneCountryCode { get; set; }
        }

        public class Data
        {
            public List<Country> countries { get; set; }
        }

        public class Header
        {
            public string requestID { get; set; }
            public Status status { get; set; }
        }

        public class Status
        {
            public string code { get; set; }
            public string description { get; set; }
        }


    }
}
