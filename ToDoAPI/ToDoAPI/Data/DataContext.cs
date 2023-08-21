using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;


namespace ToDoAPI.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Todolist> Todolist { get; set; }    

    }
}
