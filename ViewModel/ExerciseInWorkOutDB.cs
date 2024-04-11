using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ExerciseInWorkOutDB : BaseDB
    {
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            ExerciseInWorkOut exinworkout = entity as ExerciseInWorkOut;
            exinworkout.ID=int.Parse(reader["id"].ToString());
            exinworkout.Reps = int.Parse(reader["reps"].ToString());
            exinworkout.Sets = int.Parse(reader["sets"].ToString());
            WorkoutDB workoutDB = new WorkoutDB();
            exinworkout.Workout = workoutDB.SelectById(int.Parse(reader["workoutID"].ToString()));

            ExerciseDB exerciseDB = new ExerciseDB();
            exinworkout.Exercise = exerciseDB.SelectById(int.Parse(reader["exercisesID"].ToString()));
            return exinworkout;
        }

        protected override void LoadParameters(BaseEntity entity)
        {
            ExerciseInWorkOut exinworkout = entity as ExerciseInWorkOut;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@workoutID", exinworkout.Workout.ID);
            command.Parameters.AddWithValue("@exercisesID", exinworkout.Exercise.ID);
            command.Parameters.AddWithValue("@reps", exinworkout.Reps);
            command.Parameters.AddWithValue("@sets", exinworkout.Sets);
            command.Parameters.AddWithValue("@id", exinworkout.ID);
        }

        protected override BaseEntity NewEntity()
        {
            return new ExerciseInWorkOut();
        }

        public ExerciseInWorkOutList SelectAll()
        {
            command.CommandText = "SELECT * FROM tblExInWorkout";
            ExerciseInWorkOutList list = new ExerciseInWorkOutList(ExecuteCommand());
            return list;
        }
        public ExerciseInWorkOut SelectById(int id)
        {
            command.CommandText = "SELECT * FROM tblExInWorkout WHERE id=" + id;
            ExerciseInWorkOutList list = new ExerciseInWorkOutList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];
        }
        public int InsertWorkout(ExerciseInWorkOut exinwo)
        {
            command.CommandText = "INSERT INTO tblExInWorkout (workoutID,exercisesID,reps,sets) VALUES (@workoutID,@exercisesID,@reps,@sets)";
            LoadParameters(exinwo);
            return ExecuteCRUD();
        }
        public int UpdateWorkout(ExerciseInWorkOut exinwo)
        {
            command.CommandText = "UPDATE tblExInWorkout SET workoutID = @workoutID,exercisesID = @exercisesID,reps = @reps,sets = @sets WHERE ID = @ID";
            LoadParameters(exinwo);
            return ExecuteCRUD();
        }
        public int DeleteWorkout(ExerciseInWorkOut exinwo)
        {
            command.CommandText = $"DELETE FROM tblExInWorkout WHERE ID = {exinwo.ID}";
            return ExecuteCRUD();
        }
        public int DeleteWorkout(Workout workout)
        {
            command.CommandText = $"DELETE FROM tblExInWorkout WHERE workoutID = {workout.ID}";
            return ExecuteCRUD();
        }

        public ExerciseInWorkOutList SelectByWorkout(Workout workout)
        {
            command.CommandText = $"SELECT * FROM tblExInWorkout WHERE workoutID={workout.ID}";
            ExerciseInWorkOutList list = new ExerciseInWorkOutList(ExecuteCommand());
            return list;
        }
    }
}
