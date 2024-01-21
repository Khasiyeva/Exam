using AnyarMVC.Helpers;
using AnyarMVC.Models;
using AnyarMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnyarMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = new AppUser()
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                Surname = registerVM.Surname,
                UserName = registerVM.Username

            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View();
                }
            }


            await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());
            await _signInManager.SignInAsync(user,true);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
                    if (user == null)
                    {
                        user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
                        if (user == null)
                        {
                            ModelState.AddModelError("", "User not found");
                             return View();
                        }
                    }


           var result=await _signInManager.CheckPasswordSignInAsync(user, loginVM.Password,false);
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username/Email or Password is incorrect");
                return View();
            }

           await _signInManager.SignInAsync(user,true);

            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> CreateRole()
        {

            var role = new IdentityRole("Admin");
            await _roleManager.CreateAsync(role);
            role = new IdentityRole("Member");
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
