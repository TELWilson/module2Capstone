using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOrganizerTest
{
    [TestClass]
    public class DepartmentTest : ProjectOrganizerMasterTest
    {
        [TestMethod]
        public void CreateDepartmentAddsADepartment()
        {
            // Arrange
            DepartmentSqlDAO department = new DepartmentSqlDAO(ConnectionString);

            Department newDepartment = new Department();
            newDepartment.Name = "GoBucks";

            // Act
            int result = department.CreateDepartment(newDepartment);


            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
