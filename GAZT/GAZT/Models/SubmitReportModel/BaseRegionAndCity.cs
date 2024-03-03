using System.Collections.Generic;

namespace EGAZT.Models.SubmitReportModel
{
  /*  public class BaseRegionAndCity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }*/


    public class RegionsListModel
    {
        public List<BaseRegionAndCity> regions { get; set; }
    }

    public class CitiesListModel
    {
        public List<BaseRegionAndCity> cities { get; set; }
    }

    public class LookUpsModel
    {
        public List<BaseRegionAndCity> lookUpList { get; set; }
    }
    public class BaseRegionAndCity
    {
        public string id { get; set; }
        public string name { get; set; }
        public string Id { get { return id; } }
        public string Name { get { return name; } }
    }
}
