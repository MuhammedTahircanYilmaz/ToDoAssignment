using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using ToDoAssignment.Models.Todos.Dtos.Requests;
using ToDoAssignment.Models.Todos.Dtos.Response;
using ToDoAssignment.Models.Todos.Entity;
using ToDoAssignment.Models.Todos.Enums;
using ToDoAssignment.Service.Constants;
using ToDoAssignment.Service.Rules;
using ToDoAssignment.Service.Todos.Concretes;
using ToDoAssignment.Repository.ToDos.Repositories.Abstracts;

[TestFixture]
public class EfToDoServiceTests
{
    private Mock<IMapper> _mapperMock;
    private Mock<IToDoRepository> _toDoRepositoryMock;
    private Mock<ToDoBusinessRules> _businessRulesMock;
    private EfToDoService _service;

    [SetUp]
    public void SetUp()
    {
        _mapperMock = new Mock<IMapper>();
        _toDoRepositoryMock = new Mock<IToDoRepository>();
        _businessRulesMock = new Mock<ToDoBusinessRules>(_toDoRepositoryMock.Object);
        _service = new EfToDoService(_mapperMock.Object, _toDoRepositoryMock.Object, _businessRulesMock.Object);
    }

    [Test]
    public void Add_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var dto = new CreateToDoRequestDto { Title = "New Task", Description = "Task Description" };
        var userId = "user-1";
        var toDo = new ToDo { Id = Guid.NewGuid(), UserId = userId };

        _mapperMock.Setup(m => m.Map<ToDo>(dto)).Returns(toDo);
        _toDoRepositoryMock.Setup(r => r.Add(It.IsAny<ToDo>()));

        // Act
        var result = _service.Add(dto, userId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(200, result.Status);
        Assert.AreEqual(Messages.ToDoCreatedMessage, result.Message);
        _toDoRepositoryMock.Verify(r => r.Add(It.IsAny<ToDo>()), Times.Once);
    }

    [Test]
    public void Update_ExistingToDo_ReturnsSuccess()
    {
        // Arrange
        var dto = new UpdateToDoRequestDto
        {
            Id = Guid.NewGuid(),
            Title = "Updated Task",
            CategoryId = Guid.NewGuid(),
            Description = "Updated Description",
            Priority = Priority.High
        };
        var userId = "user-1";
        var toDo = new ToDo { Id = dto.Id, UserId = userId, IsUpdateable = true };

        _businessRulesMock.Setup(r => r.ToDoExists(dto.Id)).Returns(true);
        _toDoRepositoryMock.Setup(r => r.GetById(dto.Id)).Returns(toDo);
        _toDoRepositoryMock.Setup(r => r.Update(It.IsAny<ToDo>()));

        // Act
        var result = _service.Update(dto, userId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(200, result.Status);
        Assert.AreEqual(Messages.ToDoUpdatedMessage, result.Message);
        _toDoRepositoryMock.Verify(r => r.Update(It.IsAny<ToDo>()), Times.Once);
    }

    [Test]
    public void Delete_ExistingToDo_ReturnsSuccess()
    {
        // Arrange
        var toDoId = Guid.NewGuid();
        var userId = "user-1";
        var toDo = new ToDo { Id = toDoId, UserId = userId };

        _businessRulesMock.Setup(r => r.ToDoExists(toDoId)).Returns(true);
        _toDoRepositoryMock.Setup(r => r.GetById(toDoId)).Returns(toDo);
        _toDoRepositoryMock.Setup(r => r.Delete(It.IsAny<ToDo>()));

        // Act
        var result = _service.Delete(toDoId, userId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(204, result.Status);
        Assert.AreEqual(Messages.ToDoDeletedMessage, result.Message);
        _toDoRepositoryMock.Verify(r => r.Delete(It.IsAny<ToDo>()), Times.Once);
    }

    //[Test]
    //public void GetById_ExistingToDo_ReturnsToDo()
    //{
    //    // Arrange
    //    var toDoId = Guid.NewGuid();
    //    var userId = "user-1";
    //    var toDo = new ToDo { Id = toDoId, UserId = userId };
    //    var toDoResponse = new ToDoResponseDto();

    //    _businessRulesMock.Setup(r => r.ToDoExists(toDoId)).Returns(true);
    //    _toDoRepositoryMock.Setup(r => r.GetAll(It.IsAny<Func<ToDo, bool>>())).Returns(new List<ToDo> { toDo });
    //    _mapperMock.Setup(m => m.Map<ToDoResponseDto>(It.IsAny<ToDo>())).Returns(toDoResponse);

    //    // Act
    //    var result = _service.GetById(toDoId, userId);

    //    // Assert
    //    Assert.IsTrue(result.Success);
    //    Assert.AreEqual(200, result.Status);
    //    Assert.IsNotNull(result.Data);
    //}

    //[Test]
    //public void GetAllByUserId_ExistingToDos_ReturnsList()
    //{
    //    // Arrange
    //    var userId = "user-1";
    //    var toDos = new List<ToDo>
    //    {
    //        new ToDo { Id = Guid.NewGuid(), UserId = userId },
    //        new ToDo { Id = Guid.NewGuid(), UserId = userId }
    //    };
    //    var toDoResponses = new List<ToDoResponseDto>();

    //    _toDoRepositoryMock.Setup(r => r.GetAll(It.IsAny<Func<ToDo, bool>>())).Returns(toDos);
    //    _mapperMock.Setup(m => m.Map<List<ToDoResponseDto>>(toDos)).Returns(toDoResponses);

    //    // Act
    //    var result = _service.GetAllByUserId(userId);

    //    // Assert
    //    Assert.IsTrue(result.Success);
    //    Assert.AreEqual(200, result.Status);
    //    Assert.IsNotNull(result.Data);
    //}
}
