using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementWebApp.Manager;
using UniversityCourseAndResultManagementWebApp.Menager;
using UniversityCourseAndResultManagementWebApp.Models;

namespace UniversityCourseAndResultManagementWebApp.Controllers
{
    public class CourseController : Controller
    {
        TeacherManager aTeacherManager = new TeacherManager();
        CourseManager aCourseManager = new CourseManager();
        public ActionResult Save()
        {
            List<Department> departments = aCourseManager.GetAllDepartments();
            ViewBag.Departments = departments;
            List<Semester> semesters = aCourseManager.GetAllSemesters();
            ViewBag.Semesters = semesters;
            
            return View();
        }
        public ActionResult Assign()
        {
            List<Department> departments = aCourseManager.GetAllDepartments();
            ViewBag.Departments = departments;
            return View();
        }
        public ActionResult ViewCourse()
        {
            List<Department> departments = aCourseManager.GetAllDepartments();
            ViewBag.Departments = departments;
            return View();
        }
        [HttpPost]

        public ActionResult Save(Course aCourse)
        {
            List<Department> departments = aCourseManager.GetAllDepartments();
            ViewBag.Departments = departments;
            List<Semester> semesters = aCourseManager.GetAllSemesters();
            ViewBag.Semesters = semesters;
            if (aCourse.Credit >= 0.5 && aCourse.Credit <= 5)
            {
                ViewBag.Message = aCourseManager.SaveCourse(aCourse);
                return View();
            }
            ViewBag.Message = "Credit cannot be less than 0.5 and more than 5.0 !!";
            return View();
            
        }
        

        [HttpPost]
        public ActionResult Assign(Course aCourse)
        {
            List<Department> departments = aCourseManager.GetAllDepartments();

            ViewBag.Departments = departments;
            //aCourse.RemainingCredit = aCourse.RemainingCredit - aCourse.Credit;
            ViewBag.Message = aCourseManager.SaveCourseAssign(aCourse);
            if (ViewBag.Message == "Course assigned Successfully !!")
             {
                Teacher teacher = aTeacherManager.GetTeacherInfo(aCourse.TeacherId);
                Course course = aCourseManager.GetCourseInfo(aCourse.CourseId);
                aCourse.RemainingCredit = teacher.RemainingCredit - course.Credit;
                aCourseManager.UpdateRemainingCredit(aCourse);
             }
                
            
            return View();
        }

        public JsonResult GetTeachersByDepartmentId(int departmentId)
        {
            List<Teacher> teachers = aTeacherManager.GetTeachersByDepartmentId(departmentId);
            return Json(teachers);
        }
        public JsonResult GetCoursesByDepartmentId(int departmentId)
        {
            List<Course> courses = aTeacherManager.GetCoursesByDepartmentId(departmentId);
            return Json(courses);
        }
        public JsonResult GetTeacherInfo(int teacherId)
        {
            Teacher aTeacher = aTeacherManager.GetTeacherInfo(teacherId);
            return Json(aTeacher);
        }
        public JsonResult GetCourseInfo(int courseId)
        {
            Course aCourse = aCourseManager.GetCourseInfo(courseId);
            return Json(aCourse);
        }
        public JsonResult GetCourseDetailes(int departmentId)
        {
            List<ViewModel> viewModels = aCourseManager.GetCourseDetailes(departmentId);
            return Json(viewModels);
        }
        public ActionResult UnassignCourse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnassignCourse(string confirmValue)
        {
            if (confirmValue == "Yes")
            {
                List<Teacher> teachers = aCourseManager.GetTeacherCredit();
                foreach (Teacher teacher in teachers)
                {
                    aCourseManager.UpdateRemainingCr(teacher);
                }
                ViewBag.Message = aCourseManager.UnassignCourses();
            }
            else
            {
                ViewBag.Message = "You clicked NO!";
            }
 
            return View();
        }
	}
}