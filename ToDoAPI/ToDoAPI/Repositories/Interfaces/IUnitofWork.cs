namespace ToDoAPI.Repositories.Interfaces
{
    public interface IUnitofWork
    {
        IUserRepository UserRepository { get; }

        IToDoListRepository ToDoListRepository { get; }

        void Commit();
    }
}
