using AutoMapper;
using Azure;
using Core.Entities;
using ToDoAssignment.Models.Dtos.Categories.Requests;
using ToDoAssignment.Models.Dtos.Categories.Response;
using ToDoAssignment.Models.Entities;
using ToDoAssignment.Repository.Repositories.Categories.Abstracts;
using ToDoAssignment.Service.Constants;
using ToDoAssignment.Service.Rules;
using ToDoAssignment.Service.Services.Categories.Abstracts;

namespace ToDoAssignment.Service.Services.Categories.Concretes;

public class EfCategoryService (ICategoryRepository _categoryRepository, IMapper _mapper, CategoryBusinessRules _businessRules) : ICategoryService
{
    public ReturnModel<CategoryResponseDto> Add(AddCategoryRequestDto dto, string userId)
    {
       
        Category createdCategory = _mapper.Map<Category>(dto);
        createdCategory.UserId = userId;

        Category category = _categoryRepository.Add(createdCategory);

        var response = _mapper.Map<CategoryResponseDto>(category);

        return ReturnModel<CategoryResponseDto>.ReturnModelOfSuccess(response, 200, Messages.CategoryAddedMessage);
 
    }

    public ReturnModel<CategoryResponseDto> Update(UpdateCategoryRequestDto dto)
    {
        _businessRules.CategoryIsPresent(dto.Id);

        Category? category = _categoryRepository.GetById(dto.Id);
        category.Name = dto.Name;
        
        _categoryRepository.Update(category);

        CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(category);

        return ReturnModel<CategoryResponseDto>.ReturnModelOfSuccess(response, 200, Messages.CategoryUpdatedMessage);
    }

    public ReturnModel<string> Delete(Guid id)
    {
        _businessRules.CategoryIsPresent(id);

        Category? category = _categoryRepository.GetById(id);

        _categoryRepository.Delete(category);

        return ReturnModel<string>.ReturnModelOfSuccess(Messages.CategoryDeletedMessage, 204);
    }

    public ReturnModel<List<CategoryResponseDto>> GetAll()
    {
        List<Category> categories = _categoryRepository.GetAll();

        var response = _mapper.Map<List<CategoryResponseDto>>(categories);

        return ReturnModel<List<CategoryResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<CategoryResponseDto>> GetAllByTitleContains(string text)
    {
        List<Category> categories = _categoryRepository.GetAll(
            x => x.Name.Contains(text, StringComparison.InvariantCultureIgnoreCase));

        var response = _mapper.Map<List<CategoryResponseDto>>(categories);

        return ReturnModel<List<CategoryResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<CategoryResponseDto> GetById(Guid id)
    {
        _businessRules.CategoryIsPresent(id);

        Category? category = _categoryRepository.GetById(id);
        var response = _mapper.Map<CategoryResponseDto>(category);

        return ReturnModel<CategoryResponseDto>.ReturnModelOfSuccess(response, 200);

    }
}
