using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PlanModel
{
    [DataContract]
    public class WorkoutPlan:BaseEntity
    {
        protected User user;//משתמש
        protected Workout workout;//אימון
        protected int day;//יום בשבוע

        [DataMember]
        public User User { get { return user; } set { user = value; } }
        [DataMember]
        public Workout Workout { get { return workout; } set { workout = value; } }
        [DataMember]
        public int Day
        {
            get { return day; }
            set { day = value; }
        }
    }
    [CollectionDataContract]
    public class WorkoutPlanList : List<WorkoutPlan>
    {
        //בנאי ברירת מחדל - אוסף ריק
        public WorkoutPlanList() { }
        //המרה אוסף גנרי לרשימת משתמשים
        public WorkoutPlanList(IEnumerable<WorkoutPlan> list)
            : base(list) { }
        //המרה מטה מטיפוס בסיס לרשימת משתמשים
        public WorkoutPlanList(IEnumerable<BaseEntity> list)
            : base(list.Cast<WorkoutPlan>().ToList()) { }
    }
}
