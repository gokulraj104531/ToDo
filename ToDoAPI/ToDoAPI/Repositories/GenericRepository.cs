using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Repositories.Interfaces;

namespace ToDoAPI.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        protected readonly DataContext _dataContext;
        private readonly DbSet<T> _dbSet;


        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
        }

        public void Updates(T entity)
        {
            _dbSet.Update(entity);
            _dataContext.SaveChanges();
        }

        public List<T> GetAll()
        {
            List<T> list = _dbSet.ToList();
            return list;
        }


    }
}
