using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Data;
using ToDoAPI.Models;
using ToDoAPI.Repositories;
using ToDoAPI.Repositories.Interfaces;

namespace ToDoNUnitTest.Reposistory
{
    [TestFixture]
    public class UserRepoistoryNUnitTest
    {
        private DataContext _dataContext;
        private UserRepository _userRepository;
        
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dataContext = new DataContext(options);
            _userRepository = new UserRepository(_dataContext);
        }




        [Test]
        public async Task AddUser_ValidUser_ShouldAddUser()
        {
            var user = new User
            {
                UserName = "Test123",
                Password = "password",
                Name = "Test",
                Email = "test@gmail.com",
                PhoneNumber = "1234567890",
            };


            await _userRepository.AddUser(user);

            var addUser=_dataContext.Users.FirstOrDefault(u=> u.UserName == user.UserName);
            Assert.IsNotNull(addUser);
            Assert.AreEqual("Test123", addUser.UserName);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllUser()
        {
           
            var users = new List<User>
            {
                new User { UserName = "user1", Password = "password1",Name="user11",Email="user1@gmail.com",PhoneNumber="213456789" },
                new User { UserName = "user2", Password = "password2" ,Name="user12",Email="user2@gmail.com",PhoneNumber="452313389"},
                new User { UserName = "user3", Password = "password3",Name="user13",Email="user3@gmail.com",PhoneNumber="213452319" }
            };
            _dataContext.Users.AddRange(users);
            await _dataContext.SaveChangesAsync();

           
            var allUsers = _userRepository.GetAll();

            
            Assert.AreEqual(users.Count, allUsers.Count);
        }

        [Test]
        public async Task DeleteUser_ExistingUser_ShouldDeleteUser()
        {
            var user = new User
            {
                UserName = "test123",
                Password = "password",
                Name = "Test",
                Email = "test@gmail.com",
                PhoneNumber = "1234567890",
            };
            await _userRepository.AddUser(user);

            await _userRepository.DeleteUser("test123");

            var deletedUser = _dataContext.Users.FirstOrDefault(u => u.UserName == "test123");
            Assert.IsNull(deletedUser);
        }


        [Test]
        public async Task UpdateUser_ValidUser_ShouldUpdateUser()
        {
            var user = new User
            {
                UserName = "Test123",
                Password = "password",
                Name = "Test",
                Email = "test@gmail.com",
                PhoneNumber = "1234567890",
            };
            await _userRepository.AddUser(user);

            user.Password = "newpassword";
            var updatedUser = await _userRepository.UpdateUser(user);

            Assert.AreEqual("newpassword", updatedUser.Password);
        }

        [Test]
        public void GetByUserName_ExistingUser_ShouldReturnUser()
        {
            var user = new User
            {
                UserName = "gokulraj1045",
                Password = "password",
                Name = "Test",
                Email = "test@gmail.com",
                PhoneNumber = "1234567890",
            };
            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();

            var foundUser = _userRepository.GetByUserName("gokulraj1045");

            Assert.IsNotNull(foundUser);
            Assert.AreEqual("gokulraj1045", foundUser.UserName);
        }

        [Test]
        public void GetByUserName_NonExistingUser_ShouldReturnNull()
        {
            var foundUser = _userRepository.GetByUserName("gokulraj104531");
            Assert.IsNull(foundUser);
        }


        [Test]
        public async Task Login_ValidCredentials_ShouldReturnUser()
        {
            var user = new User
            {
                UserName = "mrwhite",
                Password = ("password123")
            };
            var result =_userRepository.AddUser(user);

            var loggedInUser = _userRepository.Login("mrwhite", "password123");

            Assert.IsNotNull(loggedInUser);
            Assert.AreEqual("mrwhite", loggedInUser.UserName);
        }

    }
}
