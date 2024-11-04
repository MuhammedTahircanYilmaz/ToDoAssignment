namespace ToDoAssignment.Service.Constants;

public static class Messages
{
    public const string CategoryAddedMessage = "The Category has been added.";
    public const string CategoryUpdatedMessage = "The Category has been updated.";
    public const string CategoryDeletedMessage = "The Category has been deleted.";

    public const string ToDoCreatedMessage = "The Task has been created.";
    public const string ToDoUpdatedMessage = "The Task has been updated.";
    public const string ToDoDeletedMessage = "The Task has been deleted.";
    public const string ToDoCompletedMessage = "The Task has been completed.";

    public const string UserRegisteredMessage = "The User has been registered";
    public const string UserLoggedInMessage = "The User has logged in";
    public const string UserUpdatedMessage = "The User has been updated";
    public const string UserDeletedMessage = "The User has been deleted";
    public static string CategoryIsNotPresentMessage(Guid id)
    {
        return $"The Category with the Id : {id} could not be found.";
    }
    public static string ToDoIsNotPresentMessage(Guid id)
    {
        return $"The ToDo with the Id > {id} could not be found";
    }
}
