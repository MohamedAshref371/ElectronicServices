using System.Globalization;

namespace ElectronicServices
{
    public static class DateTimeExtensions
    {
        public static string ToStandardString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public static DateTime ToStandardDateTime(this string dateStr)
        {
            if (DateTime.TryParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                return result;
            
            return DateTime.MinValue;
        }

        public static readonly CultureInfo Arabic = new ("ar-EG");
        public static string GetArabic(this string dateStr, bool day)
        {
            if (DateTime.TryParseExact(dateStr, day ? "yyyy-MM-dd" : "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                return result.ToString(day ? "dddd" : "MMMM", Arabic);

            return "";
        }

        public static string ToCompleteStandardString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static DateTime ToCompleteStandardDateTime(this string dateStr)
        {
            if (DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                return result;

            return DateTime.MinValue;
        }
    }
}
