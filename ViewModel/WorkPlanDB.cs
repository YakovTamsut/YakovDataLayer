using Model;
using PlanModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ViewModel
{
    public class WorkoutPlanDB : BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new WorkoutPlan() as BaseEntity;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            WorkoutPlan userWorkPlan = entity as WorkoutPlan;
            userWorkPlan.ID = int.Parse(reader["id"].ToString());
            userWorkPlan.Day =int.Parse(reader["day"].ToString());

            int userId = int.Parse(reader["userID"].ToString());
            UserDB userDB = new UserDB();
            userWorkPlan.User = userDB.SelectById(userId);

            int workoutID = int.Parse(reader["WorkoutID"].ToString());
            WorkoutDB workoutDB = new WorkoutDB();
            userWorkPlan.Workout = workoutDB.SelectById(workoutID);

            return userWorkPlan;
        }

        protected override void LoadParameters(BaseEntity entity)
        {
            WorkoutPlan userWorkPlan = entity as WorkoutPlan;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@userID", userWorkPlan.User.ID);
            command.Parameters.AddWithValue("@WorkoutID", userWorkPlan.Workout.ID);
            command.Parameters.AddWithValue("@day", userWorkPlan.Day);
            command.Parameters.AddWithValue("@id", userWorkPlan.ID);
        }

        public int Insert(WorkoutPlan userWorkPlan)
        {
            command.CommandText = "INSERT INTO tblUserWorkPlan " +
                "(userID, WorkoutID,[day]) " +
                "VALUES (@userID, @WorkoutID, @day)";
            LoadParameters(userWorkPlan);
            return ExecuteCRUD();
        }
        public int Update(WorkoutPlan userWorkPlan)
        {
            command.CommandText = "UPDATE tblUserWorkPlan SET userID = @userID, WorkoutID = @WorkoutID, [day] = @day " +
                "WHERE (id = @id)";
            LoadParameters(userWorkPlan);
            return ExecuteCRUD();
        }
        public int Delete(WorkoutPlan userWorkPlan)
        {
            command.CommandText = "DELETE FROM tblUserWorkPlan WHERE userID = @userID AND WorkoutID = @WorkoutID, [day] = @day";
            LoadParameters(userWorkPlan);
            return ExecuteCRUD();
        }

        public WorkoutPlanList GetUserWorkoutPlans(User user)
        {
            command.CommandText = $"SELECT * FROM tblUserWorkPlan WHERE userID = {user.ID}";
            WorkoutPlanList workouts = new WorkoutPlanList(ExecuteCommand());
            return workouts;
        }

        public WorkoutPlan GetWorkoutPlanByDayAndUser(User user)
        {
            command.CommandText = $"SELECT * FROM tblUserWorkPlan WHERE userID = {user.ID} AND day = {(int)DateTime.Today.DayOfWeek+1}";
            WorkoutPlanList wp = new WorkoutPlanList(ExecuteCommand());
            if (wp.Count == 0) return null;
            return wp[0];
        }

        public int DeleteWorkoutPlanByWorkout(Workout workout)
        {
            command.CommandText = $"DELETE * FROM tblUserWorkPlan WHERE WorkoutID = {workout.ID}";
            return ExecuteCRUD();
        }

    }
}
