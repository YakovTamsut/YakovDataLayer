using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class UserDB : BaseDB
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
            command.CommandText = "SELECT * FROM tblUser";
            UserList list = new UserList(ExecuteCommand());
            return list;
        }
        public User SelectById(int id)
        {
            command.CommandText = "SELECT * FROM tblUser WHERE id=" + id;
            UserList list = new UserList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];
        }

        public User SelectByEmail(string email)
        {
            command.CommandText = $"SELECT * FROM tblUser WHERE Email='{email}'";
            UserList list = new UserList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];
        }

        protected override void LoadParameters(BaseEntity entity)
        {
            User user = entity as User;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@firstName", user.Firstname);
            command.Parameters.AddWithValue("@lastName", user.Lastname);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@gender", user.Gender);
            command.Parameters.AddWithValue("@isManager", user.IsManager);
            command.Parameters.AddWithValue("@birthday", user.BirthDay);
            command.Parameters.AddWithValue("@id", user.ID);
        }

        public int InsertUser(User user)
        {
            command.CommandText = "INSERT INTO TblUser (firstName,lastName,email,[password],gender,isManager,birthDay) VALUES (@firstName,@lastName,@email,@password,@gender,@isManager,@birthDay)";
            LoadParameters(user);
            return ExecuteCRUD();
        }
        public int UpdateUser(User user)
        {
            command.CommandText = "UPDATE TblUser SET firstName = @firstName,lastName = @lastName,email = @email,[password]=@password,gender = @gender,isManager = @isManager,birthDay = @birthDay WHERE ID = @ID";
            LoadParameters(user);
            return ExecuteCRUD();
        }
        public int DeleteUser(User user)
        {
            command.CommandText = $"DELETE FROM TblUser WHERE ID ={user.ID}";
            return ExecuteCRUD();
        }
        public User Login(User user)
        {
            command.CommandText = $"SELECT * FROM TblUser WHERE (Email = '{user.Email}') " +
                $"AND ([Password] = '{user.Password}')";
            UserList list = new UserList(base.ExecuteCommand());
            if (list.Count == 1)
                return list[0];
            return null;
        }

        
    }
}
