using ToDoAPI.DTO;
using ToDoAPI.Models;

namespace ToDoAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUserService(UserDTO userDTO);

        Task UpdateUserService(UserDTO userDTO);

        Task DeleteUserService(string userName);

        List<UserDTO> GetAllService();

        
        LoginModel LoginServices(string username, string password);
        string GenerateToken(string username);
        bool IsTokenValid(string token);

    }
}
