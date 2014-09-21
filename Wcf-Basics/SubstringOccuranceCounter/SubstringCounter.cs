namespace SubstringOccuranceCounter
{
    using System;
    using System.Linq;

    public class SubstringCounter : ISubstringCounter
    {
        public int GetSubstringCount(string target, string substr)
        {
            int count = 0;
            int n = 0;

            while ((n = target.IndexOf(substr, n, StringComparison.InvariantCulture)) != -1)
            {
                n += substr.Length;
                count++;
            }

            return count;
        }
    }
}