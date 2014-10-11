
namespace App.Wcf.Services.DataModels
{
    using App.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.Serialization;
    using System.Web;

    [DataContractAttribute]
    public class BasicUserInfoDataModel
    {
        public static Expression<Func<User, BasicUserInfoDataModel>> FromUser
        {
            get
            {
                return u => new BasicUserInfoDataModel
                {
                    Id = u.Id,
                    Username = u.UserName
                };
            }
        }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Username { get; set; }
    }
}