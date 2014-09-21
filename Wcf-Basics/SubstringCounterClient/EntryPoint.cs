namespace SubstringCounterConsoleClient
{
    using System;
    using System.Linq;

    using SubstringCounterConsoleClient.SubstringCounterServiceReference;

    class EntryPoint
    {
        static void Main()
        {
            string target = "My hip-hop is better than your hip-hop";
            string substr = "hip-hop";

            SubstringCounterClient client = new SubstringCounterClient();

            int count = client.GetSubstringCount(target, substr);

            Console.WriteLine(count);
        }
    }
}