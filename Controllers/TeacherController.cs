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
    public class TeacherController : Controller
    {
        TeacherManager aTeacherManager = new TeacherManager();
        CourseManager aCourseManager = new CourseManager();
        public ActionResult Save()
        {
            List<Department> departments = aCourseManager.GetAllDepartments();
            ViewBag.Departments = departments;
            List<Designation> designations = aTeacherManager.GetAllDesignations();
            ViewBag.Designation = designations;
            return View();
        }
        [HttpPost]
        public ActionResult Save(Teacher aTeacher)
        {
            List<Department> departments = aCourseManager.GetAllDepartments();
            ViewBag.Departments = departments;
            List<Designation> designations = aTeacherManager.GetAllDesignations();
            ViewBag.Designation = designations;
            if (aTeacher.CreditLimit >= 0)
            {
                ViewBag.Message = aTeacherManager.SaveTeacher(aTeacher);
                return View();  
            }
            ViewBag.Message = " Credit to be taken field must contain a non-negative value !!";
            return View();

        }
	}
}