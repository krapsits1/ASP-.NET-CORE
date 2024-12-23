using md4.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace md4.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var courses = _context.Courses.Include(c => c.Teacher).ToList();
            return View(courses);
        }
        public ActionResult Details(int id)
        {
            var course = _context.Courses.Include(c => c.Teacher).FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

    }
}
