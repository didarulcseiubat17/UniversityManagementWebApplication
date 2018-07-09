using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Gateway
{
    public class CourseGateway
    {
        private string connString =
            @"Data Source=DESKTOP-UPLTLT5\SQL2012;Initial Catalog=UniversityDb;Integrated Security=True";

        private string query;
        private int rowEffected;
        private SqlConnection connection;
        private SqlDataReader reader;
        private SqlCommand command;
        private bool isExist = false;

        public void UpdateCourseStatus(Course course)
        {
            query = "UPDATE Courses SET CourseStatus='" + course.CourseStatus + "', CourseAssignTo='" +
                    course.CourseAssignTo + "' WHERE Id='" + course.Id + "'";
            connection=new SqlConnection(connString);
            connection.Open();
            command=new SqlCommand(query,connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Course> GetCourseCodeAndCredit(int departmentId)
        {
            query = "SELECT * FROM Courses WHERE DepartmentId = '" + departmentId + "'";
            List<Course> courses = new List<Course>();
            connection = new SqlConnection(connString);
            connection.Open();
            command = new SqlCommand(query, connection);
            reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                Course course = new Course();
                course.CourseCode = reader["CourseCode"].ToString();
                course.CourseCredit = Convert.ToInt32(reader["CourseCredit"].ToString());
                
                courses.Add(course);
            } 
            reader.Close();
            connection.Close();
            return courses;
        } 
    }
}