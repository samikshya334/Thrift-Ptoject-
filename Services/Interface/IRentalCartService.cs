using System.Threading.Tasks;
using Thrift_Us.ViewModel;

namespace Thrift_Us.Services.Interface
{
    public interface IRentalCartService
    {
        Task<CartOrderViewModel> GetCartDetailsAsync(string userId);
        Task IncreaseItemCountAsync(int cartItemId);
        Task DecreaseItemCountAsync(int cartItemId, string userId);
        Task DeleteCartItemAsync(int cartItemId, string userId);
        Task<CartOrderViewModel>GetCartSummaryListAsync(string userId);

    }
}
