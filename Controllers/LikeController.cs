using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using Thrift_Us.Data;
using Thrift_Us.Models;
using System.Security.Claims;

public class LikeController : Controller
{
    private readonly ThriftDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public LikeController(ThriftDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Like(int productId)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Redirect("/Identity/Account/Login?ReturnUrl=/Like/Like?productId=" + productId);
        }
        var user = await _userManager.GetUserAsync(User);

        if (user != null)
        {

            bool alreadyLiked = _context.Likes.Any(l => l.ApplicationUserId == user.Id && l.ProductId == productId);

            if (!alreadyLiked)
            {

                var like = new Like { ApplicationUserId = user.Id, ProductId = productId };
                _context.Likes.Add(like);
                await _context.SaveChangesAsync();
            }
        }

        return RedirectToAction("Index", "Home");
    }
}
