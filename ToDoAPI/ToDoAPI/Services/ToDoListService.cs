using AutoMapper;
using ToDoAPI.DTO;
using ToDoAPI.Models;
using ToDoAPI.Repositories;
using ToDoAPI.Repositories.Interfaces;
using ToDoAPI.Services.Interfaces;

namespace ToDoAPI.Services
{
    public class ToDoListService:IToDoListService
    {
        private readonly IToDoListRepository toDoListRepository;
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitofWork;

        public ToDoListService(IToDoListRepository toDoListRepository,IMapper mapper, IUnitofWork unitofWork)
        {
            this.toDoListRepository = toDoListRepository;
            _mapper = mapper;
            _unitofWork = unitofWork;

        }

        public async Task AddToDoListService(ToDoListDTO toDoListDTO)
        {
            try
            {
                ToDoList toDoList=_mapper.Map<ToDoList>(toDoListDTO);
                await toDoListRepository.AddToDoList(toDoList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateToDoListService(ToDoListDTO toDoListDTO)
        {
            try
            {
                ToDoList toDoList = _mapper.Map<ToDoList>(toDoListDTO);
                _unitofWork.ToDoListRepository.Updates(toDoList);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteToDoListService(int toDoListId)
        {
            try
            {
                await toDoListRepository.DeleteToDoList(toDoListId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ToDoListDTO> GetToDoListsService()
        {
            try
            {
                var toDoList = _unitofWork.ToDoListRepository.GetAll();
                List<ToDoListDTO> toDoListDto=_mapper.Map<List<ToDoListDTO>>(toDoList);
                return toDoListDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ToDoListDTO> GetToDoListByUserName(string userName)
        {
            try
            {
                var toDoList = toDoListRepository.GetToDoListByUserName(userName);
                List<ToDoListDTO> toDoListDTOs = _mapper.Map<List<ToDoListDTO>>(toDoList);
                return toDoListDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ToDoListDTO> GetToDoListsById(int toDoListId)
        {
            try
            {
                var toDoListById = toDoListRepository.GetToDoListById(toDoListId);
                List<ToDoListDTO> toDoLists = _mapper.Map<List<ToDoListDTO>>(toDoListById);
                return toDoLists;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ToDoList> ActiveListService(string userName)
        {
            try
            {
                var toDoListData=toDoListRepository.GetToDoListByUserName(userName);
                var activeList = toDoListData.Where(x => x.isCompleted==false).ToList();
                return activeList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ToDoList> CompletedListService(string userName)
        {
            try
            {
                var toDoListData = toDoListRepository.GetToDoListByUserName(userName);
                var completedList=toDoListData.Where(x=>x.isCompleted==true).ToList();
                return completedList;   
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EditToDoListActiveService(int toDoListId,bool isCompleted)
        {
            try
            {
                toDoListRepository.EditToDoListActive(toDoListId,isCompleted);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
