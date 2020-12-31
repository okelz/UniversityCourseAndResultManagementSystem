using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementWebApp.Models;

namespace UniversityCourseAndResultManagementWebApp.Gateway
{
    public class CourseGateway:Gateway
    {
        public List<Department> GetAllDepartments()
        {
            Query = "Select * from Department";
            Command = new SqlCommand(Query,Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();    
            List<Department> departments = new List<Department>();
            while (Reader.Read())
            {
                Department aDepartment = new Department()
                {
                    Id = (int)Reader["Id"],
                    Code = Reader["Code"].ToString(),
                    Name = Reader["Name"].ToString()
                    
                };
                departments.Add(aDepartment);     
            }
            Reader.Close();
            Connection.Close();
            return departments;
        }

        public List<Semester> GetAllSemesters()
        {
            Query = "Select * from Semester";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Semester> semesters = new List<Semester>();
            while (Reader.Read())
            {
                Semester aSemester = new Semester()
                {
                    Id = (int)Reader["Id"],
                    Name = Reader["Name"].ToString()

                };
                semesters.Add(aSemester);
            }
            Reader.Close();
            Connection.Close();
            return semesters;
        }
        public bool IsCodeExists(Course aCourse)
        {
            Query = "SELECT * FROM Course WHERE Code=@Code ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("Code", aCourse.Code);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExist = Reader.HasRows;
            Connection.Close();
            return isExist;
        }
        public bool IsNameExists(Course aCourse)
        {
            Query = "SELECT * FROM Course WHERE Name=@name ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("name", aCourse.Name);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExist = Reader.HasRows;
            Connection.Close();
            return isExist;
        }

        public int SaveCourse(Course aCourse)
        {
            Query = "INSERT INTO Course VALUES(@Code,@Name,@Credit,@Description,@DepartmentId,@SemesterId)";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@Code", aCourse.Code);
            Command.Parameters.AddWithValue("@Name", aCourse.Name);
            Command.Parameters.AddWithValue("@Credit", aCourse.Credit);
            Command.Parameters.AddWithValue("@Description", aCourse.Description);
            Command.Parameters.AddWithValue("@DepartmentId", aCourse.DepartmentId);
            Command.Parameters.AddWithValue("@SemesterId", aCourse.SemesterId);

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        public bool IsCourseAssigned(Course aCourse)
        {
            Query = "SELECT * FROM CourseAssign WHERE CourseId=@CourseId ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("CourseId", aCourse.CourseId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExist = Reader.HasRows;
            Connection.Close();
            return isExist;
        }

        public int SaveCourseAssign(Course aCourse)
        {
            Query = "INSERT INTO CourseAssign VALUES(@DepartmentId,@TeacherId,@CourseId)";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@DepartmentId", aCourse.DepartmentId);
            Command.Parameters.AddWithValue("@TeacherId", aCourse.TeacherId);
            Command.Parameters.AddWithValue("@CourseId", aCourse.CourseId);

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public void UpdateRemainingCredit(Course aCourse)
        {
            Query = "Update Teacher set RemainingCredit=@RemainingCredit Where Id=@TeacherId";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@RemainingCredit", aCourse.RemainingCredit);
            Command.Parameters.AddWithValue("@TeacherId", aCourse.TeacherId);
            
            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();
          
        }

        public Course GetCourseInfo(int courseId)
        {
            Query = "Select * from Course where Id=@Id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("Id", courseId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            Course aCourse = new Course()
            {
                CourseId = (int)Reader["Id"],
                Credit = Convert.ToDouble(Reader["Credit"]),
                Code = Reader["Code"].ToString(),
                Name = Reader["Name"].ToString()
            };
            Reader.Close();
            Connection.Close();
            return aCourse;
        }

        public List<ViewModel> GetCourseDetailes(int departmentId)
        {
            Query = "Select * from ViewModel where DepartmentId=@DepartmentId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("DepartmentId", departmentId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<ViewModel> viewModels = new List<ViewModel>();
            while (Reader.Read())
            {
                ViewModel aViewModel = new ViewModel();
                aViewModel.SemesterName = Reader["SemesterName"].ToString();
                aViewModel.DepartmentId =(int) Reader["DepartmentId"];
                aViewModel.TeacherName = Reader["TeacherName"].ToString();
                aViewModel.CourseName = Reader["CourseName"].ToString();
                aViewModel.CourseCode = Reader["CourseCode"].ToString();
                if (aViewModel.TeacherName == "")
                {
                    aViewModel.TeacherName = "Not Assigned Yet";
                }
                viewModels.Add(aViewModel);
            }
            Reader.Close();
            Connection.Close();
            return viewModels;
        }

        public Course GetTeacherInfo(int teacherId)
        {
            Query = "Select * from Teacher where Id=@Id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("Id", teacherId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            Course aCourse = new Course()
            {
                TeacherId = (int)Reader["Id"],
                CreditLimit = Convert.ToDouble(Reader["CreditLimit"]),
                RemainingCredit = Convert.ToDouble(Reader["RemainingCredit"]),
                TeacherName = Reader["Name"].ToString()
            };
            Reader.Close();
            Connection.Close();
            return aCourse;
        }

        public int UnassignCourses()
        {
            Query = "Update CourseAssign set CourseId=0 ";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<Teacher> GetTeacherCredit()
        {
            Query = "Select * from Teacher";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Teacher> teachers = new List<Teacher>();
            while (Reader.Read())
            {
                Teacher aTeacher = new Teacher();
              
                aTeacher.Id = (int)Reader["Id"];
                aTeacher.CreditLimit = Convert.ToDouble(Reader["CreditLimit"]);
                aTeacher.RemainingCredit = Convert.ToDouble(Reader["RemainingCredit"]);

                teachers.Add(aTeacher);
            }
            Reader.Close();
            Connection.Close();
            return teachers;
        }

        public void UpdateRemainingCr(Teacher teacher)
        {
            Query = "Update Teacher set RemainingCredit=@CrediteLimit Where Id=@Id";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@CrediteLimit", teacher.CreditLimit);
            Command.Parameters.AddWithValue("@Id", teacher.Id);

            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();
        }
    }
}