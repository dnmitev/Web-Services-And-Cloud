namespace DayOfWeekService
{
    using System;
    using System.Linq;
    using System.ServiceModel;

    [ServiceContract]
    public interface IDayOfWeek
    {
        [OperationContract]
        string GetDayOfWeek(DateTime date);
    }
}