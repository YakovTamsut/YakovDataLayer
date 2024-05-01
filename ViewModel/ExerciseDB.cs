using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ViewModel
{
    public class ExerciseDB:BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new Exercise() as BaseEntity;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Exercise exercise = entity as Exercise;
            exercise.ID = int.Parse(reader["id"].ToString());
            exercise.ExerciseName = reader["exerciseName"].ToString();
            exercise.Difficulty = int.Parse(reader["difficulty"].ToString());
            exercise.IsCompound = bool.Parse(reader["isCompound"].ToString());
            exercise.TargetMuscle = reader["targetMuscle"].ToString();
            exercise.Type = reader["type"].ToString();
            exercise.ExerciseUrl = reader["exerciseUrl"].ToString();

            return exercise;
        }
        public ExerciseList SelectAll()
        {
            command.CommandText = "SELECT * FROM tblExercises";
            ExerciseList list = new ExerciseList(ExecuteCommand());
            return list;
        }
        public Exercise SelectById(int id)
        {
            command.CommandText = "SELECT * FROM tblExercises WHERE id=" + id;
            ExerciseList list = new ExerciseList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];
        }       
        protected override void LoadParameters(BaseEntity entity)
        {
            Exercise exercise = entity as Exercise;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@exerciseName", exercise.ExerciseName);
            command.Parameters.AddWithValue("@difficulty", exercise.Difficulty);
            command.Parameters.AddWithValue("@isCompound", exercise.IsCompound);
            command.Parameters.AddWithValue("@targetMuscle", exercise.TargetMuscle);
            command.Parameters.AddWithValue("@type", exercise.Type);
            command.Parameters.AddWithValue("@exerciseUrl", exercise.ExerciseUrl);
            command.Parameters.AddWithValue("@id", exercise.ID);
        }
        public int InsertExercises(Exercise exercise)
        {
            command.CommandText = "INSERT INTO tblExercises (exerciseName,difficulty,isCompound,targetMuscle,type,exerciseUrl) VALUES (@exerciseName,@difficulty,@isCompound,@targetMuscle,@type,@exerciseUrl)";
            LoadParameters(exercise);
            return ExecuteCRUD();
        }
        public int UpdateExercises(Exercise exercise)
        {
            command.CommandText = "UPDATE tblExercises SET exerciseName = @exerciseName,difficulty = @difficulty,isCompound = @isCompound, targetMuscle = @targetMuscle, type = @type, exerciseUrl = @exerciseUrl WHERE ID = @ID";
            LoadParameters(exercise);
            return ExecuteCRUD();
        }
        public int DeleteExercises(Exercise exercise)
        {
            command.CommandText = $"DELETE FROM tblExercises WHERE ID ={exercise.ID}";
            return ExecuteCRUD();
        }
        public bool IsExist(string name)
        {
            command.CommandText = $"SELECT * FROM tblExercises WHERE exerciseName LIKE '{name}'";
            ExerciseList list = new ExerciseList(ExecuteCommand());
            if (list.Count == 0)
                return false;
            return true;
        }

    }
}
