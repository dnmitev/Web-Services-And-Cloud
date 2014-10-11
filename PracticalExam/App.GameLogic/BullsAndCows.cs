namespace App.GameLogic
{
    using App.GameLogic.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public class BullsAndCows : IBullsAndCows
    {
        public int BullsCount { get; set; }

        public int CowsCount { get; set; }
    }
}