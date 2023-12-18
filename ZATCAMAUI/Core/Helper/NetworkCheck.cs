namespace ZATCAMAUI.Core.Helper
{
    public static class NetworkCheck
    {
        public static bool IsInternet()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
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