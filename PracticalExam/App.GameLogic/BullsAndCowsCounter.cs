namespace App.GameLogic
{
    using App.GameLogic.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BullsAndCowsCounter : IBullsAndCowsCounter
    {
        public BullsAndCows CountBullsAndCows(int? secretNumber, int guessNumber)
        {
            if (secretNumber == null)
            {
                throw new ArgumentNullException("Secret number cannot be null");
            }

            var result = new BullsAndCows();

            var secretString = secretNumber.ToString();
            var guessString = guessNumber.ToString();

            for (int i = 0; i < secretString.Length; i++)
            {
                for (int j = 0; j < secretString.Length; j++)
                {
                    if (i == j && secretString[i] == guessString[j])
                    {
                        // Bull found
                        result.BullsCount++;
                    }
                    else if (secretString[i] == guessString[j])
                    {
                        // Cow found
                        result.CowsCount++;
                    }
                }
            }

            return result;
        }
    }
}