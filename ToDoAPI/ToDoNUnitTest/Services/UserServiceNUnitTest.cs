using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoAPI.DTO;
using ToDoAPI.Models;
using ToDoAPI.Repositories.Interfaces;
using ToDoAPI.Services;
using ToDoAPI.Services.Interfaces;

namespace ToDoNUnitTest.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _userService;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();

            _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task AddUserService_ValidUserDTO_ShouldCallAddUserRepository()
        {
            // Arrange
            var userDTO = new UserDTO
            {
                // Initialize userDTO properties as needed
                UserName = "testuser",
                Name = "Test User",
                Email = "test@example.com",
                Password = "password",
                PhoneNumber = "1234567890"
            };

            _mapperMock.Setup(m => m.Map<User>(userDTO)).Returns(new User());

            // Act
            await _userService.AddUserService(userDTO);

            // Assert
            _userRepositoryMock.Verify(r => r.AddUser(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task UpdateUserService_ValidUserDTO_ShouldCallUpdateUserRepository()
        {
            // Arrange
            var userDTO = new UserDTO
            {
                // Initialize userDTO properties as needed
                UserName = "testuser",
                Name = "Test User",
                Email = "test@example.com",
                Password = "password",
                PhoneNumber = "1234567890"
            };

            _mapperMock.Setup(m => m.Map<User>(userDTO)).Returns(new User());

            // Act
            await _userService.UpdateUserService(userDTO);

            // Assert
            _userRepositoryMock.Verify(r => r.UpdateUser(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task DeleteUserService_ValidUserName_ShouldCallDeleteUserRepository()
        {
            // Arrange
            var userName = "testuser";

            // Act
            await _userService.DeleteUserService(userName);

            // Assert
            _userRepositoryMock.Verify(r => r.DeleteUser(userName), Times.Once);
        }

        [Test]
        public void GetAllService_ShouldReturnListOfUserDTO()
        {
            // Arrange
            var users = new List<User>
            {
                new User
                {
                    UserName = "user1",
                    Name = "User One",
                    Email = "user1@example.com",
                    Password = "password1",
                    PhoneNumber = "1111111111"
                },
                new User
                {
                    UserName = "user2",
                    Name = "User Two",
                    Email = "user2@example.com",
                    Password = "password2",
                    PhoneNumber = "2222222222"
                }
                // Add more users as needed
            };

            var userDTOs = users.Select(u => new UserDTO
            {
                UserName = u.UserName,
                Name = u.Name,
                Email = u.Email,
                Password = u.Password,
                PhoneNumber = u.PhoneNumber
            }).ToList();

            _userRepositoryMock.Setup(r => r.GetAll()).Returns(users);
            _mapperMock.Setup(m => m.Map<List<UserDTO>>(users)).Returns(userDTOs);

            // Act
            var result = _userService.GetAllService();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<UserDTO>>(result);
            Assert.AreEqual(users.Count, result.Count);
        }

        [Test]
        public void LoginServices_ValidCredentials_ShouldReturnLoginModel()
        {
            // Arrange
            var username = "testuser";
            var password = "password";

            var user = new User
            {
                UserName = username,
                Name = "Test User",
                Email = "test@example.com",
                Password = password,
                PhoneNumber = "1234567890"
            };

            _userRepositoryMock.Setup(r => r.Login(username, password)).Returns(user);

            // Act
            var result = _userService.LoginServices(username, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<LoginModel>(result);
            Assert.AreEqual(username, result.UserName);
        }

        [Test]
        public void GenerateToken_ValidUsername_ShouldReturnToken()
        {
            // Arrange
            var username = "testuser";

            // Act
            var result = _userService.GenerateToken(username);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void IsTokenValid_ValidToken_ShouldReturnTrue()
        {
            // Arrange
            var token = "valid_token";

            // Act
            var result = _userService.IsTokenValid(token);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsTokenValid_InvalidToken_ShouldReturnFalse()
        {
            // Arrange
            var token = "invalid_token";

            // Act
            var result = _userService.IsTokenValid(token);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
