using App.Models;
using App.Wcf.Services.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace App.Wcf.Services
{
    [ServiceContract]
    public interface IUserInfo
    {
        [OperationContract]
        [WebGet(UriTemplate = "users")]
        ICollection<BasicUserInfoDataModel> Get();

        [OperationContract]
        [WebGet(UriTemplate = "users?page={p}")]
        ICollection<BasicUserInfoDataModel> GetByPage(int p);

        [OperationContract]
        [WebGet(UriTemplate = "users/{id}")]
        UserDetails GetUserDetails(string id);
    }
}
