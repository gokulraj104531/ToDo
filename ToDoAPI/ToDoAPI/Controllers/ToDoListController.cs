using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ToDoAPI.DTO;
using ToDoAPI.Services.Interfaces;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListService toDoListService;

        public ToDoListController(IToDoListService toDoListService)
        {
            this.toDoListService = toDoListService;
        }


        [HttpPost]
        [Route("AddToDoList")]
        public async Task AddToDoList(ToDoListDTO toDoListDTO)
        {
            try
            {
                await toDoListService.AddToDoListService(toDoListDTO);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPut]
        [Route("EditToDoList")]
        public async Task<IActionResult> EditToDoList(ToDoListDTO toDoListDTO)
        {
            try
            {
                await toDoListService.UpdateToDoListService(toDoListDTO);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteToDoList/{toDoListId}")]
        public async Task DeleteToDoList(int toDoListId)
        {
            try
            {
                await toDoListService.DeleteToDoListService(toDoListId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetToDoList")]
        public List<ToDoListDTO> GetToDoLists()
        {
            try
            {
                var toDoLists = toDoListService.GetToDoListsService();
                return toDoLists;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetToDoListByUserName/{userName}")]
        public List<ToDoListDTO> GetToDoListByUserName(string userName)
        {
            try
            {
                var toDoListByUsername = toDoListService.GetToDoListByUserName(userName);
                return toDoListByUsername;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("ActiveList/{userName}")]
        public IActionResult ActiveList(string userName)
        {
            try
            {
                var activeLists = toDoListService.ActiveListService(userName).ToList();
                return Ok(activeLists);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("CompletedList/{userName}")]
        public IActionResult CompletedList(string userName)
        {
            try
            {
                var completedList = toDoListService.CompletedListService(userName).ToList();
                return Ok(completedList);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("ToComplete/{toDoListId}")]
        public IActionResult ToComplete(int toDoListId)
        {
            try
            {
                toDoListService.EditToDoListActiveService(toDoListId,true);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }






    }
}
