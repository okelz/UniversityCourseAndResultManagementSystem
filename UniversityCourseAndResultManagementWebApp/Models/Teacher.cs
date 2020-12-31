using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementWebApp.Models
{
    public class Teacher
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string DesignationId { get; set; }
        public string DepartmentId { get; set; }
        public double CreditLimit { get; set; }
        public double RemainingCredit { get; set; }
    }
}