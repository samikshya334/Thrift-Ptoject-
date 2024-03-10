
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System;
using System.Data;
using System.Security.Claims;
using Thrift_Us.Data;
using Thrift_Us.Models;

using Thrift_Us.ViewModel;
using Thrift_Us.ViewModel.Category;
using Thrift_Us.ViewModels;

namespace Thrift_Us.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var details = await _cartService.GetCartDetailsAsync(claim.Value);

            return View(details);
        }
     
        public async Task<IActionResult> Plus(int id)
        {
            await _cartService.IncreaseItemCountAsync(id);
          
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Minus(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return RedirectToAction("Index", "Home");
            }

            await _cartService.DecreaseItemCountAsync(id, claim.Value);
       
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return RedirectToAction("Index", "Home");
            }

            await _cartService.DeleteCartItemAsync(id, claim.Value);
            return RedirectToAction("Index");
        }

     
        public async Task<IActionResult>Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                var userId = claim.Value;
                var details = await _cartService.GetCartSummaryAsync(userId);
                return View(details);
            }

            
            return RedirectToAction("Index", "Home");
        }

       
    }
}
        
    




    

