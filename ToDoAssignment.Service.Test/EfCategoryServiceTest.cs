using AutoMapper;
using Core.Exceptions;
using Moq;
using ToDoAssignment.Models.Categories.Dtos.Requests;
using ToDoAssignment.Models.Categories.Entity;
using ToDoAssignment.Repository.Categories.Repositories.Abstracts;
using ToDoAssignment.Service.Categories.Concretes;
using ToDoAssignment.Service.Rules;

namespace ToDoAssignment.Service.Test;

[TestFixture]
public class EfCategoryServiceTests
{
    private Mock<ICategoryRepository> _categoryRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private Mock<CategoryBusinessRules> _businessRulesMock;
    private EfCategoryService _service;

    [SetUp]
    public void SetUp()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _mapperMock = new Mock<IMapper>();
        _businessRulesMock = new Mock<CategoryBusinessRules>(_categoryRepositoryMock.Object);
        _service = new EfCategoryService(_categoryRepositoryMock.Object, _mapperMock.Object, _businessRulesMock.Object);
    }

    [Test]
    public void Add_MapperThrowsException_ReturnsError()
    {
        // Arrange
        var dto = new AddCategoryRequestDto();
        var userId = "user-123";

        _mapperMock.Setup(m => m.Map<Category>(dto)).Throws<Exception>();

        // Act & Assert
        var ex = Assert.Throws<Exception>(() => _service.Add(dto, userId));
        Assert.That(ex.Message, Is.Not.Empty);
    }

    [Test]
    public void Update_CategoryNotPresent_ThrowsBusinessException()
    {
        // Arrange
        var dto = new UpdateCategoryRequestDto { Id = Guid.NewGuid() };
        var userId = "user-123";

        _businessRulesMock.Setup(b => b.CategoryIsPresent(dto.Id)).Throws(new BusinessException("Category not found"));

        // Act & Assert
        var ex = Assert.Throws<BusinessException>(() => _service.Update(dto, userId));
        Assert.AreEqual("Category not found", ex.Message);
    }

    [Test]
    public void Update_UserMismatch_ThrowsBusinessException()
    {
        // Arrange
        var dto = new UpdateCategoryRequestDto { Id = Guid.NewGuid(), Name = "Updated Name" };
        var userId = "user-123";
        var category = new Category { Id = dto.Id, UserId = "{0B8B508A-3D92-41CD-9490-D7A32A131EAA}" };

        _businessRulesMock.Setup(b => b.CategoryIsPresent(dto.Id)).Verifiable();
        _categoryRepositoryMock.Setup(r => r.GetById(dto.Id)).Returns(category);

        // Act & Assert
        var ex = Assert.Throws<BusinessException>(() => _service.Update(dto, userId));
        Assert.AreEqual("The Category does not belong to the user!", ex.Message);
    }

    [Test]
    public void Delete_CategoryNotPresent_ThrowsBusinessException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userId = "user-123";

        _businessRulesMock.Setup(b => b.CategoryIsPresent(id)).Throws(new BusinessException("Category not found"));

        // Act & Assert
        var ex = Assert.Throws<BusinessException>(() => _service.Delete(id, userId));
        Assert.AreEqual("Category not found", ex.Message);
    }

    [Test]
    public void Delete_UserMismatch_ThrowsBusinessException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userId = "user-123";
        var category = new Category { Id = id, UserId = "{0B8B508A-3D92-41CD-9490-D7A32A131EAA}" };

        _businessRulesMock.Setup(b => b.CategoryIsPresent(id)).Verifiable();
        _categoryRepositoryMock.Setup(r => r.GetById(id)).Returns(category);

        // Act & Assert
        var ex = Assert.Throws<BusinessException>(() => _service.Delete(id, userId));
        Assert.AreEqual("The Category does not belong to the user!", ex.Message);
    }

    [Test]
    public void GetById_CategoryNotPresent_ThrowsBusinessException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userId = "user-123";

        _businessRulesMock.Setup(b => b.CategoryIsPresent(id)).Throws(new BusinessException("Category not found"));

        // Act & Assert
        var ex = Assert.Throws<BusinessException>(() => _service.GetById(id, userId));
        Assert.AreEqual("Category not found", ex.Message);
    }

    //[Test]
    //public void GetById_UserMismatch_ThrowsBusinessException()
    //{
    //     Arrange
    //    var id = Guid.NewGuid();
    //    var userId = "user-123";
    //    var category = new Category { Id = id, UserId = "different-user-id" };

    //    _businessRulesMock.Setup(b => b.CategoryIsPresent(id)).Verifiable();
    //    _categoryRepositoryMock.Setup(r => r.GetAll(It.IsAny<Func<Category, bool>>())).Returns(new List<Category> { category });

    //     Act & Assert
    //    var ex = Assert.Throws<BusinessException>(() => _service.GetById(id, userId));
    //    Assert.AreEqual("The Category does not belong to the user!", ex.Message);
    //}
}


