using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementWebApp.Models
{
    public class ViewModel
    {
        public string TeacherName { get; set; }
        public int TeacherId { get; set; }
        public int DepartmentId { get; set; }
        public double CreditLimit { get; set; }
        public string CourseCode { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public double CourseCredit { get; set; }
        public string DepartmentName { get; set; }
        public string SemesterName { get; set; }
    }
}