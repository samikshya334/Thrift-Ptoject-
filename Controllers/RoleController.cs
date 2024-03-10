using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Thrift_Us.Controllers
{
 
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;

        }
        [HttpGet]//to list the roles data
        public IActionResult Index()
        {
            var role = _roleManager.Roles;
            return View(role);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(IdentityRole roles) 

        {
            try
            {
                //to check if there exits any duplication of role or not if not then create the new role i.e duplication hataucha )
                if (!_roleManager.RoleExistsAsync(roles.Name).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roles.Name)).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
