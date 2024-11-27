using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using ToDoAssignment.Models.Users.Dtos.Requests;
using ToDoAssignment.Models.Users.Dtos.Response;
using ToDoAssignment.Models.Users.Entity;
using ToDoAssignment.Service.Constants;
using ToDoAssignment.Service.Services.Users.Concretes;

namespace ToDoAssignment.Tests.Services.Users
{
    [TestFixture]
    public class EfUserServiceTests
    {
        private Mock<UserManager<User>> _userManagerMock;
        private Mock<IMapper> _mapperMock;
        private EfUserService _efUserService;

        [SetUp]
        public void SetUp()
        {
            _userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null
            );
            _mapperMock = new Mock<IMapper>();
            _efUserService = new EfUserService(_mapperMock.Object,_userManagerMock.Object);
        }

        [Test]
        public async Task RegisterUserAsync_ShouldReturnSuccess_WhenUserIsCreated()
        {
            // Arrange
            var dto = new RegisterUserRequestDto { Username = "testuser", Email = "test@example.com", Password = "Password123" };
            var user = new User { UserName = dto.Username, Email = dto.Email };

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), dto.Password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _efUserService.RegisterUserAsync(dto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.UserRegisteredMessage, result.Message);
            _userManagerMock.Verify(x => x.CreateAsync(It.Is<User>(u => u.UserName == dto.Username), dto.Password), Times.Once);
        }

        [Test]
        public async Task RegisterUserAsync_ShouldThrowException_WhenCreationFails()
        {
            // Arrange
            var dto = new RegisterUserRequestDto { Username = "testuser", Email = "test@example.com", Password = "Password123" };

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), dto.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Failed to create user" }));

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessException>(async () => await _efUserService.RegisterUserAsync(dto));
            Assert.AreEqual("Failed to create user", ex.Message);
        }

        [Test]
        public async Task LoginAsync_ShouldReturnUser_WhenCredentialsAreCorrect()
        {
            // Arrange
            var dto = new LoginRequestDto { Username = "testuser", Password = "Password123" };
            var user = new User { UserName = dto.Username };

            _userManagerMock.Setup(x => x.FindByNameAsync(dto.Username)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, dto.Password)).ReturnsAsync(true);

            // Act
            var result = await _efUserService.LoginAsync(dto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user, result);
            _userManagerMock.Verify(x => x.CheckPasswordAsync(user, dto.Password), Times.Once);
        }

        [Test]
        public void LoginAsync_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var dto = new LoginRequestDto { Username = "nonexistentuser", Password = "Password123" };

            _userManagerMock.Setup(x => x.FindByNameAsync(dto.Username)).ReturnsAsync((User)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _efUserService.LoginAsync(dto));
            Assert.AreEqual("The User could not be found.", ex.Message);
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnSuccess_WhenUpdateIsSuccessful()
        {
            // Arrange
            var id = "test-id";
            var dto = new UpdateUserRequestDto { Username = "updatedUser" };
            var user = new User { Id = id, UserName = "testUser" };

            _userManagerMock.Setup(x => x.FindByIdAsync(id)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _efUserService.UpdateAsync(id, dto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.UserUpdatedMessage, result.Message);
            Assert.AreEqual(dto.Username, user.UserName);
            _userManagerMock.Verify(x => x.UpdateAsync(user), Times.Once);
        }

        [Test]
        public void UpdateAsync_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var id = "invalid-id";
            var dto = new UpdateUserRequestDto { Username = "updatedUser" };

            _userManagerMock.Setup(x => x.FindByIdAsync(id)).ReturnsAsync((User)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _efUserService.UpdateAsync(id, dto));
            Assert.AreEqual("The User could not be found.", ex.Message);
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnSuccess_WhenDeleteIsSuccessful()
        {
            // Arrange
            var id = "test-id";
            var user = new User { Id = id };

            _userManagerMock.Setup(x => x.FindByIdAsync(id)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _efUserService.DeleteAsync(id);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.UserDeletedMessage, result.Message);
            _userManagerMock.Verify(x => x.DeleteAsync(user), Times.Once);
        }

        [Test]
        public void DeleteAsync_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var id = "invalid-id";

            _userManagerMock.Setup(x => x.FindByIdAsync(id)).ReturnsAsync((User)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _efUserService.DeleteAsync(id));
            Assert.AreEqual("The User could not be found.", ex.Message);
        }
    }
}
