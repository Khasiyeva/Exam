using AnyarMVC.Areas.Admin.ViewModels;
using AnyarMVC.DAL;
using AnyarMVC.Helpers;
using AnyarMVC.Migrations;
using AnyarMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AnyarMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Team> teams = _context.Teams.ToList(); 
            return View(teams);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVM createVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!createVm.ImageFile.CheckContent("image/"))
            {
                ModelState.AddModelError("ImageFile", "Enter the correct format");
                return View();
            }

            if (!createVm.ImageFile.CheckLength(2000000))
            {
                ModelState.AddModelError("ImageFile", "You cann't enter more than 2Mb");
                return View();
            }

            Team team = new Team() 
            { 
                Name=createVm.Name,
                Description=createVm.Description,
                Profession=createVm.Profession,
                ImgUrl=createVm.ImageFile.Upload(_env.WebRootPath,@"\Upload\Teams\")
            };

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Team team = _context.Teams.Find(id);

            UpdateVM updateVM = new UpdateVM() 
            {
                Name=team.Name,
                Description=team.Description,
                Profession=team.Profession,
                ImageFile=team.ImageFile 
            };

            return View(updateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateVM updateVM)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            if (!updateVM.ImageFile.CheckContent("image/"))
            {
                ModelState.AddModelError("ImageFile", "Enter the correct format");
                return View();
            }

            if (!updateVM.ImageFile.CheckLength(2000000))
            {
                ModelState.AddModelError("ImageFile", "You cann't enter more than 2Mb");
                return View();
            }
            updateVM.ImgUrl = updateVM.ImageFile.Upload(_env.WebRootPath, @"\Upload\Teams\");

            Team oldTeam= _context.Teams.Find(id);
            oldTeam.Name=updateVM.Name;
            oldTeam.Description=updateVM.Description;
            oldTeam.Profession=updateVM.Profession;
            oldTeam.ImgUrl=updateVM.ImgUrl;


            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Team team = _context.Teams.Find(id);
            _context.Teams.Remove(team);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
