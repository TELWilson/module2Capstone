using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class SpaceDAOTest : ParentTest
    {
        [TestMethod]
        public void GetSpaces_Test()
        {
            // ARRANGE
            SpaceDAO spaceDAO = new SpaceDAO(connectionString);
            int selectedVenueId = 267;
            bool found = false;

            // ACT
            IList<Space> listOfSpaces = spaceDAO.GetSpaces(selectedVenueId);

            foreach (Space space in listOfSpaces)
            {
                if (space.id == 999)
                {
                    found = true;
                    break;
                }
            }

            // ASSERT
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void NumToMonth_Test()
        {
            // ARRANGE
            SpaceDAO spaceDAO = new SpaceDAO(connectionString);
            string monthNum = "1";

            // ACT
            string result = spaceDAO.NumToMonth(monthNum);

            // ASSERT
            Assert.AreEqual("Jan.", result);
        }
    }
}
