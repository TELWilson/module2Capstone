using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ProjectOrganizer.DAL
{
    public class ProjectSqlDAO : IProjectDAO
    {
        private string connectionString;

        // SQL Command to get all projects from the project table
        private string sqlGetAllProjects = "SELECT * FROM project;";

        // SQL Command to create a new project
        private string sqlCreateProject = "SET IDENTITY_INSERT project ON; " +
                                          "INSERT INTO project(project_id, name, from_date, to_date) " + 
                                          "VALUES(@project_id, @name, @from_date, @to_date); " + 
                                          "SET IDENTITY_INSERT project OFF;";

        // SQL Command to create a new project_employee with project_id and employee_id
        private string sqlAssignEmployeeToProject = "INSERT INTO project_employee(employee_id, project_id) VALUES (@employee_id,@project_id);";

        // SQL Command to remove a row from project_employee with matching project_id and employee_id
        private string sqlRemoveEmployeeFromProject = "DELETE FROM project_employee WHERE project_employee.employee_id = @employee_id AND project_employee.project_id = @project_id;";

        // Single Parameter Constructor
        public ProjectSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns></returns>
        public IList<Project> GetAllProjects()
        {
            IList<Project> projects = new List<Project>();

            // Create the SQL connection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Open the SQL connection
                conn.Open();

                SqlCommand command = new SqlCommand(sqlGetAllProjects, conn);

                // Create a reader
                // An exception can be thrown here. IE: Connection string is bad
                SqlDataReader reader = command.ExecuteReader();

                // While the reader has a row to read...
                while (reader.Read())
                {
                    // Create a new instance of department every loop (every line)
                    Project project = new Project();

                    project.ProjectId = Convert.ToInt32(reader["project_id"]);
                    project.Name = Convert.ToString(reader["name"]);
                    project.StartDate = Convert.ToDateTime(reader["from_date"]);
                    project.EndDate = Convert.ToDateTime(reader["to_date"]);

                    projects.Add(project);
                }
            }

            return projects;
        }

        /// <summary>
        /// Assigns an employee to a project using their IDs.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            int count = 0;
            bool isSuccessful = false;

            // Create the SQL connection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                // Open the SQL connection
                conn.Open();

                SqlCommand command = new SqlCommand(sqlAssignEmployeeToProject, conn);

                // We passed a Department object to the method
                // We can call the parameters that we need from the object
                command.Parameters.AddWithValue("@employee_id", employeeId);
                command.Parameters.AddWithValue("@project_id", projectId);

                count = command.ExecuteNonQuery();

                // If the count affects more than 0 lines...
                if (count > 0)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        /// <summary>
        /// Removes an employee from a project.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            int count = 0;
            bool isSuccessful = false;

            // Create the SQL connection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                // Open the SQL connection
                conn.Open();

                SqlCommand command = new SqlCommand(sqlRemoveEmployeeFromProject, conn);

                // We passed a Department object to the method
                // We can call the parameters that we need from the object
                command.Parameters.AddWithValue("@employee_id", employeeId);
                command.Parameters.AddWithValue("@project_id", projectId);

                count = command.ExecuteNonQuery();

                // If the count affects more than 0 lines...
                if (count > 0)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="newProject">The new project object.</param>
        /// <returns>The new id of the project.</returns>
        public int CreateProject(Project newProject)
        {
            int count = 0;

            // Create the SQL connection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                // Open the SQL connection
                conn.Open();

                SqlCommand command = new SqlCommand(sqlCreateProject, conn);

                // We passed a Department object to the method
                // We can call the parameters that we need from the object
                command.Parameters.AddWithValue("@name", newProject.Name);
                command.Parameters.AddWithValue("@project_id", newProject.ProjectId);
                command.Parameters.AddWithValue("@from_date", newProject.StartDate);
                command.Parameters.AddWithValue("@to_date", newProject.EndDate);

                count = command.ExecuteNonQuery();
            }

            return count;
        }

    }
}
