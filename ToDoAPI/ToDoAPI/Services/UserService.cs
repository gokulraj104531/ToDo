using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ToDoAPI.DTO;
using ToDoAPI.Models;
using ToDoAPI.Repositories;
using ToDoAPI.Repositories.Interfaces;
using ToDoAPI.Services.Interfaces;

namespace ToDoAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userrepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userrepository, IMapper mapper)
        {
            this.userrepository = userrepository;
            _mapper = mapper;
        }

        public async Task AddUserService(UserDTO userDTO)
        {
            try
            {
                User user = _mapper.Map<User>(userDTO);
                await userrepository.AddUser(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateUserService(UserDTO userDTO)
        {
            try
            {
                User user = _mapper.Map<User>(userDTO);
                await userrepository.UpdateUser(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUserService(string userName)
        {
            try
            {
                await userrepository.DeleteUser(userName);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UserDTO> GetAllService()
        {
            try
            {
                var user = userrepository.GetAll();
                List<UserDTO> userDto = _mapper.Map<List<UserDTO>>(user);
                return userDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUserByUserName(string userName)
        {
            return userrepository.GetByUserName(userName);
        }

        public LoginModel LoginServices(string username, string password)
        {
            try
            {
                var login = userrepository.Login(username, password);
                LoginModel model = _mapper.Map<LoginModel>(login);
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public string GenerateToken(string username)
        {
            byte[] keyBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }
            var key = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "http://localhost:7012",
                audience: "https://localhost:4200",
                claims: new[] { new Claim("UserName", username) },
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public bool IsTokenValid(string token)
        {
            try
            {
                byte[] keyBytes = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(keyBytes);
                }
                var key = new SymmetricSecurityKey((keyBytes));
                var tokenHandler = new JwtSecurityTokenHandler();



                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:7012",
                    ValidAudience = "http://localhost:4200",
                    IssuerSigningKey = key
                }, out var validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
