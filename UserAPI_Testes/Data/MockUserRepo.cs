using System;
using System.Collections.Generic;
using UserAPI.Models;

namespace UserAPI.Data
{
    public class MockUserRepo : IUserRepo
    {
        public List<User> users;

        public MockUserRepo()
        {
            users = new List<User>
            {
                new User{Id=0, firstName="Teste1", surname="Teste1", age=20,creationDate=DateTime.Now},
                new User{Id=1, firstName="Teste2", surname="Teste2", age=20,creationDate=DateTime.Now},
                new User{Id=2, firstName="Teste3", surname="Teste3", age=20,creationDate=DateTime.Now}
            };
        }
        
        public void CreateUser(User user)
        {
            int indice = 0;
            users.ForEach(user => {
                if(user.Id > indice){
                    indice = user.Id;
                }
            });
            indice++;
            user.Id = indice;
            users.Add(user);
        }

        public void DeleteUser(User user)
        {
            int indice = 0;
            foreach(User u in users){
                if(u.Id == user.Id){
                    break;
                }
                indice++;
            } 
            users.RemoveAt(indice);
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public User GetUserById(int id)
        {
            User userP = new User();
            userP.Id = -1;
            users.ForEach(user => {
                if(user.Id == id){
                    userP.Id = user.Id;
                    userP.firstName = user.firstName;
                    userP.surname = user.surname;
                    userP.creationDate = user.creationDate;
                }
            });
            return userP;
        }

        public bool SaveChanges()
        {
            return true;
        }

        public void UpdateUser(User userUp)
        {
            users.Find(user => user.Id == userUp.Id).firstName = userUp.firstName;
            users.Find(user => user.Id == userUp.Id).surname = userUp.surname;
            users.Find(user => user.Id == userUp.Id).age = userUp.age;
        }
    }
}