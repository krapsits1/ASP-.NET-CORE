using md4.Data;
using md4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace md4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Teachers = _context.Teachers.ToList(),
                Students = _context.Students.ToList(),
                Courses = _context.Courses.ToList(),
                Submissions = _context.Submissions.ToList(),
                Assignments = _context.Assignments.ToList() 
            };
            return View(viewModel);
        }

        //metode, kas izveido testa datus
        [HttpPost]
        public IActionResult CreateTestData()
        {
            // Add new test data
            var teacher = new Teacher { Name = "Emīls", Surname = "Vētra", Gender = "Male", ContractDate = DateTime.Now };
            _context.Teachers.Add(teacher);
            _context.SaveChanges();


            var student = new Student { Name = "Uldis", Surname = "Gurķis", Gender = "Male", StudentIdNumber = "SG23345" };
            _context.Students.Add(student);
            _context.SaveChanges();

            var course = new Course { Name = ".NET", TeacherId = teacher.Id };
            _context.Courses.Add(course);
            _context.SaveChanges();

            var assignment = new Assignment { Deadline = DateTime.Now.AddDays(7), CourseId = course.Id, Description = "MD4" };
            _context.Assignments.Add(assignment);
            _context.SaveChanges();

            var submission = new Submission { AssignmentId = assignment.Id, StudentId = student.Id, SubmissionTime = DateTime.Now, Score = 95 };
            _context.Submissions.Add(submission);

            _context.SaveChanges();


            // Display success message
            TempData["Message"] = "Test data created successfully!";
            return RedirectToAction("Index");
        }
    }
}
