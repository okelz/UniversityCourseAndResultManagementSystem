using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementWebApp.Gateway;
using UniversityCourseAndResultManagementWebApp.Models;

namespace UniversityCourseAndResultManagementWebApp.Manager
{
    public class TeacherManager
    {
        TeacherGateway aTeacherGateway = new TeacherGateway();
        public List<Designation> GetAllDesignations()
        {
            return aTeacherGateway.GetAllDesignations();
        }

        public object SaveTeacher(Teacher aTeacher)
        {
            if (!aTeacherGateway.IsEmailExists(aTeacher))
            {
                int RowAffected = aTeacherGateway.SaveTeacher(aTeacher);
                if (RowAffected > 0)
                {
                    return "Teacher Saved Successfully !!";
                }
                return "Saving Failed!!";
            }
            return "Email already Exist!!"; 
        }

      

        public List<Teacher> GetTeachersByDepartmentId(int departmentId)
        {
            return aTeacherGateway.GetTeachersByDepartmentId(departmentId);
        }

        public List<Course> GetCoursesByDepartmentId(int departmentId)
        {
            return aTeacherGateway.GetCoursesByDepartmentId(departmentId);
        }

        public Teacher GetTeacherInfo(int teacherId)
        {
            return aTeacherGateway.GetTeacherInfo(teacherId);
        }
    }
}