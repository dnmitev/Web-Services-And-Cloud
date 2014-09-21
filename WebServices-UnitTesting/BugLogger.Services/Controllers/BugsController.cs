namespace BugLogger.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using BugLogger.Data;
    using BugLogger.Data.Contracts;
    using BugLogger.Models;
    using BugLogger.Services.Models;

    public class BugsController : ApiController
    {
        private readonly IAppData data;

        public BugsController()
            : this(new AppData())
        {
        }

        public BugsController(IAppData data)
        {
            this.data = data;
        }

        [Route("bugs")]
        [HttpGet]
        public IHttpActionResult All()
        {
            var bugs = this.data.Bugs
                           .All()
                           .Select(BugModel.BugTemplate);

            return this.Ok(bugs);
        }

        [Route("bugs")]
        [HttpGet]
        public IHttpActionResult AllAfterDate(DateTime date)
        {
            var bugs = this.data.Bugs
                           .All()
                           .Where(b => b.LogDate >= date)
                           .Select(BugModel.BugTemplate);

            return this.Ok(bugs);
        }

        [Route("bugs")]
        [HttpGet]
        public IHttpActionResult AllWithStatus(Status status)
        {
            var bugs = this.data.Bugs
                           .All()
                           .Where(b => b.Status == status)
                           .Select(BugModel.BugTemplate);

            return this.Ok(bugs);
        }

        [Route("bugs")]
        [HttpPost]
        public IHttpActionResult Create(string bugLogText)
        {
            if (string.IsNullOrEmpty(bugLogText))
            {
                return this.BadRequest("Bug log text cannot be null or empty");
            }

            if (bugLogText.Length <= 5)
            {
                return this.BadRequest("Bug log text cannot be less than 5 symbols");
            }

            var bugToAdd = new Bug
            {
                LogText = bugLogText,
                LogDate = DateTime.Now,
                Status = Status.Pending
            };

            this.data.Bugs.Add(bugToAdd);
            this.data.SaveChanges();

            var addedBug = new BugModel
            {
                Id = bugToAdd.Id,
                LogText = bugToAdd.LogText,
                LogDate = bugToAdd.LogDate,
                Status = bugToAdd.Status
            };

            return this.Ok(addedBug);
        }

        [Route("bugs")]
        [HttpPut]
        public IHttpActionResult ChangeBugStatus(int bugId, Status status)
        {
            var bugToBeChanged = this.data.Bugs
                                     .All()
                                     .FirstOrDefault(b => b.Id == bugId);

            if (bugToBeChanged == null)
            {
                return this.BadRequest("Bug not found - probably invalid bug ID");
            }

            if (bugToBeChanged.Status == status)
            {
                return BadRequest("Non sense in changing the status with the same status");
            }

            bugToBeChanged.Status = status;
            this.data.SaveChanges();

            var updatedBug = new BugModel
            {
                Id = bugToBeChanged.Id,
                LogText = bugToBeChanged.LogText,
                LogDate = bugToBeChanged.LogDate,
                Status = bugToBeChanged.Status
            };

            return this.Ok(updatedBug);
        }
    }
}