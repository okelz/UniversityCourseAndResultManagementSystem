using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementWebApp.Gateway;
using UniversityCourseAndResultManagementWebApp.Models;

namespace UniversityCourseAndResultManagementWebApp.Manager
{
    public class DepartmentManager
    {
        DepartmentGateway aDepartmentGateway = new DepartmentGateway();
        public string Save(Department aDepartment)
        {
            if (!aDepartmentGateway.IsCodeExists(aDepartment))
            {
                if (!aDepartmentGateway.IsNameExists((aDepartment)))
                {
                    int rowAffected = aDepartmentGateway.Save(aDepartment);
                    if (rowAffected > 0)
                    {
                        return "Department Saved successfully !!";
                    }
                    return "Saving Failed !!";
                }
                return "Name already exists !!";
            }
            return "Code already exists !!";

        }
    }
}