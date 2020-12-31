using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementWebApp.Gateway;
using UniversityCourseAndResultManagementWebApp.Models;

namespace UniversityCourseAndResultManagementWebApp.Manager
{
    public class StudentManager
    {
        StudentGateway aStudentGateway = new StudentGateway();
        public object Save(Student aStudent)
        {
            if (!aStudentGateway.IsEmailExists(aStudent))
            {
                int rowAffected = aStudentGateway.Save(aStudent);
                if (rowAffected > 0)
                {
                    return "Student saved successfully !!";
                }
                return "Saving Failed !!"; 
            }
            return "Email alrady Exist!!"; 
        
        }

        public string GetDeptCode(int departmentId)
        {
            return aStudentGateway.GetDeptCode(departmentId);
        }

        public int GetNoOfStudents(string reistrationNo)
        {
            return aStudentGateway.GetNoOfStudents(reistrationNo);
        }

        public string GetDeptName(int departmentId)
        {
            return aStudentGateway.GetDeptName(departmentId);
        }

        public List<Student> GetAllRegistrationNo()
        {
            return aStudentGateway.GetAllRegistrationNo();
        }

        public List<Student> GetCoursesByRegistrationId(int id)
        {
            return aStudentGateway.GetCoursesByRegistrationId(id);
        }

        public Student GetStudentInfo(int id)
        {
            return aStudentGateway.GetStudentInfo(id);
        }

        public string SaveResult(Student aStudent)
        {
            int rowAffected;
            if (aStudentGateway.IsResultExist(aStudent))
            {
                rowAffected = aStudentGateway.UpdateResult(aStudent);
                if (rowAffected > 0)
                {
                    return "Student Result Update successfully !!";
                }
                return "Result Updating Failed !!";  
            }
            else
            {
                rowAffected = aStudentGateway.SaveResult(aStudent);
                if (rowAffected > 0)
                {
                    return "Student Result saved successfully !!";
                }
                return "Result Saving Failed !!";  
            }
            
        }

        public string GetRegNo(int id)
        {
            return aStudentGateway.GetRegNo(id);
        }

        public int GetDeptIdByRegId(int id)
        {
            return aStudentGateway.GetDeptIdByRegId(id);
        }

        public List<Student> GetCoursesByDeptId(int departmentId)
        {
            return aStudentGateway.GetCoursesByDeptId(departmentId);
        }

        public Student GetStudentInfoByRegNo(int id)
        {
            return aStudentGateway.GetStudentInfoByRegNo(id);
        }

        public string EnrollCourse(Student aStudent)
        {
            if (!aStudentGateway.IsAssigned(aStudent))
            {
                int rowAffected = aStudentGateway.EnrollCourse(aStudent);
                if (rowAffected > 0)
                {
                    return "Course Enrolled successfully !!";
                }
                return "Course Enrolling Failed !!";  
            }
            return "Course already Enrolled To this Student !!";
           
        }
    }
}