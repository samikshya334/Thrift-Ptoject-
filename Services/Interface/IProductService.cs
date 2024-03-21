using Thrift_Us.Models;
using Thrift_Us.ViewModel.Category;
using Thrift_Us.ViewModel.Product;

namespace Thrift_Us.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductIndexViewModel>> GetAllProducts(string userId = null);
        ProductEditViewModel GetProductById(int productId);
        List<Category> GetCategories(); 
        bool CreateProduct(ProduceCreateViewModel viewModel, string userId);
        bool UpdateProduct(ProduceCreateViewModel viewModel);
        ProductDetailsViewModel GetProductDetails(int productId);
        bool DeleteProduct(int productId);
    }
    }
