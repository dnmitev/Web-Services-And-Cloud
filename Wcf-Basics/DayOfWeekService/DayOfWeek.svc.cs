namespace DayOfWeekService
{
    using System;
    using System.Globalization;
    using System.Linq;

    public class DayOfWeek : IDayOfWeek
    {
        public string GetDayOfWeek(DateTime date)
        {
            var culture = CultureInfo.CreateSpecificCulture("bg-BG");
            var day = culture.DateTimeFormat.GetDayName(date.DayOfWeek);

            return day;
        }
    }
}