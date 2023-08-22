using AutoMapper;
using System.ComponentModel;
using ToDoAPI.DTO;
using ToDoAPI.Models;
using ToDoAPI.Repositories;
using ToDoAPI.Repositories.Interfaces;
using ToDoAPI.Services.Interfaces;

namespace ToDoAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userrepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userrepository, IMapper mapper)
        {
            this.userrepository = userrepository;
            _mapper = mapper;
        }

        public async Task AddUserService(UserDTO userDTO)
        {
            try
            {
                User user = _mapper.Map<User>(userDTO);
                await userrepository.AddUser(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateUserService(UserDTO userDTO)
        {
            try
            {
                User user = _mapper.Map<User>(userDTO);
                await userrepository.UpdateUser(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUserService(string userName)
        {
            try
            {
                await userrepository.DeleteUser(userName);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UserDTO> GetAllService()
        {
            try
            {
                var user = userrepository.GetAll();
                List<UserDTO> userDto = _mapper.Map<List<UserDTO>>(user);
                return userDto;
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}
