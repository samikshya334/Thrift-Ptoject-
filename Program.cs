using Microsoft.EntityFrameworkCore;
using Thrift_Us.Data;
using Microsoft.AspNetCore.Identity;
using Thrift_Us.Repositories;
using AutoMapper;

using Thrift_Us.Services.Interface;
using Thrift_Us.Models;
using Thrift_Us.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Configuration;


namespace Thrift_Us
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ThriftDbContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("ThriftConnectionString")));

            builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ThriftDbContext>();
         
      
            _=builder.Services.AddScoped<IThriftService, ThriftService>();
            _=builder.Services.AddScoped<IProductService, Thrift_Us.Services.ProductService>();
            _=builder.Services.AddHttpClient();
            _=builder.Services.AddScoped<ICartService, CartService>();
            _=builder.Services.AddScoped<IRentalCartService, RentalCartService>();
            _=builder.Services.AddScoped<IOrderService, OrderService>();
            _=builder.Services.AddScoped<IRentalOrderService, RentalOrderService>();
            _=builder.Services.AddScoped<IRecommendationService, RecommendationService>();


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();;

            app.UseAuthorization();
            app.MapRazorPages();
            app.UseStaticFiles();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
           

            IdentitySeedData.EnsurePopulated(app);
            app.Run();
            
        }
    }
}