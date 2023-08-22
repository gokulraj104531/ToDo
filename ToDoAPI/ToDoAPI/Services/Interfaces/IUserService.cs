using ToDoAPI.DTO;

namespace ToDoAPI.Services.Interfaces
{
    public interface IUserService
    {
         Task AddUserService(UserDTO userDTO);

        Task UpdateUserService(UserDTO userDTO);

        Task DeleteUserService(string userName);

        List<UserDTO> GetAllService();


    }
}
