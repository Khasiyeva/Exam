using AnyarMVC.DAL;
using AnyarMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnyarMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Team> teams = _context.Teams.ToList();
            return View(teams);
        }

    }
}
