using Thrift_Us.Data;
using Thrift_Us.Models;
using Thrift_Us.Services.Interface;

public class RecommendationService : IRecommendationService
{
    private readonly ThriftDbContext _dbContext;
    private const double Threshold = 0.5; // Adjust as needed
    private const int MaxRecommendations = 10;

    public RecommendationService(ThriftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Product> GetRecommendedProducts(string userId)
    {
        // Get liked productId from all the user except the currentuser
        var likedProductIdsByAllUsers = _dbContext.Likes
            .Where(l => l.ApplicationUserId != userId)
            .Select(l => l.ProductId)
            .ToList();

        var recommendedProducts = new List<Product>();

        foreach (var product in _dbContext.Products)
        {
            // Each product ko similariry calculate garne
            double similarity = CalculateCosineSimilarity(userId, product.ProductId, likedProductIdsByAllUsers);

            // yadi product chei kunei pani deuta differnt user le like gare based on similarity
            if (similarity >= Threshold)
            {
                // Populate the similarity property of Product
                product.Similarity = similarity;

                recommendedProducts.Add(product);

                if (recommendedProducts.Count >= MaxRecommendations)
                    break;
            }
        }

        return recommendedProducts.OrderByDescending(p => p.Similarity);
    }

    private double CalculateCosineSimilarity(string userId, int productId, List<int> likedProductIdsByAllUsers)
    {
        // Get liked product IDs by the current user
        var likedProductIdsByUser = _dbContext.Likes
            .Where(l => l.ApplicationUserId == userId)
            .Select(l => l.ProductId)
            .ToList();

        // Add the current productID to the list if it is  liked by the current user
        if (likedProductIdsByUser.Contains(productId))
        {
            likedProductIdsByUser.Add(productId);
        }

        // Calculate dot product and magnitudes
        double dotProduct = likedProductIdsByUser.Intersect(likedProductIdsByAllUsers).Count();
        double magnitude1 = Math.Sqrt(likedProductIdsByUser.Count);
        double magnitude2 = Math.Sqrt(likedProductIdsByAllUsers.Count);

        // Calculate cosine similarity
        double cosineSimilarity = dotProduct / (magnitude1 * magnitude2);

        return cosineSimilarity;
    }
}
