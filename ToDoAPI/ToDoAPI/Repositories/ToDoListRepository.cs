﻿using ToDoAPI.Data;
using ToDoAPI.Models;
using ToDoAPI.Repositories.Interfaces;

namespace ToDoAPI.Repositories
{
    public class ToDoListRepository: IToDoListRepository
    {
        private readonly DataContext _dataContext;
        public ToDoListRepository(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public async Task AddToDoList(ToDoList toDoList)
        {
            try
            {
                _dataContext.ToDoLists.Add(toDoList);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<ToDoList> GetToDoLists()
        {
            try
            {
                return _dataContext.ToDoLists.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ToDoList> UpdateToDoList(ToDoList toDoList)
        {
            try
            {
                _dataContext.ToDoLists.Update(toDoList);
                await _dataContext.SaveChangesAsync();
                return toDoList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteToDoList(int toDoListId)
        {
            try
            {
                var toDoList=_dataContext.ToDoLists.FirstOrDefault(x=>x.ToDoListId == toDoListId);
                if(toDoList != null)
                {
                    _dataContext.ToDoLists.Remove(toDoList);
                    await _dataContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ToDoList> GetToDoListByUserName(string userName)
        {
            try
            {
                return _dataContext.ToDoLists.Where(x=>x.UserName == userName).ToList();    

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ToDoList> GetToDoListById(int toDoListId)
        {
            try
            {
                var getToDoListById = _dataContext.ToDoLists.Where(x => x.ToDoListId == toDoListId).ToList();
                return getToDoListById;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditToDoListActive(int toDoListId,bool isCompleted)
        {
            try
            {
                //var editToDoListActive = _dataContext.ToDoLists.FirstOrDefault(x => x.ToDoListId == toDoListId);
                //editToDoListActive.isCompleted = true;
                var task = _dataContext.ToDoLists.Find(toDoListId);
                if (task != null)
                {
                    task.isCompleted = isCompleted;
                    _dataContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
