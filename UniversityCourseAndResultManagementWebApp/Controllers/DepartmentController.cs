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
    public class DepartmentController : Controller
    {
        DepartmentManager aDepartmentManager = new DepartmentManager();
        CourseManager aCourseManager = new CourseManager();
        //
        // GET: /Department/
        public ActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(Department aDepartment)
        {
            ViewBag.Message = aDepartmentManager.Save(aDepartment);
            return View();
        }
        public ActionResult ViewDepartments ()
        {
            List<Department> departments = aCourseManager.GetAllDepartments();
            ViewBag.Departments = departments;
            return View();
        }
	}
}