
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.DTO;
using ToDoAPI.Models;
using ToDoAPI.Repositories.Interfaces;
using ToDoAPI.Services;
using ToDoAPI.Services.Interfaces;

namespace ToDoNUnitTest.Services
{
    [TestFixture]
    public class ToDoListServiceTests
    {
        private IToDoListService _toDoListService;
        private Mock<IToDoListRepository> _toDoListRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _toDoListRepositoryMock = new Mock<IToDoListRepository>();
            _mapperMock = new Mock<IMapper>();

            _toDoListService = new ToDoListService(_toDoListRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task AddToDoListService_ValidToDoListDTO_ShouldCallAddToDoListRepository()
        {
           
            var toDoListDTO = new ToDoListDTO
            {
               
                ToDoTitle = "Test ToDo",
                ToDoListDescription = "Test Description",
                UserName = "testuser",
                isCompleted = false
            };

            _mapperMock.Setup(m => m.Map<ToDoList>(toDoListDTO)).Returns(new ToDoList());

           
            await _toDoListService.AddToDoListService(toDoListDTO);

            
            _toDoListRepositoryMock.Verify(r => r.AddToDoList(It.IsAny<ToDoList>()), Times.Once);
        }

        [Test]
        public async Task UpdateToDoListService_ValidToDoListDTO_ShouldCallUpdateToDoListRepository()
        {
           
            var toDoListDTO = new ToDoListDTO
            {
               
                ToDoTitle = "Test ToDo",
                ToDoListDescription = "Test Description",
                UserName = "testuser",
                isCompleted = false
            };

            _mapperMock.Setup(m => m.Map<ToDoList>(toDoListDTO)).Returns(new ToDoList());

           
            await _toDoListService.UpdateToDoListService(toDoListDTO);

           
            _toDoListRepositoryMock.Verify(r => r.UpdateToDoList(It.IsAny<ToDoList>()), Times.Once);
        }

        [Test]
        public async Task DeleteToDoListService_ValidToDoListId_ShouldCallDeleteToDoListRepository()
        {
           
            var toDoListId = 1;

       
            await _toDoListService.DeleteToDoListService(toDoListId);

            _toDoListRepositoryMock.Verify(r => r.DeleteToDoList(toDoListId), Times.Once);
        }

        [Test]
        public void GetToDoListsService_ShouldReturnListOfToDoListDTO()
        {
          
            var toDoLists = new List<ToDoList>
            {
                new ToDoList
                {
                    ToDoTitle = "ToDo 1",
                    ToDoListDescription = "Description 1",
                    UserName = "testuser",
                    isCompleted = false
                },
                new ToDoList
                {
                    ToDoTitle = "ToDo 2",
                    ToDoListDescription = "Description 2",
                    UserName = "testuser",
                    isCompleted = true
                }
                
            };

            var toDoListDTOs = toDoLists.Select(t => new ToDoListDTO
            {
                ToDoTitle = t.ToDoTitle,
                ToDoListDescription = t.ToDoListDescription,
                UserName = t.UserName,
                isCompleted = t.isCompleted
            }).ToList();

            _toDoListRepositoryMock.Setup(r => r.GetToDoLists()).Returns(toDoLists);
            _mapperMock.Setup(m => m.Map<List<ToDoListDTO>>(toDoLists)).Returns(toDoListDTOs);

            
            var result = _toDoListService.GetToDoListsService();

           
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ToDoListDTO>>(result);
            Assert.AreEqual(toDoLists.Count, result.Count);
        }

        [Test]
        public void GetToDoListByUserName_ShouldReturnListOfToDoListDTO()
        {
            
            var userName = "testuser";
            var toDoLists = new List<ToDoList>
            {
                new ToDoList
                {
                    ToDoTitle = "ToDo 1",
                    ToDoListDescription = "Description 1",
                    UserName = userName,
                    isCompleted = false
                },
                new ToDoList
                {
                    ToDoTitle = "ToDo 2",
                    ToDoListDescription = "Description 2",
                    UserName = userName,
                    isCompleted = true
                }
                
            };

            var toDoListDTOs = toDoLists.Select(t => new ToDoListDTO
            {
                ToDoTitle = t.ToDoTitle,
                ToDoListDescription = t.ToDoListDescription,
                UserName = t.UserName,
                isCompleted = t.isCompleted
            }).ToList();

            _toDoListRepositoryMock.Setup(r => r.GetToDoListByUserName(userName)).Returns(toDoLists);
            _mapperMock.Setup(m => m.Map<List<ToDoListDTO>>(toDoLists)).Returns(toDoListDTOs);

            
            var result = _toDoListService.GetToDoListByUserName(userName);

            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ToDoListDTO>>(result);
            Assert.AreEqual(toDoLists.Count, result.Count);
        }

        [Test]
        public void GetToDoListsById_ShouldReturnListOfToDoListDTO()
        {
            
            var toDoListId = 1;
            var toDoLists = new List<ToDoList>
            {
                new ToDoList
                {
                    ToDoListId = toDoListId,
                    ToDoTitle = "ToDo 1",
                    ToDoListDescription = "Description 1",
                    UserName = "testuser",
                    isCompleted = false
                }

            };

            var toDoListDTOs = toDoLists.Select(t => new ToDoListDTO
            {
                ToDoTitle = t.ToDoTitle,
                ToDoListDescription = t.ToDoListDescription,
                UserName = t.UserName,
                isCompleted = t.isCompleted
            }).ToList();

            _toDoListRepositoryMock.Setup(r => r.GetToDoListById(toDoListId)).Returns(toDoLists);
            _mapperMock.Setup(m => m.Map<List<ToDoListDTO>>(toDoLists)).Returns(toDoListDTOs);

           
            var result = _toDoListService.GetToDoListsById(toDoListId);

     
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ToDoListDTO>>(result);
            Assert.AreEqual(toDoLists.Count, result.Count);
        }

        [Test]
        public void ActiveListService_ShouldReturnListOfToDoList()
        {
            
            var userName = "testuser";
            var toDoLists = new List<ToDoList>
            {
                new ToDoList
                {
                    ToDoTitle = "ToDo 1",
                    UserName = userName,
                    isCompleted = false
                },
                new ToDoList
                {
                    ToDoTitle = "ToDo 2",
                    UserName = userName,
                    isCompleted = true
                },
                new ToDoList
                {
                    ToDoTitle = "ToDo 3",
                    UserName = userName,
                    isCompleted = false
                }
                
            };

            _toDoListRepositoryMock.Setup(r => r.GetToDoListByUserName(userName)).Returns(toDoLists);

            
            var result = _toDoListService.ActiveListService(userName);

           
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ToDoList>>(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(t => t.isCompleted == false));
        }

        [Test]
        public void CompletedListService_ShouldReturnListOfToDoList()
        {
            
            var userName = "testuser";
            var toDoLists = new List<ToDoList>
            {
                new ToDoList
                {
                    ToDoTitle = "ToDo 1",
                    UserName = userName,
                    isCompleted = false
                },
                new ToDoList
                {
                    ToDoTitle = "ToDo 2",
                    UserName = userName,
                    isCompleted = true
                },
                new ToDoList
                {
                    ToDoTitle = "ToDo 3",
                    UserName = userName,
                    isCompleted = true
                }
               
            };

            _toDoListRepositoryMock.Setup(r => r.GetToDoListByUserName(userName)).Returns(toDoLists);

           
            var result = _toDoListService.CompletedListService(userName);

            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ToDoList>>(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(t => t.isCompleted == true));
        }

        [Test]
        public void EditToDoListActiveService_ValidToDoListId_ShouldCallEditToDoListActiveRepository()
        {
            
            var toDoListId = 1;
            var isCompleted = true;

           
            _toDoListService.EditToDoListActiveService(toDoListId, isCompleted);

          
            _toDoListRepositoryMock.Verify(r => r.EditToDoListActive(toDoListId, isCompleted), Times.Once);
        }
    }
}