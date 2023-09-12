using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoAPI.Controllers;
using ToDoAPI.DTO;
using ToDoAPI.Models;
using ToDoAPI.Services.Interfaces;

namespace ToDoNUnitTest.Controller
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController _controller;
        private Mock<IUserService> _userServiceMock;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Test]
        public async Task AddUser_ValidUser_ReturnsOk()
        {
            
            var userDTO = new UserDTO(); 
            _userServiceMock.Setup(x => x.AddUserService(userDTO)).Returns(Task.CompletedTask);

           
            var result = await _controller.AddUser(userDTO) as OkResult;

           
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task EditUser_ValidUser_ReturnsNoContent()
        {
            
            var userDTO = new UserDTO(); 
            _userServiceMock.Setup(x => x.UpdateUserService(userDTO)).Returns(Task.CompletedTask);


            var result = await _controller.Edit(userDTO) as NoContentResult;

    
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public async Task DeleteUser_ValidUserName_ReturnsOk()
        {

            var userName = "validUserName"; 
            _userServiceMock.Setup(x => x.DeleteUserService(userName)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteUser(userName) as OkResult;

          
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void GetUsers_ReturnsListOfUserDTO()
        {
            
            var expectedUsers = new List<UserDTO>(); 
            _userServiceMock.Setup(x => x.GetAllService()).Returns(expectedUsers);

            
            var result = _controller.GetUsers();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<UserDTO>>(result);
        }

       [Test]
        public void Login_ValidUser_ReturnsToken()
        {
     
        var user = "validUser"; 
        var pass = "validPassword"; 
        var expectedToken = "validToken";
        var expectedLoginModel = new LoginModel { Token = expectedToken }; 
    
        _userServiceMock.Setup(x => x.LoginServices(user, pass)).Returns(expectedLoginModel);
        _userServiceMock.Setup(x => x.GenerateToken(user)).Returns(expectedToken);

        var result = _controller.Login(user, pass);

        Assert.AreEqual(expectedToken, result);
        }


    }
}
