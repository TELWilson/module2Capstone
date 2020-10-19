using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectOrganizer.DAL
{
    public class EmployeeSqlDAO : IEmployeeDAO
    {
        private string connectionString;

        // SQL Command to get all employees from the employees table
        private string sqlGetAllEmployees = "SELECT * FROM employee;";

        // SQL command to get all employees with the given firstname and lastname
        private string sqlSearch = "SELECT * FROM employee WHERE employee.first_name = @first_name AND employee.last_name = @last_name;";

        // SQL command to get all employees without a project
        private string sqlGetEmployeesWithoutProjects = "SELECT * FROM employee JOIN project_employee ON project_employee.employee_id = employee_id JOIN project ON project.project_id = project_employee.project_id WHERE project_employee.project_id IS NULL;";
        
        // Single Parameter Constructor
        public EmployeeSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        public IList<Employee> GetAllEmployees()
        {
            IList<Employee> employees = new List<Employee>();

            // Create the SQL connection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Open the SQL connection
                conn.Open();

                SqlCommand command = new SqlCommand(sqlGetAllEmployees, conn);

                // Create a reader
                // An exception can be thrown here. IE: Connection string is bad
                SqlDataReader reader = command.ExecuteReader();

                // While the reader has a row to read...
                while (reader.Read())
                {
                    // Create a new instance of department every loop (every line)
                    Employee employee = new Employee();

                    employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                    employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                    employee.FirstName = Convert.ToString(reader["first_name"]);
                    employee.LastName = Convert.ToString(reader["last_name"]);
                    employee.JobTitle = Convert.ToString(reader["job_title"]);
                    employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                    employee.Gender = Convert.ToString(reader["gender"]);
                    employee.HireDate = Convert.ToDateTime(reader["hire_date"]);
                    

                    employees.Add(employee);
                }
            }

            return employees;
        }

        /// <summary>
        /// Searches the system for an employee by first name or last name.
        /// </summary>
        /// <remarks>The search performed is a wildcard search.</remarks>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns>A list of employees that match the search.</returns>
        public IList<Employee> Search(string firstname, string lastname)
        {
            IList<Employee> employees = new List<Employee>();

            // Create the SQL connection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Open the SQL connection
                conn.Open();

                SqlCommand command = new SqlCommand(sqlSearch, conn);

                // We passed a string firstname and lastname to the method
                // We can call the parameters that we need from the passed strings
                // (<name of our variable>, <the value>)
                command.Parameters.AddWithValue("@first_name", firstname);
                command.Parameters.AddWithValue("@last_name", lastname);


                // Create a reader
                // An exception can be thrown here. IE: Connection string is bad
                SqlDataReader reader = command.ExecuteReader();

                // While the reader has a row to read...
                while (reader.Read())
                {
                    // Create a new instance of department every loop (every line)
                    Employee employee = new Employee();

                    employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                    employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                    employee.FirstName = Convert.ToString(reader["first_name"]);
                    employee.LastName = Convert.ToString(reader["last_name"]);
                    employee.JobTitle = Convert.ToString(reader["job_title"]);
                    employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                    employee.Gender = Convert.ToString(reader["gender"]);
                    employee.HireDate = Convert.ToDateTime(reader["hire_date"]);


                    employees.Add(employee);
                }
            }

            return employees;
        }

        /// <summary>
        /// Gets a list of employees who are not assigned to any active projects.
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetEmployeesWithoutProjects()
        {
            IList<Employee> employees = new List<Employee>();

            // Create the SQL connection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Open the SQL connection
                conn.Open();

                SqlCommand command = new SqlCommand(sqlGetEmployeesWithoutProjects, conn);

                // We passed a string firstname and lastname to the method
                // We can call the parameters that we need from the passed strings
                // (<name of our variable>, <the value>)
                command.Parameters.AddWithValue("@first_name", firstname);
                command.Parameters.AddWithValue("@last_name", lastname);


                // Create a reader
                // An exception can be thrown here. IE: Connection string is bad
                SqlDataReader reader = command.ExecuteReader();

                // While the reader has a row to read...
                while (reader.Read())
                {
                    // Create a new instance of department every loop (every line)
                    Employee employee = new Employee();

                    employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                    employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                    employee.FirstName = Convert.ToString(reader["first_name"]);
                    employee.LastName = Convert.ToString(reader["last_name"]);
                    employee.JobTitle = Convert.ToString(reader["job_title"]);
                    employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                    employee.Gender = Convert.ToString(reader["gender"]);
                    employee.HireDate = Convert.ToDateTime(reader["hire_date"]);


                    employees.Add(employee);
                }
            }

            return employees;
        }
    }
}
