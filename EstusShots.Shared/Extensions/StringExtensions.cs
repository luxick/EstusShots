using System.Text.RegularExpressions;

namespace EstusShots.Shared.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Forces a string into the "yyyy-mm-dd" format
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string DateMask(this string @this)
        {
            // Remove all non-numbers
            @this = Regex.Replace(@this, "[^0-9.]", "");
            if (@this.Length < 8) @this += "????????";
            return string.Format("{0}-{1}-{2}",
                @this.Substring(0, 4), // The year,
                @this.Substring(4, 2), // The month,
                @this.Substring(6, 2)); // The day);
        }
        
        /// <summary>
        /// Forces a string into the "HH:MM" format
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string HourMinuteMask(this string @this)
        {
            // Remove all non-numbers
            @this = Regex.Replace(@this, "[^0-9.]", "");
            if (@this.Length < 4) @this += "0000";
            return string.Format("{0}:{1}",
                @this.Substring(0, 2),  // The hours,
                @this.Substring(2, 4)); // The minutes
        }
    }
}