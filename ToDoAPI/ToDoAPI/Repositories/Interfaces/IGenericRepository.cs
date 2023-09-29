namespace ToDoAPI.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Updates(T  entity);

        List<T> GetAll();
    }
}
