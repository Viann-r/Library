using ichi.Models;
using ichi.Repositories;

namespace ichi.Services
{
    internal class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void RegisterUser(User user)
        {
            var users = _userRepository.GetAllUsers();
            int maxId = 0;
            if (users.Count != 0)
            {
                maxId = users.Max(u => u.Id);

            }
            user.Id = ++maxId;
            if (user == null)
            {
                Console.WriteLine("user cannot be null");
                return;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _userRepository.SaveUser(user);
        }




    }
}
