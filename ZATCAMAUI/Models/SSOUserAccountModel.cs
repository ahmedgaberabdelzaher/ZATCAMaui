
namespace ZATCAMAUI.Models
{
    public class SSOUserAccount
    {
        public string username { get; set; }
        public string token { get; set; }
        public string GUID { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }

    public class SSOUserAccountModelResponse
    {
        public SSOUserAccountModel data { get; set;}
    }

    public class SSOUserAccountModel
    {
        public List<SSOUserAccount> SSOUserAccounts { get; set;}
    }

}
