using Model;
using PlanModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace ServiceModel
{
    public class BaseServiceModel : IServiceModel
    {
        #region User
        public int DeleteUser(User user)
        {
            UserDB db = new UserDB();
            return db.DeleteUser(user);
        }
        public User Login(User user)
        {
            UserDB db = new UserDB();
            User us = db.Login(user);
            return us;
        }
        public bool IsEmailFree(string email)
        {
            UserDB db = new UserDB();
            if (db.SelectByEmail(email) == null)
            {
                return true;
            }
            return false;
        }
        public int UpdateUser(User user)
        {
            UserDB db = new UserDB();
            return db.UpdateUser(user);
        }
        public int NewUser(User user)
        {
            UserDB db = new UserDB();
            return db.InsertUser(user);
        }

        public UserList SelectAllUsers()
        {
            UserDB db = new UserDB();
            return db.SelectAll();
        }

        public UserList GetAllPlanAdmins()
        {
            UserDB db = new UserDB();
            UserList list = db.SelectAll();
            list.RemoveAll(x => !x.IsManager);
            list.RemoveAll(x => x.Email.Contains('@'));
            return list;
        }
        #endregion
        #region Exercises
        public int DeleteExercises(Exercise exercise)
        {
            ExerciseDB db = new ExerciseDB();
            return db.DeleteExercises(exercise);
        }
        public int InsertExercises(Exercise exercise)
        {
            ExerciseDB db = new ExerciseDB();
            return db.InsertExercises(exercise);
        }
        public int UpdateExercises(Exercise exercise)
        {
            ExerciseDB db = new ExerciseDB();
            return db.UpdateExercises(exercise);
        }

        public ExerciseList SelectAllExercises()
        {
            ExerciseDB db = new ExerciseDB();
            return db.SelectAll();
        }
        #endregion
        #region Workout
        public int DeleteWorkout(Workout workout)
        {
            foreach (ExerciseInWorkOut ex in SelectExInByWorkOut(workout))
            {
                DeleteExInWorkout(ex);
            }
            WorkoutPlanDB wpdb = new WorkoutPlanDB();
            wpdb.DeleteWorkoutPlanByWorkout(workout);
            WorkoutDB db = new WorkoutDB();
            return db.DeleteWorkout(workout);
        }
        public int InsertWorkout(Workout workout)
        {
            WorkoutDB db = new WorkoutDB();
            db.InsertWorkout(workout);
            workout.ID= SelectAllWorkouts().Last().ID;  
            foreach(ExerciseInWorkOut eiw in workout.ExInWorkout)
            {
                eiw.Workout=new Workout { ID = workout.ID };
                InsertExInWorkout(eiw);
            }
            return workout.ID;
        }
        public int UpdateWorkout(Workout workout)
        {
            DeleteAllExInWorkout(workout);
            foreach (ExerciseInWorkOut eiw in workout.ExInWorkout)
            {
                eiw.Workout = new Workout { ID = workout.ID };
                InsertExInWorkout(eiw);
            }
            WorkoutDB db = new WorkoutDB();
            return db.UpdateWorkout(workout);
        }
        public WorkoutList SelectAllWorkouts()
        {
            WorkoutDB db = new WorkoutDB();
            return db.SelectAll();
        }
        #endregion
        #region Exercise In WorkOut
        public ExerciseInWorkOutList SelectAllExercisesInWorkout()
        {
            ExerciseInWorkOutDB db = new ExerciseInWorkOutDB();
            return db.SelectAll();
        }
       
        public int InsertExInWorkout(ExerciseInWorkOut exinw)
        {
            ExerciseInWorkOutDB db = new ExerciseInWorkOutDB();
            return db.InsertWorkout(exinw);
        }

        public int UpdateExInWorkout(ExerciseInWorkOut exinw)
        {
            ExerciseInWorkOutDB db = new ExerciseInWorkOutDB();
            return db.UpdateWorkout(exinw);
        }

        public int DeleteExInWorkout(ExerciseInWorkOut exinw)
        {
            ExerciseInWorkOutDB db = new ExerciseInWorkOutDB();
            return db.DeleteWorkout(exinw);
        }
        public int DeleteAllExInWorkout(Workout workout)
        {
            ExerciseInWorkOutDB db = new ExerciseInWorkOutDB();
            return db.DeleteWorkout(workout);
        }
        public ExerciseInWorkOutList SelectExInByWorkOut(Workout workout)
        {
            ExerciseInWorkOutDB db = new ExerciseInWorkOutDB();
            return db.SelectByWorkout(workout);
        }
        #endregion
        #region WorkOut Plans
        public WorkoutPlanList GetUserWorkoutPlans(User user)
        {
            WorkoutPlanDB workoutPlanDB = new WorkoutPlanDB();
            ExerciseInWorkOutDB exerciseInWorkOutDB = new ExerciseInWorkOutDB();

            WorkoutPlanList list = workoutPlanDB.GetUserWorkoutPlans(user);

            foreach (WorkoutPlan plan in list)
            {
                //load exercise for workout
                plan.Workout.ExInWorkout = exerciseInWorkOutDB.SelectByWorkout(plan.Workout);
            }
            return new WorkoutPlanList(list.OrderBy(w => w.Day).ToList());
        }

        public int InsertWorkoutPlan(WorkoutPlan plan)
        {
            WorkoutPlanDB db = new WorkoutPlanDB();
            return db.Insert(plan);
        }

        public int UpdateWorkoutPlan(WorkoutPlan plan)
        {
            WorkoutPlanDB db = new WorkoutPlanDB();
            return db.Update(plan);
        }

        public int DeleteWorkoutPlan(WorkoutPlan plan)
        {
            WorkoutPlanDB db = new WorkoutPlanDB();
            return db.Delete(plan);
        }

        public int DeleteAllWorkoutPlan(Workout workout)
        {
            ExerciseInWorkOutDB db = new ExerciseInWorkOutDB();
            return db.DeleteWorkout(workout);
        }

        public Workout SelectWorkoutById(int id)
        {
            WorkoutDB db = new WorkoutDB();
            return db.SelectById(id);
        }

        public WorkoutPlan GetTodayWorkoutPlan(User user)
        {
            WorkoutPlanDB db = new WorkoutPlanDB();
            WorkoutPlan wp = db.GetWorkoutPlanByDayAndUser(user);
            return wp;
        }

        #endregion
    }
}
