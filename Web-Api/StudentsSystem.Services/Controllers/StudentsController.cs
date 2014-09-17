namespace StudentsSystem.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using StudentsSystem.Services.Models;
    using StudentSystem.Data;
    using StudentSystem.Data.Contracts;
    using StudentSystem.Models;

    public class StudentsController : ApiController
    {
        private readonly IStudentSystemData data;

        public StudentsController()
            : this(new StudentSystemData())
        {
        }

        public StudentsController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var students = data.Students
                               .All()
                               .Select(StudentModel.FromStudent);

            return Ok(students);
        }

        [HttpGet]
        public IHttpActionResult ById(string id)
        {
            var idAsGuid = new Guid(id);
            var student = data.Students
                .All()
                .Where(s => s.Id == idAsGuid)
                .Select(StudentModel.FromStudent)
                .FirstOrDefault();

            if (student == null)
            {
                return BadRequest("Student not found - invalid ID.");
            }

            return Ok(student);
        }

        [HttpPost]
        public IHttpActionResult Create(StudentModel student)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newStudent = new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName
            };

            this.data.Students.Add(newStudent);
            this.data.SaveChanges();

            student.Id = newStudent.Id;

            return Ok(student);
        }

        [HttpPut]
        public IHttpActionResult Update(string id, StudentModel student)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idAsGuid = new Guid(id);
            var existingStudent = this.data.Students
                .All()
                .FirstOrDefault(s => s.Id == idAsGuid);

            if (existingStudent == null)
            {
                return BadRequest("Student not found - invalid ID or not existing student at all");
            }

            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;

            this.data.SaveChanges();

            student.Id = new Guid(id);
            return Ok(student);
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            var idAsGuid = new Guid(id);
            var existingStudent = this.data.Students
                .All()
                .FirstOrDefault(s => s.Id == idAsGuid);

            if (existingStudent == null)
            {
                return BadRequest("Student does not exist - probably invalid ID");
            }

            this.data.Students.Delete(existingStudent);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddHomework(int hwId, string studentId)
        {
            var hw = this.data.Homeworks.All().FirstOrDefault(h => h.Id == hwId);
            if (hw == null)
            {
                return BadRequest("Homework not found - probably invalid hw ID");
            }

            var idAsGuid = new Guid(studentId);
            var student = this.data.Students.All().FirstOrDefault(s => s.Id == idAsGuid);
            if (student == null)
            {
                return BadRequest("Student not found - probably invalid id.");
            }

            student.Homeworks.Add(hw);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddCourse(int coucrseId, string studentId)
        {
            var course = this.data.Courses.All().FirstOrDefault(c => c.Id == coucrseId);
            if (course == null)
            {
                return BadRequest("Homework not found - probably invalid course ID");
            }

            var idAsGuid = new Guid(studentId);
            var student = this.data.Students.All().FirstOrDefault(s => s.Id == idAsGuid);
            if (student == null)
            {
                return BadRequest("Student not found - probably invalid id.");
            }

            student.Courses.Add(course);
            this.data.SaveChanges();

            return Ok();
        }
    }
}