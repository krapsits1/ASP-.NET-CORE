using md4.Data;
using md4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace md4.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var submissions = _context.Submissions
                .Include(s => s.Assignment)
                .Include(s => s.Student)
                .ToList();
            return View(submissions);
        }
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Assignments = _context.Assignments.ToList();
            ViewBag.Students = _context.Students.ToList();
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Submission submission)
        {
            if (ModelState.IsValid)
            {
                _context.Submissions.Add(submission);
                _context.SaveChanges();
                TempData["Message"] = "Submission created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Assignments = _context.Assignments.ToList();
            ViewBag.Students = _context.Students.ToList();
            return View(submission);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var submission = _context.Submissions
                .Include(s => s.Assignment)
                .Include(s => s.Student)
                .FirstOrDefault(s => s.Id == id);
            if (submission == null)
            {
                return NotFound();
            }

            ViewBag.Assignments = _context.Assignments.ToList();
            ViewBag.Students = _context.Students.ToList();

            return View(submission);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Submission submission)
        {
            Debug.WriteLine("EDIT CONTROLLLLERRRERERER");  // Logs validation errors

            if (id != submission.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(submission);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Debug.WriteLine("NAV VALID");  // Logs validation errors

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Debug.WriteLine(error.ErrorMessage);
                }

            }

            ViewBag.Assignments = _context.Assignments.ToList();
            ViewBag.Students = _context.Students.ToList();

            return View(submission);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var submission = _context.Submissions
                .Include(s => s.Assignment)
                .Include(s => s.Student)
                .FirstOrDefault(s => s.Id == id);
            if (submission == null)
            {
                return NotFound();
            }
            return View(submission);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var submission = _context.Submissions.Find(id);
            if (submission == null)
            {
                return NotFound();
            }
            _context.Submissions.Remove(submission);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
