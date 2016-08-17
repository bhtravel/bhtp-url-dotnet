using System;

namespace Bhtp.Url.Utility
{
    internal static class Extensions
    {
        public static string ToIso8601(this DateTime date)
        {
            string formattedDate = null;

            if (date != null)
            {
                formattedDate = date.ToString(Constants.DateFormat);
            }

            return formattedDate;
        }
    }
}