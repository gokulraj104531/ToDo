using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Models;
using ToDoAPI.Repositories.Interfaces;

namespace ToDoAPI.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddUser(User user)
        {
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _dataContext.Users.Add(user);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public  List<User> GetAll()
        {
            try
            {
                return _dataContext.Users.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteUser(string userName)
        {
            try
            {
                var user= await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
                if (user != null)
                {

                    _dataContext.Users.Remove(user);
                    await _dataContext.SaveChangesAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                _dataContext.Users.Update(user);
                await _dataContext.SaveChangesAsync();
                return user;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public User GetByUserName(string userName)
        {
            try
            {
                User user = _dataContext.Users.FirstOrDefault(x => x.UserName == userName);
                if (user != null)
                {
                    return user;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User? Login(string username, string password)
        {
            try
            {
                User userRegistration = _dataContext.Users.Find(username);
                if (userRegistration != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, userRegistration.Password))
                    {
                        return userRegistration;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
