using Core.Entities;
using ToDoAssignment.Models.Dtos.Categories.Requests;
using ToDoAssignment.Models.Dtos.Categories.Response;

namespace ToDoAssignment.Service.Services.Categories.Abstracts;

public interface ICategoryService
{
    // Summary:
    //      Sends the Category creation request to the repository after the validation checks
    //
    // Parameters:
    //   dto:
    //      A dto with the necessary fields for the creation of a Category
    //   userId:
    //      The Id of the user who is creating the Category
    //
    // Returns:
    //      A ReturnModel of a dto of the Category information
    ReturnModel<CategoryResponseDto> Add(AddCategoryRequestDto dto, string userId);


    // Summary:
    //      Sends the Category Update request to the repository after the validation checks
    //
    // Parameters:
    //   dto:
    //      A dto with the necessary fields for the update of a Category
    //   userId:
    //      The Id of the user who is updating the Category
    //
    // Returns:
    //      A ReturnModel of a dto of the Category information

    ReturnModel<CategoryResponseDto> Update(UpdateCategoryRequestDto dto, string userId);


    // Summary:
    //      Sends the Category delete request to the repository after the validation checks
    //
    // Parameters:
    //   id:
    //      The Id of the Category to be deleted, of type int
    //
    // Returns:
    //      A string of message on whether the deletion succeeded or not  

    ReturnModel<string> Delete(Guid id, string userId);


    // Summary:
    //      Sends a request to the Repository to retrieve all Categories
    //
    // Returns:
    //      A List of all existing Categories  

    ReturnModel<List<CategoryResponseDto>> GetAll(string userId);


    // Summary:
    //      Sends a request to the Repository to retrieve all Categories whose name contains the
    //      piece of text provided as the parameter
    //
    // Parameters:
    //   text:
    //      The string to use as a filter for the GetAll function
    //
    // Returns:
    //      A List of all the Categories whose name contains the 'text' 

    ReturnModel<List<CategoryResponseDto>> GetAllByTitleContains(string text, string userId);


    // Summary:
    //      Sends a request to the Repository to retrieve the Category whose Id matches the parameter
    //
    // Parameters:
    //   id:
    //      the Id of the Category to be retrieved, of type int
    //
    // Returns:
    //      A List of all the Categories whose Id matches the 'id' 

    ReturnModel<CategoryResponseDto> GetById(Guid id);

    ReturnModel<List<CategoryResponseDto>> GetAllByUserId(string userId);

    
}
