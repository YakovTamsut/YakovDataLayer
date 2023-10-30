using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    internal class UserDB : BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new User();
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            User user = entity as User;
            user.ID = int.Parse(reader["id"].ToString());
            user.Firstname = reader["firstName"].ToString();
            user.Lastname = reader["lastName"].ToString();
            user.Email = reader["email"].ToString();
            user.Password = reader["password"].ToString();
            user.Gender = bool.Parse(reader["gender"].ToString());
            user.IsManager = bool.Parse(reader["isManager"].ToString());
            user.BirthDay = DateTime.Parse(reader["birthday"].ToString());
            return user;
        }
        public UserList SelectAll()
        {
            command.CommandText = "SELECT * FROM tbUser";
            UserList list = new UserList(ExecuteCommand());
            return list;
        }
        public User SelectById(int id)
        {
            command.CommandText = "SELECT * FROM tbUser WHERE id=" + id;
            UserList list = new UserList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];


        }
    }
}
