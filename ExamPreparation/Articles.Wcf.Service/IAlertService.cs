namespace Articles.Wcf.Service
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    
    using Articles.Models;

    [ServiceContract]
    public interface IAlertService
    {
        [OperationContract]
        [WebGet(UriTemplate = "")]
        ICollection<Alert> Get();
    }
}