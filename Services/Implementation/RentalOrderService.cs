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


public class RentalOrderService : IRentalOrderService
{
    private readonly ThriftDbContext _context;
    private readonly UserManager<IdentityUser> _usermanager;

    public RentalOrderService(ThriftDbContext context, UserManager<IdentityUser> usermanager)
    {
        _context = context;
        _usermanager = usermanager;

    }

    public async Task<IEnumerable<RentalOrderHeader>> GetOrdersAsync(string userId, string status)
    {
        var user = await _usermanager.FindByIdAsync(userId);
        var roles = await _usermanager.GetRolesAsync(user);

        IQueryable<RentalOrderHeader> orders = _context.RentalOrderHeaders.Include(x => x.ApplicationUser);

        if (roles.Contains("Admin"))
        {
            orders = _context.RentalOrderHeaders.AsQueryable();
        }
        else if (roles.Contains("Seller"))
        {
            orders = orders.Where(order =>
                order.RentalOrderDetails.Any(detail =>
                    detail.Product.ApplicationUserId == userId));
        }
        else if (roles.Contains("Buyer"))
        {
            orders = orders.Where(order => order.ApplicationUserId == userId);
        }

        return await orders.ToListAsync();
    }

    public async Task<OrderDetailsViewModel> GetOrderDetailsAsync(int orderId)
    {
        var rentalOrderHeader = await _context.RentalOrderHeaders
            .Include(o => o.ApplicationUser)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (rentalOrderHeader == null)
        {
            return null;
        }

        var rentalOrderDetails = await _context.RentalOrderDetails
            .Include(o => o.Product)
            .Where(o => o.RentalOrderHeaderId == orderId)
            .ToListAsync();

        // Calculate refund amount based on payment status
        foreach (var cart in rentalOrderDetails)
        {
            if (rentalOrderHeader.PaymentStatus == Payment_Status.Recieved)
            {
              
            }
            else
            {
                cart.RefundAmount = (cart.Product.Price * cart.RentalOrderHeader.RentalDuration * cart.Count) - (cart.Product.RentalPrice * cart.RentalOrderHeader.RentalDuration * cart.Count);
            }
        }

        return new OrderDetailsViewModel
        {
            RentalOrderHeader = rentalOrderHeader,
            RentalOrderDetails = rentalOrderDetails
        };
    }


    public async Task<bool> SaveOrderAsync(CartOrderViewModel cartorderviewmodel, string userId)
    {

        using var transaction = _context.Database.BeginTransaction();
        try
        {
            decimal orderTotal = cartorderviewmodel.ListOfRentalCart.Sum(item => item.Product.Price* item.Count * item.RentalDuration);

            var RentalorderHeader = new RentalOrderHeader
            {
                ApplicationUserId = userId,
                Name = cartorderviewmodel.RentalOrderHeader.Name,
                Phone = cartorderviewmodel.RentalOrderHeader.Phone,
                Address = cartorderviewmodel.RentalOrderHeader.Address,
                City=cartorderviewmodel.RentalOrderHeader.City,
                State =cartorderviewmodel.RentalOrderHeader.State,
                PostalCode=cartorderviewmodel.RentalOrderHeader.PostalCode,
                StartDate = cartorderviewmodel.RentalOrderHeader.StartDate,
                EndDate = cartorderviewmodel.RentalOrderHeader.EndDate,
                RentalDuration = cartorderviewmodel.RentalOrderHeader.RentalDuration,
                OrderDate=cartorderviewmodel.RentalOrderHeader.OrderDate,
                OrderTotal = orderTotal,
                OrderStatus = cartorderviewmodel.RentalOrderHeader.OrderStatus,
                PaymentStatus = cartorderviewmodel.RentalOrderHeader.PaymentStatus,
                TransId=GenerateTransactionId()
            };

            _context.RentalOrderHeaders.Add(RentalorderHeader);
            await _context.SaveChangesAsync();
            if (cartorderviewmodel.ListOfRentalCart != null && cartorderviewmodel.ListOfRentalCart.Any())
            {
                foreach (var cartItem in cartorderviewmodel.ListOfRentalCart)
                {
                    var RentalorderDetail = new RentalOrderDetails
                    {
                        RentalOrderHeaderId = RentalorderHeader.Id,
                        ProductId = cartItem.ProductId,
                        Count = cartItem.Count,
                        Description = "Good",
                        Name=cartItem.Product.ProductName,
                        Price=cartItem.Product.Price,
                        RentalPrice = cartItem.Product.RentalPrice
                    };

                    _context.RentalOrderDetails.Add(RentalorderDetail);
                    await _context.SaveChangesAsync();

                    var cart = await _context.RentalCarts.FirstOrDefaultAsync(x => x.Id == cartItem.Id);
                    if (cart != null)
                    {
                        _context.RentalCarts.Remove(cart);
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

    public async Task<bool> UpdateOrderStatusAsync(int orderId, Payment_Status paymentStatus, Order_Status orderStatus)
    {
        var order = await _context.RentalOrderHeaders.FindAsync(orderId);
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

            var order = await _context.RentalOrderHeaders.FindAsync(orderId);

            if (order == null)
                return false;


            _context.RentalOrderHeaders.Remove(order);

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
