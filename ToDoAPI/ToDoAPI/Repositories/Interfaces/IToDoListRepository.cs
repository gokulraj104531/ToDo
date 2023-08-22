using ToDoAPI.Models;

namespace ToDoAPI.Repositories.Interfaces
{
    public interface IToDoListRepository
    {
        Task AddToDoList(ToDoList toDoList);

        List<ToDoList> GetToDoLists();
        Task<ToDoList> UpdateToDoList(ToDoList toDoList);

        Task DeleteToDoList(int toDoListId);

        List<ToDoList> GetToDoListByUserName(string userName);

        List<ToDoList> GetToDoListById(int toDoListId);

        void EditToDoListActive(int toDoListId,bool isCompleted);
    }
}
