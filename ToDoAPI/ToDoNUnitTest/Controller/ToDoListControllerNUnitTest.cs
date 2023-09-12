using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoAPI.Controllers;
using ToDoAPI.DTO;
using ToDoAPI.Services.Interfaces;

namespace ToDoNUnitTest.Controller
{
    [TestFixture]
    public class ToDoListControllerTests
    {
        private ToDoListController _controller;
        private Mock<IToDoListService> _toDoListServiceMock;

        [SetUp]
        public void Setup()
        {
            _toDoListServiceMock = new Mock<IToDoListService>();
            _controller = new ToDoListController(_toDoListServiceMock.Object);
        }

        [Test]
        public async Task AddToDoList_ValidToDoList_ReturnsOk()
        {
            var toDoListDTO = new ToDoListDTO(); 
            _toDoListServiceMock.Setup(x => x.AddToDoListService(toDoListDTO)).Returns(Task.CompletedTask);

            
            await _controller.AddToDoList(toDoListDTO);

            
        }

        [Test]
        public async Task EditToDoList_ValidToDoList_ReturnsOk()
        {
           
            var toDoListDTO = new ToDoListDTO(); 
            _toDoListServiceMock.Setup(x => x.UpdateToDoListService(toDoListDTO)).Returns(Task.CompletedTask);

            
            var result = await _controller.EditToDoList(toDoListDTO) as OkResult;

         
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task DeleteToDoList_ValidToDoListId_ReturnsNoContent()
        {
            
            var toDoListId = 1; 
            _toDoListServiceMock.Setup(x => x.DeleteToDoListService(toDoListId)).Returns(Task.CompletedTask);

            await _controller.DeleteToDoList(toDoListId);

          
        }

        [Test]
        public void GetToDoLists_ReturnsListOfToDoListDTO()
        {

            var expectedToDoLists = new List<ToDoListDTO>(); 
            _toDoListServiceMock.Setup(x => x.GetToDoListsService()).Returns(expectedToDoLists);


            var result = _controller.GetToDoLists();

         
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ToDoListDTO>>(result);
        }

        [Test]
        public void GetToDoListById_ValidToDoListId_ReturnsListOfToDoListDTO()
        {
 
            var toDoListId = 1;
            var expectedToDoList = new List<ToDoListDTO>(); 
            _toDoListServiceMock.Setup(x => x.GetToDoListsById(toDoListId)).Returns(expectedToDoList);

        
            var result = _controller.GetToDoListById(toDoListId);

       
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ToDoListDTO>>(result);
        }

        [Test]
        public void GetToDoListByUserName_ValidUserName_ReturnsListOfToDoListDTO()
        {
            
            var userName = "validUserName"; 
            var expectedToDoList = new List<ToDoListDTO>(); 
            _toDoListServiceMock.Setup(x => x.GetToDoListByUserName(userName)).Returns(expectedToDoList);

      
            var result = _controller.GetToDoListByUserName(userName);

            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ToDoListDTO>>(result);
        }

       

        [Test]
        public void ToComplete_ValidToDoListId_ReturnsOk()
        {
           
            var toDoListId = 1; 
            _toDoListServiceMock.Setup(x => x.EditToDoListActiveService(toDoListId, true));

         
            var result = _controller.ToComplete(toDoListId) as OkResult;

           
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}










//[Test]
//public void ActiveList_ValidUserName_ReturnsOk()
//{
//    // Arrange
//    var userName = "validUserName"; // Provide a valid user name here
//    var activeLists = new List<ToDoListDTO>(); // Provide active lists data here
//    _toDoListServiceMock.Setup(x => x.ActiveListService(userName)).Returns(activeLists);

//    // Act
//    var result = _controller.ActiveList(userName) as OkObjectResult;

//    // Assert
//    Assert.IsNotNull(result);
//    Assert.AreEqual(200, result.StatusCode);
//}


//public void CompletedList_ValidUserName_ReturnsOk()
//{
//    // Arrange
//    var userName = "validUserName"; // Provide a valid user name here
//    var completedList = new List<ToDoListDTO>(); // Provide completed list data here
//    _toDoListServiceMock.Setup(x => x.CompletedListService(userName)).Returns(completedList);

//    // Act
//    var result = _controller.CompletedList(userName) as OkObjectResult;

//    // Assert
//    Assert.IsNotNull(result);
//    Assert.AreEqual(200, result.StatusCode);
//}