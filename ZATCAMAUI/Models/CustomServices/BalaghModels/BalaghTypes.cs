using System.Collections.ObjectModel;

namespace ZATCAMAUI.Models.CustomServices.BalaghModels
{

    public class Balagh
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nameen { get; set; }
        public string Name
        {
            get
            {
                if (!App.IsArabic)
                {
                    if (!string.IsNullOrEmpty(nameen))
                    {
                        return nameen;
                    }
                }
                return name;
            }
        }

        public int tickettypeid { get; set; }
        public List<object> ticket { get; set; }
    }

    public class BalaghTypes
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ObservableCollection<Balagh> data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }

}
