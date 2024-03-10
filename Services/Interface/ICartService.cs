using Thrift_Us.Models;

using Thrift_Us.ViewModel;
using Thrift_Us.ViewModels;

public interface ICartService
{
    Task<CartOrderViewModel> GetCartDetailsAsync(string userId);
    Task IncreaseItemCountAsync(int cartItemId);
    Task DecreaseItemCountAsync(int cartItemId, string userId);
    Task DeleteCartItemAsync(int cartItemId, string userId);
    Task<CartOrderViewModel> GetCartSummaryAsync(string userId);






}
