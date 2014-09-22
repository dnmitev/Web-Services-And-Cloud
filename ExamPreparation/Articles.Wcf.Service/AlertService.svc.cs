namespace Articles.Wcf.Service
{
    using Articles.Data;
    using Articles.Data.Contracts;
    using Articles.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Text;

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AlertService : IAlertService
    {
        private IAppData data;

        public AlertService()
        {
            this.data = new AppData();
        }

        public ICollection<Alert> Get()
        {
            var alerts = this.data.Alerts.All().ToArray();
            return alerts;
        }
    }
}