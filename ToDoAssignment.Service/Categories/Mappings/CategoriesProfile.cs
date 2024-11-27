using AutoMapper;
using ToDoAssignment.Models.Categories.Dtos.Requests;
using ToDoAssignment.Models.Categories.Dtos.Response;
using ToDoAssignment.Models.Categories.Entity;

namespace ToDoAssignment.Service.Categories.Mappings
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<AddCategoryRequestDto, Category>();
            CreateMap<Category, CategoryResponseDto>();
        }
    }

}
