using ToDoAPI.Models;

namespace ToDoAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        
        Task<User> UpdateUser(User user);

        Task DeleteUser(string userName);

        List<User> GetAll();

        User GetByUserName(string userName);
        User? Login(string username, string password);
    }
}
