using System;
using System.Collections.ObjectModel;

namespace EGAZT.Models.CustomServices.BalaghModels
{

    public class BalaghLocations
    {
        public int portcd { get; set; }
        public int porttype { get; set; }
        public string portname { get; set; }
        public string portengname { get; set; }
        public bool portstatus { get; set; }
        public string Name
        {
            get
            {
                if (!App.IsArabic)
                {
                    if (!String.IsNullOrEmpty(portengname))
                    {
                        return portengname;
                    }
                }
                return portname;
            }
        }
        public ObservableCollection<object> ticket { get; set; }
    }

    public class BalaghLocationsResponse
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ObservableCollection<BalaghLocations> data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }


}
