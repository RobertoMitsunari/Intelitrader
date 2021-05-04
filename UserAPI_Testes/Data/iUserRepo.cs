using System.Collections.Generic;
using UserAPI.Models;

namespace UserAPI.Data
{
    public interface IUserRepo{
        bool SaveChanges();
        List<User> GetAllUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}