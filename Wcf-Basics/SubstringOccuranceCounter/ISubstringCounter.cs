namespace SubstringOccuranceCounter
{
    using System;
    using System.Linq;
    using System.ServiceModel;

    [ServiceContract]
    public interface ISubstringCounter
    {
        [OperationContract]
        int GetSubstringCount(string target, string substr);
    }
}