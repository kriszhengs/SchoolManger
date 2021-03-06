using System;
using Models;
using SchoolMangerDAL;
using System.IO;
namespace SchoolMangerBLL
{
	/// <summary>
	/// BaseBLL 的摘要说明。
	/// </summary>
	public class BaseBLL
	{		
		protected static User user;
		protected static AdminDAL admins;
		protected static StudentDAL students;
		protected static TeacherDAL teachers;
		protected static CourseDAL courses;
		protected static TermCourseDAL termCourses;

		protected BaseBLL(){}
		static BaseBLL()
		{
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }
			admins = DataFileAccess.GetAdmins();
            Admin.currid = admins.id;
			students = DataFileAccess.GetStudents();
            Student.cuurid = students.id;
			teachers = DataFileAccess.GetTeachers();
            Teacher.cuurid = teachers.id;
			courses = DataFileAccess.GetCourses();
			termCourses = DataFileAccess.GetTermCourses();
		}

		public static User User
		{
			get{return user;}
		}

		public static TermCourse RetrieveTermCourse(string termCourseId)
		{
			return termCourses.RetrieveTermCourse(termCourseId);
		}
        public static bool ChangePasswd(string old,string newpw, string compw, out string err)
        {
            err = string.Empty;
            if (!user.CheckPasswd(old))
            {
                err = "原始密码错误";
                return false;
            }
            if (newpw != compw)
            {
                err = "两次输入的密码不一致";
                return false;
            }
            user.Password = newpw;
            return true;
        }
        public static void saveAll()
        {
            admins.id = Admin.currid;
            students.id = Student.cuurid;
            teachers.id = Teacher.cuurid;
            DataFileAccess.SaveAdmins(admins);
            DataFileAccess.SaveCourses(courses);
            DataFileAccess.SaveStudents(students);
            DataFileAccess.SaveTeachers(teachers);
            DataFileAccess.SaveTermCourses(termCourses);
        }
    }
}
