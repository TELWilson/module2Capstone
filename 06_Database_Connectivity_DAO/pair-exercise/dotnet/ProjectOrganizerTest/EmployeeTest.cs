using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectOrganizer.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOrganizerTest
{
    [TestClass]
    public class EmployeeTest : ProjectOrganizerMasterTest
    {
        [TestMethod]
        public void GetAllEmployees_ShouldReturnEmployees()
        {
            //Arrange
            EmployeeSqlDAO employee = new EmployeeSqlDAO(ConnectionString);


            //Act
            int result = employee.GetAllEmployees().Count;


            //Assert
            Assert.AreEqual(1, result);
        }
    }
}
