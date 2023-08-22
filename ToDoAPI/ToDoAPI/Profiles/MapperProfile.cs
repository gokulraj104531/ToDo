using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ToDoAPI.DTO;
using ToDoAPI.Models;

namespace ToDoAPI.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile() {
            CreateMap<User,UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<ToDoList, ToDoListDTO>();
            CreateMap<ToDoListDTO, ToDoList>();

            CreateMap<User, LoginModel>();
            CreateMap<LoginModel, User>();

        }

    }
}
