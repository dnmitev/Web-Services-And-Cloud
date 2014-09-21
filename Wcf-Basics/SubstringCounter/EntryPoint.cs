namespace SubstringCounter
{
    using System;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using SubstringOccuranceCounter;
    
    internal class EntryPoint
    {
        private static void Main()
        {
            Uri serviceAddress = new Uri("http://localhost:6969/stubstringCounter");
            ServiceHost selfHost = new ServiceHost(typeof(SubstringCounter), serviceAddress);
            ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();

            serviceBehavior.HttpGetEnabled = true;
            selfHost.Description.Behaviors.Add(serviceBehavior);

            using (selfHost)
            {
                selfHost.Open();
                Console.WriteLine(string.Format("The service is started at endpoint {0}", serviceAddress));

                Console.WriteLine("Press [Enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}