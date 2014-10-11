namespace App.Utilities
{
    using System;
    using System.Linq;
    
    using App.Utilities.Contracts;

    public sealed class RandomProvider : IRandomProvider
    {
        private static Random randomGenerator;
        private static IRandomProvider randomProvider;

        private RandomProvider()
        {
            randomGenerator = new Random();
        }

        public static IRandomProvider Instance
        {
            get
            {
                if (randomProvider == null)
                {
                    randomProvider = new RandomProvider();
                }

                return randomProvider;
            }
        }

        public int GetRandomInt(int minValue = 0, int maxValue = int.MaxValue)
        {
            int number = randomGenerator.Next(minValue, maxValue + 1);

            return number;
        }
    }
}