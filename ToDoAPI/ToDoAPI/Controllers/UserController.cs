using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ToDoAPI.DTO;
using ToDoAPI.Models;
using ToDoAPI.Repositories;
using ToDoAPI.Services.Interfaces;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task AddUser(UserDTO userDTO)
        {
            try
            {
                await _userService.AddUserService(userDTO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("EditUser")]
        public async Task<IActionResult> Edit(UserDTO userDTO)
        {
            try
            {
                await _userService.UpdateUserService(userDTO);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteUser/{userName}")]
        public async Task DeleteUser(string userName)
        {
            try
            {
                await _userService.DeleteUserService(userName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetUsers")]
        public List<UserDTO> GetUsers()
        {
            try
            {
                var users = _userService.GetAllService();
                return users;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("Login/{user}/{pass}")]
        public string? Login(string user, string pass)
        {
            try
            {
                var validuser = _userService.LoginServices(user, pass);
                if (validuser != null)
                {
                    var result = _userService.GenerateToken(user);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
