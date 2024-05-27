using ZATCAMAUI.Core.Helper;
namespace ZATCAMAUI.Models.CustomServices.Tawreed
{
	public class CrTinNoModelOld
	{

        public string englishName { get; set; }
        public string arabicName { get; set; }
        public string TINNumber { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(arabicName, englishName);
            }
        }
    }

    public class CrTinNoModel
    {
        public string count { get; set; }
        public List<Taxpayer> taxpayers { get; set; }
    }

       public class IdType
    {
        public string code { get; set; }
        public string descriptionArabic { get; set; }
    }



    public class Taxpayer
    {
        public string TINNumber { get; set; }
        public string idNumber { get; set; }
        public string idStatus { get; set; }
        public string idValidFrom { get; set; }
        public string idValidTo { get; set; }
        public IdType idType { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string statusDescription { get; set; }
        public long mobile { get; set; }
        public string email { get; set; }
        public string vatAccountNumber { get; set; }
        public VatGroup vatGroup { get; set; }
    }

    public class VatGroup
    {
        public string status { get; set; }
        public string statusDescription { get; set; }
    }

}

