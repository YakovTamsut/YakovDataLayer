using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class ExerciseInWorkOut : BaseEntity
    {
        protected Exercise exercise;
        protected Workout workout;
        protected int reps;
        protected int sets;

        [DataMember]
        public Exercise Exercise { get { return exercise; } set { exercise = value; } }
        [DataMember]
        public Workout Workout { get { return workout; } set { workout = value; } }
        [DataMember]
        public int Reps { get { return reps; } set { reps = value; } }
        [DataMember]
        public int Sets { get { return sets; } set { sets = value; } }
    }

    [CollectionDataContract]
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
