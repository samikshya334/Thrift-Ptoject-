using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using Thrift_Us.Data;
using Thrift_Us.Models;

namespace Thrift_Us.Areas.Admin.Controllers
{

    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ThriftDbContext _context;

        public AdminController(UserManager<IdentityUser> userManager, ThriftDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }
        public IActionResult Dashboards()
        {
       
            return View();
        }
        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                if (claim != null)
                {

                    var users = _context.ApplicationUsers.ToList();
                    return View(users);
                }
            }


            return RedirectToAction("Login", "Account");
        }
        
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

  
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
   
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email")] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(id);

                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
               

                var result = await _userManager.UpdateAsync(existingUser);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(user);
        }


        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
           
                return RedirectToAction("Index");
            }
            else
            {
        
                return RedirectToAction("Index");
            }
        }


        
    }
}

