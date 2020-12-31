using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityCourseAndResultManagementWebApp.Gateway
{
    public class Gateway
    {
        public SqlConnection Connection { get; set; }
        public SqlDataReader Reader { get; set; }
        public SqlCommand Command { get; set; }
        public string Query { get; set; }

        public string ConnectionString =
            WebConfigurationManager.ConnectionStrings["UniversityDBConnection"].ConnectionString;

        public Gateway()
        {
            Connection = new SqlConnection(ConnectionString);
        }
    }
}