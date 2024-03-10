using AutoMapper;
using Thrift_Us.Models;
using Thrift_Us.ViewModel.Category;

public class MapperProfile : Profile
{
    public MapperProfile()
    {

        CreateMap<Category, CategoryIndexViewModel>();
        CreateMap<Category, CategoryDetailsViewModel>();
        CreateMap<Category, CategoryEditViewModel>();
        CreateMap<CategoryCreateViewModel, Category>();
        CreateMap<CategoryEditViewModel, Category>();

    }
}
