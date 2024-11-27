using AutoMapper;
using Azure;
using Core.Entities;
using Core.Exceptions;
using ToDoAssignment.Models.Categories.Dtos.Requests;
using ToDoAssignment.Models.Categories.Dtos.Response;
using ToDoAssignment.Models.Categories.Entity;
using ToDoAssignment.Repository.Categories.Repositories.Abstracts;
using ToDoAssignment.Service.Categories.Abstracts;
using ToDoAssignment.Service.Constants;
using ToDoAssignment.Service.Rules;

namespace ToDoAssignment.Service.Categories.Concretes;

public class EfCategoryService(ICategoryRepository _categoryRepository, IMapper _mapper, CategoryBusinessRules _businessRules) : ICategoryService
{
    public ReturnModel<CategoryResponseDto> Add(AddCategoryRequestDto dto, string userId)
    {

        Category createdCategory = _mapper.Map<Category>(dto);
        createdCategory.UserId = userId;

        Category category = _categoryRepository.Add(createdCategory);

        var response = _mapper.Map<CategoryResponseDto>(category);

        return ReturnModel<CategoryResponseDto>.ReturnModelOfSuccess(response, 200, Messages.CategoryAddedMessage);

    }

    public ReturnModel<CategoryResponseDto> Update(UpdateCategoryRequestDto dto, string userId)
    {
        _businessRules.CategoryIsPresent(dto.Id);

        Category? category = _categoryRepository.GetById(dto.Id);

        CheckUserMatches(category.UserId, userId);

        category.Name = dto.Name;

        _categoryRepository.Update(category);

        CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(category);

        return ReturnModel<CategoryResponseDto>.ReturnModelOfSuccess(response, 200, Messages.CategoryUpdatedMessage);
    }

    public ReturnModel<string> Delete(Guid id, string userId)
    {
        _businessRules.CategoryIsPresent(id);

        Category? category = _categoryRepository.GetById(id);

        CheckUserMatches(category.UserId, userId);

        _categoryRepository.Delete(category);

        return ReturnModel<string>.ReturnModelOfSuccess(Messages.CategoryDeletedMessage, 204);
    }

    public ReturnModel<List<CategoryResponseDto>> GetAll()
    {
        List<Category> categories = _categoryRepository.GetAll();

        var response = _mapper.Map<List<CategoryResponseDto>>(categories);

        return ReturnModel<List<CategoryResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<CategoryResponseDto>> GetAllByTitleContains(string text, string userId)
    {
        List<Category> categoriesByTitle = _categoryRepository.GetAll(x => x.UserId == userId
        && x.Name.Contains(text, StringComparison.InvariantCultureIgnoreCase));


        var response = _mapper.Map<List<CategoryResponseDto>>(categoriesByTitle);

        return ReturnModel<List<CategoryResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<CategoryResponseDto>> GetAllByUserId(string userId)
    {
        List<Category> categories = _categoryRepository.GetAll(x => x.UserId == userId);
        var response = _mapper.Map<List<CategoryResponseDto>>(categories);

        return ReturnModel<List<CategoryResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<CategoryResponseDto> GetById(Guid id, string userId)
    {
        _businessRules.CategoryIsPresent(id);

        List<Category> category = _categoryRepository.GetAll(x => x.UserId == userId && x.Id == id);
        var response = _mapper.Map<CategoryResponseDto>(category);

        return ReturnModel<CategoryResponseDto>.ReturnModelOfSuccess(response, 200);

    }

    private void CheckUserMatches(string userId, string IdToBeChecked)
    {
        if (!(userId == IdToBeChecked))
        {
            throw new BusinessException("The Category does not belong to the user!");
        }
    }
}
