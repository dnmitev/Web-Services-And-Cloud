namespace StudentsSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using StudentsSystem.Services.Models;
    using StudentSystem.Data;
    using StudentSystem.Data.Contracts;
    using StudentSystem.Models;

    public class CoursesController : ApiController
    {
        private readonly IStudentSystemData data;

        public CoursesController() : this(new StudentSystemData())
        {
        }

        public CoursesController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var courses = this.data.Courses
                              .All()
                              .Select(CourseModel.FromCourse);

            return Ok(courses);
        }

        [HttpPost]
        public IHttpActionResult Create(CourseModel course)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCourse = new Course
            {
                Name = course.Name,
                Description = course.Description
            };

            this.data.Courses.Add(newCourse);
            this.data.SaveChanges();

            course.Id = newCourse.Id;

            return Ok(course);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, CourseModel course)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCourse = this.data.Courses.All().FirstOrDefault(c => c.Id == id);
            if (existingCourse == null)
            {
                return BadRequest("Course not found - probably invalid id");
            }

            existingCourse.Name = course.Name;
            existingCourse.Description = course.Description;

            this.data.SaveChanges();

            course.Id = existingCourse.Id;

            return Ok(course);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingCourse = this.data.Courses
                                     .All()
                                     .FirstOrDefault(c => c.Id == id);

            if (existingCourse == null)
            {
                return BadRequest("Course not found - probably invalid id");
            }

            this.data.Courses.Delete(existingCourse);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddHomerwork(int courseId, int hwId)
        {
            var hw = this.data.Homeworks
                         .All()
                         .FirstOrDefault(h => h.Id == hwId);
            if (hw == null)
            {
                return BadRequest("Homework not found - probably invalid id");
            }

            var course = this.data.Courses
                             .All()
                             .FirstOrDefault(c => c.Id == courseId);
            if (course ==null)
            {
                return BadRequest("Course not found - probably invalid id");
            }

            course.Homeworks.Add(hw);
            this.data.SaveChanges();

            return Ok();
        }
    }
}