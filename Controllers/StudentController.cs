using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using UniversityCourseAndResultManagementWebApp.Manager;
using UniversityCourseAndResultManagementWebApp.Menager;
using UniversityCourseAndResultManagementWebApp.Models;

namespace UniversityCourseAndResultManagementWebApp.Controllers
{
    public class StudentController : Controller
    {
        StudentManager aStudentManager = new StudentManager();
        CourseManager aCourseManager = new CourseManager();
        //
        // GET: /Student/
        public ActionResult Register()
        {
            List<Department> departments = aCourseManager.GetAllDepartments();
            ViewBag.Departments = departments;
            return View();
        }
        [HttpPost]
        public ActionResult Register(Student aStudent)
        {
            ViewBag.Message = DateTime.Now.Year.ToString();
            List<Department> departments = aCourseManager.GetAllDepartments();
            ViewBag.Departments = departments;
           
            aStudent.RegistrationNo = GetRegistrtionNo(aStudent);
            ViewBag.Message = aStudentManager.Save(aStudent);
            aStudent.DepartmentName = aStudentManager.GetDeptName(aStudent.DepartmentId);
            ViewBag.Student = aStudent;
            return View();
        }

        private string GetRegistrtionNo(Student aStudent)
        {
            string deptCode = aStudentManager.GetDeptCode(aStudent.DepartmentId);
            var test = Convert.ToDateTime(aStudent.Date);
            int year = test.Year;
            string registrationNo = deptCode + "-" + year;
            string code = aStudentManager.GetNoOfStudents(registrationNo).ToString();
            if (code.Length == 1)
            {
                code = "00" + code;
            }
            else if (code.Length == 2)
            {
                code = "0" + code;
            }
            registrationNo = registrationNo + "-" + code;
            return registrationNo;
            
        }

        public ActionResult SaveResult()
        {
            List<Student> registrationNos = aStudentManager.GetAllRegistrationNo();
            ViewBag.RegistrationNos = registrationNos;
            return View();
        }
        [HttpPost]
        public ActionResult SaveResult(Student aStudent)
        {
            List<Student> registrationNos = aStudentManager.GetAllRegistrationNo();
            aStudent.RegistrationNo = aStudentManager.GetRegNo(aStudent.Id);
            ViewBag.RegistrationNos = registrationNos;
            ViewBag.Message = aStudentManager.SaveResult(aStudent);
            return View();
        }
     

         public JsonResult GetStudentInfo(int id)
        {
            Student student = aStudentManager.GetStudentInfo(id);
            return Json(student);
        }
         public JsonResult GetCoursesByStudentId(int id)
         {
             List<Student> courses = aStudentManager.GetCoursesByRegistrationId(id);
             return Json(courses);
         }
         public ActionResult EnrollCourse()
         {
             List<Student> registrationNos = aStudentManager.GetAllRegistrationNo();
             ViewBag.RegistrationNos = registrationNos;
             return View();
         }
         [HttpPost]
         public ActionResult EnrollCourse(Student aStudent)
         {
             List<Student> registrationNos = aStudentManager.GetAllRegistrationNo();
             ViewBag.RegistrationNos = registrationNos;
             aStudent.RegistrationNo = aStudentManager.GetRegNo(aStudent.Id);
             ViewBag.MEssage = aStudentManager.EnrollCourse(aStudent);
             return View();
         }
         public JsonResult GetCoursesByRegId(int id)
         {
             int departmentId = aStudentManager.GetDeptIdByRegId(id);
             List<Student> courses = aStudentManager.GetCoursesByDeptId(departmentId);
             return Json(courses);
         }
         public JsonResult GetStudentInfoByRegNo(int id)
         {
             Student student = aStudentManager.GetStudentInfoByRegNo(id);
             return Json(student);
         }
       
    }
}