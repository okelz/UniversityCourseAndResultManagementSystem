using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementWebApp.Models;

namespace UniversityCourseAndResultManagementWebApp.Gateway
{
    public class TeacherGateway:Gateway
    {
        public List<Designation> GetAllDesignations()
        {
            Query = "Select * from Designation";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Designation> designations = new List<Designation>();
            while (Reader.Read())
            {
                Designation aDesignation = new Designation()
                {
                    Id = (int)Reader["Id"],
                    Name = Reader["Name"].ToString()

                };
                designations.Add(aDesignation);
            }
            Reader.Close();
            Connection.Close();
            return designations;
        }

        public bool IsEmailExists(Teacher aTeacher)
        {
            Query = "SELECT * FROM Teacher WHERE Email=@Email ";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("Email", aTeacher.Email);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool isExist = Reader.HasRows;
            Connection.Close();
            return isExist;
        }

        public int SaveTeacher(Teacher aTeacher)
        {
            Query = "INSERT INTO Teacher VALUES(@Name,@Address,@Email,@ContactNo,@DesignationId,@DepartmentId,@CreditLimit,@RemainingCredit)";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@Name", aTeacher.Name);
            Command.Parameters.AddWithValue("@Address", aTeacher.Address);
            Command.Parameters.AddWithValue("@Email", aTeacher.Email);
            Command.Parameters.AddWithValue("@ContactNo", aTeacher.ContactNo);
            Command.Parameters.AddWithValue("@DesignationId", aTeacher.DesignationId);
            Command.Parameters.AddWithValue("@DepartmentId", aTeacher.DepartmentId);
            Command.Parameters.AddWithValue("@CreditLimit", aTeacher.CreditLimit);
            Command.Parameters.AddWithValue("@RemainingCredit", aTeacher.CreditLimit);

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }


        public List<Teacher> GetTeachersByDepartmentId(int departmentId)
        {
            Query = "Select * from Teacher where DepartmentId=@DepartmentId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("DepartmentId", departmentId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Teacher> teachers = new List<Teacher>();
            while (Reader.Read())
            {
                Teacher teacher = new Teacher()
                {
                  Id = (int) Reader["Id"],
                  CreditLimit = Convert.ToDouble(Reader["CreditLimit"]),
                  Name = Reader["Name"].ToString()
                };

                
                teachers.Add(teacher);
            }
            Reader.Close();
            Connection.Close();
            return teachers;
        }

        public List<Course> GetCoursesByDepartmentId(int departmentId)
        {
            Query = "Select * from Course where DepartmentId=@DepartmentId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("DepartmentId", departmentId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Course> teachers = new List<Course>();
            while (Reader.Read())
            {
                Course teacher = new Course()
                {
                  Id = (int) Reader["Id"],
                  Code = Reader["Code"].ToString(),
                  Credit = Convert.ToDouble(Reader["Credit"]),
                  Name = Reader["Name"].ToString()
                };

                
                teachers.Add(teacher);
            }
            Reader.Close();
            Connection.Close();
            return teachers;
        }

        public Teacher GetTeacherInfo(int teacherId)
        {
            Query = "Select * from Teacher where Id=@Id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("Id", teacherId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            Teacher aTeacher = new Teacher()
               {
                    Id = (int)Reader["Id"],
                    CreditLimit = Convert.ToDouble(Reader["CreditLimit"]),
                    RemainingCredit = Convert.ToDouble(Reader["RemainingCredit"]),
                    Name = Reader["Name"].ToString()
                };
            Reader.Close();
            Connection.Close();
            return aTeacher;
        }
    }
    
}