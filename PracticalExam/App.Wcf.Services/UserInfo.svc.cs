using App.Data;
using App.Data.Contracts;
using App.Models;
using App.Wcf.Services.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace App.Wcf.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserInfo : IUserInfo
    {
        private const int DefaultCount = 10;

        private IAppData data;

        public UserInfo()
        {
            this.data = new AppData();
        }

        public ICollection<BasicUserInfoDataModel> Get()
        {
            return this.GetByPage(0);
        }

        public ICollection<BasicUserInfoDataModel> GetByPage(int p)
        {
            var users = this.data.Users
                            .All()
                            .OrderBy(u => u.UserName)
                            .Skip(DefaultCount * p)
                            .Take(p != 0 ? (p + 1) * DefaultCount - 1 : DefaultCount)
                            .Select(BasicUserInfoDataModel.FromUser)
                            .ToList();

            return users;
        }

        public UserDetails GetUserDetails(string id)
        {
            var user = this.data.Users
                           .All()
                           .FirstOrDefault(u => u.Id == id);

            return new UserDetails(user);
        }
    }
}