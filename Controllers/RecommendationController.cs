using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using Thrift_Us.Data;
using Thrift_Us.Models;
using System.Security.Claims;
using Humanizer;
using Thrift_Us.Services.Interface;

public class RecommendationController : Controller
{
    private readonly IRecommendationService _recommendationService;

    public RecommendationController(IRecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    [HttpGet]
    public IActionResult GetRecommendedProducts()
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var recommendedProducts = _recommendationService.GetRecommendedProducts(userId);

        return View(recommendedProducts);
    }
}