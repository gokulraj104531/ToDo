using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.Data;
using ToDoAPI.Models;
using ToDoAPI.Repositories;

namespace ToDoNUnitTest.Reposistory
{
    [TestFixture]
    public class ToDoListRepositoryTests
    {
        private DataContext _dataContext;
        private ToDoListRepository _toDoListRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dataContext = new DataContext(options);
            _toDoListRepository = new ToDoListRepository(_dataContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Dispose();
        }

        [Test]
        public async Task AddToDoList_ValidToDoList_ShouldAddToDoList()
        {
         
            var toDoList = new ToDoList
            {
                ToDoTitle = "Test ToDo",
                ToDoListDescription = "Test Description",
                UserName = "testuser",
                isCompleted = false,

            };

            await _toDoListRepository.AddToDoList(toDoList);

           
            var addedToDoList = _dataContext.ToDoLists.FirstOrDefault();
            Assert.IsNotNull(addedToDoList);
            Assert.AreEqual("Test ToDo", addedToDoList.ToDoTitle);
            Assert.AreEqual("Test Description", addedToDoList.ToDoListDescription);
            Assert.AreEqual("testuser", addedToDoList.UserName);
            Assert.IsFalse(addedToDoList.isCompleted);
        }

        [Test]
        public async Task GetToDoLists_ShouldReturnAllToDoLists()
        {
            // Arrange
            var toDoList1 = new ToDoList
            {
                ToDoTitle = "ToDo 1",
                UserName = "testuser",
                isCompleted = false
            };
            var toDoList2 = new ToDoList
            {
                ToDoTitle = "ToDo 2",
                UserName = "testuser",
                isCompleted = true
            };
            var toDoList3 = new ToDoList
            {
                ToDoTitle = "ToDo 3",
                UserName = "otheruser",
                isCompleted = false
            };
            await _toDoListRepository.AddToDoList(toDoList1);
            await _toDoListRepository.AddToDoList(toDoList2);
            await _toDoListRepository.AddToDoList(toDoList3);

            
            var allToDoLists = _toDoListRepository.GetToDoLists();

            
            Assert.AreEqual(3, allToDoLists.Count);
        }

        [Test]
        public async Task UpdateToDoList_ValidToDoList_ShouldUpdateToDoList()
        {
            
            var toDoList = new ToDoList
            {
                ToDoTitle = "Test ToDo",
                ToDoListDescription = "Test Description",
                UserName = "testuser",
                isCompleted = false
            };
            await _toDoListRepository.AddToDoList(toDoList);

            toDoList.ToDoTitle = "Updated ToDo";

            var updatedToDoList = await _toDoListRepository.UpdateToDoList(toDoList);

            Assert.AreEqual("Updated ToDo", updatedToDoList.ToDoTitle);
        }

        [Test]
        public async Task DeleteToDoList_ExistingToDoList_ShouldDeleteToDoList()
        {
           
            var toDoList = new ToDoList
            {
                ToDoTitle = "Test ToDo",
                ToDoListDescription = "Test Description",
                UserName = "testuser",
                isCompleted = false
            };
            await _toDoListRepository.AddToDoList(toDoList);

            
            await _toDoListRepository.DeleteToDoList(toDoList.ToDoListId);

            
            var deletedToDoList = _dataContext.ToDoLists.FirstOrDefault();
            Assert.IsNull(deletedToDoList);
        }

        [Test]
        public async Task GetToDoListByUserName_ShouldReturnMatchingToDoLists()
        {
            
            var toDoList1 = new ToDoList
            {
                ToDoTitle = "ToDo 1",
                UserName = "testuser",
                isCompleted = false
            };
            var toDoList2 = new ToDoList
            {
                ToDoTitle = "ToDo 2",
                UserName = "testuser",
                isCompleted = true
            };
            var toDoList3 = new ToDoList
            {
                ToDoTitle = "ToDo 3",
                UserName = "otheruser",
                isCompleted = false
            };
            await _toDoListRepository.AddToDoList(toDoList1);
            await _toDoListRepository.AddToDoList(toDoList2);
            await _toDoListRepository.AddToDoList(toDoList3);

            
            var matchingToDoLists = _toDoListRepository.GetToDoListByUserName("testuser");

            
            Assert.AreEqual(2, matchingToDoLists.Count);
            Assert.IsTrue(matchingToDoLists.All(t => t.UserName == "testuser"));
        }

        [Test]
        public async Task GetToDoListById_ExistingToDoList_ShouldReturnToDoList()
        {
            var toDoList = new ToDoList
            {
                ToDoTitle = "Test ToDo",
                ToDoListDescription = "Test Description",
                UserName = "testuser",
                isCompleted = false
            };
            await _toDoListRepository.AddToDoList(toDoList);

            var retrievedToDoList = _toDoListRepository.GetToDoListById(toDoList.ToDoListId);

        
            Assert.IsNotNull(retrievedToDoList);
            Assert.AreEqual(1, retrievedToDoList.Count);
            Assert.AreEqual("Test ToDo", retrievedToDoList[0].ToDoTitle);
        }

        [Test]
        public async Task EditToDoListActive_ShouldEditActiveStatus()
        {
           
            var toDoList = new ToDoList
            {
                ToDoTitle = "Test ToDo",
                ToDoListDescription = "Test Description",
                UserName = "testuser",
                isCompleted = false
            };
            await _toDoListRepository.AddToDoList(toDoList);

      
            _toDoListRepository.EditToDoListActive(toDoList.ToDoListId, true);

            var editedToDoList = _dataContext.ToDoLists.FirstOrDefault();
            Assert.IsTrue(editedToDoList.isCompleted);
        }
    }
}
