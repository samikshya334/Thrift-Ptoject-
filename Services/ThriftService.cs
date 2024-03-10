using AutoMapper;
using System.Collections.Generic;
using Thrift_Us.Repositories;
using Thrift_Us.Models;
using Thrift_Us.ViewModel.Category;

namespace Thrift_Us.Services
{
    public class ThriftService : IThriftService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public ThriftService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public List<CategoryIndexViewModel> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            return _mapper.Map<List<CategoryIndexViewModel>>(categories);
        }

        public bool CreateCategory(CategoryCreateViewModel vm)
        {
            var category = _mapper.Map<Category>(vm);
            return _categoryRepository.Add(category);
        }

        public CategoryDetailsViewModel GetCategoryDetails(int id)
        {
            var category = _categoryRepository.GetById(id);
            return _mapper.Map<CategoryDetailsViewModel>(category);
        }

        public CategoryEditViewModel GetCategoryForEdit(int id)
        {
            var category = _categoryRepository.GetById(id);
            return _mapper.Map<CategoryEditViewModel>(category);
        }

        public bool EditCategory(CategoryEditViewModel vm)
        {
            var category = _mapper.Map<Category>(vm);
            return _categoryRepository.Update(category);
        }

        public bool DeleteCategory(int id)
        {
            return _categoryRepository.Delete(id);
        }
    }
}
