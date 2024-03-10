using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Thrift_Us.Data;
using Thrift_Us.Models;

using Thrift_Us.ViewModel;

public class CartService : ICartService
{
    private readonly ThriftDbContext _context;
 
    public CartService(ThriftDbContext context)
    {
        _context = context;
   
    }

    public async Task<CartOrderViewModel> GetCartDetailsAsync(string userId)
    {
        var details = new CartOrderViewModel
        {
            OrderHeader = new OrderHeader()
        };

        details.ListOfCart = await _context.Carts
            .Where(x => x.ApplicationUserId == userId)
            .Include(c => c.Product)
            .ToListAsync();

        if (details.ListOfCart != null)
        {
            foreach (var cart in details.ListOfCart)
            {
                if (cart.Product != null)
                {
                    details.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
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
        var cart = await _context.Carts
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
        var cart = await _context.Carts
            .Include(c => c.Product)
            .FirstOrDefaultAsync(x => x.Id == cartItemId);

        if (cart != null)
        {
            if (cart.Count == 1)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                var count = await _context.Carts
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
        var cart = await _context.Carts.FirstOrDefaultAsync(x => x.Id == cartItemId);
        if (cart != null)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            var count = await _context.Carts
                .Where(x => x.ApplicationUserId == userId)
                .CountAsync();

        }


    }
    public async Task<CartOrderViewModel> GetCartSummaryAsync(string userId)
    {
        var details = new CartOrderViewModel
        {
            OrderHeader = new OrderHeader(),  
            ListOfCart = await _context.Carts
                .Where(c => c.ApplicationUserId == userId)
                .Include(c => c.Product)
                .ToListAsync()
        };

        var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
        if (user != null)
        {
            details.OrderHeader.ApplicationUser = user;
            details.OrderHeader.Phone = user.PhoneNumber;
            details.OrderHeader.TimeofPick=DateTime.Now;
        }

        if (details.ListOfCart != null)
        {
            foreach (var cart in details.ListOfCart)
            {
                if (cart.Product != null)
                {
                    details.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
                }
            }
        }

        return details;
    }

   

}






