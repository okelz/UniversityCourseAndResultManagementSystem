using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementWebApp.Models;

namespace UniversityCourseAndResultManagementWebApp.Gateway
{
    public class DepartmentGateway:Gateway
    {
        public int Save(Department aDepartment)
        {
            Query = "INSERT INTO Department Values (@Name,@Code)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@Code", aDepartment.Code);
            Command.Parameters.AddWithValue("@Name", aDepartment.Name);
            Connection.Open();
            int rowaffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowaffected;

        }

        public bool IsCodeExists(Department aDepartment)
        {
            Query = "Select * From Department where Code=@Code";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@Code", aDepartment.Code);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;

        }
        public bool IsNameExists(Department aDepartment)
        {
            Query = "Select * From Department where Name=@Name";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@Name", aDepartment.Name);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;

        }
    }
}