using md4.Data;
using md4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace md4.Controllers
{
    public class AssignmentsController : Controller
    {
        //get contex data from database
        private readonly ApplicationDbContext _context;

        public AssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //index metode atgriež skatu ar assignments datiem no datubāzes
        public ActionResult Index()
        {
            var assignments = _context.Assignments.Include(a => a.Course).ToList();
            return View(assignments);
        }
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _context.Assignments.Add(assignment);
                _context.SaveChanges();
                TempData["Message"] = "Assignment created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Courses = _context.Courses.ToList();
            return View(assignment);
        }
        //edit metode
        [Authorize]
        public ActionResult Edit(int id)
        {
            var assignment = _context.Assignments.Include(a => a.Course).FirstOrDefault(a => a.Id == id);
            if (assignment == null)
            {
                return NotFound();
            }
            ViewBag.Courses = _context.Courses.ToList();

            return View(assignment);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Assignment assignment)
        {
    
            Debug.WriteLine("EDIT CONTROLLLLERRRERERER");  // Logs validation errors

            if (id != assignment.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                Debug.WriteLine("IR VALID");  // Logs validation errors

                _context.Update(assignment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Debug.WriteLine("NAV VALID");  // Logs validation errors

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Debug.WriteLine(error.ErrorMessage);  // Logs validation errors
                }

            }

            // Repopulate the dropdown if validation fails
            ViewBag.Courses = _context.Courses.ToList();
            return View(assignment);
        }



        [Authorize]
        public ActionResult Delete(int id)
        {
            var assignments = _context.Assignments.Include(a => a.Course).FirstOrDefault(a => a.Id == id);
            if (assignments == null)
            {
                return NotFound();
            }
            return View(assignments);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var assignments = _context.Assignments.Find(id);
            if (assignments == null)
            {
                return NotFound();
            }
            _context.Assignments.Remove(assignments);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
