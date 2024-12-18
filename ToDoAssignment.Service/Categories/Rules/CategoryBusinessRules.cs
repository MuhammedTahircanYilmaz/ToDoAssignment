﻿using Core.Exceptions;
using FluentValidation.AspNetCore;
using ToDoAssignment.Repository.Categories.Repositories.Abstracts;
using ToDoAssignment.Service.Constants;

namespace ToDoAssignment.Service.Rules;

public class CategoryBusinessRules(ICategoryRepository _categoryRepository)
{
    public virtual bool CategoryIsPresent(Guid id)
    {
        var category = _categoryRepository.GetById(id);
        if (category is null)
        {
            throw new NotFoundException(Messages.CategoryIsNotPresentMessage(id));
        }
        return true;
    }
}
