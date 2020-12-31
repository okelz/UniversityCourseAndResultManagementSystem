using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementWebApp.Gateway;
using UniversityCourseAndResultManagementWebApp.Models;

namespace UniversityCourseAndResultManagementWebApp.Menager
{
    public class CourseManager
    {
        CourseGateway aCourseGateway = new CourseGateway();
        public List<Department> GetAllDepartments()
        {
            return aCourseGateway.GetAllDepartments();
        }

        public List<Semester> GetAllSemesters()
        {
            return aCourseGateway.GetAllSemesters();
        }

        public string SaveCourse(Course aCourse)
        {
            if (!aCourseGateway.IsCodeExists(aCourse))
            {
                if (!aCourseGateway.IsNameExists(aCourse))
                {
                    int RowAffected = aCourseGateway.SaveCourse(aCourse);
                    if (RowAffected > 0)
                    {
                        return "Course Saved Successfully !!";
                    }
                    return "Saving Failed!!";
                }
                 return "Name already Exist!!";  
            }
            return "Code already Exist!!";
        }

        public string SaveCourseAssign(Course aCourse)
        {
            if (!aCourseGateway.IsCourseAssigned(aCourse))
            {
                int rowAffected = aCourseGateway.SaveCourseAssign(aCourse);
                if (rowAffected > 0)
                {
                    return "Course assigned Successfully !!";
                }
                return "Assigning Failed!!";
            }
            return "Course already Assigned!!";  
            
        }

        public void UpdateRemainingCredit(Course aCourse)
        {
            aCourseGateway.UpdateRemainingCredit(aCourse);
        }

        public Course GetCourseInfo(int courseId)
        {
            return aCourseGateway.GetCourseInfo( courseId);
        }



        public List<ViewModel> GetCourseDetailes(int departmentId)
        {
            return aCourseGateway.GetCourseDetailes(departmentId);
        }

        public Course GetTeacherInfo(int teacherId)
        {
            return aCourseGateway.GetTeacherInfo(teacherId);
        }

        public object UnassignCourses()
        {
            int rowAffected = aCourseGateway.UnassignCourses();
            if (rowAffected <= 0 || rowAffected >= 0)
            {
                return "Courses are Unassigned Successfully !!";
            }
            return "Courses Unassigning failed !!";
           
        }

        public List<Teacher> GetTeacherCredit()
        {
            return aCourseGateway.GetTeacherCredit();
        }


        public void UpdateRemainingCr(Teacher teacher)
        {
           aCourseGateway.UpdateRemainingCr(teacher);
        }
    }
}