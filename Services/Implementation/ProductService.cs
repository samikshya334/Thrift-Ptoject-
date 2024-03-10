using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Thrift_Us.Data;
using Thrift_Us.Models;
using Thrift_Us.Services.Interface;
using Thrift_Us.ViewModel.Product;

namespace Thrift_Us.Services
{
    public class ProductService : IProductService
    {
        private readonly ThriftDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(ThriftDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<ProductIndexViewModel> GetAllProducts()
        {
            return _context.Products.AsNoTracking()
                .Include(x => x.Category)
                .Select(x => new ProductIndexViewModel
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Description = x.Description,
                    CategoryId = x.CategoryId,
                    Category = x.Category,
                    Price = x.Price,
                    RentalPrice= x.RentalPrice,
                    Size = x.Size,
                    Quantity=x.Quantity,
                    Condition = x.Condition,
                    ImageUrl = x.ImageUrl,
                    PostedOn = x.PostedOn
                }).ToList();
        }

        public ProductEditViewModel GetProductById(int productId)
        {
            var data = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.ProductId == productId);
            if (data == null)
            {
                return null;
            }

            return new ProductEditViewModel
            {
                ProductId = data.ProductId,
                ProductName = data.ProductName,
                Description = data.Description,
                CategoryId = data.CategoryId,
                Price = data.Price,
                RentalPrice= data.RentalPrice,
                Size = data.Size,
                Quantity=data.Quantity,
                Condition = data.Condition,
                ImageUrl = data.ImageUrl,
                PostedOn = data.PostedOn
            };
        }

        public List<Category> GetCategories()
        {
            var categories = _context.Categories.ToList();
            if (categories == null || !categories.Any())
            {
                return null;
            }
            return categories;
        }

        public bool CreateProduct(ProduceCreateViewModel viewModel, string userId)
        {
            try
            {
                string stringFileName = UploadFile(viewModel.ImagePath);
                var product = new Product
                {
                    ProductName = viewModel.ProductName,
                    Description = viewModel.Description,
                    CategoryId = viewModel.CategoryId,
                    Price = viewModel.Price,
                    RentalPrice= viewModel.RentalPrice,
                    Size = viewModel.Size,
                    Quantity=viewModel.Quantity,
                    ImageUrl = stringFileName,
                    Condition = viewModel.Condition,
                    PostedOn = DateTime.Now,
                    ApplicationUserId = userId
                };

                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool UpdateProduct(ProduceCreateViewModel viewModel)
        {
            try
            {
                var data = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.ProductId == viewModel.ProductId);
                if (data == null)
                {

                    return false;
                }

                string fileName = string.Empty;
                if (viewModel.ImagePath != null)
                {
                    if (data.ImageUrl != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Content/Image", data.ImageUrl);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                    fileName = UploadFile(viewModel.ImagePath);
                }

                data.ProductName = viewModel.ProductName;
                data.Description = viewModel.Description;
                data.CategoryId = viewModel.CategoryId;
                data.Price = viewModel.Price;
                data.RentalPrice= viewModel.RentalPrice;
                data.Size = viewModel.Size;
                data.Quantity=viewModel.Quantity;
                data.Condition = viewModel.Condition;
                data.PostedOn = DateTime.Now;

                if (viewModel.ImagePath != null)
                {
                    data.ImageUrl = fileName;
                }

                _context.Products.Update(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public ProductDetailsViewModel GetProductDetails(int productId)
        {
            var productDetails = _context.Products
                .Where(p => p.ProductId == productId)
                .Select(p => new ProductDetailsViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    Price = p.Price,
                    RentalPrice=p.RentalPrice,
                    Size = p.Size,
                    Quantity=p.Quantity,
                    Condition = p.Condition,
                    ImageUrl = p.ImageUrl,
                    PostedOn = p.PostedOn,

                })
                .FirstOrDefault();

            return productDetails;
        }


        public bool DeleteProduct(int productId)
        {
            try
            {
                var data = _context.Products.FirstOrDefault(x => x.ProductId == productId);
                if (data != null)
                {
                    string deleteFromFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Content/Image/");
                    string currentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFromFolder, data.ImageUrl);
                    if (currentImage != null && System.IO.File.Exists(currentImage))
                    {
                        System.IO.File.Delete(currentImage);
                    }

                    _context.Products.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        private string UploadFile(IFormFile imageFile)
        {
            string filename = null;
            if (imageFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Content/Image/");
                filename = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadFolder, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
            }
            return filename;
        }
    }
}