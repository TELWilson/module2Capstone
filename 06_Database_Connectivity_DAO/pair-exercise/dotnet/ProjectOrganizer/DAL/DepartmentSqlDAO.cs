using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace ProjectOrganizer.DAL
{
    public class DepartmentSqlDAO : IDepartmentDAO
    {
        private string connectionString;

        // SQL command to get all departments
        private string sqlGetDepartment = "SELECT * FROM department;";

        // SQL command to add a new department
        private string sqlCreateDepartment = "SET IDENTITY_INSERT department ON; INSERT INTO department(department_id, name) VALUES (@department_id, @name); SET IDENTITY_INSERT department OFF;";

        // SQL command to update a department
        private string sqlUpdateDepartment = "UPDATE department SET name = @name WHERE department.department_id = @department_id;";


        // Single Parameter Constructor
        public DepartmentSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the departments.
        /// </summary>
        /// <returns></returns>
        public IList<Department> GetDepartments()
        {
            IList<Department> departments = new List<Department>();

            // Create the SQL connection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Open the SQL connection
                conn.Open();

                SqlCommand command = new SqlCommand(sqlGetDepartment, conn);

                // Create a reader
                // An exception can be thrown here. IE: Connection string is bad
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Create a new instance of department every loop (every line)
                    Department department = new Department();

                    department.Id = Convert.ToInt32(reader["department_id"]);
                    department.Name = Convert.ToString(reader["name"]);

                    departments.Add(department);
                }
            }

            return departments;
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="newDepartment">The department object.</param>
        /// <returns>The id of the new department (if successful).</returns>
        public int CreateDepartment(Department newDepartment)
        {
            int count = 0;

            // Create the SQL connection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                // Open the SQL connection
                conn.Open();

                SqlCommand command = new SqlCommand(sqlCreateDepartment, conn);

                // We passed a Department object to the method
                // We can call the parameters that we need from the object
                command.Parameters.AddWithValue("@name", newDepartment.Name);
                command.Parameters.AddWithValue("@department_id", newDepartment.Id);

                count = command.ExecuteNonQuery();
            }

            return count;
        }
        
        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="updatedDepartment">The department object.</param>
        /// <returns>True, if successful.</returns>
        public bool UpdateDepartment(Department updatedDepartment)
        {
            int count = 0;
            bool isSuccessful = false;

            // Create the SQL connection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                // Open the SQL connection
                conn.Open();

                SqlCommand command = new SqlCommand(sqlUpdateDepartment, conn);

                // We passed a Department object to the method
                // We can call the parameters that we need from the object
                command.Parameters.AddWithValue("@name", updatedDepartment.Name);
                command.Parameters.AddWithValue("@department_id", updatedDepartment.Id);

                count = command.ExecuteNonQuery();

                // If the count affects more than 0 lines...
                if (count > 0)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

    }
}
