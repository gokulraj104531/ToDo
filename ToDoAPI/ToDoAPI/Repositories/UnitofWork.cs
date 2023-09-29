using ToDoAPI.Data;
using ToDoAPI.Repositories.Interfaces;

namespace ToDoAPI.Repositories
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DataContext _dataContext;
        private IUserRepository _userRepository;
        private IToDoListRepository _todoListRepository;


        public UnitofWork(DataContext dataContext, IUserRepository userRepository, IToDoListRepository todoListRepository)
        {
            _dataContext = dataContext;
            _userRepository = userRepository;
            _todoListRepository = todoListRepository;
        }

        public IToDoListRepository ToDoListRepository {
            get 
            {
                return _todoListRepository = _todoListRepository ?? new ToDoListRepository(_dataContext);
            }
        }

        public IUserRepository  UserRepository {
            get {
                return _userRepository =_userRepository ?? new UserRepository(_dataContext);
            }
        }

        public void Commit()
        {
            _dataContext.SaveChanges();
        }
        
}
}