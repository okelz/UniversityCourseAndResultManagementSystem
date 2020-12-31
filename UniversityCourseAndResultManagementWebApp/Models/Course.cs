using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementWebApp.Models
{
    public class Course
    {
        public string Code { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Credit { get; set; }
        public double CreditLimit { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int CourseId { get; set; }
        public string DepartmentName { get; set; }
        public int SemesterId { get; set; }
        public double RemainingCredit { get; set; }
        public string SemesterName { get; set; }
    }
}