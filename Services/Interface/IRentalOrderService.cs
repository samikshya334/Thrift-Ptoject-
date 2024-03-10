using Thrift_Us.Models;
using Thrift_Us.ViewModel;

public interface IRentalOrderService
{
    Task<IEnumerable<RentalOrderHeader>> GetOrdersAsync(string userId, string status);
    Task<OrderDetailsViewModel> GetOrderDetailsAsync(int orderId);
    Task<bool> SaveOrderAsync(CartOrderViewModel cartOrderViewModel, string userId);
    Task<bool> UpdateOrderStatusAsync(int orderId, Payment_Status paymentStatus, Order_Status orderStatus);
    Task<bool> DeleteOrderAsync(int orderId);
}