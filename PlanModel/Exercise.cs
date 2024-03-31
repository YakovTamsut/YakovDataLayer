using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class Exercise : BaseEntity
    {
        protected string exerciseName;
        protected int difficulty;
        protected bool isCompound;
        protected string targetMuscle;
        protected string type;
        protected string exerciseUrl;

        [DataMember]
        public string ExerciseName { get { return exerciseName; } set { exerciseName = value; } }
        [DataMember]
        public int Difficulty { get { return difficulty; } set { difficulty = value; } }
        [DataMember]
        public bool IsCompound { get { return isCompound; } set { isCompound = value; } }
        [DataMember]
        public string TargetMuscle { get { return targetMuscle; } set { targetMuscle = value; } }
        [DataMember]
        public string Type { get { return type; } set { type = value; } }
        [DataMember]
        public string ExerciseUrl { get { return exerciseUrl; } set { exerciseUrl = value; } }

    }

    [CollectionDataContract]
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
