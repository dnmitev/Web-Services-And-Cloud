namespace StudentsSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using StudentsSystem.Services.Models;
    using StudentSystem.Data;
    using StudentSystem.Data.Contracts;
    using StudentSystem.Models;

    public class HomeworksController : ApiController
    {
        private readonly IStudentSystemData data;

        public HomeworksController() : this(new StudentSystemData())
        {
        }

        public HomeworksController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var homeworks = this.data.Homeworks
                .All()
                .Select(HomeworkModel.FromHomework);

            return Ok(homeworks);
        }

        [HttpPost]
        public IHttpActionResult Create(HomeworkModel homework)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newHomework = new Homework
            {
                Deadline = homework.Deadline,
                TimeSent = homework.TimeSent
            };

            this.data.Homeworks.Add(newHomework);
            this.data.SaveChanges();

            homework.Id = newHomework.Id;
            return Ok(homework);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, HomeworkModel homework)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingHomework = this.data.Homeworks
                .All()
                .FirstOrDefault(h => h.Id == id);

            if (existingHomework == null)
            {
                return BadRequest("Homework not found - probably invalid ID");
            }

            existingHomework.Deadline = homework.Deadline;
            existingHomework.TimeSent = homework.TimeSent;

            this.data.SaveChanges();

            homework.Id = existingHomework.Id;
            return Ok(homework);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingHomework = this.data.Homeworks
                .All()
                .FirstOrDefault(h => h.Id == id);

            if (existingHomework ==null)
            {
                return BadRequest("Homework not found - probably invalid id");
            }

            this.data.Homeworks.Delete(existingHomework);
            this.data.SaveChanges();

            return Ok();
        }
    }
}