using FlorMoon.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FlorMoon.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User? GetCurrentUser();
        User? GetUserById(int id);
        User? GetUserByEmail(string email);
        void CreateUser(User user);
        void UpdateUser(User user, IFormFile? profilePicture = null);
        void DeleteUser(int id);
        bool EmailExists(string email);
        User? Authenticate(string email, string password);
    }
}