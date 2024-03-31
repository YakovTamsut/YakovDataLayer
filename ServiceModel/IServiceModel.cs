using Model;
using PlanModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel
{
    [ServiceContract]
    public interface IServiceModel
    {
        #region Exercise
        [OperationContract] int InsertExercises(Exercise exercise);
        [OperationContract] int UpdateExercises(Exercise exercise);
        [OperationContract] int DeleteExercises(Exercise exercise);
        [OperationContract] ExerciseList SelectAllExercises();
        #endregion

        #region Exercise In WorkOut
        [OperationContract] int InsertExInWorkout(ExerciseInWorkOut exinw);
        [OperationContract] int UpdateExInWorkout(ExerciseInWorkOut exinw);
        [OperationContract] int DeleteExInWorkout(ExerciseInWorkOut exinw);
        [OperationContract] ExerciseInWorkOutList SelectAllExercisesInWorkout();
        [OperationContract] ExerciseInWorkOutList SelectExInByWorkOut(Workout workout);
        #endregion

        #region Workout
        [OperationContract] int InsertWorkout(Workout workout);
        [OperationContract] int UpdateWorkout(Workout workout);
        [OperationContract] int DeleteWorkout(Workout workout);
        [OperationContract] WorkoutList SelectAllWorkouts();
        [OperationContract] Workout SelectWorkoutById(int id);
        #endregion

        #region User
        [OperationContract] User Login(User user);
        [OperationContract] int NewUser(User user);
        [OperationContract] bool IsEmailFree(string email);
        [OperationContract] int UpdateUser(User user);
        [OperationContract] int DeleteUser(User user);
        [OperationContract] UserList SelectAllUsers();
        #endregion

        #region Workout plan
        [OperationContract] WorkoutPlanList GetUserWorkoutPlans(User user);
        [OperationContract] int InsertWorkoutPlan(WorkoutPlan plan);
        [OperationContract] int UpdateWorkoutPlan(WorkoutPlan plan);
        [OperationContract] int DeleteWorkoutPlan(WorkoutPlan plan);
        [OperationContract] WorkoutPlan GetTodayWorkoutPlan(User user);
        #endregion
    }
}