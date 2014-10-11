namespace App.GameLogic.Contracts
{
    using System;
    using System.Linq;

    public interface INumberValidator
    {
        bool IsNumberValid(int number);
    }
}