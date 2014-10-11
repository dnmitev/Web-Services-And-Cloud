namespace App.GameLogic.Contracts
{
    using System;
    using System.Linq;

    public interface IBullsAndCows
    {
        int BullsCount { get; set; }

        int CowsCount { get; set; }
    }
}