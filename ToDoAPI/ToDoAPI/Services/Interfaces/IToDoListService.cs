using ToDoAPI.DTO;
using ToDoAPI.Models;

namespace ToDoAPI.Services.Interfaces
{
    public interface IToDoListService
    {
        Task AddToDoListService(ToDoListDTO toDoListDTO);

        Task UpdateToDoListService(ToDoListDTO toDoListDTO);

        Task DeleteToDoListService(int toDoListId);

        List<ToDoListDTO> GetToDoListsService();

        List<ToDoListDTO> GetToDoListByUserName(string userName);


        List<ToDoListDTO> GetToDoListsById(int toDoListId);

        List<ToDoList> ActiveListService(string userName);

        List<ToDoList> CompletedListService(string userName);

        void EditToDoListActiveService(int toDoListId, bool isCompleted);
    }
}
