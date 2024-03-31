using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Exercise : BaseEntity
    {
        protected string exerciseName;
        protected int difficulty;
        protected bool isCompound;
        protected int repRange;

        public string ExerciseName { get { return exerciseName; } set { exerciseName = value; } }
        public int Difficulty { get { return difficulty; } set { value = difficulty; } }
        public bool IsCompound { get { return isCompound; } set { isCompound = value; } }
        public int RepRange { get { return repRange; } set { repRange = value; } }

    }

    public class ExerciseList : List<Exercise>
    {
        //בנאי ברירת מחדל - אוסף ריק
        public ExerciseList() { }
        //המרה אוסף גנרי לרשימת משתמשים
        public ExerciseList(IEnumerable<Exercise> list)
            : base(list) { }
        //המרה מטה מטיפוס בסיס לרשימת משתמשים
        public ExerciseList(IEnumerable<BaseEntity> list)
            : base(list.Cast<Exercise>().ToList()) { }
    }

}
