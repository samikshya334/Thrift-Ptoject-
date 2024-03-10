using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Thrift_Us.Data;
using Thrift_Us.Services;
using Thrift_Us.ViewModel.Product;
using Thrift_Us.ViewModel.Category;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Thrift_Us.Services.Interface;
using Thrift_Us.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Thrift_Us.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductController(IProductService productService, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var productIndexViewModel = _productService.GetAllProducts();
            return View(productIndexViewModel);
        }

        private void PopulateCategories()
        {
            var categories = _productService.GetCategories()
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();

            ViewBag.Categories = categories;
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopulateCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProduceCreateViewModel vm)
        {

            try
            {
               
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

             
                _productService.CreateProduct(vm, userId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            PopulateCategories();
            return View(vm);
          
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _productService.GetProductById(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            var categories = _productService.GetCategories();

           
            viewModel.Categories = categories.Select(c => new CategoryCreateViewModel
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
              
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProduceCreateViewModel vm)
        {
            try
            {
                _productService.UpdateProduct(vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var viewModel = _productService.GetProductDetails(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
    }
}
