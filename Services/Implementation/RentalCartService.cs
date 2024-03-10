using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Thrift_Us.Data;
using Thrift_Us.Models;
using Thrift_Us.Services.Interface;
using Thrift_Us.ViewModel;

public class RentalCartService : IRentalCartService
{
    private readonly ThriftDbContext _context;

    public RentalCartService(ThriftDbContext context)
    {
        _context = context;

    }
    public async Task<CartOrderViewModel> GetCartDetailsAsync(string userId)
    {
        var details = new CartOrderViewModel
        {
            RentalOrderHeader = new RentalOrderHeader(),
            Rental = new Rental() 
        };

        details.ListOfRentalCart = await _context.RentalCarts
            .Where(x => x.ApplicationUserId == userId)
            .Include(c => c.Product)
            .ToListAsync();

        if (details.ListOfRentalCart != null)
        {
            foreach (var cart in details.ListOfRentalCart)
            {
                if (cart.Product != null)
                {
                    details.RentalOrderHeader.OrderTotal += (cart.Product.Price * cart.RentalDuration * cart.Count);
                    cart.RefundAmount = (cart.Product.Price * cart.RentalDuration * cart.Count) - (cart.Product.RentalPrice * cart.RentalDuration * cart.Count);
                }
                else
                {
                    return details;
                }
            }
        }

        return details;
    }



    public async Task IncreaseItemCountAsync(int cartItemId)
    {
        var cart = await _context.RentalCarts
            .Include(c => c.Product)
            .FirstOrDefaultAsync(x => x.Id == cartItemId);
        if (cart != null)
        {
            if (cart.Count < cart.Product.Quantity)
            {
                cart.Count += 1;
                await _context.SaveChangesAsync();
            }
            else
            {


            }
        }
    }

    public async Task DecreaseItemCountAsync(int cartItemId, string userId)
    {
        var cart = await _context.RentalCarts
            .Include(c => c.Product)
            .FirstOrDefaultAsync(x => x.Id == cartItemId);

        if (cart != null)
        {
            if (cart.Count == 1)
            {
                _context.RentalCarts.Remove(cart);
                await _context.SaveChangesAsync();
                var count = await _context.RentalCarts
                    .Where(x => x.ApplicationUserId == userId)
                    .CountAsync();
            }
            else
            {
                cart.Count -= 1;
                await _context.SaveChangesAsync();
            }
        }
    }


    public async Task DeleteCartItemAsync(int cartItemId, string userId)
    {
        var cart = await _context.RentalCarts.FirstOrDefaultAsync(x => x.Id == cartItemId);
        if (cart != null)
        {
            _context.RentalCarts.Remove(cart);
            await _context.SaveChangesAsync();
            var count = await _context.RentalCarts
                .Where(x => x.ApplicationUserId == userId)
                .CountAsync();

        }


    }
    public async Task<CartOrderViewModel> GetCartSummaryListAsync(string userId)
    {
        

        var details = new CartOrderViewModel
        {
            RentalOrderHeader = new RentalOrderHeader(),
            ListOfRentalCart = await _context.RentalCarts
                .Where(c => c.ApplicationUserId == userId)
                .Include(c => c.Product)
                .ToListAsync()
        };

        var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
        if (user != null)
        {
            details.RentalOrderHeader.ApplicationUser = user;
            details.RentalOrderHeader.Phone = user.PhoneNumber;
        }

        if (details.ListOfRentalCart != null)
        {
            foreach (var cart in details.ListOfRentalCart)
            {
                if (cart.Product != null)
                {
                    details.RentalOrderHeader.OrderTotal += (cart.Product.Price*cart.RentalDuration * cart.Count);
                    cart.RefundAmount = (cart.Product.Price * cart.RentalDuration * cart.Count) - (cart.Product.RentalPrice * cart.RentalDuration * cart.Count);
                }
            }
        }



        return details; 
    }




}






