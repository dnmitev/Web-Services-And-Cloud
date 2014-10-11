namespace App.Utilities.Contracts
{
    using System;
    using System.Linq;

    public interface IRandomProvider
    {
        int GetRandomInt(int minValue, int maxValue);
    }
}