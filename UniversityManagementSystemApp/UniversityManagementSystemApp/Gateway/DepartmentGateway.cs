using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Gateway
{
    public class DepartmentGateway
    {
        private string connString =
            @"Data Source=DESKTOP-UPLTLT5\SQL2012;Initial Catalog=UniversityDb;Integrated Security=True";

        private string query;
        private int rowEffected;
        private SqlConnection connection;
        private SqlDataReader reader;
        private SqlCommand command;
        private bool isExist = false;

        public bool IsExistDepartmentName(string DeptName)
        {
            query = "SELECT DeptName FROM Departments WHERE DeptName='" + DeptName + "'";
            connection = new SqlConnection(connString);
            connection.Open();
            command = new SqlCommand(query, connection);
            reader = command.ExecuteReader();
            isExist = reader.HasRows;
            reader.Close();
            connection.Close();

            return isExist;
        }

        public bool IsExistDepartmentCode(string DeptCode)
        {
            query = "SELECT DeptCode FROM Departments WHERE DeptCode='" + DeptCode + "'";
            connection = new SqlConnection(connString);
            connection.Open();
            command = new SqlCommand(query, connection);
            reader = command.ExecuteReader();
            isExist = reader.HasRows;
            reader.Close();
            connection.Close();

            return isExist;
        }
    }
}