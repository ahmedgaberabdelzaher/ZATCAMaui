using System;
namespace EGAZT.Helper
{
    public static class NameLocalization
    {
       public static string GetLocalizedName(string NameAr,string NameEn)
        {
            return App.IsArabic ? NameAr : NameEn;
        }
    }
}

