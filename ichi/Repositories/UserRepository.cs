using ichi.Models;

namespace ichi.Repositories
{
    internal class UserRepository
    {
        private readonly string _filePath = "C:\\Users\\student\\source\\repos\\Library\\ichi\\Data\\user.txt";

        public void SaveUser(User user)
        {
            File.AppendAllLines(_filePath, [user.ToString()]);
        }
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            var lines = File.ReadAllLines(_filePath);
            foreach (var item in lines)
            {
                var line = item.Split("|");
                User user = new User();
                user.Id = int.Parse(line[0]);
                user.Name = line[1];
                user.Email = line[2];
                user.Password = line[3];
                user.IsLogged = bool.Parse(line[4]);
                users.Add(user);
            }
            return users;
        }
    }
}
