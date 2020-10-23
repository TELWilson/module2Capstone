using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationDAOTest : ParentTest
    {
        [TestMethod]
        public void GetReservations_Test()
        {
            // ARRANGE
            IReserveDAO reservation = new ReservationDAO(connectionString);
            bool found = false;

            // ACT
            IList<Reservation> reservations = reservation.GetReservations();

            foreach (Reservation reserve in reservations)
            {
                if (reserve.reservation_id == 400)
                {
                    found = true;
                    break;
                }
            }

            // ASSERT
            Assert.IsTrue(found);

        }

        [TestMethod]
        public void CheckAvailability_Test()
        {
            // ARRANGE
            int venue_id = 267;
            int numOfAttendees = 80;
            DateTime start_date = new DateTime(2020, 10, 25);
            int numOfDays = 2;

            IReserveDAO reservation = new ReservationDAO(connectionString);
            bool found = false;

            // ACT
            IList<Reservation> reservations = reservation.CheckAvailability(venue_id, numOfAttendees, start_date, numOfDays);

            foreach (Reservation reserve in reservations)
            {
                if (reserve.space_id == 999)
                {
                    found = true;
                    break;
                }
            }

            // ASSERT
            Assert.IsTrue(found);

        }

        [TestMethod]
        public void MakeReservation_Test()
        {
            // ARRANGE
            IReserveDAO reservation = new ReservationDAO(connectionString);

            int venue_id = 267;
            int numOfAttendees = 1;
            DateTime start_date = new DateTime(2020, 10, 25);
            int numOfDays = 1;
            int intUserSpaceID = 999;
            string userName = "Test Person";

            // ACT
            int count = reservation.MakeReservation(venue_id, numOfAttendees, start_date, numOfDays, intUserSpaceID, userName);

            // ASSERT
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void GetLastReservationDetails_Test()
        {
            // ARRANGE
            IReserveDAO reservation = new ReservationDAO(connectionString);

            int venue_id = 267;
            int numOfAttendees = 1;
            DateTime start_date = new DateTime(2020, 10, 25);
            int numOfDays = 1;
            int intUserSpaceID = 999;
            string userName = "Test Person";

            // ACT
            int count = reservation.MakeReservation(venue_id, numOfAttendees, start_date, numOfDays, intUserSpaceID, userName);
            Reservation lastRes = reservation.GetLastReservationDetails(1);

            // ASSERT
            Assert.AreEqual("Test Person", lastRes.reserved_for);
        }
    }
}
