using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;


namespace Capstone.Tests
{
    [TestClass]
    public class VenueDAOTest : ParentTest
    {
        [TestMethod]
        public void GetVenues_Test()
        {
            //ARRANGE
            VenueDAO venueDAO = new VenueDAO(connectionString);
            bool found = false;


            //ACT
            IList<Venue> venues = venueDAO.GetVenues();

            foreach (Venue venue in venues)
            {
                if (venue.id == 267)
                {
                    found = true;
                    break;
                }
            }

            //ASSERT
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void ListVenue_Test()
        {
            // ARRANGE
            VenueDAO venueDAO = new VenueDAO(connectionString);
            int usersVenueId = 267;

            //ACT
            Venue result = venueDAO.ListVenue(usersVenueId);

            //ASSERT
            Assert.AreEqual("Cigar Party Palace", result.name);
        }
    }
}
