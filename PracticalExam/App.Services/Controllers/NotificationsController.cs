namespace App.Services.Controllers
{
    using App.Data;
    using App.Data.Contracts;
    using App.Models;
    using App.Services.DataModels;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    [RoutePrefix("api/notifications")]
    public class NotificationsController : BaseApiController
    {
        private const int DefaultCount = 10;

        // TODO: Uncomment to use without Ninject if there are problems
        //public NotificationsController()
        //    : this(new AppData())
        //{
        //}

        public NotificationsController(IAppData data)
            : base(data)
        {
        }

        [HttpGet]
        [Authorize]
        [Route("")]
        public IHttpActionResult All()
        {
            return this.All(0);
        }

        [HttpGet]
        [Authorize]
        [Route("")]
        public IHttpActionResult All(int page)
        {
            var currentUserId = this.GetCurrentUserId();
            var notifications = this.Data.Notifications
                                    .All()
                                    .Where(n => n.State == NotificationState.Unread && n.UserId == currentUserId)
                                    .OrderByDescending(n => n.DateCreated)
                                    .Skip(DefaultCount * page)
                                    .Take(page != 0 ? (page + 1) * DefaultCount - 1 : DefaultCount)
                                    .Select(NotificationDataModel.FromNotification);

            return Ok(notifications);
        }

        [HttpGet]
        [Authorize]
        [Route("next")]
        public HttpResponseMessage GetOldestUnreadNotification()
        {
            var currentUserId = this.GetCurrentUserId();
            var notification = this.Data.Notifications
                                   .All()
                                   .Where(n => n.State == NotificationState.Unread && n.UserId == currentUserId)
                                   .OrderByDescending(n => n.DateCreated)
                                   .Select(NotificationDataModel.FromNotification)
                                   .LastOrDefault();

            if (notification == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotModified);
            }

            return Request.CreateResponse(HttpStatusCode.OK, notification);
        }

        private string GetCurrentUserId()
        {
            var userId = this.User.Identity.GetUserId();
            return userId;
        }
    }
}