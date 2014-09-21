namespace DayOfWeekConsumer
{
    using System;
    using System.Linq;

    using DayOfWeekConsumer.DayOfWeekServiceReference;

    internal class EntryPoint
    {
        private static void Main()
        {
            DayOfWeekClient client = new DayOfWeekClient();

            var today = client.GetDayOfWeek(DateTime.Now);

            Console.WriteLine(today);
        }
    }
}