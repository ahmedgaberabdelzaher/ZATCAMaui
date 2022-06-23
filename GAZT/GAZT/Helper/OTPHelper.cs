using System;
using System.Text;

namespace EGAZT.Helper
{
    public static class OTPHelper
    {
        public static string Generate()
        {
            var builder = new StringBuilder("1");
            var numStr = "";
            for (var j = 0; j < 3; j++)
            {
                builder.Append("0");
            }
            numStr = builder.ToString();
            var num = int.Parse(numStr);
            var generator = new Random();
            var randomNum = generator.Next(0, 10000).ToString("D4");
            return randomNum;
        }
    }
}
