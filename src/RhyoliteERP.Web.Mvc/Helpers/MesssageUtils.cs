using System;

namespace RhyoliteERP.Web.Helpers
{
    public static class MesssageUtils
    {
        public static int CalculateParts(string message)
        {
            const int creditPerPart = 1;
            const int singleMessageLength = 160;
            const int multiMessageLength = 153;
            double len = message.Length;
            if (len <= singleMessageLength)
                return creditPerPart;
            return (int)(Math.Ceiling(len / multiMessageLength) * creditPerPart);
        }
    }
}
