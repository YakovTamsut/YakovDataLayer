using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    public class User : BaseEntity
    {

        protected string firstname;//שם פרטי
        protected string lastname;//שם משפחה
        protected string email;//מייל
        protected string password;//סיסמה
        protected DateTime birthday;//תאריך לידה
        protected bool gender;//מגדר
        protected bool isManager;//האם מנהל
        public string Firstname { get { return firstname; } set { firstname = value; } }
        public string Lastname { get { return lastname; } set { lastname = value; } }
        public DateTime BirthDay { get { return birthday; } set { value = birthday; } }
        public bool Gender { get { return gender; } set { gender = value; } }
        public bool IsManager { get { return isManager; } set { isManager = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Password { get { return password; } set { password = value; } }

    }
    public class UserList : List<User>
    {
        //בנאי ברירת מחדל - אוסף ריק
        public UserList() { }
        //המרה אוסף גנרי לרשימת משתמשים
        public UserList(IEnumerable<User> list)
            : base(list) { }
        //המרה מטה מטיפוס בסיס לרשימת משתמשים
        public UserList(IEnumerable<BaseEntity> list)
            : base(list.Cast<User>().ToList()) { }
    }
}
