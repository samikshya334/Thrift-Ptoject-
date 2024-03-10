using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Thrift_Us.Data;
using Thrift_Us.Models;
using Thrift_Us.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using System.Data;


public class OrderService : IOrderService
{
    private readonly ThriftDbContext _context;
    private readonly UserManager<IdentityUser> _usermanager;

    public OrderService(ThriftDbContext context, UserManager<IdentityUser> usermanager)
    {
        _context = context;
        _usermanager = usermanager;

    }

    public async Task<IEnumerable<OrderHeader>> GetOrdersAsync(string userId, string status)
    {
        IQueryable<OrderHeader> orders = _context.OrderHeaders.AsQueryable();

        var user = await _usermanager.FindByIdAsync(userId);
        var roles = await _usermanager.GetRolesAsync(user);

        if (roles.Contains("Admin"))
        {
          
            orders = _context.OrderHeaders.AsQueryable();

        }
        else if (roles.Contains("Seller"))
        {
            
            orders = orders.Where(orderHeader =>
                orderHeader.OrderDetails.Any(orderDetail =>
                    orderDetail.Product.ApplicationUserId == userId));
        }
        else if (roles.Contains("Buyer"))
        {
        
            orders = orders.Where(x => x.ApplicationUserId == userId);
        }

     

        return await orders.Include(x => x.ApplicationUser).ToListAsync();
    }




    public async Task<OrderDetailsViewModel> GetOrderDetailsAsync(int orderId)
    {
        var orderHeader = await _context.OrderHeaders
            .Include(o => o.ApplicationUser)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (orderHeader == null)
        {
            return null;
        }

        var orderDetails = await _context.OrderDetails
            .Include(o => o.Product)
            .Where(o => o.OrderHeaderId == orderId)
            .ToListAsync();

        return new OrderDetailsViewModel
        {
            OrderHeader = orderHeader,
            OrderDetails = orderDetails
        };
    }

    public async Task<bool> SaveOrderAsync(CartOrderViewModel cartorderviewmodel, string userId)
    {

        using var transaction = _context.Database.BeginTransaction();
        try
        {
            decimal orderTotal = cartorderviewmodel.ListOfCart.Sum(item => item.Count * item.Product.Price);
            var orderHeader = new OrderHeader
            {
                ApplicationUserId = userId,
                Name = cartorderviewmodel.OrderHeader.Name,
                Phone = cartorderviewmodel.OrderHeader.Phone,
                Address = cartorderviewmodel.OrderHeader.Address,
                City=cartorderviewmodel.OrderHeader.City,
                State =cartorderviewmodel.OrderHeader.State,
                PostalCode=cartorderviewmodel.OrderHeader.PostalCode,
                DateofPick = cartorderviewmodel.OrderHeader.DateofPick,
                TimeofPick = cartorderviewmodel.OrderHeader.TimeofPick,
                OrderDate=cartorderviewmodel.OrderHeader.OrderDate,
                OrderTotal = orderTotal,
                OrderStatus = cartorderviewmodel.OrderHeader.OrderStatus, 
                PaymentStatus = cartorderviewmodel.OrderHeader.PaymentStatus,
                TransId=GenerateTransactionId()
            };

            _context.OrderHeaders.Add(orderHeader);
            await _context.SaveChangesAsync();
            if (cartorderviewmodel.ListOfCart != null && cartorderviewmodel.ListOfCart.Any())
            {
                foreach (var cartItem in cartorderviewmodel.ListOfCart)
                {
                    var orderDetail = new OrderDetails
                    {
                        OrderHeaderId = orderHeader.Id,
                        ProductId = cartItem.ProductId,
                        Count = cartItem.Count,
                        Description = "Good",
                        Name=cartItem.Product.ProductName,
                        Price = cartItem.Product.Price
                    };

                    _context.OrderDetails.Add(orderDetail);
                    await _context.SaveChangesAsync();

                    var cart = await _context.Carts.FirstOrDefaultAsync(x => x.Id == cartItem.Id);
                    if (cart != null)
                    {
                        _context.Carts.Remove(cart);
                    }
                }

                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();
            

            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
    }
    public string GenerateTransactionId()
    {
        return Guid.NewGuid().ToString();
    }

    public async Task<bool> UpdateOrderStatusAsync(int orderId, PaymentStatus paymentStatus, OrderStatus orderStatus)
    {
        var order = await _context.OrderHeaders.FindAsync(orderId);
        if (order != null)
        {
            order.OrderStatus = orderStatus;
            order.PaymentStatus = paymentStatus;
            _context.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }


    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        try
        {

            var order = await _context.OrderHeaders.FindAsync(orderId);

            if (order == null)
                return false;


            _context.OrderHeaders.Remove(order);

            await _context.SaveChangesAsync();


            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting order: {ex.Message}");
            return false;
        }
    }
}
