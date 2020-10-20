using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOrganizerTest
{
    [TestClass]
    public class ProjectTest : ProjectOrganizerMasterTest
    {
        [TestMethod]
        public void CreateProject_ShouldReturnAProject()
        {
            // Arrange
            ProjectSqlDAO project = new ProjectSqlDAO(ConnectionString);

            Project newProject = new Project();
            newProject.Name = "TestProject";
            //newProject.ProjectId = 2;
            newProject.StartDate = new DateTime(2020, 10, 20);
            newProject.EndDate = new DateTime(2020, 10, 21);
            int startingRowCount = GetRowCount("project");

            // Act
            project.CreateProject(newProject);
            int endingRowCount = GetRowCount("project");

            // Assert
            Assert.AreNotEqual(startingRowCount, endingRowCount);
        }
    }
}
