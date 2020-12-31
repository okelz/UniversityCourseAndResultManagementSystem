using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using UniversityCourseAndResultManagementWebApp.Models;

namespace UniversityCourseAndResultManagementWebApp.Gateway
{
    public class StudentGateway:Gateway
    {
        public int Save(Student aStudent)
        {
            Query = "INSERT INTO Student VALUES(@Name,@Email,@ContactNo,@Date,@Address,@DepartmentId,@RegistrationNo)";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@Name", aStudent.Name);
            Command.Parameters.AddWithValue("@Email", aStudent.Email);
            Command.Parameters.AddWithValue("@ContactNo", aStudent.ContactNo);
            Command.Parameters.AddWithValue("@Date", aStudent.Date);
            Command.Parameters.AddWithValue("@Address", aStudent.Address);
            Command.Parameters.AddWithValue("@DepartmentId", aStudent.DepartmentId);
            Command.Parameters.AddWithValue("@RegistrationNo", aStudent.RegistrationNo);

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        public bool IsEmailExists(Student aStudent)
        {
            Query = "SELECT * FROM Student WHERE Email=@Email ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("Email", aStudent.Email);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExist = Reader.HasRows;
            Connection.Close();
            return isExist;
        }

        public string GetDeptCode(int departmentId)
        {
            Query = "SELECT Code FROM Department WHERE Id=@Id ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("Id", departmentId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            string code = Reader["Code"].ToString();
            Reader.Close();
            Connection.Close();
            return code;
        }

        public int GetNoOfStudents(string reistrationNo)
        {
            Query = "SELECT * FROM Student WHERE RegistrationNo LIKE @RegistrationNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("RegistrationNo", reistrationNo+"%");
            Connection.Open();
            Reader = Command.ExecuteReader();
            int number = 1;
            while (Reader.Read())
            {
                number++;
            }
            Reader.Close();
            Connection.Close();
            return number;
        }

        public string GetDeptName(int departmentId)
        {
            Query = "SELECT * FROM Department WHERE Id=@Id ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("Id", departmentId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            string name = Reader["Name"].ToString();
            Reader.Close();
            Connection.Close();
            return name; 
        }

        public List<Student> GetAllRegistrationNo()
        {
            Query = "Select * from Student";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Student> students = new List<Student>();
            while (Reader.Read())
            {
                Student aStudent = new Student()
                {
                    Id = (int)Reader["Id"],
                    RegistrationNo = Reader["RegistrationNo"].ToString()
                };
                students.Add(aStudent);
            }
            Reader.Close();
            Connection.Close();
            return students;
        }

        public List<Student> GetCoursesByRegistrationId(int id)
        {
            Query = "SELECT * FROM StudentViewModel WHERE StudentId=@StudentId ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("StudentId", id);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Student> students = new List<Student>();
            while (Reader.Read())
            {
                Student aStudent = new Student()
                {
                    CourseId = (int)Reader["CourseId"],
                    CourseName = Reader["CourseName"].ToString(),
                    CourseCode = Reader["CourseCode"].ToString()
                };
                students.Add(aStudent);
            }
            Reader.Close();
            Connection.Close();
            return students; 
        }

        public Student GetStudentInfo(int id)
        {
            Query = "Select * FROM StudentViewModel WHERE StudentId=@StudentId ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("StudentId", id);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Student aStudent = new Student();
            while (Reader.Read())
            {
                aStudent = new Student()
                {
                    Id = (int)Reader["StudentId"],
                    Name = Reader["StudentName"].ToString(),
                    Email = Reader["Email"].ToString(),
                    DepartmentName = Reader["DepartmentName"].ToString()
                };
              
            }
            Reader.Close();
            Connection.Close();
            return aStudent;
        }

        public int SaveResult(Student aStudent)
        {
            Query = "INSERT INTO Result VALUES(@RegistrationNo,@CourseId,@Grade)";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@RegistrationNo", aStudent.RegistrationNo);
            Command.Parameters.AddWithValue("@CourseId", aStudent.CourseId);
            Command.Parameters.AddWithValue("@Grade", aStudent.Grade);

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public string GetRegNo(int id)
        {
            Query = "SELECT * FROM Student WHERE Id=@Id ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("Id", id);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            string registrationNo = Reader["RegistrationNo"].ToString();
            Reader.Close();
            Connection.Close();
            return registrationNo; 
        }

        public int GetDeptIdByRegId(int id)
        {
            Query = "SELECT * FROM Student WHERE Id=@Id ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("Id", id);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            int departmentId = (int)Reader["DepartmentId"];
            Reader.Close();
            Connection.Close();
            return departmentId; 
        }

        public List<Student> GetCoursesByDeptId(int departmentId)
        {
            Query = "SELECT * FROM Course WHERE DepartmentId=@DepartmentId ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("DepartmentId", departmentId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Student> students = new List<Student>();
            while (Reader.Read())
            {
                Student aStudent = new Student()
                {
                    CourseId = (int)Reader["Id"],
                    CourseName = Reader["Name"].ToString(),
                    CourseCode = Reader["Code"].ToString()
                };
                students.Add(aStudent);
            }
            Reader.Close();
            Connection.Close();
            return students; 
        }

        public Student GetStudentInfoByRegNo(int id)
        {
            Query = "Select * FROM StudentWithDept WHERE StudentId=@StudentId ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("StudentId", id);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Student aStudent = new Student();
            while (Reader.Read())
            {
                aStudent = new Student()
                {
                    Id = (int)Reader["StudentId"],
                    Name = Reader["StudentName"].ToString(),
                    Email = Reader["Email"].ToString(),
                    DepartmentName = Reader["DepartmentName"].ToString(),
                    DepartmentId = (int)Reader["DepartmentId"]
                };

            }
            Reader.Close();
            Connection.Close();
            return aStudent;
        }

        public int EnrollCourse(Student aStudent)
        {
            Query = "INSERT INTO EnrollCourse VALUES(@RegistrationNo,@CourseId,@Date)";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@RegistrationNo", aStudent.RegistrationNo);
            Command.Parameters.AddWithValue("@CourseId", aStudent.CourseId);
            Command.Parameters.AddWithValue("@Date", aStudent.Date);

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool IsAssigned(Student aStudent)
        {
            Query = "SELECT * FROM EnrollCourse WHERE RegistrationNo=@RegistrationNo and CourseId=@CourseId ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("RegistrationNo", aStudent.RegistrationNo);
            Command.Parameters.AddWithValue("CourseId", aStudent.CourseId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExist = Reader.HasRows;
            Connection.Close();
            return isExist;
        }
        public bool IsResultExist(Student aStudent)
        {
            Query = "SELECT * FROM Result WHERE RegistrationNo=@RegistrationNo and CourseId=@CourseId ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("RegistrationNo", aStudent.RegistrationNo);
            Command.Parameters.AddWithValue("CourseId", aStudent.CourseId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExist = Reader.HasRows;
            Connection.Close();
            return isExist;
        }

        public int UpdateResult(Student aStudent)
        {
            Query = "Update Result set Grade=@Grade WHERE RegistrationNo=@RegistrationNo and CourseId=@CourseId ";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@RegistrationNo", aStudent.RegistrationNo);
            Command.Parameters.AddWithValue("@CourseId", aStudent.CourseId);
            Command.Parameters.AddWithValue("@Grade", aStudent.Grade);

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}