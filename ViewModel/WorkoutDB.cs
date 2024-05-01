using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class WorkoutDB:BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new Workout() as BaseEntity;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Workout workout = entity as Workout;
            workout.ID = int.Parse(reader["id"].ToString());
            workout.Type = reader["type"].ToString();
            workout.Duration = int.Parse(reader["duration"].ToString());

            int userId = int.Parse(reader["userID"].ToString());
            UserDB userDB = new UserDB();
            workout.User = userDB.SelectById(userId);

            return workout;
        }
        public WorkoutList SelectAll()
        {
            command.CommandText = "SELECT * FROM tblWorkouts";
            WorkoutList list = new WorkoutList(ExecuteCommand());
            return list;
        }
        public Workout SelectById(int id)
        {
            command.CommandText = "SELECT * FROM tblWorkouts WHERE id=" + id;
            WorkoutList list = new WorkoutList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];
        }
        protected override void LoadParameters(BaseEntity entity)
        {
            Workout workout = entity as Workout;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@type", workout.Type);
            command.Parameters.AddWithValue("@duration", workout.Duration);
            command.Parameters.AddWithValue("@userID", workout.User.ID);
            command.Parameters.AddWithValue("@id", workout.ID);
        }
        public int InsertWorkout(Workout workout)
        {
            command.CommandText = "INSERT INTO tblWorkouts (type,duration,userID) VALUES (@type,@duration,@userID)";
            LoadParameters(workout);
            return ExecuteCRUD();
        }
        public int UpdateWorkout(Workout workout)
        {
            command.CommandText = "UPDATE tblWorkouts SET type = @type,duration = @duration,userID=@userID WHERE ID = @ID";
            LoadParameters(workout);
            return ExecuteCRUD();
        }
        public int DeleteWorkout(Workout workout)
        {
            command.CommandText = $"DELETE FROM tblWorkouts WHERE ID = {workout.ID}";
            return ExecuteCRUD();
        }
        public int DeleteWorkoutByUser(User user)
        {
            command.CommandText = $"DELETE FROM tblWorkouts WHERE ID = {user.ID}";
            return ExecuteCRUD();
        }
    }
}
