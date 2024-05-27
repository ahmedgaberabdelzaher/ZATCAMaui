
using ZATCAMAUI.Core.Helper;

namespace ZATCAMAUI.Models.NativeNafath
{
	public class CardIssuePlaceModel
	{
		public string arName { get; set; }
		public string enName { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(arName, enName);
            }
        }
    }
}

