using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thrift_Us.Data;

namespace Thrift_Us.Controllers
{
    public class SellerController : Controller
    {
        private readonly ThriftDbContext _context;
        public SellerController( ThriftDbContext context)
        {
           
            _context = context;

        }
        public IActionResult Dashboards()
        {

            string currentUserName = User.Identity.Name;

          /*
            var userOrders = _context.OrderHeaders
                .Include(o => o.OrderDetails) 
                .Where(o => o.ApplicationUserId == currentUserName)
                .ToList();

            int productCount = _context.Products.Count();
            int orderCount = userOrders.Count;

            ViewBag.ProductCount = productCount;
            ViewBag.OrderCount = orderCount;*/
            return View();
        }
    }
}
