using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Gateway;

namespace UniversityManagementSystemApp.Manager
{
    public class DepartmentManager
    {
        DepartmentGateway departmentGateway = new DepartmentGateway();
        private bool isExist = false;
        public bool IsExistDepartmentName(string DeptName)
        {
            return departmentGateway.IsExistDepartmentName(DeptName);
        }

        public bool IsExistDepartmentCode(string DeptCode)
        {
            return departmentGateway.IsExistDepartmentCode(DeptCode);
        }
    }
}