using Xamarin.Essentials;

namespace EGAZT.Helper
{
    public static class NetworkCheck
    {
        public static bool IsInternet()
        {
            if (Connectivity.NetworkAccess==NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}