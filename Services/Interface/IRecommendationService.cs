using Thrift_Us.Models;

namespace Thrift_Us.Services.Interface
{
    public interface IRecommendationService
    {
        IEnumerable<Product> GetRecommendedProducts(string userId);
    }
}
