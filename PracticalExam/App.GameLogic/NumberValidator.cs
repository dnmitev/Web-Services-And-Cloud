using App.GameLogic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GameLogic
{
    public class NumberValidator : INumberValidator
    {
        public bool IsNumberValid(int number)
        {
            var numberToString = number.ToString();

            if (numberToString.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < numberToString.Length - 1; i++)
            {
                for (int j = i + 1; j < numberToString.Length; j++)
                {
                    if (numberToString[i] == numberToString[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
