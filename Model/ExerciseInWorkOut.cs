using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ExerciseInWorkOut : BaseEntity
    {
        protected Exercise exercise;
        protected Workout workout;
        protected string repRange;

        public Exercise Exercise { get { return exercise; } set { exercise = value; } }
        public Workout Workout { get { return workout; } set { workout = value; } }
        public string RepRange { get { return repRange; } set { repRange = value; } }
    }
    public class ExerciseInWorkOutList : List<ExerciseInWorkOut>
    {
        //בנאי ברירת מחדל - אוסף ריק
        public ExerciseInWorkOutList() { }
        //המרה אוסף גנרי לרשימת משתמשים
        public ExerciseInWorkOutList(IEnumerable<ExerciseInWorkOut> list)
            : base(list) { }
        //המרה מטה מטיפוס בסיס לרשימת משתמשים
        public ExerciseInWorkOutList(IEnumerable<BaseEntity> list)
            : base(list.Cast<ExerciseInWorkOut>().ToList()) { }
    }
}
