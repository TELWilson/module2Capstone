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

        private string sqlGetDepartment = "SELECT * FROM department;";

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
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="updatedDepartment">The department object.</param>
        /// <returns>True, if successful.</returns>
        public bool UpdateDepartment(Department updatedDepartment)
        {
            throw new NotImplementedException();
        }

    }
}
