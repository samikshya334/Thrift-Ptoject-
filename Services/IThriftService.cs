using System.Collections.Generic;
using Thrift_Us.ViewModel.Category;

namespace Thrift_Us.Services
{
    public interface IThriftService
    {
        List<CategoryIndexViewModel> GetAllCategories();
        bool CreateCategory(CategoryCreateViewModel vm);
        CategoryDetailsViewModel GetCategoryDetails(int id);
        CategoryEditViewModel GetCategoryForEdit(int id);
        bool EditCategory(CategoryEditViewModel vm);
        bool DeleteCategory(int id);
    }
}

