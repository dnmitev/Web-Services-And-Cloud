namespace App.GameLogic.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public interface IBullsAndCowsCounter
    {
        BullsAndCows CountBullsAndCows(int? secretNumber, int guessNumber);
    }
}