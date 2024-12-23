using md4.Data;
using md4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace md4.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }
        //index metode atgriež skolotāju skatu ar skolotāju datiem no datubāzes
        public ActionResult Index()
        {
            var Teachers = _context.Teachers.ToList();
            return View(Teachers);
        }

   
    }
}

