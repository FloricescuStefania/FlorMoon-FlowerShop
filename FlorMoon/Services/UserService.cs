using FlorMoon.Models;
using FlorMoon.Services.Interfaces;
using FlorMoon.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FlorMoon.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _uploadsFolder;

        public UserService(IRepositoryWrapper repoWrapper, IHttpContextAccessor httpContextAccessor, string uploadsFolder)
        {
            _repoWrapper = repoWrapper;
            _httpContextAccessor = httpContextAccessor;
            _uploadsFolder = uploadsFolder;
        }

        public User? GetCurrentUser()
        {
            var loggedInEmail = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            if (string.IsNullOrEmpty(loggedInEmail))
            {
                return null;
            }

            var user = _repoWrapper.UserRepository
                .FindByCondition(u => u.Email == loggedInEmail)
                .FirstOrDefault();

            if (user == null)
            {
                user = new User
                {
                    Email = loggedInEmail,
                    FirstName = "New",
                    LastName = "User",
                    Password = "ManagedByIdentity",
                    Address = "",
                    PhoneNumber = "",
                    ProfilePicturePath = "/images/default-avatar.png"
                };

                _repoWrapper.UserRepository.Create(user);
                _repoWrapper.Save();

                user = _repoWrapper.UserRepository
                    .FindByCondition(u => u.Email == loggedInEmail)
                    .FirstOrDefault();
            }

            return user;
        }

        public void UpdateUser(User user, IFormFile? profilePicture = null)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (profilePicture != null && profilePicture.Length > 0)
            {
                if (!Directory.Exists(_uploadsFolder))
                {
                    Directory.CreateDirectory(_uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(profilePicture.FileName);
                string filePath = Path.Combine(_uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    profilePicture.CopyTo(fileStream);
                }

                user.ProfilePicturePath = "/images/profiles/" + uniqueFileName;
            }

            _repoWrapper.UserRepository.Update(user);
            _repoWrapper.Save();
        }

        public User? GetUserByEmail(string email) =>
            _repoWrapper.UserRepository.FindByCondition(u => u.Email == email).FirstOrDefault();

        public User GetUserById(int id) =>
            _repoWrapper.UserRepository.FindByCondition(u => u.UserID == id).FirstOrDefault()!;

        public IEnumerable<User> GetAllUsers() => _repoWrapper.UserRepository.FindAll();
        public void CreateUser(User user) { _repoWrapper.UserRepository.Create(user); _repoWrapper.Save(); }
        public void DeleteUser(int id) { }
        public bool EmailExists(string email) => GetUserByEmail(email) != null;
        public User? Authenticate(string email, string password) => null;
    }
}