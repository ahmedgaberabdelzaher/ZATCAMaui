

namespace ZATCAMAUI.Models.NativeNafath
{
    public class IdInfoModel
	{
		public int idVersion { get; set; }
		public string idIssueDateH { get; set; }
		public string idIssueDateG{ get; set; }
		public string idExpiryDateH{ get; set; }
		public string idExpiryDateG { get; set; }
		public CardIssuePlaceModel cardIssuePlace { get; set; }
	}
}

